package com.enfotech.rest.dao.domain;

import java.util.ArrayList;

import Node.Biz.Administration.Operation;

import com.enfotech.rest.dao.BaseDao;
import com.enfotech.rest.domain.node.NodeOperation;

public interface OperationDao extends BaseDao{
	
	public Operation getOperation(String opID);
	public ArrayList<Object[]> getPublicQueryOperations();

}
