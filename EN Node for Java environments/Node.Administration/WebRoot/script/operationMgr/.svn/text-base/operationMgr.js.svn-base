	// Create search panel
 var operationDocumentSearch = new Ext.FormPanel({
	 id:"operationDocumentSearch",
     labelWidth: 120, // label settings here cascade unless overridden
     frame:true,
     title: 'Search Document',
	 region:'north',
     bodyStyle:'padding:5px 5px 0',
     width: 400,
     defaults: {width: 400},
     height:160,
     defaultType: 'textfield',

     items: [Ext.create('Ext.form.field.ComboBox',{
 				id:'opDocumentOperationNameCombox',
                fieldLabel: 'Operation Name',
                 store: Ext.create('Ext.data.Store', {
			        remoteSort: true,
					id: 'opDocumentOperationNameComboxStore',					
			        fields: [
							{name: 'operationId', type: 'string', mapping: 'operationId'},
							{name: 'operationName', type: 'string', mapping: 'operationName'}
			        ],
			        proxy: {
			            type: 'ajax',
			            url: 'OperationMgr.do?act=getGrantOperationList',
			            timeout: gridTimeout,
			            reader: {
			                type: 'json',
			                totalProperty: 'total',
			                root: 'results'
			            }
			        }
                 }),
                 listeners: {
                     // delete the previous query in the beforequery event or set
                     // combo.lastQuery = null (this will reload the store the next time it expands)
                     'beforequery': function(qe){
                         delete qe.combo.lastQuery;
                     },
     				 'select':function(combo,record,index){
             			var record = Ext.getCmp('opDocumentOperationNameCombox').getValue();
            	   		if(record!=null && record!=""){            	   		
            	   			var isUpload = false;
            	   			var isGenerate = false;
            	   			var isSubmit = false;
            				var combo = Ext.getCmp('opDocumentOperationNameCombox');
            				var operationName = combo.getValue();
            	   			loadOperationMgrStore();
            	   			operationMgrStore.each(function(r){
            					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
            						if(r.get("upload")=="True"){
            							isUpload = true;
            						}
            						if(r.get("generate")=="True"){
             							isGenerate = true;
            						}
            						if(r.get("submit")=="True"){
            							isSubmit = true;
            						}
            						return;
            					}        					            					
            				},this);
            	   			if(!isUpload){
            	   				uploadFileBtn.disable();
            		   		}else{
            	   				uploadFileBtn.enable();            		   			
            		   		}
            	   			if(!isGenerate){
            	   				generateFileBtn.disable();            	   				
            		   		}else{
            		   			generateFileBtn.enable();            		   			
            		   		}
            	   			if(!isSubmit){
            	   				submitFileBtn.disable();            	   				
            		   		}else{
            		   			submitFileBtn.enable();            		   			
            		   		}
            	   		}                    	 
                     }
                 },
                 allowBlank:true,
                 displayField:'operationName',
                 typeAhead: true,
                 mode: 'remote',
                 triggerAction: 'all',
                 emptyText:'Select an Operation Name...',
                 selectOnFocus:true
             }),{
			        id: 'opDocumentStartdt',
			        fieldLabel: 'Start Date',
			        name: 'opDocumentStartdt',
			        xtype:'datefield',
			        vtype: 'daterange',
			        endDateField: 'opDocumentEnddt' // id of the end date field
			    },{
			        id: 'opDocumentEnddt',
			        fieldLabel: 'End Date',
			        name: 'opDocumentEnddt',
			        xtype:'datefield',
			        vtype: 'daterange',
			        startDateField: 'opDocumentStartdt' // id of the start date field
  			}],
    buttons: [{
				id:'opSetFilter',
				text:'Add Filter Condition',
				type:'submit',
				handler:function (){
    				if(Ext.getCmp('opDocumentOperationNameCombox').getValue() != null && Ext.getCmp('opDocumentOperationNameCombox').getValue() != ""){
    					openOperationMgrFilterConditionWin();		    					
    				}else{
    					Ext.Msg.alert('Message', "Please select operation name.");
    				}
				}
    		},{
	    		id:'opDocumentSearch',
	    		text:'Search',
	    		type:'submit',
	    		handler:function (){

	 				// Get paramters
    				var combo = Ext.getCmp("opDocumentOperationNameCombox");
					var operationName = combo.getValue();
					var operationId = null;
					if(operationName!=null && operationName !=''){
							combo.store.each(function(r){
			 					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
			 						operationId = r.get("operationId");
			 						return;
			 					}        					
		 					
							},this);								
					}else{
						Ext.Msg.alert('Message', "Please select operation name.");
						return;
					}
					operationMgrFilterSetParams.operationId=operationId;
				    operationMgrFilterSetStore.load({
						callback:function(r,options,success){
							if(!success){
								if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'/Node.Administration/Page/Entry/OperationMgr.do?act=getOperationParameterList',
									success:function(response,options){						
										var rsp = response.responseText;							
					            		Ext.Msg.alert('Message', rsp);	        		
									},		
									failure:function(){
										Ext.Msg.alert('Message', "Fail to access database.");												
									}
								})		    					    				
							}else{
		    	 				//create ColModel and store field
		    	 				var column;
		    	 				var newOperationMgrSubmitColModel = null;
		    	 				
	            	   			var isUpload = false;
	            	   			var isGenerate = false;
	            				var combo = Ext.getCmp('opDocumentOperationNameCombox');
	            				var operationName = combo.getValue();
	            	   			loadOperationMgrStore();
	            	   			operationMgrStore.each(function(r){
	            					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
	            						if(r.get("upload")=="True"){
	            							isUpload = true;
	            						}
	            						if(r.get("generate")=="True"){
	             							isGenerate = true;
	            						}
	            						return;
	            					}        					            					
	            				},this);
	            	   			if(isUpload){
			    	 				newOperationMgrSubmitColModel = [ {
			    	 						    						   id:'document_View',
			    	 						    						   text: 'View',
			    	 						    						   renderer: renderTemplateView,
			    	 						    						   width:35,
			    	 						    						   sortable: false
			    	 						    					   },{
			    	 						    					       	id:'document_Id',
			    	 						    					        text: 'Document Id',
			    	 						    					        dataIndex: 'fileId',
			    	 						    							hidden: true
			    	 						    					    },{
			    	 						    					       	id:'document_Download',
			    	 						    					        text: 'Download',
			    	 						    							renderer: renderTemplateDownload,
			    	 						    							width:60,
			    	 						    							sortable: false
			    	 						    					    },{
			    	 						    					    	id:'document_Name',
			    	 						    					        text: 'Uploaded File',
			    	 						    					        dataIndex: 'fileName',
			    	 						    					        width:150,
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_Status',
			    	 						    					        text: 'Uploaded Status',
			    	 						    					        dataIndex: 'documentStatus',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'submit_status',
			    	 						    					        text: 'Submission Status',
			    	 						    					        dataIndex: 'submitStatus',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'submit_status_report',
			    	 						    					        text: 'Submission Status Report',
			    	 						    					        renderer: renderSubmitDownload,
			    	 						    					        width:140,
			    	 						    							sortable: false
			    	 						    					    },{
			    	 						    					    	id:'document_Type',
			    	 						    					        text: 'Document Type',
			    	 						    					        dataIndex: 'fileType',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_Size',
			    	 						    					        text: 'Document Size',
			    	 						    					        dataIndex: 'fileSize',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_TransId',
			    	 						    					        text: 'Transaction ID',
			    	 						    					        dataIndex: 'transId',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_domainName',
			    	 						    					        text: 'Domain Name',
			    	 						    					        dataIndex: 'domainName',
			    	 						    							hidden: true,
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_OperationName',
			    	 						    					        text: 'Data Flow Name',
			    	 						    					        dataIndex: 'dataFlowName',
			    	 						    					        width:200,
			    	 						    							hidden: true,
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_SubmittedDate',
			    	 						    					        text: 'Submitted Date',
			    	 						    					        dataIndex: 'updatedDate',
			    	 						    					        width:120,
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    }];
	            		   		}else if(isGenerate){
			    	 				newOperationMgrSubmitColModel = [  {
			    	 						    						   id:'document_View',
			    	 						    						   text: 'View',
			    	 						    						   renderer: renderTemplateView,
			    	 						    						   width:35,
			    	 						    						   sortable: false
			    	 						    					   },{
			    	 						    					       	id:'document_Id',
			    	 						    					        text: 'Document Id',
			    	 						    					        dataIndex: 'fileId',
			    	 						    							hidden: true
			    	 						    					    },{
			    	 						    					       	id:'document_Download',
			    	 						    					        text: 'Download',
			    	 						    							renderer: renderTemplateDownload,
			    	 						    							width:60,
			    	 						    							sortable: false
			    	 						    					    },{
			    	 						    					    	id:'document_Name',
			    	 						    					        text: 'Generated File',
			    	 						    					        dataIndex: 'fileName',
			    	 						    					        width:150,
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_Status',
			    	 						    					        text: 'Generated Status',
			    	 						    					        dataIndex: 'documentStatus',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'submit_status',
			    	 						    					        text: 'Submission Status',
			    	 						    					        dataIndex: 'submitStatus',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'submit_status_report',
			    	 						    					        text: 'Submission Status Report',
			    	 						    					        renderer: renderSubmitDownload,
			    	 						    					        width:140,
			    	 						    							sortable: false
			    	 						    					    },{
			    	 						    					    	id:'document_Type',
			    	 						    					        text: 'Document Type',
			    	 						    					        dataIndex: 'fileType',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_Size',
			    	 						    					        text: 'Document Size',
			    	 						    					        dataIndex: 'fileSize',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_TransId',
			    	 						    					        text: 'Transaction ID',
			    	 						    					        dataIndex: 'transId',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_domainName',
			    	 						    					        text: 'Domain Name',
			    	 						    					        dataIndex: 'domainName',
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							hidden: true,
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_OperationName',
			    	 						    					        text: 'Data Flow Name',
			    	 						    					        dataIndex: 'dataFlowName',
			    	 						    					        width:200,
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							hidden: true,
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_SubmittedDate',
			    	 						    					        text: 'Submitted Date',
			    	 						    					        dataIndex: 'updatedDate',
			    	 						    					        width:120,
			    	 						    							editor: new Ext.form.TextField({
			    	 						    							}),
			    	 						    							sortable: true
			    	 						    					    }];
	            		   		}else {
			    	 				newOperationMgrSubmitColModel = [  {
			    	 						    						   id:'document_View',
			    	 						    						   text: 'View',
			    	 						    						   renderer: renderTemplateView,
			    	 						    						   width:35,
			    	 						    						   sortable: false
			    	 						    					   },{
			    	 						    					       	id:'document_Id',
			    	 						    					        text: 'Document Id',
			    	 						    					        dataIndex: 'fileId',
			    	 						    							hidden: true
			    	 						    					    },{
			    	 						    					       	id:'document_Download',
			    	 						    					        text: 'Download',
			    	 						    							renderer: renderTemplateDownload,
			    	 						    							width:60,
			    	 						    							sortable: false
			    	 						    					    },{
			    	 						    					    	id:'document_Name',
			    	 						    					        text: 'Document Name',
			    	 						    					        dataIndex: 'fileName',
			    	 						    					        width:150,
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_Status',
			    	 						    					        text: 'Document Status',
			    	 						    					        dataIndex: 'documentStatus',
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'submit_status',
			    	 						    					        text: 'Submission Status',
			    	 						    					        dataIndex: 'submitStatus',
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'submit_status_report',
			    	 						    					        text: 'Submission Status Report',
			    	 						    					        renderer: renderSubmitDownload,
			    	 						    					        width:140,
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_Type',
			    	 						    					        text: 'Document Type',
			    	 						    					        dataIndex: 'fileType',
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_Size',
			    	 						    					        text: 'Document Size',
			    	 						    					        dataIndex: 'fileSize',
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_TransId',
			    	 						    					        text: 'Transaction ID',
			    	 						    					        dataIndex: 'transId',
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_domainName',
			    	 						    					        text: 'Domain Name',
			    	 						    					        dataIndex: 'domainName',
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_OperationName',
			    	 						    					        text: 'Data Flow Name',
			    	 						    					        dataIndex: 'dataFlowName',
			    	 						    					        width:200,
			    	 						    							sortable: true
			    	 						    					    },{
			    	 						    					    	id:'document_SubmittedDate',
			    	 						    					        text: 'Submitted Date',
			    	 						    					        dataIndex: 'updatedDate',
			    	 						    					        width:120,
			    	 						    							sortable: true
			    	 						    					    }];
	            		   		}
		    	 				
		    	 				var newOperationMgrSubmitStoreField = [
	                             	    {name: 'fileId', type: 'string', mapping: 'fileId'},
	                           	        {name: 'fileName', type: 'string', mapping: 'fileName'},
	                           			{name: 'fileType', type: 'string', mapping: 'fileType'},
	                           	        {name: 'fileSize', type: 'string', mapping: 'fileSize'},
	                           			{name: 'transId', type: 'string', mapping: 'transId'},
	                           			{name: 'documentStatus', type: 'string', mapping: 'documentStatus'},
	                           			{name: 'submitStatus', type: 'string', mapping: 'submitStatus'},
	                           			{name: 'submitStatusReport', type: 'string', mapping: 'submitStatusReport'},
	                        			{name: 'domainName', type: 'string', mapping: 'domainName'},
	                           			{name: 'dataFlowName', type: 'string', mapping: 'dataFlowName'},
	                           			{name: 'updatedDate', type: 'string', mapping: 'updatedDate'}
	                                  ];
		    	 				
		    	 				// create columns based on parameter set and filter condition
		    	 				/*operationMgrFilterConditionStore.each(function(r){
		         					if(r.get("conditionName")!=null && r.get("conditionName")!=""){
		         						var conditionName = r.get("conditionName");
				    	 				operationMgrFilterSetStore.each(function(r){
				         					if(r.get("parameterName")!=null && r.get("parameterName")!="" && r.get("parameterName")==conditionName){
				         						var isSet = false;
				         						column = conditionName;
				         						for(var i=0;i<newOperationMgrSubmitColModel.length;i++){
				         							if(newOperationMgrSubmitColModel[i].dataIndex==column){
				         								isSet = true;
				         							}
				         						}
				         						if(!isSet){
				         							newOperationMgrSubmitColModel.push({
				                				    	id:column,
				                				        text: column,
				                				        dataIndex: column,
				                						sortable: true
				                				    });         						         							
				         							newOperationMgrSubmitStoreField.push({name: column, type: 'string', mapping: column});
				         						}
				         					}        					
				         					
				     					},this);								
		         					}        								         					
		     					},this);	*/							

         		           		var j = 0;
         						operationMgrSubmitParams.conditionName = "";
         						operationMgrSubmitParams.conditionSign = "";
         						operationMgrSubmitParams.conditionValue = "";
         						operationMgrSubmitParams.conditionStyle = "";
	    						var conditionName = "";
	    						var conditionSign = "";
	    						var conditionValue = "";
	    						var conditionStyle = "";

         						operationMgrFilterSetStore.each(function(r){
		         					if(r.get("parameterName")!=null && r.get("parameterName")!="" ){
		         						var isSet = false;
		         						column = r.get("parameterName");
		         						for(var i=0;i<newOperationMgrSubmitColModel.length;i++){
		         							if(newOperationMgrSubmitColModel[i].dataIndex==column){
		         								isSet = true;
		         							}
		         						}
		         						if(!isSet){
		         							newOperationMgrSubmitColModel.push({
		                				    	id:column,
		                				        text: column,
		                				        dataIndex: column,
		                						sortable: true
		                				    });         						         							
		         							newOperationMgrSubmitStoreField.push({name: column, type: 'string', mapping: column});
		         						}
		         		        	   // set postback parameters for search
     		    						conditionName = r.get("parameterName");
     		    						conditionSign = "all";
     		    						conditionValue = "";
     		    						conditionStyle = "string";
     		    						
     		    			   			operationMgrFilterConditionStore.each(function(record){
     			    						if(record.get("conditionName")!=null && record.get("conditionName")!="" && record.get("conditionName")==conditionName){
         			    						conditionSign = record.get("conditionSign");
         			    						conditionValue = record.get("conditionValue");
         			    						conditionStyle = record.get("conditionStyle");
         			    						return;
     			    						}
     			    					},this)
     		    						
     		    						if(j==0){
     		        	    				operationMgrSubmitParams.conditionName += conditionName;   							
     		        	    				operationMgrSubmitParams.conditionSign += conditionSign;   							
     			        	    			operationMgrSubmitParams.conditionValue += conditionValue;	        	    					
     		        	    				operationMgrSubmitParams.conditionStyle += conditionStyle;   							
     		        	    				
     		    						}else{
     		    							operationMgrSubmitParams.conditionName += "," + conditionName;
     		        	    				operationMgrSubmitParams.conditionSign += "," + conditionSign;   							
     			        	    			operationMgrSubmitParams.conditionValue += "," + conditionValue;   							
     		        	    				operationMgrSubmitParams.conditionStyle += "," + conditionStyle;   							
     		    						}
     		    						j++;
		         					}        					
		         					
		     					},this);								
 		    	 				// create new json store
		    	 				newOperationMgrSubmitStore = Ext.create('Ext.data.Store',{
		    	 				   storeId:'newOperationMgrSubmitStore',
		    	 			       remoteSort: true,
		    	 			       autoLoad:false,
		    	 			       fields: eval(newOperationMgrSubmitStoreField),
		    	 			       proxy: {
		    	 			            type: 'ajax',
			    	 			        url: 'document.do?method=getOperationMgrDocumentList',
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
				    	 			    	operationMgrSubmitParams.limit = rowNum;
				    	 			    	operationMgrSubmitParams.page = operation.page;
				    	 			    	operationMgrSubmitParams.sort = operation.sort;
				    	 			    	operationMgrSubmitParams.start = operation.start;

		    	 			    	   		operationMgrSubmitParams.dataFlowName=Ext.getCmp('opDocumentOperationNameCombox').getValue();
						    				operationMgrSubmitParams.startdt=Ext.getCmp('opDocumentStartdt').getValue()==null ? Ext.getCmp('opDocumentStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('opDocumentStartdt').getValue()), 'Y-m-d');
						    				operationMgrSubmitParams.enddt=Ext.getCmp('opDocumentEnddt').getValue()==null ? Ext.getCmp('opDocumentEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('opDocumentEnddt').getValue()), 'Y-m-d');
		    	 			 				
		    	 							operation.params = operationMgrSubmitParams;
		    	 						}
		    	 			       },
			    	 			   sortOnLoad: true,
			    	 			   sorters: { property: 'updatedDate', direction : 'DESC' }
		    	 			   });
		    	 				//newOperationMgrSubmitStore.setDefaultSort('updatedDate', 'DESC');
		    	    			/* update store of pagebar*/
		    	 				Ext.getCmp('submitDatePagingBar').bind(newOperationMgrSubmitStore);
		    	 				// update grid structure
		    	 				operationMgrSubmitGrid.reconfigure(newOperationMgrSubmitStore,newOperationMgrSubmitColModel);									

				    			loadOperationMgrSubmitStore();
							}
						}
				    });
	 				    			
				}
     }]
 });

 var operationDetailPanel = new Ext.FormPanel({
	 id:"operationDetailPanel",
	 labelWidth: 120, // label settings here cascade unless overridden
	 frame:true,
	 title: 'File Information',
	 region:'north',
	 bodyStyle:'padding:5px 5px 0',
	 width: 400,
	 defaults: {width: 400},
	 height:250,
	 defaultType: 'textfield',
     items: [{
     	id:'opDocumentName',
        fieldLabel: 'Document Name',
        disabled:true,
        name: 'opDocumentName'
    },{
    	id:'opDocumentType',
        fieldLabel: 'Document Type',
        disabled:true,
        name: 'opDocumentType'
    },{
    	id:'opDocumentSize',
        fieldLabel: 'Document Size',
        disabled:true,
        name: 'opDocumentSize'
    },{
    	id:'opTransactionID',
        fieldLabel: 'Transaction ID',
        disabled:true,
        name: 'opTransactionID'
    },{
    	id:'opDomainName',
        fieldLabel: 'Domain Name',
        disabled:true,
        name: 'opDomainName'
    },{
    	id:'opOperationName',
        fieldLabel: 'Data Flow Name',
        disabled:true,
        name: 'opOperationName'
    },{
    	id:'opSubmittedDate',
        fieldLabel: 'Submitted Date',
        disabled:true,
        name: 'opSubmittedDate'
    }]
 });
	    
 var operationFilterConditionPanel = new Ext.FormPanel({
	 id:"operationFilterConditionPanel",
	 labelWidth: 150, // label settings here cascade unless overridden
	 frame:true,
	 title: 'Filter Parameters',
	 region:'north',
	 bodyStyle:'padding:5px 5px 0',
	 width: 400,
	 defaults: {width: 400},
	 height:100,
	 defaultType: 'textfield',
     items: [new Ext.form.field.ComboBox({
			id:'opFilterConditionOperationNameCombox',
            fieldLabel: 'Filter Condition Name',
			listeners: {
				'select':function(combo,records,index){
    	 			 var isSpace = combo.getValue().search(" ");
    	 			 if(isSpace==-1){
    			         var r = Ext.create('OperationMgrFilterConditionGridRecord',{
    			             conditionName: records[0].get("conditionName"),
    			             conditionSign: '=',
    				         conditionValue: '',
    					     conditionStyle: 'string'
    			         });
    			         operationMgrFilterConditionStore.insert(0, r);
    	 			 }else{
    	 				Ext.Msg.alert('Message', "You can not select a parameter includes space as filter condition!");	
    	 			 }
				},
			     // delete the previous query in the beforequery event or set
			     // combo.lastQuery = null (this will reload the store the next time it expands)
			     'beforequery': function(qe){
			         delete qe.combo.lastQuery;
			     }
			}, 	                    
            store: Ext.create('Ext.data.Store', {
		        remoteSort: true,
		        fields: [
				        {name: 'conditionId', type: 'string', mapping: 'conditionId'},
				        {name: 'conditionName', type: 'string', mapping: 'conditionName'}
		        ],
				listeners: {
					'beforeload':function(store,options){
						var combo = Ext.getCmp('opDocumentOperationNameCombox');
						var operationName = combo.getValue();
						var operationId = null;
						if(operationName!=null && operationName !=''){
          					combo.store.each(function(r){
             					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
             						operationId = r.get("operationId");
             						return;
             					}        					
             					
         					},this);								
						}
						options.params = {operationId:operationId};
					}
				}, 	                    
		        proxy: {
		            type: 'ajax',
		            url: 'OperationMgr.do?act=getFilterConditionNameList',
		            timeout: gridTimeout,
		            reader: {
		                type: 'json',
		                totalProperty: 'total',
		                root: 'results'
		            }
		        }
            }),
            allowBlank:true,
            displayField:'conditionName',
            typeAhead: true,
            mode: 'remote',
            triggerAction: 'all',
            emptyText:'Select a Condition Name...',
            selectOnFocus:true
        })]
 });

 var uploadConfirmBtn = new Ext.Button({
    	   id:'uploadConfirmBtn',
           text: 'Ignore',
           handler : function(){
    	   		var operationName = "";
				var combo = Ext.getCmp('opDocumentOperationNameCombox');
				var operationName = combo.getValue();
				Ext.Msg.wait('loading');
				operationMgrValidateWin.hide();
				Ext.Ajax.request({
					url:'/Node.Administration/Page/Entry/OperationMgr.do?act=saveSubmissionFile&operationName='+operationName,
					success:function(response,options){						
						if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
						var rsp = response.responseText;							
	            		Ext.Msg.alert('Message', rsp);	        		
					},		
					failure:function(){
						if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
						Ext.Msg.alert('Message', "Fail to access database.");												
					}
				})		    					    					   			
           }
       });
 var uploadCancelBtn =  new Ext.Button({
    	   id:'uploadCancelBtn',
           text: 'Cancel',
           handler : function(){
				Ext.Ajax.request({
					url:'/Node.Administration/Page/Entry/OperationMgr.do?act=cancelSubmissionFile',
					success:function(response,options){						
						var rsp = response.responseText;							
	            		Ext.Msg.alert('Message', rsp);	        		
					},		
					failure:function(){
						Ext.Msg.alert('Message', "Fail to access database.");												
					}
				})		    					    					   			      	   		
           }
       })
 var ValidatePanel = new Ext.Panel({
	 id:"ValidatePanel",
	 labelWidth: 120, // label settings here cascade unless overridden
	 frame:true,
	 title: '',
	 region:'south',
	 bodyStyle:'padding:5px 5px 0',
	 width: 800,
	 height:40,
	 defaultType: 'textfield',
	 buttons: [uploadConfirmBtn,uploadCancelBtn]
	 
 })

