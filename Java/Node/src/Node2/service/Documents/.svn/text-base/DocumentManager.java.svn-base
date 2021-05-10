package Node2.service.Documents;

import java.util.List;

import Node.Biz.Administration.User;
import Node2.model.Documents.Document;
/**
 * <p>This class create DocumentManager.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface DocumentManager {
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
    public List getDocuments(String startIndex,String recordsReturned,String sort,String dir,User admin,String loggerName,String documentName,String transId,String domainName,String dataFlowName,String startDate,String endDate);

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
    public List getOperationMgrDocuments(String startIndex,String recordsReturned,String sort,String dir,User admin,String loggerName,String documentName,String transId,String domainName,String[] dataFlowName,String startDate,String endDate,String conditionName,String conditionSign,String conditionValue,String conditionStyle);

	/**
     * getDocument
     * @param documentId
     * @return Document
     */
    public Document getDocument(String documentId);

	/**
     * saveDocument
     * @param document
     * @return 
     */
    public void saveDocument(Document document);

	/**
     * removeDocument
     * @param documentId
     * @return boolean
     */
    public boolean removeDocument(String documentId);

	/**
     * removeDocuments
     * @param documentIdJson
     * @return boolean
     */
    public boolean removeDocuments(String documentIdJson);
    
	/**
     * getTotalRecords
     * @param 
     * @return String
     */
    public String getTotalRecords();

}