package Node2.dao.hibernate.Domains;

import java.math.BigDecimal;
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

import com.enfotech.basecomponent.jndi.JNDIAccess;

import Node.Phrase;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeWebService;
import Node.Utils.Utility;
import Node2.dao.Domains.OperationLogDAO;
import Node2.model.Domains.OperationLog;

/**
 * <p>This class interacts with Spring and Hibernate to save and retrieve Operation
 * objects.</p>
 * 
 * @author Enfotech
 */
public class OperationLogDAOHibernate extends HibernateDaoSupport implements
		OperationLogDAO {
	private static Log log = LogFactory.getLog(OperationLogDAOHibernate.class);
	private long totalOperationLogRecords = 0;
	private long totalScheduledTasksLogRecords = 0;
	private long totalNotificationLogRecords = 0;
	
	  /**
	   * getOperations
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
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
	   * @param domainPermissions
	   * @param version
	   * @return List
	   */
    public List getOperationLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transId,String startDate,String endDate,String[] domainPermissions, String version){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;

		try {
			sql = "SELECT A.OPERATION_LOG_ID,A.OPERATION_NAME,A.OPERATION_TYPE,A.WEB_SERVICE_NAME,A.DOMAIN_NAME,A.USER_NAME,A.TOKEN,A.TRANS_ID,A.START_DTTM,A.END_DTTM, (SELECT STATUS_CD FROM NODE_OPERATION_LOG_STATUS WHERE OPERATION_LOG_STATUS_ID = A.OPERATION_LOG_STATUS_ID) as STATUS_CD "
				+	"FROM ("
				+	"select distinct a.OPERATION_LOG_ID as OPERATION_LOG_ID, b.OPERATION_NAME as OPERATION_NAME, b.OPERATION_TYPE as OPERATION_TYPE, c.WEB_SERVICE_NAME as WEB_SERVICE_NAME, d.DOMAIN_NAME as DOMAIN_NAME, a.USER_NAME as USER_NAME, a.TOKEN as TOKEN, a.TRANS_ID as TRANS_ID, CAST(a.START_DTTM AS TIMESTAMP) as START_DTTM, CAST(a.END_DTTM AS TIMESTAMP) as END_DTTM, max(e.OPERATION_LOG_STATUS_ID) as OPERATION_LOG_STATUS_ID ";

			sqlCount = "SELECT count(*) "
				+	"FROM ("
				+	"select distinct a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM, max(e.OPERATION_LOG_STATUS_ID) as OPERATION_LOG_STATUS_ID ";

			//tableName = " from (select * from NODE_OPERATION_LOG where rownum <=" + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)) + " ORDER BY OPERATION_LOG_ID DESC) a "
			// initial just get fix record
			if((opName == null || opName.equals(""))&& (opType == null || opType.equals("")) && (opWebservice == null || opWebservice.equals("")) && (opStatus == null || opStatus.equals("")) && (opDomain == null || opDomain.equals("")) && (userId == null || userId.equals("")) && (token == null || token.equals("")) && (transId == null || transId.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				tableName = " from (select  aa.* from (SELECT row_number() over (order by f.OPERATION_LOG_ID DESC) as id, f.* FROM NODE_OPERATION_LOG f LEFT OUTER JOIN NODE_OPERATION g ON f.OPERATION_ID = g.OPERATION_ID WHERE g.VERSION_NO = '" + version + "' ) aa where aa.id <= " + recordsReturned + ") a ";
			}else{
				tableName = " from NODE_OPERATION_LOG a ";
			}
			tableName += 	"left outer join NODE_OPERATION b on a.OPERATION_ID=b.OPERATION_ID "
						+	"left outer join NODE_WEB_SERVICE c on b.WEB_SERVICE_ID=c.WEB_SERVICE_ID "
						+	"left outer join NODE_DOMAIN d on b.DOMAIN_ID=d.DOMAIN_ID "
						+	"left outer join NODE_OPERATION_LOG_STATUS e on a.OPERATION_LOG_ID=e.OPERATION_LOG_ID ";

			condition = "where  b.VERSION_NO = '" + version + "'";
			
		    if (domainPermissions != null && domainPermissions.length > 0) {
		    	condition = condition + " and d.DOMAIN_NAME in (";
		        for (int i = 0; i < domainPermissions.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+domainPermissions[i]+"'";
		        }
		        condition += ")";
		    }

		    if(sort.equalsIgnoreCase("operationLogId")) sortDir = " order by " + "OPERATION_LOG_ID" + " " + dir;
			else if(sort.equalsIgnoreCase("operationName")) sortDir = " order by " + "OPERATION_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("operationType")) sortDir = " order by " + "OPERATION_TYPE" + " " + dir;
			else if(sort.equalsIgnoreCase("webServiceName")) sortDir = " order by " + "WEB_SERVICE_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("domainName")) sortDir = " order by " + "DOMAIN_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("userName")) sortDir = " order by " + "USER_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("token")) sortDir = " order by " + "TOKEN" + " " + dir;
			else if(sort.equalsIgnoreCase("transId")) sortDir = " order by " + "TRANS_ID" + " " + dir;
			else if(sort.equalsIgnoreCase("startDate")) sortDir = " order by " + "START_DTTM" + " " + dir;
			else if(sort.equalsIgnoreCase("endDate")) sortDir = " order by " + "END_DTTM" + " " + dir;
			else if(sort.equalsIgnoreCase("operationLogStatusCD")) sortDir = " order by " + "STATUS_CD" + " " + dir;
			
			
			if (opName != null && !opName.equals(""))
				condition += " and b.OPERATION_NAME = :opName";
			if (opType != null && !opType.equals(""))
				condition += " and b.OPERATION_TYPE = :opType";
			if (opWebservice != null && !opWebservice.equals(""))
				condition += " and c.WEB_SERVICE_NAME = :opWebservice";
			if (opDomain != null && !opDomain.equals("")) 
				condition += " and d.DOMAIN_NAME = :opDomain";
			if (userId != null && !userId.equals(""))
				condition += " and a.USER_NAME = :userId";
			if (token != null && !token.equals(""))
				condition += " and a.TOKEN = :token";
			if (transId != null && !transId.equals(""))
				condition += " and a.TRANS_ID = :transId";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.START_DTTM > :startDate or a.END_DTTM > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.START_DTTM < :endDate or a.END_DTTM < :endDate)";				
			if (opStatus != null && !opStatus.equals(""))
				condition += " and e.STATUS_CD = :opStatus";

			tx = session.beginTransaction();
			
			//condition += (" and rownum <= " + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)));
			if(sort.equalsIgnoreCase("operationLogStatusCD")) sql+=tableName+condition+" group by a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM order by a.OPERATION_LOG_ID DESC) A " + sortDir;
			else sql+=tableName+condition+" group by a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM " + sortDir +") A";
			sqlCount+=tableName+condition+" group by a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM) A";

			Query query = session.createSQLQuery(sql);

			
			Query queryCount = session.createSQLQuery(sqlCount);
					
			if (opName != null && !opName.equals("")){
				query.setString("opName", opName);
				queryCount.setString("opName", opName);
			}
			if (opType != null && !opType.equals("")){
				query.setString("opType", opType);
				queryCount.setString("opType", opType);
			}
			if (opWebservice != null && !opWebservice.equals("")){
				query.setString("opWebservice", opWebservice);
				queryCount.setString("opWebservice", opWebservice);			
			}
			if (opDomain != null  && !opDomain.equals("")){
				query.setString("opDomain", opDomain);
				queryCount.setString("opDomain", opDomain);
			}
			if (userId != null && !userId.equals("")){
				query.setString("userId", userId);
				queryCount.setString("userId", userId);
			}
			if (token != null && !token.equals("")){
				query.setString("token", token);
				queryCount.setString("token", token);
			}
			if (transId != null && !transId.equals("")){
				query.setString("transId", transId);
				queryCount.setString("transId", transId);
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
			if (opStatus != null && !opStatus.equals("")){
				query.setString("opStatus", opStatus);
				queryCount.setString("opStatus", opStatus);
			}
			
			// initial just get fix record
			if((opName == null || opName.equals(""))&& (opType == null || opType.equals("")) && (opWebservice == null || opWebservice.equals("")) && (opStatus == null || opStatus.equals("")) && (opDomain == null || opDomain.equals("")) && (userId == null || userId.equals("")) && (token == null || token.equals("")) && (transId == null || transId.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				this.totalOperationLogRecords = Integer.parseInt(recordsReturned);			
			}else{
				ret = queryCount.list();
				this.totalOperationLogRecords = ((BigDecimal)ret.get(0)).longValue();				
			}
			
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


	
		
	/*    public List getOperationLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transId,String startDate,String endDate,String[] domainPermissions, String version){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;

		try {
			sql = "select a.operationLogId,b.operationName,b.operationType,c.webServiceName,d.domainName,a.userName,a.token,a.transId,a.startDate,a.endDate,e.operationLogStatusCD ";
			sqlCount = "select count(*) ";
			tableName = "from OperationLog a, Operation b left outer join b.webservice c,Domain d,OperationLogStatus e ";
			condition = "where a.operation.operationId = b.operationId " +
					"and b.domain.domainId = d.domainId " +
					//"and b.webservice.webServiceId = c.webServiceId " +
					"and e.operationLog.operationLogId = a.operationLogId " +
					"and e.operationLogStatusId = (select MAX(f.operationLogStatusId) " +
					"from OperationLogStatus f where a.operationLogId = f.operationLog.operationLogId) " +
					"and b.versionNo = '" + version + "'";
			
		    if (domainPermissions != null && domainPermissions.length > 0) {
		    	condition = condition + " and d.domainName in (";
		        for (int i = 0; i < domainPermissions.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+domainPermissions[i]+"'";
		        }
		        condition += ")";
		    }

		    if(sort.equalsIgnoreCase("operationLogId")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("operationName")) sortDir = " order by " + "b."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("operationType")) sortDir = " order by " + "b."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("webServiceName")) sortDir = " order by " + "c."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("domainName")) sortDir = " order by " + "d."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("userName")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("token")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("transId")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("startDate")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("endDate")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("operationLogStatusCD")) sortDir = " order by " + "e."+sort + " " + dir;
			
			
			if (opName != null && !opName.equals(""))
				condition += " and b.operationName like :opName";
			if (opType != null && !opType.equals(""))
				condition += " and b.operationType = :opType";
			if (opWebservice != null && !opWebservice.equals(""))
				condition += " and c.webServiceName = :opWebservice";
			if (opDomain != null && !opDomain.equals("")) 
				condition += " and d.domainName = :opDomain";
			if (userId != null && !userId.equals(""))
				condition += " and a.userName = :userId";
			if (token != null && !token.equals(""))
				condition += " and a.token = :token";
			if (transId != null && !transId.equals(""))
				condition += " and a.transId = :transId";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.startDate > :startDate or a.endDate > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.startDate < :endDate or a.endDate < :endDate)";				
			if (opStatus != null && !opStatus.equals(""))
				condition += " and e.operationLogStatusCD = :opStatus";

			tx = session.beginTransaction();
			
			sql+=tableName+condition+sortDir;
			sqlCount+=tableName+condition;
			Query query = session.createQuery(sql);
			Query queryCount = session.createQuery(sqlCount);
			
			if (opName != null && !opName.equals("")){
				query.setString("opName", opName+"%");
				queryCount.setString("opName", opName+"%");
			}
			if (opType != null && !opType.equals("")){
				query.setString("opType", opType);
				queryCount.setString("opType", opType);
			}
			if (opWebservice != null && !opWebservice.equals("")){
				query.setString("opWebservice", opWebservice);
				queryCount.setString("opWebservice", opWebservice);			
			}
			if (opDomain != null  && !opDomain.equals("")){
				query.setString("opDomain", opDomain);
				queryCount.setString("opDomain", opDomain);
			}
			if (userId != null && !userId.equals("")){
				query.setString("userId", userId);
				queryCount.setString("userId", userId);
			}
			if (token != null && !token.equals("")){
				query.setString("token", token);
				queryCount.setString("token", token);
			}
			if (transId != null && !transId.equals("")){
				query.setString("transId", transId);
				queryCount.setString("transId", transId);
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
			if (opStatus != null && !opStatus.equals("")){
				query.setString("opStatus", opStatus);
				queryCount.setString("opStatus", opStatus);
			}

			ret = queryCount.list();
			this.totalOperationLogRecords = ((Long)ret.get(0)).longValue();

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
	   * getOperations
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param opName
	   * @param opType
	   * @param opWebservice
	   * @param opStatus
	   * @param opDomain
	   * @param userId
	   * @param token
	   * @param transId
	   * @param startDate
	   * @param endDate
	   * @param domainPermissions
	   * @param version
	   * @return List
	   */
    public List getScheduledTasksLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transId,String startDate,String endDate, String[] domainPermissions, String version){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;

		try {
			sql = "SELECT A.OPERATION_LOG_ID,A.OPERATION_NAME,A.OPERATION_TYPE,A.WEB_SERVICE_NAME,A.DOMAIN_NAME,A.USER_NAME,A.TOKEN,A.TRANS_ID,A.START_DTTM,A.END_DTTM, (SELECT STATUS_CD FROM NODE_OPERATION_LOG_STATUS WHERE OPERATION_LOG_STATUS_ID = A.OPERATION_LOG_STATUS_ID) as STATUS_CD "
				+	"FROM ("
				+	"select distinct a.OPERATION_LOG_ID as OPERATION_LOG_ID, b.OPERATION_NAME as OPERATION_NAME, b.OPERATION_TYPE as OPERATION_TYPE, c.WEB_SERVICE_NAME as WEB_SERVICE_NAME, d.DOMAIN_NAME as DOMAIN_NAME, a.USER_NAME as USER_NAME, a.TOKEN as TOKEN, a.TRANS_ID as TRANS_ID, CAST(a.START_DTTM AS TIMESTAMP) as START_DTTM, CAST(a.END_DTTM AS TIMESTAMP) as END_DTTM, max(e.OPERATION_LOG_STATUS_ID) as OPERATION_LOG_STATUS_ID ";

			sqlCount = "SELECT count(*) "
				+	"FROM ("
				+	"select distinct a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM, max(e.OPERATION_LOG_STATUS_ID) as OPERATION_LOG_STATUS_ID ";

			//tableName = " from (select * from NODE_OPERATION_LOG where OPERATION_LOG_ID > (select max(OPERATION_LOG_ID) from NODE_OPERATION_LOG) - " + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)) + " ) a "
			// initial just get fix record
			if((opName == null || opName.equals("")) && (opWebservice == null || opWebservice.equals("")) && (opStatus == null || opStatus.equals("")) && (opDomain == null || opDomain.equals("")) && (userId == null || userId.equals("")) && (token == null || token.equals("")) && (transId == null || transId.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				//tableName = " from (SELECT a.* FROM NODE_OPERATION_LOG a LEFT OUTER JOIN NODE_OPERATION b ON a.OPERATION_ID=b.OPERATION_ID where b.operation_type = '" + opType + "'and b.VERSION_NO = '" + version + "' and a.operation_log_id > ( SELECT MAX(OPERATION_LOG_ID)  FROM NODE_OPERATION_LOG a LEFT OUTER JOIN NODE_OPERATION b ON a.OPERATION_ID=b.OPERATION_ID where b.operation_type = '" + opType + "' and b.VERSION_NO = '" + version + "') - " + Integer.parseInt(recordsReturned) + " ) a ";
				tableName = " from (select * from (SELECT row_number() over (order by a.OPERATION_LOG_ID DESC) as id, a.* FROM NODE_OPERATION_LOG a LEFT OUTER JOIN NODE_OPERATION b ON a.OPERATION_ID=b.OPERATION_ID where b.operation_type = '" + opType + "' and b.VERSION_NO = '" + version + "') aa where aa.id <= " + recordsReturned +") a ";
			}else{
				tableName = " from NODE_OPERATION_LOG a ";
			}
			//tableName = " from NODE_OPERATION_LOG a ";
			tableName +=	"left outer join NODE_OPERATION b on a.OPERATION_ID=b.OPERATION_ID "
			+	"left outer join NODE_WEB_SERVICE c on b.WEB_SERVICE_ID=c.WEB_SERVICE_ID "
			+	"left outer join NODE_DOMAIN d on b.DOMAIN_ID=d.DOMAIN_ID "
			+	"left outer join NODE_OPERATION_LOG_STATUS e on a.OPERATION_LOG_ID=e.OPERATION_LOG_ID ";

			condition = "where  b.VERSION_NO = '" + version + "'";
			
		    if (domainPermissions != null && domainPermissions.length > 0) {
		    	condition = condition + " and d.DOMAIN_NAME in (";
		        for (int i = 0; i < domainPermissions.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+domainPermissions[i]+"'";
		        }
		        condition += ")";
		    }

		    if(sort.equalsIgnoreCase("operationLogId")) sortDir = " order by " + "OPERATION_LOG_ID" + " " + dir;
			else if(sort.equalsIgnoreCase("operationName")) sortDir = " order by " + "OPERATION_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("operationType")) sortDir = " order by " + "OPERATION_TYPE" + " " + dir;
			else if(sort.equalsIgnoreCase("webServiceName")) sortDir = " order by " + "WEB_SERVICE_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("domainName")) sortDir = " order by " + "DOMAIN_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("userName")) sortDir = " order by " + "USER_NAME" + " " + dir;
			else if(sort.equalsIgnoreCase("token")) sortDir = " order by " + "TOKEN" + " " + dir;
			else if(sort.equalsIgnoreCase("transId")) sortDir = " order by " + "TRANS_ID" + " " + dir;
			else if(sort.equalsIgnoreCase("startDate")) sortDir = " order by " + "START_DTTM" + " " + dir;
			else if(sort.equalsIgnoreCase("endDate")) sortDir = " order by " + "END_DTTM" + " " + dir;
			else if(sort.equalsIgnoreCase("operationLogStatusCD")) sortDir = " order by " + "STATUS_CD" + " " + dir;
			
			
			if (opName != null && !opName.equals(""))
				condition += " and b.OPERATION_NAME = :opName";
			if (opType != null && !opType.equals(""))
				condition += " and b.OPERATION_TYPE = :opType";
			if (opWebservice != null && !opWebservice.equals(""))
				condition += " and c.WEB_SERVICE_NAME = :opWebservice";
			if (opDomain != null && !opDomain.equals("")) 
				condition += " and d.DOMAIN_NAME = :opDomain";
			if (userId != null && !userId.equals(""))
				condition += " and a.USER_NAME = :userId";
			if (token != null && !token.equals(""))
				condition += " and a.TOKEN = :token";
			if (transId != null && !transId.equals(""))
				condition += " and a.TRANS_ID = :transId";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.START_DTTM > :startDate or a.END_DTTM > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.START_DTTM < :endDate or a.END_DTTM < :endDate)";				
			if (opStatus != null && !opStatus.equals(""))
				condition += " and e.STATUS_CD = :opStatus";

			tx = session.beginTransaction();
			
			//condition += (" and rownum <= " + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)));
			if(sort.equalsIgnoreCase("operationLogStatusCD")) sql+=tableName+condition+" group by a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM order by a.OPERATION_LOG_ID DESC) A " + sortDir;
			else sql+=tableName+condition+" group by a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM" + sortDir +") A";
			sqlCount+=tableName+condition+" group by a.OPERATION_LOG_ID, b.OPERATION_NAME, b.OPERATION_TYPE, c.WEB_SERVICE_NAME, d.DOMAIN_NAME, a.USER_NAME, a.TOKEN, a.TRANS_ID, a.START_DTTM, a.END_DTTM) A";

			Query query = session.createSQLQuery(sql);			
			Query queryCount = session.createSQLQuery(sqlCount);
					
			if (opName != null && !opName.equals("")){
				query.setString("opName", opName);
				queryCount.setString("opName", opName);
			}
			if (opType != null && !opType.equals("")){
				query.setString("opType", opType);
				queryCount.setString("opType", opType);
			}
			if (opWebservice != null && !opWebservice.equals("")){
				query.setString("opWebservice", opWebservice);
				queryCount.setString("opWebservice", opWebservice);			
			}
			if (opDomain != null  && !opDomain.equals("")){
				query.setString("opDomain", opDomain);
				queryCount.setString("opDomain", opDomain);
			}
			if (userId != null && !userId.equals("")){
				query.setString("userId", userId);
				queryCount.setString("userId", userId);
			}
			if (token != null && !token.equals("")){
				query.setString("token", token);
				queryCount.setString("token", token);
			}
			if (transId != null && !transId.equals("")){
				query.setString("transId", transId);
				queryCount.setString("transId", transId);
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
			if (opStatus != null && !opStatus.equals("")){
				query.setString("opStatus", opStatus);
				queryCount.setString("opStatus", opStatus);
			}
			
			// initial just get fix record
			if((opName == null || opName.equals("")) && (opWebservice == null || opWebservice.equals("")) && (opStatus == null || opStatus.equals("")) && (opDomain == null || opDomain.equals("")) && (userId == null || userId.equals("")) && (token == null || token.equals("")) && (transId == null || transId.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				this.totalScheduledTasksLogRecords = Integer.parseInt(recordsReturned);			
			}else{
				ret = queryCount.list();
				this.totalScheduledTasksLogRecords = ((BigDecimal)ret.get(0)).longValue();				
			}
			
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

    /*
    public List getScheduledTasksLogs(String startIndex,String recordsReturned,String sort,String dir,String opName,String opType,String opWebservice,String opStatus,String opDomain,String userId,String token,String transId,String startDate,String endDate, String[] domainPermissions, String version){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;

		try {
			sql = "select a.operationLogId,b.operationName,b.operationType,c.webServiceName,d.domainName,a.userName,a.token,a.transId,a.startDate,a.endDate,e.operationLogStatusCD ";
			sqlCount = "select count(*) ";
			tableName = "from OperationLog a, Operation b left outer join b.webservice c,Domain d,OperationLogStatus e ";
			condition = "where a.operation.operationId = b.operationId " +
					"and b.domain.domainId = d.domainId " +
					//"and b.webservice.webServiceId = c.webServiceId " +
					"and e.operationLog.operationLogId = a.operationLogId " +
					"and e.operationLogStatusId = (select MAX(f.operationLogStatusId) " +
					"from OperationLogStatus f where a.operationLogId = f.operationLog.operationLogId) " +
					"and b.versionNo = '" + version + "'";
			
		    if (domainPermissions != null && domainPermissions.length > 0) {
		    	condition = condition + " and d.domainName in (";
		        for (int i = 0; i < domainPermissions.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+domainPermissions[i]+"'";
		        }
		        condition += ")";
		    }

		    if(sort.equalsIgnoreCase("operationLogId")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("operationName")) sortDir = " order by " + "b."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("operationType")) sortDir = " order by " + "b."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("webServiceName")) sortDir = " order by " + "c."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("domainName")) sortDir = " order by " + "d."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("userName")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("token")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("transId")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("startDate")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("endDate")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("operationLogStatusCD")) sortDir = " order by " + "e."+sort + " " + dir;
			
			
			if (opName != null && !opName.equals(""))
				condition += " and b.operationName like :opName";
			if (opType != null && !opType.equals(""))
				condition += " and b.operationType like :opType";
			if (opWebservice != null && !opWebservice.equals(""))
				condition += " and c.webServiceName like :opWebservice";
			if (opDomain != null && !opDomain.equals("")) 
				condition += " and d.domainName like :opDomain";
			if (userId != null && !userId.equals(""))
				condition += " and a.userName like :userId";
			if (token != null && !token.equals(""))
				condition += " and a.token like :token";
			if (transId != null && !transId.equals(""))
				condition += " and a.transId like :transId";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.startDate > :startDate or a.endDate > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.startDate < :endDate or a.endDate < :endDate)";				
			if (opStatus != null && !opStatus.equals(""))
				condition += " and e.operationLogStatusCD = :opStatus";

			tx = session.beginTransaction();
			
			sql+=tableName+condition+sortDir;
			sqlCount+=tableName+condition;
			Query query = session.createQuery(sql);
			Query queryCount = session.createQuery(sqlCount);
			
			if (opName != null && !opName.equals("")){
				query.setString("opName", opName+"%");
				queryCount.setString("opName", opName+"%");
			}
			if (opType != null && !opType.equals("")){
				query.setString("opType", opType+"%");
				queryCount.setString("opType", opType+"%");
			}
			if (opWebservice != null && !opWebservice.equals("")){
				query.setString("opWebservice", opWebservice+"%");
				queryCount.setString("opWebservice", opWebservice+"%");			
			}
			if (opDomain != null  && !opDomain.equals("")){
				query.setString("opDomain", opDomain+"%");
				queryCount.setString("opDomain", opDomain+"%");
			}
			if (userId != null && !userId.equals("")){
				query.setString("userId", userId+"%");
				queryCount.setString("userId", userId+"%");
			}
			if (token != null && !token.equals("")){
				query.setString("token", token+"%");
				queryCount.setString("token", token+"%");
			}
			if (transId != null && !transId.equals("")){
				query.setString("transId", transId+"%");
				queryCount.setString("transId", transId+"%");
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
			if (opStatus != null && !opStatus.equals("")){
				query.setString("opStatus", opStatus+"%");
				queryCount.setString("opStatus", opStatus+"%");
			}

			ret = queryCount.list();
			this.totalScheduledTasksLogRecords = ((Long)ret.get(0)).longValue();

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
	   * getOperations
	   * @param startIndex
	   * @param recordsReturned
	   * @param sort
	   * @param dir
	   * @param nodeAddress
	   * @param opWebservice
	   * @param startDate
	   * @param endDate
	   * @param domainPermissions
	   * @param version
	   * @return List
	   */
    public List getNotificationLogs(String startIndex,String recordsReturned,String sort,String dir,String nodeAddress,String opWebservice,String startDate, String endDate, String[] domainPermissions, String version){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;
		int wsID = 5;

		try {
			INodeWebService wsDB = DBManager.GetNodeWebService(Phrase.WebServicesLoggerName);
			wsID = wsDB.GetWebServiceID(Phrase.WEB_METHOD_NOTIFY);
			sql = "SELECT A.OPERATION_LOG_ID,A.NODE_ADDRESS,A.START_DTTM, (SELECT STATUS_CD FROM NODE_OPERATION_LOG_STATUS WHERE OPERATION_LOG_STATUS_ID = A.OPERATION_LOG_STATUS_ID) as STATUS_CD "
				+	"FROM ("
				+	"select distinct a.OPERATION_LOG_ID as OPERATION_LOG_ID, a.NODE_ADDRESS as NODE_ADDRESS, c.WEB_SERVICE_NAME as WEB_SERVICE_NAME, CAST(a.START_DTTM AS TIMESTAMP) as START_DTTM, CAST(a.END_DTTM AS TIMESTAMP) as END_DTTM, max(e.OPERATION_LOG_STATUS_ID) as OPERATION_LOG_STATUS_ID ";

			sqlCount = "SELECT count(*) "
				+	"FROM ("
				+	"select distinct a.OPERATION_LOG_ID as OPERATION_LOG_ID, a.NODE_ADDRESS as NODE_ADDRESS, c.WEB_SERVICE_NAME as WEB_SERVICE_NAME, CAST(a.START_DTTM AS TIMESTAMP) as START_DTTM, CAST(a.END_DTTM AS TIMESTAMP) as END_DTTM, max(e.OPERATION_LOG_STATUS_ID) as OPERATION_LOG_STATUS_ID ";

			//tableName = " from (select * from NODE_OPERATION_LOG where OPERATION_LOG_ID > (select max(OPERATION_LOG_ID) from NODE_OPERATION_LOG) - " + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)) + " ) a "
			// initial just get fix record
			/*if((nodeAddress == null || nodeAddress.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				tableName = " from (select * from NODE_OPERATION_LOG where OPERATION_LOG_ID > (SELECT MAX(OPERATION_LOG_ID)"
							+" FROM NODE_OPERATION_LOG a LEFT OUTER JOIN NODE_OPERATION b "
							+" ON a.OPERATION_ID=b.OPERATION_ID "
							+"  LEFT OUTER JOIN NODE_WEB_SERVICE c "
							+"  ON b.WEB_SERVICE_ID=c.WEB_SERVICE_ID where c.WEB_SERVICE_NAME = '"+ ((opWebservice != null && !opWebservice.equals(""))?opWebservice:null ) +"') - " + Integer.parseInt(recordsReturned) + " ) a ";
			}else{
				tableName = " from NODE_OPERATION_LOG a ";
			} */
			tableName = " from (SELECT * FROM "
						+ " (SELECT row_number() over (order by a.OPERATION_LOG_ID DESC) as id, a.* FROM NODE_OPERATION_LOG a LEFT OUTER JOIN NODE_OPERATION b ON a.OPERATION_ID = b.OPERATION_ID "
						+ " where b.WEB_SERVICE_ID = " + wsID +" and b.VERSION_NO = '" + version + "') aa where aa.id <=4) a ";
			tableName += "left outer join NODE_OPERATION b on a.OPERATION_ID=b.OPERATION_ID "
			+	"left outer join NODE_WEB_SERVICE c on b.WEB_SERVICE_ID=c.WEB_SERVICE_ID "
			+	"left outer join NODE_DOMAIN d on b.DOMAIN_ID=d.DOMAIN_ID "
			+	"left outer join NODE_OPERATION_LOG_STATUS e on a.OPERATION_LOG_ID=e.OPERATION_LOG_ID ";

			condition = "where  b.VERSION_NO = '" + version + "'";
			
		    if (domainPermissions != null && domainPermissions.length > 0) {
		    	condition = condition + " and d.DOMAIN_NAME in (";
		        for (int i = 0; i < domainPermissions.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+domainPermissions[i]+"'";
		        }
		        condition += ")";
		    }

		    if(sort.equalsIgnoreCase("operationLogId")) sortDir = " order by " + "OPERATION_LOG_ID" + " " + dir;
			else if(sort.equalsIgnoreCase("nodeAddress")) sortDir = " order by " + "NODE_ADDRESS" + " " + dir;
			else if(sort.equalsIgnoreCase("startDate")) sortDir = " order by " + "START_DTTM" + " " + dir;
			else if(sort.equalsIgnoreCase("endDate")) sortDir = " order by " + "END_DTTM" + " " + dir;
					    
			if (nodeAddress != null && !nodeAddress.equals(""))
				condition += " and a.NODE_ADDRESS = :nodeAddress";
			if (opWebservice != null && !opWebservice.equals(""))
				condition += " and c.WEB_SERVICE_NAME = :opWebservice";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.START_DTTM > :startDate or a.END_DTTM > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.START_DTTM < :endDate or a.END_DTTM < :endDate)";				

			tx = session.beginTransaction();
			
			//condition += (" and rownum <= " + Integer.parseInt((maxGridRecords == null || maxGridRecords.equals("")?maxGridRecordsDefault:maxGridRecords)));
			sql+=tableName+condition+" group by a.OPERATION_LOG_ID, a.NODE_ADDRESS,  c.WEB_SERVICE_NAME,  a.START_DTTM, a.END_DTTM " + (Utility.isNullOrEmpty(sortDir)?"":sortDir) +") A";
			sqlCount+=tableName+condition+" group by a.OPERATION_LOG_ID, a.NODE_ADDRESS,  c.WEB_SERVICE_NAME, a.START_DTTM, a.END_DTTM) A";

			Query query = session.createSQLQuery(sql);			
			Query queryCount = session.createSQLQuery(sqlCount);
					
			if (nodeAddress != null && !nodeAddress.equals("")){
				query.setString("nodeAddress", nodeAddress);
				queryCount.setString("nodeAddress", nodeAddress);
			}
			if (opWebservice != null && !opWebservice.equals("")){
				query.setString("opWebservice", opWebservice);
				queryCount.setString("opWebservice", opWebservice);			
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
			
			// initial just get fix record
			if((nodeAddress == null || nodeAddress.equals("")) && (startDate == null || startDate.equals("")) && (endDate == null || endDate.equals(""))){
				this.totalNotificationLogRecords = Integer.parseInt(recordsReturned);			
			}else{
				ret = queryCount.list();
				this.totalNotificationLogRecords = ((BigDecimal)ret.get(0)).longValue();
			}
			
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

    /*
    public List getNotificationLogs(String startIndex,String recordsReturned,String sort,String dir,String nodeAddress,String opWebservice,String startDate, String endDate, String[] domainPermissions, String version){
		Session session = getHibernateTemplate().getSessionFactory().openSession();
		Transaction tx = null;
		List ret = null;
		String sql = null;
		String sqlCount = null;
		String tableName = null;
		String condition = null;
		String sortDir = null;

		try {
			sql = "select a.operationLogId,a.nodeAddress,a.startDate ";
			sqlCount = "select count(*) ";
			tableName = "from OperationLog a, Operation b left outer join b.webservice c,Domain d,OperationLogStatus e ";
			condition = "where a.operation.operationId = b.operationId " +
					"and b.domain.domainId = d.domainId " +
					//"and b.webservice.webServiceId = c.webServiceId " +
					"and e.operationLog.operationLogId = a.operationLogId " +
					"and e.operationLogStatusId = (select MAX(f.operationLogStatusId) " +
					"from OperationLogStatus f where a.operationLogId = f.operationLog.operationLogId) " +
					"and b.versionNo = '" + version + "'";
			
		    if (domainPermissions != null && domainPermissions.length > 0) {
		    	condition = condition + " and d.domainName in (";
		        for (int i = 0; i < domainPermissions.length; i++) {
		          if (i != 0)
		        	  condition += ",";
		          condition += "'"+domainPermissions[i]+"'";
		        }
		        condition += ")";
		    }

		    if(sort.equalsIgnoreCase("operationLogId")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("nodeAddress")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("startDate")) sortDir = " order by " + "a."+sort + " " + dir;
			else if(sort.equalsIgnoreCase("endDate")) sortDir = " order by " + "a."+sort + " " + dir;
			
			if (nodeAddress != null && !nodeAddress.equals(""))
				condition += " and a.nodeAddress like :nodeAddress";
			if (opWebservice != null && !opWebservice.equals(""))
				condition += " and c.webServiceName like :opWebservice";
			if (startDate != null && !startDate.equals(""))
				condition += " and (a.startDate > :startDate or a.endDate > :startDate)";
			if	(endDate != null && !endDate.equals(""))
				condition += " and (a.startDate < :endDate or a.endDate < :endDate)";				

			tx = session.beginTransaction();
			
			sql+=tableName+condition+sortDir;
			sqlCount+=tableName+condition;
			Query query = session.createQuery(sql);
			Query queryCount = session.createQuery(sqlCount);
			
			if (nodeAddress != null && !nodeAddress.equals("")){
				query.setString("nodeAddress", nodeAddress+"%");
				queryCount.setString("nodeAddress", nodeAddress+"%");
			}
			if (opWebservice != null && !opWebservice.equals("")){
				query.setString("opWebservice", opWebservice+"%");
				queryCount.setString("opWebservice", opWebservice+"%");			
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
			this.totalNotificationLogRecords = ((Long)ret.get(0)).longValue();

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
	   * getOperationLog
	   * @param operId
	   * @return OperationLog
	   */
    public OperationLog getOperationLog(Long operId) {
		return (OperationLog) getHibernateTemplate().get(OperationLog.class, operId);
	}

	  /**
	   * saveOperationLog
	   * @param OperationLog
	   * @return 
	   */
	public void saveOperationLog(OperationLog operationLog) {
		getHibernateTemplate().saveOrUpdate(operationLog);

		if (log.isDebugEnabled()) {
			log.debug("OperationLog id set to: " + operationLog.getOperationLogId());
		}
	}

	  /**
	   * removeOperationLog
	   * @param operId
	   * @return 
	   */
	public void removeOperationLog(Long opLogId) {
		Object operationLog = getHibernateTemplate().load(OperationLog.class, opLogId);
		getHibernateTemplate().delete(operationLog);
	}

	  /**
	   * getTotalOperationLogRecords
	   * @param 
	   * @return long
	   */
	public long getTotalOperationLogRecords() {
		return this.totalOperationLogRecords;
	}
	
	  /**
	   * getTotalScheduledTasksLogRecords
	   * @param 
	   * @return long
	   */
	public long getTotalScheduledTasksLogRecords() {
		return this.totalScheduledTasksLogRecords;
	}
	
	  /**
	   * getTotalNotificationLogRecords
	   * @param 
	   * @return long
	   */
	public long getTotalNotificationLogRecords() {
		return this.totalNotificationLogRecords;
	}

}
