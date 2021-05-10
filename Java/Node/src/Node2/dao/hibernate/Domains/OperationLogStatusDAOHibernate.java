package Node2.dao.hibernate.Domains;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;
import Node2.dao.Domains.OperationLogStatusDAO;
import Node2.model.Domains.OperationLogStatus;

/**
 * <p>This class interacts with Spring and Hibernate to save and retrieve Operation
 * objects.</p>
 * 
 * @author Enfotech
 */
public class OperationLogStatusDAOHibernate extends HibernateDaoSupport implements
		OperationLogStatusDAO {
	private static Log log = LogFactory.getLog(OperationLogStatusDAOHibernate.class);
	private int totalRecords;

	  /**
	   * getOperations
	   * @param startIndex
	   * @param recordsReturned
	   * @param opName
	   * @param opType
	   * @param opWebservice
	   * @param opStatus
	   * @param opDomain
	   * @param userId
	   * @param token
	   * @param transactionId
	   * @param startDate
	   * @param endDate
	   * @return List
	   */
    public List getOperationLogStatuss(String startIndex,String recordsReturned,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transactionId,String startDate,String endDate){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		try {
			tx = session.beginTransaction();

			Query query = session.createQuery("from NODE_OPERATION_LOG_STATUS");

			query.setFirstResult(Integer.parseInt(startIndex));
			query.setMaxResults(Integer.parseInt(recordsReturned));
			ret = query.list();
			tx.commit();

		} catch (Exception e) {
			if (tx != null) {
				// Something went wrong; discard all partial changes
				tx.rollback();
			}
			e.printStackTrace();
		} finally {
			// No matter what, close the session
			session.close();
		}
		return ret;
	}

	  /**
	   * getOperationLogStatus
	   * @param operId
	   * @return OperationLogStatus
	   */
	public OperationLogStatus getOperationLogStatus(Long operId) {
		return (OperationLogStatus) getHibernateTemplate().get(OperationLogStatus.class, operId);
	}

	  /**
	   * saveOperationLogStatus
	   * @param operationLogStatus
	   * @return 
	   */
	public void saveOperationLogStatus(OperationLogStatus operationLogStatus) {
		getHibernateTemplate().saveOrUpdate(operationLogStatus);

		if (log.isDebugEnabled()) {
			log.debug("OperationLogStatus id set to: " + operationLogStatus.getOperationLogStatusId());
		}
	}

	  /**
	   * removeOperationLogStatus
	   * @param operId
	   * @return 
	   */
	public void removeOperationLogStatus(Long opLogId) {
		Object operationLogStatus = getHibernateTemplate().load(OperationLogStatus.class, opLogId);
		getHibernateTemplate().delete(operationLogStatus);
	}

	  /**
	   * getTotalRecords
	   * @param 
	   * @return int
	   */
	public int getTotalRecords() {
		return this.totalRecords;
	}

}
