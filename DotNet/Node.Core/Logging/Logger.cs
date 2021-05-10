using System;
using System.IO;

namespace Node.Core.Logging
{
    /// <summary>
    /// Logger is used to catch any exception throw by system when database connection is not existed.
    /// The exception will be logged into local text file specified by Node Application in Web.Config.
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// The Constant value for LEVEL_FATAL 
        /// </summary>
        public const int LEVEL_FATAL = 1;
        /// <summary>
        /// The Constant value for LEVEL_ERROR 
        /// </summary>
        public const int LEVEL_ERROR = 2;
        /// <summary>
        /// The Constant value for LEVEL_WARN 
        /// </summary>
        public const int LEVEL_WARN = 3;
        /// <summary>
        /// The Constant value for LEVEL_INFO 
        /// </summary>
        public const int LEVEL_INFO = 4;
        /// <summary>
        /// The Constant value for LEVEL_DEBUG 
        /// </summary>
        public const int LEVEL_DEBUG = 5;

        private string Path = null;
        private int Level = LEVEL_FATAL;
        /// <summary>
        /// Constructor of Logger
        /// </summary>
        /// <param name="path">Physical file Location of logger's output.</param>
        /// <param name="level">Specified level of logging</param>
        public Logger(string path, int level)
        {
            this.Path = path;
            this.Level = level;
            Init();
        }
        /// <summary>
        /// Constructor of Logger
        /// The default value will be use for Path and log Level.
        /// </summary>
        public Logger()
        {
            this.Path = Phrase.LoggerPath;
            this.Level = Phrase.LoggerLevel;
            Init();
        }
        /// <summary>
        /// Add text message and log level to Logger.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public void Log(string message, int level)
        {
            if (level <= this.Level)
            {
                StreamWriter sw = new StreamWriter(new FileStream(this.Path, FileMode.Append, FileAccess.Write));
                sw.WriteLine(message);
                sw.WriteLine();
                sw.Close();
            }
        }
        /// <summary>
        /// Add status, text message and log level to Logger. 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="level"></param>
        public void Log(string status, string message, int level)
        {
            if (level <= this.Level)
            {
                StreamWriter sw = new StreamWriter(new FileStream(this.Path, FileMode.Append, FileAccess.Write));
                sw.WriteLine("[" + DateTime.Now + "]");
                sw.WriteLine(status + ": " + message);
                sw.WriteLine();
                sw.Close();
            }
        }
        /// <summary>
        /// Add Exception to Logger.
        /// </summary>
        /// <param name="e"><see cref="System.Exception">e</see></param>
        public void Log(Exception e)
        {
            if (this.Level > Logger.LEVEL_FATAL)
            {
                StreamWriter sw = new StreamWriter(new FileStream(this.Path, FileMode.Append, FileAccess.Write));
                sw.WriteLine("[" + DateTime.Now + "]");
                sw.WriteLine(e.ToString());
                sw.Close();
            }
        }

        private void LogRecursiveExceptions(StreamWriter sw, Exception e)
        {
            if (e == null)
                return;
            this.LogRecursiveExceptions(sw, e.InnerException);
            sw.WriteLine(e.ToString());
            sw.WriteLine(e.StackTrace);
        }

        private void Init()
        {
            FileInfo fileInfo = new FileInfo(this.Path);
            if (!fileInfo.Exists)
                fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + this.Path);
            if (!fileInfo.Exists)
                fileInfo = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"logs\" + fileInfo.Name);
            if (!fileInfo.Exists)
            {
                if (!Directory.Exists(fileInfo.DirectoryName))
                    Directory.CreateDirectory(fileInfo.DirectoryName);
                FileStream fs = File.Create(fileInfo.FullName);
                fs.Close();
            }

            this.Path = fileInfo.FullName;
            try
            {
                //make sure to set it as writable.
                if ((File.GetAttributes(this.Path) & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    File.SetAttributes(this.Path, FileAttributes.Normal);
            }
            catch { }
        }
    }
}
