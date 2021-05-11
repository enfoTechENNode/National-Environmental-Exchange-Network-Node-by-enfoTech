package com.enfotech.rest.dao.hibernate;

import java.util.ArrayList;

import org.hibernate.Session;
import org.springframework.transaction.annotation.Transactional;

import Node.Phrase;
import Node.Biz.Administration.Operation;
import Node.DB.DBManager;
import Node.DB.Interfaces.INodeOperation;

import com.enfotech.rest.dao.domain.OperationDao;

public class OperatonDaoImpl extends BaseDaoImpl implements OperationDao {

	public OperatonDaoImpl() {
		super();
	}
	
	public Operation getOperation(String opID){
		Operation op = null;
        INodeOperation opDB = null;
		try {
			opDB = DBManager.GetNodeOperation(Phrase.WebServicesLoggerName);
			op = opDB.GetOperation(Integer.valueOf(opID));
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return op;
	}

	@SuppressWarnings("unchecked")
	@Override
	@Transactional
	public ArrayList<Object[]> getPublicQueryOperations() {
		Session session = sf.getCurrentSession();
		String hql = "";
		
//		hql = " select a.OPERATION_TYPE, b.DOMAIN_NAME " +
//				" from NODE_OPERATION a, NODE_DOMAIN b " +
//				" where a.REST_IND = '" + Phrase.SIMPLE_YES + "' " +
//				" and a.PUBLISH_IND = '" + Phrase.SIMPLE_YES + "' " +
//				" and a.OPERATION_TYPE = '" + Phrase.WEB_SERVICE_OPERATION + "' ";
//		
//		ArrayList operationLst = (ArrayList) session.createSQLQuery(hql).list();
		
		ArrayList operationLst = (ArrayList) session
				.createQuery(
						" select a.operationId,a.operationName, b.domainName from NodeOperation as a left outer join a.nodeDomain as b left outer join a.nodeWebService as c "
								+ " where "
								+ " a.restInd = :restInd "
								+ " and a.publishInd = :publishInd "
								+ " and a.operationType = :operationType "
								+ " and c.webServiceName = :webServiceName "
								)
				.setString("restInd", Phrase.SIMPLE_YES)
				.setString("publishInd", Phrase.SIMPLE_YES)
				.setString("operationType", Phrase.WEB_SERVICE_OPERATION)
				.setString("webServiceName", Phrase.WEB_METHOD_QUERY)
				.list();

		return operationLst;
	}

}
