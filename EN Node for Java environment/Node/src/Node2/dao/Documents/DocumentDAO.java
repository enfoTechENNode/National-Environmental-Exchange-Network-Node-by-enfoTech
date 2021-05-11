package Node2.dao.Documents;

import java.util.List;

import Node2.dao.DAO;
import Node2.model.Configurations.PageLayout;
import Node2.model.Documents.Document;
/**
 * <p>This class create DocumentDAO.</p>
 * <p>Company: enfoTech & Consulting, Inc.</p>
 * @author enfoTech
 * @version 2.0
 */

public interface DocumentDAO extends DAO {
	  /**
	   * getDocuments
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param documentName
	   * @param transId
	   * @param domainName
	   * @param dataFlowName
	   * @param startDate
	   * @param endDate
	   * @param adminDomains
	   * @return List
	   */
    public List getDocuments(String startIndex,String recordsReturned,String sort,String dir,String documentName,String transId,String domainName,String dataFlowName,String startDate,String endDate,String[] adminDomains);

	  /**
	   * getOperationMgrDocuments
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param documentName
	   * @param transId
	   * @param domainName
	   * @param dataFlowName
	   * @param startDate
	   * @param endDate
	   * @param adminDomains
	   * @param conditionNameArr
	   * @param conditionSignArr
	   * @param conditionValueArr
	   * @param conditionStyleArr
	   * @return List
	   */
    public List getOperationMgrDocuments(String startIndex,String recordsReturned,String sort,String dir,String documentName,String transId,String domainName,String[] dataFlowName,String startDate,String endDate,String[] adminDomains,String[] conditionNameArr,String[] conditionSignArr,String[] conditionValueArr,String[] conditionStyleArr);

	  /**
	   * getDocument
	   * @param documentId
	   * @return Document
	   */
    public Document getDocument(Long documentId);

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
    public boolean removeDocument(Long documentId);

	  /**
	   * removeDocuments
	   * @param documentIdJson
	   * @return boolean
	   */
    public boolean removeDocuments(String documentIdJson);
    
	  /**
	   * getTotalRecords
	   * @param 
	   * @return long
	   */
    public long getTotalRecords();
}
