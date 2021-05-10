using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;

using Node.Lib.Utility.Zip;

namespace Node.Lib.Utility
{
    /// <summary>
    /// Represents WinZip class. This class is used to zip an unzip files, folds.
    /// </summary>
    public class WinZip
    {
        private string zipFileName = null;
        private FastZip zip = null;

        public WinZip()
        {
        }

        /// <summary>
        /// Initializes a WinZip object.
        /// </summary>
        /// <param name="zipFileName">The zipped file name.</param>
        public WinZip(string zipFileName)
        {
            this.zipFileName = zipFileName;
            this.zip = new FastZip();
        }

        /// <summary>
        /// Create a zip file by specified source directory.
        /// </summary>
        /// <param name="sourceDirectory">The source directory will be compressed.</param>
        public void CreateZip(string sourceDirectory)
        {
            zip.CreateZip(this.zipFileName, sourceDirectory, true, "");
        }

        /// <summary>
        /// Extract a zipped filed to specified target directory.
        /// </summary>
        /// <param name="targetDirectory">The specified target directory to store decompressed files.</param>
        public void ExtractZip(string targetDirectory)
        {
            zip.ExtractZip(this.zipFileName, targetDirectory, "");
        }

        /// <summary>
        /// Create a zip.
        /// </summary>
        /// <param name="ht">The Hashtable contains the data to be compressed. The key is file name and value is byte[] content.</param>
        /// <returns>A byte[] object contains compressed data.</returns>
        public byte[] CreateZip(Hashtable ht)
        {
            if (ht == null || ht.Count == 0)
                return null;

            MemoryStream ms = new MemoryStream();
            ZipOutputStream zout = new ZipOutputStream(ms);

            foreach (string name in ht.Keys)
            {
                byte[] buffer = (byte[])ht[name];
                ZipEntry entry = new ZipEntry(name);
                zout.PutNextEntry(entry);
                zout.Write(buffer, 0, buffer.Length);
            }
            zout.Finish();

            byte[] bout = new byte[zout.Length];
            for (int i = 0; i < bout.Length; i++)
                bout[i] = ms.GetBuffer()[i];

            zout.Close();
            return bout;
        }

        /// <summary>
        /// Extract a zipped data.
        /// </summary>
        /// <param name="zipBuffer">The compressed data. The subfolder in compressed data is not supported.</param>
        /// <returns>A Hashtable contains decompresed data. The key is file name and value is byte[] content.</returns>
        public Hashtable ExtractZip(byte[] zipBuffer)
        {
            if (zipBuffer == null)
                return null;

            Hashtable ht = new Hashtable();
            MemoryStream ms = new MemoryStream(zipBuffer);
            ZipInputStream zin = new ZipInputStream(ms);

            ZipEntry entry = null;
            while ((entry = zin.GetNextEntry()) != null)
            {
                //if (!entry.IsFile)
                //    continue;
                
                //byte[] buffer = new byte[entry.Size];
                //int count = 0;
                //int size = 0;
                //do
                //{
                //    count = zin.Read(buffer, size, (int)entry.Size);
                //    size += count;
                //} while (count > 0);
                //ht.Add(entry.Name, buffer);
                byte[] content = null;

                if (entry.Size > 0)
                {
                    content = new byte[entry.Size];
                    //zis.Read(content, 0, content.Length);
                    int count = 0;
                    int size = 0;
                    do
                    {
                        count = zin.Read(content, size, (int)entry.Size);
                        size += count;
                    } while (count > 0);
                }
                else
                {
                    MemoryStream ms2 = new MemoryStream();
                    byte[] buffer = new byte[2048];
                    int count = 0;
                    while ((count = zin.Read(buffer, 0, 2048)) > 0)
                        ms2.Write(buffer, 0, count);
                    ms2.Position = 0;
                    content = new byte[(int)ms2.Length];
                    ms2.Read(content, 0, content.Length);
                }
                ht.Add(entry.Name, content);

            }
            zin.Close();
            return ht;
        }

        /// <summary>
        /// Extract the first file in the Zipped file and convert to string.
        /// </summary>
        /// <param name="zipBuffer">The compressed data. The subfolder in compressed data is not supported.</param>
        /// <returns>A Hashtable contains decompresed data. The key is file name and value is byte[] content.</returns>
        public string ExtractZipToString(byte[] zipBuffer)
        {
            string output = "";
            Hashtable ht = ExtractZip(zipBuffer);

            if (ht != null)
            {
                IDictionaryEnumerator idc = ht.GetEnumerator();
                idc.Reset();
                idc.MoveNext();
                byte[] content = idc.Value as byte[];

                MemoryStream ms = new MemoryStream(content);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms, true);
                output = sr.ReadToEnd();
                ms.Close();
                sr.Close();
            }

            return output;
        }
    }
}