// open window    
   var openOperationMgrWin=function(){
		if(!operationMgrWin){
			operationMgrWin = Ext.create('Ext.window.Window',{
				id:'operationMgrWin',
				layout      : 'border',
				width       : 900,
				height      : 600,
				minWidth 	: 100,
				miniHeight	: 300,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				items : [operationMgrGrid,operationMgrTemplateGrid,operationMgrValidateGrid]
			});
		}
		operationMgrWin.show('configurationPortlet');
		loadOperationMgrStore();
   };

   var openUploadOperationWin=function(){
		if(!uploadOperationWin){
			newOperationDocumentIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'newOperationDocumentIframe'
			});		
			uploadOperationWin = Ext.create('Ext.window.Window',{
				id:'uploadOperationWin',
				layout      : 'fit',
				width       : 700,
				height      : 200,
				minWidth 	: 300,
				miniHeight	: 200,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				listeners: {
					'beforehide':function(e){
						// get operation						
						loadOperationMgrStore();
					}
			    }, 	                    
				items : [newOperationDocumentIframe]
			});
		}
		if(!uploadOperationWin.isHidden()){
			Ext.getCmp('newOperationDocumentIframe').load('/Node.Administration/Page/Entry/OperationMgr.do?act=uploadOperationFile');
		}else{
			uploadOperationWin.on('show',function(){
				Ext.getCmp('newOperationDocumentIframe').load('/Node.Administration/Page/Entry/OperationMgr.do?act=uploadOperationFile');
			});
		}
		uploadOperationWin.show('configurationPortlet');

  };

  var openUploadOperationTemplateWin=function(id){
		if(!uploadOperationTemplateWin){
			newTemplateDocumentIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'newTemplateDocumentIframe'
			});		
			uploadOperationTemplateWin = Ext.create('Ext.window.Window',{
				id:'uploadOperationTemplateWin',
				layout      : 'fit',
				width       : 700,
				height      : 200,
				minWidth 	: 300,
				miniHeight	: 200,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				listeners: {
					'beforehide':function(e){
						// get template						
						operationMgrTemplateParams.operationId=gridEditingRecord.get("operationId");
						loadOperationMgrTemplateStore();
					}
			    }, 	                    
				items : [newTemplateDocumentIframe]
			});
		}
		if(!uploadOperationTemplateWin.isHidden()){
			Ext.getCmp('newTemplateDocumentIframe').load('/Node.Administration/Page/Entry/OperationMgr.do?act=uploadTemplateFile&id='+id);
		}else{
			uploadOperationTemplateWin.on('show',function(){
				Ext.getCmp('newTemplateDocumentIframe').load('/Node.Administration/Page/Entry/OperationMgr.do?act=uploadTemplateFile&id='+id);
			});
		}
		uploadOperationTemplateWin.show('configurationPortlet');

  };

  var openUploadOperationValidateWin=function(id){
		if(!uploadOperationValidateWin){
			newValidateDocumentIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'newValidateDocumentIframe'
			});		
			uploadOperationValidateWin = Ext.create('Ext.window.Window',{
				id:'uploadOperationValidateWin',
				layout      : 'fit',
				width       : 700,
				height      : 200,
				minWidth 	: 300,
				miniHeight	: 200,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				listeners: {
					'beforehide':function(e){
						// get template						
						operationMgrValidateParams.operationId=gridEditingRecord.get("operationId");
						loadOperationMgrValidateStore();
					}
			    }, 	                    
				items : [newValidateDocumentIframe]
			});
		}
		if(!uploadOperationValidateWin.isHidden()){
			Ext.getCmp('newValidateDocumentIframe').load('/Node.Administration/Page/Entry/OperationMgr.do?act=uploadValidationFile&id='+id);
		}else{
			uploadOperationValidateWin.on('show',function(){
				Ext.getCmp('newValidateDocumentIframe').load('/Node.Administration/Page/Entry/OperationMgr.do?act=uploadValidationFile&id='+id);
			});
		}
		uploadOperationValidateWin.show('configurationPortlet');

  };

  var openOperationMgrSubmitWin=function(){
	    // initial all operation from database
	    loadOperationMgrStore();
		if(!operationMgrSubmitWin){
			operationMgrSubmitWin = Ext.create('Ext.window.Window',{
				id:'operationMgrSubmitWin',
				layout      : 'border',
				width       : 900,
				height      : 600,
				minWidth 	: 100,
				miniHeight	: 300,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				items : [operationDocumentSearch,operationMgrSubmitGrid]
			});
		}
		operationMgrSubmitWin.show('favoriteLinksPortlet');
		loadOperationMgrSubmitStore();
 };

	 var openOperationMgrSubmitViewWin=function(fileId){
			if(!operationMgrSubmitViewWin){
				operationMgrSubmitViewWin = Ext.create('Ext.window.Window',{
					id:'operationMgrSubmitViewWin',
					layout      : 'border',
					width       : 600,
					height      : 600,
					minWidth 	: 100,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					constrainHeader:true,
					listeners: {
						'beforeshow':function(){
						 	var record = operationMgrSubmitCheckBoxSM.getSelection();
							Ext.getCmp('opDocumentName').setValue(record[0].get('fileName'));
							Ext.getCmp('opDocumentType').setValue(record[0].get('fileType'));
							Ext.getCmp('opDocumentSize').setValue(record[0].get('fileSize'));
							Ext.getCmp('opTransactionID').setValue(record[0].get('transId'));
							Ext.getCmp('opDomainName').setValue(record[0].get('domainName'));
							Ext.getCmp('opOperationName').setValue(record[0].get('dataFlowName'));
							Ext.getCmp('opSubmittedDate').setValue(record[0].get('updatedDate'));
							
							var combo = Ext.getCmp('opDocumentOperationNameCombox');
							var operationName = combo.getValue();
							var operationId = null;
							if(operationName!=null && operationName !=''){
             					combo.store.each(function(r){
                					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
                						operationId = r.get("operationId");
                						return;
                					}        					
                					
            					},this);
								
								
								// get template						
								operationMgrTemplateParams.operationId = operationId;
								loadOperationMgrTemplateStore();					
							}
						}
					}, 	                    

					items : [operationDetailPanel,operationMgrViewTemplateGrid]
				});
			}
			operationMgrSubmitViewWin.show('favoriteLinksPortlet');
			//loadOperationMgrSubmitStore();
	};

	 var openOperationMgrFilterSetWin=function(operationId){
		 	// WI 23015
			operationMgrFilterSetParams.operationId=operationId;
			if(!operationMgrFilterSetWin){
				operationMgrFilterSetWin = Ext.create('Ext.window.Window',{
					id:'operationMgrFilterSetWin',
					layout      : 'fit',
					width       : 700,
					height      : 400,
					minWidth 	: 100,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					constrainHeader:true,
					listeners: {
						'beforeshow':function(){
							loadOperationMgrFilterSetStore();
						}
					}, 	                    

					items : [operationMgrFilterSetGrid]
				});
			}else{  // WI 23015
				loadOperationMgrFilterSetStore();
			}
			operationMgrFilterSetWin.show('favoriteLinksPortlet');
	};

	 var openOperationMgrFilterConditionWin=function(){
			if(!operationMgrFilterConditionWin){
				operationMgrFilterConditionWin = Ext.create('Ext.window.Window',{
					id:'operationMgrFilterConditionWin',
					layout      : 'border',
					width       : 700,
					height      : 400,
					minWidth 	: 100,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					constrainHeader:true,
					listeners: {
						'beforeshow':function(){
							//loadOperationMgrFilterConditionStore();
						}
					}, 	                    

					items : [operationFilterConditionPanel,operationMgrFilterConditionGrid]
				});
			}
			operationMgrFilterConditionWin.show('favoriteLinksPortlet');
	};

	  var openOperationMgrViewStyleSheetWin=function(url){
			if(!operationMgrViewStyleSheetWin){
				operationMgrViewStyleSheetIframe = Ext.create('Ext.ux.IFrame',{  
					id : 'operationMgrViewStyleSheetIframe'
				});		
				operationMgrViewStyleSheetWin = Ext.create('Ext.window.Window',{
					id:'operationMgrViewStyleSheetWin',
					layout      : 'fit',
					width       : 800,
					height      : 600,
					minWidth 	: 300,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					constrainHeader:true,
					items : [operationMgrViewStyleSheetIframe]
				});
			}
			if(!operationMgrViewStyleSheetWin.isHidden()){
				Ext.getCmp('operationMgrViewStyleSheetIframe').load(url);
			}else{
				operationMgrViewStyleSheetWin.on('show',function(){
					Ext.getCmp('operationMgrViewStyleSheetIframe').load(url);
				});
			}
			operationMgrViewStyleSheetWin.show('favoriteLinksPortlet');

	 };

