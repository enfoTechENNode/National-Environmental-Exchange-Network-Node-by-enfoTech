    		                     
	// create iframe for upload and download 
	getServicesUploadIframe = Ext.create('Ext.ux.IFrame',{  
		id : 'getServicesUploadIframe',  
		src : '/Node.Administration/Page/Entry/GetServices.do?act=upload'
	});		

	getServicesDownloadIframe = Ext.create('Ext.ux.IFrame',{  
		id : 'getServicesDownloadIframe',  
		src : '/Node.Administration/Page/Entry/GetServices.do?act=showDownload'
	});		

    // base parameter
    var getServicesNodePropertyParams = {limit:rowNum,page:1,sort:{},start:0};
    var dedlGeneralParams = {limit:rowNum,page:1,sort:{},start:0};
    var dedlPropertyParams = {limit:rowNum,page:1,sort:{},start:0,dedlElementIdentifier:""};
    var getServicesNodeServicesParametersParams = {limit:rowNum,page:1,sort:{},start:0,dedlElementIdentifier:""};

    // create the Data Store
    var getServicesNodePropertyStore = Ext.create('Ext.data.Store', {
    	storeId:'getServicesNodePropertyStore',
        remoteSort: true,
        fields: [
	        {name: 'nodePropertyName', type: 'string', mapping: 'nodePropertyName'},
			{name: 'nodePropertyValue', type: 'string', mapping: 'nodePropertyValue'}
        ],
        proxy: {
            type: 'ajax',
            url: '/Node.Administration/Page/Entry/GetServices.do?act=getNodePropertyList',
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
				getServicesNodePropertyParams.limit = rowNum;
				getServicesNodePropertyParams.page = operation.page;
				getServicesNodePropertyParams.sort = operation.sort;
				getServicesNodePropertyParams.start = operation.start;
				
				operation.params = getServicesNodePropertyParams;
			}
        },
        sortOnLoad: true,
        sorters: { property: 'nodePropertyName', direction : 'ASC' }
    });
//    getServicesNodePropertyStore.setDefaultSort('nodePropertyName', 'asc');

    var dedlGeneralStore = Ext.create('Ext.data.Store', {
    	storeId:'dedlGeneralStore',
        remoteSort: true,
        fields: [
	        {name: 'elementIdentifier', type: 'string', mapping: 'elementIdentifier'},
			{name: 'applicationDomain', type: 'string', mapping: 'applicationDomain'},
			{name: 'elementType', type: 'string', mapping: 'elementType'},
			{name: 'description', type: 'string', mapping: 'description'},
			{name: 'keywords', type: 'string', mapping: 'keywords'},
			{name: 'owner', type: 'string', mapping: 'owner'},
			{name: 'elementLabel', type: 'string', mapping: 'elementLabel'},
			{name: 'defaultValue', type: 'string', mapping: 'defaultValue'},
			{name: 'upperLimit', type: 'string', mapping: 'upperLimit'},
			{name: 'lowerLimit', type: 'string', mapping: 'lowerLimit'},
			{name: 'allowMultiSelect', type: 'string', mapping: 'allowMultiSelect'},
			{name: 'additionalValuesIndicator', type: 'string', mapping: 'additionalValuesIndicator'},
			{name: 'optionality', type: 'string', mapping: 'optionality'},
			{name: 'wildcard', type: 'string', mapping: 'wildcard'},
			{name: 'formatString', type: 'string', mapping: 'formatString'},
			{name: 'validationRules', type: 'string', mapping: 'validationRules'},
			{name: 'dataSourceType', type: 'string', mapping: 'dataSourceType'},
			{name: 'connectionDescriptor', type: 'string', mapping: 'connectionDescriptor'},
			{name: 'accessStatement', type: 'string', mapping: 'accessStatement'},
			{name: 'parameters', type: 'string', mapping: 'parameters'},
			{name: 'transformation', type: 'string', mapping: 'transformation'}
        ],
        proxy: {
            type: 'ajax',
            url: '/Node.Administration/Page/Entry/GetServices.do?act=getDedlGeneralDataList',
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
	        	dedlGeneralParams.limit = rowNum;
	        	dedlGeneralParams.page = operation.page;
	        	dedlGeneralParams.sort = operation.sort;
	        	dedlGeneralParams.start = operation.start;
				
				operation.params = dedlGeneralParams;
			}
        },
        sortOnLoad: true,
        sorters: { property: 'elementIdentifier', direction : 'ASC' }
    });
//    dedlGeneralStore.setDefaultSort('elementIdentifier', 'asc');

    var dedlPropertyStore = Ext.create('Ext.data.Store', {
    	storeId:'dedlPropertyStore',
        remoteSort: true,
        fields: [
 			{name: 'propertyElementID', type: 'string', mapping: 'propertyElementID'},
			{name: 'propertyName', type: 'string', mapping: 'propertyName'},
	        {name: 'propertyValue', type: 'string', mapping: 'propertyValue'}
        ],
        proxy: {
            type: 'ajax',
            url: '/Node.Administration/Page/Entry/GetServices.do?act=getDedlPropertyDataList',
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
	        	dedlPropertyParams.limit = rowNum;
	        	dedlPropertyParams.page = operation.page;
	        	dedlPropertyParams.sort = operation.sort;
	        	dedlPropertyParams.start = operation.start;
					
				operation.params = dedlPropertyParams;
			}
        },
        sortOnLoad: true,
        sorters: { property: 'propertyName', direction : 'ASC' }
    });
//    dedlPropertyStore.setDefaultSort('propertyName', 'asc');

    var dedlElementValueStore = Ext.create('Ext.data.Store', {
    	storeId:'dedlElementValueStore',
        root: 'results',
        totalProperty: 'total',
        id: 'gridId',
        remoteSort: true,
        fields: [
 			{name: 'elementElementID', type: 'string', mapping: 'elementElementID'},
			{name: 'elementValue', type: 'string', mapping: 'elementValue'},
	        {name: 'elementValueLabel', type: 'string', mapping: 'elementValueLabel'}
        ],
        proxy: {
            type: 'ajax',
            url: '/Node.Administration/Page/Entry/GetServices.do?act=getDedlElementValueDataList',
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
	        	getServicesNodeServicesParametersParams.limit = rowNum;
	        	getServicesNodeServicesParametersParams.page = operation.page;
	        	getServicesNodeServicesParametersParams.sort = operation.sort;
	        	getServicesNodeServicesParametersParams.start = operation.start;
					
				operation.params = getServicesNodeServicesParametersParams;
			}
        },
        sortOnLoad: true,
        sorters: { property: 'elementValue', direction : 'ASC' }
    });
