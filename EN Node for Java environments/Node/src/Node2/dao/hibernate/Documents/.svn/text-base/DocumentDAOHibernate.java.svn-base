package Node2.dao.hibernate.Documents;

import java.math.BigDecimal;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.Iterator;
import java.util.List;
import java.util.TimeZone;

import net.sf.json.JSONArray;
import net.sf.json.JSONObject;

import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.Query;
import org.hibernate.SQLQuery;
import org.hibernate.Session;
import org.hibernate.Transaction;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;

import com.enfotech.basecomponent.jndi.JNDIAccess;

import Node.Phrase;
import Node.Utils.Utility;
import Node2.dao.Documents.DocumentDAO;
import Node2.dao.Domains.DomainDAO;
import Node2.model.Documents.Document;
import Node2.model.Domains.Domain;

/**
 * <p>This class interacts with Spring and Hibernate to save and retrieve Operation
 * objects.</p>
 * 
 * @author Enfotech
 */
public class DocumentDAOHibernate extends HibernateDaoSupport implements DocumentDAO {
	private static Log log = LogFactory.getLog(DocumentDAOHibernate.class);
	private long totalRecords;

/*    public List getDocuments(String startIndex,String recordsReturned,String sort,String dir,String documentName,String transId,String domainName,String dataFlowName,String startDate,String endDate,String[] adminDomains){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;
		
		try {
			sql = "select a.fileId,a.fileName,a.fileType,a.fileSize,a.transId,c.domainName,a.dataFlowName,a.updatedDate ";
			sqlCount = "select count(*) ";
			tableName = "from Document a ,Operation b left outer join b.domain c ";
			condition = "where 1=1 ";

		    if (adminDomains != null && adminDomains.length > 0) {
		    	condition = condition + " and a.fileName in (";
		        for (int i = 0; i < adminDomains.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+adminDomains[i]+"'";
		        }
		        condition += ")";
		    }

			if(sort.equalsIgnoreCase("fileId") || sort.equalsIgnoreCase("fileName")
					|| sort.equalsIgnoreCase("fileType") || sort.equalsIgnoreCase("fileSize")
					|| sort.equalsIgnoreCase("transId") || sort.equalsIgnoreCase("dataFlowName")
					|| sort.equalsIgnoreCase("updatedDate")
			)
				sortDir = " order by " + "a."+ sort + " " + dir;
			else sortDir = " order by " + "c."+ sort + " " + dir;
						
			if (documentName != null && !documentName.equals(""))
				condition += " and a.fileName like :fileName";
			if (transId != null && !transId.equals(""))
				condition += " and a.transId like :transId";
			if (domainName != null && !domainName.equals(""))
				condition += " and b.domainName like :domainName";
			if (dataFlowName != null && !dataFlowName.equals(""))
				condition += " and a.dataFlowName like :dataFlowName";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.startDate > :startDate or a.endDate > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.startDate < :endDate or a.endDate < :endDate)";				

			tx = session.beginTransaction();
			
			sql+=tableName+condition+sortDir;
			sqlCount+=tableName+condition;
			Query query = session.createQuery(sql);
			Query queryCount = session.createQuery(sqlCount);
			
			query.setString("ConsoleUser", Phrase.ConsoleUser);
			queryCount.setString("ConsoleUser", Phrase.ConsoleUser);			

			if (documentName != null && !documentName.equals("")){
				query.setString("fileName", documentName+"%");
				queryCount.setString("fileName", documentName+"%");
			}
			if (transId != null && !transId.equals("")){
				query.setString("transId", transId+"%");
				queryCount.setString("transId", transId+"%");
			}
			if (domainName != null && !domainName.equals("")){
				query.setString("domainName", domainName+"%");
				queryCount.setString("domainName", domainName+"%");			
			}
			if (dataFlowName != null && !dataFlowName.equals("")){
				query.setString("dataFlowName", dataFlowName+"%");
				queryCount.setString("dataFlowName", dataFlowName+"%");			
			}
			if (startDate != null && !startDate.equals("")){
				Date startDt = Utility.ChangeStringToDate(startDate);
				query.setDate("startDate", startDt);
				queryCount.setDate("startDate", startDt);
			}
			if (endDate != null && !endDate.equals("")){
				Date endDt = Utility.ChangeStringToDate(endDate);
		        Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		        cal.setTime(endDt);
		        cal.add(Calendar.DAY_OF_MONTH,1);
		        endDt = cal.getTime();
				query.setDate("endDate", endDt);
				queryCount.setDate("endDate", endDt);			
			}

			ret = queryCount.list();
			this.totalRecords = ((Long)ret.get(0)).longValue();

			// Here to get ride of startIndex and recordsReturned for getting all records
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

*/
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
    public List getDocuments(String startIndex,String recordsReturned,String sort,String dir,String documentName,String transId,String domainName,String dataFlowName,String startDate,String endDate,String[] adminDomains){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;
		
		try {
			sql = "select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') SUBMIT_DTTM ";
			sqlCount = "select count(*) ";
			//tableName = "from (select * from NODE_FILE_CABIN where rownum <=" + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)) + " ORDER BY FILE_ID DESC) a " +
			//tableName = " from (select * from NODE_FILE_CABIN where FILE_ID > (select max(FILE_ID) from NODE_FILE_CABIN) - " + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)) + " ) a "
			// initial just get fix record
			if((documentName == null || documentName.equals("")) && (transId == null || transId.equals("")) && (domainName == null || domainName.equals("")) && (dataFlowName == null || dataFlowName.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				tableName = " from (select * from NODE_FILE_CABIN where FILE_ID > (select max(FILE_ID) from NODE_FILE_CABIN) - " + Integer.parseInt(recordsReturned) + " ) a ";
			}else{
				tableName = " from NODE_FILE_CABIN a ";
			}
			tableName += " left outer join NODE_OPERATION b on upper(a.DATAFLOW_NAME) = upper(b.OPERATION_NAME) "
					+ " left outer join NODE_DOMAIN c on b.DOMAIN_ID = c.DOMAIN_ID ";
			condition = "where 1=1 ";

		    if (adminDomains != null && adminDomains.length > 0) {
		    	condition = condition + " and c.DOMAIN_NAME in (";
		        for (int i = 0; i < adminDomains.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+adminDomains[i]+"'";
		        }
		        condition += ")";
		    }

			if(sort.equalsIgnoreCase("fileId"))
					sortDir = " order by " + "FILE_ID " + dir;
			else if(sort.equalsIgnoreCase("fileName"))
				sortDir = " order by " + "FILE_NAME " + dir;
			else if(sort.equalsIgnoreCase("fileType"))
				sortDir = " order by " + "FILE_TYPE " + dir;
			else if(sort.equalsIgnoreCase("fileSize"))
				sortDir = " order by " + "FILE_SIZE " + dir;
			else if(sort.equalsIgnoreCase("transId"))
				sortDir = " order by " + "TRANS_ID " + dir;
			else if(sort.equalsIgnoreCase("dataFlowName"))
				sortDir = " order by " + "DATAFLOW_NAME " + dir;
			else if(sort.equalsIgnoreCase("updatedDate"))
				sortDir = " order by " + "TO_DATE(SUBMIT_DTTM,'MM/DD/YYYY HH24:MI:SS') " + dir;			
			else sortDir = " order by " + "DOMAIN_NAME  " + dir;
						
			if (documentName != null && !documentName.equals(""))
				condition += " and upper(a.FILE_NAME) like '"+"%"+documentName.toUpperCase()+"%'";
			if (transId != null && !transId.equals(""))
				condition += " and a.TRANS_ID like '"+"%"+transId+"%'";
			if (domainName != null && !domainName.equals(""))
				condition += " and upper(c.DOMAIN_NAME) like '"+"%"+domainName.toUpperCase()+"%'";
			if (dataFlowName != null && !dataFlowName.equals(""))
				condition += " and upper(a.DATAFLOW_NAME) like '"+"%"+dataFlowName.toUpperCase()+"%'";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.SUBMIT_DTTM > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.SUBMIT_DTTM < :endDate)";				
//			if (startDate != null && !startDate.equals("")){
//				Date startDt = Utility.ChangeStringToDate(startDate);
//				condition += " and (a.SUBMIT_DTTM >= "+ startDt+ ")";
//			}
//			if	(endDate != null && !endDate.equals("")){
//				Date endDt = Utility.ChangeStringToDate(endDate);
//		        Calendar cal = Calendar.getInstance(TimeZone.getDefault());
//		        cal.setTime(endDt);
//		        cal.add(Calendar.DAY_OF_MONTH,1);
//		        endDt = cal.getTime();
//				condition += " and (a.SUBMIT_DTTM <= "+ endDt+ ")";				
//			}

			tx = session.beginTransaction();			
			
			//condition += (" and rownum <= " + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)));
			sql+=tableName+condition;
			sqlCount+=tableName+condition;
			
			SQLQuery query = session.createSQLQuery("select distinct * from (" + sql + " ) " + sortDir);
			SQLQuery queryCount = session.createSQLQuery("select count(*) from (" + "select distinct * from (" + sql + " ) " + sortDir + ")");
			
			if (startDate != null && !startDate.equals("")){
				Date startDt = Utility.ChangeStringToDate(startDate);
				query.setDate("startDate", startDt);
				queryCount.setDate("startDate", startDt);
			}
			if (endDate != null && !endDate.equals("")){
				Date endDt = Utility.ChangeStringToDate(endDate);
		        Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		        cal.setTime(endDt);
		        cal.add(Calendar.DAY_OF_MONTH,1);
		        endDt = cal.getTime();
				query.setDate("endDate", endDt);
				queryCount.setDate("endDate", endDt);			
			}

			// initial just get fix record
			if((documentName == null || documentName.equals("")) && (transId == null || transId.equals("")) && (domainName == null || domainName.equals(""))  && (dataFlowName == null || dataFlowName.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				this.totalRecords = Integer.parseInt(recordsReturned);			
			}else{
				ret = queryCount.list();
				this.totalRecords = ((BigDecimal)ret.get(0)).longValue();
			}

			// Here to get ride of startIndex and recordsReturned for getting all records
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
	
/*    public List getOperationMgrDocuments(String startIndex,String recordsReturned,String sort,String dir,String documentName,String transId,String domainName,String[] dataFlowName,String startDate,String endDate,String[] adminDomains,String[] conditionNameArr,String[] conditionSignArr,String[] conditionValueArr,String[] conditionStyleArr){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;
		
		try {
			sql = "select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') ";
			sqlCount = "select count(*) ";
			// different parameters need to join different tables
			if(conditionNameArr !=null && conditionSignArr!=null && conditionValueArr!=null && conditionStyleArr!=null
			&& conditionNameArr.length>0 && conditionSignArr.length>0 && conditionValueArr.length>0 && conditionStyleArr.length>0){
				tableName = "from NODE_FILE_CABIN a left outer join NODE_OPERATION b on a.DATAFLOW_NAME = b.OPERATION_NAME " +
				"left outer join NODE_DOMAIN c on b.DOMAIN_ID = c.DOMAIN_ID " +
				"inner join NODE_OPERATION_LOG d on d.TRANS_ID = a.TRANS_ID " +
				"inner join NODE_OPERATION_LOG_PARAMETER e on e.OPERATION_LOG_ID = d.OPERATION_LOG_ID ";				
			}else{
				tableName = "from NODE_FILE_CABIN a left outer join NODE_OPERATION b on a.DATAFLOW_NAME = b.OPERATION_NAME " +
				"left outer join NODE_DOMAIN c on b.DOMAIN_ID = c.DOMAIN_ID " ;				
			}
			condition = "where 1=1 ";

		    if (adminDomains != null && adminDomains.length > 0) {
		    	condition = condition + " and c.DOMAIN_NAME in (";
		        for (int i = 0; i < adminDomains.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+adminDomains[i]+"'";
		        }
		        condition += ")";
		    }

			if(sort.equalsIgnoreCase("fileId"))
					sortDir = " order by " + "a.FILE_ID " + dir;
			else if(sort.equalsIgnoreCase("fileName"))
				sortDir = " order by " + "a.FILE_NAME " + dir;
			else if(sort.equalsIgnoreCase("fileType"))
				sortDir = " order by " + "a.FILE_TYPE " + dir;
			else if(sort.equalsIgnoreCase("fileSize"))
				sortDir = " order by " + "a.FILE_SIZE " + dir;
			else if(sort.equalsIgnoreCase("transId"))
				sortDir = " order by " + "a.TRANS_ID " + dir;
			else if(sort.equalsIgnoreCase("dataFlowName"))
				sortDir = " order by " + "a.DATAFLOW_NAME " + dir;
			else if(sort.equalsIgnoreCase("updatedDate"))
				sortDir = " order by " + "a.SUBMIT_DTTM " + dir;			
			else sortDir = " order by " + "c.DOMAIN_NAME  " + dir;
						
			if (documentName != null && !documentName.equals(""))
				condition += " and a.FILE_NAME like '"+"%"+documentName+"%'";
			if (transId != null && !transId.equals(""))
				condition += " and a.TRANS_ID like '"+"%"+transId+"%'";
			if (domainName != null && !domainName.equals(""))
				condition += " and c.DOMAIN_NAME like '"+"%"+domainName+"%'";
			if (dataFlowName != null && dataFlowName.length > 0){
			    if (dataFlowName != null && dataFlowName.length > 0) {
			    	condition = condition + " and a.DATAFLOW_NAME in (";
			        for (int i = 0; i < dataFlowName.length; i++) {
			          if (i != 0)
			        	  condition += ",";
			          condition += "'"+dataFlowName[i]+"'";
			        }
			        condition += ")";
			    }
			}
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.SUBMIT_DTTM > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.SUBMIT_DTTM < :endDate)";				

			// add extra parameters
			if(conditionNameArr !=null && conditionSignArr!=null && conditionValueArr!=null && conditionStyleArr!=null
					&& conditionNameArr.length>0 && conditionSignArr.length>0 && conditionValueArr.length>0 && conditionStyleArr.length>0){
				condition += this.createParameter(conditionNameArr,conditionSignArr,conditionValueArr,conditionStyleArr);
			}
			
			
			tx = session.beginTransaction();			
			
			sql+=tableName+condition+sortDir;
			sqlCount+=tableName+condition;
			
			SQLQuery query = session.createSQLQuery(sql);
			SQLQuery queryCount = session.createSQLQuery(sqlCount);
			
			if (startDate != null && !startDate.equals("")){
				Date startDt = Utility.ChangeStringToDate(startDate);
				query.setDate("startDate", startDt);
				queryCount.setDate("startDate", startDt);
			}
			if (endDate != null && !endDate.equals("")){
				Date endDt = Utility.ChangeStringToDate(endDate);
		        Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		        cal.setTime(endDt);
		        cal.add(Calendar.DAY_OF_MONTH,1);
		        endDt = cal.getTime();
				query.setDate("endDate", endDt);
				queryCount.setDate("endDate", endDt);			
			}

			ret = queryCount.list();
			this.totalRecords = ((BigDecimal)ret.get(0)).longValue();

			// Here to get ride of startIndex and recordsReturned for getting all records
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
	
*/
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
    public List getOperationMgrDocuments(String startIndex,String recordsReturned,String sort,String dir,String documentName,String transId,String domainName,String[] dataFlowName,String startDate,String endDate,String[] adminDomains,String[] conditionNameArr,String[] conditionSignArr,String[] conditionValueArr,String[] conditionStyleArr){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = "";
		String sqlCount = "";
		String tableName = "";
		String condition = "";
		String sortDir = "";
		try {
			sql = "select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,a.STATUS_CD,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') SUBMIT_DTTM ";
			sqlCount = "select count(*) ";
			// different parameters need to join different tables
			if(conditionNameArr !=null && conditionSignArr!=null && conditionValueArr!=null && conditionStyleArr!=null
			&& conditionNameArr.length>0 && conditionSignArr.length>0 && conditionStyleArr.length>0){
				tableName = "from NODE_FILE_CABIN a left outer join NODE_OPERATION b on a.DATAFLOW_NAME = b.OPERATION_NAME " +
				"left outer join NODE_DOMAIN c on b.DOMAIN_ID = c.DOMAIN_ID " +
				"inner join NODE_OPERATION_LOG d on d.TRANS_ID = a.TRANS_ID " +
				"inner join NODE_OPERATION_LOG_PARAMETER e on e.OPERATION_LOG_ID = d.OPERATION_LOG_ID, ";				
			}else{
				tableName = "from NODE_FILE_CABIN a left outer join NODE_OPERATION b on upper(a.DATAFLOW_NAME) = upper(b.OPERATION_NAME) " +
				"left outer join NODE_DOMAIN c on b.DOMAIN_ID = c.DOMAIN_ID " ;				
			}
			condition = " where 1=1 ";

		    if (adminDomains != null && adminDomains.length > 0) {
		    	condition += " and c.DOMAIN_NAME in (";
		        for (int i = 0; i < adminDomains.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+adminDomains[i]+"'";
		        }
		        condition += ")";
		    }

			if(conditionNameArr !=null && conditionSignArr!=null && conditionValueArr!=null && conditionStyleArr!=null
					&& conditionNameArr.length>0 && conditionSignArr.length>0 && conditionStyleArr.length>0){
				if(conditionNameArr != null && conditionNameArr.length>0){
					String prefix = conditionNameArr[0];
					prefix += "_Result";
					if(sort.equalsIgnoreCase("fileId"))
						sortDir = " order by " + prefix + ".FILE_ID " + dir;
					else if(sort.equalsIgnoreCase("fileName"))
						sortDir = " order by " + prefix + ".FILE_NAME " + dir;
					else if(sort.equalsIgnoreCase("fileType"))
						sortDir = " order by " + prefix + ".FILE_TYPE " + dir;
					else if(sort.equalsIgnoreCase("fileSize"))
						sortDir = " order by " + prefix + ".FILE_SIZE " + dir;
					else if(sort.equalsIgnoreCase("transId"))
						sortDir = " order by " + prefix + ".TRANS_ID " + dir;
					else if(sort.equalsIgnoreCase("documentStatus"))
						sortDir = " order by " + prefix + ".STATUS_CD " + dir;
					else if(sort.equalsIgnoreCase("dataFlowName"))
						sortDir = " order by " + prefix + ".DATAFLOW_NAME " + dir;
					else if(sort.equalsIgnoreCase("updatedDate"))
						sortDir = " order by " + prefix + ".SUBMIT_DTTM " + dir;			
					else if(sort.equalsIgnoreCase("domainName"))
						sortDir = " order by " + prefix + ".DOMAIN_NAME  " + dir;
					else if(!sort.equalsIgnoreCase("submitStatus") && !sort.equalsIgnoreCase("submitStatusReport")){ 
						for(int i=0;i<conditionNameArr.length;i++){
							if(conditionNameArr[i].equalsIgnoreCase(sort)){
								sortDir = " order by " + conditionNameArr[i] + "_Result." + sort + " " + dir;				
								break;
							}
						}
					}					
				}				
			}else{
				if(sort.equalsIgnoreCase("fileId"))
					sortDir = " order by " + "a.FILE_ID " + dir;
				else if(sort.equalsIgnoreCase("fileName"))
					sortDir = " order by " + "a.FILE_NAME " + dir;
				else if(sort.equalsIgnoreCase("fileType"))
					sortDir = " order by " + "a.FILE_TYPE " + dir;
				else if(sort.equalsIgnoreCase("fileSize"))
					sortDir = " order by " + "a.FILE_SIZE " + dir;
				else if(sort.equalsIgnoreCase("transId"))
					sortDir = " order by " + "a.TRANS_ID " + dir;
				else if(sort.equalsIgnoreCase("documentStatus"))
					sortDir = " order by " + "a.STATUS_CD " + dir;
				else if(sort.equalsIgnoreCase("dataFlowName"))
					sortDir = " order by " + "a.DATAFLOW_NAME " + dir;
				else if(sort.equalsIgnoreCase("updatedDate"))
					sortDir = " order by " + "a.SUBMIT_DTTM " + dir;			
				else if(sort.equalsIgnoreCase("domainName"))
					sortDir = " order by " + "c.DOMAIN_NAME  " + dir;				
			}
			
						
			if (documentName != null && !documentName.equals(""))
				condition += " and a.FILE_NAME like '"+"%"+documentName+"%'";
			if (transId != null && !transId.equals(""))
				condition += " and a.TRANS_ID like '"+"%"+transId+"%'";
			if (domainName != null && !domainName.equals(""))
				condition += " and c.DOMAIN_NAME like '"+"%"+domainName+"%'";
			if (dataFlowName != null && dataFlowName.length > 0){
			    if (dataFlowName != null && dataFlowName.length > 0) {
			    	condition += " and a.DATAFLOW_NAME in (";
			        for (int i = 0; i < dataFlowName.length; i++) {
			          if (i != 0)
			        	  condition += ",";
			          condition += "'"+dataFlowName[i]+"'";
			        }
			        condition += ")";
			    }
			}
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.SUBMIT_DTTM > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.SUBMIT_DTTM < :endDate)";				

			// add extra parameters and create combine sql
			if(conditionNameArr !=null && conditionSignArr!=null && conditionValueArr!=null && conditionStyleArr!=null
					&& conditionNameArr.length>0 && conditionSignArr.length>0 && conditionStyleArr.length>0){
				sql = this.createSql(tableName, condition, sortDir, conditionNameArr, conditionSignArr, conditionValueArr, conditionStyleArr, "sql");
				sqlCount = this.createSql(tableName, condition, sortDir, conditionNameArr, conditionSignArr, conditionValueArr, conditionStyleArr, "count");
			}else{
				sql+=tableName+condition+sortDir;
				sqlCount+=tableName+condition;				
			}
			
			// Add filter condition from NODE_OPERATION_MANAGER table
			if(sort.equalsIgnoreCase("submitStatus")){ 
				sql = "select g.*, h.STATUS_CD SUBMIT_CD, h.SUBMIT_ID from (" + sql + ") g left outer join (select STATUS_CD,SUBMIT_ID,TRANS_ID from NODE_OPERATION_MANAGER where SUBMIT_ID in (select  MAX(SUBMIT_ID) SUBMIT_ID  from NODE_OPERATION_MANAGER group by TRANS_ID )) h ON g.TRANS_ID = h.TRANS_ID order by h.STATUS_CD " + dir;
			}else if(sort.equalsIgnoreCase("submitStatusReport")){
				sql = "select g.*, h.STATUS_CD SUBMIT_CD, h.SUBMIT_ID from (" + sql + ") g left outer join (select STATUS_CD,SUBMIT_ID,TRANS_ID from NODE_OPERATION_MANAGER where SUBMIT_ID in (select  MAX(SUBMIT_ID) SUBMIT_ID  from NODE_OPERATION_MANAGER group by TRANS_ID )) h ON g.TRANS_ID = h.TRANS_ID order by h.STATUS_CD " + dir;				
			}else{
				sortDir = sortDir.substring(0,sortDir.indexOf("by")+3) + sortDir.substring(sortDir.indexOf('.')+1);
				sql = "select g.*, h.STATUS_CD SUBMIT_CD, h.SUBMIT_ID from (" + sql + ") g left outer join (select STATUS_CD,SUBMIT_ID,TRANS_ID from NODE_OPERATION_MANAGER where SUBMIT_ID in (select  MAX(SUBMIT_ID) SUBMIT_ID  from NODE_OPERATION_MANAGER group by TRANS_ID )) h ON g.TRANS_ID = h.TRANS_ID " + sortDir;								
			}
			tx = session.beginTransaction();			
						
			SQLQuery query = session.createSQLQuery(sql);
			SQLQuery queryCount = session.createSQLQuery(sqlCount);
			
			if (startDate != null && !startDate.equals("")){
				Date startDt = Utility.ChangeStringToDate(startDate);
				query.setDate("startDate", startDt);
				queryCount.setDate("startDate", startDt);
			}
			if (endDate != null && !endDate.equals("")){
				Date endDt = Utility.ChangeStringToDate(endDate);
		        Calendar cal = Calendar.getInstance(TimeZone.getDefault());
		        cal.setTime(endDt);
		        cal.add(Calendar.DAY_OF_MONTH,1);
		        endDt = cal.getTime();
				query.setDate("endDate", endDt);
				queryCount.setDate("endDate", endDt);			
			}

			ret = queryCount.list();
			this.totalRecords = ((BigDecimal)ret.get(0)).longValue();

			// Here to get ride of startIndex and recordsReturned for getting all records
			query.setFirstResult(Integer.parseInt(startIndex));
			query.setMaxResults(Integer.parseInt(recordsReturned));
			ret = query.list();
			ArrayList newRet = new ArrayList();
			// delete the hibernate additional rownum column when startIndex > 0
			if(Integer.parseInt(startIndex) > 0){
				Iterator it = ret.iterator();
				while(it.hasNext()){
					Object[] obArray = (Object[])it.next();
					Object[] newObArray = new Object[obArray.length-1];
					for(int i=0;i<obArray.length-1;i++){
						newObArray[i] = obArray[i];
					}
					newRet.add(newObArray);
				}
				ret = null;
				ret = newRet;
			}
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
	   * getDocument
	   * @param documentId
	   * @return Document
	   */
    public Document getDocument(Long documentId) {
		return (Document) getHibernateTemplate().get(Document.class, documentId);
	}

	  /**
	   * saveDocument
	   * @param document
	   * @return 
	   */
	public void saveDocument(Document document) {
		getHibernateTemplate().saveOrUpdate(document);

		if (log.isDebugEnabled()) {
			log.debug("Document id set to: " + document.getFileId());
		}
	}

	  /**
	   * removeDocument
	   * @param documentId
	   * @return boolean
	   */
	public boolean removeDocument(Long documentId) {
		Object document = getHibernateTemplate().load(Document.class, documentId);
		getHibernateTemplate().delete(document);
		return true;
	}

	  /**
	   * removeDocuments
	   * @param documentIdJson
	   * @return boolean
	   */
	public boolean removeDocuments(String documentIdJson) {
		JSONObject jsonObject = JSONObject.fromObject( documentIdJson );   
		List documentIdList = (List)JSONArray.toCollection(jsonObject.getJSONArray("documentList" ));
		Iterator it = documentIdList.iterator();
		
		while(it.hasNext()){
			//System.out.println((String)it.next());
			this.removeDocument(Long.valueOf((String)it.next()));
		}
		return true;
	}
	
	  /**
	   * getTotalRecords
	   * @param 
	   * @return long
	   */
	public long getTotalRecords() {
		return this.totalRecords;
	}

	  /**
	   * createSql
	   * @param tableName
	   * @param condition
	   * @param sortDir
	   * @param conditionNameArr
	   * @param conditionSignArr
	   * @param conditionValueArr
	   * @param conditionStyleArr
	   * @param indicate
	   * @return String
	   */
	private String createSql(String tableName,String condition,String sortDir, String[] conditionNameArr,String[] conditionSignArr,String[] conditionValueArr,String[] conditionStyleArr, String indicate) {
		String ret = "select ";
		String subSelect1 = "";
		String[] subSelect2 = new String[conditionNameArr.length];
		String[] subSelect3 = new String[conditionNameArr.length];
		String subCondition = "";
		List subConditionList = new ArrayList();
		
		// create sub sql result
		for(int i=0;i<conditionNameArr.length;i++){
			subSelect3[i] = "(select distinct PARAMETER_NAME,PARAMETER_VALUE from NODE_OPERATION_LOG_PARAMETER where PARAMETER_NAME='"+conditionNameArr[i]+"') f";
			if(conditionStyleArr[i].equalsIgnoreCase("string") || conditionStyleArr[i].equalsIgnoreCase("date")){
				if(conditionSignArr[i] != null && !conditionSignArr[i].equals("") && conditionSignArr[i].equalsIgnoreCase("=")){
					subSelect2[i] = "(select distinct * from  (select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,a.STATUS_CD,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') SUBMIT_DTTM, e.PARAMETER_VALUE " + conditionNameArr[i] + " "
					+ tableName
					+ subSelect3[i]
					+ condition
					+ " and f.PARAMETER_NAME = e.PARAMETER_NAME  and e.PARAMETER_VALUE = '" + conditionValueArr[i] + "' "
					+ "))" + conditionNameArr[i] + "_Result";					
				}else if(conditionSignArr[i] != null && !conditionSignArr[i].equals("") && conditionSignArr[i].equalsIgnoreCase("like")){
					subSelect2[i] = "(select distinct * from  (select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,a.STATUS_CD,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') SUBMIT_DTTM, e.PARAMETER_VALUE " + conditionNameArr[i] + " "
					+ tableName
					+ subSelect3[i]
					+ condition
					+ " and f.PARAMETER_NAME = e.PARAMETER_NAME  and e.PARAMETER_VALUE like '" + "%" + conditionValueArr[i] + "%' "
					+ "))" + conditionNameArr[i] + "_Result";					
				}else{
					subSelect2[i] = "(select distinct * from  (select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,a.STATUS_CD,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') SUBMIT_DTTM, e.PARAMETER_VALUE " + conditionNameArr[i] + " "
					+ tableName
					+ subSelect3[i]
					+ condition
					+ " and f.PARAMETER_NAME = e.PARAMETER_NAME "
					+ "))" + conditionNameArr[i] + "_Result";										
				}
			}else if(conditionStyleArr[i].equalsIgnoreCase("number")){
				if(conditionSignArr[i] != null && !conditionSignArr[i].equals("") && !conditionSignArr[i].equalsIgnoreCase("all")){
					subSelect2[i] = "(select distinct * from  (select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,a.STATUS_CD,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') SUBMIT_DTTM, e.PARAMETER_VALUE " + conditionNameArr[i] + " "
					+ tableName
					+ subSelect3[i]
					+ condition
					+ " and f.PARAMETER_NAME = e.PARAMETER_NAME  and e.PARAMETER_VALUE = " + conditionValueArr[i]
					+ "))" + conditionNameArr[i] + "_Result";					
				}else{
					subSelect2[i] = "(select distinct * from  (select a.FILE_ID,a.FILE_NAME,a.FILE_TYPE,a.FILE_SIZE,a.TRANS_ID,a.STATUS_CD,c.DOMAIN_NAME,a.DATAFLOW_NAME,to_char(a.SUBMIT_DTTM, 'MM/DD/YYYY HH24:MI:SS') SUBMIT_DTTM, e.PARAMETER_VALUE " + conditionNameArr[i] + " "
					+ tableName
					+ subSelect3[i]
					+ condition
					+ " and f.PARAMETER_NAME = e.PARAMETER_NAME "
					+ "))" + conditionNameArr[i] + "_Result";										
				}
			}
		}
		
		// Create sql
		if(indicate.equalsIgnoreCase("sql")){
			for(int i=0;i<conditionNameArr.length;i++){
				if(conditionNameArr.length == 1){	// only one parameter
					ret += conditionNameArr[i] + "_Result" + ".*";
					subSelect1 += subSelect2[i];
				}else if(conditionNameArr.length > 1 && i==0){ // first parameter
					ret += conditionNameArr[i] + "_Result" + ".*";
					subSelect1 += subSelect2[i];
				}else{// middle parameters
					ret += "," + conditionNameArr[i] + "_Result" + "." + conditionNameArr[i];
					subSelect1 += " left outer join " + subSelect2[i] + " on " + conditionNameArr[i-1] + "_Result" + ".TRANS_ID=" +  conditionNameArr[i] + "_Result" + ".TRANS_ID";
				}
			}
		}	
/*		if(indicate.equalsIgnoreCase("sql")){
			for(int i=0;i<conditionNameArr.length;i++){
				if(conditionNameArr.length == 1){	// only one parameter
					ret += conditionNameArr[i] + "_Result" + ".*";
					subSelect1 += subSelect2[i];
				}else if(conditionNameArr.length > 1 && i==0){ // first parameter
					ret += conditionNameArr[i] + "_Result" + "." + conditionNameArr[i];
					subSelect1 += subSelect2[i];
				}else if(conditionNameArr.length > 1 && i==conditionNameArr.length-1){ // last parameter
					ret += "," + conditionNameArr[i] + "_Result" + ".*";
					subSelect1 += "," + subSelect2[i];
				}else{// middle parameters
					ret += "," + conditionNameArr[i] + "_Result" + "." + conditionNameArr[i];
					subSelect1 += "," + subSelect2[i];
				}
			}
		}	
		if(indicate.equalsIgnoreCase("sql")){
			for(int i = conditionNameArr.length-1; i >= 0; i--){
				if(conditionNameArr.length == 1){	// only one parameter
					ret += conditionNameArr[i] + "_Result" + ".*";
					subSelect1 += subSelect2[i];
				}else if(conditionNameArr.length > 1 && i==conditionNameArr.length-1){ // first parameter
					ret += conditionNameArr[i] + "_Result" + ".*";
					subSelect1 += subSelect2[i];
				}else{	// middle parameters
					ret += "," + conditionNameArr[i] + "_Result" + "." + conditionNameArr[i];
					subSelect1 += "," + subSelect2[i];
				}
			}
		}*/else{
			ret += " count(*) ";
			for(int i=0;i<conditionNameArr.length;i++){
				if(i==0){
					subSelect1 += subSelect2[i];
				}else{
					subSelect1 += " left outer join " + subSelect2[i] + " on " + conditionNameArr[i-1] + "_Result" + ".TRANS_ID=" +  conditionNameArr[i] + "_Result" + ".TRANS_ID";
				}
			}
		}
		
		// create condition List
//		for(int i=0;i<conditionNameArr.length;i++){
//			if(conditionNameArr.length > 1 && i % 2 == 0){
//				subCondition = " where " + conditionNameArr[i] + "_Result" + ".TRANS_ID";
//			}else if(conditionNameArr.length > 1 && i % 2 != 0){
//				subCondition += "=" + conditionNameArr[i] + "_Result" + ".TRANS_ID";
//				subConditionList.add(subCondition);
//				subCondition = "";
//			}
//		}
		

		for(int i=0;i<conditionNameArr.length;i++){
			if(conditionStyleArr[i].equalsIgnoreCase("string") && !conditionSignArr[i].equalsIgnoreCase("all")){
				subCondition = " and " + conditionNameArr[i] + "_Result."+ conditionNameArr[i] + "='" + conditionValueArr[i]+"'" ;
			}else if(conditionStyleArr[i].equalsIgnoreCase("number") && !conditionSignArr[i].equalsIgnoreCase("all")){
				subCondition = " and " + conditionNameArr[i] + "_Result."+ conditionNameArr[i] + "=" + conditionValueArr[i] ;
			}
			subConditionList.add(subCondition);
		}

		
		// create condition from list
//		subCondition = "";
		subCondition = " where 1=1 ";
		for(int i=0;i<subConditionList.size();i++){
			subCondition += (String)subConditionList.get(i);							
		}
		
		if(indicate.equalsIgnoreCase("sql")){
			ret += " from " + subSelect1 + subCondition + sortDir;						
		}else{
			ret += " from " + subSelect1 + subCondition;			
		}
		
		return ret;
	}

}