/*	  var openOperationMgrValidateWin=function(url){
			if(!operationMgrValidateWin){
				operationMgrValidateIframe = new Ext.ux.IFrameComponent({  
					id : 'operationMgrValidateIframe',  
					url : url,
					region:'center',
					height: 300
				});		
				operationMgrValidateWin = new Ext.Window({
					id:'operationMgrValidateWin',
					layout      : 'border',
					width       : 800,
					height      : 500,
					minWidth 	: 300,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					items : [operationMgrValidateIframe,ValidatePanel]
				});
			}else{
				// refresh data
				Ext.get('iframe-operationMgrValidateIframe').dom.src=url;
			}
			operationMgrValidateWin.show('favoriteLinksPortlet');

	 };
*/
	 
	  var openOperationMgrValidateWin=function(url){
			if(!operationMgrValidateWin){					
				operationMgrValidateIframe = Ext.create('Ext.ux.IFrame',{  
					id          : 'mifp_1',
					loadMask  	: false,
					title		: 'Upload',
					region		: 'center',
					width       : 800,
					height		: 300
//				    listeners: {
//						'beforeshow' : function(){
//									var doc = this.getDocument();
//									if(doc != null){
//										console.log(doc.getElementById("invalid"));
//										var invalid = doc.getElementById("invalid");
//										if(invalid != null){
//											Ext.getCmp('uploadConfirmBtn').show();
//											Ext.getCmp('uploadCancelBtn').show();											
//										}else{
//											Ext.getCmp('uploadConfirmBtn').hide();
//											Ext.getCmp('uploadCancelBtn').hide();
//										}
//										operationMgrValidateWin.doLayout();
//									}
//								}
//					}
				});	
				operationMgrValidateWin = Ext.create('Ext.window.Window',{
					id:'operationMgrValidateWin',
					layout      : 'border',
					width       : 800,
					height      : 500,
					minWidth 	: 300,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					constrainHeader:true,
					items : [operationMgrValidateIframe,ValidatePanel]
				});
			}
			if(!operationMgrValidateWin.isHidden()){
				Ext.getCmp('mifp_1').load(url);
			}else{
				operationMgrValidateWin.on('show',function(){
					Ext.getCmp('mifp_1').load(url);
				});
			}
			operationMgrValidateWin.show('favoriteLinksPortlet');

	 };
	 
	  var openOperationMgrGenerateWin=function(){
			if(!operationMgrGenerateWin){
				operationMgrGenerateWin = Ext.create('Ext.window.Window',{
					id:'operationMgrGenerateWin',
					layout      : 'fit',
					width       : 600,
					height      : 400,
					minWidth 	: 300,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					constrainHeader:true,
					items : [operationMgrGenerateParameterGrid]
				});
			}
			operationMgrGenerateWin.show('favoriteLinksPortlet');

	 };

	 var openOperationMgrManagerWin=function(transId){
			if(!operationMgrManagerWin){
				operationMgrManagerWin = Ext.create('Ext.window.Window',{
					id:'operationMgrManagerWin',
					layout      : 'border',
					width       : 700,
					height      : 400,
					minWidth 	: 100,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain: true,
					constrainHeader:true,
					listeners: {
						'beforeshow':function(){
					
						}
					}, 	                    

					items : [operationMgrManagerGrid]
				});
			}
			operationMgrManagerParams.transId=transId;
			loadOperationMgrManagerStore();
			operationMgrManagerWin.show('favoriteLinksPortlet');
	};

	// base parameter
   var operationMgrParams = {limit:rowNum,page:1,sort:{},start:0};
   var operationMgrTemplateParams = {limit:rowNum,page:1,sort:{},start:0,operationId:""};
   var operationMgrFilterSetParams = {limit:rowNum,page:1,sort:{},start:0,operationId:""};
   var operationMgrGenerateParameterParams = {limit:rowNum,page:1,sort:{},start:0,operationId:""};
   var operationMgrFilterConditionParams = {limit:rowNum,page:1,sort:{},start:0,operationId:""};
   var operationMgrValidateParams = {limit:rowNum,page:1,sort:{},start:0,operationId:""};
   var operationMgrManagerParams = {limit:rowNum,page:1,sort:{},start:0,transId:""};
   var operationMgrSubmitParams = {limit:rowNum,page:1,sort:{},start:0,documentName:"",
	    				transId:"",
	    				domainName:"",
	    				dataFlowName:Ext.getCmp('opDocumentOperationNameCombox').getValue(),
	    				conditionName:"",
	    				conditionSign:"",
	    				conditionValue:"",
	    				conditionStyle:"",
	    				startdt: Ext.getCmp('opDocumentStartdt').getValue()==null ? Ext.getCmp('opDocumentStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('opDocumentStartdt').getValue()), 'Y-m-d'),
	    	    		enddt: Ext.getCmp('opDocumentEnddt').getValue()==null ? Ext.getCmp('opDocumentEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('opDocumentEnddt').getValue()), 'Y-m-d')};

   // create the Data Store
   var operationMgrStore = Ext.create('Ext.data.Store', {
	   storeId:'operationMgrStore',
       remoteSort: true,
       fields: [
	        {name: 'operationId', type: 'string', mapping: 'operationId'},
	        {name: 'operationName', type: 'string', mapping: 'operationName'},
	        {name: 'operationVersion', type: 'string', mapping: 'operationVersion'},
	        {name: 'dataFlow', type: 'string', mapping: 'dataFlow'},
	        {name: 'upload', type: 'string', mapping: 'upload'},
	        {name: 'generate', type: 'string', mapping: 'generate'},
	        {name: 'submit', type: 'string', mapping: 'submit'},
	        {name: 'URL', type: 'string', mapping: 'URL'},
	        {name: 'userName', type: 'string', mapping: 'userName'},
	        {name: 'password', type: 'string', mapping: 'password'},
	        {name: 'domainName', type: 'string', mapping: 'domainName'},
	        {name: 'getStatus', type: 'string', mapping: 'getStatus'},
	        {name: 'getStatusComplete', type: 'string', mapping: 'getStatusComplete'},
	        {name: 'getStatusError', type: 'string', mapping: 'getStatusError'}
       ],
       proxy: {
           type: 'ajax',
           url: 'OperationMgr.do?act=getOperationList',
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
	    	   operationMgrParams.limit = rowNum;
	    	   operationMgrParams.page = operation.page;
	    	   operationMgrParams.sort = operation.sort;
	    	   operationMgrParams.start = operation.start;

		    	operation.params = operationMgrParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'operationId', direction : 'ASC' }
   });
//   operationMgrStore.setDefaultSort('operationId', 'ASC');

   // create the Data Store
   var operationMgrValidateStore = Ext.create('Ext.data.Store', {
   	   storeId:'operationMgrValidateStore',
       remoteSort: true,
       fields: [
	        {name: 'validateFileId', type: 'string', mapping: 'validateFileId'},
	        {name: 'validateFileName', type: 'string', mapping: 'validateFileName'}
       ],
       proxy: {
           type: 'ajax',
           url: 'OperationMgr.do?act=getOperationValidateList',
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
	    	   operationMgrValidateParams.limit = rowNum;
	    	   operationMgrValidateParams.page = operation.page;
	    	   operationMgrValidateParams.sort = operation.sort;
	    	   operationMgrValidateParams.start = operation.start;
	    	   
				operation.params = operationMgrValidateParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'validateFileId', direction : 'ASC' }
   });
//   operationMgrValidateStore.setDefaultSort('validateFileId', 'ASC');

   // create the Data Store
   var operationMgrTemplateStore = Ext.create('Ext.data.Store',{
	   storeId:'operationMgrTemplateStore',
       remoteSort: true,
       fields: [
	        {name: 'templateId', type: 'string', mapping: 'templateId'},
	        {name: 'templateName', type: 'string', mapping: 'templateName'},
	        {name: 'transformSuffix', type: 'string', mapping: 'transformSuffix'}
       ],
       proxy: {
           type: 'ajax',
           url: 'OperationMgr.do?act=getOperationTemplateList',
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
	    	   operationMgrTemplateParams.limit = rowNum;
	    	   operationMgrTemplateParams.page = operation.page;
	    	   operationMgrTemplateParams.sort = operation.sort;
	    	   operationMgrTemplateParams.start = operation.start;

    	   		operation.params = operationMgrTemplateParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'templateId', direction : 'ASC' }
   });
//   operationMgrTemplateStore.setDefaultSort('templateId', 'ASC');

   // create the Data Store
   var operationMgrFilterSetStore = Ext.create('Ext.data.Store',{
	   storeId:'operationMgrFilterSetStore',
       remoteSort: true,
       fields: [
	        {name: 'parameterId', type: 'string', mapping: 'parameterId'},
	        {name: 'parameterName', type: 'string', mapping: 'parameterName'},
	        {name: 'parameterValue', type: 'string', mapping: 'parameterValue'}
       ],
       proxy: {
           type: 'ajax',
           url: 'OperationMgr.do?act=getOperationParameterList',
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
	    	   operationMgrFilterSetParams.limit = rowNum;
	    	   operationMgrFilterSetParams.page = operation.page;
	    	   operationMgrFilterSetParams.sort = operation.sort;
	    	   operationMgrFilterSetParams.start = operation.start;
 				
				operation.params = operationMgrFilterSetParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'parameterId', direction : 'ASC' }
   });
//   operationMgrFilterSetStore.setDefaultSort('parameterId', 'ASC');

   // create the Data Store
   var operationMgrFilterConditionStore = Ext.create('Ext.data.Store',{
   	   storeId:'operationMgrFilterConditionStore',
       remoteSort: true,
       fields: [
	        {name: 'conditionName', type: 'string', mapping: 'conditionName'},
	        {name: 'conditionSign', type: 'string', mapping: 'conditionSign'},
	        {name: 'conditionValue', type: 'string', mapping: 'conditionValue'},
	        {name: 'conditionStyle', type: 'string', mapping: 'conditionStyle'}
       ],
       proxy: {
           type: 'ajax',
           url: 'OperationMgr.do?act=getOperationConditionList',
           timeout: gridTimeout,
           listeners: {
				'beforeload':function(proxy,para){
   	   			if(para != null)
   	   				return false;
				}
   		   }, 	                    
           reader: {
               type: 'json',
               totalProperty: 'total',
               root: 'results'
           }
       },
       listeners:{
    	   'beforeload':function(store, operation, eOpts){
	    		// Must pass page refresh parameters
	    	   operationMgrFilterSetParams.limit = rowNum;
	    	   operationMgrFilterSetParams.page = operation.page;
	    	   operationMgrFilterSetParams.sort = operation.sort;
	    	   operationMgrFilterSetParams.start = operation.start;
 				
				operation.params = operationMgrFilterSetParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'conditionName', direction : 'ASC' }
   });
//   operationMgrFilterConditionStore.setDefaultSort('conditionName', 'ASC');

   // create the Data Store
   var operationMgrGenerateParameterStore = Ext.create('Ext.data.Store',{
   	   storeId:'operationMgrGenerateParameterStore',
       remoteSort: true,
       fields: [
	        {name: 'generateParameterName', type: 'string', mapping: 'generateParameterName'},
	        {name: 'generateParameterValue', type: 'string', mapping: 'generateParameterValue'}
       ],
       proxy: {
           type: 'ajax',
           url: 'OperationMgr.do?act=getOperationWebServiceParameterList',
           timeout: gridTimeout,
           listeners: {
				'beforeload':function(proxy,para){
   	   			if(para != null)
   	   				return false;
				}
   		   }, 	                    
           reader: {
               type: 'json',
               totalProperty: 'total',
               root: 'results'
           }
       },
       listeners:{
    	   'beforeload':function(store, operation, eOpts){
	    		// Must pass page refresh parameters
	    	   operationMgrGenerateParameterParams.limit = rowNum;
	    	   operationMgrGenerateParameterParams.page = operation.page;
	    	   operationMgrGenerateParameterParams.sort = operation.sort;
	    	   operationMgrGenerateParameterParams.start = operation.start;
 				
				operation.params = operationMgrGenerateParameterParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'generateParameterName', direction : 'ASC' }
   });
//   operationMgrGenerateParameterStore.setDefaultSort('generateParameterName', 'ASC');

   // create the Data Store
   var operationMgrSubmitStoreField = [
                           	        {name: 'fileId', type: 'string', mapping: 'fileId'},
                        	        {name: 'fileName', type: 'string', mapping: 'fileName'},
                        			{name: 'fileType', type: 'string', mapping: 'fileType'},
                        	        {name: 'fileSize', type: 'string', mapping: 'fileSize'},
                        			{name: 'transId', type: 'string', mapping: 'transId'},
                        			{name: 'documentStatus', type: 'string', mapping: 'documentStatus'},
                           			{name: 'submitStatus', type: 'string', mapping: 'submitStatus'},
                           			{name: 'submitStatusReport', type: 'string', mapping: 'submitStatusReport'},
                        			{name: 'domainName', type: 'string', mapping: 'domainName'},
                        			{name: 'dataFlowName', type: 'string', mapping: 'dataFlowName'},
                        			{name: 'updatedDate', type: 'string', mapping: 'updatedDate'}
                               ];
   var operationMgrSubmitStore = Ext.create('Ext.data.Store',{
	   storeId:'operationMgrSubmitStore',
       remoteSort: true,
       autoLoad:false,
       fields: eval(operationMgrSubmitStoreField),
       proxy: {
           type: 'ajax',
           url: 'document.do?method=getOperationMgrDocumentList',
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
				operationMgrSubmitParams.limit = rowNum;
				operationMgrSubmitParams.page = operation.page;
				operationMgrSubmitParams.sort = operation.sort;
				operationMgrSubmitParams.start = operation.start;

       			operationMgrSubmitParams.dataFlowName=Ext.getCmp('opDocumentOperationNameCombox').getValue();
				operationMgrSubmitParams.startdt=Ext.getCmp('opDocumentStartdt').getValue()==null ? Ext.getCmp('opDocumentStartdt').getValue():Ext.Date.format(new Date(Ext.getCmp('opDocumentStartdt').getValue()), 'Y-m-d');
				operationMgrSubmitParams.enddt=Ext.getCmp('opDocumentEnddt').getValue()==null ? Ext.getCmp('opDocumentEnddt').getValue():Ext.Date.format(new Date(Ext.getCmp('opDocumentEnddt').getValue()), 'Y-m-d');
 				
				operation.params = operationMgrSubmitParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'updatedDate', direction : 'DESC' }
   });
   
//   operationMgrSubmitStore.setDefaultSort('updatedDate', 'DESC');
   var newOperationMgrSubmitStore = operationMgrSubmitStore;

   // Fix the bug of Extjs when load the same store all events will be triggered 
	var operationNameTestStore = Ext.create('Ext.data.Store',{
        remoteSort: true,
        fields: [
			{name: 'operationId', type: 'string', mapping: 'operationId'},
			{name: 'operationName', type: 'string', mapping: 'operationName'}
        ],
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getOperatioIdNameList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        }
    });
	
	var operationNameStore = Ext.create('Ext.data.Store',{
        remoteSort: true,
        fields: [
			{name: 'operationId', type: 'string', mapping: 'operationId'},
			{name: 'operationName', type: 'string', mapping: 'operationName'}
        ],
        proxy: {
            type: 'ajax',
            url: 'operation.do?method=getOperatioIdNameList',
            timeout: gridTimeout,
            reader: {
                type: 'json',
                totalProperty: 'total',
                root: 'results'
            }
        }
    });

   // create the Data Store
   var operationMgrManagerStore = Ext.create('Ext.data.Store',{
   	   storeId:'operationMgrManagerStore',
       remoteSort: true,
       fields: [
	        {name: 'submitId', type: 'string', mapping: 'submitId'},
	        {name: 'operationName', type: 'string', mapping: 'operationName'},
	        {name: 'statusCD', type: 'string', mapping: 'statusCD'},
	        {name: 'submitURL', type: 'string', mapping: 'submitURL'},
	        {name: 'submitDate', type: 'string', mapping: 'submitDate'},	        
	        {name: 'version', type: 'string', mapping: 'version'},
	        {name: 'transId', type: 'string', mapping: 'transId'},
	        {name: 'supplyTransId', type: 'string', mapping: 'supplyTransId'},
	        {name: 'dataFlow', type: 'string', mapping: 'dataFlow'}
       ],
       proxy: {
           type: 'ajax',
           url: 'OperationMgr.do?act=getOperationManagerTableList',
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
	    	   operationMgrManagerParams.limit = rowNum;
	    	   operationMgrManagerParams.page = operation.page;
	    	   operationMgrManagerParams.sort = operation.sort;
	    	   operationMgrManagerParams.start = operation.start;

	    	   operation.params = operationMgrManagerParams;
			}
       },
	   sortOnLoad: true,
	   sorters: { property: 'submitId', direction : 'DESC' }
   });
//   operationMgrManagerStore.setDefaultSort('submitId', 'DESC');
	
	
   var operationAddBtn = new Ext.Button({
	   id:'operationAddBtn',
       text: 'Add Operation',
       handler : function(){
	   		var isNewRecord = false;
	   		operationMgrGrid.store.each(function(r){
				if(r.get("operationName") == null || r.get("operationName") == ''){
					isNewRecord = true;
					return;
				}        					
				
			},this);
	   		if(!isNewRecord){
				Ext.Ajax.request({
					url:'/Node.Administration/Page/Entry/OperationMgr.do?act=addOperationList',
					success:function(response,options){						
					   operationMgrStore.load();
					},		
					failure:function(){
						Ext.Msg.alert('Message', "Fail to access database.");												
					}
				});		    					    					   			
	   		}
       }
   });

   var parameterAddBtn = new Ext.Button(
           {
	  		id:'parameterAddBtn',
	 		text:'Add Parameter',
	 		type:'submit',
	 		handler:function (){
	   			var operationId = null;
				var rcds = operationMgrCheckBoxSM.getSelection();
				if(rcds==null || rcds==""){
					Ext.Msg.alert('Message', "Please select one operation!");																					   					
				}else if(rcds.length>1){
					Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
				}else{
					operationId = rcds[0].get("operationId");		    					
 			   		var isNewRecord = false;
 			   		operationMgrFilterSetStore.each(function(r){
 						if(r.get("parameterName") == null || r.get("parameterName") == ''){
 							isNewRecord = true;
 							return;
 						}        					
 						
 					},this);
 			   		if(!isNewRecord){
	 					Ext.Ajax.request({
	 						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=addParameterList',
	 						params:{
	 							operationId:operationId
	 						},
	 						success:function(response,options){						
	 							operationMgrFilterSetStore.load();
	 						},		
	 						failure:function(){
	 							Ext.Msg.alert('Message', "Fail to access database.");												
	 						}
	 					});		    					    					   							
 			   		}
				}
			}
        });

   var generateGirdParameterAddBtn = new Ext.Button(
           {
	  		id:'generateGirdParameterAddBtn',
	 		text:'Add Parameter',
	 		type:'submit',
	 		handler:function (){
        	   var r = new operationMgrGenerateParameterGridRecord({
        		   generateParameterName: '',
        		   generateParameterValue: ''
        	   });
        	   operationMgrGenerateParameterStore.insert(0, r);

			}
        });
   
   var uploadOperationBtn = new Ext.Button({id:'uploadOperationBtn',
		text:'Upload Operation Management File',
		type:'submit',
		handler:function (){
	   		openUploadOperationWin();
	   }
	});
  
   var uploadTemplateBtn = new Ext.Button({id:'uploadTemplateBtn',
		text:'Upload StyleSheet',
		type:'submit',
		handler:function (){
	   				var records = operationMgrCheckBoxSM.getSelection();
	   				if(records==null || records==""){
						Ext.Msg.alert('Message', "Please select one operation!");																					   					
	   				}else if(records.length>1){
						Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
	   				}else{
		   				openUploadOperationTemplateWin(gridEditingRecord.get("operationId"));			    			   					
	   				}
				}
	});

   var uploadValidateBtn = new Ext.Button({id:'uploadValidateBtn',
		text:'Upload Validation File',
		type:'submit',
		handler:function (){
	   				var records = operationMgrCheckBoxSM.getSelection();
	   				var count = operationMgrValidateStore.getCount();
	   				if(records==null || records==""){
						Ext.Msg.alert('Message', "Please select one operation!");																					   					
	   				}else if(records.length>1){
						Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
	   				}else if(count!=0){
						Ext.Msg.alert('Message', "You only can upload one validation file!");																					   						   					
	   				}else{
		   				openUploadOperationValidateWin(gridEditingRecord.get("operationId"));			    			   					
	   				}
				}
	});

   var uploadFileBtn = new Ext.Button({id:'uploadFileBtn',
		text:'Upload',
		type:'submit',
		handler:function (){
			var record = Ext.getCmp('opDocumentOperationNameCombox').getValue();
	   		if(record==null || record==""){
				Ext.Msg.alert('Message', "Please select one operation!");																					   					
	   		}else{
	   			var isUpload = false;
				var combo = Ext.getCmp('opDocumentOperationNameCombox');
				var operationName = combo.getValue();
				var operationId = null;
	   			loadOperationMgrStore();
	   			operationMgrStore.each(function(r){
					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
						if(r.get("upload")=="True"){
							operationId = r.get("operationId");
							isUpload = true;
							return;							
						}
					}        					
					
				},this);
	   			if(isUpload){
					// upload file						
					openOperationMgrValidateWin("/Node.Administration/Page/Entry/OperationMgr.do?act=uploadSubmissionFile&operationId="+operationId+"&operationName="+operationName);			   							   			
		   		}else{
					Ext.Msg.alert('Message', "The " + operationName + " does not support Upload!");																					   					
		   		}
	   		}
	   	}
	});
   
   var generateFileBtn = new Ext.Button({id:'generateFileBtn',
		text:'Generate',
		type:'submit',
		handler:function (){
			var record = Ext.getCmp('opDocumentOperationNameCombox').getValue();
	   		if(record==null || record==""){
				Ext.Msg.alert('Message', "Please select one operation!");																					   					
	   		}else{
	   			var isGenerate = false;
				var combo = Ext.getCmp('opDocumentOperationNameCombox');
				var operationName = combo.getValue();
				var operationId = null;
	   			loadOperationMgrStore();
	   			operationMgrStore.each(function(r){
					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
						if(r.get("generate")=="True"){
							operationId = r.get("operationId");
							isGenerate = true;
							return;							
						}
					}        					
					
				},this);
	   			if(isGenerate){
					// generate file						
	   				operationMgrGenerateParameterParams.operationId=operationId;
	   				operationMgrGenerateParameterStore.load();
					openOperationMgrGenerateWin('');					
		   		}else{
					Ext.Msg.alert('Message', "The " + operationName + " does not support Generate!");																					   					
		   		}
	   		}
	   }
	});
   
   var submitFileBtn = new Ext.Button({id:'submitFileBtn',
		text:'Submit',
		type:'submit',
		handler:function (){
		 		var records = operationMgrSubmitCheckBoxSM.getSelection();
		   		if(records==null || records==""){
					Ext.Msg.alert('Message', "Please select one file!");																					   					
		   		}else{
					var combo = Ext.getCmp('opDocumentOperationNameCombox');
					var operationName = combo.getValue();
					
					if(operationName==null || operationName==""){
						Ext.Msg.alert('Message', "Please select operation!");
					}else{
						var paraNameList = ['fileId',
		                                    'fileName'];
						var jsonData = '';
						for(var i = 0; i < records.length; i++){
							if(i==0) jsonData = createJsonRecord(records[i],paraNameList);
							else jsonData = jsonData + ',' + createJsonRecord(records[i],paraNameList);		
						}
						var submissionFileListJson = '\"submissionFileList\":[' + jsonData + ']';
						var dataStr = '{' + submissionFileListJson + '}';

						Ext.Msg.wait('loading');
						Ext.Ajax.request({
							url:'/Node.Administration/Page/Entry/OperationMgr.do?act=submitFile',
							timeout:gridTimeout,
							params:{
								operationName:operationName,
								dataStr:dataStr
							},
							success:function(response,options){						
								if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								var rsp = response.responseText;							
			            		Ext.Msg.alert('Message', rsp);	        		
							},		
							failure:function(response,options){						
								if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								var rsp = response.responseText;							
			            		Ext.Msg.alert('Message', rsp);	        		
							}
						})		    					    					   					   									
					}
		   		}
	   }
	});

   var operationDeleteBtn = new Ext.Button({
	   	id:'operationDeleteBtn',
		text:'Delete Operation',
		type:'submit',
		handler:function (){
			Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these operations ?', function(flag){
				if(flag == 'yes'){
					// get records
					var records = operationMgrCheckBoxSM.getSelection();
					var paraNameList = ['operationId',
	                                      'operationName',
	                                      'operationVersion',
	                                      'dataFlow',
	                                      'upload',
	                                      'generate',
	                                      'submit',
	                                      'URL',
	                                      'userName',
	                                      'password'];
					var jsonData = '';
					if(records!=null && records !=''){
						for(var i = 0; i < records.length; i++){
							if(i==0) jsonData = createJsonRecord(records[i],paraNameList);
							else jsonData = jsonData + ',' + createJsonRecord(records[i],paraNameList);		
						}
						var operationListJson = '\"operationList\":[' + jsonData + ']';
						var dataStr = '{' + operationListJson + '}';
						Ext.Ajax.request({
							url:'/Node.Administration/Page/Entry/OperationMgr.do?act=deleteOperationList',
							params:{
								dataString:dataStr
							},
							success:function(response,options){						
								Ext.Msg.alert('Message', "Delete Operation successfully.");												
								operationMgrStore.load();
							},		
							failure:function(){
								Ext.Msg.alert('Message', "Fail to access database.");												
							}
						})		    					    				
					}else{
						Ext.Msg.alert('Message', "Please select operation!");																				
					}					
				}
			})	        					        	
		}
	});

   var templateDeleteBtn = new Ext.Button({
	   	id:'templateDeleteBtn',
		text:'Delete Template',
		type:'submit',
		handler:function (){
			var rcds = operationMgrCheckBoxSM.getSelection();
			if(rcds==null || rcds==""){
				Ext.Msg.alert('Message', "Please select one operation!");																					   					
			}else if(rcds.length>1){
				Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
			}else{
				Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these templates ?', function(flag){
					if(flag == 'yes'){
						var id = gridEditingRecord.get("operationId");
						// get records
						var records = operationMgrTemplateCheckBoxSM.getSelection();
						var paraNameList = ['templateId',
		                                    'templateName',
		                                    'transformSuffix'
						                    ];
						var jsonData = '';
						if(records != null && records !=''){
							for(var i = 0; i < records.length; i++){
								if(i==0) jsonData = createJsonRecord(records[i],paraNameList);
								else jsonData = jsonData + ',' + createJsonRecord(records[i],paraNameList);		
							}
							var operationTemplateListJson = '\"templateList\":[' + jsonData + ']';
							var operationIdJson = '\"operationId\":' + id + '';
							var dataStr = '{' + operationIdJson + ',' + operationTemplateListJson + '}';
							Ext.Ajax.request({
								url:'/Node.Administration/Page/Entry/OperationMgr.do?act=deleteTemplateList&id='+id,
								params:{
									dataString:dataStr
								},
								success:function(response,options){						
									Ext.Msg.alert('Message', "Delete Template file successfully.");												
									// get template						
									operationMgrTemplateParams.operationId=id;
									loadOperationMgrTemplateStore();
								},		
								failure:function(){
									Ext.Msg.alert('Message', "Fail to access database.");												
								}
							});		    					    				
						}else{
							Ext.Msg.alert('Message', "Please select template!");																				
						}					
					}
				});	        					        	
			}
		}
	});

   var validateDeleteBtn = new Ext.Button({
	   	id:'validateDeleteBtn',
		text:'Delete Validation File',
		type:'submit',
		handler:function (){
			var rcds = operationMgrCheckBoxSM.getSelection();
			if(rcds==null || rcds==""){
				Ext.Msg.alert('Message', "Please select one operation!");																					   					
			}else if(rcds.length>1){
				Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
			}else{
				Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these validation files ?', function(flag){
					if(flag == 'yes'){
						var id = gridEditingRecord.get("operationId");
						// get records
						var records = operationMgrValidateCheckBoxSM.getSelection();
						var paraNameList = ['validateFileId',
		                                      'validateFileName'];
						var jsonData = '';
						if(records != null && records !=''){
							for(var i = 0; i < records.length; i++){
								if(i==0) jsonData = createJsonRecord(records[i],paraNameList);
								else jsonData = jsonData + ',' + createJsonRecord(records[i],paraNameList);		
							}
							var operationValidateListJson = '\"validateList\":[' + jsonData + ']';
							var operationIdJson = '\"operationId\":' + id + '';
							var dataStr = '{' + operationIdJson + ',' + operationValidateListJson + '}';
							Ext.Ajax.request({
								url:'/Node.Administration/Page/Entry/OperationMgr.do?act=deleteValidateList&id='+id,
								params:{
									dataString:dataStr
								},
								success:function(response,options){						
									Ext.Msg.alert('Message', "Delete Validation file successfully.");												
									// get template						
									operationMgrValidateParams.operationId=id;
									loadOperationMgrValidateStore();
								},		
								failure:function(){
									Ext.Msg.alert('Message', "Fail to access database.");												
								}
							})		    					    				
						}else{
							Ext.Msg.alert('Message', "Please select validation file!");																				
						}					
					}
				})	        					        	
			}
		}
	});

   var parameterDeleteBtn = new Ext.Button({
	   	id:'parameterDeleteBtn',
		text:'Delete Parameter',
		type:'submit',
		handler:function (){
			var operationId = null;
			var rcds = operationMgrCheckBoxSM.getSelection();
			if(rcds==null || rcds==""){
				Ext.Msg.alert('Message', "Please select one operation!");																					   					
			}else if(rcds.length>1){
				Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
			}else{
				operationId = rcds[0].get("operationId");		    					
				Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these parameters ?', function(flag){
					if(flag == 'yes'){
						// get records
						var records = operationMgrFilterSetCheckBoxSM.getSelection();
						var paraNameList = ['parameterId',
		                                    'parameterName',
		                                    'parameterValue'];
						var jsonData = '';
						if(records != null && records !=''){
							for(var i = 0; i < records.length; i++){
								if(i==0) jsonData = createJsonRecord(records[i],paraNameList);
								else jsonData = jsonData + ',' + createJsonRecord(records[i],paraNameList);		
							}
							var operationParameterListJson = '\"parameterList\":[' + jsonData + ']';
							var operationIdJson = '\"operationId\":' + operationId + '';
							var dataStr = '{' + operationIdJson + ',' + operationParameterListJson + '}';
							Ext.Ajax.request({
								url:'/Node.Administration/Page/Entry/OperationMgr.do?act=deleteParameterList',
								params:{
									dataString:dataStr
								},
								success:function(response,options){						
									Ext.Msg.alert('Message', "Delete parameter successfully.");												
									// get template						
									operationMgrFilterSetParams.operationId=operationId;
									loadOperationMgrFilterSetStore();
								},		
								failure:function(){
									Ext.Msg.alert('Message', "Fail to access database.");												
								}
							})		    					    				
						}else{
							Ext.Msg.alert('Message', "Please select parameter!");																				
						}					
					}
				})	        					        	
			}
		}
	});

   var conditionDeleteBtn = new Ext.Button({
		id:'conditionDeleteBtn',
		text:'Remove Selected',
		type:'submit',
		handler:function (){
			Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these filter conditions ?', function(flag){
				if(flag == 'yes'){
    				// get records
					var records = operationMgrFilterConditionCheckBoxSM.getSelection();
					if(records!=null && records !=''){
						for(var i = 0; i < records.length; i++){
							operationMgrFilterConditionStore.remove(records[i]);									 
						}
					}else{
						Ext.Msg.alert('Message', "Please select one condition.");																				
					}        							
				}
			})	        					        	
		}
	});

   var generateGirdParameterDeleteBtn = new Ext.Button({
		id:'generateGirdParameterDeleteBtn',
		text:'Remove Selected',
		type:'submit',
		handler:function (){
			Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these Parameters ?', function(flag){
				if(flag == 'yes'){
   				// get records
					var records = operationMgrGenerateParameterCheckBoxSM.getSelection();
					if(records!=null && records !=''){
						for(var i = 0; i < records.length; i++){
							operationMgrGenerateParameterStore.remove(records[i]);									 
						}
					}else{
						Ext.Msg.alert('Message', "Please select one condition.");																				
					}        							
				}
			})	        					        	
		}
	});

   var generateGirdParameterSendBtn = new Ext.Button({
	   	id:'generateGirdParameterSendBtn',
		text:'Generate',
		type:'submit',
		handler:function (){
			// get records
	  		var paraNameList = [  'generateParameterName',
	                              'generateParameterValue'];
			
			var combo = Ext.getCmp("opDocumentOperationNameCombox");
			var operationName = combo.getValue();
	
			var jsonData = '';
			var dataStr = '';
			var i=0;
	   		operationMgrGenerateParameterStore.each(function(r){
				if(i==0) jsonData = createJsonRecord(r,paraNameList);
				else jsonData = jsonData + ',' + createJsonRecord(r,paraNameList);		
				i++;
			},this);								
			var generateParameterListJson = '\"generateParameterList\":[' + jsonData + ']';
			dataStr = '{' + generateParameterListJson + '}';

			Ext.Msg.wait('loading');
			Ext.Ajax.request({
				timeout:gridTimeout,
				url:'/Node.Administration/Page/Entry/OperationMgr.do?act=generateFile',
				params:{
					dataFlow:operationName,
					dataString:dataStr
				},
				success:function(response,options){						
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					var rsp = response.responseText;							
            		Ext.Msg.alert('Message', rsp);	        		
				},		
				failure:function(){
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					Ext.Msg.alert('Message', "Fail to access database.");												
				}
			})		    					    				
		}
	});
   
   var transformBtn = new Ext.Button(
           {
	  		id:'transformBtn',
	 		text:'Transform',
	 		type:'submit',
	 		handler:function (){
			 	var records = operationMgrSubmitCheckBoxSM.getSelection();
			 	var templates = operationMgrViewTemplateCheckBoxSM.getSelection();
				var fileId = records[0].get('fileId');
			    if(templates!=null && templates.length > 0){
					var templateId = templates[0].get('templateId');
					openOperationMgrViewStyleSheetWin("/Node.Administration/Page/Entry/OperationMgr.do?act=viewDocument&documentId="+fileId+"&templateId="+templateId+"&onlineTrans=y&operationId=");
//					Ext.Ajax.request({
//						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=viewDocument',
//						params:{
//							documentId:fileId,
//							templateId:templateId
//						},
//						success:function(response,options){						
//							var rsp = response.responseText;
//							alert(rsp);
//		            		Ext.Msg.alert('Message', rsp);	        		
//						},		
//						failure:function(){
//							Ext.Msg.alert('Message', "Fail to access database.");												
//						}
//					})		    					    								   
			    }else{
			    	Ext.Msg.alert('Message', "Please select one template.");
			    }
			}
        });

   // WI 17860
   var downloadTransformBtn = new Ext.Button(
           {
	  		id:'downloadTransformBtn',
	 		text:'Download Transform',
	 		type:'submit',
	 		handler:function (){
			 	var records = operationMgrSubmitCheckBoxSM.getSelection();
			 	var templates = operationMgrViewTemplateCheckBoxSM.getSelection();
				var fileId = records[0].get('fileId');
			    if(templates!=null && templates.length > 0){
					var templateId = templates[0].get('templateId');
	 				// Get paramters
    				var combo = Ext.getCmp("opDocumentOperationNameCombox");
					var operationName = combo.getValue();
					var operationId = null;
					if(operationName!=null && operationName !=''){
							combo.store.each(function(r){
		 					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
		 						operationId = r.get("operationId");
		 						return;
		 					}        					
		 					
							},this);								
					}else{
						Ext.Msg.alert('Message', "Please select operation name.");
						return;
					}
					openOperationMgrViewStyleSheetWin("/Node.Administration/Page/Entry/OperationMgr.do?act=viewDocument&documentId="+fileId+"&templateId="+templateId+"&onlineTrans=n&operationId="+operationId);
					
//					Ext.Ajax.request({
//						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=viewDocument',
//						params:{
//							documentId:fileId,
//							templateId:templateId
//						},
//						success:function(response,options){						
//							var rsp = response.responseText;
//							alert(rsp);
//		            		Ext.Msg.alert('Message', rsp);	        		
//						},		
//						failure:function(){
//							Ext.Msg.alert('Message', "Fail to access database.");												
//						}
//					})		    					    								   
			    }else{
			    	Ext.Msg.alert('Message', "Please select one template.");
			    }
			}
        });

   var okBtn = new Ext.Button(
           {
	  		id:'okBtn',
	 		text:'OK',
	 		type:'submit',
	 		handler:function (){
        	   
	        	   // set postback parameters for search
	           		var i = 0;
	           		var isEmpty = false;
					operationMgrSubmitParams.conditionName = "";
					operationMgrSubmitParams.conditionSign = "";
					operationMgrSubmitParams.conditionValue = "";
					operationMgrSubmitParams.conditionStyle = "";
		   			operationMgrFilterConditionStore.each(function(r){
	    						var conditionName = r.get("conditionName");
	    						var conditionSign = r.get("conditionSign");
	    						var conditionValue = r.get("conditionValue");
	    						var conditionStyle = r.get("conditionStyle");
	    						if((conditionSign!="all" && conditionValue==null) || (conditionSign!="all" && conditionValue=="")){
	    							isEmpty = true;
	    						}else if(i==0){
	        	    				operationMgrSubmitParams.conditionName += conditionName;   							
	        	    				operationMgrSubmitParams.conditionSign += conditionSign;   							
		        	    			operationMgrSubmitParams.conditionValue += conditionValue;	        	    					
	        	    				operationMgrSubmitParams.conditionStyle += conditionStyle;   							
	        	    				
	    						}else{
	    							operationMgrSubmitParams.conditionName += "," + conditionName;
	        	    				operationMgrSubmitParams.conditionSign += "," + conditionSign;   							
		        	    			operationMgrSubmitParams.conditionValue += "," + conditionValue;   							
	        	    				operationMgrSubmitParams.conditionStyle += "," + conditionStyle;   							
	    						}
	    						i++;
	    					},this);								
			   				if(isEmpty){
			   					Ext.Msg.alert('Message', "Condition Value could not be empty. ");
			   				}else{
				 	        	   operationMgrFilterConditionWin.hide();	   						   					
			   				}
				}
        });

   // trigger the list data store load
   var loadOperationMgrStore=function(){
	   operationMgrStore.load({
//			callback:function(r,options,success){
//				if(!success){
//					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
//					Ext.Ajax.request({
//						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=getOperationList',
//						success:function(response,options){						
//							var rsp = response.responseText;							
//		            		Ext.Msg.alert('Message', rsp);	        		
//						},		
//						failure:function(){
//							Ext.Msg.alert('Message', "Fail to access database.");												
//						}
//					})		    					    				
//				}
//			}
	    });
   }

   var loadOperationMgrTemplateStore=function(){
	   operationMgrTemplateStore.load({
//			callback:function(r,options,success){
//				if(!success){
//					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
//					Ext.Ajax.request({
//						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=getOperationTemplateList',
//						success:function(response,options){						
//							var rsp = response.responseText;							
//		            		Ext.Msg.alert('Message', rsp);	        		
//						},		
//						failure:function(){
//							Ext.Msg.alert('Message', "Fail to access database.");												
//						}
//					})		    					    				
//				}
//			}
	    });
   }

   var loadOperationMgrValidateStore=function(){
	   operationMgrValidateStore.load({
//			callback:function(r,options,success){
//				if(!success){
//					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
//					Ext.Ajax.request({
//						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=getOperationValidateList',
//						success:function(response,options){						
//							var rsp = response.responseText;							
//		            		Ext.Msg.alert('Message', rsp);	        		
//						},		
//						failure:function(){
//							Ext.Msg.alert('Message', "Fail to access database.");												
//						}
//					})		    					    				
//				}
//			}
	    });
   }

   var loadOperationMgrSubmitStore=function(){
	   if(operationMgrSubmitGrid.getStore().id=='operationMgrSubmitStore'){
		   operationMgrSubmitStore.load({
					callback:function(r,options,success){
	   				if(!success ){
							Ext.Ajax.request({
								url:'document.do?method=getOperationMgrDocumentList',
								success:function(response,options){						
									var rsp = response.responseText;							
				            		Ext.Msg.alert('Message', rsp);	        		
								},		
								failure:function(){
									Ext.Msg.alert('Message', "Fail to access database.");												
								}
							})
	   				}
		    		if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');
				}
		   });		   
	   }else{
		   newOperationMgrSubmitStore.load({
				callback:function(r,options,success){
	   				if(!success ){
							Ext.Ajax.request({
								url:'document.do?method=getOperationMgrDocumentList',
								success:function(response,options){						
									var rsp = response.responseText;							
				            		Ext.Msg.alert('Message', rsp);	        		
								},		
								failure:function(){
									Ext.Msg.alert('Message', "Fail to access database.");												
								}
							})
	   				}
		    		if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');
				}
		   });		   
	   }
   }

   var loadOperationMgrFilterSetStore=function(){
	   operationMgrFilterSetStore.load({
			callback:function(r,options,success){
				if(!success){
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					Ext.Ajax.request({
						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=getOperationParameterList',
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

   var loadOperationMgrFilterConditionStore=function(){
	   operationMgrFilterConditionStore.load({
			callback:function(r,options,success){
				if(!success){
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					Ext.Ajax.request({
						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=getOperationConditionList',
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

   var loadOperationMgrManagerStore=function(){
	   operationMgrManagerStore.load({
			callback:function(r,options,success){
				if(!success){
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					Ext.Ajax.request({
						url:'/Node.Administration/Page/Entry/OperationMgr.do?act=getOperationManagerTableList',
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

   var passwordRenderer = function(value) {
	   var s = "";

	   for (var i = 0; i < value.length; i++) s = s + "*";

	   return s;
	}
	var operationMgrCheckBoxSM = Ext.create('Ext.selection.CheckboxModel');
	var gridEditingRecord;
	var operationMgrGrid = Ext.create('Ext.grid.Panel',  {
	   id: 'operationMgrGrid',
	   title :'Operations',
	   region:'north',
	   height: 300,
       store: operationMgrStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       selModel:operationMgrCheckBoxSM,
       plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
           clicksToEdit: 1
       })],
       listeners: {
			'itemclick':function(grid, record, item, rowIndex, e, eOpts ){
				if(record!=null || record !=''){
					// get template	
					if(record.get("operationId")!=null && record.get("operationId") != ''){
						operationMgrTemplateParams.operationId=record.get("operationId");
						loadOperationMgrTemplateStore();					
						// get validation file						
						operationMgrValidateParams.operationId=record.get("operationId");
						loadOperationMgrValidateStore();											
					}
					
					// Disable GetStatusComplete and GetStatusError base on GetStatus
					/*if(record.get("getStatus")=="False"){
						Ext.getCmp('isGetStatusComplete').disable();
						Ext.getCmp('isGetStatusError').disable() ;
					}else{
						Ext.getCmp('isGetStatusComplete').enable() ;
						Ext.getCmp('isGetStatusError').enable() ;						
					}*/
				}
				gridEditingRecord = record;
			},
			'celldblclick':function(grid, rowIndex, columnIndex, e){
				// set current record index
				gridEditingRecord = grid.getStore().getAt(rowIndex);
			},
			'edit':function(){
				updatOperationList();
			}
	    }, 	                    


       // grid columns
       columns:[
       {
       	id:'Operation_Id',
           text: 'Operation Id',
	       dataIndex: 'operationId',
	       hidden: true,
	       sortable: true
       },{
       		id:'Operation_Name',
       		text: 'Operation Name',
       		dataIndex: 'operationName',
       		width:200,
       		editor: new Ext.form.field.ComboBox({
				id:'operationMgrNameCombox',
                store: operationNameStore,
                allowBlank:true,
                displayField:'operationName',
                typeAhead: false,
                mode: 'remote',
                triggerAction: 'all',
                editable:false,
        		listeners: {
        			'beforeselect':function(combo, record, index){
        				var recordGrid = gridEditingRecord;
        				var isAuth = false;
        				if(recordGrid!=null && recordGrid !=''){
        					var operationName =recordGrid.get("operationName");
        					if(operationName!=null && operationName == ""){
            					// check operation privilage
             					combo.store.each(function(r){
                					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
                							isAuth = true;
                							return;
                					}        					
                					
            					},this);
            					if(!isAuth){
            						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
            						return false;
            					}else{
            						return true;
            					}        						
        					}else{
        						Ext.Msg.alert('Message', 'You can not change the existing operation name.');
        						return false;        						
        					}        					
        				}
        			},
        			'select':function(combo,records,index){
    					// set operation id automatically
        				if(gridEditingRecord!=null){
        					gridEditingRecord.set('operationId',records[0].get("operationId"));
        				}
        			}
        	    }, 	                    
                selectOnFocus:false
            }),
			sortable: true
       },{
           id:'operation_Version',
            text: 'Operation Version',
 	      	dataIndex: 'operationVersion',
			editor:
				new Ext.form.field.ComboBox({
   				id:'version',
		        store: new Ext.data.Store({
		            fields: ['enabled'],
		            data : VersionData
		        }),
                displayField:'enabled',
                mode: 'local',
                allowBlank:false,
			    editable:false,
			    triggerAction: 'all',
        		listeners: {
        			'beforeselect':function(combo, record, index){
        				var recordGrid = gridEditingRecord;
        				var isAuth = false;
        				if(recordGrid!=null || recordGrid !=''){
        					// check operation privilage						 
        					var operationName =recordGrid.get("operationName");
        					
        					if(operationName!=null && operationName!=''){
        						operationNameTestStore.load({
            	    				callback:function(r,options,success){
    	    		    				if(!success){
//    	    								Ext.Ajax.request({
//    	    									url:'/Node.Administration/Page/Entry/operation.do?method=getOperationNameList',
//    	    									success:function(response,options){						
//    	    										var rsp = response.responseText;							
//    	    					            		Ext.Msg.alert('Message', rsp);	        		
//    	    									},		
//    	    									failure:function(){
//    	    										Ext.Msg.alert('Message', "Fail to access database.");												
//    	    									}
//    	    								})		    					    				
    	    		    				}else{
    	    		    					operationNameTestStore.each(function(r){
    			            					if(operationName!=null && operationName == r.get("operationName")){
    			            							isAuth = true;
    			            							return;
    			            					}        					
    			            					
    			        					},this);
    			        					if(!isAuth){
    			        						Ext.getCmp('version').reset();
    			        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
    			        					}		    		    					
    	    		    				}
        	    					}
            					});        						
        					}else{
        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
        						return false;
        					}
        					return true;
        				}
        			}
        	    }, 	                    
			    selectOnFocus:false
			}),			
			sortable: true
        },{
           id:'Data_Flow',
           text: 'Data Flow',
 	       dataIndex: 'dataFlow',
 	       editor: new Ext.form.TextField({
			       	listeners: {
			   			'focus':function(combo, record, index){
			   				var recordGrid = gridEditingRecord;
			   				var isAuth = false;
			   				if(recordGrid!=null && recordGrid !=''){
			   					// check operation privilage						 
			   					var operationName =recordGrid.get("operationName");
	        					if(operationName!=null && operationName!=''){
	        						operationNameTestStore.load({
				   	    				callback:function(r,options,success){
				   		    				if(!success){
//			   									Ext.Msg.alert('Message', "Fail to access database.");												
				   		    				}else{
				   		    					operationNameTestStore.each(function(r){
					            					if(operationName!=null && operationName == r.get("operationName")){
					            							isAuth = true;
					            							return;
					            					}        					
					            					
					        					},this);
					        					if(!isAuth){
					        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
						        					loadOperationMgrStore();				        											        						
					        					}		    		    					
				   		    				}
					    				}
				   					});	        						
	        					}else{
	        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
	        						return false;
	        					}
			   					return true;
			   				}
			   			}
			   	    }, 	                    
	               allowBlank: false
				}),
 	       sortable: true
        },{
	       	id:'upload',
	        text: 'Upload',
	       	dataIndex: 'upload',
		    width: 50,
			editor:
				new Ext.form.field.ComboBox({
	       				id:'isUpload',
				        store: new Ext.data.Store({
				            fields: ['enabled'],
				            data : EnableData
				        }),
	                    displayField:'enabled',
	                    mode: 'local',
	                    allowBlank:false,
					    editable:false,
					    triggerAction: 'all',
		        		listeners: {
		        			'beforeselect':function(combo, record, index){
		        				var recordGrid = gridEditingRecord;
		        				var isAuth = false;
		        				if(recordGrid!=null && recordGrid !=''){
		        					// check operation privilage						 
		        					var operationName =recordGrid.get("operationName");
		        					if(operationName!=null && operationName!=''){
		        						operationNameTestStore.load({
			        	    				callback:function(r,options,success){
				    		    				if(!success){
//				    								Ext.Ajax.request({
//				    									url:'/Node.Administration/Page/Entry/operation.do?method=getOperationNameList',
//				    									success:function(response,options){						
//				    										var rsp = response.responseText;							
//				    					            		Ext.Msg.alert('Message', rsp);	        		
//				    									},		
//				    									failure:function(){
//				    										Ext.Msg.alert('Message', "Fail to access database.");												
//				    									}
//				    								})		    					    				
				    		    				}else{
				    		    					operationNameTestStore.each(function(r){
						            					if(operationName!=null && operationName == r.get("operationName")){
						            							isAuth = true;
						            							return;
						            					}        					
						            					
						        					},this);
						        					if(!isAuth){
						        						Ext.getCmp('isUpload').reset();
						        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
						        					}		    		    					
				    		    				}
			    	    					}
			        					});		        						
		        					}else{
		        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
		        						return false;
		        					}
		        					return true;
		        				}
		        			},
		        			'change':function(field, newvalue, oldvalue){
		        				var recordGrid = gridEditingRecord;
		        				var isAuth = false;
		        				if(recordGrid!=null && recordGrid !=''){
	        						if(newvalue=="True"){
	        							recordGrid.set("generate","False");						        							
	        						}else{
	        							recordGrid.set("generate","True");						        							
	        						}						        					
		        				}
		        			}
		        	    }, 	                    
					    selectOnFocus:false
				}),			
 	       sortable: true
        },{
           id:'generate',
           text: 'Generate',
 	       dataIndex: 'generate',
	       width: 60,
 	       editor:
				new Ext.form.field.ComboBox({
	       				id:'isGenerate',
				        store: new Ext.data.Store({
				            fields: ['enabled'],
				            data : EnableData
				        }),
	                    displayField:'enabled',
	                    mode: 'local',
	                    allowBlank:false,
					    editable:false,
					    triggerAction: 'all',
		        		listeners: {
		        			'beforeselect':function(combo, record, index){
		        				var recordGrid = gridEditingRecord;
		        				var isAuth = false;
		        				if(recordGrid!=null && recordGrid !=''){
		        					// check operation privilage						 
		        					var operationName =recordGrid.get("operationName");
		        					if(operationName!=null && operationName!=''){
		        						operationNameTestStore.load({
			        	    				callback:function(r,options,success){
				    		    				if(!success){
//				    								Ext.Ajax.request({
//				    									url:'/Node.Administration/Page/Entry/operation.do?method=getOperationNameList',
//				    									success:function(response,options){						
//				    										var rsp = response.responseText;							
//				    					            		Ext.Msg.alert('Message', rsp);	        		
//				    									},		
//				    									failure:function(){
//				    										Ext.Msg.alert('Message', "Fail to access database.");												
//				    									}
//				    								})		    					    				
				    		    				}else{
				    		    					operationNameTestStore.each(function(r){
						            					if(operationName!=null && operationName == r.get("operationName")){
						            							isAuth = true;
						            							return;
						            					}        					
						            					
						        					},this);
						        					if(!isAuth){
						        						Ext.getCmp('isGenerate').reset();
						        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
						        					}		    		    					
				    		    				}
			    	    					}
			        					});		        						
		        					}else{
		        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
		        						return false;
		        					}
		        					return true;
		        				}
		        			},
		        			'change':function(field, newvalue, oldvalue){
		        				var recordGrid = gridEditingRecord;
		        				var isAuth = false;
		        				if(recordGrid!=null && recordGrid !=''){
	        						if(newvalue=="True"){
	        							recordGrid.set("upload","False");						        							
	        						}else{
	        							recordGrid.set("upload","True");						        							
	        						}						        					
		        				}
		        			}
		        	    }, 	                    
					    selectOnFocus:false
				}),			
 	       sortable: true
        },{
           id:'submit',
           text: 'Submit',
 	       dataIndex: 'submit',
	       width: 50,
 	       editor:
				new Ext.form.field.ComboBox({
	       				id:'isSubmit',
				        store: new Ext.data.Store({
				            fields: ['enabled'],
				            data : EnableData
				        }),
	                    displayField:'enabled',
	                    mode: 'local',
	                    allowBlank:false,
					    editable:false,
					    triggerAction: 'all',
		        		listeners: {
		        			'beforeselect':function(combo, record, index){
		        				var recordGrid = gridEditingRecord;
		        				var isAuth = false;
		        				if(recordGrid!=null && recordGrid !=''){
		        					// check operation privilage						 
		        					var operationName =recordGrid.get("operationName");
		        					if(operationName!=null && operationName!=''){
		        						operationNameTestStore.load({
			        	    				callback:function(r,options,success){
				    		    				if(!success){
//				    								Ext.Ajax.request({
//				    									url:'/Node.Administration/Page/Entry/operation.do?method=getOperationNameList',
//				    									success:function(response,options){						
//				    										var rsp = response.responseText;							
//				    					            		Ext.Msg.alert('Message', rsp);	        		
//				    									},		
//				    									failure:function(){
//				    										Ext.Msg.alert('Message', "Fail to access database.");												
//				    									}
//				    								})		    					    				
				    		    				}else{
				    		    					operationNameTestStore.each(function(r){
						            					if(operationName!=null && operationName == r.get("operationName")){
						            							isAuth = true;
						            							return;
						            					}        					
						            					
						        					},this);
						        					if(!isAuth){
						        						Ext.getCmp('isSubmit').reset();
						        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
						        					}		    		    					
				    		    				}
			    	    					}
			        					});		        						
		        					}else{
		        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
		        						return false;
		        					}
		        					return true;
		        				}
		        			}
		        	    }, 	                    
					    selectOnFocus:false
				}),			
 	       sortable: true
        },{
           	id:'URL',
            text: 'URL',
 	       	dataIndex: 'URL',
	        width: 300,
	        editor: new Ext.form.TextField({
			       	listeners: {
			   			'focus':function(combo, record, index){
			   				var recordGrid = gridEditingRecord;
			   				var isAuth = false;
			   				if(recordGrid!=null && recordGrid !=''){
			   					// check operation privilage						 
			   					var operationName =recordGrid.get("operationName");
	        					if(operationName!=null && operationName!=''){
	        						operationNameTestStore.load({
				   	    				callback:function(r,options,success){
				   		    				if(!success){
//			   									Ext.Msg.alert('Message', "Fail to access database.");												
				   		    				}else{
				   		    					operationNameTestStore.each(function(r){
					            					if(operationName!=null && operationName == r.get("operationName")){
					            							isAuth = true;
					            							return;
					            					}        					
					            					
					        					},this);
					        					if(!isAuth){
					        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
						        					loadOperationMgrStore();				        											        						
					        					}		    		    					
				   		    				}
					    				}
				   					});	        						
	        					}else{
	        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
	        						return false;
	        					}
			   					return true;
			   				}
			   			}
			   	    }, 	                    
	               allowBlank: false
				}),
			sortable: true
        },{
           id:'Username',
           text: 'User Name',
 	       dataIndex: 'userName',
	       width: 200,
 	       editor: new Ext.form.TextField({
			       	listeners: {
		   			'focus':function(combo, record, index){
		   				var recordGrid = gridEditingRecord;
		   				var isAuth = false;
		   				if(recordGrid!=null && recordGrid !=''){
		   					// check operation privilage						 
		   					var operationName =recordGrid.get("operationName");
        					if(operationName!=null && operationName!=''){
        						operationNameTestStore.load({
			   	    				callback:function(r,options,success){
			   		    				if(!success){
//		   									Ext.Msg.alert('Message', "Fail to access database.");												
			   		    				}else{
			   		    					operationNameTestStore.each(function(r){
				            					if(operationName!=null && operationName == r.get("operationName")){
				            							isAuth = true;
				            							return;
				            					}        					
				            					
				        					},this);
				        					if(!isAuth){
				        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
					        					loadOperationMgrStore();				        											        						
				        					}		    		    					
			   		    				}
				    				}
			   					});	        						
        					}else{
        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
        						return false;
        					}
		   					return true;
		   				}
		   			}
		   	    }, 	                    
               allowBlank: false
			}),
 	       		sortable: true
        },{
           id:'Password',
           text: 'Password',
 	       dataIndex: 'password',
 	       renderer:passwordRenderer,
 	       editor: new Ext.form.TextField({
 	    	   		inputType:'password',
			       	listeners: {
		   			'focus':function(combo, record, index){
		   				var recordGrid = gridEditingRecord;
		   				var isAuth = false;
		   				if(recordGrid!=null && recordGrid !=''){
		   					// check operation privilage						 
		   					var operationName =recordGrid.get("operationName");
        					if(operationName!=null && operationName!=''){
        						operationNameTestStore.load({
			   	    				callback:function(r,options,success){
			   		    				if(!success){
//		   									Ext.Msg.alert('Message', "Fail to access database.");												
			   		    				}else{
			   		    					operationNameTestStore.each(function(r){
				            					if(operationName!=null && operationName == r.get("operationName")){
				            							isAuth = true;
				            							return;
				            					}        					
				            					
				        					},this);
				        					if(!isAuth){
				        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
					        					loadOperationMgrStore();				        											        						
				        					}		    		    					
			   		    				}
				    				}
			   					});	        						
        					}else{
        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
        						return false;
        					}
		   					return true;
		   				}
		   			}
		   	    }, 	                    
	   	    	allowBlank: false
				}),
 	       sortable: true
        },{
           	id:'domainName',
            text: 'Domain Name',
 	       	dataIndex: 'domainName',
	        editor: new Ext.form.TextField({
			       	listeners: {
			   			'focus':function(combo, record, index){
			   				var recordGrid = gridEditingRecord;
			   				var isAuth = false;
			   				if(recordGrid!=null && recordGrid !=''){
			   					// check operation privilage						 
			   					var operationName =recordGrid.get("operationName");
	        					if(operationName!=null && operationName!=''){
	        						operationNameTestStore.load({
				   	    				callback:function(r,options,success){
				   		    				if(!success){
//			   									Ext.Msg.alert('Message', "Fail to access database.");												
				   		    				}else{
				   		    					operationNameTestStore.each(function(r){
					            					if(operationName!=null && operationName == r.get("operationName")){
					            							isAuth = true;
					            							return;
					            					}        					
					            					
					        					},this);
					        					if(!isAuth){
					        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
						        					loadOperationMgrStore();				        											        						
					        					}		    		    					
				   		    				}
					    				}
				   					});	        						
	        					}else{
	        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
	        						return false;
	        					}
			   					return true;
			   				}
			   			}
			   	    }, 	                    
	               allowBlank: true
				}),
			sortable: true
        },{
            id:'getStatus',
            text: 'Get Status',
  	        dataIndex: 'getStatus',
  	        editor:
 				new Ext.form.field.ComboBox({
 	       				id:'isGetStatus',
 				        store: new Ext.data.Store({
 				            fields: ['enabled'],
 				            data : EnableData
 				        }),
 	                    displayField:'enabled',
 	                    mode: 'local',
 	                    allowBlank:false,
 					    editable:false,
 					    triggerAction: 'all',
 		        		listeners: {
 		        			'beforeselect':function(combo, record, index){
 		        				var recordGrid = gridEditingRecord;
 		        				var isAuth = false;
 		        				if(recordGrid!=null && recordGrid !=''){
 		        					// check operation privilage						 
 		        					var operationName =recordGrid.get("operationName");
 		        					var operationId = recordGrid.get("operationId");
 		        					if(operationName!=null && operationName!=''){
 		        						operationNameTestStore.load({
 			        	    				callback:function(r,options,success){
 				    		    				if(!success){
// 				    								Ext.Ajax.request({
// 				    									url:'/Node.Administration/Page/Entry/operation.do?method=getOperationNameList',
// 				    									success:function(response,options){						
// 				    										var rsp = response.responseText;							
// 				    					            		Ext.Msg.alert('Message', rsp);	        		
// 				    									},		
// 				    									failure:function(){
// 				    										Ext.Msg.alert('Message', "Fail to access database.");												
// 				    									}
// 				    								})		    					    				
 				    		    				}else{
 				    		    					operationNameTestStore.each(function(r){
 						            					if(operationName!=null && operationName == r.get("operationName")){
 						            							isAuth = true;
 						            							return;
 						            					}        					
 						            					
 						        					},this);
 						        					if(!isAuth){
 						        						Ext.getCmp('isSubmit').reset();
 						        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
 						        					}		    		    					
 				    		    				}
 			    	    					}
 			        					});
	    								Ext.Ajax.request({
	    									url:'/Node.Administration/Page/Entry/OperationMgr.do?act=checkGetStatusAvalible&operationId='+operationId,
	    									success:function(response,options){						
	    										var rsp = response.responseText;							
	    					            		if(rsp=='false'){
	    					            			Ext.getCmp('isGetStatus').reset();
	    					            			Ext.Msg.alert('Message', 'You can not change Get Status since this operation is not created by Node Wizard.');
	    					            		}
	    									},		
	    									failure:function(){
	    										Ext.Msg.alert('Message', "Fail to access database.");												
	    									}
	    								})		    					    				
 			        					
 		        					}else{
 		        						Ext.Msg.alert('Message', 'Please select operation name first.');        						
 		        						return false;
 		        					}
 		        					return true;
 		        				}
 		        			},
 		        			'change':function(field, newvalue, oldvalue){
		        				var recordGrid = gridEditingRecord;
		        				var isAuth = false;
		        				if(recordGrid!=null && recordGrid !=''){
	        						if(newvalue=="False"){
	        							Ext.Msg.alert('Message', 'Get Status Complete and Get Status Error are disable because Get Status is False.'); 
	        							/*Ext.getCmp('isGetStatusComplete').disable() ;
	        							Ext.getCmp('isGetStatusError').disable();*/
	        							//recordGrid.set("upload","False");						        							
	        						}else{
	        							/*Ext.getCmp('isGetStatusComplete').enable() ;
	        							Ext.getCmp('isGetStatusError').enable();*/
	        						}
		        				}
		        			}
 		        	    }, 	                    
 					    selectOnFocus:false
 				}),			
  	       sortable: true
         },{
              	id:'getStatusComplete',
              	text: 'Get Status Complete',
    	        dataIndex: 'getStatusComplete',
    		    width: 130,
    	  	    editor: new Ext.form.TextField({
  	       			id:'isGetStatusComplete',
   			       	listeners: {
   		   			'focus':function(combo, record, index){
   		   				var recordGrid = gridEditingRecord;
   		   				var isAuth = false;
   		   				if(recordGrid!=null && recordGrid !=''){
   		   					// check operation privilage						 
   		   					var operationName =recordGrid.get("operationName");
           					if(operationName!=null && operationName!=''){
           						operationNameTestStore.load({
   			   	    				callback:function(r,options,success){
   			   		    				if(!success){
//   		   									Ext.Msg.alert('Message', "Fail to access database.");												
   			   		    				}else{
   			   		    				operationNameTestStore.each(function(r){
   				            					if(operationName!=null && operationName == r.get("operationName")){
   				            							isAuth = true;
   				            							return;
   				            					}        					
   				            					
   				        					},this);
   				        					if(!isAuth){
   				        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
   					        					loadOperationMgrStore();				        											        						
   				        					}		    		    					
   			   		    				}
   				    				}
   			   					});	        						
           					}else{
           						Ext.Msg.alert('Message', 'Please select operation name first.');        						
           						return false;
           					}
   		   					return true;
   		   				}
   		   			}
   		   	    }, 	                    
                  allowBlank: true
   			}),
    	       sortable: true
           },{
               id:'getStatusError',
               text: 'Get Status Error',
     	        dataIndex: 'getStatusError',
    	  	    editor: new Ext.form.TextField({
  	       			id:'isGetStatusError',
   			       	listeners: {
   		   			'focus':function(combo, record, index){
   		   				var recordGrid = gridEditingRecord;
   		   				var isAuth = false;
   		   				if(recordGrid!=null && recordGrid !=''){
   		   					// check operation privilage						 
   		   					var operationName =recordGrid.get("operationName");
           					if(operationName!=null && operationName!=''){
           						operationNameTestStore.load({
   			   	    				callback:function(r,options,success){
   			   		    				if(!success){
//   		   									Ext.Msg.alert('Message', "Fail to access database.");												
   			   		    				}else{
   			   		    				operationNameTestStore.each(function(r){
   				            					if(operationName!=null && operationName == r.get("operationName")){
   				            							isAuth = true;
   				            							return;
   				            					}        					
   				            					
   				        					},this);
   				        					if(!isAuth){
   				        						Ext.Msg.alert('Message', 'You can not change this operation since you do not have privilage.');
   					        					loadOperationMgrStore();				        											        						
   				        					}		    		    					
   			   		    				}
   				    				}
   			   					});	        						
           					}else{
           						Ext.Msg.alert('Message', 'Please select operation name first.');        						
           						return false;
           					}
   		   					return true;
   		   				}
   		   			}
   		   	    }, 	                    
                  allowBlank: true
   			}),
     	       sortable: true
            }],

       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrPagingBar',
	        pageSize: rowNum,
	        store: operationMgrStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
       buttons: [operationAddBtn,operationDeleteBtn,{
			id:'opSetParameters',
			text:'Set Filter Parameters',
			type:'submit',
			handler:function (){
				var rcds = operationMgrCheckBoxSM.getSelection();
				if(rcds==null || rcds==""){
					Ext.Msg.alert('Message', "Please select one operation!");																					   					
				}else if(rcds.length>1){
					Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
				}else{
    				openOperationMgrFilterSetWin(rcds[0].get("operationId"));		    					
				}
			}
		},uploadOperationBtn]
   });

   
	var templateGridEditingRecord = null;
	var operationMgrTemplateCheckBoxSM = Ext.create('Ext.selection.CheckboxModel');
	var operationMgrTemplateGrid = new Ext.grid.Panel({
	   id: 'operationMgrTemplateGrid',
	   title :'Templates',
	   region:'west',
	   width: 450,
       store: operationMgrTemplateStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
           clicksToEdit: 2
       })],
       listeners: {
			'rowclick':function(grid, rowIndex, e){
				templateGridEditingRecord = grid.getStore().getAt(rowIndex);
			},
			'celldblclick':function(grid, rowIndex, columnIndex, e){
				// set current record index
				templateGridEditingRecord = grid.getStore().getAt(rowIndex);
			},
			'edit':function(e){
				updateTransformSuffixList();
			}
	    }, 	                    
	    selModel:operationMgrTemplateCheckBoxSM,
 
       // grid columns
       columns:[
       {
       	id:'Template_Id',
           text: 'Template Id',
	       dataIndex: 'templateId',
	       width: 100,
	       hidden: true,
	       sortable: true
       },{
       		id:'Template_Name',
       		text: 'Template Name',
       		dataIndex: 'templateName',
       		width: 250,
			sortable: true
       },{
       		id:'Transform_Suffix',
       		text: 'Transform Suffix',
       		dataIndex: 'transformSuffix',
       		width: 100,
			editor: new Ext.form.TextField({
	              allowBlank: true
			}),
			sortable: true
       }],

       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrTemplatePagingBar',
	        pageSize: rowNum,
	        store: operationMgrTemplateStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
       buttons: [uploadTemplateBtn,templateDeleteBtn]
   });

   var operationMgrValidateCheckBoxSM = Ext.create('Ext.selection.CheckboxModel'); 
   var operationMgrValidateGrid = new Ext.grid.Panel({
	   id: 'operationMgrValidateGrid',
	   title :'Validation Files',
	   width : 450,
	   region:'center',
       store: operationMgrValidateStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       selModel:operationMgrValidateCheckBoxSM,

       // grid columns
       columns:[
       {
       	id:'Validate_File_Id',
           text: 'Validate File Id',
	       dataIndex: 'validateFileId',
	       width: 100,
	       hidden: true,
	       sortable: true
       },{
       		id:'Validate_File_Name',
       		text: 'Validate File Name',
       		dataIndex: 'validateFileName',
       		width: 250,
			sortable: true
       }],

       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrValidatePagingBar',
	        pageSize: rowNum,
	        store: operationMgrValidateStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
       buttons: [uploadValidateBtn,validateDeleteBtn]
   });

/*   var operationMgrSelectValidateCheckBoxSM = new Ext.grid.CheckboxSelectionModel({
	   singleSelect:true
   });
   var operationMgrSelectValidateGrid = new Ext.grid.GridPanel({
	   id: 'operationMgrSelectValidateGrid',
	   title :'Validation Files',
	   region:'center',
       height:300,
       store: operationMgrValidateStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       sm:operationMgrSelectValidateCheckBoxSM,

       // grid columns
       columns:[operationMgrSelectValidateCheckBoxSM,
       {
       	id:'Validate_File_Id',
           text: 'Validate File Id',
	       dataIndex: 'validateFileId',
	       width: 100,
	       sortable: true
       },{
       		id:'Validate_File_Name',
       		text: 'Validate File Name',
       		dataIndex: 'validateFileName',
       		width: 150,
			sortable: true
       }],

       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrValidatePagingBar',
	        pageSize: rowNum,
	        store: operationMgrValidateStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
       buttons: [{
      	   id:'uploadConfirmBtn',
           text: 'Upload',
           handler : function(){
		   		var record = operationMgrSelectValidateCheckBoxSM..getSelection();
//		   		var fileRecord = operationMgrSubmitCheckBoxSM..getSelection();
		   		if(record==null || record==""){
					Ext.Msg.alert('Message', "Please select one validation file!");																					   					
		   		}else{
					var combo = Ext.getCmp('opDocumentOperationNameCombox');
					var operationName = combo.getValue();
					var operationId = null;
					if(operationName!=null && operationName !=''){
	 					combo.store.each(function(r){
	    					if((operationName!=null && operationName == r.get("operationName"))||(operationName!=null && operationName == "")){
	    						operationId = r.get("operationId");
	    						return;
	    					}        					
	    					
						},this);					
					}
					var validateFileId = record.get("validateFileId");
//					var submissionFileId = fileRecord.get("fileId");
//					var transId = fileRecord.get("transId");
					
//     	   			Ext.get('iframe-operationMgrValidateIframe').dom.src="/Node.Administration/Page/Entry/OperationMgr.do?act=uploadSubmissionFile&validateFileId="+validateFileId+"&submissionFileId="+submissionFileId+"&operationName="+operationName+"&transId="+transId;
     	   			Ext.get('iframe-operationMgrValidateIframe').dom.src="/Node.Administration/Page/Entry/OperationMgr.do?act=uploadSubmissionFile&validateFileId="+validateFileId+"&operationName="+operationName;
    	   		}
           }
       }]
   });
*/
   var operationMgrViewTemplateCheckBoxSM = Ext.create('Ext.selection.CheckboxModel',{
	   singleSelect:true
   });
   var operationMgrViewTemplateGrid = Ext.create('Ext.grid.Panel',{
	   id: 'operationMgrViewTemplateGrid',
	   title :'Templates',
	   region:'center',
       height:350,
       store: operationMgrTemplateStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       selModel:operationMgrViewTemplateCheckBoxSM,
       listeners: {
			'edit':function(e){
	   			updatOperationParameterList();
			}
	    }, 	                    

       // grid columns
       columns:[
       {
       	id:'Template_Id',
           text: 'Template Id',
	       dataIndex: 'templateId',
	       width: 100,
	       sortable: true
       },{
       		id:'Template_Name',
       		text: 'Template Name',
       		dataIndex: 'templateName',
       		width: 150,
			editor: new Ext.form.TextField({
	              allowBlank: true
			}),
			sortable: true
       }],

       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrTemplatePagingBar',
	        pageSize: rowNum,
	        store: operationMgrTemplateStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
       buttons: [transformBtn,downloadTransformBtn]
   });

   var filterSetGridEditingRecord = null;
   var operationMgrFilterSetCheckBoxSM = Ext.create('Ext.selection.CheckboxModel',{
	   singleSelect:false
   });
   var operationMgrFilterSetGrid = new Ext.grid.Panel({
	   id: 'operationMgrFilterSetGrid',
	   title :'Filter Parameters',
       height:350,
       store: operationMgrFilterSetStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       selModel:operationMgrFilterSetCheckBoxSM,
       plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
           clicksToEdit: 1
       })],
       listeners: {
			'itemclick':function(grid, record, item, rowIndex, e, eOpts){
				filterSetGridEditingRecord = record;
			},
			'celldblclick':function(grid, rowIndex, columnIndex, e){
				// set current record index
				filterSetGridEditingRecord = grid.getStore().getAt(rowIndex);
			},
			'edit':function(e){
	   			updatOperationParameterList();
			}
	    }, 	                    

       // grid columns
       columns:[
	       {
	       	id:'Parameter_Id',
	           text: 'Parameter Id',
		       dataIndex: 'parameterId',
		       width: 80,
		       hidden:true,
		       sortable: false
	       },{
				id:'Parameter_Name',
				text: 'Parameter Name',
				dataIndex: 'parameterName',
				width: 120,
				editor: new Ext.form.TextField({
						allowBlank: false
				}),
				sortable: true
	       },{
	       		id:'Parameter_Value',
	       		text: 'Parameter Value (XPath)',
	       		dataIndex: 'parameterValue',
	       		width: 500,
				editor: new Ext.form.TextField({
		              allowBlank: false
				}),
				sortable: true
       }],

       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrFilterSetPagingBar',
	        pageSize: rowNum,
	        store: operationMgrFilterSetStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
       buttons: [parameterAddBtn,parameterDeleteBtn]
   });
      

   var filterConditionGridEditingRecord = null;
   Ext.define('OperationMgrFilterConditionGridRecord', {
		extend: 'Ext.data.Model',
	    fields:[
	            {name: 'conditionName', type: 'string'},
	            {name: 'conditionSign', type: 'string'},
	            {name: 'conditionValue', type: 'string'},
	            {name: 'conditionStyle', type: 'string'}
	           ]
   });

   var operationMgrFilterConditionGridRecord = Ext.create('OperationMgrFilterConditionGridRecord');
   var operationMgrFilterConditionCheckBoxSM = Ext.create('Ext.selection.CheckboxModel',{
	   singleSelect:false
   });
   var operationMgrFilterConditionGrid = new Ext.grid.Panel({
	   id: 'operationMgrFilterConditionGrid',
	   title :'Filter Conditions',
	   region:'center',
       height:300,
       store: operationMgrFilterConditionStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       selModel:operationMgrFilterConditionCheckBoxSM,
       plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
           clicksToEdit: 1
       })],
       enableColumnMove:false,
       listeners: {
			'rowclick':function(grid, rowIndex, e){
				filterConditionGridEditingRecord = grid.getStore().getAt(rowIndex);
			},
			'celldblclick':function(grid, rowIndex, columnIndex, e){
				// set current record index
				filterConditionGridEditingRecord = grid.getStore().getAt(rowIndex);
			},
			'edit':function(e){
	   			;
			}
	    }, 	                    

       // grid columns
       columns:[
		       {
					id:'Condition_Name',
					text: 'Condition Name',
					dataIndex: 'conditionName',
					width: 120,
					sortable: true
		       },{
		           	id:'Condition_Sign',
		            text: 'Condition Sign',
		 	      	dataIndex: 'conditionSign',
					width: 80,
					editor:
						new Ext.form.field.ComboBox({
			   				id:'sign',
					        store: new Ext.data.Store({
					            fields: ['sign'],
					            data : sign
					        }),
			                displayField:'sign',
			                mode: 'local',
			     	       	width: 80,
			                allowBlank:false,
						    editable:false,
						    triggerAction: 'all',
						    selectOnFocus:false
					}),			
					sortable: false
		        },{
		       		id:'Condition_Value',
		       		text: 'Condition Value',
		       		dataIndex: 'conditionValue',
		       		width: 200,
					editor: new Ext.form.TextField({
			              allowBlank: true
					}),
					sortable: false
		        },{
		           id:'Condition_Style',
		           text: 'Value Style',
			       dataIndex: 'conditionStyle',
			       width: 100,
			       editor:
						new Ext.form.field.ComboBox({
			   				id:'sign',
					        store: new Ext.data.Store({
					            fields: ['style'],
					            data : style
					        }),
			                displayField:'style',
			                width: 100,
			                mode: 'local',
			                allowBlank:false,
						    editable:false,
						    triggerAction: 'all',
						    selectOnFocus:false
					}),			
					sortable: false
		        }],

       /* paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrFilterSetPagingBar',
	        pageSize: rowNum,
	        store: operationMgrFilterSetStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),*/
       buttons: [okBtn,conditionDeleteBtn]
   });
      
   function renderTemplateView(value, p, record){
       return Ext.String.format(
               '<span style="color:#0055A6;"><a href="#" onclick="openOperationMgrSubmitViewWin(\'{0}\')"><img src="../../images/PnlIco/pnlico_view.gif" ext:qtip="Click to view {1}"/></a></span>',
               record.get('fileId'),record.get('fileName'));
   }
   function renderTemplateDownload(value, p, record){
       return Ext.String.format(
               '<span style="color:#0055A6;"><a href="#" onclick="openDocumentDownloadWin(\'{0}\')">Download</a></span>',
               record.get('fileId'));
   }
   function renderSubmitDownload(value, p, record){
       return Ext.String.format(
               '<span style="color:#0055A6;"><a href="#" onclick="openOperationMgrManagerWin(\'{0}\')">Download</a></span>',
               record.get('transId'));
   }
   var operationMgrSubmitCheckBoxSM = Ext.create('Ext.selection.CheckboxModel',{
	   singleSelect:false
   });
   var operationMgrSubmitColModel = [
				   {
					   id:'document_View',
					   text: 'View',
					   renderer: renderTemplateView,
					   width:35,
					   sortable: false
				   },{
				       	id:'document_Id',
				        text: 'Document Id',
				        dataIndex: 'fileId',
						hidden: true
				    },{
				       	id:'document_Download',
				        text: 'Download',
						renderer: renderTemplateDownload,
						width:60,
						sortable: false
				    },{
				    	id:'document_Name',
				        text: 'Document Name',
				        dataIndex: 'fileName',
				        width:150,
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'document_Status',
				        text: 'Document Status',
				        dataIndex: 'documentStatus',
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'submit_status',
				        text: 'Submission Status',
				        dataIndex: 'submitStatus',
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'submit_status_report',
				        text: 'Submission Status Report',
				        renderer: renderSubmitDownload,
					    width:140,
					    sortable: false
				    },{
				    	id:'document_Type',
				        text: 'Document Type',
				        dataIndex: 'fileType',
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'document_Size',
				        text: 'Document Size',
				        dataIndex: 'fileSize',
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'document_TransId',
				        text: 'Transaction ID',
				        dataIndex: 'transId',
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'document_domainName',
				        text: 'Domain Name',
				        dataIndex: 'domainName',
						hidden: true,
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'document_OperationName',
				        text: 'Data Flow Name',
				        dataIndex: 'dataFlowName',
				        width:200,
						hidden: true,
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    },{
				    	id:'document_SubmittedDate',
				        text: 'Submitted Date',
				        dataIndex: 'updatedDate',
				        width:120,
						editor: new Ext.form.TextField({
						}),
						sortable: true
				    }];
   var operationMgrSubmitGrid = Ext.create('Ext.grid.Panel',{
	   id: 'operationMgrSubmitGrid',
	   title :'Submitted Files',
	   region:'center',
       store: operationMgrSubmitStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       selModel:operationMgrSubmitCheckBoxSM,
 
       // grid columns
       columns:eval(operationMgrSubmitColModel),
	
       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'submitDatePagingBar',
	        pageSize: rowNum,
	        store: operationMgrSubmitStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
	   // new Document button on the top
       buttons: [uploadFileBtn,generateFileBtn,submitFileBtn]
   });

   
   function renderManagerTableSubmitDownload(value, p, record){
       return Ext.String.format(
               '<span style="color:#0055A6;"><a href="#" onclick="openOperationDocumentDownloadWin(\'{0}\')"><img src="../../images/PnlIco/pnlico_view.gif" ext:qtip="Click to view {1}"/></a></span>',
               record.get('submitId'),record.get('operationName'));
   }   
   var operationMgrManagerColModel = [
                  				   {
                  					   id:'mgrCol_document_View',
                  					   text: 'View',
                  					   renderer: renderManagerTableSubmitDownload,
                  					   width:35,
                  					   sortable: false
                  				   },{
                  				       	id:'mgrCol_submit_Id',
                  				        text: 'Submit Id',
                  				        dataIndex: 'submitId',
                  				        width:70,
                  				        sortable: true
                  				    },{
                  				    	id:'mgrCol_operation_Name',
                  				        text: 'Opreation Name',
                  				        dataIndex: 'operationName',
                  				        width:150,
										editor: new Ext.form.TextField({
										}),
                  						sortable: true
                  				    },{
                  				    	id:'mgrCol_submit_Status',
                  				        text: 'Submit Status',
                  				        dataIndex: 'statusCD',
                  				        width:80,
										editor: new Ext.form.TextField({
										}),
                  						sortable: true
                  				    },{
                  				    	id:'mgrCol_submit_URL',
                  				        text: 'Submission URL',
                  				        dataIndex: 'submitURL',
                  				        width:250,
										editor: new Ext.form.TextField({
										}),
                  						sortable: true
                  				    },{
                  				    	id:'mgrCol_Submitted_Date',
                  				        text: 'Submitted Date',
                  				        dataIndex: 'submitDate',
                  				        width:80,
										editor: new Ext.form.TextField({
										}),
                  						sortable: true
                  				    },{
                  				    	id:'mgrCol_version',
                  				        text: 'Version',
                  				        dataIndex: 'version',
                  				        width:50,
										editor: new Ext.form.TextField({
										}),
                  					    sortable: true
                  				    },{
                  				    	id:'mgrCol_trans_Id',
                  				        text: 'Trans ID',
                  				        dataIndex: 'transId',
                  				        width:225,
										editor: new Ext.form.TextField({
										}),
                  						sortable: true
                  				    },{
                  				    	id:'mgrCol_supply_trans_Id',
                  				        text: 'Supplied Transaction ID',
                  				        dataIndex: 'supplyTransId',
                  				        width:225,
										editor: new Ext.form.TextField({
										}),
                  						sortable: true
                  				    },{
                  				    	id:'mgrCol_data_flow',
                  				        text: 'Data Flow',
                  				        dataIndex: 'dataFlow',
										editor: new Ext.form.TextField({
										}),
                  						sortable: true
                  				    }];
     var operationMgrManagerGrid = new Ext.grid.Panel({
		id: 'operationMgrManagerGrid',
		title :'Submitted File Detail',
		region:'center',
        store: operationMgrManagerStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,
        autoheight:true,
   
         // grid columns
        columns:eval(operationMgrManagerColModel),
  	
         // paging bar on the bottom
        bbar: new Ext.PagingToolbar({
  	    	id:'submitManagerPagingBar',
  	        pageSize: rowNum,
  	        store: operationMgrManagerStore,
  	        displayInfo: true,
  	        displayMsg: 'Total:{0}-{1} of {2}',
  	        emptyMsg: "Total:0"
  	    })
     });
   
     Ext.define('OperationMgrGenerateParameterGridRecord', {
 		extend: 'Ext.data.Model',
 	    fields:[
 	            {name: 'generateParameterName', type: 'string'},
 	            {name: 'generateParameterValue', type: 'string'}
 	           ]
    });
   var operationMgrGenerateParameterGridRecord = Ext.create('OperationMgrGenerateParameterGridRecord');
   var operationMgrGenerateParameterCheckBoxSM = Ext.create('Ext.selection.CheckboxModel',{
	   singleSelect:false
   });
   var operationMgrGenerateParameterGrid = new Ext.grid.Panel({
	   id: 'operationMgrGenerateParameterGrid',
	   title :'Parameters',
	   region:'center',
       height:400,
       store: operationMgrGenerateParameterStore,
       trackMouseOver:true,
       disableSelection:false,
       autoScroll:true,
       autoheight:true,
       selModel:operationMgrGenerateParameterCheckBoxSM,
       plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
           clicksToEdit: 1
       })],
       listeners: {
			'rowclick':function(grid, rowIndex, e){
			},
			'celldblclick':function(grid, rowIndex, columnIndex, e){
			},
			'edit':function(e){
			}
	    }, 	                    

       // grid columns
       columns:[{
				id:'GenerateParameter_Name',
				text: 'Parameter Name',
				dataIndex: 'generateParameterName',
				width: 120,
				editor: new Ext.form.TextField({
						allowBlank: false
				}),
				sortable: true
	       },{
	       		id:'GenerateParameter_Value',
	       		text: 'Parameter Value',
	       		dataIndex: 'generateParameterValue',
	       		width: 430,
				editor: new Ext.form.TextField({
		              allowBlank: false
				}),
				sortable: true
       }],

       // paging bar on the bottom
       bbar: new Ext.PagingToolbar({
	    	id:'operationMgrGenerateParameterPagingBar',
	        pageSize: rowNum,
	        store: operationMgrGenerateParameterStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    }),
       buttons: [generateGirdParameterSendBtn]
   });
      
   var createJsonString = function(paraNameList,paraValueList){
		var jsonData = '{';
		for(var j = 0; j < paraNameList.length; j++){
			if(j==0) jsonData = jsonData + '\"' + paraNameList[j] + '\":' + '\"' + paraValueList[j] + '\"';				
			else jsonData = jsonData + ',\"' + paraNameList[j] + '\":' + '\"' + paraValueList[j] + '\"';				
		} 
		return jsonData = jsonData + '}';    	
   };
   
   var createJsonRecord = function(record,paraNameList){
		var jsonData = '{';
		for(var j = 0; j < paraNameList.length; j++){
			if(j==0) jsonData = jsonData + '\"' + paraNameList[j] + '\":' + '\"' + record.get(paraNameList[j]) + '\"';				
			else jsonData = jsonData + ',\"' + paraNameList[j] + '\":' + '\"' + record.get(paraNameList[j]) + '\"';				
		} 
		return jsonData = jsonData + '}';
	};
    
	var createJsonList = function(store,listName,paraNameList){
		var jsonData = '';
		var i = 0;
		store.each(function(r){
			if(i==0) jsonData = createJsonRecord(r,paraNameList);
			else jsonData = jsonData + ',' + createJsonRecord(r,paraNameList);		
			i++;
		},this);

		jsonData = '\"' + listName + '\":[' + jsonData + ']';
		return jsonData;
	};
	
    var updatOperationList = function (){
		var jsonObject = null;
		var operationListParaNameList = ['operationId',
	                                      'operationName',
	                                      'operationVersion',
	                                      'dataFlow',
	                                      'upload',
	                                      'generate',
	                                      'submit',
	                                      'URL',
	                                      'userName',
	                                      'password',
	                                      'domainName',
	                                      'getStatus',
	                                      'getStatusComplete',
	                                      'getStatusError'];
		
		var operationList = 'operationList';
		
		var operationListJson = createJsonList(operationMgrStore,operationList,operationListParaNameList);
								
		var dataStr = '{' + operationListJson + '}';
		
		// check if there is same operation name and version
		var recordGrid = gridEditingRecord;		
		var operationName =recordGrid.get("operationName");
		var operationCnt = 0;
		operationMgrStore.each(function(r){	 
			if(operationName!=null && operationName == r.get("operationName")){
				operationCnt++;
			}        					
			
		},this);
		if(operationCnt > 1){
			Ext.Msg.alert('Message', 'You can not select this operation since there is same one.');
			loadOperationMgrStore();
			return;
		}

		//send back  
		Ext.Ajax.request({
			url:'/Node.Administration/Page/Entry/OperationMgr.do?act=updateOperationList',
			params:{
				dataString:dataStr
			},
			success:function(response,options){
				//loadOperationMgrStore();
			},		
			failure:function(){
				Ext.Msg.alert('Message', 'Fail to save operation list file.');												
			}
		});
	};

    var updatOperationParameterList = function (){
		var jsonObject = null;
		var parameterListParaNameList = ['parameterId',
	                                      'parameterName',
	                                      'parameterValue'];
		
		var parameterList = 'parameterList';
		
		var parameterListJson = createJsonList(operationMgrFilterSetStore,parameterList,parameterListParaNameList);
								
		var dataStr = '{' + parameterListJson + '}';

		
		// check if there is same parameter name
		var recordGrid = filterSetGridEditingRecord;		
		var parameterName =recordGrid.get("parameterName");
		var parameterCnt = 0;
		operationMgrFilterSetStore.each(function(r){	 
			if(parameterName!=null && parameterName == r.get("parameterName")){
				parameterCnt++;
			}        					
			
		},this);
		if(parameterCnt > 1){
			Ext.Msg.alert('Message', 'You can not add this parameter since there is same one.');
			loadOperationMgrFilterSetStore();
			return;
		}
		// check if parameter name include space
		if(parameterName.search(" ")!=-1){
			Ext.Msg.alert('Message', 'You can not create a parameter name with space.');
			loadOperationMgrFilterSetStore();
			return;			
		}

		// get operationId
		var operationId = null;
		var rcds = operationMgrCheckBoxSM.getSelection();
		if(rcds==null || rcds==""){
			Ext.Msg.alert('Message', "Please select one operation!");																					   					
		}else if(rcds.length>1){
			Ext.Msg.alert('Message', "Please select only one operation!");																					   						   					
		}else{
			operationId = rcds[0].get("operationId");		    					
			//send back  
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/OperationMgr.do?act=updateParameterList',
				params:{
					operationId:operationId,
					dataString:dataStr
				},
				success:function(response,options){
					//loadOperationMgrStore();
				},		
				failure:function(){
					Ext.Msg.alert('Message', 'Fail to save parameter list.');												
				}
			});
		}
	};

    var updateTransformSuffixList = function (){
		var jsonObject = null;
		var transformSuffixListParaNameList = ['templateId',
	                                      'templateName',
	                                      'transformSuffix'];
		
		var transformSuffixList = 'transformSuffixList';
		
		var transformSuffixListJson = createJsonList(operationMgrTemplateStore,transformSuffixList,transformSuffixListParaNameList);
								
		var dataStr = '{' + transformSuffixListJson + '}';

		// get operationId
		var operationId = null;
		var rcds = operationMgrCheckBoxSM.getSelection();
		if(rcds==null || rcds==""){
			Ext.Msg.alert('Message', "Please select one Operation!");																					   					
		}else if(rcds.length>1){
			Ext.Msg.alert('Message', "Please select only one Operation!");																					   						   					
		}else{
			operationId = rcds[0].get("operationId");		    					
			//send back  
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/OperationMgr.do?act=updateTransformSuffix',
				params:{
					operationId:operationId,
					dataString:dataStr
				},
				success:function(response,options){
					//loadOperationMgrStore();
				},		
				failure:function(){
					Ext.Msg.alert('Message', 'Fail to save transform suffix.');												
				}
			});
		}
	};