//    dedlElementValueStore.setDefaultSort('elementValue', 'asc');

    Ext.define('GetServicesNodePropertyRecord', {
		extend: 'Ext.data.Model',
	    fields:[
	           {name: 'nodePropertyName', type: 'string'},
	           {name: 'nodePropertyValue', type: 'string'}
	           ]
    });
    
    var getServicesNodePropertyRecord = Ext.create('GetServicesNodePropertyRecord');
      
    Ext.define('DedlGeneralRecord', {
		extend: 'Ext.data.Model',
	    fields:[
				{name: 'elementIdentifier', type: 'string'},
				{name: 'applicationDomain', type: 'string'},
				{name: 'elementType', type: 'string'},
				{name: 'description', type: 'string'},
				{name: 'keywords', type: 'string'},
				{name: 'owner', type: 'string'},
				{name: 'elementLabel', type: 'string'},
				{name: 'defaultValue', type: 'string'},
				{name: 'upperLimit', type: 'string'},
				{name: 'lowerLimit', type: 'string'},
				{name: 'allowMultiSelect', type: 'string'},
				{name: 'additionalValuesIndicator', type: 'string'},
				{name: 'optionality', type: 'string'},
				{name: 'wildcard', type: 'string'},
				{name: 'formatString', type: 'string'},
				{name: 'validationRules', type: 'string'},
				{name: 'dataSourceType', type: 'string'},
				{name: 'connectionDescriptor', type: 'string'},
				{name: 'accessStatement', type: 'string'},
				{name: 'parameters', type: 'string'},
				{name: 'transformation', type: 'string'}
	           ]
    });

    var dedlGeneralRecord = Ext.create('DedlGeneralRecord');

    Ext.define('DedlPropertyRecord', {
		extend: 'Ext.data.Model',
	    fields:[
	            {name: 'propertyElementID', type: 'string'},
	    		{name: 'propertyName', type: 'string'},
	    		{name: 'propertyValue', type: 'string'}
	           ]
    });

    var dedlPropertyRecord = Ext.create('DedlPropertyRecord');
 
    Ext.define('DedlElementValueRecord', {
		extend: 'Ext.data.Model',
	    fields:[
	            {name: 'elementElementID', type: 'string'},
	            {name: 'elementValue', type: 'string'},
	            {name: 'elementValueLabel', type: 'string'}
	           ]
    });

    var dedlElementValueRecord = Ext.create('DedlElementValueRecord');
 

   var getServicesRefreshBtn = new Ext.Button({
            text: 'Refresh',
            handler : function(){
			    Ext.Msg.wait('loading');   	
				Ext.Ajax.request({
					url:'/Node.Administration/Page/Entry/GetServices.do?act=getGeneralData',
					success:function(response,options){						
						if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
						var rsp = response.responseText;
						if(rsp.substring(1,5)!='html'){
							var jsonObject = Ext.JSON.decode(rsp);	
							Ext.getCmp('nodeIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeIdentifier);
							Ext.getCmp('nodeName').setValue(jsonObject.getServicesGeneralData[0].nodeName);
							Ext.getCmp('nodeAddress').setValue(jsonObject.getServicesGeneralData[0].nodeAddress);
							Ext.getCmp('organizationIdentifier').setValue(jsonObject.getServicesGeneralData[0].organizationIdentifier);
							Ext.getCmp('nodeContact').setValue(jsonObject.getServicesGeneralData[0].nodeContact);
							Ext.getCmp('nodeVersionIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeVersionIdentifier);
							Ext.getCmp('nodeDeploymentTypeCode').setValue(jsonObject.getServicesGeneralData[0].nodeDeploymentTypeCode);
							Ext.getCmp('nodeStatus').setValue(jsonObject.getServicesGeneralData[0].nodeStatus);
							Ext.getCmp('north').setValue(jsonObject.getServicesGeneralData[0].north);
							Ext.getCmp('south').setValue(jsonObject.getServicesGeneralData[0].south);
							Ext.getCmp('east').setValue(jsonObject.getServicesGeneralData[0].east);
							Ext.getCmp('west').setValue(jsonObject.getServicesGeneralData[0].west);
							loadGetServicesNodePropertyStore();
							//loadDedlGeneralStore();
							//loadDedlPropertyStore();
							//loadDedlElementValueStore();						
						}else Ext.Msg.alert('Message', rsp);
					},		
					failure:function(){
						if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
						Ext.Msg.alert('Message', "Fail to get general data.");												
					}
				})
           }
        });
        
   var getDedlRefreshBtn = new Ext.Button({
       text: 'Refresh',
       handler : function(){
		    Ext.Msg.wait('loading');   	
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/GetServices.do?act=getDedlGeneralDataList',
				success:function(response,options){						
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					var rsp = response.responseText;
					if(rsp.substring(1,5)!='html'){
						var jsonObject = Ext.JSON.decode(rsp);	
						//Ext.getCmp('nodeIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeIdentifier);
						//Ext.getCmp('nodeName').setValue(jsonObject.getServicesGeneralData[0].nodeName);
						//Ext.getCmp('nodeAddress').setValue(jsonObject.getServicesGeneralData[0].nodeAddress);
						//Ext.getCmp('organizationIdentifier').setValue(jsonObject.getServicesGeneralData[0].organizationIdentifier);
						//Ext.getCmp('nodeContact').setValue(jsonObject.getServicesGeneralData[0].nodeContact);
						//Ext.getCmp('nodeVersionIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeVersionIdentifier);
						//Ext.getCmp('nodeDeploymentTypeCode').setValue(jsonObject.getServicesGeneralData[0].nodeDeploymentTypeCode);
						//Ext.getCmp('nodeStatus').setValue(jsonObject.getServicesGeneralData[0].nodeStatus);
						//Ext.getCmp('north').setValue(jsonObject.getServicesGeneralData[0].north);
						//Ext.getCmp('south').setValue(jsonObject.getServicesGeneralData[0].south);
						//Ext.getCmp('east').setValue(jsonObject.getServicesGeneralData[0].east);
						//Ext.getCmp('west').setValue(jsonObject.getServicesGeneralData[0].west);
						//loadGetServicesNodePropertyStore();
						loadDedlGeneralStore();
						loadDedlPropertyStore();
						loadDedlElementValueStore();						
					}else Ext.Msg.alert('Message', rsp);
				},		
				failure:function(){
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					Ext.Msg.alert('Message', "Fail to get DEDL general data.");												
				}
			})
      }
   });

   var getServicesNodePropertyAddBtn = new Ext.Button({
            text: 'Add Record',
            handler : function(){
                var r = Ext.create('GetServicesNodePropertyRecord',{
                    nodePropertyName: '',
                    nodePropertyValue: ''
                });
                getServicesNodePropertyStore.insert(0, r);
            }
        });
        
   var dedlGeneralAddBtn = new Ext.Button({
            text: 'Add Record',
            handler : function(){
                var r = Ext.create('DedlGeneralRecord',{
                	elementIdentifier: '',
                	applicationDomain: '',
                	elementType: '',
                	description: '',
                	keywords: '',
                   	owner: '',
                   	elementLabel: '',
                   	defaultValue: '',
                   	upperLimit: '',
                   	lowerLimit: '',
                   	allowMultiSelect: '',
                   	additionalValuesIndicator: '',
                   	optionality: '',
                   	wildcard: '',
                   	formatString: '',
                   	validationRules: '',
                   	dataSourceType: '',
                   	connectionDescriptor: '',
                   	accessStatement: '',
                   	parameters: '',
                   	transformation: ''
                });
                dedlGeneralStore.insert(0, r);
            }
        });
        
   var dedlPropertyAddBtn = new Ext.Button({
            text: 'Add Record',
            handler : function(){
				var records = dedlGeneralCheckBoxSM.getSelection();
				if(records!=null && records !='' && records.length == 1){
	                var r = Ext.create('DedlPropertyRecord',{	                	
	                    propertyElementID: records[0].get('elementIdentifier'),
	                    propertyName: '',
	                    propertyValue: ''
	                });
	                dedlPropertyStore.insert(0, r);
				}else{
					Ext.Msg.alert('Message', "Please select one Data Element Definition.");																				
				}
            }
        });
        
   var deleElementValueAddBtn = new Ext.Button({
            text: 'Add Record',
            handler : function(){
				var records = dedlGeneralCheckBoxSM.getSelection();
				if(records!=null && records !='' && records.length == 1){
	                var r = Ext.create('DedlElementValueRecord',{
	                	elementElementID: records[0].get('elementIdentifier'),
	                	elementValue: '',
	                	elementValueLabel: ''
	                });
	                dedlElementValueStore.insert(0, r);
				}else{
					Ext.Msg.alert('Message', "Please select one Data Element Definition.");																				
				}
            }
        });
        
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
	}
	
	var createJsonRecord = function(record,paraNameList){
		var jsonData = '{';
		for(var j = 0; j < paraNameList.length; j++){
			if(j==0) jsonData = jsonData + '\"' + paraNameList[j] + '\":' + '\"' + record.get(paraNameList[j]) + '\"';				
			else jsonData = jsonData + ',\"' + paraNameList[j] + '\":' + '\"' + record.get(paraNameList[j]) + '\"';				
		}
		return jsonData = jsonData + '}';
	}
    
    var createJsonString = function(paraNameList,paraValueList){
    	var jsonData = '{';
		for(var j = 0; j < paraNameList.length; j++){
			if(j==0) jsonData = jsonData + '\"' + paraNameList[j] + '\":' + '\"' + paraValueList[j] + '\"';				
			else jsonData = jsonData + ',\"' + paraNameList[j] + '\":' + '\"' + paraValueList[j] + '\"';				
		} 
		return jsonData = jsonData + '}';    	
    }
       
   var getServicesIdentificationPanelSaveBtn = new Ext.Button(
           {
	  		id:'getServicesIdentificationPanelSaveBtn',
	 		text:'Save',
	 		type:'submit',
	 		handler:function (){
				saveAll();
			}
        });

   var dedlElementValueGridSaveBtn = new Ext.Button(
           {
	  		id:'dedlElementValueGridSaveBtn',
	 		text:'Save',
	 		type:'submit',
	 		handler:function (){
				var records = dedlGeneralCheckBoxSM.getSelection();
				if(records!=null && records !=''){
					saveDedlAll();
				}else{
					Ext.Msg.alert('Message', "Please select one Data Element Definition.");																				
				}
			}
        });

   var getServicesNodePropertyDeleteBtn = new Ext.Button({id:'getServicesNodePropertyDeleteBtn',
			    		text:'Remove Selected',
			    		type:'submit',
			    		handler:function (){
        					Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these Node properties ?', function(flag){
        						if(flag == 'yes'){
    			    				// get records
    								var records = getServicesNodePropertyCheckBoxSM.getSelection();
    								if(records!=null && records !=''){
    									for(var i = 0; i < records.length; i++){
    										getServicesNodePropertyStore.remove(records[i]);									 
    									}
    								}else{
    									Ext.Msg.alert('Message', "Please select Node Property.");																				
    								}        							
        						}
							});	        					        	
		        		}
		        	});

   var dedlGeneralDeleteBtn = new Ext.Button({id:'dedlGeneralDeleteBtn',
			    		text:'Remove Selected',
			    		type:'submit',
			    		handler:function (){
        					Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these data element. ?', function(flag){
        						if(flag == 'yes'){
    			    				// get records
    								var records = dedlGeneralCheckBoxSM.getSelection();
    								if(records!=null && records !=''){
    									for(var i = 0; i < records.length; i++){
    										dedlGeneralStore.remove(records[i]);									 
    									}
    									// Remove all dedlProperty and dedlElementValue
										dedlPropertyStore.removeAll();									        								
										dedlElementValueStore.removeAll();									         								
        							}else{
    									Ext.Msg.alert('Message', "Please select Node Services.");																				
    								}        							
        						}
							});	        					        	
		        		}
		        	});

   var dedlPropertyDeleteBtn = new Ext.Button({id:'dedlPropertyDeleteBtn',
			    		text:'Remove Selected',
			    		type:'submit',
			    		handler:function (){
        					Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these data element properties ?', function(flag){
        						if(flag == 'yes'){
    			    				// get records
    								var records = dedlPropertyCheckBoxSM.getSelection();
    								var elementIdentifier = "";
    								if(records!=null && records !=''){
    									elementIdentifier = records[0].get("propertyElementID")
    									for(var i = 0; i < records.length; i++){
    										dedlPropertyStore.remove(records[i]);									 
    									}
    									if(dedlPropertyStore.getCount()==0){	// add one empty record to create a relation between General and Property
    										var records = dedlPropertyCheckBoxSM.getSelection();
    										if(records==null || records ==''){
    							                var r = Ext.create('DedlPropertyRecord',{
    							                    propertyElementID: elementIdentifier,
    							                    propertyName: '',
    							                    propertyValue: ''
    							                });
    							                dedlPropertyStore.insert(0, r);
    										}										
    									}
    								}else{
    									Ext.Msg.alert('Message', "Please select data element properties.");																				
    								}        							
        						}
							});	        					        	
		        		}
		        	});

   	var dedlElementValueDeleteBtn = new Ext.Button({id:'dedlElementValueDeleteBtn',
			    		text:'Remove Selected',
			    		type:'submit',
			    		handler:function (){
        					Ext.Msg.confirm('Confirm', 'Are you sure you want to delete these data element values ?', function(flag){
        						if(flag == 'yes'){
    			    				// get records
    								var records = dedlElementValueCheckBoxSM.getSelection();
    								var elementIdentifier = "";
    								if(records!=null && records !=''){
    									elementIdentifier = records[0].get("elementElementID");
    									for(var i = 0; i < records.length; i++){
    										dedlElementValueStore.remove(records[i]);									 
    									}
    									if(dedlElementValueStore.getCount()==0){	// add one empty record to create a relation between General and ElementValue
    										var records = dedlElementValueCheckBoxSM.getSelection();
    										if(records==null || records ==''){
    							                var r = Ext.create('DedlElementValueRecord',{
    							                	elementElementID: elementIdentifier,
    							                	elementValue: '',
    							                	elementValueLabel: ''
    							                });
    							                dedlElementValueStore.insert(0, r);
    										}
    									}
    								}else{
    									Ext.Msg.alert('Message', "Please select data element values.");																				
    								}        							
        						}
							});	        					        	
		        		}
		        	});

    var getServicesNodePropertyCheckBoxSM = Ext.create('Ext.selection.CheckboxModel'); 
    var getServicesNodePropertyGrid = Ext.create('Ext.grid.Panel',{
		id: 'getServicesNodePropertyGrid',
		title :'Node Properties',
		region:'center',
        store: getServicesNodePropertyStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,
        autoheight:true,
        width: 400,
        selModel:getServicesNodePropertyCheckBoxSM,
        plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit: 1
        })],

        // grid columns
        columns:[
        {
        	id:'NodeProperty_Name',
            header: 'Node Property Name',
	        width: 150,
	        dataIndex: 'nodePropertyName',
			editor: new Ext.form.TextField({
               allowBlank: false
			}),
  			sortable: true
        },{
        	id:'NodeProperty_Value',
            header: 'Node Property Value',
	        width: 150,
	        dataIndex: 'nodePropertyValue',
			editor: new Ext.form.TextField({
               allowBlank: false
			}),
 			sortable: true
        }],

       // paging bar on the bottom
        /*bbar: new Ext.PagingToolbar({
	    	id:'getServicesNodePropertyPagingBar',
	        pageSize: 200,
	        store: getServicesNodePropertyStore,
	        displayInfo: true,
	        displayMsg: 'Total:{2}',
	        emptyMsg: "Total:0"
	    }),*/
	   // new Document button on the top
        buttons: [getServicesRefreshBtn,getServicesIdentificationPanelSaveBtn
        ,getServicesNodePropertyAddBtn,getServicesNodePropertyDeleteBtn]
    });

    var dedlGeneralCheckBoxSM = Ext.create('Ext.selection.CheckboxModel');
    var dedlGeneralGrid = Ext.create('Ext.grid.Panel',{
		id: 'dedlGeneralGrid',
		title :'Data Element General Information',
		region:'north',
		height:150,
        store: dedlGeneralStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,
        autoheight:true,
        selModel: dedlGeneralCheckBoxSM,
        plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit: 1
        })],
		listeners: {
			'itemclick':function(grid, row, e){
				var records = dedlGeneralCheckBoxSM.getSelection();
				if(records!=null || records.length > 0){
					// get property	
					dedlPropertyParams.dedlElementIdentifier=records[0].get("elementIdentifier");
					loadDedlPropertyStore();
					// get parameters
					getServicesNodeServicesParametersParams.dedlElementIdentifier=records[0].get("elementIdentifier");
					loadDedlElementValueStore();
					
				}
			}
         }, 	                    

        // grid columns
        columns:[
        {
        	id:'Element_Identifier',
            header: 'ElementIdentifier',
	        width: 100,
	        dataIndex: 'elementIdentifier',
			editor: new Ext.form.TextField({
               allowBlank: false
			}),
 			sortable: true
        },{
        	id:'Element_ApplicationDomain',
            header: 'ApplicationDomain',
	        width: 100,
	        dataIndex: 'applicationDomain',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
 			sortable: true
        },{
        	id:'Element_Type',
            header: 'ElementType',
	        width: 100,
	        dataIndex: 'elementType',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
 			sortable: true
        },{
        	id:'Element_Description',
            header: 'Description',
	        width: 100,
	        dataIndex: 'description',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
 			sortable: true
        },{
        	id:'Element_Keywords',
            header: 'Keywords',
	        width: 100,
	        dataIndex: 'keywords',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_Owner',
            header: 'Owner',
	        width: 110,
	        dataIndex: 'owner',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_Label',
            header: 'ElementLabel',
	        width: 120,
	        dataIndex: 'elementLabel',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_DefaultValue',
            header: 'DefaultValue',
	        width: 120,
	        dataIndex: 'defaultValue',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_UpperLimit',
            header: 'UpperLimit',
	        width: 120,
	        dataIndex: 'upperLimit',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_LowerLimit',
            header: 'LowerLimit',
	        width: 120,
	        dataIndex: 'lowerLimit',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_AllowMultiSelect',
            header: 'AllowMultiSelect',
	        width: 100,
	        dataIndex: 'allowMultiSelect',
			editor: /*new Ext.form.TextField({
               allowBlank: false
			})*/
			new Ext.form.field.ComboBox({
       				id:'Element_AllowMultiSelect_Combox',
			        store: new Ext.data.Store({
			            fields: ['AllowMultiSelect'],
			            data : RequiredData
			        }),
                    displayField:'AllowMultiSelect',
                    mode: 'local',
                    allowBlank:true,
				  	editable:false,
				    triggerAction: 'all',
				    selectOnFocus:true
			}),
  			sortable: true
        },{
        	id:'Element_AdditionalValuesIndicator',
            header: 'AdditionalValuesIndicator',
	        width: 100,
	        dataIndex: 'additionalValuesIndicator',
			editor: /*new Ext.form.TextField({
               allowBlank: false
			})*/
			new Ext.form.field.ComboBox({
       				id:'Element_AdditionalValuesIndicator_Combox',
			        store: new Ext.data.Store({
			            fields: ['AdditionalValuesIndicator'],
			            data : [
		 	                    {'AdditionalValuesIndicator':'true'}, 
		 	                    {'AdditionalValuesIndicator':'false'}
		 	                   ]
			        }),
                    displayField:'AdditionalValuesIndicator',
                    mode: 'local',
                    allowBlank:true,
				  	editable:false,
				    triggerAction: 'all',
				    selectOnFocus:true
			}),
  			sortable: true
        },{
        	id:'Element_Optionality',
            header: 'Optionality',
	        width: 100,
	        dataIndex: 'optionality',
			editor: /*new Ext.form.TextField({
               allowBlank: false
			})*/
			new Ext.form.field.ComboBox({
       				id:'Element_Optionality_Combox',
			        store: new Ext.data.Store({
			            fields: ['Optionality'],
			            data : [
		 	                    {'Optionality':'true'}, 
		 	                    {'Optionality':'false'}
		 	                   ]
			        }),
                    displayField:'Optionality',
                    mode: 'local',
                    allowBlank:true,
				  	editable:false,
				    triggerAction: 'all',
				    selectOnFocus:true
			}),
  			sortable: true
        },{
        	id:'Element_Wildcard',
            header: 'Wildcard',
	        width: 120,
	        dataIndex: 'wildcard',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_FormatString',
            header: 'FormatString',
	        width: 120,
	        dataIndex: 'formatString',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_ValidationRules',
            header: 'ValidationRules',
	        width: 120,
	        dataIndex: 'validationRules',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_DataSourceType',
            header: 'DataSourceType',
	        width: 100,
	        dataIndex: 'dataSourceType',
			editor: /*new Ext.form.TextField({
               allowBlank: false
			})*/
			new Ext.form.field.ComboBox({
       				id:'Element_DataSourceType_Combox',
			        store: new Ext.data.Store({
			            fields: ['DataSourceType'],
			            data : Element_DataSourceType
			        }),
                    displayField:'DataSourceType',
                    mode: 'local',
                    allowBlank:true,
				  	editable:false,
				    triggerAction: 'all',
				    selectOnFocus:true
			}),
  			sortable: true
        },{
        	id:'Element_ConnectionDescriptor',
            header: 'ConnectionDescriptor',
	        width: 120,
	        dataIndex: 'connectionDescriptor',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_AccessStatement',
            header: 'AccessStatement',
	        width: 280,
	        dataIndex: 'accessStatement',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_Parameters',
            header: 'Parameters',
	        width: 120,
	        dataIndex: 'parameters',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_Transformation',
            header: 'Transformation',
	        width: 120,
	        dataIndex: 'transformation',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        }],

       /* paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'getServicesNodeServicesPagingBar',
	        pageSize: 200,
	        store: dedlGeneralStore,
	        displayInfo: true,
	        displayMsg: 'Total:{2}',
	        emptyMsg: "Total:0"
	    }),*/
	   // new Document button on the top
        buttons: [dedlGeneralAddBtn,dedlGeneralDeleteBtn]
    });

    var dedlPropertyCheckBoxSM = Ext.create('Ext.selection.CheckboxModel'); 
    var dedlPropertyGrid = new Ext.grid.Panel({
		id: 'dedlPropertyGrid',
		title :'Data Element Definition Properties',
		region:'center',
		height:150,
        store: dedlPropertyStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,
        autoheight:true,
        selModel:dedlPropertyCheckBoxSM,
        plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit: 1
        })],

        // grid columns
        columns:[
        {
        	id:'Element_PropertyName',
            header: 'Property Name',
	        width: 150,
	        dataIndex: 'propertyName',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_PropertyValue',
            header: 'Property Value',
	        width: 150,
	        dataIndex: 'propertyValue',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
 			sortable: true
        }],

       /* paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'getServicesNodeServicesPropertyPagingBar',
	        pageSize: 200,
	        store: dedlPropertyStore,
	        displayInfo: true,
	        displayMsg: 'Total:{2}',
	        emptyMsg: "Total:0"
	    }),*/
	   // new Document button on the top
        buttons: [dedlPropertyAddBtn,dedlPropertyDeleteBtn]
    });

    var dedlElementValueCheckBoxSM = Ext.create('Ext.selection.CheckboxModel'); 
    var dedlElementValueGrid = new Ext.grid.Panel({
		id: 'dedlElementValueGrid',
		title :'Data Element Definition Element Values',
		region:'south',
		height:150,
        store: dedlElementValueStore,
        trackMouseOver:true,
        disableSelection:false,
        autoScroll:true,
        autoheight:true,
        selModel:dedlElementValueCheckBoxSM,
        listeners: {
			'edit':function(e){
	   			validateParameters(e);
			}
	    }, 	                    
        plugins: [Ext.create('Ext.grid.plugin.CellEditing', {
            clicksToEdit: 1
        })],

        // grid columns
        columns:[
        {
        	id:'Element_ElementValue',
            header: 'Element Value',
	        width: 130,
	        dataIndex: 'elementValue',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        },{
        	id:'Element_ElementValueLabel',
            header: 'Element Value Label',
	        width: 130,
	        dataIndex: 'elementValueLabel',
			editor: new Ext.form.TextField({
               allowBlank: true
			}),
  			sortable: true
        }],

       /* paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'getServicesNodeServicesParametersPagingBar',
	        pageSize: 200,
	        store: dedlElementValueStore,
	        displayInfo: true,
	        displayMsg: 'Total:{2}',
	        emptyMsg: "Total:0"
	    }),*/
	   // new Document button on the top
        buttons: [getDedlRefreshBtn,dedlElementValueGridSaveBtn,deleElementValueAddBtn,dedlElementValueDeleteBtn]
    });

	// Create Identification panel
    var getServicesIdentificationPanel = new Ext.FormPanel({
    	id:'getServicesIdentificationPanel',
		region:'north',
        labelWidth: 250, // label settings here cascade unless overridden
        bodyStyle:'padding:5px 0px 0px 5px',
        frame:true,
        title: 'Node Identification',
        defaults: {width: 300},
		height: 360,
        autoScroll:true,
        autoheight:true,
        defaultType: 'textfield',
		listeners: {
			'beforerender':function(cmp){
				//get data  
				Ext.Ajax.request({
					url:'/Node.Administration/Page/Entry/GetServices.do?act=getGeneralData',
					success:function(response,options){						
						var rsp = response.responseText;
						if(rsp.substring(1,5)!='html'){
							var jsonObject = Ext.JSON.decode(rsp);							
							Ext.getCmp('nodeIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeIdentifier);
							Ext.getCmp('nodeName').setValue(jsonObject.getServicesGeneralData[0].nodeName);
							Ext.getCmp('nodeAddress').setValue(jsonObject.getServicesGeneralData[0].nodeAddress);
							Ext.getCmp('organizationIdentifier').setValue(jsonObject.getServicesGeneralData[0].organizationIdentifier);
							Ext.getCmp('nodeContact').setValue(jsonObject.getServicesGeneralData[0].nodeContact);
							Ext.getCmp('nodeVersionIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeVersionIdentifier);
							Ext.getCmp('nodeDeploymentTypeCode').setValue(jsonObject.getServicesGeneralData[0].nodeDeploymentTypeCode);
							Ext.getCmp('nodeStatus').setValue(jsonObject.getServicesGeneralData[0].nodeStatus);
							Ext.getCmp('north').setValue(jsonObject.getServicesGeneralData[0].north);
							Ext.getCmp('south').setValue(jsonObject.getServicesGeneralData[0].south);
							Ext.getCmp('east').setValue(jsonObject.getServicesGeneralData[0].east);
							Ext.getCmp('west').setValue(jsonObject.getServicesGeneralData[0].west);
							loadGetServicesNodePropertyStore();
							loadDedlGeneralStore();						
						}
					},		
					failure:function(){
						Ext.Msg.alert('Message', "Fail to get general data.");												
					}
				})
			}
         }, 	                    
        items: [{
                	id:'nodeIdentifier',
                    fieldLabel: 'Node Identifier*',
                    name: 'nodeIdentifier'
                },{
                	id:'nodeName',
                    fieldLabel: 'Node Name',
                    name: 'nodeName'
                },{
                	id:'nodeAddress',
                    fieldLabel: 'Node Address(URL)',
                    name: 'nodeAddress'
                },{
                	id:'organizationIdentifier',
                    fieldLabel: 'Organization Identifier',
                    name: 'organizationIdentifier'
                },{
                	id:'nodeContact',
                    fieldLabel: 'Node Contact',
                    name: 'nodeContact'
                },new Ext.form.field.ComboBox({
	        				id:'nodeVersionIdentifier',
	                        fieldLabel: 'Node Version Identifier',
					        store: new Ext.data.Store({
					            fields: ['nodeVersionIdentifier'],
					            data : nodeVersionIdentifierData
					        }),
                         	displayField:'nodeVersionIdentifier',
                        	mode: 'local',
                        	allowBlank:true,
						    editable:false,
						    triggerAction: 'all',
						    emptyText:'Select a Version...',
						    selectOnFocus:true
					})
				,new Ext.form.field.ComboBox({
	        				id:'nodeDeploymentTypeCode',
	                        fieldLabel: 'Node Deployment Type Code',
					        store: new Ext.data.Store({
					            fields: ['nodeDeploymentTypeCode'],
					            data : nodeDeploymentTypeCodeData
					        }),
                         	displayField:'nodeDeploymentTypeCode',
                        	mode: 'local',
                        	allowBlank:true,
						    editable:false,
						    triggerAction: 'all',
						    emptyText:'Select a Node Deployment Type...',
						    selectOnFocus:true
					})
				,new Ext.form.field.ComboBox({
	        				id:'nodeStatus',
	                        fieldLabel: 'Node Status',
					        store: new Ext.data.Store({
					            fields: ['nodeStatus'],
					            data : nodeStatusData
					        }),
                         	displayField:'nodeStatus',
                        	mode: 'local',
                        	allowBlank:true,
						    editable:false,
						    triggerAction: 'all',
						    emptyText:'Select a Node Status...',
						    selectOnFocus:true
					})
                ,{
                	id:'north',
                    fieldLabel: 'Bounding Coordinate North (longitude)',
                    name: 'north'
                },{
                	id:'south',
                    fieldLabel: 'Bounding Coordinate South (longitude)',
                    name: 'south'
                },{
                	id:'east',
                    fieldLabel: 'Bounding Coordinate East (latitude)',
                    name: 'east'
                },{
                	id:'west',
                    fieldLabel: 'Bounding Coordinate West (latitude)',
                    name: 'west'
                }]
    });
              
	// Create Identification panel
 /*   var getServicesNodeServicesPanel = new Ext.FormPanel({
    	id:'getServicesNodeServicesPanel',
		region:'center',
        labelWidth: 200, // label settings here cascade unless overridden
        bodyStyle:'padding:5px 0px 0px 5px',
        frame:true,
        title: 'Node Identification',
        defaults: {width: 300},
		height: 360,
        defaultType: 'textfield',
		listeners: {
			'beforerender':function(cmp){
				//get data  
				Ext.Ajax.request({
					url:'/Node.Administration/Page/Entry/GetServices.do?act=getNodeServicesData',
					success:function(response,options){						
						var rsp = response.responseText;
						var jsonObject = Ext.JSON.decode(rsp);							
						Ext.getCmp('methodName').setValue(jsonObject.getServicesNodeServicesData[0].methodName);
						Ext.getCmp('dataFlow').setValue(jsonObject.getServicesNodeServicesData[0].dataFlow);
						Ext.getCmp('serviceIdentifier').setValue(jsonObject.getServicesNodeServicesData[0].serviceIdentifier);
						Ext.getCmp('serviceDescription').setValue(jsonObject.getServicesNodeServicesData[0].serviceDescription);
						Ext.getCmp('serviceDocumentURL').setValue(jsonObject.getServicesNodeServicesData[0].serviceDocumentURL);
						Ext.getCmp('servicePropertyName').setValue(jsonObject.getServicesNodeServicesData[0].servicePropertyName);
						Ext.getCmp('servicePropertyValue').setValue(jsonObject.getServicesNodeServicesData[0].servicePropertyValue);
						Ext.getCmp('styleSheetURL').setValue(jsonObject.getServicesNodeServicesData[0].styleSheetURL);
						loadGetServicesNodePropertyStore();
					},		
					failure:function(){
						Ext.Msg.alert('Message', "Fail to get Node services data.");												
					}
				})
			}
         }, 	                    
        items: [{
                	id:'methodName',
                    fieldLabel: 'Method Name*',
                    name: 'methodName'
                },{
                	id:'dataFlow',
                    fieldLabel: 'Data Flow',
                    name: 'dataFlow'
                },{
                	id:'serviceIdentifier',
                    fieldLabel: 'Service Identifier',
                    name: 'serviceIdentifier'
                },{
                	id:'serviceDescription',
                    fieldLabel: 'Service Description',
                    name: 'serviceDescription'
                },{
                	id:'serviceDocumentURL',
                    fieldLabel: 'Service Document URL',
                    name: 'serviceDocumentURL'
                },{
                	id:'servicePropertyName',
                    fieldLabel: 'Service Property Name',
                    name: 'servicePropertyName'
                },{
                	id:'servicePropertyValue',
                    fieldLabel: 'Service Property Value',
                    name: 'servicePropertyValue'
                },{
                	id:'styleSheetURL',
                    fieldLabel: 'Style Sheet URL',
                    name: 'styleSheetURL'
                }]
    });
*/              
		var getServicesGeneralInformation = {
	           id: "getServicesGeneralInformation",
	           title: 'Node General Information',
	           layout:'border',
	           autoScroll:true,
	           items: [getServicesIdentificationPanel,getServicesNodePropertyGrid]
		     };
		     
		var dedlData = {
	              id: "dedlData",
	              title: 'Data Element Definition',
		          layout:'border',
	              autoScroll:true,
		          items: [dedlGeneralGrid,dedlPropertyGrid,dedlElementValueGrid]
	     	};
	     	
		var getServicesUpload = {
	              id: "getServicesUpload",
	              title: 'Node Metadata File Upload',
		          layout:'fit',
	              autoScroll:true,
		          items: [getServicesUploadIframe]
	     	};
		              
		var getServicesDownload = {
	              id: "getServicesDownload",
	              title: 'Node Metadata File Download',
		          layout:'fit',
	              autoScroll:true,
		          items: [getServicesDownloadIframe]
	     	};

		var getServicesCenterPanel = new Ext.TabPanel({
		              id:'getServicesCenterPanel',
		              deferredRender:false,
		              activeTab:0,
		              layoutOnTabChange: true,
		              items:[getServicesGeneralInformation,getServicesDownload,dedlData]
		    });
		      
    // trigger the list data store load
    var loadGetServicesNodePropertyStore=function(){
    	getServicesNodePropertyStore.load({
//	    				callback:function(r,options,success){
//		    				if(!success){
//		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
//								Ext.Ajax.request({
//									url:'/Node.Administration/Page/Entry/GetServices.do?act=getNodePropertyList',
//									success:function(response,options){						
//										var rsp = response.responseText;							
//					            		Ext.Msg.alert('Message', rsp);	        		
//									},		
//									failure:function(){
//										Ext.Msg.alert('Message', "Fail to access database.");												
//									}
//								})		    					    				
//		    				}
//	    				}
	    });
    }
    
 /*   var loadDedlGeneralStoreInitial=function(){
    	dedlGeneralStore.load({
	    				callback:function(r,options,success){
		    				if(!success){
								Ext.Msg.alert('Message', "Fail to get Node Services data.");												
		    				}else{
								if(r!=0){
									dedlPropertyParams.dedlElementIdentifier=r[0].get("dataFlow");
									dedlPropertyGrid.baseParams=dedlPropertyParams;						
									getServicesNodeServicesParametersParams.dedlElementIdentifier=r[0].get("dataFlow");
									dedlElementValueGrid.baseParams=getServicesNodeServicesParametersParams;
								}						
								loadDedlPropertyStore();
								loadDedlElementValueStore();		    				
		    				}
	    				}
	    });
    }
    */
    
    var loadDedlGeneralStore=function(){
    	dedlGeneralStore.load({
//	    				callback:function(r,options,success){
//		    				if(!success){		    				
//		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
//								Ext.Ajax.request({
//									url:'/Node.Administration/Page/Entry/GetServices.do?act=getNodeServicesDataList',
//									success:function(response,options){						
//										var rsp = response.responseText;							
//					            		Ext.Msg.alert('Message', rsp);	        		
//									},		
//									failure:function(){
//										Ext.Msg.alert('Message', "Fail to access database.");												
//									}
//								})		    					    				
//		    				}
//	    				}
	    });
    }

    var loadDedlPropertyStore=function(){
    	dedlPropertyStore.load({
//	    				callback:function(r,options,success){
//		    				if(!success){
//		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
//								Ext.Ajax.request({
//									url:'/Node.Administration/Page/Entry/GetServices.do?act=getNodeServicesPropertyDataList',
//									success:function(response,options){						
//										var rsp = response.responseText;							
//					            		Ext.Msg.alert('Message', rsp);	        		
//									},		
//									failure:function(){
//										Ext.Msg.alert('Message', "Fail to access database.");												
//									}
//								})		    					    				
//		    				}
//	    				}
	    });
    }

    var loadDedlElementValueStore=function(){
    	dedlElementValueStore.load({
//	    				callback:function(r,options,success){
//							if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');
//		    				if(!success){
//								Ext.Ajax.request({
//									url:'/Node.Administration/Page/Entry/GetServices.do?act=getNodeServicesParametersDataList',
//									success:function(response,options){						
//										var rsp = response.responseText;							
//					            		Ext.Msg.alert('Message', rsp);	        		
//									},		
//									failure:function(){
//										Ext.Msg.alert('Message', "Fail to access database.");												
//									}
//								})		    					    				
//		    				}
//	    				}
	    });
    }

// open favoriteLinks window    
   var openGetServicesWin=function(){
	   // WI 21296
	   //if(version==ver_2){
			if(!getServicesWin){
				getServicesWin = new Ext.Window({
					id:'getServicesWin',
				    layout:'fit',
					width       : 700,
					height      : 560,
					minWidth 	: 300,
					miniHeight	: 300,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain       : true,
					constrainHeader:true,
					items : [getServicesCenterPanel]
				});
			}
			getServicesWin.show('favoriteLinksPortlet');
			
		    Ext.Msg.wait('loading');   	
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/GetServices.do?act=getGeneralData',
				success:function(response,options){						
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					var rsp = response.responseText;
					if(rsp.substring(1,5)!='html'){
						var jsonObject = Ext.JSON.decode(rsp);	
						Ext.getCmp('nodeIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeIdentifier);
						Ext.getCmp('nodeName').setValue(jsonObject.getServicesGeneralData[0].nodeName);
						Ext.getCmp('nodeAddress').setValue(jsonObject.getServicesGeneralData[0].nodeAddress);
						Ext.getCmp('organizationIdentifier').setValue(jsonObject.getServicesGeneralData[0].organizationIdentifier);
						Ext.getCmp('nodeContact').setValue(jsonObject.getServicesGeneralData[0].nodeContact);
						Ext.getCmp('nodeVersionIdentifier').setValue(jsonObject.getServicesGeneralData[0].nodeVersionIdentifier);
						Ext.getCmp('nodeDeploymentTypeCode').setValue(jsonObject.getServicesGeneralData[0].nodeDeploymentTypeCode);
						Ext.getCmp('nodeStatus').setValue(jsonObject.getServicesGeneralData[0].nodeStatus);
						Ext.getCmp('north').setValue(jsonObject.getServicesGeneralData[0].north);
						Ext.getCmp('south').setValue(jsonObject.getServicesGeneralData[0].south);
						Ext.getCmp('east').setValue(jsonObject.getServicesGeneralData[0].east);
						Ext.getCmp('west').setValue(jsonObject.getServicesGeneralData[0].west);
						loadGetServicesNodePropertyStore();
						loadDedlGeneralStore();
						loadDedlPropertyStore();
						loadDedlElementValueStore();						
					}else Ext.Msg.alert('Message', rsp);
				},		
				failure:function(){
					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
					Ext.Msg.alert('Message', "Fail to get general data.");												
				}
			})
	   /*}else if(version==ver_1){
	   		Ext.Msg.alert('Message', 'This is Node 2.0 function.');
	   }*/
   };
   
		var saveAll = function (){
 			var jsonObject = null;
 			var NodeGeneralDataNameList = ['nodeIdentifier','nodeName','nodeAddress','organizationIdentifier','nodeContact','nodeVersionIdentifier','nodeDeploymentTypeCode','nodeStatus','north','south','east','west'];
 			var NodeGeneralDataValueList = [Ext.getCmp('nodeIdentifier').getValue(),Ext.getCmp('nodeName').getValue(),Ext.getCmp('nodeAddress').getValue(),Ext.getCmp('organizationIdentifier').getValue(),Ext.getCmp('nodeContact').getValue(),Ext.getCmp('nodeVersionIdentifier').getValue(),Ext.getCmp('nodeDeploymentTypeCode').getValue(),Ext.getCmp('nodeStatus').getValue(),Ext.getCmp('north').getValue(),Ext.getCmp('south').getValue(),Ext.getCmp('east').getValue(),Ext.getCmp('west').getValue()];
 			var NodePropertyParaNameList = ['nodePropertyName','nodePropertyValue'];
 			var NodeServicesParaNameList = ['methodName','dataFlow','serviceIdentifier','serviceDescription','serviceDocumentURL','styleSheetURL'];
 			var NodeServicesPropertyParaNameList = ['nodeServicesPropertyDataFlow','nodeServicesPropertyName','nodeServicesPropertyValue'];
 			var NodeServicesParametersParaNameList = ['nodeServicesParametersDataFlow','nodeServicesParametersName','nodeServicesParametersValue','nodeServicesParametersSortIndex','nodeServicesParametersOccurenceNo','nodeServicesParametersEncoding','nodeServicesParametersType','nodeServicesParametersTypeDesc','nodeServicesParametersRequired'];
 			
 			var NodePropertyList = 'NodePropertyList';
 			var NodeServicesList = 'NodeServicesList';
 			var NodeServicesPropertyList = 'NodeServicesPropertyList';
 			var NodeServicesParametersList = 'NodeServicesParametersList';
 			
 			var generalDataJson = createJsonString(NodeGeneralDataNameList,NodeGeneralDataValueList);
 			var NodePropertyJson = createJsonList(getServicesNodePropertyStore,NodePropertyList,NodePropertyParaNameList);
 			var NodeServicesJson = createJsonList(dedlGeneralStore,NodeServicesList,NodeServicesParaNameList);
 			var NodeServicesPropertyJson = createJsonList(dedlPropertyStore,NodeServicesPropertyList,NodeServicesPropertyParaNameList);
 			var NodeServicesParametersJson = createJsonList(dedlElementValueStore,NodeServicesParametersList,NodeServicesParametersParaNameList);
 									
			var dataStr = '{\"NodeGeneralData\":' + generalDataJson + ',' 
			+ NodePropertyJson + ',' + NodeServicesJson + ',' + NodeServicesPropertyJson + ','
			+ NodeServicesParametersJson + '}';
        	//send back  
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/GetServices.do?act=saveRegistrationData',
				params:{
					dataString:dataStr
				},
				success:function(response,options){
					var rsp = response.responseText;							
            		Ext.Msg.alert('Message', rsp);	        		
				},		
				failure:function(){
					Ext.Msg.alert('Message', 'Fail to save Registration file.');												
				}
			});
		};
			
		var saveDedlAll = function (){
			var jsonObject = null;
			var dedlGeneralList = ['elementIdentifier','applicationDomain','elementType','description','keywords','owner','elementLabel','defaultValue','upperLimit','lowerLimit','allowMultiSelect','additionalValuesIndicator','optionality','wildcard','formatString','validationRules','dataSourceType','connectionDescriptor','accessStatement','parameters','transformation'];
			var dedlProperyList = ['propertyElementID','propertyName','propertyValue'];
			var dedlElementValueList = ['elementElementID','elementValue','elementValueLabel'];
			
			var dedlGeneralListName = 'dedlGeneralList';
			var dedlProperyListName = 'dedlProperyList';
			var dedlElementValueListName = 'dedlElementValueList';
			
			var dedlGeneralListJson = createJsonList(dedlGeneralStore,dedlGeneralListName,dedlGeneralList);
			var dedlProperyListJson = createJsonList(dedlPropertyStore,dedlProperyListName,dedlProperyList);
			var dedlElementValueListJson = createJsonList(dedlElementValueStore,dedlElementValueListName,dedlElementValueList);

			var dataStr = '{' 
			+ dedlGeneralListJson + ',' + dedlProperyListJson + ',' + dedlElementValueListJson
			+ '}';
			//send back  
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/GetServices.do?act=saveDedlData',
				params:{
					dataString:dataStr
				},
				success:function(response,options){
					var rsp = response.responseText;							
		    		Ext.Msg.alert('Message', rsp);	        		
				},		
				failure:function(){
					Ext.Msg.alert('Message', 'Fail to save Dedl file.');												
				}
			});
		};
				
		var validateParameters = function (e){
			/*var record = e.record;
			var ret = record.get("nodeServicesParametersSortIndex").match(/^\d+$/);
			if(ret == null ){
				Ext.Msg.alert('Message', 'You must input number for Sort Index.');
				record.set("nodeServicesParametersSortIndex",e.originalValue );
				return;
			}
			ret = record.get("nodeServicesParametersOccurenceNo").match(/^\d+$/);
			if(ret == null ){
				Ext.Msg.alert('Message', 'You must input number for Occurence No.');
				record.set("nodeServicesParametersOccurenceNo",e.originalValue );
				return;
			}
			*/
		};
   
