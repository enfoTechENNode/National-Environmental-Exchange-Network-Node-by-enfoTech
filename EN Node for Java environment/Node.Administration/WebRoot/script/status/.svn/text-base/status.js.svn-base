	
	var isReturnRecords = false;
	
	// Create search panel
    var statusSearch = new Ext.FormPanel({
        labelWidth: 120, // label settings here cascade unless overridden
        frame:true,
        title: 'Search Status',
        bodyStyle:'padding:5px 5px 0',
        width: 400,
        defaults: {width: 300},
        defaultType: 'textfield',

        items: [{
            	id:'statusNodeAddress',
                fieldLabel: 'Node Address',
                name: 'statusNodeAddress'
                },{
		        id: 'statusStartdt',
		        fieldLabel: 'Start Date',
		        name: 'startdt',
		        xtype:'datefield',
		        vtype: 'daterange',
		        endDateField: 'scheduledTasksEnddt' // id of the end date field
			    },{
		        id: 'statusEnddt',
		        fieldLabel: 'End Date',
		        name: 'enddt',
		        xtype:'datefield',
		        vtype: 'daterange',
		        startDateField: 'statusStartdt' // id of the start date field		        
			    }],
        buttons: [{
 	    		id:'statusSearch',
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
					if(Ext.getCmp('statusPagingBarSmall')){		    						
						Ext.getCmp('statusPagingBarSmall').moveFirst();
					};
	    			statusStoreList.load({
	    				callback:function(r,options,success){
		    				if(success){
		    	    			// Reset the pageToolBar for the new search
		    					if(Ext.getCmp('statusPagingBarLarge')){		    						
		    						Ext.getCmp('statusPagingBarLarge').moveFirst();
		    					};
				    			statusStore.load({
				    				params:{start:startIndex, limit:rowNum},
				    				callback:function(r,options,success){
					    				if(success){
					    					Ext.Msg.close('loading'); 	    				
					    					Ext.getCmp('statusSearchWin').hide('statusPortlet');	    				
					    				}
				    				}
				    			});
		    				}
	    				}
	    			});
				}
        }]
    });
	    
    // create the Data Store of List
    var statusStoreList = new Ext.data.JsonStore({
    	storeId:'statusStoreList',
        id: 'gridId',
        remoteSort: true,
        baseParams:statusParams,
        model: Ext.define('StatusMd', {
            extend: 'Ext.data.Model',
            fields: [
         	        {name: 'gridId', type: 'string', mapping: 'gridId'},
        	        {name: 'pId', type: 'string', mapping: 'pId'},
        	        {name: 'processName', type: 'string', mapping: 'processName'},
        			{name: 'processStatus', type: 'string', mapping: 'processStatus'},
        			{name: 'operationId', type: 'string', mapping: 'operationId'}
        			]
        }),

        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'status.do?method=getStatusList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        },
        listeners:{
        	'beforeload':function(store, operation, eOpts){
	    		// Must pass page refresh parameters
	        	statusParams.limit = topNum;
	        	statusParams.page = operation.page;
	        	statusParams.sort = operation.sort;
	        	statusParams.start = operation.start;
					
    			statusParams.nodeAddress=Ext.getCmp('statusNodeAddress').getValue();
    			statusParams.startdt=Ext.getCmp('statusStartdt').getValue()==null ? Ext.getCmp('statusStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('statusStartdt').getValue()), 'Y-m-d');
    			statusParams.enddt=Ext.getCmp('statusEnddt').getValue()==null ? Ext.getCmp('statusEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('statusEnddt').getValue()), 'Y-m-d');
				
				operation.params = statusParams;
			}
        },
	    sortOnLoad: true,
	    sorters: { property: 'pId', direction : 'DESC' }
    });
//    statusStoreList.setDefaultSort('pId', 'desc');


    // create the Data Store
    var statusStore = Ext.create('Ext.data.Store', {
    	storeId:'statusStore',
        remoteSort: true,
        baseParams:statusParams,
        model: Ext.define('StatusMd', {
            extend: 'Ext.data.Model',
            fields: [
         	        {name: 'gridId', type: 'string', mapping: 'gridId'},
        	        {name: 'pId', type: 'string', mapping: 'pId'},
        	        {name: 'processName', type: 'string', mapping: 'processName'},
        			{name: 'processStatus', type: 'string', mapping: 'processStatus'},
        			{name: 'operationId', type: 'string', mapping: 'operationId'}
        			]
        }),

        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'status.do?method=getStatusList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        },
        listeners:{
        	'beforeload':function(store, operation, eOpts){
	    		// Must pass page refresh parameters
	        	statusParams.limit = rowNum;
	        	statusParams.page = operation.page;
	        	statusParams.sort = operation.sort;
	        	statusParams.start = operation.start;
					
    			statusParams.nodeAddress=Ext.getCmp('statusNodeAddress').getValue();
    			statusParams.startdt=Ext.getCmp('statusStartdt').getValue()==null ? Ext.getCmp('statusStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('statusStartdt').getValue()), 'Y-m-d');
    			statusParams.enddt=Ext.getCmp('statusEnddt').getValue()==null ? Ext.getCmp('statusEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('statusEnddt').getValue()), 'Y-m-d');
				
				operation.params = statusParams;
			}
        },
	    sortOnLoad: true,
	    sorters: { property: 'pId', direction : 'DESC' }
    });
//    statusStore.setDefaultSort('pId', 'desc');

 /*   function renderStatusNameSmall(value, p, record){
        return Ext.String.format(
                '<div style="color:#0055A6;background-color:#DDDDDD;" >Process Name: <a href="#" onclick="openStatusDetailWin({0})">{1}</a></div> <br/>'
                +'<span style="color:#000000;">Process Status:</span> <span style="color:green;">{2}</span>',
                record.get('operationId'), record.get('processName'),record.get('processStatus'));
    }
    
    function renderStatusNameLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openStatusDetailWin({0})">{1}</a></span>',
                record.get('operationId'), record.get('processName'));
    }
  */  
    function renderStatusNameSmall(value,metadata, record, rowIndex ){
    	if(record.get('processName')!='status'){
	        return Ext.String.format(
	                '<div style="color:#0055A6;background-color:#DDDDDD;" >Process Name: {0}</div>'
	                +'<span style="color:#000000;">Process Status:</span> <span style="color:green;">{1}</span>',
	               record.get('processName'),record.get('processStatus'));    	
    	}else if(isReturnRecords && rowIndex == 0 && record.get('processName')=='status'){
    		return Ext.String.format('<table style="color:red;background-color:#FFFF99;font-size:11px" width="100%"><tr><td>{0}</td></tr></table>',record.get('processStatus'));   
    	}else  return Ext.String.format('<table style="color:red;background-color:#FFFF99;font-size:11px" width="100%"><tr><td>{0}</td></tr><tr><td>Total of running processes is 0.</td></tr></table>',record.get('processStatus'));   	
    }
    
    function renderStatusNameLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;">{0}</span>',
                record.get('processName'));
    }
    
	var statusCol = {
        	id:'statusCol',
            header: '',
            renderer: renderStatusNameSmall,
            width:'100%',
			sortable: false
        };
	
	var createStatusGridSmall = function(){
		var tmp = Ext.create('Ext.grid.Panel',{
			id: 'StatusGridSmall',
	        store: statusStoreList,
	        trackMouseOver:true,
	        disableSelection:false,
	        autoScroll:true,
	        autoHeight:true,
	        height:240,
	        border:false,
	        autoExpandColumn : 'statusCol',
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
	        columns:[statusCol],
	        // paging bar on the bottom to control the page size
	        bbar: new Ext.PagingToolbar({
		    	id:'statusPagingBarSmall',
		        store: statusStoreList,
		        displayInfo: false,
		        displayMsg: 'Total running: {2}',
		        emptyMsg: "Total running: 0",
		        hidden:false
	    	})
	    });
		return tmp;
	}
    var StatusGridSmall = createStatusGridSmall();

    var createStatusGridLarge = function(){
    	var tmp = Ext.create('Ext.grid.Panel',{
    		id: 'StatusGridLarge',
            store: statusStore,
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
            	id:'Status_Large_Process_Name',
                header: 'Process Name',
    	        dataIndex: 'processName',
                renderer: renderStatusNameLarge,
    			sortable: true
            },{
            	id:'Status_Large_Process_Status',
                header: 'Process Status',
    	        dataIndex: 'processStatus',
    	        width: 200,
    			sortable: true
            }],

            // paging bar on the bottom
            bbar: new Ext.PagingToolbar({
    	    	id:'statusPagingBarLarge',
    	        store: statusStore,
    	        displayInfo: false,
    	        displayMsg: 'Total:{2}',
    	        emptyMsg: "Total:0"
    	    })
        });
    	return tmp;
    }
    var StatusGridLarge = createStatusGridLarge();

    var StatusGridSeperate = Ext.create('Ext.grid.Panel',{
		id: 'StatusGridSeperate',
        store: statusStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,
        // grid columns
        columns:[{
        	id:'Status_Seperate_Process_Name',
            header: 'Process Name',
	        dataIndex: 'processName',
            renderer: renderStatusNameLarge,
			sortable: true
        },{
        	id:'Status_Seperate_Process_Status',
            header: 'Process Status',
	        dataIndex: 'processStatus',
	        width: 200,
			sortable: true
        }],

        // paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'statusPagingBarSeperate',
	        store: statusStore,
	        displayInfo: false,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    })
    });

    // base parameter
    var statusParams = {limit:rowNum,page:1,sort:{},start:0,nodeAddress:Ext.getCmp('statusNodeAddress').getValue(),
    					opWebservice:opWebserviceData[5],
	    				startdt: Ext.getCmp('statusStartdt').getValue()==null ? Ext.getCmp('statusStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('statusStartdt').getValue()), 'Y-m-d'),
	    				enddt: Ext.getCmp('statusEnddt').getValue()==null ? Ext.getCmp('statusEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('statusEnddt').getValue()), 'Y-m-d')};

    // trigger the list data store  load
    var loadStatusStoreList=function(){
    	statusStoreList.load({
	    				callback:function(r,options,success){
		    				if(success ){
		    					if(r.length>1){
		    						isReturnRecords = true;
		    					}else isReturnRecords = false;
		    				
		    				/*Generate an empty record to triger showing empty information		    				
		    					if(r==null || r==''){
		    						statusStoreList.removeAll();
		    						record = new Ext.data.Record({
		    							'pId':null		    						
		    						})
		    						statusStoreList.add(record);
		    					}*/
		    				// if success and return record then load another Grid 	    				
								loadStatusStore();
		    				}		    				
	    				}
	    });
    }
    
    // trigger the grid data store load
    var loadStatusStore=function(){
    	statusStore.load({
	    				callback:function(r,options,success){
		    				if(success){
		    				// switch list and grid	    				
								if(r.length>1 && r.length > pageSize){
									statusStore.remove(0);
//									Ext.getCmp('StatusGridSmall').hide();
//									Ext.getCmp('StatusGridLarge').show();
									Ext.getCmp('statusPortlet').removeAll();
									StatusGridLarge = createStatusGridLarge();
									Ext.getCmp('statusPortlet').add(Ext.getCmp('StatusGridLarge'));
									statusGridShow = 'large';
								    // trigger the data store load
								    //loadStatusStore();
//									viewport.doLayout();
								}
		    				//	Ext.Msg.close('loading'); 	    				
		    				}
	    				}
	    });
    }
    
    // function to open a detail window
    
   var openStatusDetailWin=function(statusId){
		if(!statusDetailWin){
			statusIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'statusIframe',  
				src : '/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+statusId
			});		
			statusDetailWin = new Ext.Window({
				id:'statusDetailWin',
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
				items : [statusIframe]
			});
		}
		statusDetailWin.on('show',function(){
			Ext.get('statusIframe').load('/Node.Administration/Page/NodeMonitoring/TransactionView.do?opLogID='+statusId);
		});
		statusDetailWin.show('statusPortlet');

   };
   
	
   // Start a simple task that refresh once per 1.5 second
	var task = {
	    run: function(){
	        loadStatusStoreList();
	    },
	    interval: statusRefreshInterval //1 second
	}
	Ext.util.TaskManager.start(task);
   

