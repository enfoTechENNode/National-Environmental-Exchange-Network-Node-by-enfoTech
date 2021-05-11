package Node2.dao.hibernate.Domains;

import java.util.List;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;
import Node2.dao.Domains.WebserviceDAO;
import Node2.model.Domains.Webservice;

/**
 * <p>This class interacts with Spring and Hibernate to save and retrieve Webservice
 * objects.</p>
 * 
 * @author Enfotech
 */
public class WebserviceDAOHibernate extends HibernateDaoSupport implements WebserviceDAO {
	private static Log log = LogFactory.getLog(WebserviceDAOHibernate.class);
	private int totalRecords;

	  /**
	   * getWebservices
	   * @param startIndex
	   * @param recordsReturned
	   * @param webserviceName
	   * @param webserviceDesc
	   * @return List
	   */
    public List getWebservices(String startIndex,String recordsReturned,String webserviceName,String webserviceDesc){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		try {
			tx = session.beginTransaction();

			Query query = session.createQuery("from NODE_WEB_SERVICE");

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
	   * getWebservice
	   * @param webserviceId
	   * @return Webservice
	   */
	public Webservice getWebservice(Long webserviceId) {
		return (Webservice) getHibernateTemplate().get(Webservice.class, webserviceId);
	}

	  /**
	   * saveWebservice
	   * @param webservice
	   * @return 
	   */
	public void saveWebservice(Webservice webservice) {
		getHibernateTemplate().saveOrUpdate(webservice);

		if (log.isDebugEnabled()) {
			log.debug("Webservice id set to: " + webservice.getWebServiceId());
		}
	}

	  /**
	   * removeWebservice
	   * @param webserviceId
	   * @return 
	   */
	public void removeWebservice(Long webserviceId) {
		Object webservice = getHibernateTemplate().load(Webservice.class, webserviceId);
		getHibernateTemplate().delete(webservice);
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
