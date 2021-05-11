package Node2.dao.hibernate.Domains;

import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import Node2.dao.Domains.OperationDAO;
import Node2.model.Domains.Operation;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;

/**
 * <p>This class interacts with Spring and Hibernate to save and retrieve Operation
 * objects.</p>
 * 
 * @author Enfotech
 */
public class OperationDAOHibernate extends HibernateDaoSupport implements
		OperationDAO {
	private static Log log = LogFactory.getLog(OperationDAOHibernate.class);
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
	   * @return List
	   */
	public List getOperations(String startIndex,String recordsReturned,String opName,String opType,String opWebservice,String opStatus,String opDomain) {
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		try {
			tx = session.beginTransaction();

			Query query = session.createQuery("from Operation");

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
	   * getOperation
	   * @param operId
	   * @return Operation
	   */
	public Operation getOperation(Long operId) {
		return (Operation) getHibernateTemplate().get(Operation.class, operId);
	}

	  /**
	   * saveOperation
	   * @param Operation
	   * @return 
	   */
	public void saveOperation(Operation Operation) {
		getHibernateTemplate().saveOrUpdate(Operation);

		if (log.isDebugEnabled()) {
			log.debug("Operation id set to: " + Operation.getOperationId());
		}
	}

	  /**
	   * removeOperation
	   * @param operId
	   * @return 
	   */
	public void removeOperation(Long operId) {
		Object Operation = getHibernateTemplate().load(Operation.class, operId);
		getHibernateTemplate().delete(Operation);
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
