package Node.Utils;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.ByteArrayInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FilenameFilter;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.PrintWriter;
import java.math.BigDecimal;
import java.math.BigInteger;
import java.net.URL;
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.Date;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.text.CharacterIterator;
import java.text.SimpleDateFormat;
import java.text.StringCharacterIterator;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Calendar;
import java.util.Enumeration;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.Properties;
import java.util.TimeZone;
import java.util.Vector;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;
import java.util.zip.ZipOutputStream;

import javax.servlet.http.HttpServletRequest;

import net.exchangenetwork.schema.ends.x2.EncodingType;
import net.exchangenetwork.schema.ends.x2.NetworkNodeListType;
import net.exchangenetwork.schema.ends.x2.NetworkNodeType;
import net.exchangenetwork.schema.ends.x2.NetworkNodesDocument;
import net.exchangenetwork.schema.ends.x2.NodeBoundingBoxType;
import net.exchangenetwork.schema.ends.x2.NodeMethodTypeCode;
import net.exchangenetwork.schema.ends.x2.ObjectPropertyType;
import net.exchangenetwork.schema.ends.x2.RequestParameterType;
import net.exchangenetwork.schema.ends.x2.ServiceDescriptionListType;
import net.exchangenetwork.schema.ends.x2.ServiceDescriptionListType.Service;
import net.sf.json.JSONArray;
import net.sf.json.JSONObject;
import oracle.sql.CLOB;

import org.apache.axis2.context.MessageContext;
import org.apache.axis2.transport.http.HTTPConstants;
import org.apache.commons.net.PrintCommandListener;
import org.apache.commons.net.ftp.FTP;
import org.apache.commons.net.ftp.FTPClient;
import org.apache.commons.net.ftp.FTPReply;
import org.apache.log4j.Level;
import org.dom4j.Attribute;
import org.dom4j.io.SAXReader;

import sun.misc.BASE64Decoder;
import sun.misc.BASE64Encoder;
import EnfoTech.Task.Xtask;
import Node.Phrase;
import Node.API.NodeUtils;
import Node.API.PropertyFiles;
import Node.Biz.Administration.Operation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;
import Node.DB.Interfaces.Configuration.IConfigurationMgr;
import Node.DB.Interfaces.Configuration.IGetServices;
import Node.DB.Interfaces.Configuration.ISystemConfiguration;
import Node.DB.Interfaces.Configuration.ITaskConfiguration;
import Node.WebServices.Document.ClsNodeDocument;
import Node.WebServices.Requestor.NodeRequestor;

