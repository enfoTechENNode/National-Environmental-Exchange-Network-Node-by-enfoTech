package com.enfotech.rest.dao.hibernate;

import org.hibernate.SessionFactory;
import org.springframework.beans.factory.annotation.Autowired;

import com.enfotech.rest.dao.BaseDao;

public class BaseDaoImpl implements BaseDao{

	@Autowired
	public SessionFactory sf;
}
