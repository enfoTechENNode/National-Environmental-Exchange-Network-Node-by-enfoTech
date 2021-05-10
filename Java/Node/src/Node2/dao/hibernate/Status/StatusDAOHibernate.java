package Node2.dao.hibernate.Status;

import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import Node2.dao.Domains.OperationDAO;
import Node2.dao.Status.StatusDAO;
import Node2.model.Domains.Operation;
import Node2.model.Status.Status;

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
public class StatusDAOHibernate extends HibernateDaoSupport implements
		StatusDAO {
	private static Log log = LogFactory.getLog(StatusDAOHibernate.class);
	private int totalRecords;

	  /**
	   * getStatusList
	   * @param startIndex
	   * @param recordsReturned
	   * @return List
	   */
	public List getStatusList(String startIndex,String recordsReturned) {
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		try {
			tx = session.beginTransaction();

			Query query = session.createQuery("from Status");

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
	   * getStatusList
	   * @param pId
	   * @return Status
	   */
	public Status getStatus(Long operId) {
		return (Status) getHibernateTemplate().get(Status.class, operId);
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
