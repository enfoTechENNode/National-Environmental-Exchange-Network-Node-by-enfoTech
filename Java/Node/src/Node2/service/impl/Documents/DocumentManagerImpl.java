package Node2.service.impl.Documents;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import Node.Biz.Administration.User;
import Node2.dao.Documents.DocumentDAO;
import Node2.model.Documents.Document;
import Node2.service.Documents.DocumentManager;
/**
 * <p>This class create DocumentManagerImpl.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public class DocumentManagerImpl implements DocumentManager {
    private static Log log = LogFactory.getLog(DocumentManagerImpl.class);
    private DocumentDAO dao;

	/**
     * setDocumentDAO
     * @param dao
     * @return 
     */
    public void setDocumentDAO(DocumentDAO dao) {
        this.dao = dao;
    }

	  /**
	   * getDocuments
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param admin
	   * @param loggerName
	   * @param documentName
	   * @param transId
	   * @param domainName
	   * @param dataFlowName
	   * @param startDate
	   * @param endDate
	   * @return List
	   */
    public List getDocuments(String startIndex,String recordsReturned,String sort,String dir,User admin,String loggerName,String documentName,String transId,String domainName,String dataFlowName,String startDate,String endDate){
        String[] adminDomains = null;
        if (!admin.IsNodeAdmin())
            adminDomains = admin.GetAssignedDomains();
    	
    	return dao.getDocuments( startIndex, recordsReturned, sort, dir, documentName, transId, domainName, dataFlowName, startDate, endDate, adminDomains);
    }

	  /**
	   * getOperationMgrDocuments
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param admin
	   * @param loggerName
	   * @param documentName
	   * @param transId
	   * @param domainName
	   * @param dataFlowName
	   * @param startDate
	   * @param endDate
	   * @param conditionName
	   * @param conditionSign
	   * @param conditionValue
	   * @param conditionStyle
	   * @return List
	   */
    public List getOperationMgrDocuments(String startIndex,String recordsReturned,String sort,String dir,User admin,String loggerName,String documentName,String transId,String domainName,String[] dataFlowName,String startDate,String endDate,String conditionName,String conditionSign,String conditionValue,String conditionStyle){
        String[] adminDomains = null;
        String[] conditionNameArr = null;
        String[] conditionSignArr = null;
        String[] conditionValueArr = null;
        String[] conditionStyleArr = null;
        if (!admin.IsNodeAdmin())
            adminDomains = admin.GetAssignedDomains();
    	if(conditionName != null && !conditionName.equals("")){
    		conditionNameArr = conditionName.split(",");
    		conditionSignArr = conditionSign.split(",");
    		conditionValueArr = conditionValue.split(",");
    		conditionStyleArr = conditionStyle.split(",");    		
    	}
    	return dao.getOperationMgrDocuments( startIndex, recordsReturned, sort, dir, documentName, transId, domainName, dataFlowName, startDate, endDate, adminDomains, conditionNameArr, conditionSignArr,conditionValueArr,conditionStyleArr);
    }

	/**
     * getDocument
     * @param documentId
     * @return Document
     */
    public Document getDocument(String DocumentId) {
        Document document = dao.getDocument(Long.valueOf(DocumentId));

        if (document == null) {
            log.warn("DocumentId '" + DocumentId + "' not found in database.");
        }

        return document;
    }

	/**
     * saveDocument
     * @param document
     * @return 
     */
    public void saveDocument(Document document) {
        dao.saveDocument(document);
    }

	/**
     * removeDocument
     * @param documentId
     * @return boolean
     */
    public boolean removeDocument(String documentId) {
        return dao.removeDocument(Long.valueOf(documentId));
    }
    
	/**
     * removeDocuments
     * @param documentIdJson
     * @return boolean
     */
    public boolean removeDocuments(String documentIdJson) {
        return dao.removeDocuments(documentIdJson);
    }
    
	/**
     * getTotalRecords
     * @param 
     * @return String
     */
    public String getTotalRecords(){
    	return Long.toString(dao.getTotalRecords());
    }

}
