package Node2.webservice.Document;

/**
 * <p>Title: </p>
 * <p>Description: </p>
 * <p>Copyright: Copyright (c) 2002</p>
 * <p>Company: </p>
 * @author Denis Broydo
 * @version 1.0
 */

//import Node.CDX.*;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;

import javax.activation.DataHandler;

import net.exchangenetwork.www.schema.node._2.AttachmentType;
import net.exchangenetwork.www.schema.node._2.DocumentFormatType;
import net.exchangenetwork.www.schema.node._2.NodeDocumentType;

import org.apache.axiom.attachments.ByteArrayDataSource;
import org.apache.axis.attachments.AttachmentPart;
import org.apache.log4j.Level;
import org.w3.www._2005._05.xmlmime.ContentType_type0;

import Node.Phrase;
import Node.Utils.LoggingUtils;
import Node.WebServices.Document.ClsNodeDocument;
/**
 * <p>This class create NodeDocumentContentConverter.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class NodeDocumentContentConverter {

    //protected static LogInterface log = LogFactory.getLog(NodeDocumentContentConverter.class.getName());

    public static final int CONTENT_TYPE_BYTES      = 0;
    public static final int CONTENT_TYPE_ATTACHMENT = 1;

    public static final String[] ATTACHMENT_TYPE_NAMES =  {"text/plain", "text/xml",
                                 "application/octet-stream", "application/octet-stream",
                                 "application/octet-stream","application/octet-stream"};

    public static final int ATTACHMENT_TYPE_TEXT  = 0;
    public static final int ATTACHMENT_TYPE_XML   = 1;
    public static final int ATTACHMENT_TYPE_BIN   = 2;
    public static final int ATTACHMENT_TYPE_ZIP   = 3;
    public static final int ATTACHMENT_TYPE_OTHER = 4;
    public static final int ATTACHMENT_TYPE_IMG = 5;

    public static final String[] CONTENT_TYPE = {"text/plain", "text/xml","application/zip","image/png","application/octet-stream"};

	  /**
	   * Constructor
	   * @param 
	   * @return 
	   */
    public NodeDocumentContentConverter() {
    }

    /**
     * convertToBytes
     * @param doc
     * @return byte[]
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
        else 
        {
            
          LoggingUtils.Log("Converting No Type", Level.DEBUG, Phrase.WebServicesLoggerName);
            throw new IllegalArgumentException("Could not convert to bytes from: " + content
                      + " Argument type is unknown!");
        }

        return dataOUT;
    }

    /**
     * getBytesFromStream
     * @param data
     * @return byte[]
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
     * convertToAttachment
     * @param dataIN
     * @param dataType
     * @return
     * @throws Exception 
     */
    public static DataHandler convertToAttachment(byte[] dataIN, String dataType) throws Exception
    {
        DataHandler dataOUT = null;
        ByteArrayDataSource source = null;
        
        try {
            source = new ByteArrayDataSource(dataIN, getDataTypeName(dataType));
            //source = new ByteArrayDataSource(Base64.encode(dataIN).getBytes(), getDataTypeName(dataType));
        }
        catch (Exception ex) {
            throw ex;
        }
        dataOUT = new DataHandler(source);

        return dataOUT;
    }

    /**
    *
    * @param doc
    * @return
 
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
     * @param documents
     * @return ClsNodeDocument[]
     */
    public static ClsNodeDocument[] convertToLocalNodeDocument(NodeDocumentType[] documents)
    {
        ClsNodeDocument[] localDoc = null;
        ByteArrayOutputStream os = new ByteArrayOutputStream();
        byte[] content = null;
        try
        {
            if((documents!=null)&&(documents.length>0))
            {
            	localDoc = new ClsNodeDocument[documents.length];
            	String documentID = null;
                for(int i=0; i<documents.length; i++)
                {
                  LoggingUtils.Log("Converting Document #" + i + " with name: " + documents[i].getDocumentName(), Level.DEBUG, Phrase.WebServicesLoggerName);
                    ClsNodeDocument newDoc = new ClsNodeDocument();
                    // WI 22317
                    if(documents[i].getDocumentContent().getBase64Binary()!=null){
                     	newDoc.setContent(NodeDocumentContentConverter.getBytesFromStream(documents[i].getDocumentContent().getBase64Binary()));
                    }else{
                    	newDoc.setContent("".getBytes());
                    }
                    newDoc.setType(documents[i].getDocumentFormat().getValue());
                    newDoc.setName(documents[i].getDocumentName());
                    if(documents[i].getDocumentId()!=null && !documents[i].getDocumentId().toString().equals("")){
                    	documentID = documents[i].getDocumentId().toString();
                    }
                    newDoc.setId(documentID);
                    localDoc[i] = newDoc;
                }
            }
        }
        catch(Exception e)
        {
        	localDoc = null;
        }
        return localDoc;
    }

    /**
     * Convert NodeDocument to Node.CDX.ClsNodeDocument
     * @param documents
     * @return NodeDocumentType[]
     */
    public static NodeDocumentType[] convertToNodeDocument(ClsNodeDocument[] documents)
    {
    	NodeDocumentType[] nodeDoc = null;
        try
        {
            if((documents!=null)&&(documents.length>0))
            {
                nodeDoc = new NodeDocumentType[documents.length];
                for(int i=0; i<documents.length; i++)
                {
                	NodeDocumentType newDoc = new NodeDocumentType();
                	DataHandler dh = convertToAttachment(documents[i].getContent(),getDataTypeName(documents[i].getType()));
                	AttachmentType attachmentType = new AttachmentType();
                	ContentType_type0 contentType_type = new ContentType_type0();
            		attachmentType.setContentType(contentType_type);
                    attachmentType.setBase64Binary(dh);
            		newDoc.setDocumentContent(attachmentType);
            		DocumentFormatType documentFormatType = null;               		
                	if(documents[i].getType().equalsIgnoreCase(documentFormatType.XML.toString()) || documents[i].getType().equalsIgnoreCase("text/xml")){
                		documentFormatType = DocumentFormatType.Factory.fromValue(documentFormatType.XML.toString());
                		contentType_type.setContentType_type0(CONTENT_TYPE[1]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.BIN.toString())){
                		documentFormatType = DocumentFormatType.Factory.fromValue(documentFormatType.BIN.toString()); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[3]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.FLAT.toString()) || documents[i].getType().equalsIgnoreCase("text/plain")){
                		documentFormatType = DocumentFormatType.Factory.fromValue(documentFormatType.FLAT.toString()); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[0]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.ODF.toString())){
                		documentFormatType = DocumentFormatType.Factory.fromValue(documentFormatType.ODF.toString()); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[4]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.ZIP.toString()) || documents[i].getType().equalsIgnoreCase("application/zip")){
                		documentFormatType = DocumentFormatType.Factory.fromValue(documentFormatType.ZIP.toString()); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[2]);
                	}else{
                		documentFormatType = DocumentFormatType.Factory.fromValue(documentFormatType.OTHER.toString()); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[4]);
                	}
                    newDoc.setDocumentFormat(documentFormatType);
                    newDoc.setDocumentName(documents[i].getName());
                    // WI 20786
                    if(documents[i].getId()!=null && !documents[i].getId().equalsIgnoreCase("")){
                        org.apache.axis2.databinding.types.Id documentID = new org.apache.axis2.databinding.types.Id(documents[i].getId()==null?"":documents[i].getId());
                        newDoc.setDocumentId(documentID);                    	
                    }
                    nodeDoc[i] = newDoc;
                }
            }
        }
        catch(Exception e)
        {
            nodeDoc = null;
        }
        return nodeDoc;
    }

