package Node2.dao.hibernate.Domains;

import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.TimeZone;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.Query;
import org.hibernate.Session;
import org.hibernate.Transaction;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;

import Node.Phrase;
import Node.Utils.Utility;
import Node2.dao.Domains.DomainDAO;
import Node2.model.Domains.Domain;

/**
 * <p>This class interacts with Spring and Hibernate to save and retrieve Operation
 * objects.</p>
 * 
 * @author Enfotech
 */
public class DomainDAOHibernate extends HibernateDaoSupport implements DomainDAO {
	private static Log log = LogFactory.getLog(DomainDAOHibernate.class);
	private long totalRecords;

	  /**
	   * getDomains
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param domainPermissions
	   * @param domainName
	   * @param status
	   * @param statusMessage
	   * @return List
	   */
    public List getDomains(String startIndex,String recordsReturned,String sort,String dir,String[] domainPermissions,String domainName,String status,String statusMessage){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;
		
		try {
			sql = "select distinct a.domainId,a.domainName,e.loginName,a.domainStatusCD,a.domainStatusMSG ";
			//sqlCount = "select count(*) ";
			/*tableName = "from Domain a left outer join a.webservices b left outer join a.accountTypeXrefs c " +
			"left outer join c.accountType d left outer join c.user e ";*/
			tableName = "from Domain a left outer join a.webservices b left outer join a.accountTypeXrefs c " +
			"left outer join c.accountType d left outer join c.user e ";
			condition = "where 1=1 ";

		    if (domainPermissions != null && domainPermissions.length > 0) {
		    	condition = condition + " and a.domainName in (";
		        for (int i = 0; i < domainPermissions.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+domainPermissions[i]+"'";
		        }
		        condition += ")";
		    }

			if(sort.equalsIgnoreCase("domainId") || sort.equalsIgnoreCase("domainName")
				|| sort.equalsIgnoreCase("domainStatusCD") || sort.equalsIgnoreCase("domainStatusMSG"))
				sortDir = " order by " + "a."+ sort + " " + dir;
						
			if (domainName != null && !domainName.equals(""))
				condition += " and a.domainName = :domainName";
			if (status != null && !status.equals(""))
				condition += " and a.domainStatusCD = :domainStatusCD";
			if (statusMessage != null && !statusMessage.equals(""))
				condition += " and a.domainStatusMSG = :domainStatusMSG";

			tx = session.beginTransaction();
			
			sql+=tableName+condition+sortDir;
			sqlCount+=tableName+condition;
			Query query = session.createQuery(sql);
			//Query queryCount = session.createQuery(sqlCount);
			
/*			query.setString("ConsoleUser", Phrase.ConsoleUser);
			queryCount.setString("ConsoleUser", Phrase.ConsoleUser);			
*/			if (domainName != null && !domainName.equals("")){
				query.setString("domainName", domainName);
				//queryCount.setString("domainName", domainName);
			}
			if (status != null && !status.equals("")){
				query.setString("domainStatusCD", status);
				//queryCount.setString("domainStatusCD", status);
			}
			if (statusMessage != null && !statusMessage.equals("")){
				query.setString("domainStatusMSG", statusMessage);
				//queryCount.setString("domainStatusMSG", statusMessage);			
			}

			//ret = queryCount.list();

			// Here to get ride of startIndex and recordsReturned for getting all records
			//query.setFirstResult(Integer.parseInt(startIndex));
			//query.setMaxResults(Integer.parseInt(recordsReturned));
			ret = query.list();
			tx.commit();

			// get real domain count divide by administrator
			String records = "";
			Object[] retList;
			List jsonList = new ArrayList();
			String tmpDomainId = "";
			String tmpDomainAdminList = "";
			int recordIndex = -1;
			for(int i=0;i<ret.size();i++){
	        	retList = (Object[])ret.get(i);
	        	if(tmpDomainId.equalsIgnoreCase(((Long)retList[0]).toString())){	// append admin to the adminlist
	        		tmpDomainAdminList = tmpDomainAdminList + "," +(String)retList[2];
	        		jsonList.set(recordIndex,"{\"gridId\":" + i
	                        +",\"domainId\":" + "\"" + retList[0] + "\"" 
	                        +",\"domainName\":" + "\"" + (String)retList[1] + "\"" 
	            			+",\"domainAdmin\":" + "\"" + tmpDomainAdminList + "\"" 
	            			+",\"domainStatusCD\":" + ((String)retList[3]==null?"\"" + "\"":"\""+(String)retList[3]+ "\"")
	            			+",\"domainStatusMSG\":" + ((String)retList[4]==null?"\"" + "\"":"\""+(String)retList[4]+ "\"")
	            			+"}");
	        	}else{
	                jsonList.add("{\"gridId\":" + i
	                        +",\"domainId\":" + "\"" + retList[0] + "\"" 
	                        +",\"domainName\":" + "\"" + (String)retList[1] + "\"" 
	            			+",\"domainAdmin\":" + ((String)retList[2]==null?"\"" + "\"":"\""+(String)retList[2]+ "\"") 
	            			+",\"domainStatusCD\":" + ((String)retList[3]==null?"\"" + "\"":"\""+(String)retList[3]+ "\"")
	            			+",\"domainStatusMSG\":" + ((String)retList[4]==null?"\"" + "\"":"\""+(String)retList[4]+ "\"")
	            			+"}");
	                recordIndex ++;
	            	tmpDomainAdminList = (String)retList[2];
	        	}
	        	tmpDomainId = ((Long)retList[0]).toString();
			}
			this.totalRecords = jsonList.size();

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
	   * getDomain
	   * @param domainId
	   * @return Domain
	   */
	public Domain getDomain(Long domainId) {
		return (Domain) getHibernateTemplate().get(Domain.class, domainId);
	}

	  /**
	   * saveDomain
	   * @param domain
	   * @return 
	   */
	public void saveDomain(Domain domain) {
		getHibernateTemplate().saveOrUpdate(domain);

		if (log.isDebugEnabled()) {
			log.debug("Domain id set to: " + domain.getDomainId());
		}
	}

	  /**
	   * removeDomain
	   * @param domainId
	   * @return 
	   */
	public void removeDomain(Long domainId) {
		Object domain = getHibernateTemplate().load(Domain.class, domainId);
		getHibernateTemplate().delete(domain);
	}

	  /**
	   * getTotalRecords
	   * @param 
	   * @return long
	   */
	public long getTotalRecords() {
		return this.totalRecords;
	}

}
