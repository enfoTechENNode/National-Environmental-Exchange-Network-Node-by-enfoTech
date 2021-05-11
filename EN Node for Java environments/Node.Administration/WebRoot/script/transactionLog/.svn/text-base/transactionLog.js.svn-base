
	// Add the additional 'advanced' VTypes for date search

	Ext.apply(Ext.form.field.VTypes,{
	    daterange : function(val, field) {
	        var date = field.parseDate(val);
	
	        if(!date){
	            return;
	        }
	        if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
	            var start = Ext.getCmp(field.startDateField);
	            start.setMaxValue(date);
	            start.validate();
	            this.dateRangeMax = date;
	        } 
	        else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
	            var end = Ext.getCmp(field.endDateField);
	            end.setMinValue(date);
	            end.validate();
	            this.dateRangeMin = date;
	        }
	        /*
	         * Always return true since we're only using this vtype to set the
	         * min/max allowed values (these are tested for after the vtype test)
	         */
	        return true;
	    }
	});
	
	// Create search panel
    var transactionSearch = Ext.create('Ext.FormPanel', {
        labelWidth: 120, // label settings here cascade unless overridden
        frame:true,
        title: 'Search Transaction',
        bodyStyle:'padding:5px 5px 0',
        width: 400,
        defaults: {width: 300},
        defaultType: 'textfield',

        items: [Ext.create('Ext.form.field.ComboBox', {
        				id:'operationNameCombox',
                        fieldLabel: 'Operation Name',
    	                listeners: {
	   	                     // delete the previous query in the beforequery event or set
	   	                     // combo.lastQuery = null (this will reload the store the next time it expands)
	   	                     'beforequery': function(qe){
	   	                         delete qe.combo.lastQuery;
	   	                     }
  	                 	},
                        store: Ext.create('Ext.data.Store', {
					        remoteSort: true,
							id: 'id',					
					        fields: [
								{name: 'operationName', type: 'string', mapping: 'operationName'}
					        ],
					        // load using script tags for cross domain, if the data in on the same domain as
					        // this page, an HttpProxy would be better
					        proxy: {
					            type: 'ajax',
					            url: 'operation.do?method=getOperationNameList&ver='+version,
					            timeout: gridTimeout,
					            reader: {
					                type: 'json',
					                totalProperty: 'total',
					                root: 'results'
					            }
					        }
                        }),
                        width:400,
                        allowBlank:true,
                        displayField:'operationName',
                        typeAhead: true,
                        mode: 'remote',
                        triggerAction: 'all',
                        emptyText:'Select an Operation Name...',
                        selectOnFocus:true
                    }),Ext.create('Ext.form.field.ComboBox', {
	        				id:'operationTypeCombox',
	                        fieldLabel: 'Operation Type',
					        store: Ext.create('Ext.data.Store', {
					            fields: [{name:'operationType', type: 'string', mapping: 'operationType'}],
					            data : operationTypeData
					        }),
                         	displayField:'operationType',
                        	mode: 'local',
                        	allowBlank:true,
						    typeAhead: true,
						    triggerAction: 'all',
						    emptyText:'Select an Operation type...',
						    selectOnFocus:true
					}),Ext.create('Ext.form.field.ComboBox', {
	        				id:'webserviceCombox',
	                        fieldLabel: 'Web Service',
					        store: Ext.create('Ext.data.Store',{
					            fields: [{name:'opWebservice', type: 'string', mapping: 'opWebservice'}],
					            data : opWebserviceData
					        }),
                         	displayField:'opWebservice',
                        	mode: 'local',
                        	allowBlank:true,
						    typeAhead: true,
						    triggerAction: 'all',
						    emptyText:'Select a Web Service...',
						    selectOnFocus:true
					}),Ext.create('Ext.form.field.ComboBox', {
        				id:'operationStatusCombox',
                        fieldLabel: 'Status',
    	                listeners: {
	   	                     // delete the previous query in the beforequery event or set
	   	                     // combo.lastQuery = null (this will reload the store the next time it expands)
	   	                     'beforequery': function(qe){
	   	                         delete qe.combo.lastQuery;
	   	                     }
  	                 	},
                        store: Ext.create('Ext.data.Store', {
					        root: 'results',
					        totalProperty: 'total',
					        remoteSort: true,
							id: 'id',					
					        fields: [
								{name: 'opStatus', type: 'string', mapping: 'opStatus'}
					        ],					
					        // load using script tags for cross domain, if the data in on the same domain as
					        // this page, an HttpProxy would be better
					        proxy: {
					            type: 'ajax',
					            url: 'operation.do?method=getOperationStatusList&ver='+version,
					            timeout: gridTimeout,
					            reader: {
					                type: 'json',
					                totalProperty: 'total',
					                root: 'results'
					            }
					        }
                        }),
                        allowBlank:true,
                        displayField:'opStatus',
                        typeAhead: true,
                        mode: 'remote',
                        triggerAction: 'all',
                        emptyText:'Select a Status...',
                        selectOnFocus:true
                    }),Ext.create('Ext.form.field.ComboBox', {
        				id:'operationDomainCombox',
                        fieldLabel: 'Domain',
    	                listeners: {
	   	                     // delete the previous query in the beforequery event or set
	   	                     // combo.lastQuery = null (this will reload the store the next time it expands)
	   	                     'beforequery': function(qe){
	   	                         delete qe.combo.lastQuery;
	   	                     }
  	                 	},
                        store: Ext.create('Ext.data.Store' ,{
					        remoteSort: true,
							id: 'id',					
					        fields: [
								{name: 'opDomain', type: 'string', mapping: 'opDomain'}
					        ],
					        // load using script tags for cross domain, if the data in on the same domain as
					        // this page, an HttpProxy would be better
					        proxy: {
					            type: 'ajax',
					            url: 'operation.do?method=getDomainList&ver='+version,
					            timeout: gridTimeout,
					            reader: {
					                type: 'json',
					                totalProperty: 'total',
					                root: 'results'
					            }
					        }
                        }),
                        allowBlank:true,
                        displayField:'opDomain',
                        typeAhead: true,
                        mode: 'remote',
                        triggerAction: 'all',
                        emptyText:'Select a Domain...',
                        selectOnFocus:true
                    }),{
                    	id:'opUserId',
                        fieldLabel: 'User ID',
                        name: 'opUserId'
                    }, {
                    	id:'opSecurityToken',
                        fieldLabel: 'Security Token',
                        name: 'opSecurityToken'
                    }, {
                    	id:'opTransactionId',
                        fieldLabel: 'Transaction ID',
                        name: 'opTransactionId'
                    },{
				        id: 'opStartdt',
				        fieldLabel: 'Start Date',
				        name: 'opStartdt',
				        xtype:'datefield',
				        vtype: 'daterange',
				        endDateField: 'opEnddt' // id of the end date field
				    },{
				        id: 'opEnddt',
				        fieldLabel: 'End Date',
				        name: 'opEnddt',
				        xtype:'datefield',
				        vtype: 'daterange',
				        startDateField: 'opStartdt' // id of the start date field
      	}],
        buttons: [{
 	    		id:'transactionLogSearch',
	    		text:'Search',
	    		type:'submit',
	    		handler:function (){
	    			Ext.Msg.wait('loading'); 
	    			// Reset the pageToolBar for the new search
					if(Ext.getCmp('transactionLogPagingBarSmall')){		    						
						Ext.getCmp('transactionLogPagingBarSmall').moveFirst();
					};
	    			transactionLogStoreList.load({
	    				callback:function(r,options,success){
		    				if(success){
		    	    			// Reset the pageToolBar for the new search
		    					if(Ext.getCmp('transactionLogPagingBarLarge')){		    						
		    						Ext.getCmp('transactionLogPagingBarLarge').moveFirst();
		    					};
				    			transactionLogStore.load({
				    				callback:function(r,options,success){
					    				if(success){
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 	    				
					    					Ext.getCmp('TransactionLogSearchWin').hide('transactionLogPortlet');	    				
					    				}else{
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
											Ext.Ajax.request({
												url:'operation.do?method=getOperationLogList',
												success:function(response,options){						
													var rsp = response.responseText;							
								            		Ext.Msg.alert('Message', rsp);	        		
												},		
												failure:function(){
													Ext.Msg.alert('Message', "Fail to access database.");												
												}
											})
					    				}
				    				}
				    			});
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'operation.do?method=getOperationLogList',
									success:function(response,options){						
										var rsp = response.responseText;							
					            		Ext.Msg.alert('Message', rsp);	        		
									},		
									failure:function(){
										Ext.Msg.alert('Message', "Fail to access database.");												
									}
								})
		    				}
	    				}
	    			});
				}
        }]
    });
	    
    // create the Data Store of List
    var transactionLogStoreList = Ext.create('Ext.data.Store',{
    	storeId:'transactionLogStoreList',
        remoteSort: true,
        model: Ext.define('TransactionLogStoreListMd', {
            extend: 'Ext.data.Model',
            fields: [
         	        {name: 'operationLogId', type: 'string', mapping: 'operationLogId'},
         	        {name: 'operationName', type: 'string', mapping: 'operationName'},
         			{name: 'operationType', type: 'string', mapping: 'operationType'},
         	        {name: 'webServiceName', type: 'string', mapping: 'webServiceName'},
         			{name: 'domainName', type: 'string', mapping: 'domainName'},
         	        {name: 'userName', type: 'string', mapping: 'userName'},
         			{name: 'token', type: 'string', mapping: 'token'},
         			{name: 'transId', type: 'string', mapping: 'transId'},
         	        {name: 'operationLogStatusCD', type: 'string', mapping: 'operationLogStatusCD'},
         			{name: 'startDate', type: 'string', mapping: 'startDate'},
         			{name: 'endDate', type: 'string', mapping: 'endDate'}
                 ]
        }),
        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getOperationLogList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        },
        listeners: {
        	'beforeload':function(store, operation, eOpts){
			// Must pass page refresh parameters
	        	transactionLogParams.limit = topNum;
	        	transactionLogParams.page = operation.page;
	        	transactionLogParams.sort = operation.sort;
	        	transactionLogParams.start = operation.start;

	    		transactionLogParams.opName=Ext.getCmp('operationNameCombox').getValue();
    			transactionLogParams.opType=Ext.getCmp('operationTypeCombox').getValue();
    			transactionLogParams.opWebservice=Ext.getCmp('webserviceCombox').getValue();
    			transactionLogParams.opStatus=Ext.getCmp('operationStatusCombox').getValue();
    			transactionLogParams.opDomain=Ext.getCmp('operationDomainCombox').getValue();
    			transactionLogParams.opUserId=Ext.getCmp('opUserId').getValue();
    			transactionLogParams.opSecurityToken=Ext.getCmp('opSecurityToken').getValue();
    			transactionLogParams.opTransactionId=Ext.getCmp('opTransactionId').getValue();
    			transactionLogParams.startdt=Ext.getCmp('opStartdt').getValue()==null ? Ext.getCmp('opStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('opStartdt').getValue()), 'Y-m-d');
    			transactionLogParams.enddt=Ext.getCmp('opEnddt').getValue()==null ? Ext.getCmp('opEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('opEnddt').getValue()), 'Y-m-d');
    			transactionLogParams.ver=version;
    			
				operation.params = transactionLogParams;
			}
        }, 	                    
        sortOnLoad: true,
        sorters: { property: 'operationLogId', direction : 'DESC' }
    });
//    transactionLogStoreList.setDefaultSort('operationLogId', 'desc');


    // create the Data Store
    var transactionLogStore = Ext.create('Ext.data.Store',{
    	storeId:'transactionLogStore',
        remoteSort: true,
        model: Ext.define('TransactionLogStoreMd', {
            extend: 'Ext.data.Model',
            fields: [
         	        {name: 'operationLogId', type: 'string', mapping: 'operationLogId'},
         	        {name: 'operationName', type: 'string', mapping: 'operationName'},
         			{name: 'operationType', type: 'string', mapping: 'operationType'},
         	        {name: 'webServiceName', type: 'string', mapping: 'webServiceName'},
         			{name: 'domainName', type: 'string', mapping: 'domainName'},
         	        {name: 'userName', type: 'string', mapping: 'userName'},
         			{name: 'token', type: 'string', mapping: 'token'},
         			{name: 'transId', type: 'string', mapping: 'transId'},
         	        {name: 'operationLogStatusCD', type: 'string', mapping: 'operationLogStatusCD'},
         			{name: 'startDate', type: 'string', mapping: 'startDate'},
         			{name: 'endDate', type: 'string', mapping: 'endDate'}
                 ]
        }),
        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getOperationLogList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        },
        listeners: {
        	'beforeload':function(store, operation, eOpts){
			// Must pass page refresh parameters
	        	transactionLogParams.limit = rowNum;
	        	transactionLogParams.page = operation.page;
	        	transactionLogParams.sort = operation.sort;
	        	transactionLogParams.start = operation.start;

        		transactionLogParams.opName=Ext.getCmp('operationNameCombox').getValue();
    			transactionLogParams.opType=Ext.getCmp('operationTypeCombox').getValue();
    			transactionLogParams.opWebservice=Ext.getCmp('webserviceCombox').getValue();
    			transactionLogParams.opStatus=Ext.getCmp('operationStatusCombox').getValue();
    			transactionLogParams.opDomain=Ext.getCmp('operationDomainCombox').getValue();
    			transactionLogParams.opUserId=Ext.getCmp('opUserId').getValue();
    			transactionLogParams.opSecurityToken=Ext.getCmp('opSecurityToken').getValue();
    			transactionLogParams.opTransactionId=Ext.getCmp('opTransactionId').getValue();
    			transactionLogParams.startdt=Ext.getCmp('opStartdt').getValue()==null ? Ext.getCmp('opStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('opStartdt').getValue()), 'Y-m-d');
    			transactionLogParams.enddt=Ext.getCmp('opEnddt').getValue()==null ? Ext.getCmp('opEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('opEnddt').getValue()), 'Y-m-d');
    			transactionLogParams.ver=version;
    			
				operation.params = transactionLogParams;
			}
        }, 	                    
        sortOnLoad: true,
        sorters: { property: 'operationLogId', direction : 'DESC' }
    });
//    transactionLogStore.setDefaultSort('operationLogId', 'desc');


    /* pluggable renders
    function renderTopic(value, p, record){
        return String.format(
                '<b><a href="http://extjs.com/forum/showthread.php?t={2}" target="_blank">{0}</a></b><a href="http://extjs.com/forum/forumdisplay.php?f={3}" target="_blank">{1} Forum</a>',
                value, record.data.forumtitle, record.id, record.data.forumid);
    }
    function renderLast(value, p, r){
        return String.format('{0}<br/>by {1}', value.dateFormat('M j, Y, g:i a'), r.data['lastposter']);
    }*/

    function renderOperationNameSmall(value, p, record){
        return Ext.String.format(
                '<table border="0" style="color:#0055A6;background-color:#FFFFCC;font-size:11px;" width="100%"><tr><td width="8%"><a href="#" onclick="openTransactionLogDetailWin({0})"><img src="../../images/PnlIco/pnlico_view.gif" data-qtip="Click to view {1}"/></a></td><td width="90%">{1}</td></tr></table>'
                +'<span style="color:#000000;">Operation Type:</span> <span style="color:green;">{2}</span><br/>'
                +'<span style="color:#000000;">WebService Name:</span> <span style="color:green;">{3}</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#000000;">Domain Name:</span> <span style="color:green;">{4}</span><br/>'
                +'<span style="color:#000000;">Status:</span> <span style="color:green;">{5}</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#000000;">Date Create:</span> <span style="color:green;">{6}</span>',
                record.get('operationLogId'), record.get('operationName'),record.get('operationType'),record.get('webServiceName'),record.get('domainName'),record.get('operationLogStatusCD'),record.get('startDate'));
    }
    
    function renderOperationNameLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openTransactionLogDetailWin({0})">{1}</a></span>',
                record.get('operationLogId'), record.get('operationName'));
    }
    
    var createTransactionLogGridSmall = function(){
    	var tmp = Ext.create('Ext.grid.Panel',{
    		id: 'TransactionLogGridSmall',
            store: transactionLogStoreList,
            trackMouseOver:true,
            disableSelection:false,
            autoScroll:true,
            height:240,
            border:false,
            autoExpandColumn : 'searchHeader',
            viewConfig:{
                loadMask:{
        			listeners: {	// Remove the modal to the bottom of floating window
        				'show':function(){
    				    			this.toBack();
    				    		}
        			}
        		}
            },

            // grid columns
            columns:[{
            	id:'transactionLogGridSmallHeader',
                text: '&nbsp;',
    	        dataIndex: 'operation_Name',
    	        width :'100%',
                renderer: renderOperationNameSmall,
    			sortable: true
            }],

            // paging bar on the bottom to control the page size
            bbar: new Ext.PagingToolbar({
    	    	id:'transactionLogPagingBarSmall',
    	        store: transactionLogStoreList,
    	        hidden:false
        	})
        });
    	return tmp;
    }
    var TransactionLogGridSmall = createTransactionLogGridSmall();
    	
    var createTransactionLogGridLarge = function(){
    	var tmp = Ext.create('Ext.grid.Panel',{
			id: 'TransactionLogGridLarge',
	        store: transactionLogStore,
	        height:240,
	        viewConfig:{
	            loadMask:{
	    			listeners: {	// Remove the modal to the bottom of floating window
	    				'show':function(){
					    			this.toBack();
					    		}
	    			}
	    		}
	        },
	
	        // grid columns
	        columns:[{
	        	id:'transLog_Large_operation_Name',
	            text: 'Operation',
		        dataIndex: 'operationName',
	            renderer: renderOperationNameLarge,
				sortable: true
	        },{
	        	id:'transLog_Large_webService_Name',
	            text: 'WebService',
		        dataIndex: 'webServiceName',
				sortable: true
	        },{
	        	id:'transLog_Large_domain_Name',
	            text: 'Domain',
		        dataIndex: 'domainName',
				sortable: true
	        },{
	        	id:'transLog_Large_user_Name',
	            text: 'User',
		        dataIndex: 'userName',
				sortable: true
	        },{
	        	id:'transLog_Large_trans_Id',
	            text: 'Transaction',
		        dataIndex: 'transId',
				sortable: true
	        },{
	        	id:'transLog_Large_operationLog_StatusCD',
	            text: 'Status',
		        dataIndex: 'operationLogStatusCD',
				sortable: true
	        },{
	        	id:'transLog_Large_token',
	            text: 'Token',
		        dataIndex: 'token',
				sortable: true
	        },{
	        	id:'transLog_Large_operation_Type',
	            text: 'Operation Type',
		        dataIndex: 'operationType',
				sortable: true
	        },{
	        	id:'transLog_Large_startDate',
	            text: 'Start',
		        dataIndex: 'startDate',
		        width: 110,
				sortable: true
	        },{
	        	id:'transLog_Large_endDate',
	            text: 'End',
		        dataIndex: 'endDate',
		        width: 110,
				sortable: true
	        }],
	
	        // paging bar on the bottom
	        bbar: new Ext.PagingToolbar({
		    	id:'transactionLogPagingBarLarge',
		        store: transactionLogStore,
		        displayInfo: true,
		        displayMsg: 'Total:{2}',
		        emptyMsg: "Total:0"
		    })
	    });
    	return tmp;
    };
    var TransactionLogGridLarge = createTransactionLogGridLarge();

    var TransactionLogGridSeperate = Ext.create('Ext.grid.Panel',{
		id: 'TransactionLogGridSeperate',
        store: transactionLogStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,

        // grid columns
        columns:[{
        	id:'transLog_Seperate_operation_Name',
            text: 'Operation',
	        dataIndex: 'operationName',
	        width: 100,
            renderer: renderOperationNameLarge,
			sortable: true
        },{
        	id:'transLog_Seperate_webService_Name',
            text: 'WebService',
	        dataIndex: 'webServiceName',
	        width: 100,
			sortable: true
        },{
        	id:'transLog_Seperate_domain_Name',
            text: 'Domain',
	        dataIndex: 'domainName',
	        width: 100,
			sortable: true
        },{
        	id:'transLog_Seperate_user_Name',
            text: 'User',
	        dataIndex: 'userName',
	        width: 100,
			sortable: true
        },{
        	id:'transLog_Seperate_trans_Id',
            text: 'Transaction',
	        dataIndex: 'transId',
	        width: 100,
			sortable: true
        },{
        	id:'transLog_Seperate_operationLog_StatusCD',
            text: 'Status',
	        dataIndex: 'operationLogStatusCD',
	        width: 70,
			sortable: true
        },{
        	id:'transLog_Seperate_token',
            text: 'Token',
	        dataIndex: 'token',
	        width: 100,
			sortable: true
        },{
        	id:'transLog_Seperate_operation_Type',
            text: 'Operation Type',
	        dataIndex: 'operationType',
	        width: 106,
			sortable: true
        },{
        	id:'transLog_Seperate_startDate',
            text: 'Start',
	        dataIndex: 'startDate',
	        width: 110,
			sortable: true
        },{
        	id:'transLog_Seperate_endDate',
            text: 'End',
	        dataIndex: 'endDate',
	        width: 110,
			sortable: true
        }],

        // paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'transactionLogPagingBarSeperate',
	        store: transactionLogStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    })
    });

    // base parameter
    var transactionLogParams = {limit:rowNum,page:1,sort:{},start:0,opName:Ext.getCmp('operationNameCombox').getValue(),
	    				opType:Ext.getCmp('operationTypeCombox').getValue(),opWebservice:Ext.getCmp('webserviceCombox').getValue(),
	    				opStatus:Ext.getCmp('operationStatusCombox').getValue(),opDomain:Ext.getCmp('operationDomainCombox').getValue(),
	    				opUserId:Ext.getCmp('opUserId').getValue(),opSecurityToken:Ext.getCmp('opSecurityToken').getValue(),
	    				opTransactionId:Ext.getCmp('opTransactionId').getValue(),
	    				startdt: Ext.getCmp('opStartdt').getValue()==null ? Ext.getCmp('opStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('opStartdt').getValue()), 'Y-m-d'),
	    				enddt: Ext.getCmp('opEnddt').getValue()==null ? Ext.getCmp('opEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('opEnddt').getValue()), 'Y-m-d'),
	    				ver:version};

    // trigger the list data store  load
    var loadTransactionLogStoreList=function(){
    	transactionLogStoreList.load({start:startIndex, limit:topNum,
	    				callback:function(r,options,success){
		    				if(success ){
		    				// if success and return record number bigger than switch number then load Grid	    				
								//loadTransactionLogStore();
								// to Stop the initial loading process panel
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');   				
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'operation.do?method=getOperationLogList',
									success:function(response,options){						
										var rsp = response.responseText;							
					            		Ext.Msg.alert('Message', rsp);	        		
									},		
									failure:function(){
										Ext.Msg.alert('Message', "Fail to access database.");												
									}
								})
		    				}
	    				}
	    });
    }
    
    // trigger the grid data store load
    var loadTransactionLogStore=function(){
    	transactionLogStore.load({start:startIndex, limit:rowNum,
	    				callback:function(r,options,success){
		    				if(success){
		    				// switch list and grid	    
								if(r.length > pageSize){
//									Ext.getCmp('TransactionLogGridSmall').hide();
//									Ext.getCmp('TransactionLogGridLarge').show();
									Ext.getCmp('transactionLogPortlet').remove(Ext.getCmp('TransactionLogGridSmall'),false);
									TransactionLogGridLarge = createTransactionLogGridLarge();
									Ext.getCmp('transactionLogPortlet').add(Ext.getCmp('TransactionLogGridLarge'));
									transactionLogGridShow = 'large';
								    // trigger the data store load
								    //loadTransactionLogStore();
//								    Ext.getCmp('viewport').doLayout();
								}
								// to Stop the initial loading process panel
		    					Ext.Msg.close('loading'); 	    				
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'operation.do?method=getOperationLogList',
									success:function(response,options){						
										var rsp = response.responseText;							
					            		Ext.Msg.alert('Message', rsp);	        		
									},		
									failure:function(){
										Ext.Msg.alert('Message', "Fail to access database.");												
									}
								})
		    				}
	    				}
	    });
    }
    
    // function to open a detail window
    
   var openTransactionLogDetailWin=function(transactionLogId){
		if(!transactionLogDetailWin){
			transactionLogIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'transactionLogIframe'
			});		
			transactionLogDetailWin = Ext.create('Ext.window.Window',{
				id:'transactionLogDetailWin',
				layout      : 'fit',
				width       : 800,
				height      : 530,
				minWidth 	: 300,
				miniHeight	: 300,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [transactionLogIframe]
			});
		}
		if(!transactionLogDetailWin.isHidden()){
			Ext.getCmp('transactionLogIframe').load('/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+transactionLogId);
		}else{
			transactionLogDetailWin.on('show',function(){
				Ext.getCmp('transactionLogIframe').load('/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+transactionLogId);
			});
		}
		transactionLogDetailWin.show('transactionLogPortlet');

   };
