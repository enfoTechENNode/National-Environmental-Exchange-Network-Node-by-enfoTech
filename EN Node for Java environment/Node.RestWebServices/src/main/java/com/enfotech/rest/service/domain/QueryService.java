package com.enfotech.rest.service.domain;

import java.util.ArrayList;
import java.util.Hashtable;

import Node.Biz.Administration.Operation;

public interface QueryService {
	public ArrayList<Object[]> getPublicQueryOperations();
	public Operation getOperation(String opID);
	public String invoke(Hashtable<String , Object> allParamHt);
	public ArrayList<String> getRestfulServiceIntroduction();
}
