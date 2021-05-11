	
	// Create search panel
    var domainSearch = Ext.create('Ext.FormPanel', {
        labelWidth: 120, // label settings here cascade unless overridden
        frame:true,
        title: 'Search Domain',
        bodyStyle:'padding:5px 5px 0',
        width: 400,
        defaults: {width: 300},
        defaultType: 'textfield',

        items: [new Ext.form.field.ComboBox({
        				id:'domainNameCombox',
                        fieldLabel: 'Domain Name',
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
								{name: 'domainName', type: 'string', mapping: 'domainName'}
					        ],					
					        // load using script tags for cross domain, if the data in on the same domain as
					        // this page, an HttpProxy would be better
					        proxy: {
					            type: 'ajax',
					            url: 'domain.do?method=getDomainNameList',
					            timeout: gridTimeout,
					            reader: {
					                type: 'json',
					                totalProperty: 'total',
					                root: 'results'
					            }
					        }
                        }),
                        allowBlank:true,
                        displayField:'domainName',
                        typeAhead: true,
                        mode: 'remote',
                        triggerAction: 'all',
                        emptyText:'Select a Domain Name...',
                        selectOnFocus:true
                    }),new Ext.form.field.ComboBox({
	        				id:'domainStatusCombox',
	                        fieldLabel: 'Status',
					        store: Ext.create('Ext.data.Store',  {
					            fields: [{name:'domainStatus', type: 'string', mapping: 'domainStatus'}],
					            data : domainStatusData
					        }),
                         	displayField:'domainStatus',
                        	mode: 'local',
                        	allowBlank:true,
						    typeAhead: true,
						    triggerAction: 'all',
						    emptyText:'Select a Status...',
						    selectOnFocus:true
					})],
        buttons: [{
 	    		id:'domainSearch',
	    		text:'Search',
	    		type:'submit',
	    		handler:function (){
	    			Ext.Msg.wait('loading'); 
	    			// Reset the pageToolBar for the new search
					if(Ext.getCmp('domainPagingBarSmall')){		    						
						Ext.getCmp('domainPagingBarSmall').moveFirst();
					};
	    			domainStoreList.load({
	    				callback:function(r,options,success){
		    				if(success){
		    	    			// Reset the pageToolBar for the new search
		    					if(Ext.getCmp('domainPagingBarLarge')){		    						
		    						Ext.getCmp('domainPagingBarLarge').moveFirst();
		    					};
				    			domainStore.load({
				    				callback:function(r,options,success){
					    				if(success){
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 	    				
					    					Ext.getCmp('domainSearchWin').hide('domainPortlet');	    				
					    				}else{
					    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
											Ext.Ajax.request({
												url:'domain.do?method=getDomainList',
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
									url:'domain.do?method=getDomainList',
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
    var domainStoreList = Ext.create('Ext.data.Store', {
    	storeId:'domainStoreList',
        remoteSort: true,
        model: Ext.define('DomainStoreListMd', {
            extend: 'Ext.data.Model',
            fields: [
         	        {name: 'domainId', type: 'string', mapping: 'domainId'},
         	        {name: 'domainName', type: 'string', mapping: 'domainName'},
         			{name: 'domainAdmin', type: 'string', mapping: 'domainAdmin'},
         	        {name: 'domainStatusCD', type: 'string', mapping: 'domainStatusCD'},
         			{name: 'domainStatusMSG', type: 'string', mapping: 'domainStatusMSG'}
                 ]
        }),

        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'domain.do?method=getDomainList',
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
	        	domainParams.limit = topNum;
	        	domainParams.page = operation.page;
	        	domainParams.sort = operation.sort;
	        	domainParams.start = operation.start;
					
    			domainParams.domainName=Ext.getCmp('domainNameCombox').getValue();
    			domainParams.domainStatus=Ext.getCmp('domainStatusCombox').getValue();
				
				operation.params = domainParams;
			}
        },
	    sortOnLoad: true,
	    sorters: { property: 'domainName', direction : 'asc' }
    });
//    domainStoreList.setDefaultSort('domainName', 'asc');

    // create the Data Store
    var domainStore = Ext.create('Ext.data.Store', {
    	storeId:'domainStore',
        remoteSort: true,
        model: Ext.define('DomainStoreMd', {
            extend: 'Ext.data.Model',
            fields: [
         	        {name: 'domainId', type: 'string', mapping: 'domainId'},
         	        {name: 'domainName', type: 'string', mapping: 'domainName'},
         			{name: 'domainAdmin', type: 'string', mapping: 'domainAdmin'},
         	        {name: 'domainStatusCD', type: 'string', mapping: 'domainStatusCD'},
         			{name: 'domainStatusMSG', type: 'string', mapping: 'domainStatusMSG'}
                 ]
        }),

        // load using script tags for cross domain, if the data in on the same domain as
        // this page, an HttpProxy would be better
        proxy: {
            type: 'ajax',
            url: 'domain.do?method=getDomainList',
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
	        	domainParams.limit = rowNum;
	        	domainParams.page = operation.page;
	        	domainParams.sort = operation.sort;
	        	domainParams.start = operation.start;
					
    			domainParams.domainName=Ext.getCmp('domainNameCombox').getValue();
    			domainParams.domainStatus=Ext.getCmp('domainStatusCombox').getValue();
				
				operation.params = domainParams;
			}
        },
	    sortOnLoad: true,
	    sorters: { property: 'domainName', direction : 'asc' }
    });
//    domainStore.setDefaultSort('domainName', 'asc');

    function renderDomainNameSmall(value, p, record){
        return Ext.String.format(
                '<table border="0" style="color:#0055A6;background-color:#FFFFCC;font-size:11px;" width="97%"><tr><td width="10%"><a href="#" onclick="openDomainEditWin({0})"><img src="../../images/PnlIco/pnlico_new_edit.gif" data-qtip="Click to edit {1}"/></a></td><td>{1}</td>'
                +'<td width="80%" align="right"><a href="#" onclick="openDomainOperationsEditWin(\'{1}\')" data-qtip="Click to edit Operations of {1}">Go to Operations</a></td></tr></table>'
                +'<span style="color:#000000;">Status:</span> <span style="color:green;">{2}</span><br/>'
                +'<span style="color:#000000;">Status Message:</span> <span style="color:green;">{3}</span>',
                record.get('domainId'), record.get('domainName'),record.get('domainStatusCD'),record.get('domainStatusMSG'));
    }
    
    function renderDomainNameLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openDomainEditWin({0})">{1}</a></span>',
                record.get('domainId'), record.get('domainName'));
    }
    
    function renderDomainOperationsLarge(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openDomainOperationsEditWin(\'{0}\')">Operations</a></span>',
                record.get('domainName'));
    }

    function renderDomainNameSeperate(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openDomainEditWin({0})">{1}</a></span>',
                record.get('domainId'), record.get('domainName'));
    }
    
    function renderDomainOperationsSeperate(value, p, record){
        return Ext.String.format(
                '<span style="color:#0055A6;"><a href="#" onclick="openDomainOperationsEditWin(\'{0}\')">Operations</a></span>',
                record.get('domainName'));
    }    

		    
	// define the components of portlet
   var createNewDomainBtnSmall = function(){
	   var tmp = new Ext.Button({id:'newDomainSmall',
   		text:'New Domain',
		type:'submit',
		handler:function (){
					openNewDomainWin();
				}
	   });
	   return tmp;
   }
   var newDomainBtnSmall = createNewDomainBtnSmall();
   
   var createNewDomainBtnLarge = function(){
	  var tmp = new Ext.Button({id:'newDomainLarge',
  		text:'New Domain',
		type:'submit',
		handler:function (){
					openNewDomainWin();			    		
				}
	  });
	  return tmp;
   }
   var newDomainBtnLarge = createNewDomainBtnLarge();

   var createDomainGridSmall = function(){
	   var tmp = Ext.create('Ext.grid.Panel',{
			id: 'domainGridSmall',
	        store: domainStoreList,
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
	        	id:'domainGridSmallHeader',
	            text: '&nbsp;',
		        dataIndex: 'domain_Name',
		        width :'100%',
	            renderer: renderDomainNameSmall,
				sortable: true
	        }],
	        // paging bar on the bottom to control the page size
	        bbar: new Ext.PagingToolbar({
		    	id:'domainPagingBarSmall',
		        store: domainStoreList,
		        hidden:false
	    	}),
	        // button on the bottom
	       buttons: [newDomainBtnSmall]     
	    });
	   return tmp;
   }
   var domainGridSmall = createDomainGridSmall();

   var createDomainGridLarge = function(){
	   var tmp = Ext.create('Ext.grid.Panel',{
			id: 'domainGridLarge',
	        store: domainStore,
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
	        	id:'domain_Large_domain_Name',
	            text: 'Domain Name',
		        dataIndex: 'domainName',
	            renderer: renderDomainNameLarge,
				sortable: true
	        },{
	        	id:'domain_Large_domain_Operations',
	            text: 'Domain Operations',
	            renderer: renderDomainOperationsLarge,
				sortable: false
	        },{
	        	id:'domain_Large_domain_Admin',
	            text: 'Domain Admin',
		        dataIndex: 'domainAdmin',
				sortable: false
	        },{
	        	id:'domain_Large_domain_Status',
	            text: 'Status',
		        dataIndex: 'domainStatusCD',
				sortable: true
	        },{
	        	id:'domain_Large_domain_Status_Message',
	            text: 'Status Message',
		        dataIndex: 'domainStatusMSG',
				sortable: true
	        }],

	       // paging bar on the bottom
	        bbar: new Ext.PagingToolbar({
		    	id:'domainPagingBarLarge',
		        store: domainStore,
		        displayInfo: true,
		        displayMsg: 'Total:{2}',
		        emptyMsg: "Total:0"
		    }),
		   // new Domain button on the top
	        buttons: [newDomainBtnLarge]
	    });
	   return tmp;
   }
   var domainGridLarge = createDomainGridLarge();

   var domainGridSeperate = Ext.create('Ext.grid.Panel',{
		id: 'domainGridSeperate',
        store: domainStore,
        autoScroll:true,

        // grid columns
        columns:[{
        	id:'domain_Seperate_domain_Name',
            text: 'Domain Name',
	        dataIndex: 'domainName',
            renderer: renderDomainNameSeperate,
			sortable: true
        },{
        	id:'domain_Seperate_domain_Operations',
            text: 'Domain Operations',
            renderer: renderDomainOperationsSeperate,
			sortable: true
        },{
        	id:'domain_Seperate_domain_Admin',
            text: 'Domain Admin',
	        dataIndex: 'domainAdmin',
			sortable: true
        },{
        	id:'domain_Seperate_domain_Status',
            text: 'Status',
	        dataIndex: 'domainStatusCD',
			sortable: true
        },{
        	id:'domain_Seperate_domain_Status_Message',
            text: 'Status Message',
	        dataIndex: 'domainStatusMSG',
			sortable: true
        }],

        // paging bar on the bottom
        bbar: new Ext.PagingToolbar({
	    	id:'domainPagingBarSeperate',
	        store: domainStore,
	        displayInfo: true,
	        displayMsg: 'Total:{0}-{1} of {2}',
	        emptyMsg: "Total:0"
	    })
    });

    // base parameter
    var domainParams = {limit:rowNum,page:1,sort:{},start:0,domainName:Ext.getCmp('domainNameCombox').getValue(),
	    				domainStatus:Ext.getCmp('domainStatusCombox').getValue()
	    			};

    // trigger the list data store load
    var loadDomainStoreList=function(){
    	domainStoreList.load({
	    				callback:function(r,options,success){
		    				if(success ){
		    				// if success and return record number bigger than switch number then load Grid	    				
								loadDomainStore();
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'domain.do?method=getDomainList',
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
    var loadDomainStore=function(){
    	domainStore.load({
	    				callback:function(r,options,success){
		    				if(success){
		    				// switch list and grid	    				
								if(r.length > pageSize){
//									Ext.getCmp('domainGridSmall').hide();
//									Ext.getCmp('domainGridLarge').show();
									Ext.getCmp('domainPortlet').removeAll();
									newDomainBtnLarge = createNewDomainBtnLarge();
									domainGridLarge = createDomainGridLarge();
									Ext.getCmp('domainPortlet').add(Ext.getCmp('domainGridLarge'));
									domainGridShow = 'large';
								    // trigger the data store load
								    //loadDomainStore();
//								    Ext.getCmp('viewport').doLayout();
								}
		    				}else{
		    					if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading'); 
								Ext.Ajax.request({
									url:'domain.do?method=getDomainList',
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
    
    // function to open a domain Edit window
    
   var openDomainEditWin=function(domainId){
		if(!domainEditWin){
			domainIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'domainIframe'
			});		
			domainEditWin = Ext.create('Ext.window.Window', {
				id:'domainEditWin',
				layout      : 'fit',
				width       :850,
				height      :500,
				minWidth 	: 300,
				miniHeight	: 300,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [domainIframe]
			});
		}
		if(!domainEditWin.isHidden()){
			Ext.getCmp('domainIframe').load('/Node.Administration/Page/Domains/DomainsEdit.do?domainid='+domainId);
		}else{
			domainEditWin.on('show',function(){
				Ext.getCmp('domainIframe').load('/Node.Administration/Page/Domains/DomainsEdit.do?domainid='+domainId);
			});
		}
		domainEditWin.show('domainPortlet');

   };

    // function to open a domain Operations Edit window
    
   var openDomainOperationsEditWin=function(domainName){
		if(!domainOperationsEditWin){
			domainOperationsIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'domainOperationsIframe'
			});		
			domainOperationsEditWin = Ext.create('Ext.window.Window', {
				id:'domainOperationsEditWin',
				layout      : 'fit',
				width       : 850,
				height      : 580,
				minWidth 	: 300,
				miniHeight	: 300,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [domainOperationsIframe]
			});
		}
		if(!domainOperationsEditWin.isHidden()){
			Ext.getCmp('domainOperationsIframe').load('/Node.Administration/Page/Domains/Operations.do?domain='+domainName);
		}else{
			domainOperationsEditWin.on('show',function(){
				Ext.getCmp('domainOperationsIframe').load('/Node.Administration/Page/Domains/Operations.do?domain='+domainName);
			});
		}
		domainOperationsEditWin.show('domainPortlet');

   };

   var openNewDomainWin=function(){
		if(!newDomainWin){
			newDomainIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'newDomainIframe'
			});		
			newDomainWin = Ext.create('Ext.window.Window', {
				id:'newDomainWin',
				layout      : 'fit',
				width       : 700,
				height      : 500,
				minWidth 	: 300,
				miniHeight	: 300,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [newDomainIframe]
			});
		}
		if(!newDomainWin.isHidden()){
			Ext.getCmp('newDomainIframe').load('/Node.Administration/Page/Domains/Domains.do?act=NEW_DOMAIN');
		}else{
			newDomainWin.on('show',function(){
				Ext.getCmp('newDomainIframe').load('/Node.Administration/Page/Domains/Domains.do?act=NEW_DOMAIN');
			});
		}
		newDomainWin.show('domainPortlet');

   };