/*    public static NodeDocumentType[] convertToNodeDocument(ClsNodeDocument[] documents)
    {
    	NodeDocumentType[] nodeDoc = null;
        try
        {
            if((documents!=null)&&(documents.length>0))
            {
                nodeDoc = new NodeDocumentType[documents.length];
                for(int i=0; i<documents.length; i++)
                {
                	NodeDocumentType newDoc = new NodeDocumentType();
                	DataHandler dh = convertToAttachment(documents[i].getContent(),getDataTypeName(documents[i].getType()));
                	AttachmentType attachmentType = new AttachmentType();
                	attachmentType.setBase64Binary(dh);
                	ContentType_type0 contentType_type = new ContentType_type0();
            		attachmentType.setContentType(contentType_type);
            		newDoc.setDocumentContent(attachmentType);
            		DocumentFormatType documentFormatType = null;               		
                	if(documents[i].getType().equalsIgnoreCase(documentFormatType.XML.toString()) || documents[i].getType().equalsIgnoreCase("text/xml")){
                		documentFormatType = DocumentFormatType.Factory.fromValue("XML");
                		contentType_type.setContentType_type0(CONTENT_TYPE[1]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.BIN.toString())){
                		documentFormatType = DocumentFormatType.Factory.fromValue("BIN"); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[3]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.FLAT.toString()) || documents[i].getType().equalsIgnoreCase("text/plain")){
                		documentFormatType = DocumentFormatType.Factory.fromValue("FLAT"); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[0]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.ODF.toString())){
                		documentFormatType = DocumentFormatType.Factory.fromValue("ODF"); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[2]);
                	}else if(documents[i].getType().equalsIgnoreCase(documentFormatType.ZIP.toString()) || documents[i].getType().equalsIgnoreCase("application/zip")){
                		documentFormatType = DocumentFormatType.Factory.fromValue("ZIP"); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[2]);
                	}else{
                		documentFormatType = DocumentFormatType.Factory.fromValue("OTHER"); 
                		contentType_type.setContentType_type0(CONTENT_TYPE[2]);
                	}
                    newDoc.setDocumentFormat(documentFormatType);
                    newDoc.setDocumentName(documents[i].getName());
                    nodeDoc[i] = newDoc;
                }
            }
        }
        catch(Exception e)
        {
            nodeDoc = null;
        }
        return nodeDoc;
    }
*/
    /**
     * getDataTypeName
     * @param dataType
     * @return String
     */
    private static String getDataTypeName(String dataType)
    {
        String out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_OTHER];

        if(dataType.equalsIgnoreCase("flat") || dataType.equalsIgnoreCase("text/plain")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_TEXT];
        }
        else if(dataType.equalsIgnoreCase("xml") || dataType.equalsIgnoreCase("text/xml")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_XML];
        }
        else if(dataType.equalsIgnoreCase("bin")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_BIN];
        }
        else if(dataType.equalsIgnoreCase("zip") || dataType.equalsIgnoreCase("application/zip")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_ZIP];
        }
        else if(dataType.equalsIgnoreCase("image") || dataType.equalsIgnoreCase("image/png")) {
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_IMG];
        }
        else {
            //dataType.equalsIgnoreCase("other")
            out = ATTACHMENT_TYPE_NAMES[ATTACHMENT_TYPE_OTHER];
        }

        return out;
    }

}