import com.enfotech.basecomponent.jndi.JNDIAccess;
import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlElement;
import com.jcraft.jsch.Channel;
import com.jcraft.jsch.ChannelSftp;
import com.jcraft.jsch.JSch;
import com.jcraft.jsch.Session;
import com.jcraft.jsch.SftpException;
/**
 * <p>This class create Utility.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class Utility {
	/**
	 *Constructor
	 * @param 
	 * @return 
	 */
	public Utility() {
	}

	/**
	 * GetNowDate
	 * @param 
	 * @return Date
	 */
	public static Date GetNowDate ()
	{
		Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		SimpleDateFormat f = new SimpleDateFormat("yyyy-MM-dd");
		String temp = f.format(cal.getTime());
		return java.sql.Date.valueOf(temp);
	}

	/**
	 * GetSysTimeString
	 * @param 
	 * @return String
	 */
	public static String GetSysTimeString ()
	{
		Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		SimpleDateFormat f = new SimpleDateFormat("yyyyMMddhhmmss");
		String temp = f.format(cal.getTime());
		return temp;
	}

	/**
	 * GetSysTimeString
	 * @param format
	 * @return String
	 */
	public static String GetSysTimeString (String format)
	{
		Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		SimpleDateFormat f = new SimpleDateFormat(format);
		String temp = f.format(cal.getTime());
		return temp;
	}

	/**
	 * ChangeStringToDate
	 * @param date
	 * @return Date
	 */
	public static Date ChangeStringToDate (String date)
	{
		if(date!=null){
			return Date.valueOf(date);		  
		}else return null;
	}

	/**
	 * GetNowTimeStamp
	 * @param 
	 * @return Timestamp
	 */
	public static Timestamp GetNowTimeStamp ()
	{
		Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		return new Timestamp(cal.getTime().getTime());
	}

	/**
	 * GetNowTimeStamp
	 * @param date
	 * @return Date
	 */
	public static Date GetMMDDYYYYDate (String date)
	{
		Date tmpDate = Utility.ChangeStringToDate(date);
		SimpleDateFormat f = new SimpleDateFormat("MMDDYYYY");
		String temp = f.format(tmpDate);
		return java.sql.Date.valueOf(temp);
	}

	/**
	 * getCalendarDate
	 * @param date
	 * @return Calendar
	 */
	public static Calendar getCalendarDate(String date) throws Exception {
		SimpleDateFormat df = new SimpleDateFormat("MM/dd/yyyy");
		java.util.Date dt = null;
		dt = df.parse(date);
		Calendar cal = Calendar.getInstance();
		cal.setTime(dt);
		return cal;
	}

	/**
	 * getCalendarDate
	 * @param date
	 * @param format
	 * @return Calendar
	 */
	public static Calendar getCalendarDate(String date, String format) throws Exception {
		SimpleDateFormat df = new SimpleDateFormat(format);
		java.util.Date dt = null;
		dt = df.parse(date);
		Calendar cal = Calendar.getInstance();
		cal.setTime(dt);
		return cal;
	}

	/**
	 * format
	 * @param dateTime
	 * @return Date
	 */
	public static java.util.Date format(String dateTime) throws Exception {
		java.util.Date d = null;
		try {
			SimpleDateFormat formatter = new SimpleDateFormat("MM/dd/yyyy");
			d = formatter.parse(dateTime);
		}
		catch (Exception e) {
			throw e;
		}
		return d;
	}

	/**
	 * formatTimeStamp
	 * @param timeStamp
	 * @param dateFormat
	 * @return String
	 */
	public static String formatTimeStamp(Timestamp timeStamp, String dateFormat){
		String stime = "";
		SimpleDateFormat formatter = new SimpleDateFormat(dateFormat);
		stime = formatter.format(timeStamp);
		return stime;
	}

	/**
	 * formatDate
	 * @param date
	 * @param dateFormat
	 * @return String
	 */
	public static String formatDate(Date date, String dateFormat){
		String sDate = "";
		SimpleDateFormat formatter = new SimpleDateFormat(dateFormat);
		sDate = formatter.format(date);
		return sDate;
	}

	/**
	 * transforXMLDateToOralceTimeStamp
	 * @param xml Date format
	 * @return Oracle timestamp
	 */
	public static String transforXMLDateToOralceTimeStamp (String xmlDate)
	{
		String tempDate = xmlDate.replaceAll("T", " ");
		tempDate = tempDate.substring(0, tempDate.lastIndexOf("-"));

		/*int year = Integer.parseInt(tempDate.substring(0, tempDate.indexOf("-")));	
		int month = Integer.parseInt(tempDate.substring(tempDate.indexOf("-")+1, tempDate.lastIndexOf("-")));	
		int day = Integer.parseInt(tempDate.substring(tempDate.lastIndexOf("-")+1, tempDate.indexOf(" ")));	
		int hh = Integer.parseInt(tempDate.substring(tempDate.indexOf(" ")+1, tempDate.indexOf(":")));	
		int mm = Integer.parseInt(tempDate.substring(tempDate.indexOf(":")+1, tempDate.lastIndexOf(":")));	
		int ss = Integer.parseInt(tempDate.substring(tempDate.lastIndexOf(":")+1));	
		Calendar cal = Calendar.getInstance();
		cal.set( Calendar.YEAR, year );
		cal.set( Calendar.MONTH, month );
		cal.set( Calendar.DAY_OF_MONTH, day );
		cal.set( Calendar.HOUR, hh );
		cal.set( Calendar.MINUTE, mm );
		cal.set( Calendar.SECOND, ss );
		Timestamp ts = new Timestamp( cal.getTime().getTime() );*/

		//Timestamp ts = Timestamp.valueOf(tempDate); 

		return tempDate;
	}
	/**
	 * GetDocType
	 * @param name
	 * @return String
	 */
	public static String GetDocType (String name) {
		String retString = null;
		if (name != null) {
			int index = name.indexOf(".");
			if (index >= 0 && name.length() > index + 1) {
				String temp = name.substring(index+1);
				if (temp != null) {
					for (int i = 0; i < Phrase.DOC_TYPES.length; i++) {
						if (temp.equalsIgnoreCase(Phrase.DOC_TYPES[i])) {
							retString = Phrase.DOC_TYPES[i];
							break;
						}
					}
				}
			}
		}
		return retString;
	}

	/**
	 * ParseOperationString
	 * @param op
	 * @return String[]
	 */
	public static String[] ParseOperationString (String op)
	{
		String[] retArray = null;
		int wsIndex = op.indexOf(": ");
		int opIndex = op.indexOf(".");
		if (wsIndex >= 0 && opIndex >= 0 && op.length() > opIndex + 1) {
			retArray = new String [2];
			retArray[0] = op.substring(wsIndex+2,opIndex);
			retArray[1] = op.substring(opIndex+1);
		}
		return retArray;
	}

	/**
	 * GeneratePassword
	 * @param 
	 * @return String
	 */
	public static String GeneratePassword () throws Exception
	{
		String retString = null;
		NodeUtils utils = new NodeUtils();
		String temp = utils.GenerateTransID(Phrase.MD5).substring(0,10);
		int count = 0;
		for (int i = 0; i < temp.length(); i++) {
			char c = temp.charAt(i);
			if (Character.isLetter(c)) {
				if (count > 0) {
					char newChar = Character.toUpperCase(c);
					if (i < temp.length() - 1)
						retString = temp.substring(0,i) + newChar + temp.substring(i+1);
					else
						retString = temp.substring(0,i) + newChar;
					count++;
					break;
				}
				else
					count++;
			}
		}
		if (count < 2)
			return Utility.GeneratePassword();
		return retString;
	}

	/**
	 * changeStringArrayToJsonString
	 * @param aList
	 * @param name
	 * @return String
	 */
	public static String changeStringArrayToJsonString(String[] aList, String name){
		String records = "";
		String tmp = "";
		List jsonList = new ArrayList();

		for(int i=0;i<aList.length;i++){
			jsonList.add("{id:"+i+",\""+name+"\":"+"\""+aList[i]+"\"}");
		}
		JSONArray jsonArray = JSONArray.fromObject(jsonList);           

		Map map = new HashMap();
		//map.put("records", opList);
		map.put("results", jsonArray);
		JSONObject jsonObject = JSONObject.fromObject(map);
		records = jsonObject.toString();
		records = records.substring(records.indexOf("{") + 1);
		records = "{\"total\":"+ aList.length +","	+ records;

		return records;
	}

	/**
	 * checkEmail
	 * @param mail
	 * @return boolean
	 */
	public static boolean checkEmail(String mail){  
		String regex = "\\w+([-+.]\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*" ;  
		Pattern p = Pattern.compile(regex);  
		Matcher m = p.matcher(mail);  
		return m.find();  
	}  

	/**
	 * getWebservicePath
	 * @param 
	 * @return String
	 */
	public static String getWebservicePath() {
		// Get clienthost from the Axis Servlet Request
		MessageContext msgContext = MessageContext.getCurrentMessageContext();
		HttpServletRequest req = (HttpServletRequest) msgContext
		.getProperty(HTTPConstants.MC_HTTP_SERVLETREQUEST);
		String webservicePath = req.getContextPath();
		//		System.out.println("The path is: " + webservicePath);
		return webservicePath;
	}

	/**
	 * mapStatusMessage
	 * @param status
	 * @return String
	 */
	public static String mapStatusMessage(String status) {

		if(status.equalsIgnoreCase(Phrase.ReceivedStatus)) return Phrase.ReceivedMessage;
		else if(status.equalsIgnoreCase(Phrase.CompleteStatus)) return Phrase.CompleteMessage;
		else if(status.equalsIgnoreCase(Phrase.FailedStatus)) return Phrase.FailedMessage;
		else if(status.equalsIgnoreCase(Phrase.ProcessingStatus)) return Phrase.ProcessingMessage;
		else if(status.equalsIgnoreCase(Phrase.PendingStatus)) return Phrase.PendingMessage;
		else if(status.equalsIgnoreCase(Phrase.ApprovedStatus)) return Phrase.ApprovedMessage;
		else if(status.equalsIgnoreCase(Phrase.ProcessedStatus)) return Phrase.ProcessedMessage;
		else if(status.equalsIgnoreCase(Phrase.WarningStatus)) return Phrase.WarningMessage;
		else if(status.equalsIgnoreCase(Phrase.CanceledStatus)) return Phrase.CanceledMessage;
		else if(status.equalsIgnoreCase(Phrase.UnknownStatus)) return Phrase.UnknownMessage;
		else return null;

	}

	/**
	 * readFile
	 * @param filePath
	 * @return byte[]
	 */
	public static byte[] readFile(String filePath) {
		try {
			int byteread = 0;
			int rowCounter = 0;
			int beforeLastByte = 0;
			File oldfile = new File(filePath);
			if (oldfile.exists()) {
				InputStream inStream = new FileInputStream(filePath);
				byte[] buffer = new byte[inStream.available()];
				int bufferCounter = 0;
				while ( (byteread = inStream.read()) != -1) {
					buffer[bufferCounter] = (byte) byteread;
					bufferCounter++;
				}
				inStream.close();
				return buffer;
			}
			else {
				Node.Utils.LoggingUtils.Log("Utility>>>readFile>>>No file found.", Level.DEBUG,
						Phrase.AdministrationLoggerName);
				return null;
			}
		}
		catch (Exception e) {
			Node.Utils.LoggingUtils.Log("Utility>>>>readFile>>>Read file error." + e.toString(), Level.DEBUG,
					Phrase.AdministrationLoggerName);
			return null;
		}

	}

	/**
	 * newFolder
	 * @param folderPath
	 * @return 
	 */
	public static void newFolder(String folderPath) {
		try {
			String filePath = folderPath;
			filePath = filePath.toString();
			java.io.File myFilePath = new java.io.File(filePath);
			if (!myFilePath.exists()) {
				myFilePath.mkdir();
			}
		}
		catch (Exception e) {
			Node.Utils.LoggingUtils.Log("Utility>>>newFolder>>>Fail to create folder: " + folderPath, Level.DEBUG, Phrase.AdministrationLoggerName);
		}
	}

	//------------------------------------------------------------------
	/**
	 * Delete file
	 * @param filePathAndName String etc: c:/fqf.txt
	 * @param fileContent String
	 * @return boolean
	 */
	//------------------------------------------------------------------
	public static void delFile(String filePathAndName) {
		try {
			String filePath = filePathAndName;
			File myDelFile = new File(filePath);
			if (myDelFile.exists()) {
				myDelFile.delete();
			}
		}
		catch (Exception e) {
			Node.Utils.LoggingUtils.Log("Utility>>>>delFile>>>Delete file error." + e.toString(), Level.DEBUG,
					Phrase.AdministrationLoggerName);
		}

	}

	/**
	 * writeFile
	 * @param filePathAndName
	 * @param is
	 * @return 
	 */
	public static void writeFile(String filePathAndName, InputStream is) throws Exception {
		int bytesum = 0;
		int byteread = 0;
		InputStream inStream = is;
		FileOutputStream fs = new FileOutputStream(filePathAndName);
		byte[] buffer = new byte[1024];
		try {
			while ( (byteread = inStream.read(buffer)) != -1) {
				//bytesum += byteread; //byte number
				//System.out.println(bytesum);
				fs.write(buffer, 0, byteread);
			}
		}
		catch (Exception ex) {
			throw ex;
		}finally{
			inStream.close();
			fs.close();
		}
	}

	/**
	 * AppendFile
	 * @param filePathAndName
	 * @param is
	 * @return 
	 */
	public static void AppendFile(String filePathAndName, InputStream is) throws Exception {
		int bytesum = 0;
		int byteread = 0;
		InputStream inStream = is;
		FileOutputStream fs = new FileOutputStream(filePathAndName, true);
		byte[] buffer = new byte[1024];
		try {
			while ( (byteread = inStream.read(buffer)) != -1) {
				//bytesum += byteread; //byte number
				//System.out.println(bytesum);
				fs.write(buffer, 0, byteread);
			}
		}
		catch (Exception ex) {
			throw ex;
		}finally{
			inStream.close();
			fs.close();
		}
	}

	/**
	 * delFolder
	 * @param folderPath
	 * @return 
	 */
	public static void  delFolder(String  folderPath)  {  
		try  {  
			delAllFile(folderPath);   
			String  filePath  =  folderPath;  
			filePath  =  filePath.toString();  
			java.io.File  myFilePath  =  new  java.io.File(filePath);  
			myFilePath.delete();   

		}  
		catch  (Exception  e)  {  
			e.printStackTrace();  

		}  

	}  

	/**
	 * delFodelAllFilelder
	 * @param path
	 * @return 
	 */
	public static void  delAllFile(String  path)  {  
		File  file  =  new  File(path);  
		if  (!file.exists())  {  
			return;  
		}  
		if  (!file.isDirectory())  {  
			return;  
		}  
		String[]  tempList  =  file.list();  
		File  temp  =  null;  
		for  (int  i  =  0;  i  <  tempList.length;  i++)  {  
			if  (path.endsWith(File.separator))  {  
				temp  =  new  File(path  +  tempList[i]);  
			}  
			else  {  
				temp  =  new  File(path  +  File.separator  +  tempList[i]);  
			}  
			if  (temp.isFile())  {  
				temp.delete();  
			}  
			if  (temp.isDirectory())  {  
				delAllFile(path+"/"+  tempList[i]); 
			}  
		}  
	}  

	/**
	 * ExtensionFilter
	 * @param 
	 * @return 
	 */
	public static class ExtensionFilter implements FilenameFilter {
		private String extension;
		public ExtensionFilter( String extension ) {
			this.extension = extension;             
		}

		public boolean accept(File dir, String name) {
			return (name.endsWith(extension));
		}
	}

	/**
	 * PrefixFilter
	 * @param 
	 * @return 
	 */
	public static class PrefixFilter implements FilenameFilter {
		private String prefix;
		public PrefixFilter( String prefix ) {
			this.prefix = prefix;             
		}

		public boolean accept(File dir, String name) {
			return (name.startsWith(prefix));
		}
	}

	/**
	 * deleteFilesByExt
	 * @param directory
	 * @param extension
	 * @return 
	 */
	public static void deleteFilesByExt( String directory, String extension ) {
		ExtensionFilter filter = null;
		if(extension!=null) filter = new ExtensionFilter(extension);
		File dir = new File(directory);

		String[] list = dir.list(filter);
		File file;
		if (list.length == 0) return;

		for (int i = 0; i < list.length; i++) {
			file = new File(directory, list[i]);
			//System.out.println(file + "  deleted : " + file.delete());
			file.delete();
		}
	}

	/**
	 * deleteFilesByPrefix
	 * @param directory
	 * @param prefix
	 * @return 
	 */
	public static void deleteFilesByPrefix( String directory, String prefix ) {
		PrefixFilter filter = null;
		if(prefix!=null) filter = new PrefixFilter(prefix);
		File dir = new File(directory);

		String[] list = dir.list(filter);
		File file;
		if (list.length == 0) return;

		for (int i = 0; i < list.length; i++) {
			file = new File(directory, list[i]);
			//System.out.println(file + "  deleted : " + file.delete());
			file.delete();
		}
	}

	/**
	 * copyFile
	 * @param oldPath
	 * @param newPath
	 * @return 
	 */
	public static void copyFile(String  oldPath,  String  newPath)  { 
		InputStream  inStream = null;
		FileOutputStream  fs  = null;
		try  {  
			int  bytesum  =  0;  
			int  byteread  =  0;  
			File  oldfile  =  new  File(oldPath);  
			if  (oldfile.exists())  {  //file exist 
				inStream  =  new  FileInputStream(oldPath);  //read source file  
				fs = new  FileOutputStream(newPath);  
				byte[]  buffer  =  new  byte[1444];  
				while  (  (byteread  =  inStream.read(buffer))  !=  -1)  {  
					//bytesum  +=  byteread;   
					//System.out.println(bytesum);  
					fs.write(buffer,  0,  byteread);  
				}  
			}  
		}  
		catch  (Exception  e)  {  
			e.printStackTrace();  
		}finally{
			try {
				inStream.close();
				fs.close();
			} catch (IOException e) {
				e.printStackTrace();
			} 
		}  
	}  

	// ------------------------------------------------------------------
	/**
	 * Zip file
	 *
	 * @param sourceFile
	 *            String sourc file etc fqf.txt
	 * @param zipFile
	 *            String target file etc fqf.zip
	 * @param path
	 *            String target file path etc c:\\
	 * @return boolean
	 */
	// ------------------------------------------------------------------


	public static boolean zipFile(String sourceFile, String zipFile, String path) {
		boolean ret = false;
		Long[] params = new Long[3];
		if(sourceFile!=null){
			params = ZipSingleFile(sourceFile, zipFile, path,0,0,0);
			if(params[0]!=null){
				Utility.delFile(path + zipFile);
				params = ZipSingleFile(sourceFile, zipFile, path, params[0].longValue(),params[1].longValue(),params[2].longValue());
				ret = true;
			}else Node.Utils.LoggingUtils.Log("Utility>>>zipFile>>>Fail to create zip file.", Level.ERROR, Phrase.AdministrationLoggerName);
		}		
		return ret;
	}
	// ------------------------------------------------------------------
	/**
	 * Zip Multiple files
	 *
	 * @param sourceFileList
	 *            String[] sourc file list etc fqf.txt,fff.jpg
	 * @param zipFile
	 *            String target file etc fqf.zip
	 * @param path
	 *            String target file path etc c:\\
	 * @return boolean
	 */
	// ------------------------------------------------------------------


	public static boolean zipFiles(String[] sourceFileList, String zipFile, String path) {
		boolean ret = false;
		Long[][] params = null;
		if(sourceFileList!=null && sourceFileList.length > 0){
			params = ZipMultipleFiles(sourceFileList, zipFile, path, null);
			if(params != null && params[0][0].longValue() > 0){
				Utility.delFile(path + zipFile);
				params = ZipMultipleFiles(sourceFileList, zipFile, path, params);
				ret = true;
			}else Node.Utils.LoggingUtils.Log("Utility>>>zipFiles>>>Fail to create zip file.", Level.ERROR, Phrase.AdministrationLoggerName);			
		}

		return ret;
	}
	// ------------------------------------------------------------------
	/**
	 * Zip single file
	 *
	 * @param sourceFile
	 *            String sourc file etc fqf.txt
	 * @param zipFile
	 *            String target file etc fqf.zip
	 * @param path
	 *            String target file path etc c:\\
	 * @param crc
	 * @param csize
	 * @param size
	 * @return boolean
	 */
	// ------------------------------------------------------------------
	public static Long[] ZipSingleFile(String sourceFile, String zipFile, String path, long crc,long csize,long size) {
		Long[] params = new Long[3];
		ZipOutputStream zipos = null;
		DataOutputStream os = null;
		try {
			//long bytesum  =  0;  
			int byteread = 0;

			File sourcefile = new File(path + sourceFile);
			if (sourcefile.exists()) {
				InputStream ins = new FileInputStream(path + sourceFile);
				File f = new File(path + zipFile);
				if (!f.exists()) {
					Node.Utils.LoggingUtils.Log("Utility>>>ZipSigleFile>>>Create zip output file.", Level.DEBUG,
							Phrase.AdministrationLoggerName);
				}
				FileOutputStream dest = new FileOutputStream(f);
				zipos = new ZipOutputStream(dest);

				ZipEntry newZipEntry = new ZipEntry(sourceFile);

				if(crc!=0 && csize!=0 && size !=0){
					newZipEntry.setCrc(crc);
					newZipEntry.setCompressedSize(csize);
					newZipEntry.setSize(size);					
				}
				zipos.putNextEntry(newZipEntry);
				os = new DataOutputStream(zipos);

				byte[] buffer = new byte[1024];
				while ( (byteread = ins.read(buffer)) != -1) {
					//bytesum  +=  byteread;   
					os.write(buffer, 0, byteread);
				}

				zipos.closeEntry();
				zipos.close();
				os.close();

				params[0] = new Long(newZipEntry.getCrc());
				params[1] = new Long(newZipEntry.getCompressedSize());
				params[2] = new Long(sourcefile.length());

			}
			else {
				Node.Utils.LoggingUtils.Log("Utility>>>ZipSigleFile>>>No input file.", Level.DEBUG, Phrase.AdministrationLoggerName);
				return params;
			}
			Node.Utils.LoggingUtils.Log("Utility>>>ZipSigleFile>>>Create output file successful.", Level.DEBUG,
					Phrase.AdministrationLoggerName);
		}
		catch (Exception ioe) {
			ioe.printStackTrace();
		}finally{
			if(zipos!=null){
				try {
					zipos.close();
				} catch (IOException e) {
					e.printStackTrace();
				}							
			}
			if(os!=null){
				try {
					os.close();
				} catch (IOException e) {
					e.printStackTrace();
				}							
			}
		}
		return params;
	}

	// ------------------------------------------------------------------
	/**
	 * Zip Multiple files
	 *
	 * @param sourceFileList
	 *            String[] sourc files etc fqf.txt,fff.jpg
	 * @param zipFile
	 *            String target file etc fqf.zip
	 * @param path
	 *            String target file path etc c:\\
	 * @param zipEntryParams zip Entry parameters
	 * @return boolean
	 */
	// ------------------------------------------------------------------
	public static Long[][] ZipMultipleFiles(String[] sourceFileList, String zipFile, String path, Long[][] zipEntryParams) {
		Long[][] params = null;
		long crc = 0;
		long csize = 0;
		long size = 0;
		ZipOutputStream zipos = null;
		DataOutputStream os = null;
		InputStream ins = null;


		if(zipEntryParams!=null && zipEntryParams.length>0){
			params = zipEntryParams;
		}else if(sourceFileList!=null && sourceFileList.length>0){
			params = new Long[sourceFileList.length][3];
		}
		try {
			//long bytesum  =  0;  
			int byteread = 0;

			File f = new File(path + zipFile);
			if (!f.exists()) {
				Node.Utils.LoggingUtils.Log("Utility>>>ZipMultipleFiles>>>Create zip output file.", Level.DEBUG,
						Phrase.AdministrationLoggerName);
			}
			FileOutputStream dest = new FileOutputStream(f);
			zipos = new ZipOutputStream(dest);
			for(int i=0;i<params.length;i++){
				File sourcefile = new File(path + sourceFileList[i]);
				if (sourcefile.exists()) {
					ins = new FileInputStream(sourcefile);

					ZipEntry newZipEntry = new ZipEntry(sourceFileList[i]);

					crc = params[i][0]==null?0:params[i][0].longValue();
					csize = params[i][1]==null?0:params[i][1].longValue();
					size = params[i][2]==null?0:params[i][2].longValue();
					if(crc!=0 && csize!=0 && size !=0){
						newZipEntry.setCrc(crc);
						newZipEntry.setCompressedSize(csize);
						newZipEntry.setSize(size);					
					}
					zipos.putNextEntry(newZipEntry);
					os = new DataOutputStream(zipos);

					byte[] buffer = new byte[1024];
					while ( (byteread = ins.read(buffer)) != -1) {
						//bytesum  +=  byteread;   
						os.write(buffer, 0, byteread);
					}

					zipos.closeEntry();

					params[i][0] = new Long(newZipEntry.getCrc());
					params[i][1] = new Long(newZipEntry.getCompressedSize());
					params[i][2] = new Long(sourcefile.length());
					ins.close();

				}
				else {
					Node.Utils.LoggingUtils.Log("Utility>>>ZipMultipleFiles>>>No input file for " + sourceFileList[i], Level.DEBUG, Phrase.AdministrationLoggerName);
//					return params;
				}
				Node.Utils.LoggingUtils.Log("Utility>>>ZipMultipleFiles>>>Create output file successfully.", Level.DEBUG,
						Phrase.AdministrationLoggerName);									
			}
		}
		catch (Exception ioe) {
			ioe.printStackTrace();
		}finally{
			if(zipos!=null){
				try {
					zipos.close();
				} catch (IOException e) {
					e.printStackTrace();
				}							
			}
			if(ins!=null){
				try {
					ins.close();
				} catch (IOException e) {
					e.printStackTrace();
				}							
			}
			if(os!=null){
				try {
					os.close();
				} catch (IOException e) {
					e.printStackTrace();
				}							
			}
		}
		return params;
	}

	/*	public static boolean zipFile(String sourceFile, String zipFile, String path) {
		try {
			long bytesum  =  0;  
			int byteread = 0;
			File sourcefile = new File(path + sourceFile);
			if (sourcefile.exists()) {
				InputStream ins = new FileInputStream(path + sourceFile);
				File f = new File(path + zipFile);
				if (!f.exists()) {
					Node.Utils.LoggingUtils.Log("Utility>>>zipFile>>>Create output file.", Level.DEBUG,
							Phrase.AdministrationLoggerName);
				}
				FileOutputStream dest = new FileOutputStream(f);
				CheckedOutputStream checksum = new CheckedOutputStream(dest, new CRC32());
				ZipOutputStream zipos = new ZipOutputStream(new BufferedOutputStream(checksum));

				ZipEntry newZipEntry = new ZipEntry(sourceFile);
				newZipEntry.setSize(sourcefile.length());
				newZipEntry.setMethod(ZipEntry.DEFLATED);
				zipos.putNextEntry(newZipEntry);
				DataOutputStream os = new DataOutputStream(zipos);

				byte[] buffer = new byte[1024];
				while ( (byteread = ins.read(buffer)) != -1) {
					bytesum  +=  byteread;   
					os.write(buffer, 0, byteread);
				}
				zipos.closeEntry();
				zipos.close();
				ins.close();
				os.close();
//				System.out.println(newZipEntry.getSize());
//				System.out.println(newZipEntry.getCompressedSize());
//				System.out.println(newZipEntry.getCrc());
//				System.out.println(newZipEntry.getMethod());
//		        System.out.println("checksum: "+checksum.getChecksum().getValue());
			}
			else {
				Node.Utils.LoggingUtils.Log("Utility>>>zipFile>>>No input file.", Level.DEBUG, Phrase.AdministrationLoggerName);
				return false;
			}
			Node.Utils.LoggingUtils.Log("Utility>>>zipFile>>>Create output file successful.", Level.DEBUG,
					Phrase.AdministrationLoggerName);
			return true;
		}
		catch (IOException ioe) {
			ioe.printStackTrace();
		}
		return false;
	}
	 */
	// ------------------------------------------------------------------
	/**
	 * Zip file
	 * 
	 * @param sourceFile
	 *            String sourc file etc fqf.txt
	 * @param zipFile
	 *            String target file etc fqf.zip
	 * @param path
	 *            String target file path etc c:\\
	 * @return boolean
	 */
	// ------------------------------------------------------------------
	public static boolean unZipFile(String sourceFile, String unzipFile,String path) {
		final int BUFFER = 2048;
		try {
			String fileName = sourceFile;
			String filePath = path;
			ZipFile zipFile = new ZipFile(path+fileName);
			Enumeration emu = zipFile.entries();
			int i = 0;
			while (emu.hasMoreElements()) {
				ZipEntry entry = (ZipEntry) emu.nextElement();
				// Create first directory
				if (entry.isDirectory()) {
					new File(filePath + entry.getName()).mkdirs();
					continue;
				}
				BufferedInputStream bis = new BufferedInputStream(zipFile
						.getInputStream(entry));
				File file = new File(filePath + entry.getName());
				// Since read files is random so create the folder first
				File parent = file.getParentFile();
				if (parent != null && (!parent.exists())) {
					parent.mkdirs();
				}
				FileOutputStream fos = new FileOutputStream(file);
				BufferedOutputStream bos = new BufferedOutputStream(fos, BUFFER);

				int count=0;
				byte data[] = new byte[BUFFER];
				while ((count = bis.read(data, 0, BUFFER)) != -1) {
					bos.write(data, 0, count);
				}
				bos.flush();
				bos.close();
				bis.close();
			}
			zipFile.close();
			return true;
		} catch (Exception e) {
			e.printStackTrace();
			return false;
		}
	}


	// ------------------------------------------------------------------
	/**
	 * Zip file
	 * 
	 * @param sourceFile
	 *            String sourc file etc fqf.txt
	 * @param zipFile
	 *            String target file etc fqf.zip
	 * @param path
	 *            String target file path etc c:\\
	 * @return ArrayList
	 * 			  Array of unzipped file name
	 */
	// ------------------------------------------------------------------
	public static ArrayList unZipFile(String sourceFile,String path) {
		final int BUFFER = 2048;
		ArrayList fileList = new ArrayList();
		try {
			String fileName = sourceFile; 
			String filePath = path;
			ZipFile zipFile = new ZipFile(path+fileName);
			Enumeration emu = zipFile.entries();
			int i = 0;
			while (emu.hasMoreElements()) {
				ZipEntry entry = (ZipEntry) emu.nextElement();
				// Create first directory
				if (entry.isDirectory()) {
					new File(filePath + entry.getName()).mkdirs();
					continue;
				}
				BufferedInputStream bis = new BufferedInputStream(zipFile
						.getInputStream(entry));
				File file = new File(filePath + entry.getName());
				// Since read files is random so create the folder first
				File parent = file.getParentFile();
				if (parent != null && (!parent.exists())) {
					parent.mkdirs();
				}
				FileOutputStream fos = new FileOutputStream(file);
				BufferedOutputStream bos = new BufferedOutputStream(fos, BUFFER);

				int count=0;
				byte data[] = new byte[BUFFER];
				while ((count = bis.read(data, 0, BUFFER)) != -1) {
					bos.write(data, 0, count);
				}
				bos.flush();
				bos.close();
				bis.close();
				fileList.add(entry.getName());
			}
			zipFile.close();
			return fileList;
		} catch (Exception e) {
			e.printStackTrace();
			return null;
		}
	}

	/**
	 * sqlDateToString
	 * @param date
	 * @return String
	 */
	public static String sqlDateToString(Date date) throws Exception {
		Calendar cal = Calendar.getInstance();
		String ret = null;
		try {
			// java.sql.Date sqlDate = new java.sql.Date(date.getTime());
			// System.out.println(sqlDate.getTime());
			cal.setTime(date);
			ret = cal.get(Calendar.YEAR) + "-" + (cal.get(Calendar.MONTH) + 1) + "-" +
			cal.get(Calendar.DAY_OF_MONTH);
		}
		catch (Exception ex) {
			throw ex;
		}
		return ret;
	}

	/* get Phrase message based on Phrase status */
	/**
	 * getPhraseMsgByStatus
	 * @param phraseStatus
	 * @return String
	 */
	public static String getPhraseMsgByStatus(String phraseStatus){
		String ret = null;
		String msg = phraseStatus.substring(0,phraseStatus.indexOf("Status"));
		msg = msg+"Message";

		if(msg.equalsIgnoreCase("ReceivedMessage")){
			ret = Phrase.ReceivedMessage;
		}else if(msg.equalsIgnoreCase("ProcessingMessage")){
			ret = Phrase.ProcessingMessage;
		}else if(msg.equalsIgnoreCase("PendingMessage")){
			ret = Phrase.PendingMessage;
		}else if(msg.equalsIgnoreCase("ApprovedMessage")){
			ret = Phrase.ApprovedMessage;
		}else if(msg.equalsIgnoreCase("ProcessedMessage")){
			ret = Phrase.ProcessedMessage;
		}else if(msg.equalsIgnoreCase("CanceledMessage")){
			ret = Phrase.CanceledMessage;
		}else if(msg.equalsIgnoreCase("UnknownMessage")){
			ret = Phrase.UnknownMessage;
		}else if(msg.equalsIgnoreCase("CompleteMessage")){
			ret = Phrase.CompleteMessage;
		}else if(msg.equalsIgnoreCase("WarningMessage")){
			ret = Phrase.CompleteMessage;
		}else if(msg.equalsIgnoreCase("FailedMessage")){
			ret = Phrase.CompleteMessage;
		}
		return ret;
	}

	/* get Phrase message based on Phrase status */
	/**
	 * getPhraseMsgByContent
	 * @param phraseContent
	 * @return String
	 */
	public static String getPhraseMsgByContent(String phraseContent){
		String ret = null;

		if(phraseContent.equalsIgnoreCase("Received")){
			ret = Phrase.ReceivedMessage;
		}else if(phraseContent.equalsIgnoreCase("Processing")){
			ret = Phrase.ProcessingMessage;
		}else if(phraseContent.equalsIgnoreCase("Pending")){
			ret = Phrase.PendingMessage;
		}else if(phraseContent.equalsIgnoreCase("Approved")){
			ret = Phrase.ApprovedMessage;
		}else if(phraseContent.equalsIgnoreCase("Processed")){
			ret = Phrase.ProcessedMessage;
		}else if(phraseContent.equalsIgnoreCase("Canceled")){
			ret = Phrase.CanceledMessage;
		}else if(phraseContent.equalsIgnoreCase("Unknown")){
			ret = Phrase.UnknownMessage;
		}else if(phraseContent.equalsIgnoreCase("Completed")){
			ret = Phrase.CompleteMessage;
		}else if(phraseContent.equalsIgnoreCase("Warning")){
			ret = Phrase.WarningMessage;
		}else if(phraseContent.equalsIgnoreCase("Failed")){
			ret = Phrase.FailedMessage;
		}
		return ret;
	}

	/* get Phrase fault message based on Phrase code */
	/**
	 * getPhraseFaultMsgByCode
	 * @param phraseCode
	 * @return String
	 */
	public static String getPhraseFaultMsgByCode(String phraseCode){
		String ret = null;

		if(phraseCode.equalsIgnoreCase(Phrase.UnknownUser)){
			ret = Phrase.UnknownUserMSG;
		}else if(phraseCode.equalsIgnoreCase(Phrase.InvalidCredential)){
			ret = Phrase.InvalidCredentialMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.Query)){
			ret = Phrase.QueryMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.TransactionId)){
			ret = Phrase.TransactionIdMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.UnknownMethod)){
			ret = Phrase.UnknownMethodMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.ServiceUnavailable)){
			ret = Phrase.ServiceUnavailableMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.AccessDenied)){
			ret = Phrase.AccessDeniedMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.InvalidToken)){
			ret = Phrase.InvalidTokenMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.TokenExpired)){
			ret = Phrase.TokenExpiredMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.FileNotFound)){
			ret = Phrase.FileNotFoundMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.ValidationFailed)){
			ret = Phrase.ValidationFailedMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.ServerBusy)){
			ret = Phrase.ServerBusyMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.RowIdOutofRange)){
			ret = Phrase.RowIdOutofRangeMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.FeatureUnsupported)){
			ret = Phrase.FeatureUnsupportedMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.VersionMismatch)){
			ret = Phrase.VersionMismatchMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.InvalidFileName)){
			ret = Phrase.InvalidFileNameMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.InvalidFileType)){
			ret = Phrase.InvalidFileTypeMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.InvalidDataFlow)){
			ret = Phrase.InvalidDataFlowMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.InvalidParameter)){
			ret = Phrase.InvalidParameterMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.InternalError)){
			ret = Phrase.InternalErrorMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.InvalidSQL)){
			ret = Phrase.InvalidSQLMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.AuthMethod)){
			ret = Phrase.AuthMethodMSG;				  
		}else if(phraseCode.equalsIgnoreCase(Phrase.Unknown)){
			ret = Phrase.UnknownMSG;				  
		}
		return ret;
	}

	/* Base64 encode */
	/**
	 * encode
	 * @param fileName
	 * @return String
	 */
	public static String encode(String fileName) {
		String string = null;
		try {
			InputStream in = new FileInputStream(fileName);
			// in.available() return file byte length
			byte[] bytes = new byte[in.available()];
			// read file into array
			in.read(bytes);
			string = new BASE64Encoder().encode(bytes);
			in.close();
		} catch (FileNotFoundException fe) {
			fe.printStackTrace();
		} catch(IOException ioe) {
			ioe.printStackTrace();
		}
		return string;
	}

	/* Base64 decode */
	/**
	 * decode
	 * @param string
	 * @param fileName
	 * @return 
	 */
	public static void decode(String string, String fileName) {
		try {
			// decode transfer byte to file
			byte[] bytes = new BASE64Decoder().decodeBuffer(string);
			ByteArrayInputStream in = new ByteArrayInputStream(bytes);
			byte[] buffer = new byte[1024];
			FileOutputStream out = new FileOutputStream(fileName);
			int bytesum = 0;
			int byteread = 0;
			while ((byteread = in.read(buffer)) != -1) {
				bytesum += byteread;
				out.write(buffer, 0, byteread); 
			}
			out.close();
		} catch(IOException ioe) {
			ioe.printStackTrace();
		}
	}

	/* Change xml file to String */
	/**
	 * xmlToString
	 * @param xmlString
	 * @return String
	 */
	public static String xmlToString(String xmlString) throws Exception {
		String ret = xmlString;
		if(xmlString!=null && !xmlString.equals("")){
			XmlDocument doc = new XmlDocument();
			//InputStream is = new ByteArrayInputStream(xmlString.getBytes());
			doc.LoadXml(xmlString);
			XmlElement root = doc.DocumentElement();
			ret = root.GetInnerText();			
		}
		return ret;
	}

	/* Get domain Name by class name */
	/**
	 * GetDomainName
	 * @param className
	 * @param webMethod
	 * @return String
	 */
	public static String GetDomainName(String className,String webMethod){
		String ret = null;
		Utility util = new Utility();
		try {
			INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
			ITaskConfiguration taskDB = DBManager.GetTaskConfiguration(Phrase.WebServicesLoggerName);
			String[] opArr = null;
			if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SUBMIT)){
				opArr = opDB.GetSubmits();
			}else if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_QUERY)){
				opArr = opDB.GetQueries();
			}else if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT)){
				opArr = opDB.GetSolicits();
			}else if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)){
				Xtask[] xtask = taskDB.GetTasks();
				opArr = new String[xtask.length];
				for(int i=0;i<opArr.length;i++){
					opArr[i] = xtask[i].getName();
				}
			}
			if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)){
				for(int i=0;i<opArr.length;i++){
					Operation op = opDB.GetOperation(opArr[i], null);
					if(op!=null &&  op.GetClassName()!=null && op.GetClassName().equalsIgnoreCase(className)){
						ret = op.GetDomain();
					}
				}
			}else{
				for(int i=0;i<opArr.length;i++){
					Operation op = opDB.GetOperation(opArr[i], webMethod);
					if(op!=null &&  op.GetProcessClass()!=null && op.GetProcessClass().equalsIgnoreCase(className)){
						ret = op.GetDomain();
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return ret;
	}

	/* Get Operation by class name */
	/**
	 * Operation
	 * @param className
	 * @param webMethod
	 * @return Operation
	 */
	public static Operation GetOperation(String className,String webMethod){
		Operation ret = null;
		Utility util = new Utility();
		try {
			INodeOperation opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
			ITaskConfiguration taskDB = DBManager.GetTaskConfiguration(Phrase.WebServicesLoggerName);
			String[] opArr = null;
			if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SUBMIT)){
				opArr = opDB.GetSubmits();
			}else if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_QUERY)){
				opArr = opDB.GetQueries();
			}else if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.WEB_METHOD_SOLICIT)){
				opArr = opDB.GetSolicits();
			}else if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)){
				Xtask[] xtask = taskDB.GetTasks();
				opArr = new String[xtask.length];
				for(int i=0;i<opArr.length;i++){
					opArr[i] = xtask[i].getName();
				}
			}
			if(webMethod!=null && webMethod.equalsIgnoreCase(Phrase.SCHEDULED_TASK_OPERATION)){
				for(int i=0;i<opArr.length;i++){
					Operation op = opDB.GetOperation(opArr[i], null);
					if(op!=null &&  op.GetClassName()!=null && op.GetClassName().equalsIgnoreCase(className)){
						ret = op;
					}
				}
			}else{
				for(int i=0;i<opArr.length;i++){
					Operation op = opDB.GetOperation(opArr[i], webMethod);
					if(op!=null &&  op.GetProcessClass()!=null && op.GetProcessClass().equalsIgnoreCase(className)){
						ret = op;
					}
				}
			}
		} catch (Exception e) {
			e.printStackTrace();
		}
		return ret;
	}

	/**
	 * getPropertyValue
	 * @param propertyFile
	 * @param propertyName
	 * @return String
	 */
	public static String getPropertyValue(String propertyFile, String propertyName) {
		Properties props = new Properties();
		String ret = null;

		//try retrieve data from file
		try {

			props.load(new FileInputStream(propertyFile));

			ret = props.getProperty(propertyName);
		} catch(Exception ioe) {
			ioe.printStackTrace();
		}
		return ret;
	}

	/**
	 * @author Connect SFTP
	 */
	/**
	 * connect
	 * @param host
	 * @param port
	 * @param username
	 * @param password
	 * @return ChannelSftp
	 * @throws Exception 
	 */
	public static ChannelSftp connectSFTP(String host, int port, String username,
			String password) throws Exception {
		ChannelSftp sftp = null;
		try {
			JSch jsch = new JSch();
			jsch.getSession(username, host, port);
			Session sshSession = jsch.getSession(username, host, port);
			//System.out.println("Session created.");
			sshSession.setPassword(password);
			Properties sshConfig = new Properties();
			sshConfig.put("StrictHostKeyChecking", "no");
			sshSession.setConfig(sshConfig);
			sshSession.connect();
			//System.out.println("Session connected.");
			//System.out.println("Opening Channel.");
			Channel channel = sshSession.openChannel("sftp");
			channel.connect();
			sftp = (ChannelSftp) channel;
			//System.out.println("Connected to " + host + ".");
		} catch (Exception e) {
			throw e;
		}
		return sftp;
	}

	/**
	 * upload file
	 * 
	 * @param directory
	 * @param uploadFile
	 * @param sftp
	 * @return
	 * @throws Exception 
	 */
	public static void upload(String directory, String uploadFile, ChannelSftp sftp) throws Exception {
		try {
			if(sftp != null && uploadFile != null){
				sftp.cd(directory);
				File file = new File(uploadFile);
				sftp.put(new FileInputStream(file), file.getName());				
			}
		} catch (Exception e) {
			throw e;
		}
	}

	/**
	 * upload file
	 * 
	 * @param directory
	 * @param uploadFile
	 * @param sftp
	 * @return
	 * @throws Exception 
	 */
	public static void upload(String directory, byte[] content,String fileName, ChannelSftp sftp) throws Exception {
		try {
			if(sftp != null && content != null){
				sftp.cd(directory);
				sftp.put(new ByteArrayInputStream(content), fileName);				
			}
		} catch (Exception e) {
			throw e;
		}
	}

	/**
	 * download file
	 * 
	 * @param directory
	 * @param downloadFile
	 * @param saveFile
	 * @param sftp
	 * @return
	 * @throws Exception 
	 */
	public static void download(String directory, String downloadFile,
			String saveFile, ChannelSftp sftp) throws Exception {
		try {
			sftp.cd(directory);
			File file = new File(saveFile);
			sftp.get(downloadFile, new FileOutputStream(file));
		} catch (Exception e) {
			throw e;
		}
	}

	/**
	 * Delete File
	 * 
	 * @param directory
	 * @param deleteFile
	 * @param sftp
	 * @return
	 * @throws Exception 
	 */
	public static void delete(String directory, String deleteFile, ChannelSftp sftp) throws Exception {
		try {
			sftp.cd(directory);
			sftp.rm(deleteFile);
		} catch (Exception e) {
			throw e;
		}
	}

	/**
	 * List directory
	 * 
	 * @param directory
	 * @param sftp
	 * @return Vector
	 * @throws SftpException
	 */
	public static Vector listFiles(String directory, ChannelSftp sftp)
	throws SftpException {
		return sftp.ls(directory);
	}

	public static String backslashReplace(String myStr){
		final StringBuffer result = new StringBuffer();
		final StringCharacterIterator iterator = new StringCharacterIterator(myStr);
		char character =  iterator.current();
		while (character != CharacterIterator.DONE ){

			if (character == '\\') {
				result.append("/");
			}
			else {
				result.append(character);
			}


			character = iterator.next();
		}
		return result.toString();
	}

	/**
	 * Get temporary file path
	 * 
	 * @return String
	 */
	public static String GetTempFilePath(){
		String path = null;

		path = (String)JNDIAccess.GetJNDIValue(Phrase.JNDITempFilePathLocation, false);

		if(path==null || path.equalsIgnoreCase("")) 	path = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIAdministrationLogLocation, false);
		if(path==null || path.equalsIgnoreCase("")) 	path = (String)JNDIAccess.GetJNDIValue(Phrase.JNDITaskLogLocation, false);
		if(path==null || path.equalsIgnoreCase("")) 	path = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIWebServicesLogLocation, false);
		if(path==null || path.equalsIgnoreCase("")) 	path = (String)JNDIAccess.GetJNDIValue(Phrase.JNDIClientLogLocation, false);

		return path;
	}

	  /**
	   * Convert xml specific value.
	   * @param value The content of xml file.
	   * @throws 
	   * @return string of replaced content
	   */
	  public static String convertToXmlValue(String value)
	  {
	    //String conValue = null;
	    if(value == null) return value;
	    String[] specialValue = new String[]{"&", "<", ">", "\"", "'"};
	    String[] xmlValue = new String[]{"&amp;", "&lt;", "&gt;", "&quot;", "&apos;"};
	    for(int i=0; i<specialValue.length; i++)
	      value = value.replaceAll(specialValue[i], xmlValue[i]);

	    return value;
	  }

	/**
	 * change Special Character
	 * @param input
	 * 
	 * @return String
	 */

	public static String changeSpecialCharacter (String input)
	{
		if(input.matches("(?i).*&amp;.*") ){
			input = input.replaceAll("&amp;", "&");
		}
		if(input.matches("(?i).*&lt;.*")){
			input = input.replaceAll("&lt;", "<");
		}
		if(input.matches("(?i).*&gt;.*")){
			input = input.replaceAll("&gt;", ">");
		}
		if(input.matches("(?i).*&quot;.*")){
			input = input.replaceAll("&quot;", "\"");
		}
		if(input.matches("(?i).*&apos;.*")){
			input = input.replaceAll("&apos;", "\'");			
		}
		return input;		
	}

	/**
	 * change Special Character
	 * @param input
	 * 
	 * @return String
	 */

	public static String changeSpecialCharacterForPage (String input)
	{
		if(input.matches("(?i).*&amp;.*") ){
			input = input.replaceAll("&amp;", "&amp;amp;");
		}
		if(input.matches("(?i).*&lt;.*")){
			input = input.replaceAll("&lt;", "&amp;lt;");
		}
		if(input.matches("(?i).*&gt;.*")){
			input = input.replaceAll("&gt;", "&amp;gt;");
		}
		if(input.matches("(?i).*&quot;.*")){
			input = input.replaceAll("&quot;", "&amp;quot;");
		}
		if(input.matches("(?i).*&apos;.*")){
			input = input.replaceAll("&apos;", "&amp;apos;");			
		}
		return input;		
	}

	/**
	 * get build version
	 * @param buildDate
	 * 
	 * @return build version
	 */
	public static String getBuildVer(String buildDate){
		String buildVer = "";
		try
		{
			SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
			java.util.Date release = dateFormat.parse(buildDate);
			java.util.Date start = dateFormat.parse("2000-01-01 00:00");
			long diff = release.getTime() - start.getTime();
			buildVer = "." + diff / (1000 * 60 * 60 * 24);
		}
		catch (Exception ex)
		{
			ex.printStackTrace();
		}
		return buildVer;

	}

	/**
	 * Validate hex with regular expression
	 * @param hex hex for validation
	 * @return true valid hex, false invalid hex
	 */
	public static boolean IsValidateEmail(String hex){

		Pattern pattern = null;
		Matcher matcher = null;	 
		String EMAIL_PATTERN = "^[_A-Za-z0-9-]+(\\.[_A-Za-z0-9-]+)*@[A-Za-z0-9]+(\\.[A-Za-z0-9]+)*(\\.[A-Za-z]{2,})$";
		pattern = Pattern.compile(EMAIL_PATTERN);
		matcher = pattern.matcher(hex);
		return matcher.matches();
	}

	/**
	 * getVersion
	 * @return String
	 */
	public static String getVersion ()
	{
		String buildVer = "";
		String vers = (String)JNDIAccess.GetJNDIValue(Phrase.jndiNodeVersion, false);
		Properties myProp = PropertyFiles.loadProperties(AppUtils.WebServicesRoot + "WEB-INF/ApplicationResources.properties");

		String buildDate = myProp.getProperty(Phrase.buildDate);
		try
		{
			SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
			java.util.Date release = dateFormat.parse(buildDate);
			java.util.Date start = dateFormat.parse("2000-01-01 00:00");
			long diff = release.getTime() - start.getTime();
			buildVer = "." + diff / (1000 * 60 * 60 * 24);
		}
		catch (Exception ex)
		{
			ex.printStackTrace();
		}
		return "EN Node v"+ vers + buildVer;
	}

	/**
	 * get the config file name with version
	 * @param file name
	 * @return file name with version
	 */
	public static String getConfigFileNameWithVersion(String version, String fileName){
		String ret = null;
		if(version!=null && version.equalsIgnoreCase(Phrase.ver_1)){			  
			ret = fileName.substring(0,fileName.indexOf(".")) + "_v1" + fileName.substring(fileName.indexOf("."));
		}else if(version!=null && version.equalsIgnoreCase(Phrase.ver_2)){
			ret = fileName.substring(0,fileName.indexOf(".")) + "_v2" + fileName.substring(fileName.indexOf("."));			  
		}else{
			ret = fileName;			  				  
		}
		return ret;
	}

	// WI 21296
	/**
	 * generateGetServiceFile
	 * @param version
	 * @return String
	 * @throws Exception 
	 */
	public static String generateGetServiceFile(String version) throws Exception{
		String ret = null;
		int[] retIdArray = null;
		ArrayList retParams = null;
		ArrayList wsParams = null;

		INodeOperation opDB = DBManager.GetNodeOperation(Phrase.AdministrationLoggerName);
		retIdArray = opDB.GetAllQSEServicesID(version);
		NetworkNodesDocument networkNodesDocument = NetworkNodesDocument.Factory.newInstance();
		NetworkNodeListType networkNodeListType = NetworkNodeListType.Factory.newInstance();
		NetworkNodeType networkNodeType = networkNodeListType.addNewNetworkNodeDetails();
		ServiceDescriptionListType serviceDescriptionListType = ServiceDescriptionListType.Factory.newInstance();
		NodeMethodTypeCode nodeMethodTypeCode = NodeMethodTypeCode.Factory.newInstance();
		RequestParameterType[] requestParameterTypeList = null;
		LoggingUtils.Log("GetServicesAction>>> The retIdArray is:"+ (retIdArray==null?"":retIdArray[0]+""), Level.DEBUG, Phrase.AdministrationLoggerName);			        
		// WI 21296
		IGetServices getService = DBManager.getGetServices(Phrase.AdministrationLoggerName);
		byte[] fileByte = getService.getConfigFile(version,Phrase.REGISTRATION_FILE_NAME);
		if (fileByte != null){
			String fileStr = new String(fileByte);
			NetworkNodesDocument networkNodesDocumentGeneral = NetworkNodesDocument.Factory.parse(fileStr);

			// Set General Data
			networkNodeType.setNodeIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeIdentifier().trim());
			networkNodeType.setNodeName(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeName().trim());
			networkNodeType.setNodeAddress(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeAddress().trim());
			networkNodeType.setOrganizationIdentifier(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getOrganizationIdentifier().trim());
			networkNodeType.setNodeContact(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeContact().trim());
			net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum enumNodeVersionID = net.exchangenetwork.schema.ends.x2.NodeVersionCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeVersionIdentifier().toString().trim());
			networkNodeType.setNodeVersionIdentifier(enumNodeVersionID);
			net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum enumNodeDeploymentType = net.exchangenetwork.schema.ends.x2.NodeStageCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeDeploymentTypeCode().toString().trim());
			networkNodeType.setNodeDeploymentTypeCode(enumNodeDeploymentType);
			net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum enumNodeStatus = net.exchangenetwork.schema.ends.x2.NodeStatusCode.Enum.forString(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodeStatus().toString().trim());
			networkNodeType.setNodeStatus(enumNodeStatus);
			NodeBoundingBoxType nodeBoundingBoxType = NodeBoundingBoxType.Factory.newInstance();
			nodeBoundingBoxType.setBoundingCoordinateNorth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateNorth().toString().trim()));
			nodeBoundingBoxType.setBoundingCoordinateSouth(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateSouth().toString().trim()));
			nodeBoundingBoxType.setBoundingCoordinateEast(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateEast().toString().trim()));
			nodeBoundingBoxType.setBoundingCoordinateWest(new BigDecimal(networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getBoundingBoxDetails().getBoundingCoordinateWest().toString().trim()));
			networkNodeType.setBoundingBoxDetails(nodeBoundingBoxType);

			// Set Node Property
			ObjectPropertyType[] objectPropertyTypeList = networkNodesDocumentGeneral.getNetworkNodes().getNetworkNodeDetailsArray(0).getNodePropertyArray();
			if(objectPropertyTypeList!=null && objectPropertyTypeList.length>0){
				networkNodeType.setNodePropertyArray(objectPropertyTypeList);
			}
		};

		if(retIdArray!=null){						
			for(int i=0;i<retIdArray.length;i++){
				LoggingUtils.Log("GetServicesAction>>> The retIdArray[i] is:"+retIdArray[i], Level.DEBUG, Phrase.AdministrationLoggerName);
				Service service = serviceDescriptionListType.addNewService();
				Operation op = opDB.GetOperation(retIdArray[i]);
				String opName = op.GetOpName();
				LoggingUtils.Log("GetServicesAction>>> The opName is:"+opName, Level.DEBUG, Phrase.AdministrationLoggerName);
				String wsName = op.GetWebService();
				retParams = op.getWebServiceParameters();
				requestParameterTypeList = null;
				if(retParams!=null){
					requestParameterTypeList = new RequestParameterType[retParams.size()];
					for(int j=0;j<retParams.size();j++){
						//retParamH.put(opName, retParams[j]);
						requestParameterTypeList[j] = RequestParameterType.Factory.newInstance();
						wsParams = (ArrayList)retParams.get(j);
						// WI 23224
						if(wsParams != null && !((String)wsParams.get(1)).equalsIgnoreCase("output:")){
							requestParameterTypeList[j].setParameterName((String)wsParams.get(1));										
							requestParameterTypeList[j].setParameterType((String)wsParams.get(2));										
							requestParameterTypeList[j].setParameterTypeDescriptor((String)wsParams.get(3));
							requestParameterTypeList[j].setParameterSortIndex(BigInteger.valueOf(j+1));
							if((String)wsParams.get(4) != null && !((String)wsParams.get(4)).equals("")){
								requestParameterTypeList[j].setParameterOccurrenceNumber((String)wsParams.get(4));
							}
							if((String)wsParams.get(5) != null && !((String)wsParams.get(5)).equals("")){
								EncodingType.Enum encodingTypeEnum = EncodingType.Enum.forString((String)wsParams.get(5));
								requestParameterTypeList[j].setParameterEncoding(encodingTypeEnum);
							}
							if((String)wsParams.get(6) != null && !((String)wsParams.get(6)).equals("")){
								requestParameterTypeList[j].setParameterRequiredIndicator((new Boolean((String)wsParams.get(6))).booleanValue());
							}
							LoggingUtils.Log("GetServicesAction>>> The retParams name is:"+requestParameterTypeList[j].getParameterName(), Level.DEBUG, Phrase.AdministrationLoggerName);											
						}									
					}
				}
				if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.QUERY.toString().toUpperCase())){
					service.setMethodName(nodeMethodTypeCode.QUERY);									
				}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.SOLICIT.toString().toUpperCase())){
					service.setMethodName(nodeMethodTypeCode.SOLICIT);																		
				}else if(wsName!=null && wsName.equalsIgnoreCase(nodeMethodTypeCode.EXECUTE.toString().toUpperCase())){
					service.setMethodName(nodeMethodTypeCode.EXECUTE);																		
				}
				service.setDataflow(op.GetDomain());
				service.setServiceIdentifier(opName);
				service.setServiceDescription(op.GetDescription()==null?"":op.GetDescription());
				service.setParameterArray(requestParameterTypeList);
				LoggingUtils.Log("GetServicesAction>>> The opName is:"+opName, Level.DEBUG, Phrase.AdministrationLoggerName);
			}
			networkNodeType.setNodeServiceList(serviceDescriptionListType);
		}
		// create output xml
		networkNodesDocument.setNetworkNodes(networkNodeListType);
		ret = networkNodesDocument.toString();											

		return ret;
	}

	// WI 22065
	/**
	 * Sort Operation
	 * @param Operation array
	 * @return Sorted Operation array
	 */
	public static List sortOperationNode(List opNodeList){
		List ret = null;
		String[] opNameArr = null;

		if(opNodeList != null && opNodeList.size()>0){
			opNameArr = new String[opNodeList.size()];
			ret = new ArrayList();
			for(int i=0;i<opNameArr.length;i++){
				opNameArr[i] = Utility.getAttributeValue((org.dom4j.Node) opNodeList.get(i),"@name");
			}
			Arrays.sort(opNameArr, String.CASE_INSENSITIVE_ORDER);

			for(int i=0;i<opNameArr.length;i++){
				for(int j=0;j<opNodeList.size();j++){
					if(Utility.getAttributeValue((org.dom4j.Node) opNodeList.get(j),"@name").equalsIgnoreCase(opNameArr[i])){
						ret.add(opNodeList.get(j));							
					}
				}
			}
		}
		return ret;
	}

	// WI 22065
	/**
	 * Sort Operation
	 * @param Operation array
	 * @return Sorted Operation array
	 */
	public static Operation[] sortOperation(Operation[] opArr){
		Operation[] ret = null;
		String[] opNameArr = null;

		if(opArr != null && opArr.length>0){
			opNameArr = new String[opArr.length];
			ret = new Operation[opArr.length];
			for(int i=0;i<opNameArr.length;i++){
				opNameArr[i] = opArr[i].GetOpName();
			}
			Arrays.sort(opNameArr, String.CASE_INSENSITIVE_ORDER);

			for(int i=0;i<opNameArr.length;i++){
				for(int j=0;j<opArr.length;j++){
					if(opArr[j].GetOpName().equalsIgnoreCase(opNameArr[i])){
						ret[i] = opArr[j];							
					}
				}
			}
		}
		return ret;
	}

	// WI 22065
	/**
	 * getElementValue
	 * @param node
	 * @param xPath
	 * @return String
	 */
	public static String getElementValue(org.dom4j.Node node, String xPath) {
		String value = "";

		if (node == null || xPath == null) {
			return value;
		}

		org.dom4j.Node attNode = node.selectSingleNode(xPath);
		if (attNode != null) {
			value = attNode.getText();			
		}

		return value;
	}

	// WI 22065
	/**
	 * getAttributeValue
	 * @param node
	 * @param xPath
	 * @return String
	 */
	public static String getAttributeValue(org.dom4j.Node node, String xPath) {
		String value = "";

		if (node == null || xPath == null) {
			return value;
		}

		org.dom4j.Node attNode = node.selectSingleNode(xPath);
		if (attNode != null) {
			Attribute att = (Attribute) attNode;
			value = att.getValue().trim();
		}

		return value;
	}

	// WI 23197

	public static final String _255 = "(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
	public static final Pattern pattern = Pattern.compile("^(?:" + _255
			+ "\\.){3}" + _255 + "$");

	public static String longToIpV4(long longIp) {
		int octet3 = (int) ((longIp >> 24) % 256);
		int octet2 = (int) ((longIp >> 16) % 256);
		int octet1 = (int) ((longIp >> 8) % 256);
		int octet0 = (int) ((longIp) % 256);
		return octet3 + "." + octet2 + "." + octet1 + "." + octet0;
	}

	public static long ipV4ToLong(String ip) {
		String[] octets = ip.split("\\.");
		return (Long.parseLong(octets[0]) << 24)
		+ (Integer.parseInt(octets[1]) << 16)
		+ (Integer.parseInt(octets[2]) << 8)
		+ Integer.parseInt(octets[3]);
	}

	public static boolean isIPv4Private(String ip) {
		long longIp = ipV4ToLong(ip);
		return (longIp >= ipV4ToLong("10.0.0.0") && longIp <= ipV4ToLong("10.255.255.255"))
		|| (longIp >= ipV4ToLong("172.16.0.0") && longIp <= ipV4ToLong("172.31.255.255"))
		|| longIp >= ipV4ToLong("192.168.0.0") && longIp <= ipV4ToLong("192.168.255.255");
	}

	public static boolean isIPv4Valid(String ip) {
		return pattern.matcher(ip).matches();
	}

	public static String getIpFromRequest(HttpServletRequest request) {
		String ip = request.getHeader("X-Forwarded-For");
		boolean found = false;
		LoggingUtils.Log("Utility>>>getIpFromRequest>>>The X-Forwarded-For IP List is: " + ip, Level.DEBUG, Phrase.AdministrationLoggerName);
		if(ip == null || ip.equalsIgnoreCase("")){
			ip = request.getHeader("X-FORWARDED-FOR");
			LoggingUtils.Log("Utility>>>getIpFromRequest>>>The X-FORWARDED-FOR IP List is: " + ip, Level.DEBUG, Phrase.AdministrationLoggerName);
		}
		if(ip == null || ip.equalsIgnoreCase("")){
			ip = request.getHeader("x-forwarded-for");
			LoggingUtils.Log("Utility>>>getIpFromRequest>>>The x-forwarded-for IP List is: " + ip, Level.DEBUG, Phrase.AdministrationLoggerName);
		}
		if (ip != null && !ip.equalsIgnoreCase("")) {
			String[] ipList = ip.split(",");
			for (int i=0;i<ipList.length;i++) {
				ip = ipList[i].trim();
				if (isIPv4Valid(ip) && !isIPv4Private(ip)) {
					found = true;
					break;
				}
			}
		}
		if (!found) {
			ip = request.getRemoteAddr();
		}
		return ip;
	}		

	// WI 29446
	/**
	 * sendFTPfile
	 * @param ftpIPName
	 * @param remoteFileDirctory
	 * @param remoteFileName
	 * @param localFileDirctory
	 * @param localFileName
	 * @param isSFTP
	 * @param isDebug
	 * @return String
	 */
	public static boolean sendFTPfile(String ftpIPName,String remoteFileDirctory,String remoteFileName,
			String localFileDirctory, String localFileName, boolean isSFTP, boolean isDebug) throws Exception {
		byte[] fileByte = null;
		boolean ret = false;
		String host = ftpIPName;
		int port = 0;  
		String userName = null;    
		String passWord = null;  
		try {			
			if(isDebug){
    			host = ftpIPName;
    			if(isSFTP){
    				port = 22; 
    			}else{
    				port = 21; 
    			}
    			// Test windows user
    			userName = "test";    
    			passWord = "test";  			    				
			}else{
			    IConfigurationMgr configMgr = DBManager.GetConfigurationMgr(Phrase.AdministrationLoggerName);
			    fileByte = configMgr.GetConfig(Phrase.SYSTEM_FILE_NAME, Phrase.XML_TYPE).getBytes();
				if (fileByte != null){
				    SAXReader reader = new SAXReader();
				    org.dom4j.Document doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    if (doc4jVR != null) {
				    	//Element rootElm = doc4jVR.getRootElement();
				    	//List ftpList = rootElm.elements("FTPSettings");
				    	List ftpList = doc4jVR.selectNodes(".//Configuration/FTPSettings");
				    	for(Iterator i = ftpList.iterator(); i.hasNext(); ){
				    		org.dom4j.Node ftp = (org.dom4j.Node) i.next();
				    		if(Utility.getAttributeValue(ftp, "@host").equalsIgnoreCase(ftpIPName)){
				    			host = ftpIPName;
				    			port = Integer.parseInt(Utility.getAttributeValue(ftp, "@"+Phrase.FTP_PORT)); 
				    			userName = Utility.getElementValue(ftp, ".//Credentials/" + Phrase.FTP_USER);
				    			passWord = Utility.getElementValue(ftp, ".//Credentials/" + Phrase.FTP_PASSWORD);
				    			break;
				    		}
				    	}
				    }
					
				}		
			}
			if(host != null && port != 0 && userName != null && passWord != null){
				if(isSFTP){
	    	  		ChannelSftp sftp = Utility.connectSFTP(host, port, userName, passWord);
	    	  		Utility.upload(remoteFileDirctory, localFileDirctory+localFileName, sftp);			    				
				}else{
					FTPClient ftpClient = null;
					InputStream input = null;
					try {				
						ftpClient = new FTPClient();
						if(isDebug){
							ftpClient.addProtocolCommandListener(new PrintCommandListener(new PrintWriter(System.out)));
						}
					    int reply;
					    ftpClient.connect(host);
					    reply = ftpClient.getReplyCode();
				        if (!FTPReply.isPositiveCompletion(reply)) {
				        	ftpClient.disconnect();
				            throw new Exception("Exception in connecting to FTP Server.");
				        }
				        				        
				        if(ftpClient.login(userName, passWord)){
					        ftpClient.setFileType(FTP.BINARY_FILE_TYPE);
					        ftpClient.enterLocalPassiveMode();
					        input = new FileInputStream(new File(localFileDirctory+localFileName));
					        ftpClient.storeFile(remoteFileDirctory+remoteFileName, input);				        	
				        }else{
				        	throw new Exception("Can't log in FTP Server with given user name and password.");
				        }
					}catch(Exception ex){
						throw ex;
					}finally{
						if (input != null)
							input.close();
						if (ftpClient != null && ftpClient.isConnected()){
			            	ftpClient.logout();
			            	ftpClient.disconnect();
						}
					}			    				
				}
				ret = true;
			}
		}catch(Exception ex){
			throw ex;
		}finally{
		}
		return ret;
	}
	
	
	// WI 29446
	/**
	 * sendFTPfile
	 * @param ftpIPName
	 * @param remoteFileDirctory
	 * @param remoteFileName
	 * @param localFileContent
	 * @param isSFTP
	 * @param isDebug
	 * @return String
	 */
	public static boolean sendFTPfile(String ftpIPName,String remoteFileDirctory,String remoteFileName,
			byte[] localFileContent, boolean isSFTP, boolean isDebug) throws Exception {
		byte[] fileByte = null;
		boolean ret = false;
		String host = ftpIPName;
		int port = 0;  
		String userName = null;    
		String passWord = null;
		try {			
			if(isDebug){
    			host = ftpIPName;
    			if(isSFTP){
    				port = 22; 
    			}else{
    				port = 21; 
    			}
    			// Test windows user
    			userName = "test";    
    			passWord = "test";  			    				
			}else{
			    IConfigurationMgr configMgr = DBManager.GetConfigurationMgr(Phrase.AdministrationLoggerName);
			    fileByte = configMgr.GetConfig(Phrase.SYSTEM_FILE_NAME, Phrase.XML_TYPE).getBytes();
				if (fileByte != null){
				    SAXReader reader = new SAXReader();
				    org.dom4j.Document doc4jVR = reader.read(new ByteArrayInputStream(fileByte));
				    if (doc4jVR != null) {
				    	//Element rootElm = doc4jVR.getRootElement();
				    	//List ftpList = rootElm.elements("FTPSettings");
				    	List ftpList = doc4jVR.selectNodes(".//Configuration/FTPSettings");
				    	for(Iterator i = ftpList.iterator(); i.hasNext(); ){
				    		org.dom4j.Node ftp = (org.dom4j.Node) i.next();
				    		if(Utility.getAttributeValue(ftp, "@host").equalsIgnoreCase(ftpIPName)){
				    			host = ftpIPName;
				    			port = Integer.parseInt(Utility.getAttributeValue(ftp, "@"+Phrase.FTP_PORT)); 
				    			userName = Utility.getElementValue(ftp, ".//Credentials/" + Phrase.FTP_USER);
				    			passWord = Utility.getElementValue(ftp, ".//Credentials/" + Phrase.FTP_PASSWORD);
				    			break;
				    		}
				    	}
				    }
					
				}		
			}
			if(host != null && port != 0 && userName != null && passWord != null){
				if(isSFTP){
	    	  		ChannelSftp sftp = Utility.connectSFTP(host, port, userName, passWord);
	    	  		
	    	  		Utility.upload(remoteFileDirctory, localFileContent,remoteFileName, sftp);			    				
				}else{
					FTPClient ftpClient = null;
					InputStream input = null;
					try {				
						ftpClient = new FTPClient();
						if(isDebug){
							ftpClient.addProtocolCommandListener(new PrintCommandListener(new PrintWriter(System.out)));
						}
					    int reply;
					    ftpClient.connect(host);
					    reply = ftpClient.getReplyCode();
				        if (!FTPReply.isPositiveCompletion(reply)) {
				        	ftpClient.disconnect();
				            throw new Exception("Exception in connecting to FTP Server");
				        }
				        				        
				        if(ftpClient.login(userName, passWord)){
					        ftpClient.setFileType(FTP.BINARY_FILE_TYPE);
					        ftpClient.enterLocalPassiveMode();
					        input = new ByteArrayInputStream(localFileContent);
					        ftpClient.storeFile(remoteFileDirctory+remoteFileName, input);
				        }else{
				        	throw new Exception("Can't log in FTP Server with given user name and password.");
				        }
					}catch(Exception ex){
						throw ex;
					}finally{
						if (input != null)
							input.close();
						if (ftpClient != null && ftpClient.isConnected()){
			            	ftpClient.logout();
			            	ftpClient.disconnect();
						}
					}
				}
				ret = true;
			}
		}catch(Exception ex){
			throw ex;
		}finally{
		}
		return ret;
	}

	/**
	 * isNullOrEmpty
	 * @param stringVar
	 * @return boolean true is null or empty, false it has value
	 */
	public static boolean isNullOrEmpty(String stringVar) {
		boolean b = false;
		if (stringVar == null || stringVar.equals("") || stringVar.equalsIgnoreCase("null")){
			b = true;
		}
		return b;
	}

	/**
	 * getStringFromInputStream: convert InputStream to String
	 * @param is: InputStream
	 * @return String
	 */
	// 
	public static String getStringFromInputStream(InputStream is) {
        StringBuffer out = new StringBuffer();
        byte[] b = new byte[1024];
        try {
			for (int i; (i = is.read(b)) != -1;) {
			    out.append(new String(b, 0, i));
			}
		} catch (IOException e) {
			e.printStackTrace();
		}
        return out.toString();
	}

	// WI 32922
	  /**
	   * authenticate
	   * @param version
	   * @return String
	   */
	public static String authenticate(String version) throws Exception {
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			String userID = null;
			String password = null;
			String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
			String nodeAddress = null;
			URL node = null;
			String token = null;
			String domainName = "";
			

			userID = config.GetNodeAdminUID();
			password = config.GetNodeAdminPWD();				
			
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				nodeAddress = config.GetNodeURL();
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod);
			}else{
				nodeAddress = config.GetNodeURL_V2();
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod,domainName);
			}
			if (token != null && !token.equals("")) {
				return token;
			} else {
				return null;
			}
		} catch (Exception e) {
			throw e;
		}
	}

	  /**
	   * authenticateRemote
	   * @param version
	   * @param nodeAddress
	   * @param userID
	   * @param password
	   * @param domainName
	   * @return String
	   */
	public static String authenticateRemote(String version, String nodeAddress, String userID, String password,String domainName) throws Exception {
		try {
			String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
			URL node = null;
			String token = null;
						
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod);
			}else{
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
				token = requestor.authenticate(userID, password, authMethod,domainName);
			}
			if (token != null && !token.equals("")) {
				return token;
			} else {
				return null;
			}
		} catch (Exception e) {
			throw e;
		}
	}

	  /**
	   * spliteJsonCompositeKey
	   * @param obj
	   * @return String
	   */
	public static String spliteJsonCompositeKey(String jObj) throws Exception {
		String ret = null;
		try {
			if(!Utility.isNullOrEmpty(jObj)){
				ret = jObj;
				String startId = "\"id\":{";
				String endId = "}";
				String newContent = "";
				int startIdIndex = ret.indexOf(startId);
				int endIdIndex = ret.indexOf(endId,startIdIndex);
				while (startIdIndex != -1 && endIdIndex != -1) {
					if (startIdIndex != -1 && startIdIndex < endIdIndex) {
						newContent = ret.substring(startIdIndex + startId.length(), endIdIndex);
						ret = ret.substring(0,startIdIndex)+newContent+ret.substring(endIdIndex+endId.length(),ret.length());
					}
					startIdIndex = ret.indexOf(startId, endIdIndex);
					endIdIndex = ret.indexOf(endId,startIdIndex);
				}
			}
		} catch (Exception e) {
			throw e;
		}
		return ret;
	}
	/**
	   * submit
	   * @param opID
	   * @param version
	   * @param token
	   * @param transID
	   * @param operationName
	   * @param docs
	   * @param nodeAddress
	   * @return String
	   */
	public static String submit(String version, String token, String transID,
			String operationName, ClsNodeDocument[] docs, String nodeAddress,String dataFlow) throws Exception {
		URL node = null;
		String userID = null;
		String password = null;
		String authMethod = Phrase.AUTHENTICATION_METHOD_PASSWORD;
		String authenDomainName = "default";
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			userID = config.GetNodeAdminUID();
			password = config.GetNodeAdminPWD();				
			if(version.equalsIgnoreCase(Phrase.ver_1)){
				nodeAddress = config.GetNodeURL();
				node = new URL(nodeAddress);
				NodeRequestor requestor1 = new NodeRequestor(node,Phrase.AdministrationLoggerName);
				//token = requestor1.authenticate(userID, password, authMethod);
				if (token !=null ) {
					transID = requestor1.submit(token, null, operationName, docs);
				}
			}else{
				nodeAddress = config.GetNodeURL_V2();
				node = new URL(nodeAddress);
			    Node2.webservice.Requestor.NodeRequestor requestor2 = new Node2.webservice.Requestor.NodeRequestor(node, Phrase.AdministrationLoggerName);
				//token = requestor2.authenticate(userID, password, authMethod, authenDomainName);
				if (token !=null ) {
				    String flowOperation = operationName;
				    String[] recipientList = null;
				    String[] notificationURIList = null;
				    String notificationTypes = "None";
				    transID = requestor2.submit(token, transID, dataFlow, flowOperation,recipientList,notificationURIList,notificationTypes,docs);
				}
			}
		} catch (Exception e) {
			Node.Utils.LoggingUtils.Log("Error - " + e.getMessage(), Level.DEBUG,
					Phrase.AdministrationLoggerName);
			transID = e.getMessage();
		}
		return transID;
	}

	/**
	   * solicit
	   * @param version
	   * @param token
	   * @param transID
	   * @param dataFlow
	   * @param returnURL
	   * @param paramsNames
	   * @param params
	   * @param paramTypes
	   * @param paramEncodes
	   * @return String
	   */
	public static String solicit(String version, String token, String transID,
			String dataFlow,String flowOperation, String returnURL,String[] paramsNames, String[] params,
			String[] paramTypes, String[] paramEncodes) throws Exception {
		String nodeAddress = null;
		URL node = null;
		String result = null;
		try {
			ISystemConfiguration config = DBManager.GetSystemConfiguration(Phrase.AdministrationLoggerName);
			if (version.equalsIgnoreCase(Phrase.ver_1)) {
				nodeAddress = config.GetNodeURL();
				node = new URL(nodeAddress);
				NodeRequestor requestor = new NodeRequestor(node, Phrase.AdministrationLoggerName);
				result = requestor.solicit(token, returnURL, dataFlow, params);
			} else {
				nodeAddress = config.GetNodeURL_V2();
				node = new URL(nodeAddress);
				Node2.webservice.Requestor.NodeRequestor requestor = new Node2.webservice.Requestor.NodeRequestor(
						node, Phrase.AdministrationLoggerName);

			    String domain = dataFlow;
				String[] recipientList = null;
				String notificationTypes = "None";
				String[] notificationURITypeStrList = null;
				result = requestor.solicit(token, domain, flowOperation,
						recipientList, notificationURITypeStrList,
						notificationTypes, paramsNames, params, paramTypes,
						paramEncodes);
			}
			return result;
		} catch (Exception e) {
			Node.Utils.LoggingUtils.Log("Error - Solicit " + e.getMessage(), Level.DEBUG,
					Phrase.AdministrationLoggerName);
			throw e;
		}
	}

	// WI 38874
	public static String createAjaxSql(String sql,String maxPageRow, String pageRowID) throws Exception {
		String ret = null;
		
		ret = "select * from (select a.*,rownum rnum from (" + sql +"  ) a where rownum <= " + maxPageRow +") where rnum > " + pageRowID;
		return ret;
	}

	// WI 39429 jdbc drive change the clob.setAsciiStream(0L); to clob.setAsciiStream(1L); from Oracle 11g R2 (10.2.0.5.0)
	public static OutputStream setAsciiStream(CLOB myCLOB,Connection conn) throws SQLException {
		OutputStream os = null;
		if(myCLOB != null && conn != null){
			DatabaseMetaData meta = conn.getMetaData();
			String oracleVer = meta.getDriverVersion();
			if(!Utility.isNullOrEmpty(oracleVer)){
				String[] verLst = oracleVer.split("\\.");
				if(verLst!=null && verLst.length > 0){
					oracleVer = verLst[0] + "." + verLst[1];
					Double ver = Double.parseDouble(oracleVer);
					if(ver >= 10.2){
						os = myCLOB.setAsciiStream(1L);			
					}else{
						os = myCLOB.setAsciiStream(0L);			
					}					
				}
			}
		}		
		return os;
	}
	
	public static void main(String[] args) {
		
		/*  Test split" 
		 * 
		 * 
		 */
		
		String oracleVer = "10.2.0.5.0";
		String[] verLst = oracleVer.split("\\.");
		oracleVer = verLst[0] + "." + verLst[1];
		System.out.println("oracleVer is " + oracleVer);
		
		/*
		 * Test sftp
  		Utility sf = new Utility();
  		String host = "windsor";
  		int port = 22;
  		String username = "testadmin";
  		String password = "123456789o";
  		String directory = "/tmp";
  		String uploadFile = "D:\\temp\\upload.txt";
  		String downloadFile = "upload.txt";
  		String saveFile = "D:\\temp\\download.txt";
  		String deleteFile = "delete.txt";
  		ChannelSftp sftp=sf.connect(host, port, username, password);
  		sf.upload(directory, uploadFile, sftp);
  		sf.download(directory, downloadFile, saveFile, sftp);
  		//sf.delete(directory, deleteFile, sftp);
  		try{
  			sftp.cd(directory);
  			sftp.mkdir("ss");
  			System.out.println("finished");
  			sftp.disconnect();
  		}catch(Exception e){
  			e.printStackTrace();
  		}*/

		/* Test zip file */
		//Utility.zipFile("GenerateAndSubmitEISFacilityInventory20100316071914.xml", "GenerateAndSubmitEISFacilityInventory20100316071914.zip", "D:\\temp\\");
		//Utility.unZipFile("GenerateAndSubmitEISFacilityInventory20100316071914.zip", "GenerateAndSubmitEISFacilityInventory20100316071914aa", "D:\\temp\\");

		/* Test delete multiple files */
		//Utility.deleteFilesByPrefix("d:\\temp\\", "element");

		/* Test zip multiple files 
		String[] fileNameList = {"CredibleData_2009-11-09.xml","GenerateAndSubmitEISFacilityInventory20100316071914.xml","699submission.xml"};
		Utility.zipFiles(fileNameList, "MyMultiple.zip", "D:\\temp\\");
		 */
		
		/* Test FTP */
//		String ftpIPName = "localhost";
		String ftpIPName = "vn12stockholm";
//		String remoteFileDirctory = "/myfolder/";
		String remoteFileDirctory = "/";
		String remoteFileName = "CHEM_SSR_2012-11-08.xml";
		String localFileDirctory = "C:/temp/";
		String localFileName = "CHEM_SSR_2012-11-08.xml";
		boolean isSFTP = false;
		boolean isDebug = true;
			
		try {
			//Utility.sendFTPfile(ftpIPName, remoteFileDirctory, remoteFileName, localFileDirctory, localFileName, isSFTP, isDebug);
			Utility.sendFTPfile(ftpIPName, remoteFileDirctory, remoteFileName, Utility.readFile(localFileDirctory+localFileName), isSFTP, isDebug);
		} catch (Exception e) {
			e.printStackTrace();
		}
		
		
		System.out.println("ok.");
	}


}
