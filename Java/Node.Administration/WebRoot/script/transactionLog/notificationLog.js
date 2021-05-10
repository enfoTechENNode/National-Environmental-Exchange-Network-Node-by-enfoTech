	
	// Create search panel
    var notificationSearch = Ext.create('Ext.FormPanel',{
        labelWidth: 120, // label settings here cascade unless overridden
        frame:true,
        title: 'Search Notifications',
        bodyStyle:'padding:5px 5px 0',
        width: 400,
        defaults: {width: 300},
        defaultType: 'textfield',

        items: [{
            	id:'notificationNodeAddress',
                fieldLabel: 'Node Address',
                name: 'notificationNodeAddress'
                },{
		        id: 'notificationStartdt',
		        fieldLabel: 'Start Date',
		        name: 'startdt',
		        xtype:'datefield',
		        vtype: 'daterange',
		        endDateField: 'scheduledTasksEnddt' // id of the end date field
			    },{
		        id: 'notificationEnddt',
		        fieldLabel: 'End Date',
		        name: 'enddt',
		        xtype:'datefield',
		        vtype: 'daterange',
		        startDateField: 'notificationStartdt' // id of the start date field		        
			    }],
        buttons: [{
 	    		id:'notificationLogSearch',
	    		text:'Search',
	    		type:'submit',
	    		handler:function (){
		    		/* check if both start date and end date have been input
			        if((statdt!='' && enddt=='')||(statdt =='' && enddt!='')){
			        	Ext.Msg.alert('Message', 'You must input both start date and end date.');
			        	return;
			        };*/
	    			Ext.Msg.wait('loading'); 
	    			// Reset the pageToolBar for the new search
					if(Ext.getCmp('notificationLogPagingBarSmall')){		    						
						Ext.getCmp('notificationLogPagingBarSmall').moveFirst();
					};
	    			notificationLogStoreList.load({
	    				callback:function(r,options,success){
		    				if(success){
		    	    			// Reset the pageToolBar for the new search
		    					if(Ext.getCmp('notificationLogPagingBarLarge')){		    						
		    						Ext.getCmp('notificationLogPagingBarLarge').moveFirst();
		    					};
				    			notificationLogStore.load({
				    				callback:function(r,options,success){
					    				if(success){
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 	    				
					    					Ext.getCmp('notificationLogSearchWin').hide('notificationLogPortlet');	    				
					    				}else{
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
											Ext.Ajax.request({
												url:'operation.do?method=getNotificationLogList',
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
									url:'operation.do?method=getNotificationLogList',
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
    var notificationLogStoreList = new Ext.data.JsonStore({
    	storeId:'notificationLogStoreList',
        remoteSort: true,
         listeners: {
     		'beforeload':function(store, operation, eOpts){
		    	notificationLogParams.limit = topNum;
		    	notificationLogParams.page = operation.page;
		    	notificationLogParams.sort = operation.sort;
		    	notificationLogParams.start = operation.start;
		
		    	notificationLogParams.nodeAddress=Ext.getCmp('notificationNodeAddress').getValue();
				notificationLogParams.startdt=Ext.getCmp('notificationStartdt').getValue()==null ? Ext.getCmp('notificationStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('notificationStartdt').getValue()), 'Y-m-d');
				notificationLogParams.enddt=Ext.getCmp('notificationEnddt').getValue()==null ? Ext.getCmp('notificationEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('notificationEnddt').getValue()), 'Y-m-d');
		
				operation.params = notificationLogParams;	    			
			}
        }, 	                    
        fields: [
	        {name: 'operationLogId', type: 'string', mapping: 'operationLogId'},
	        {name: 'nodeAddress', type: 'string', mapping: 'nodeAddress'},
			{name: 'startDate', type: 'string', mapping: 'startDate'}
        ],
        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getNotificationLogList',
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
//    notificationLogStoreList.setDefaultSort('operationLogId', 'desc');


    // create the Data Store
    var notificationLogStore = new Ext.data.JsonStore({
    	storeId:'notificationLogStore',
        remoteSort: true,
        baseParams:notificationLogParams,
        listeners: {
     		'beforeload':function(store, operation, eOpts){
		    	notificationLogParams.limit = rowNum;
		    	notificationLogParams.page = operation.page;
		    	notificationLogParams.sort = operation.sort;
		    	notificationLogParams.start = operation.start;
		
		    	notificationLogParams.nodeAddress=Ext.getCmp('notificationNodeAddress').getValue();
				notificationLogParams.startdt=Ext.getCmp('notificationStartdt').getValue()==null ? Ext.getCmp('notificationStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('notificationStartdt').getValue()), 'Y-m-d');
				notificationLogParams.enddt=Ext.getCmp('notificationEnddt').getValue()==null ? Ext.getCmp('notificationEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('notificationEnddt').getValue()), 'Y-m-d');
		
				operation.params = notificationLogParams;	    			
			}
        }, 	                    
        fields: [
	        {name: 'operationLogId', type: 'string', mapping: 'operationLogId'},
	        {name: 'nodeAddress', type: 'string', mapping: 'nodeAddress'},
			{name: 'startDate', type: 'string', mapping: 'startDate'}
        ],
        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getNotificationLogList',
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
//    notificationLogStore.setDefaultSort('operationLogId', 'desc');

    function renderNotificationNameSmall(value, p, record){
        return Ext.String.format(
                '<div style="color:#0055A6;background-color:#FFFFCC;" >Node Address: <a href="#" onclick="openNotificationLogDetailWin({0})">{1}</a></div> <br/>'
                +'<span style="color:#000000;">Date Created:</span> <span style="color:green;">{2}</span>',
                record.get('operationLogId'), record.get('nodeAddress'),record.get('startDate'));
    }
    
    function renderNotificationNameLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openNotificationLogDetailWin({0})">{1}</a></span>',
                record.get('operationLogId'), record.get('nodeAddress'));
    }
    
    var createNotificationLogGridSmall = function(){
    	var tmp = Ext.create('Ext.grid.Panel',{
    		id: 'NotificationLogGridSmall',
            store: notificationLogStoreList,
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
            	id:'notificationLogGridSmallHeader',
                header: '&nbsp;',
    	        dataIndex: 'operation_Name',
                renderer: renderNotificationNameSmall,
    			sortable: true
            }],

            // paging bar on the bottom to control the page size
            bbar: new Ext.PagingToolbar({
    	    	id:'notificationLogPagingBarSmall',
    	        store: notificationLogStoreList,
    	        hidden:false
        	})
        });
    	return tmp;
    }
    var NotificationLogGridSmall = createNotificationLogGridSmall();

    var createNotificationLogGridLarge = function(){
    	var tmp = Ext.create('Ext.grid.Panel',{
    		id: 'NotificationLogGridLarge',
            store: notificationLogStore,
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
            	id:'notification_Large_node_Address',
                header: 'Node Address',
    	        dataIndex: 'nodeAddress',
                renderer: renderNotificationNameLarge,
    			sortable: true
            },{
            	id:'notification_Large_created_Date',
                header: 'Date Created',
    	        dataIndex: 'startDate',
    	        width: 110,
    			sortable: true
            }],

            // paging bar on the bottom
            bbar: new Ext.PagingToolbar({
    	    	id:'notificationLogPagingBarLarge',
    	        store: notificationLogStore,
    	        displayInfo: true,
    	        displayMsg: 'Total:{2}',
    	        emptyMsg: "Total:0"
    	    })
        });
    	return tmp;
    }
    var NotificationLogGridLarge = createNotificationLogGridLarge();

    var NotificationLogGridSeperate = Ext.create('Ext.grid.Panel',{
		id: 'NotificationLogGridSeperate',
        store: notificationLogStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,

        // grid columns
        columns:[{
        	id:'notification_Seperate_node_Address',
            header: 'Node Address',
	        dataIndex: 'nodeAddress',
            renderer: renderNotificationNameLarge,
			sortable: true
        },{
        	id:'notification_Seperate_created_Date',
            header: 'Date Created',
	        dataIndex: 'startDate',
	        width: 110,
			sortable: true
        }],

        // paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'notificationLogPagingBarSeperate',
	        store: notificationLogStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    })
    });

    // base parameter
    var notificationLogParams = {limit:rowNum,page:1,sort:{},start:0,nodeAddress:Ext.getCmp('notificationNodeAddress').getValue(),
    					opWebservice:opWebserviceData[5],
	    				startdt: Ext.getCmp('notificationStartdt').getValue()==null ? Ext.getCmp('notificationStartdt').getValue():Ext.getCmp('notificationStartdt').getValue().format('Y-m-d'),
	    				enddt: Ext.getCmp('notificationEnddt').getValue()==null ? Ext.getCmp('notificationEnddt').getValue():Ext.getCmp('notificationEnddt').getValue().format('Y-m-d'),
	    				ver:version};

    // trigger the list data store  load
    var loadNotificationLogStoreList=function(){
    	notificationLogStoreList.load({
	    				callback:function(r,options,success){
		    				if(success ){
		    				// if success and return record number bigger than switch number then load Grid	    				
							//	loadNotificationLogStore();
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'operation.do?method=getNotificationLogList',
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
    var loadNotificationLogStore=function(){
    	notificationLogStore.load({
	    				callback:function(r,options,success){
		    				if(success){
		    				// switch list and grid	    				
								if(r.length > pageSize){
//									Ext.getCmp('NotificationLogGridSmall').hide();
//									Ext.getCmp('NotificationLogGridLarge').show();
									Ext.getCmp('notificationLogPortlet').removeAll();
									NotificationLogGridSmall = createNotificationLogGridSmall();
									Ext.getCmp('notificationLogPortlet').add(Ext.getCmp('NotificationLogGridSmall'));
									notificationLogGridShow = 'large';
								    // trigger the data store load
								    //loadNotificationLogStore();
//								    Ext.getCmp('viewport').doLayout();
								}
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'operation.do?method=getNotificationLogList',
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
    
   var openNotificationLogDetailWin=function(notificationLogId){
		if(!notificationLogDetailWin){
			notificationLogIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'notificationLogIframe'
			});		
			notificationLogDetailWin = new Ext.Window({
				id:'notificationLogDetailWin',
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
				items : [notificationLogIframe]
			});
		}
		if(!notificationLogDetailWin.isHidden()){
			Ext.getCmp('notificationLogIframe').load('/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+notificationLogId);
		}else{
			notificationLogDetailWin.on('show',function(){
				Ext.getCmp('notificationLogIframe').load('/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+notificationLogId);
			});
		}
		notificationLogDetailWin.show('notificationLogPortlet');

   };

