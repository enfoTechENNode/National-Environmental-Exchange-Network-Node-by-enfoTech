package Node.WebServices.Document;

/**
 * <p>Title: </p>
 * <p>Description: </p>
 * <p>Copyright: Copyright (c) 2002</p>
 * <p>Company: </p>
 * @author Denis Broydo
 * @version 1.0
 */

//import Node.CDX.*;
import org.apache.axis.attachments.AttachmentPart;
import org.apache.axis.attachments.ManagedMemoryDataSource;
import org.apache.log4j.Level;

import javax.activation.DataHandler;
import java.io.*;

import Node.Phrase;
import Node.Utils.LoggingUtils;
//import gov.epa.cdx.commons.log.*;

public class NodeDocumentContentConverter {

    //protected static LogInterface log = LogFactory.getLog(NodeDocumentContentConverter.class.getName());

    public static final int CONTENT_TYPE_BYTES      = 0;
    public static final int CONTENT_TYPE_ATTACHMENT = 1;

    public static final String[] ATTACHMENT_TYPE_NAMES =  {"text/plain", "text/xml",
                                 "application/octet-stream", "application/octet-stream", "application/octet-stream"};

    public static final int ATTACHMENT_TYPE_TEXT  = 0;
    public static final int ATTACHMENT_TYPE_XML   = 1;
    public static final int ATTACHMENT_TYPE_BIN   = 2;
    public static final int ATTACHMENT_TYPE_ZIP   = 3;
    public static final int ATTACHMENT_TYPE_OTHER = 4;

    public NodeDocumentContentConverter() {
    }

    /**
     * @todo: Think about else case!
     * @param doc
     * @return
     */
    public static byte[] convertToBytes(NodeDocument doc)
    {
        Object content = null;
        byte[] dataOUT = null;

        content = doc.getContent();
        if(content instanceof byte[])
        {
          LoggingUtils.Log("Converting byte[]", Level.DEBUG, Phrase.WebServicesLoggerName);
            dataOUT = (byte[])content;
        }
        else if(content instanceof DataHandler)
        {
          LoggingUtils.Log("Converting DataHandler", Level.DEBUG, Phrase.WebServicesLoggerName);
            DataHandler dhData = (DataHandler)content;
            dataOUT = getBytesFromStream(dhData);
        }
        else if(content instanceof AttachmentPart)
        {
          LoggingUtils.Log("Converting AttachmentPart", Level.DEBUG, Phrase.WebServicesLoggerName);
            AttachmentPart attch = (AttachmentPart)content;
            dataOUT = getBytesFromStream(attch.getActivationDataHandler());
        }
        else //?????
        {
            //?????
          LoggingUtils.Log("Converting No Type", Level.DEBUG, Phrase.WebServicesLoggerName);
            throw new IllegalArgumentException("Could not convert to bytes from: " + content
                      + " Argument type is unknown!");
        }

        return dataOUT;
    }

    /**
     * @todo: Think about IOException
     * @param data
     * @return
     */
    private static byte[] getBytesFromStream(DataHandler data)
    {
        InputStream in = null;
        byte[] out = null;

        try {
            in = data.getInputStream();
            if(in != null)
            {
                out = new byte[in.available()];
                in.read(out);
            }
            else
            {
                out = new byte[0];
            }
        }
        catch (IOException ex) {
            //log.error("Could not get Bytes.", ex);
            //throw ex;
        }

        return out;
    }

    /**
     *
     * @param doc
     * @return
     */
    public static DataHandler convertToAttachment(byte[] dataIN, String dataType)
    {
        DataHandler dataOUT = null;
        ByteArrayInputStream byteStream = null;
        ManagedMemoryDataSource source = null;
        byteStream = new ByteArrayInputStream(dataIN);

        try {
            source = new ManagedMemoryDataSource(byteStream, 10240, getDataTypeName(dataType), true);
        }
        catch (IOException ex) {
            //log.error("Could not convert to attachment.", ex);
            return null;
        }
        dataOUT = new DataHandler(source);

        return dataOUT;
    }

    /**
     * Convert NodeDocument to Node.CDX.ClsNodeDocument
     * @param doc[]
     * @return ClsNodeDocument[]
     */
    public static ClsNodeDocument[] convertToNodeDocument(NodeDocument[] documents)
    {
        ClsNodeDocument[] cdxDoc = null;
        try
        {
            if((documents!=null)&&(documents.length>0))
            {
                cdxDoc = new ClsNodeDocument[documents.length];
                for(int i=0; i<documents.length; i++)
                {
                  LoggingUtils.Log("Converting Document #" + i + " with name: " + documents[i].getName(), Level.DEBUG, Phrase.WebServicesLoggerName);
                    ClsNodeDocument newDoc = new ClsNodeDocument();
                    newDoc.setContent(documents[i].obtainContentBytes());
                    newDoc.setType(documents[i].getType());
                    newDoc.setName(documents[i].getName());
                    cdxDoc[i] = newDoc;
                }
            }
        }
        catch(Exception e)
        {
            cdxDoc = null;
        }
        return cdxDoc;
    }

    /**
     * Convert NodeDocument to Node.CDX.ClsNodeDocument
     * @param ClsNodeDocument[]
     * @return doc[]
     */
    public static NodeDocument[] convertToNodeDocument(ClsNodeDocument[] documents)
    {
        NodeDocument[] nodeDoc = null;
        try
        {
            // WI 21157
            if((documents!=null)&&(documents.length>0))
            {
                nodeDoc = new NodeDocument[documents.length];
                for(int i=0; i<documents.length; i++)
                {
                    NodeDocument newDoc = new NodeDocument();
                    if(documents[i].getContent() != null){
                        newDoc.setContent(documents[i].getContent());                    	
                        newDoc.setName(documents[i].getName());
                    }else{
                        newDoc.setContent("Empty Content");                   	
                    }
                    if(documents[i].getName()!=null){
                        newDoc.setName(documents[i].getName());
                    }else{
                        newDoc.setName("Empty Name");                   	
                    }
                    if(documents[i].getType()!=null){
                        newDoc.setType(documents[i].getType());
                    }else{
                        newDoc.setType("Empty Type");                   	
                    }                    
                    nodeDoc[i] = newDoc;
                }
            }else{
                nodeDoc = new NodeDocument[1];
                NodeDocument newDoc = new NodeDocument();
                newDoc.setContent("Empty Content");                   	                
                newDoc.setType("Empty Type");
                newDoc.setName("Empty Name");
                nodeDoc[0] = newDoc;            	
            }
        }
        catch(Exception e)
        {
            nodeDoc = null;
        }
        return nodeDoc;
    }

    /**
     * getDataTypeName
     * @param dataType
     * @return String
     */
    private static String getDataTypeName(String dataType)
    {
        String out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_OTHER];

        if(dataType.equalsIgnoreCase("flat")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_TEXT];
        }
        else if(dataType.equalsIgnoreCase("xml")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_XML];
        }
        else if(dataType.equalsIgnoreCase("bin")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_BIN];
        }
        else if(dataType.equalsIgnoreCase("zip")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_ZIP];
        }
        else {
            //dataType.equalsIgnoreCase("other")
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_OTHER];
        }

        return out;
    }

}
