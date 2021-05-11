package Node2.dao.hibernate.Configurations;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;
import Node.Phrase;
import Node2.dao.Configurations.PageLayoutDAO;
import Node2.model.Domains.Operation;
import Node2.model.Configurations.PageLayout;

/**
 * <p>This class interacts with Spring and Hibernate to save and retrieve Page Layout
 * objects.</p>
 * 
 * @author Enfotech
 */
public class PageLayoutDAOHibernate extends HibernateDaoSupport implements PageLayoutDAO {
	private static Log log = LogFactory.getLog(PageLayoutDAOHibernate.class);

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
	   * getPageLayout
	   * @param userId
	   * @return PageLayout
	   */
	public PageLayout getPageLayout(Long userId) {
/*		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		try {
			tx = session.beginTransaction();

			Query query = session.createQuery("from PageLayout a where a.userId = :userId");

			query.setLong("userId", userId);
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
*/	
		PageLayout pageLayout = (PageLayout)getHibernateTemplate().get(PageLayout.class, userId);
        return  pageLayout;
	
	}

	  /**
	   * setPageLayout
	   * @param pageLayout
	   * @param insertOrupdate
	   * @return boolean
	   */
	public boolean setPageLayout(PageLayout pageLayout,String insertOrupdate) {
        //getHibernateTemplate().saveOrUpdate(pageLayout);
		boolean ret = false;
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		try {
			tx = session.beginTransaction();
			if(insertOrupdate.equalsIgnoreCase(Phrase.INSERT))
				session.save(pageLayout);										
			else	session.update(pageLayout);

			tx.commit();
			ret = true;
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
	   * deletePageLayout
	   * @param userId
	   * @return boolean
	   */
	public boolean deletePageLayout(Long userId) {
		boolean ret = false;
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		try {
			tx = session.beginTransaction();
			
			PageLayout pageLayout = (PageLayout)getHibernateTemplate().get(PageLayout.class, userId);
			session.delete(pageLayout);

			tx.commit();
			ret = true;
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

}
