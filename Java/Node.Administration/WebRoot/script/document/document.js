	
	// Create search panel
    var documentSearch = Ext.create('Ext.FormPanel', {
        labelWidth: 120, // label settings here cascade unless overridden
        frame:true,
        title: 'Search Document',
        bodyStyle:'padding:5px 5px 0',
        width: 400,
        defaults: {width: 300},
        defaultType: 'textfield',

        items: [{
                	id:'documentName',
                    fieldLabel: 'Document Name',
                    name: 'documentName'
                }, {
                	id:'transId',
                    fieldLabel: 'Transaction ID',
                    name: 'transId'
                },new Ext.form.field.ComboBox({
    				id:'documentOperationNameCombox',
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
                    mode: 'remote',
                    triggerAction: 'all',
                    emptyText:'Select an Operation Name...',
                    selectOnFocus:true
                }),new Ext.form.field.ComboBox({
    				id:'documentDomainCombox',
                    fieldLabel: 'Domain',
					listeners: {
                    	// delete the previous query in the beforequery event or set
                    	// combo.lastQuery = null (this will reload the store the next time it expands)
                    	'beforequery': function(qe){
                    		delete qe.combo.lastQuery;
                    	}
                    },
                    store: Ext.create('Ext.data.Store',{
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
                    mode: 'remote',
                    triggerAction: 'all',
                    emptyText:'Select a Domain...',
                    selectOnFocus:true
                }),{
			        id: 'documentStartdt',
			        fieldLabel: 'Start Date',
			        name: 'documentStartdt',
			        xtype:'datefield',
			        vtype: 'daterange',
			        endDateField: 'documentEnddt' // id of the end date field
			    },{
			        id: 'documentEnddt',
			        fieldLabel: 'End Date',
			        name: 'documentEnddt',
			        xtype:'datefield',
			        vtype: 'daterange',
			        startDateField: 'documentStartdt' // id of the start date field
     			}],
       buttons: [{
 	    		id:'documentSearch',
	    		text:'Search',
	    		type:'submit',
	    		handler:function (){
	    			Ext.Msg.wait('loading'); 
	    			// Reset the pageToolBar for the new search
					if(Ext.getCmp('documentPagingBarSmall')){		    						
						Ext.getCmp('documentPagingBarSmall').moveFirst();
					};
	    			documentStoreList.load({
	    				callback:function(r,options,success){
		    				if(success){
		    	    			// Reset the pageToolBar for the new search
		    					if(Ext.getCmp('documentPagingBarLarge')){		    						
		    						Ext.getCmp('documentPagingBarLarge').moveFirst();
		    					};
				    			documentStore.load({
				    				callback:function(r,options,success){
					    				if(success){
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 	    				
					    					Ext.getCmp('documentSearchWin').hide('documentPortlet');	    				
					    				}else{
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
											Ext.Ajax.request({
												url:'document.do?method=getDocumentList',
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
									url:'document.do?method=getDocumentList',
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
    var documentStoreList = Ext.create('Ext.data.Store', {
    	storeId:'documentStoreList',
        remoteSort: true,
        model: Ext.define('DocumentStoreListMd', {
            extend: 'Ext.data.Model',
            fields: [
          	        {name: 'gridId', type: 'string', mapping: 'gridId'},
         	        {name: 'fileId', type: 'string', mapping: 'fileId'},
         	        {name: 'fileName', type: 'string', mapping: 'fileName'},
         			{name: 'fileType', type: 'string', mapping: 'fileType'},
         	        {name: 'fileSize', type: 'string', mapping: 'fileSize'},
         			{name: 'transId', type: 'string', mapping: 'transId'},
         			{name: 'domainName', type: 'string', mapping: 'domainName'},
         			{name: 'dataFlowName', type: 'string', mapping: 'dataFlowName'},
         			{name: 'updatedDate', type: 'string', mapping: 'updatedDate'}
                 ]
        }),

        // load using script tags for cross document, if the data in on the same document as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'document.do?method=getDocumentList',
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
				documentParams.limit = topNum;
				documentParams.page = operation.page;
				documentParams.sort = operation.sort;
				documentParams.start = operation.start;
				
				documentParams.documentName=Ext.getCmp('documentName').getValue();
				documentParams.transId=Ext.getCmp('transId').getValue();
				documentParams.domainName=Ext.getCmp('documentDomainCombox').getValue();
				documentParams.dataFlowName=Ext.getCmp('documentOperationNameCombox').getValue();
				documentParams.startdt=Ext.getCmp('documentStartdt').getValue()==null ? Ext.getCmp('documentStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('documentStartdt').getValue()), 'Y-m-d');
				documentParams.enddt=Ext.getCmp('documentEnddt').getValue()==null ? Ext.getCmp('documentEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('documentEnddt').getValue()), 'Y-m-d');
				
				operation.params = documentParams;
			}
        },
        sortOnLoad: true,
        sorters: { property: 'updatedDate', direction : 'DESC' }
    });
//    documentStoreList.setDefaultSort('updatedDate', 'desc');

    // create the Data Store
    var documentStore = Ext.create('Ext.data.Store', {
    	storeId:'documentStore',
        remoteSort: true,
        model: Ext.define('DocumentStoreMd', {
            extend: 'Ext.data.Model',
            fields: [
          	        {name: 'gridId', type: 'string', mapping: 'gridId'},
        	        {name: 'fileId', type: 'string', mapping: 'fileId'},
        	        {name: 'fileName', type: 'string', mapping: 'fileName'},
        			{name: 'fileType', type: 'string', mapping: 'fileType'},
        	        {name: 'fileSize', type: 'string', mapping: 'fileSize'},
        			{name: 'transId', type: 'string', mapping: 'transId'},
        			{name: 'domainName', type: 'string', mapping: 'domainName'},
        			{name: 'dataFlowName', type: 'string', mapping: 'dataFlowName'},
        			{name: 'updatedDate', type: 'string', mapping: 'updatedDate'}
                 ]
        }),
        // load using script tags for cross document, if the data in on the same document as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'document.do?method=getDocumentList',
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
				documentParams.limit = rowNum;
				documentParams.page = operation.page;
				documentParams.sort = operation.sort;
				documentParams.start = operation.start;
				
				documentParams.documentName=Ext.getCmp('documentName').getValue();
				documentParams.transId=Ext.getCmp('transId').getValue();
				documentParams.domainName=Ext.getCmp('documentDomainCombox').getValue();
				documentParams.dataFlowName=Ext.getCmp('documentOperationNameCombox').getValue();
				documentParams.startdt=Ext.getCmp('documentStartdt').getValue()==null ? Ext.getCmp('documentStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('documentStartdt').getValue()), 'Y-m-d');
				documentParams.enddt=Ext.getCmp('documentEnddt').getValue()==null ? Ext.getCmp('documentEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('documentEnddt').getValue()), 'Y-m-d');
				
				operation.params = documentParams;
			}
        },
	    sortOnLoad: true,
	    sorters: { property: 'updatedDate', direction : 'DESC' }
    });
//    documentStore.setDefaultSort('updatedDate', 'desc');

    function renderDocumentNameSmall(value, p, record){
        return Ext.String.format(
                '<table style="color:#0055A6;background-color:#FFFFCC;font-size:11px;" width="97%"><tr><td width="10%"><a href="#" onclick="openDocumentEditWin({0})"><img src="../../images/PnlIco/pnlico_new_edit.gif" data-qtip="Click to edit {1}"/></a></td><td align="left" width="70%">{2}</td>'
                +'<td align="center" width="10%"><a href="#" onclick="openDocumentDownloadWin(\'{0}\')"><img src="../../images/PnlIco/pnlico_import.gif" data-qtip="Click to download document {1}"/></a></td><td align="right" width="10%"><a href="#" onclick="deleteDocument(\'{0}\')"><img src="../../images/PnlIco/pnlico_rejected.gif" data-qtip="Click to delete document {1}"/></a></td></tr></table>'
                +'<table width="100%" style="font-size:11px"><tr><td align="left" style="color:#000000;" width="30%">Document Name:</td><td style="color:green;word-wrap:break-word;" width="70%">{1}</td></tr>'
                +'<tr><td><span style="color:#000000;">Data Flow:</span></td><td><span style="color:green;">{5}</span></td>'
                +'<tr><td><span style="color:#000000;">Transaction ID:</span></td><td><span style="color:green;">{3}</span></td>'
                +'<tr><td><span style="color:#000000;">Submitted Date:</span></td><td><span style="color:green;">{4}</span></td></table>',
                record.get('fileId'), record.get('fileName'),record.get('domainName'),record.get('transId'),record.get('updatedDate'),record.get('dataFlowName'));    	
    }
    
    function renderDocumentViewLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openDocumentDownloadWin(\'{0}\')">View</a></span>',
                record.get('fileId'));
    }
    
    function renderDocumentNameLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openDocumentEditWin({0})">{1}</a></span>',
                record.get('fileId'), record.get('fileName'));
    }
    
		    
	// define the components of portlet
   var createUploadDocumentBtnSmall = function(){
	   var tmp = new Ext.Button({id:'uploadDocumentBtnSmall',
	   		text:'Upload New File',
			type:'submit',
			handler:function (){
						openUploadDocumentWin();
					}
		});
	   return tmp;
   };   
   var uploadDocumentBtnSmall = createUploadDocumentBtnSmall();
   
   var createUploadDocumentBtnLarge = function(){
	   var tmp = new Ext.Button({id:'uploadDocumentBtnLarge',
   		text:'Upload New File',
   		type:'submit',
   		handler:function (){
   					openUploadDocumentWin();			    		
   				}
   		});
	   return tmp;
   };
   var uploadDocumentBtnLarge = createUploadDocumentBtnLarge();
	
   var createDeleteDocumentsBtnLarge = function(){
	   var tmp = new Ext.Button({id:'deleteDocumentsBtnLarge',
   		text:'Remove Selected',
		type:'submit',
		handler:function (){
				Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these documents?', function(flag){
					// get records
					var records = documentCheckBoxSMLarge.getSelection();
					if(records!=null && records !=''){
						var jsonData = "{documentList:[";
						for(var i = 0; i < records.length; i++){ 
							var ss = '\"' + records[i].get("fileId") + '\"';
							if(i==0){
								jsonData = jsonData + ss;									
							}else jsonData = jsonData+','+ss;
						}
						jsonData = jsonData + ']}';
						
			        	//send back  
						Ext.Ajax.request({
							url:'document.do?method=deleteDocuments',
							params:{
								fileIdList:jsonData
							},
							success:function(response,options){
								var rsp = response.responseText;							
			            		Ext.Msg.alert('Message', rsp);
			            		documentCheckBoxSMLarge.clearSelections();	        		
								loadDocumentStoreList();						
							},		
							failure:function(){
								Ext.Msg.alert('Message', "Fail to delete documents.");												
							}
						})									
					}else{
						Ext.Msg.alert('Message', "Please select document.");																				
					}
				})	        					        	
			}
		});
	   return tmp;
   };
   var deleteDocumentsBtnLarge = createDeleteDocumentsBtnLarge();
   var uploadDocumentBtnSeperate = new Ext.Button({id:'uploadDocumentBtnSeperate',
			    		text:'Upload New File',
			    		type:'submit',
			    		handler:function (){
			    					openUploadDocumentWin();			    		
		        				}
		        	});
		        	
   var deleteDocumentsBtnSeperate = new Ext.Button({id:'deleteDocumentsBtnSeperate',
			    		text:'Remove Selected',
			    		type:'submit',
			    		handler:function (){
        					Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these documents?', function(flag){
			    				// get records
								var records = documentCheckBoxSMSeperate.getSelection();
								if(records!=null && records !=''){
									var jsonData = "{documentList:[";
									for(var i = 0; i < records.length; i++){ 
										var ss = '\"' + records[i].get("fileId") + '\"';
										if(i==0){
											jsonData = jsonData + ss;									
										}else jsonData = jsonData+','+ss;
									}
			    					jsonData = jsonData + ']}';
									
						        	//send back  
									Ext.Ajax.request({
										url:'document.do?method=deleteDocuments',
										params:{
											fileIdList:jsonData
										},
										success:function(response,options){
											var rsp = response.responseText;							
						            		Ext.Msg.alert('Message', rsp);	        		
						            		documentCheckBoxSMSeperate.clearSelections();	        		
											loadDocumentStoreList();						
										},		
										failure:function(){
											Ext.Msg.alert('Message', "Fail to delete documents.");												
										}
									})
								}else{
									Ext.Msg.alert('Message', "Please select document.");																				
								}
							})	        					        	
        				}
		        	});

   var createDocumentGridSmall = function(){
	   var tmp = Ext.create('Ext.grid.Panel',{
			id: 'documentGridSmall',
	        store: documentStoreList,
	        trackMouseOver:true,
	        disableSelection:false,
	        autoScroll:true,
	        autoheight:true,
	        height:240,
	        border:false,
	        autoExpandColumn : 'smallGridHeader',
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
	        	id:'documentGridSmallHeader',
	            header: '&nbsp;',
		        dataIndex: 'fileName',
		        width :'100%',
	            renderer: renderDocumentNameSmall,
				sortable: true
	        }],
	        // paging bar on the bottom to control the page size
	        bbar: new Ext.PagingToolbar({
		    	id:'documentPagingBarSmall',
		        store: documentStoreList,
		        hidden:false
	    	}),
	        // button on the bottom
	       buttons: [uploadDocumentBtnSmall]     
	    });
	   return tmp;
   };
    var documentGridSmall = createDocumentGridSmall();
    
    var documentCheckBoxSMLarge = Ext.create('Ext.selection.CheckboxModel'); 
    var documentCheckBoxSMSeperate = Ext.create('Ext.selection.CheckboxModel');

    var createDocumentGridLarge = function(){
    	var tmp = Ext.create('Ext.grid.Panel',{
    		id: 'documentGridLarge',
            store: documentStore,
            height:240,
            selModel:documentCheckBoxSMLarge,
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
            	id:'document_Large_View',
            	text: 'View',
                renderer: renderDocumentViewLarge,
                width:40,
    			sortable: false
            },{
            	id:'document_Large_Id',
            	text: 'Document Id',
    	        dataIndex: 'fileId',
    			hidden: true
            },{
            	id:'document_Large_Name',
            	text: 'Document Name',
    	        dataIndex: 'fileName',
                renderer: renderDocumentNameLarge,
    			sortable: true
            },{
            	id:'document_Large_Type',
            	text: 'Document Type',
    	        dataIndex: 'fileType',
    			sortable: true
            },{
            	id:'document_Large_Size',
            	text: 'Document Size',
    	        dataIndex: 'fileSize',
    			sortable: true
            },{
            	id:'document_Large_TransId',
            	text: 'Transaction ID',
    	        dataIndex: 'transId',
    			sortable: true
            },{
            	id:'document_Large_domainName',
            	text: 'Domain Name',
    	        dataIndex: 'domainName',
    			sortable: true
            },{
            	id:'document_Large_OperationName',
            	text: 'Operation Name',
    	        dataIndex: 'dataFlowName',
    			sortable: true
            },{
            	id:'document_Large_SubmittedDate',
            	text: 'Submitted Date',
    	        dataIndex: 'updatedDate',
    			sortable: true
            }],

           // paging bar on the bottom
            bbar: new Ext.PagingToolbar({
    	    	id:'documentPagingBarLarge',
    	        store: documentStore,
    	        displayInfo: true,
    	        displayMsg: 'Total:{2}',
    	        emptyMsg: "Total:0"
    	    }),
    	   // new Document button on the top
            buttons: [uploadDocumentBtnLarge,deleteDocumentsBtnLarge]
        });
    	return tmp;
    }

    var documentGridLarge = createDocumentGridLarge();

    var documentGridSeperate = Ext.create('Ext.grid.Panel',{
		id: 'documentGridSeperate',
        store: documentStore,
        selModel:documentCheckBoxSMSeperate,

        // grid columns
        columns:[{
        	id:'document_Seperate_View',
        	text: 'View',
            renderer: renderDocumentViewLarge,
            width:40,
			sortable: false
        },{
        	id:'document_Seperate_Id',
        	text: 'Document Id',
	        dataIndex: 'fileId',
			hidden: true
        },{
        	id:'document_Seperate_Name',
        	text: 'Document Name',
	        dataIndex: 'fileName',
            renderer: renderDocumentNameLarge,
			sortable: true
        },{
        	id:'document_Seperate_Type',
        	text: 'Document Type',
	        dataIndex: 'fileType',
			sortable: true
        },{
        	id:'document_Seperate_Size',
        	text: 'Document Size',
	        dataIndex: 'fileSize',
			sortable: true
        },{
        	id:'document_Seperate_TransId',
        	text: 'Transaction ID',
	        dataIndex: 'transId',
			sortable: true
        },{
        	id:'document_Seperate_domainName',
        	text: 'Domain Name',
	        dataIndex: 'domainName',
			sortable: true
        },{
        	id:'document_Seperate_OperationName',
        	text: 'Operation Name',
	        dataIndex: 'dataFlowName',
			sortable: true
        },{
        	id:'document_Seperate_SubmittedDate',
        	text: 'Submitted Date',
	        dataIndex: 'updatedDate',
	        width:110,
			sortable: true
        }],

        // paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'documentPagingBarSeperate',
	        store: documentStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
	   // new Document button on the top
        buttons: [uploadDocumentBtnSeperate,deleteDocumentsBtnSeperate]
    });

    // base parameter
    var documentParams = {limit:rowNum,page:1,sort:{},start:0,documentName:Ext.getCmp('documentName').getValue(),
	    				transId:Ext.getCmp('transId').getValue(),
	    				domainName:Ext.getCmp('documentDomainCombox').getValue(),
	    				dataFlowName:Ext.getCmp('documentOperationNameCombox').getValue(),
	    				startdt: Ext.getCmp('documentStartdt').getValue()==null ? Ext.getCmp('documentStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('documentStartdt').getValue()), 'Y-m-d'),
	    				enddt: Ext.getCmp('documentEnddt').getValue()==null ? Ext.getCmp('documentEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('documentEnddt').getValue()), 'Y-m-d')};

    // trigger the list data store load
    var loadDocumentStoreList=function(){
    	documentStoreList.load({
	    				callback:function(r,options,success){
    	    				if(success ){
		    				// if success and return record number bigger than switch number then load Grid	    				
							//	loadDocumentStore();
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'document.do?method=getDocumentList',
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
    var loadDocumentStore=function(){
    	documentStore.load({
	    				callback:function(r,options,success){
		    				if(success){
		    				// switch list and grid	    				
								if(r.length > pageSize){
//									Ext.getCmp('documentGridSmall').hide();
//									Ext.getCmp('documentGridLarge').show();
									Ext.getCmp('documentPortlet').removeAll();
								    documentCheckBoxSMLarge = Ext.create('Ext.selection.CheckboxModel'); 
								    documentCheckBoxSMSeperate = Ext.create('Ext.selection.CheckboxModel');
									uploadDocumentBtnLarge = createUploadDocumentBtnLarge();
									deleteDocumentsBtnLarge = createDeleteDocumentsBtnLarge();
									documentGridLarge = createDocumentGridLarge();
									Ext.getCmp('documentPortlet').add(Ext.getCmp('documentGridLarge'));
									documentGridShow = 'large';
								    // trigger the data store load
								    //loadDocumentStore();
//								    Ext.getCmp('viewport').doLayout();
								}
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'document.do?method=getDocumentList',
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
    
    // function to open a document Edit window
    
   var openDocumentEditWin=function(documentId){
		if(!documentEditWin){
			documentIframe =  Ext.create('Ext.ux.IFrame',{  
				id : 'documentIframe'
			});		
			documentEditWin = Ext.create('Ext.window.Window', {
				id:'documentEditWin',
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
				items : [documentIframe]
			});
		}
		if(!documentEditWin.isHidden()){
			Ext.getCmp('documentIframe').load('/Node.Administration/Page/Documents/DocumentsEdit.do?documentid='+documentId);
		}else{
			documentEditWin.on('show',function(){
				Ext.getCmp('documentIframe').load('/Node.Administration/Page/Documents/DocumentsEdit.do?documentid='+documentId);
			});
		}
		documentEditWin.show('documentPortlet');

   };

    // function to open a document Operations Edit window
    
   var openDocumentOperationsEditWin=function(documentName){
		if(!documentOperationsEditWin){
			documentOperationsIframe =  Ext.create('Ext.ux.IFrame',{  
				id : 'documentOperationsIframe'
			});		
			documentOperationsEditWin = Ext.create('Ext.window.Window', {
				id:'documentOperationsEditWin',
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
				items : [documentOperationsIframe]
			});
		}
		if(!documentOperationsEditWin.isHidden()){
			Ext.getCmp('documentOperationsIframe').load('/Node.Administration/Page/Documents/Operations.do?document='+documentName);
		}else{
			documentOperationsEditWin.on('show',function(){
				Ext.getCmp('documentOperationsIframe').load('/Node.Administration/Page/Documents/Operations.do?document='+documentName);
			});
		}
		documentOperationsEditWin.show('documentPortlet');

   };

   var openUploadDocumentWin=function(){
		if(!uploadDocumentWin){
			newDocumentIframe = Ext.create('Ext.ux.IFrame', {  
				id : 'newDocumentIframe'
			});		
			uploadDocumentWin = Ext.create('Ext.window.Window', {
				id:'uploadDocumentWin',
				layout      : 'fit',
				width       : 800,
				height      : 300,
				minWidth 	: 300,
				miniHeight	: 200,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [newDocumentIframe]
			});
		}
		if(!uploadDocumentWin.isHidden()){
			Ext.getCmp('newDocumentIframe').load('/Node.Administration/Page/Documents/Documents.do?act=UPLOAD_FILE');
		}else{
			uploadDocumentWin.on('show',function(){
				Ext.getCmp('newDocumentIframe').load('/Node.Administration/Page/Documents/Documents.do?act=UPLOAD_FILE');
			});
		}
		uploadDocumentWin.show('documentPortlet');

   };
   
	var openDocumentDownloadWin = function(fileId){
		  popWin = open('/Node.Administration/Page/Documents/Download.do?doc='+fileId,'Download','status=no,resizable=no,width=500,height=120');
		  popWin.focus();
	}

	var openOperationDocumentDownloadWin = function(fileId){
		  popWin = open('/Node.Administration/Page/Documents/Download.do?operationDoc='+fileId,'Download','status=no,resizable=no,width=500,height=120');
		  popWin.focus();
	}

	var deleteDocument = function(fileId){
		var docId = fileId;
        Ext.Msg.confirm('Confirm', 'Are you sure you want to delete this document?', function(flag){
				        	if(flag=='yes'){
					        	//send back  
								Ext.Ajax.request({
									url:'document.do?method=delete',
									params:{
										fileId:docId
									},
									success:function(response,options){
										var rsp = response.responseText;							
					            		Ext.Msg.alert('Message', rsp);	        		
										loadDocumentStoreList();						
									},		
									failure:function(){
										Ext.Msg.alert('Message', "Fail to delete document.");												
									}
								})		        					        	
				        	}
				        }
        );
	}
