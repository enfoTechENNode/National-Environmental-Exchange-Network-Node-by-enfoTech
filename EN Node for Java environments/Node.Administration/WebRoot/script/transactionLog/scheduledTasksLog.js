	
	// Create search panel
    var scheduledTasksSearch = Ext.create('Ext.FormPanel', {
        labelWidth: 120, // label settings here cascade unless overridden
        frame:true,
        title: 'Search Scheduled Tasks',
        bodyStyle:'padding:5px 5px 0',
        width: 400,
        defaults: {width: 300},
        defaultType: 'textfield',

        items: [Ext.create('Ext.form.field.ComboBox',  {
        	id:'scheduledTasksNameCombox',
            fieldLabel: 'Operation Name',
            listeners: {
                    // delete the previous query in the beforequery event or set
                    // combo.lastQuery = null (this will reload the store the next time it expands)
                    'beforequery': function(qe){
                        delete qe.combo.lastQuery;
                    }
            	},
            store: Ext.create('Ext.data.JsonStore',  {
                remoteSort: true,
        		id: 'id',					
                fields: [
        			{name: 'operationName', type: 'string', mapping: 'operationName'}
                ],
		        // load using script tags for cross domain, if the data in on the same domain as
		        // this page, an HttpProxy would be better
		        proxy: {
		            type: 'ajax',
                    url: 'operation.do?method=getOperationNameList',
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
            queryMode: 'remote',
            triggerAction: 'all',
            emptyText:'Select an Operation Name...',
            selectOnFocus:true        
        }),new Ext.form.field.ComboBox({
        				id:'scheduledTasksStatusCombox',
                        fieldLabel: 'Status',
    	                listeners: {
	   	                     // delete the previous query in the beforequery event or set
	   	                     // combo.lastQuery = null (this will reload the store the next time it expands)
	   	                     'beforequery': function(qe){
	   	                         delete qe.combo.lastQuery;
	   	                     }
   	                 	},
                        store: Ext.create('Ext.data.JsonStore',  {
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
					            url: 'operation.do?method=getOperationStatusList',
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
                        queryMode: 'remote',
                        triggerAction: 'all',
                        emptyText:'Select a Status...',
                        selectOnFocus:true
                    }),new Ext.form.field.ComboBox({
        				id:'scheduledTasksDomainCombox',
                        fieldLabel: 'Domain',
    	                listeners: {
	   	                     // delete the previous query in the beforequery event or set
	   	                     // combo.lastQuery = null (this will reload the store the next time it expands)
	   	                     'beforequery': function(qe){
	   	                         delete qe.combo.lastQuery;
	   	                     }
  	                 	},
                        store: Ext.create('Ext.data.JsonStore', {
					        root: 'results',
					        totalProperty: 'total',
					        remoteSort: true,
							id: 'id',					
					        fields: [
								{name: 'opDomain', type: 'string', mapping: 'opDomain'}
					        ],
					        // load using script tags for cross domain, if the data in on the same domain as
					        // this page, an HttpProxy would be better
					        proxy: {
					            type: 'ajax',
					            url: 'operation.do?method=getDomainList',
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
                        queryMode: 'remote',
                        triggerAction: 'all',
                        emptyText:'Select a Domain...',
                        selectOnFocus:true
                    }),{
                    	id:'scheduledTasksUserId',
                        fieldLabel: 'User ID',
                        name: 'scheduledTasksUserId'
                    }, {
                    	id:'scheduledTasksSecurityToken',
                        fieldLabel: 'Security Token',
                        name: 'scheduledTasksSecurityToken'
                    }, {
                    	id:'scheduledTasksTransactionId',
                        fieldLabel: 'Scheduled Tasks ID',
                        name: 'scheduledTasksTransactionId'
                    },{
				        id: 'scheduledTasksStartdt',
				        fieldLabel: 'Start Date',
				        name: 'scheduledTasksStartdt',
				        xtype:'datefield',
				        vtype: 'daterange',
				        endDateField: 'scheduledTasksEnddt' // id of the end date field
				    },{
				        id: 'scheduledTasksEnddt',
				        fieldLabel: 'End Date',
				        name: 'scheduledTasksEnddt',
				        xtype:'datefield',
				        vtype: 'daterange',
				        startDateField: 'scheduledTasksStartdt' // id of the start date field
      	}],
        buttons: [{
 	    		id:'scheduledTasksLogSearch',
	    		text:'Search',
	    		type:'submit',
	    		handler:function (){
	    			Ext.Msg.wait('loading'); 
	    			// Reset the pageToolBar for the new search
					if(Ext.getCmp('scheduledTasksLogPagingBarSmall')){		    						
						Ext.getCmp('scheduledTasksLogPagingBarSmall').moveFirst();
					};
	    			scheduledTasksLogStoreList.load({
	    				callback:function(r,options,success){
		    				if(success){
		    	    			// Reset the pageToolBar for the new search
		    					if(Ext.getCmp('scheduledTasksLogPagingBarLarge')){		    						
		    						Ext.getCmp('scheduledTasksLogPagingBarLarge').moveFirst();
		    					};
				    			scheduledTasksLogStore.load({
				    				callback:function(r,options,success){
					    				if(success){
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 	    				
					    					Ext.getCmp('scheduledTasksLogSearchWin').hide('scheduledTasksLogPortlet');	    				
					    				}else{
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
											Ext.Ajax.request({
												url:'operation.do?method=getTaskLogList',
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
									url:'operation.do?method=getTaskLogList',
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
    var scheduledTasksLogStoreList = Ext.create('Ext.data.Store',{
    	storeId:'scheduledTasksLogStoreList',
        remoteSort: true,
        model: Ext.define('ScheduledTasksLogStoreListMd', {
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
        listeners: {
     		'beforeload':function(store, operation, eOpts){
				// Must pass page refresh parameters
		    	scheduledTasksLogParams.limit = topNum;
		    	scheduledTasksLogParams.page = operation.page;
		    	scheduledTasksLogParams.sort = operation.sort;
		    	scheduledTasksLogParams.start = operation.start;
		
		    	scheduledTasksLogParams.opName=Ext.getCmp('scheduledTasksNameCombox').getValue();
    			scheduledTasksLogParams.opType=operationTypeData[0];
    			scheduledTasksLogParams.opWebservice='';
    			scheduledTasksLogParams.opStatus=Ext.getCmp('scheduledTasksStatusCombox').getValue();
    			scheduledTasksLogParams.opDomain=Ext.getCmp('scheduledTasksDomainCombox').getValue();
    			scheduledTasksLogParams.opUserId=Ext.getCmp('scheduledTasksUserId').getValue();
    			scheduledTasksLogParams.opSecurityToken=Ext.getCmp('scheduledTasksSecurityToken').getValue();
    			scheduledTasksLogParams.opTransactionId=Ext.getCmp('scheduledTasksTransactionId').getValue();
    			scheduledTasksLogParams.startdt=Ext.getCmp('scheduledTasksStartdt').getValue()==null ? Ext.getCmp('scheduledTasksStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('scheduledTasksStartdt').getValue()), 'Y-m-d'),
    			scheduledTasksLogParams.enddt=Ext.getCmp('scheduledTasksEnddt').getValue()==null ? Ext.getCmp('scheduledTasksEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('scheduledTasksEnddt').getValue()), 'Y-m-d'),
    			scheduledTasksLogParams.ver=version;

				operation.params = scheduledTasksLogParams;
			}
        }, 	                    

        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getTaskLogList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        },
        sortOnLoad: true,
        sorters: { property: 'operationLogId', direction : 'DESC' }
    });
//    scheduledTasksLogStoreList.setDefaultSort('operationLogId', 'desc');


    // create the Data Store
    var scheduledTasksLogStore = Ext.create('Ext.data.Store',{
    	storeId:'scheduledTasksLogStore',
        remoteSort: true,
        model: Ext.define('ScheduledTasksLogStoreMd', {
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
        listeners: {
     		'beforeload':function(store, operation, eOpts){
				// Must pass page refresh parameters
		    	scheduledTasksLogParams.limit = rowNum;
		    	scheduledTasksLogParams.page = operation.page;
		    	scheduledTasksLogParams.sort = operation.sort;
		    	scheduledTasksLogParams.start = operation.start;

    			scheduledTasksLogParams.opName=Ext.getCmp('scheduledTasksNameCombox').getValue();
    			scheduledTasksLogParams.opType=operationTypeData[0];
    			scheduledTasksLogParams.opWebservice='';
    			scheduledTasksLogParams.opStatus=Ext.getCmp('scheduledTasksStatusCombox').getValue();
    			scheduledTasksLogParams.opDomain=Ext.getCmp('scheduledTasksDomainCombox').getValue();
    			scheduledTasksLogParams.opUserId=Ext.getCmp('scheduledTasksUserId').getValue();
    			scheduledTasksLogParams.opSecurityToken=Ext.getCmp('scheduledTasksSecurityToken').getValue();
    			scheduledTasksLogParams.opTransactionId=Ext.getCmp('scheduledTasksTransactionId').getValue();
    			scheduledTasksLogParams.startdt=Ext.getCmp('scheduledTasksStartdt').getValue()==null ? Ext.getCmp('scheduledTasksStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('scheduledTasksStartdt').getValue()), 'Y-m-d'),
    	    	scheduledTasksLogParams.enddt=Ext.getCmp('scheduledTasksEnddt').getValue()==null ? Ext.getCmp('scheduledTasksEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('scheduledTasksEnddt').getValue()), 'Y-m-d'),
    			scheduledTasksLogParams.ver=version;

				operation.params = scheduledTasksLogParams;
			}
        }, 	                    

        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getTaskLogList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        },
        sortOnLoad: true,
        sorters: { property: 'operationLogId', direction : 'DESC'}
    });
//   scheduledTasksLogStore.setDefaultSort('operationLogId', 'desc');

    function renderScheduledTasksNameSmall(value, p, record){
        return Ext.String.format(
                '<table style="color:#0055A6;background-color:#FFFFCC;font-size:11px;" width="100%"><tr><td width="8%"><a href="#" onclick="openScheduledTasksLogDetailWin({0})"><img src="../../images/PnlIco/pnlico_view.gif" data-qtip="Click to view {1}"/></a></td><td>{1}</td></table>'
                +'<span style="color:#000000;">Domain Name:</span> <span style="color:green;">{2}</span><br/>'
                +'<span style="color:#000000;">Status:</span> <span style="color:green;">{3}</span>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#000000;">Date Finished:</span> <span style="color:green;">{4}</span>',
                record.get('operationLogId'), record.get('operationName'),record.get('domainName'),record.get('operationLogStatusCD'),record.get('endDate'));
    }
    
    function renderScheduledTasksNameLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openScheduledTasksLogDetailWin({0})">{1}</a></span>',
                record.get('operationLogId'), record.get('operationName'));
    }
    

    var createScheduledTasksLogGridSmall = function(){
    	var tmp = Ext.create('Ext.grid.Panel', {
    		id: 'ScheduledTasksLogGridSmall',
            store: scheduledTasksLogStoreList,
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
            	id:'searchHeader',
            	text: '&nbsp;',
    	        dataIndex: 'operation_Name',
    	        width:"100%",
    	        renderer: renderScheduledTasksNameSmall,
    			sortable: true
            }],

            // paging bar on the bottom to control the page size
            bbar: new Ext.PagingToolbar({
    	    	id:'scheduledTasksLogPagingBarSmall',
    	        store: scheduledTasksLogStoreList,
    	        hidden:false
        	})
        });    	
    	return tmp;
    }
    var ScheduledTasksLogGridSmall = createScheduledTasksLogGridSmall();

    var createScheduledTasksLogGridLarge = function(){
    	var tmp = new Ext.grid.Panel({
    		id: 'ScheduledTasksLogGridLarge',
            store: scheduledTasksLogStore,
            trackMouseOver:true,
            disableSelection:false,
            autoScroll:true,
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
            	id:'schedule_Large_operation_Name',
                text: 'Operation',
    	        dataIndex: 'operationName',
                renderer: renderScheduledTasksNameLarge,
    			sortable: true
            },{
            	id:'schedule_Large_domain_Name',
                text: 'Domain',
    	        dataIndex: 'domainName',
    			sortable: true
            },{
            	id:'schedule_Large_trans_Id',
                text: 'Transaction',
    	        dataIndex: 'transId',
    			sortable: true
            },{
            	id:'schedule_Large_operationLog_StatusCD',
                text: 'Status',
    	        dataIndex: 'operationLogStatusCD',
    			sortable: true
            },{
            	id:'schedule_Large_operation_Type',
                text: 'Operation Type',
    	        dataIndex: 'operationType',
    			sortable: true
            },{
            	id:'schedule_Large_startDate',
                text: 'Start',
    	        dataIndex: 'startDate',
    	        width: 110,
    			sortable: true
            },{
            	id:'schedule_Large_endDate',
                text: 'End',
    	        dataIndex: 'endDate',
    	        width: 110,
    			sortable: true
            }],

            // paging bar on the bottom
            bbar: new Ext.PagingToolbar({
    	    	id:'scheduledTasksLogPagingBarLarge',
    	        store: scheduledTasksLogStore,
    	        displayInfo: true,
    	        displayMsg: 'Total:{2}',
    	        emptyMsg: "Total:0"
    	    })
        });
    	return tmp;
    }
    var ScheduledTasksLogGridLarge = createScheduledTasksLogGridLarge();

    var ScheduledTasksLogGridSeperate = new Ext.grid.Panel({
		id: 'ScheduledTasksLogGridSeperate',
        store: scheduledTasksLogStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,

        // grid columns
        columns:[{
        	id:'schedule_Seperate_operation_Name',
            text: 'Operation',
	        dataIndex: 'operationName',
	        width: 100,
            renderer: renderScheduledTasksNameLarge,
			sortable: true
        },{
        	id:'schedule_Seperate_domain_Name',
            text: 'Domain',
	        dataIndex: 'domainName',
	        width: 100,
			sortable: true
        },{
        	id:'schedule_Seperate_trans_Id',
            text: 'Transaction',
	        dataIndex: 'transId',
	        width: 100,
			sortable: true
        },{
        	id:'schedule_Seperate_operationLog_StatusCD',
            text: 'Status',
	        dataIndex: 'operationLogStatusCD',
	        width: 70,
			sortable: true
        },{
        	id:'schedule_Seperate_operation_Type',
            text: 'Operation Type',
	        dataIndex: 'operationType',
	        width: 106,
			sortable: true
        },{
        	id:'schedule_Seperate_startDate',
            text: 'Start',
	        dataIndex: 'startDate',
	        width: 110,
			sortable: true
        },{
        	id:'schedule_Seperate_endDate',
            text: 'End',
	        dataIndex: 'endDate',
	        width: 110,
			sortable: true
        }],

        // paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'scheduledTasksLogPagingBarSeperate',
	        store: scheduledTasksLogStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    })
    });

    // base parameter
    var scheduledTasksLogParams = {limit:rowNum,page:1,sort:{},start:0,opName:Ext.getCmp('scheduledTasksNameCombox').getValue(),
	    				opType:operationTypeData[0],opWebservice:'',
	    				opStatus:Ext.getCmp('scheduledTasksStatusCombox').getValue(),opDomain:Ext.getCmp('scheduledTasksDomainCombox').getValue(),
	    				opUserId:Ext.getCmp('scheduledTasksUserId').getValue(),opSecurityToken:Ext.getCmp('scheduledTasksSecurityToken').getValue(),
	    				opTransactionId:Ext.getCmp('scheduledTasksTransactionId').getValue(),
	    				startdt: Ext.getCmp('scheduledTasksStartdt').getValue()==null ? Ext.getCmp('scheduledTasksStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('scheduledTasksStartdt').getValue()), 'Y-m-d'),
	    				enddt: Ext.getCmp('scheduledTasksEnddt').getValue()==null ? Ext.getCmp('scheduledTasksEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('scheduledTasksEnddt').getValue()), 'Y-m-d'),
	    				ver:version};

    // trigger the list data store  load
    var loadScheduledTasksLogStoreList=function(){
    	scheduledTasksLogStoreList.load({
	    				callback:function(r,options,success){
		    				if(success ){
		    				// if success and return record number bigger than switch number then load Grid	    				
							//	loadScheduledTasksLogStore();
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'operation.do?method=getTaskLogList',
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
    var loadScheduledTasksLogStore=function(){
    	scheduledTasksLogStore.load({
	    				callback:function(r,options,success){
		    				if(success){
		    				// switch list and grid	    				
								if(r.length > pageSize){
//									Ext.getCmp('ScheduledTasksLogGridSmall').hide();
//									Ext.getCmp('ScheduledTasksLogGridLarge').show();
									Ext.getCmp('scheduledTasksLogPortlet').removeAll();
									ScheduledTasksLogGridLarge = createScheduledTasksLogGridLarge();
									Ext.getCmp('scheduledTasksLogPortlet').add(Ext.getCmp('ScheduledTasksLogGridLarge'));
									scheduledTasksLogGridShow = 'large';
								    // trigger the data store load
								    //loadScheduledTasksLogStore();
//								    Ext.getCmp('viewport').doLayout();
								}
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'operation.do?method=getTaskLogList',
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
    
   var openScheduledTasksLogDetailWin=function(scheduledTasksLogId){
		if(!scheduledTasksLogDetailWin){
			scheduledTasksLogIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'scheduledTasksLogIframe'
			});		
			scheduledTasksLogDetailWin = new Ext.Window({
				id:'scheduledTasksLogDetailWin',
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
				items : [scheduledTasksLogIframe]
			});
		}
		if(!scheduledTasksLogDetailWin.isHidden()){
			Ext.getCmp('scheduledTasksLogIframe').load('/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+scheduledTasksLogId);
		}else{
			scheduledTasksLogDetailWin.on('show',function(){
				Ext.getCmp('scheduledTasksLogIframe').load('/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+scheduledTasksLogId);
			});
		}
		scheduledTasksLogDetailWin.show('scheduledTasksLogPortlet');

   };

