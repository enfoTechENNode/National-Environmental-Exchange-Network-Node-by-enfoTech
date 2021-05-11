/**
 * @class Ext.app.Portal
 * @extends Object
 * A sample portal layout application class.
 */

	// create some portlet tools using built in Ext tool ids

	var tools = [{
		type:'close',
		qtip:'Click to close panel',
		handler: function(event, toolEl, panel){
			removePanelIntoSetupLayoutPanel(event, toolEl, panel);
		}	        
	}];

	var transactionLogTools = [{
		type:'search',
		qtip:'Click to open search window',
		handler: function(){
		// create the window on the first click and reuse on subsequent clicks
		if(!transactionLogSearchWin){
			transactionLogSearchWin = Ext.create('Ext.window.Window',{
				id:'TransactionLogSearchWin',
				layout      : 'fit',
				width       : 500,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [transactionSearch]
			});
		}
		transactionLogSearchWin.show('transactionLogPortlet');
	}
	},{
		type:'gear',
		qtip:'Click to switch view model',
		handler: function(e, target, panel){
		if(transactionLogGridShow=='small'){
			Ext.getCmp('transactionLogPortlet').removeAll();
			TransactionLogGridLarge = createTransactionLogGridLarge();
			Ext.getCmp('transactionLogPortlet').add(Ext.getCmp('TransactionLogGridLarge'));
			transactionLogGridShow = 'large';
//			if(!Ext.Msg.isVisible('loading')) viewport.doLayout();
		}else{
			Ext.getCmp('transactionLogPortlet').removeAll();
			TransactionLogGridSmall = createTransactionLogGridSmall();
			Ext.getCmp('transactionLogPortlet').add(Ext.getCmp('TransactionLogGridSmall'));
			transactionLogGridShow = 'small';
//			viewport.doLayout();
		}
	}
	},{
		type:'maximize',
		qtip:'Click to open a bigger seperate window',
		handler: function(e, target, panel){
		// create the window on the first click and reuse on subsequent clicks
		if(!transactionLogMaxWin){
			transactionLogMaxWin = Ext.create('Ext.window.Window',{
				id:'TransactionLogMaxWin',
				layout      : 'fit',
				width       : 800,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				tools: [{
					type:'search',
					qtip:'Click to open search window',
					handler: function(){
					// create the window on the first click and reuse on subsequent clicks
					if(!transactionLogSearchWin){
						transactionLogSearchWin = Ext.create('Ext.window.Window',{
							id:'TransactionLogSearchWin',
							layout      : 'fit',
							width       : 500,
							height      : 500,
							minWidth : 300,
							miniHeight:400,
							closeAction :'hide',
							maximizable:true,
							autoScroll:true,
							modal:false,
							plain       : true,
							constrainHeader:true,
							items : [transactionSearch]
						});
					}
					transactionLogSearchWin.show('transactionLogPortlet');
				}
				}],
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				items : [TransactionLogGridSeperate]
			});
		}
		transactionLogMaxWin.show('transactionLogPortlet');
		loadTransactionLogStore();
	}
	},{
		type:'close',
		qtip:'Click to close panel',
		handler: function(event, toolEl, panel){
			removePanelIntoSetupLayoutPanel(event, toolEl, panel);
		}
	}];

	var scheduledTasksLogTools = [{
		type:'search',
		qtip:'Click to open search window',
		handler: function(){
		// create the window on the first click and reuse on subsequent clicks
		if(!scheduledTasksLogSearchWin){
			scheduledTasksLogSearchWin = Ext.create('Ext.window.Window',{
				id:'scheduledTasksLogSearchWin',
				layout      : 'fit',
				width       : 500,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [scheduledTasksSearch]
			});
		}
		scheduledTasksLogSearchWin.show('scheduledTasksLogPortlet');
	}
	},{
		type:'gear',
		qtip:'Click to switch view model',
		handler: function(e, target, panel){
		if(scheduledTasksLogGridShow=='small'){
			Ext.getCmp('scheduledTasksLogPortlet').removeAll();
			ScheduledTasksLogGridLarge = createScheduledTasksLogGridLarge();
			Ext.getCmp('scheduledTasksLogPortlet').add(Ext.getCmp('ScheduledTasksLogGridLarge'));
			scheduledTasksLogGridShow = 'large';
//			viewport.doLayout();
		}else{
			Ext.getCmp('scheduledTasksLogPortlet').removeAll();
			ScheduledTasksLogGridSmall = createScheduledTasksLogGridSmall();
			Ext.getCmp('scheduledTasksLogPortlet').add(Ext.getCmp('ScheduledTasksLogGridSmall'));
			scheduledTasksLogGridShow = 'small';
//			viewport.doLayout();
		}
	}
	},{
		type:'maximize',
		qtip:'Click to open a bigger seperate window',
		handler: function(e, target, panel){
		// create the window on the first click and reuse on subsequent clicks
		if(!scheduledTasksLogMaxWin){
			scheduledTasksLogMaxWin = Ext.create('Ext.window.Window',{
				id:'scheduledTasksLogMaxWin',
				layout      : 'fit',
				width       : 800,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				tools:[{
					type:'search',
					qtip:'Click to open search window',
					handler: function(){
					// create the window on the first click and reuse on subsequent clicks
					if(!scheduledTasksLogSearchWin){
						scheduledTasksLogSearchWin = Ext.create('Ext.window.Window',{
							id:'scheduledTasksLogSearchWin',
							layout      : 'fit',
							width       : 500,
							height      : 500,
							minWidth : 300,
							miniHeight:400,
							closeAction :'hide',
							maximizable:true,
							autoScroll:true,
							modal:false,
							plain       : true,
							constrainHeader:true,
							items : [scheduledTasksSearch]
						});
					}
					scheduledTasksLogSearchWin.show('scheduledTasksLogPortlet');
				}
				}],
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				items : [ScheduledTasksLogGridSeperate]
			});
		}
		scheduledTasksLogMaxWin.show('scheduledTasksLogPortlet');
		loadScheduledTasksLogStore();
	}
	},{
		type:'close',
		qtip:'Click to close panel',
		handler: function(event, toolEl, panel){
		removePanelIntoSetupLayoutPanel(event, toolEl, panel);
	}
	}];

	var notificationLogTools = [{
		type:'search',
		qtip:'Click to open search window',
		handler: function(){
		// create the window on the first click and reuse on subsequent clicks
		if(!notificationLogSearchWin){
			notificationLogSearchWin = Ext.create('Ext.window.Window',{
				id:'notificationLogSearchWin',
				layout      : 'fit',
				width       : 500,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [notificationSearch]
			});
		}
		notificationLogSearchWin.show('notificationLogPortlet');
	}
	},{
		type:'gear',
		qtip:'Click to switch view model',
		handler: function(e, target, panel){
		if(notificationLogGridShow=='small'){
			Ext.getCmp('notificationLogPortlet').removeAll();
			NotificationLogGridLarge = createNotificationLogGridLarge();
			Ext.getCmp('notificationLogPortlet').add(Ext.getCmp('NotificationLogGridLarge'));
			notificationLogGridShow = 'large';
//			viewport.doLayout();
		}else{
			Ext.getCmp('notificationLogPortlet').removeAll();
			NotificationLogGridSmall = createNotificationLogGridSmall();
			Ext.getCmp('notificationLogPortlet').add(Ext.getCmp('NotificationLogGridSmall'));
			notificationLogGridShow = 'small';
//			viewport.doLayout();
		}
	}
	},{
		type:'maximize',
		qtip:'Click to open a bigger seperate window',
		handler: function(e, target, panel){
		// create the window on the first click and reuse on subsequent clicks
		if(!notificationLogMaxWin){
			notificationLogMaxWin = Ext.create('Ext.window.Window',{
				id:'notificationLogMaxWin',
				layout      : 'fit',
				width       : 800,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				tools:[{
					type:'search',
					qtip:'Click to open search window',
					handler: function(){
					// create the window on the first click and reuse on subsequent clicks
					if(!notificationLogSearchWin){
						notificationLogSearchWin = Ext.create('Ext.window.Window',{
							id:'notificationLogSearchWin',
							layout      : 'fit',
							width       : 500,
							height      : 500,
							minWidth : 300,
							miniHeight:400,
							closeAction :'hide',
							maximizable:true,
							autoScroll:true,
							modal:false,
							plain       : true,
							constrainHeader:true,
							items : [notificationSearch]
						});
					}
					notificationLogSearchWin.show('notificationLogPortlet');
				}
				}],
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				items : [NotificationLogGridSeperate]
			});
		}
		notificationLogMaxWin.show('notificationLogPortlet');
		loadNotificationLogStore();
	}
	},{
		type:'close',
		qtip:'Click to close panel',
		handler: function(event, toolEl, panel){
		removePanelIntoSetupLayoutPanel(event, toolEl, panel);
	}
	}];

	var domainTools = [{
		type:'search',
		qtip:'Click to open search window',
		handler: function(){
		// create the window on the first click and reuse on subsequent clicks
		if(!domainSearchWin){
			domainSearchWin = Ext.create('Ext.window.Window',{
				id:'domainSearchWin',
				layout      : 'fit',
				width       : 500,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				items : [domainSearch]
			});
		}
		domainSearchWin.show('domainPortlet');
	}
	},{
		type:'gear',
		qtip:'Click to switch view model',
		handler: function(e, target, panel){
		if(domainGridShow=='small'){
			Ext.getCmp('domainPortlet').removeAll();
			newDomainBtnLarge = createNewDomainBtnLarge();
			domainGridLarge = createDomainGridLarge();
			Ext.getCmp('domainPortlet').add(Ext.getCmp('domainGridLarge'));
			domainGridShow = 'large';
			// trigger the data store load
			//loadTransactionLogStore();
//			viewport.doLayout();
		}else{
			Ext.getCmp('domainPortlet').remove(Ext.getCmp('domainGridLarge'),true);
			newDomainBtnSmall = createNewDomainBtnSmall();
			domainGridSmall = createDomainGridSmall();
			Ext.getCmp('domainPortlet').add(Ext.getCmp('domainGridSmall'));
			domainGridShow = 'small';
			// trigger the data store load
			//loadTransactionLogStore();
//			viewport.doLayout();
		}
	}
	},{
		type:'maximize',
		qtip:'Click to open a bigger seperate window',
		handler: function(e, target, panel){
		// create the window on the first click and reuse on subsequent clicks
		if(!domainMaxWin){
			domainMaxWin = Ext.create('Ext.window.Window',{
				id:'domainMaxWin',
				layout      : 'fit',
				width       : 800,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				tools:[{
					type:'search',
					qtip:'Click to open search window',
					handler: function(){
					// create the window on the first click and reuse on subsequent clicks
					if(!domainSearchWin){
						domainSearchWin = Ext.create('Ext.window.Window',{
							id:'domainSearchWin',
							layout      : 'fit',
							width       : 500,
							height      : 500,
							minWidth : 300,
							miniHeight:400,
							closeAction :'hide',
							maximizable:true,
							autoScroll:true,
							modal:false,
							plain       : true,
							constrainHeader:true,
							items : [domainSearch]
						});
					}
					domainSearchWin.show('domainPortlet');
				}
				}],
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				items : [domainGridSeperate]
			});
		}
		domainMaxWin.show('domainPortlet');
	}
	},{
		type:'close',
		qtip:'Click to close panel',
		handler: function(event, toolEl, panel){
		removePanelIntoSetupLayoutPanel(event, toolEl, panel);
	}
	}];

	var documentTools = [{
		type:'search',
		qtip:'Click to open search window',
		handler: function(){
		// create the window on the first click and reuse on subsequent clicks
		if(!documentSearchWin){
			documentSearchWin = Ext.create('Ext.window.Window',{
				id:'documentSearchWin',
				layout      : 'fit',
				width       : 500,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain       : true,
				constrainHeader:true,
				items : [documentSearch]
			});
		}
		documentSearchWin.show('documentPortlet');
	}
	},{
		type:'gear',
		qtip:'Click to switch view model',
		handler: function(e, target, panel){
		if(documentGridShow=='small'){
			Ext.getCmp('documentPortlet').removeAll();
		    documentCheckBoxSMLarge = Ext.create('Ext.selection.CheckboxModel'); 
			uploadDocumentBtnLarge = createUploadDocumentBtnLarge();
			deleteDocumentsBtnLarge = createDeleteDocumentsBtnLarge();
			documentGridLarge = createDocumentGridLarge();
			Ext.getCmp('documentPortlet').add(Ext.getCmp('documentGridLarge'));
			documentGridShow = 'large';
			// trigger the data store load
			//loadTransactionLogStore();
//			viewport.doLayout();
		}else{
			Ext.getCmp('documentPortlet').removeAll();
			uploadDocumentBtnSmall = createUploadDocumentBtnSmall();
			documentGridSmall = createDocumentGridSmall();
			Ext.getCmp('documentPortlet').add(Ext.getCmp('documentGridSmall'));
			documentGridShow = 'small';
			// trigger the data store load
			//loadTransactionLogStore();
//			viewport.doLayout();
		}
	}
	},{
		type:'maximize',
		qtip:'Click to open a bigger seperate window',
		handler: function(e, target, panel){
		// create the window on the first click and reuse on subsequent clicks
		if(!documentMaxWin){
			documentMaxWin = Ext.create('Ext.window.Window',{
				id:'documentMaxWin',
				layout      : 'fit',
				width       : 800,
				height      : 500,
				minWidth : 300,
				miniHeight:400,
				tools:[{
					type:'search',
					qtip:'Click to open search window',
					handler: function(){
					// create the window on the first click and reuse on subsequent clicks
					if(!documentSearchWin){
						documentSearchWin = Ext.create('Ext.window.Window',{
							id:'documentSearchWin',
							layout      : 'fit',
							width       : 500,
							height      : 500,
							minWidth : 300,
							miniHeight:400,
							closeAction :'hide',
							maximizable:true,
							autoScroll:true,
							modal:false,
							plain       : true,
							constrainHeader:true,
							items : [documentSearch]
						});
					}
					documentSearchWin.show('documentPortlet');
				}
				}],
				closeAction :'hide',
				maximizable:true,
				autoScroll:true,
				modal:false,
				plain: true,
				constrainHeader:true,
				items : [documentGridSeperate]
			});
		}
		documentMaxWin.show('documentPortlet');
		loadDocumentStore();
	}
	},{
		type:'close',
		qtip:'Click to close panel',
		handler: function(event, toolEl, panel){
		removePanelIntoSetupLayoutPanel(event, toolEl, panel);
	}
	}];

	var statusTools = [{
	    	type:'gear',
	    	qtip:'Click to switch view model',
	    	handler: function(e, target, panel){
	    	if(statusGridShow=='small'){
	    		Ext.getCmp('statusPortlet').removeAll();
	    		StatusGridLarge = createStatusGridLarge();
	    		Ext.getCmp('statusPortlet').add(Ext.getCmp('StatusGridLarge'));
	    		statusGridShow = 'large';
//	    		viewport.doLayout();
	    	}else{
	    		Ext.getCmp('statusPortlet').removeAll();
	    		StatusGridSmall = createStatusGridSmall();
	    		Ext.getCmp('statusPortlet').add(Ext.getCmp('StatusGridSmall'));
	    		statusGridShow = 'small';
//	    		viewport.doLayout();
	    	}
	    }
	    },{
	    	type:'maximize',
	    	qtip:'Click to open a bigger seperate window',
	    	handler: function(e, target, panel){
	    	// create the window on the first click and reuse on subsequent clicks
	    	if(!statusMaxWin){
	    		statusMaxWin = Ext.create('Ext.window.Window',{
	    			id:'statusMaxWin',
	    			layout      : 'fit',
	    			width       : 800,
	    			height      : 500,
	    			minWidth : 300,
	    			miniHeight:400,
	    			closeAction :'hide',
	    			maximizable:true,
	    			autoScroll:true,
	    			modal:false,
	    			plain: true,
					constrainHeader:true,
	    			items : [StatusGridSeperate]
	    		});
	    	}
	    	statusMaxWin.show('statusPortlet');
	    }
	    },{
	    	type:'close',
	    	qtip:'Click to close panel',
			handler: function(event, toolEl, panel){
			removePanelIntoSetupLayoutPanel(event, toolEl, panel);
		}
	    }];

//	Create setup Form panel in leftPanel setLayout section
	var setupLayoutPanel = new Ext.FormPanel({
		id:'setupLayoutPanel',
		labelWidth:0,
		frame:false,
		border:false,
		draggable:false,
		width: LeftPanelWidth,
		header: false,
		hideBorders:true,
		height:30,
		defaults: {width: LeftPanelWidth},
		buttons: [{
			id:'setupLayoutSaveBtn',
			text:'Save',
			type:'submit',
			handler:function (){
				rowNum = Ext.getCmp('rowNo').getValue();
				topNum = Ext.getCmp('topNo').getValue();	    		
				pageSize = Ext.getCmp('switchNo').getValue();	    		
				if(Ext.data.StoreManager.lookup('transactionLogStoreList')!=null) Ext.data.StoreManager.lookup('transactionLogStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('transactionLogStore')!=null) Ext.data.StoreManager.lookup('transactionLogStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('scheduledTasksLogStoreList')!=null) Ext.data.StoreManager.lookup('scheduledTasksLogStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('scheduledTasksLogStore')!=null) Ext.data.StoreManager.lookup('scheduledTasksLogStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('domainStoreList')!=null) Ext.data.StoreManager.lookup('domainStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('domainStore')!=null) Ext.data.StoreManager.lookup('domainStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('documentStoreList')!=null) Ext.data.StoreManager.lookup('documentStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('documentStore')!=null) Ext.data.StoreManager.lookup('documentStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('notificationLogStoreList')!=null) Ext.data.StoreManager.lookup('notificationLogStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('notificationLogStore')!=null) Ext.data.StoreManager.lookup('notificationLogStore').pageSize = parseInt(rowNum);
				saveConfig();
				//getInitialParams();
				//initialForm.getEl().dom.submit();
		}
		},{
			id:'setupLayoutResetBtn',
			text:'Reset',
			type:'submit',
			handler:function (){
			//send back  
			Ext.Ajax.request({
				url:'Configurations.do?method=resetLayout',
				success:function(response,options){
					getPlugInLayout();
				},		
				failure:function(){
					Ext.Msg.alert('Message', "Fail to get layout information.");												
				}
			});		    			
		}
		}]
	});

//	Create setup Form panel in leftPanel setup section
	var setupPanel = Ext.create('Ext.form.Panel',{
		id:'setupPanel',
		labelWidth: 90, // label settings here cascade unless overridden
		frame:true,
		bodyStyle:'padding:1px 1px 0',
		width: LeftPanelWidth,
		defaults: {width: LeftPanelWidth},
		defaultType: 'textfield',	
		items: [ {
			id:'rowNo',
			fieldLabel: 'Default Rows of Grid',
			name: 'rowNo',
			value:rowNum
		},{
			id:'topNo',
			fieldLabel: 'Default Top No of List',
			name: 'topNo',
			value:topNum
		},{
			id:'switchNo',
			fieldLabel: 'Page Size',
			name: 'switchNo',
			value:pageSize
		}],
		buttons: [{
			id:'setupBtn',
			text:'Save',
			type:'submit',
			handler:function (){
				rowNum = Ext.getCmp('rowNo').getValue();
				topNum = Ext.getCmp('topNo').getValue();	    		
				pageSize = Ext.getCmp('switchNo').getValue();
				if(Ext.data.StoreManager.lookup('transactionLogStoreList')!=null) Ext.data.StoreManager.lookup('transactionLogStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('transactionLogStore')!=null) Ext.data.StoreManager.lookup('transactionLogStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('scheduledTasksLogStoreList')!=null) Ext.data.StoreManager.lookup('scheduledTasksLogStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('scheduledTasksLogStore')!=null) Ext.data.StoreManager.lookup('scheduledTasksLogStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('domainStoreList')!=null) Ext.data.StoreManager.lookup('domainStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('domainStore')!=null) Ext.data.StoreManager.lookup('domainStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('documentStoreList')!=null) Ext.data.StoreManager.lookup('documentStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('documentStore')!=null) Ext.data.StoreManager.lookup('documentStore').pageSize = parseInt(rowNum);
				if(Ext.data.StoreManager.lookup('notificationLogStoreList')!=null) Ext.data.StoreManager.lookup('notificationLogStoreList').pageSize = parseInt(topNum);
				if(Ext.data.StoreManager.lookup('notificationLogStore')!=null) Ext.data.StoreManager.lookup('notificationLogStore').pageSize = parseInt(rowNum);
				saveConfig();
				loadTransactionLogStoreList();
				loadScheduledTasksLogStoreList();
				loadNotificationLogStoreList();
				loadDomainStoreList();
				loadDocumentStoreList();
				loadStatusStoreList();
			}
		}]
	});

	var domainPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Node Domain',
			id:'domainPortlet',
			layout:'fit',
			tools: domainTools,
			closable:false,
			autoScroll:true,
			style:'padding:0px 0px 0px 0px',
			listeners: {
				'beforeexpand':	function(p,anim){
					if(p.ownerCt.getId()=='SetLayout')
						return false;
					else return true;
				}
			}, 	                    
			items: [Ext.getCmp('domainGridSmall')]
	});

	var transactionLogPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Node Transaction Log',
			id:'transactionLogPortlet',
			layout:'fit',
			tools: transactionLogTools,
			closable:false,
			autoScroll:true,
			style:'padding:0px 0px 0px 0px',
			listeners: {
				'beforeexpand':	function(p,anim){
					if(p.ownerCt.getId()=='SetLayout')
						return false;
					else return true;
				}
			}, 	                    
			items: [Ext.getCmp('TransactionLogGridSmall')]
	});

	var favoriteLinksPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Favorite Links',
			id:'favoriteLinksPortlet',
			layout:'fit',
			tools: tools,
			closable:false,
			autoScroll:true, 	                    
			style:'padding:0px 0px 0px 0px',
            listeners: {
		           'beforeexpand':	function(p,anim){
								if(p.ownerCt.getId()=='SetLayout')
									return false;
								else return true;
							}
            }, 	                    
			html:
				'&nbsp;<span><a href="#" onclick="openFavoriteLinksWin(\'/Node.Administration/Page/Users/Users.do?act=INITIAL\')"><img src="../../images/PnlIco/pnlico_link.gif"/>&nbsp;Node User</a></span><br/><br/>'
				+ '&nbsp;<span><a href="#" onclick="openGetServicesWin()"><img src="../../images/PnlIco/pnlico_link.gif"/>&nbsp;Node Registration</a></span><br/><br/>'
				+ '&nbsp;<span><a href="/Node.Client/Page/Entry/FrameSet.jsp" target="_blank"><img src="../../images/PnlIco/pnlico_link.gif"/>&nbsp;Node Client</a></span><br/><br/>'
				+ '&nbsp;<span><a href="#" onclick="openOperationMgrSubmitWin()"><img src="../../images/PnlIco/pnlico_link.gif"/>&nbsp;Node Operation Manager</a></span><br/><br/>'
	});

	var configurationPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Node Configuration',
			id:'configurationPortlet',
			layout:'fit',
			tools: tools,
			closable:false,
			autoScroll:true, 	                    
			style:'padding:0px 0px 0px 0px',
            listeners: {
		           'beforeexpand':	function(p,anim){
								if(p.ownerCt.getId()=='SetLayout')
									return false;
								else return true;
							}
            }, 	                    
			html: '&nbsp;<span><a href="#" onclick="openConfigurationWin(\'/Node.Administration/Page/Configurations/Configurations.do?act=SELECT_CONFIG\')"><img src="../../images/PnlIco/pnlico_No1.gif"/>&nbsp;Node Configuration</a></span><br/><br/>'
				+'&nbsp;<span><a href="#" onclick="openConfigurationWin(\'/Node.Administration/Page/Configurations/Configurations.do?act=SELECT_EMAIL\')"><img src="../../images/PnlIco/pnlico_No2.gif"/>&nbsp;Email Configuration</a></span><br/><br/>'
				+'&nbsp;<span><a href="#" onclick="openConfigurationWin(\'/Node.Administration/Page/Configurations/Configurations.do?act=SELECT_CLIENT\')"><img src="../../images/PnlIco/pnlico_No3.gif"/>&nbsp;Client Configuration</a></span><br/><br/>'
				+'&nbsp;<span><a href="#" onclick="openOperationMgrWin()"><img src="../../images/PnlIco/pnlico_No4.gif"/>&nbsp;Operation Manager</a></span><br/><br/>'
				+'&nbsp;<span><a href="#" onclick="openDataWizardConfigWin(\'/Node.Administration/Page/Configurations/DataWizardConfig.do?act=upload\')"><img src="../../images/PnlIco/pnlico_No5.gif"/>&nbsp;Data Wizard Configuration</a></span><br/><br/>'
				+'&nbsp;<span><a href="#" onclick="openRestServiceWin(\'/Node.Administration/Page/Entry/Configurations.do?method=getRestServiceIntroduction\')"><img src="../../images/PnlIco/pnlico_No6.gif"/>&nbsp;REST Page Configuration</a></span><br/><br/>'
	});

	var scheduledTasksLogPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Scheduled Tasks',
			id:'scheduledTasksLogPortlet',
			layout:'fit',
			tools: scheduledTasksLogTools,
			closable:false,
			autoScroll:true,
			style:'padding:0px 0px 0px 0px',
			listeners: {
				'beforeexpand':	function(p,anim){
					if(p.ownerCt.getId()=='SetLayout')
						return false;
					else return true;
				}
			}, 	                    
			items: [Ext.getCmp('ScheduledTasksLogGridSmall')]
	});

	var notificationLogPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Node Notifications',
			id:'notificationLogPortlet',
			layout:'fit',
			tools: notificationLogTools,
			closable:false,
			autoScroll:true,
			style:'padding:0px 0px 0px 0px',
			listeners: {
				'beforeexpand':	function(p,anim){
					if(p.ownerCt.getId()=='SetLayout')
						return false;
					else return true;
				}
			}, 	                    
			items: [Ext.getCmp('NotificationLogGridSmall')]
	});

	var statusPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Node Status',
			id:'statusPortlet',
			layout:'fit',
			closable:false,
			tools: statusTools,
			autoScroll:true,
			style:'padding:0px 0px 0px 0px',
			listeners: {
				'beforeexpand':	function(p,anim){
				if(p.ownerCt.getId()=='SetLayout')
					return false;
				else return true;
			}
		}, 	                    
		items: [Ext.getCmp('StatusGridSmall')]
	});

	var documentPortlet = Ext.create('Ext.app.Portlet',{
			title: 'Node Document',
			id:'documentPortlet',
			layout:'fit',
			closable:false,
			tools: documentTools,
			autoScroll:true,
			style:'padding:0px 0px 0px 0px',
			listeners: {
				'beforeexpand':	function(p,anim){
					if(p.ownerCt.getId()=='SetLayout')
						return false;
					else return true;
				}
			}, 	                    
			items: [Ext.getCmp('documentGridSmall')]
	});
	
    // define buttons   
    var saveConfig = function (){
		    	var col,items='';
		    	var p = Ext.getCmp('myPortal1');
		    	var col0 = p.items.getAt(0);
		    	var col1 = p.items.getAt(1);
		    	var col2 = p.items.getAt(2);
		    	var setLayout = Ext.getCmp('SetLayout');
		    	var tmp='';
		    	
		        for(var c = 0; c < 3; c++){
				    col = p.items.getAt(c);
				    for(var i = 0;i<col.items.length;i++){
			    		//alert('The '+col.getId()+' col has: ' + col.items.getAt(i).getId());
			    		tmp = tmp + col.items.getAt(i).getId() + ',';  	    		    
				    }
				    
				   	items = items + '\"para'+c+'\":\"['+ tmp.substring(0,tmp.length-1) + ']\",';
				   	tmp='';  	    		    				    
				}
			    for(var i = 0;i<setLayout.items.length;i++){
		    		//alert('The '+col.getId()+' col has: ' + col.items.getAt(i).getId());
		    		if(setLayout.items.getAt(i).getId()!='setupLayoutPanel')
		    			tmp = tmp + setLayout.items.getAt(i).getId() + ',';  	    		    
			    }
			    
			   	tmp = '\"para0'+'\":\"'+ tmp.substring(0,tmp.length-1) + '\",';
				
				items = '{\"pageLayout\":[{'+items.substring(0,items.length-1)+'},{\"para0\":'+rowNum+',\"para1\":'+topNum+',\"para2\":'+pageSize+'},{'+ tmp +'\"para1\":null,\"para2\":null}]}';
				//alert(items);
				
				//send back  
				Ext.Ajax.request({
					url:'Configurations.do?method=setLayout',
					params:{
						rowNum:rowNum,
						topNum:topNum,
						pageSize:pageSize,
						columns:items
					},
					success:function(response,options){						
						var rsp = response.responseText;							
	            		Ext.Msg.alert('Message', rsp);	        		
					},		
					failure:function(){
						Ext.Msg.alert('Message', "Fail to save Configurations.");												
					}
				})
	        	//viewport.doLayout();
			}
    	     	
    // define tab component
    
    var TabHead1 = Ext.create('Ext.panel.Panel',{
                    id:'TabHead1',
        			border:false,
       				hideBorders:true,
        			margins:'0 0 0 0',
       				layout:'fit',
       				//items:[eval(Ext.getDom('TabHead'))]
       				
                    html:
  		'	<div id="TabHead1">'
		+'		<table id="TabHeadTbl1" border="0" cellspacing="0" width="100%" align="left">'
		+'	  	<tr>'
		+'	    	<td nowrap align="left" width="10%">'
		+'	    		<img src="../../images/Header/Node_Home.gif"/>'
		+'	    	</td>'
		+'	    	<td nowrap align="left" valign="bottom"  width="10%">'
		+'	    		<div id="TabPic2" style="cursor:pointer;">'
		+'	      			<img id="End_L1" style="padding:0; margin:0;" src="../../images/Header/HTOn_End_L.gif"  border="0"><img id="Node1On" style="padding:0; margin:0; " src="../../images/Header/HI_Node11_On.gif" border="0"><img id="Mid1" style="padding:0; margin:0;" src="../../images/Header/HTOn_Mid_R.gif" border="0"><img id="Node1Off" onClick="switchNode2()" style="padding:0; margin:0;" src="../../images/Header/HI_Node20_Off.gif" border="0"><img id="End_R1" onClick="switchNode2()" style="padding:0; margin:0;" src="../../images/Header/HTOff_End_R.gif"  border="0">'
		+'				</div>'
		+'      	</td>'
		+'	  	<td nowrap align="right" valign="bottom"  width="80%">'
		+'         	<div id="BtnEditLayout1" onClick="modifyLayout()" ><input type="image" id="btn" src="../../images/Header/btnEditLayout.gif" style="border-width:0px;padding:0; margin:0 20 5 0;" align="right"/></div>'
		+'	  	</td>'
		+'    	</tr>'
		+'  	</table>'
		+'	</div>'

        });

    var TabHead2 = Ext.create('Ext.panel.Panel',{
                    id:'TabHead2',
        			border:false,
       				margins:'0 0 0 0',
       				hideBorders:true,
       				layout:'fit',
       				//items:[eval(Ext.getDom('TabHead'))]
       				
                    html:
  		'	<div id="TabHead2">'
		+'		<table id="TabHeadTbl2" border="0" cellspacing="0" width="100%" align="left">'
		+'	  	<tr>'
		+'	    	<td nowrap align="left" width="10%">'
		+'	    		<img src="../../images/Header/Node_Home.gif"/>'
		+'	    	</td>'
		+'	    	<td nowrap align="left" valign="bottom"  width="10%">'
		+'	    		<div id="TabPic" style="cursor:pointer;">'
		+'	      			<img id="End_L2" onClick="switchNode1()" style="padding:0; margin:0;" src="../../images/Header/HTOff_End_L.gif"  border="0"><img id="Node2Off" onClick="switchNode1()" style="padding:0; margin:0; " src="../../images/Header/HI_Node11_Off.gif" border="0"><img id="Mid2" style="padding:0; margin:0;" src="../../images/Header/HTOn_Mid_L.gif" border="0"><img id="Node2On" style="padding:0; margin:0;" src="../../images/Header/HI_Node20_On.gif" border="0"><img id="End_R2" style="padding:0; margin:0;" src="../../images/Header/HTOn_End_R.gif"  border="0">'
		+'				</div>'
		+'      	</td>'
		+'	  	<td nowrap align="right" valign="bottom"  width="80%">'
		+'         	<div id="BtnEditLayout2" onClick="modifyLayout()" ><input type="image" id="btn" src="../../images/Header/btnEditLayout.gif" style="border-width:0px;padding:0; margin:0 20 5 0;" align="right"/></div>'
		+'	  	</td>'
		+'    	</tr>'
		+'  	</table>'
		+'	</div>'

    });

 	var TabHead = Ext.create('Ext.panel.Panel',{
        id:'TabHead',
        style:'	background: #ffffff;',
        split:false,
        region:'north',
        border : false,
        collapsible: false,
        margins:'0 0 0 0',
        layout:'fit',
        height:46,
        items: [TabHead1,TabHead2]
    });

 	// define body panel
 	var BodyPanel = Ext.create('Ext.panel.Panel', {
        region:'center',
        id:'BodyPanel',
        collapsible: false,
        margins:'-7 0 0 0',
        layout:'fit',
        border:false,
        items: [{
		            id: 'myPortal1',
		            xtype: 'portalpanel',
		            items: [{
		                id: 'column0',
		                items: [col0]
		            },{
		                id: 'column1',
		                items: [col1]
		            },{
		                id: 'column2',
		                items: [col2]
		            }]
		        }]
 	});

    // define left panel
    var LeftPanel = Ext.create('Ext.panel.Panel', {
            region:'west',
            id:'LeftPanel',
            title:'Config Center',
            border:true,
            width: LeftPanelWidth,
            minSize: 75,
            maxSize: 200,
            margins:'0 0 0 0',
            layout:'accordion',
            layoutConfig:{
                animate:true
            },
            closable:true,
            closeAction: 'hide',
			listeners: {
				'close':function( panel, eOpts ){
	    			panel.hide();
	    			Ext.getCmp('myPortal1').dd.lock();
	    			Ext.getDom('BtnEditLayout1').style.display='block';
	    			Ext.getDom('BtnEditLayout2').style.display='block';
				}
			}, 	                    
            items: [{	            	
                title:'Layout Settings',
                id:'SetLayout',
                autoScroll:true,
                border:false,
                layout:'anchor',
            	margins:'0 0 0 0',
                items:[setupLayoutPanel]
            },{
                title:'Grid Settings',
                border:false,
                autoScroll:true,
                items:[setupPanel]
            }]
        });

    Ext.define('Ext.app.Portal', {
	    extend: 'Ext.container.Viewport',
	    requires: ['Ext.app.PortalPanel', 'Ext.app.PortalColumn'],
        id: 'app-viewport',
        layout: 'border',
        items: [{
            id: 'app-header',
            xtype: 'panel',
            region: 'north',
            defaultMargins : 0,
            padding:0,
            border : false,
            html:NodeHeader
        },LeftPanel,{
            id: 'app-body',
            xtype: 'container',
            region: 'center',
            layout: 'border',
            border : false,
            items: [TabHead,BodyPanel]
        },{
            id: 'app-footer',
            xtype: 'panel',
            region: 'south',
            height: 35,
            html:NodeFooter
        }]   
});
    
//	var viewport = Ext.create('Ext.panel.Panel', {
//    	id:'viewport',
//        layout:'border',
//        style:'	background: #ffffff;',
//        items:[BodyPanel]
//    });

	// tab switch event handle
	var switchNode1 = function(){
		var tabHead = Ext.getCmp('TabHead');
		version = ver_1;
		Ext.Ajax.request({
			url:'/Node.Administration/Page/Entry/Login.do?act=SET_VERSION&version='+version,
			success:function(response,options){						
				Ext.getCmp('TabHead2').hide();
				Ext.getCmp('TabHead1').show();
				tabHead.doLayout();
				transactionLogParams.ver = version;
				// load all grid data
				if(!Ext.Msg.isVisible('loading')) Ext.Msg.wait('loading');
				loadTransactionLogStoreList();
				loadScheduledTasksLogStoreList();
				loadNotificationLogStoreList();
				loadDomainStoreList();
				loadDocumentStoreList();
				loadStatusStoreList();
			},		
			failure:function(){
				Ext.Msg.alert('Message', "Fail to access database.");												
			}
		});		    					    					   			
		//Ext.getCmp('TabBody').getLayout().setActiveItem(0);
		//Ext.getCmp('myPortal1').doLayout();
	}
	var switchNode2 = function(){
		var tabHead = Ext.getCmp('TabHead');
		version = ver_2;
		Ext.Ajax.request({
			url:'/Node.Administration/Page/Entry/Login.do?act=SET_VERSION&version='+version,
			success:function(response,options){						
			Ext.getCmp('TabHead1').hide();
			Ext.getCmp('TabHead2').show();
			tabHead.doLayout();
			// load all grid data
			if(!Ext.Msg.isVisible('loading')) Ext.Msg.wait('loading');
			loadTransactionLogStoreList();
			loadScheduledTasksLogStoreList();
			loadNotificationLogStoreList();
			loadDomainStoreList();
			loadDocumentStoreList();
			loadStatusStoreList();
		},		
		failure:function(){
			Ext.Msg.alert('Message', "Fail to access database.");												
		}
		});		    					    					   			
		//Ext.getCmp('TabBody').getLayout().setActiveItem(1);
	}
	// Modify page handle
	var modifyLayout = function(){
		Ext.getCmp('LeftPanel').show();
		Ext.getCmp('myPortal1').dd.unlock();
		//Ext.getCmp('myPortal2').dd.unlock();
		Ext.getDom('BtnEditLayout1').style.display='none';
		Ext.getDom('BtnEditLayout2').style.display='none';
//		viewport.doLayout();			
	}

	// Drag to portal 
	var dragToPortal = function(source, e, id) {
		var cmpID = Ext.get(source.getEl()).dom.id;
		// to force panel collapse
		if(Ext.getCmp(cmpID).ownerCt.getId()=='SetLayout'){
			Ext.getCmp(cmpID).collapse();  		            
		}

	};
	// reset portlet width to Auto
	var resetAutoWidth = function(){
		var portal1 = Ext.getCmp('myPortal1');
		// Set up all content panels' width as auto
		for(var i=0;i<portal1.items.length;i++){
			var colId=portal1.items.getAt(i);
			for(var j=0;j<colId.items.length;j++){	   		
				var itemId = colId.items.getAt(j).getId(); 
				Ext.getCmp(itemId).setWidth('auto');
			}
		}

	};
	// show all items

	// initial portal
	var initfn = function(){
		
    	var portal1 = Ext.getCmp('myPortal1');
    	// Check if already add column
    	if(Ext.getCmp('column0') == null){
	      	// set up columns of portal   	    
	    	portal1.add({
	    				id:'column0',
		                columnWidth:.333,
		                style:'padding:5px 5px 5px 5px',
		            	items:eval(col0)
		   },{
	    				id:'column1',
		                columnWidth:.333,
		                style:'padding:5px 5px 5px 5px',
		            	items:eval(col1)
		   },{
	    				id:'column2',
		                columnWidth:.333,
		                style:'padding:5px 18px 5px 5px',
		            	items:eval(col2)
		   });   	
    	}else{
    	// show all hiden plug in portlets
//        	if(plugInLayout!=null && plugInLayout!=''){
//            	var mySplitPlugIn = plugInLayout.split(",");
//            	for(var i=0;i<mySplitPlugIn.length;i++){
//         			Ext.getCmp('column0').add(eval(mySplitPlugIn[i]));
//            		Ext.getCmp(mySplitPlugIn[i]).show();
//            	}   	
//        	}
    	}
	   
    	portal1.dd.lock();
    	portal1.doLayout();
    	
	 	// expand all content panels when reset the layout
		portal1.items.each(function( portalItem ) {
			portalItem.items.each(function( colItem ) {
				Ext.getCmp(colItem.getId()).expand(true);
			});
		});
		
		// set up setup panel
		Ext.getCmp('rowNo').setValue(rowNum);
		Ext.getCmp('topNo').setValue(topNum);	    		
		Ext.getCmp('switchNo').setValue(pageSize);	    		
		// load all grid data
		if(!Ext.Msg.isVisible('loading')) Ext.Msg.wait('loading');
		if(Ext.data.StoreManager.lookup('transactionLogStoreList')!=null) Ext.data.StoreManager.lookup('transactionLogStoreList').pageSize = parseInt(topNum);
		if(Ext.data.StoreManager.lookup('transactionLogStore')!=null) Ext.data.StoreManager.lookup('transactionLogStore').pageSize = parseInt(rowNum);
		if(Ext.data.StoreManager.lookup('scheduledTasksLogStoreList')!=null) Ext.data.StoreManager.lookup('scheduledTasksLogStoreList').pageSize = parseInt(topNum);
		if(Ext.data.StoreManager.lookup('scheduledTasksLogStore')!=null) Ext.data.StoreManager.lookup('scheduledTasksLogStore').pageSize = parseInt(rowNum);
		if(Ext.data.StoreManager.lookup('domainStoreList')!=null) Ext.data.StoreManager.lookup('domainStoreList').pageSize = parseInt(topNum);
		if(Ext.data.StoreManager.lookup('domainStore')!=null) Ext.data.StoreManager.lookup('domainStore').pageSize = parseInt(rowNum);
		if(Ext.data.StoreManager.lookup('documentStoreList')!=null) Ext.data.StoreManager.lookup('documentStoreList').pageSize = parseInt(topNum);
		if(Ext.data.StoreManager.lookup('documentStore')!=null) Ext.data.StoreManager.lookup('documentStore').pageSize = parseInt(rowNum);
		if(Ext.data.StoreManager.lookup('notificationLogStoreList')!=null) Ext.data.StoreManager.lookup('notificationLogStoreList').pageSize = parseInt(topNum);
		if(Ext.data.StoreManager.lookup('notificationLogStore')!=null) Ext.data.StoreManager.lookup('notificationLogStore').pageSize = parseInt(rowNum);
   		// inital setLayout panel of leftpanel
     	if(setLayoutPanel != null && setLayoutPanel != ''){
	     	var stringArr = setLayoutPanel.split(',');
	     	for(var i=0;i<stringArr.length;i++){
	     		var tmpPanel = eval(stringArr[i]);
	     		Ext.getCmp('SetLayout').add(tmpPanel);     	
	     		tmpPanel.setWidth(LeftPanelWidth);
	     		tmpPanel.collapse();
	     		tmpPanel.collapsible=false; 
	     	}     	
     	}
		// load data
		loadTransactionLogStoreList();
		loadScheduledTasksLogStoreList();
		loadNotificationLogStoreList();
		loadDomainStoreList();
		loadDocumentStoreList();
		loadStatusStoreList();

	};

	// check layout information from database
	var col0='';
	var col1='';
	var col2='';
	var setLayoutPanel='';

	var cleanPlugIn = function (){

		var portal1 = Ext.getCmp('myPortal1');
		// remove the old components
		if(Ext.getCmp('column0') != null){
			Ext.getCmp('column0').removeAll(false);
		}
		if(Ext.getCmp('column1') != null){
			Ext.getCmp('column1').removeAll(false);
		}
		if(Ext.getCmp('column2') != null){
			Ext.getCmp('column2').removeAll(false);
		}
		if(Ext.getCmp('SetLayout') != null){
			Ext.getCmp('SetLayout').removeAll(false);
			Ext.getCmp('SetLayout').add(setupLayoutPanel); 
			setLayoutPanel='';
		}
		
//		if(plugInLayout != null && plugInLayout != ''){
//			var mySplitPlugIn = plugInLayout.split(",");
//			for(var i=0;i<mySplitPlugIn.length;i++){
//				if(Ext.getCmp('column0') != null){
//					Ext.getCmp('column0').remove(mySplitPlugIn[i],false);
//					Ext.getCmp(mySplitPlugIn[i]).hide();
//				}
//				if(Ext.getCmp('column1') != null){
//					Ext.getCmp('column1').remove(mySplitPlugIn[i],false);
//					Ext.getCmp(mySplitPlugIn[i]).hide();
//				}
//				if(Ext.getCmp('column2') != null){
//					Ext.getCmp('column2').remove(mySplitPlugIn[i],false);
//					Ext.getCmp(mySplitPlugIn[i]).hide();
//				}
//        		if(Ext.getCmp('SetLayout') != null){
//	 	   			Ext.getCmp('SetLayout').remove(mySplitPlugIn[i],false);
//	 	   			Ext.getCmp(mySplitPlugIn[i]).hide();
//        		}
//			}
//			viewport.doLayout();       	
//		}	
	};

	var getInitialParams = function (){
		// trigger wizard
		Ext.Ajax.request({
			url:'/Node.Administration/Page/Entry/Login.do?act=GET_DOTNETURL',
			method: 'POST',
			success:function(response,options){
			Ext.Ajax.request({
				url:response.responseText,
				method: 'POST',
				success:function(response,options){
			},		
			failure:function(){
			}
			});								
		},		
		failure:function(){
			//Ext.Msg.alert('Message', "Wizard function is not available. Please check web.xml to find configuration of node/dotNetHost and node/dotNetHostPort.");
		}
		});					
		// define default layout			
		var col0defaultLayout = null;
		if(plugInLayout != null && plugInLayout != ''){
			col0defaultLayout = '[' + plugInLayout + ',statusPortlet,documentPortlet,domainPortlet]';
		}else{
			col0defaultLayout = '[statusPortlet,documentPortlet,domainPortlet]';    
		}
		var col1defaultLayout = '[transactionLogPortlet,configurationPortlet,favoriteLinksPortlet]';
		var col2defaultLayout = '[scheduledTasksLogPortlet,notificationLogPortlet]';
		
		
//		var col0defaultLayout = '[documentPortlet]'; 
//		var col1defaultLayout = '[transactionLogPortlet]';
//		var col2defaultLayout = '[]';

		if(Ext.getCmp('initDataStore')==null){
			initDataStore = Ext.create('Ext.data.Store',{
				id: 'initDataStore',
		        model: Ext.define('initDataStoreMd', {
		            extend: 'Ext.data.Model',
		            fields:['para0','para1','para2']
		        }),			
		        proxy: {
		            type: 'ajax',
		            url: 'Configurations.do?method=getLayout',
		            reader: {
		                type: 'json',
		                root: 'pageLayout'
		            }
		        }
		    });			
		}
		
		initDataStore.load({
			callback: function(r,options,success){
				if(success){
					if(r.length>2){
						// get layout information from backend
						col0 = r[0].data['para0']!='[]'?r[0].data['para0']:'';
						col1 = r[0].data['para1']!='[]'?r[0].data['para1']:'';
						col2 = r[0].data['para2']!='[]'?r[0].data['para2']:'';
	
						rowNum = r[1].data['para0'];
						topNum = r[1].data['para1'];
						pageSize = r[1].data['para2'];
	
						if(r[2].data['para0']!=null && r[2].data['para0']!='') setLayoutPanel = r[2].data['para0'];
					}else{	
						// setup default layout
						//alert("This is default : "+col0defaultLayout);
						col0 = col0defaultLayout;
						col1 = col1defaultLayout;
						col2 = col2defaultLayout;				     
					}
					Ext.getCmp('column0').removeAll(false);
					Ext.getCmp('column0').add(eval(col0));
					Ext.getCmp('column1').removeAll(false);
					Ext.getCmp('column1').add(eval(col1));
					Ext.getCmp('column2').removeAll(false);
					Ext.getCmp('column2').add(eval(col2));
					initfn();		     	    		
				}else{
					if(Ext.Msg.isVisible('loading')) Ext.Msg.hide('loading'); 
					Ext.Msg.alert('Message', "Fail to access database.");
				}
			}
		})

		// show default layout
		if(version==ver_2){
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/Login.do?act=SET_VERSION&version='+version,
				success:function(response,options){	
				Ext.getCmp('TabHead1').hide();
				Ext.getCmp('TabHead2').show();	    
			},		
			failure:function(){
				Ext.Msg.alert('Message', "Fail to access database.");												
			}
			});		    					    					   			
		}else if(version==ver_1){
			Ext.Ajax.request({
				url:'/Node.Administration/Page/Entry/Login.do?act=SET_VERSION&version='+version,
				success:function(response,options){						
				Ext.getCmp('TabHead1').show();	    
				Ext.getCmp('TabHead2').hide();
			},		
			failure:function(){
				Ext.Msg.alert('Message', "Fail to access database.");												
			}
			});		    					    					   			
		}
		//initial load page should hide LeftPanel
		if(Ext.getCmp('column0') == null)  Ext.getCmp('LeftPanel').hide();

//		viewport.doLayout();
//		Ext.Msg.wait('loading');   	
	};

	var getPlugInLayout = function(){

		cleanPlugIn();

		//send back  
		Ext.Ajax.request({
			url:'Configurations.do?method=getPlugInLayout',
			success:function(response,options){						
				var domainList = response.responseText;							
				var mySplitDomain = domainList.split(",");
				var mySplitPlugIn = plugInLayout.split(",");
				var isPlugInLayout = false;
	
				// Check the domain for no admin user
				if(mySplitDomain != null && mySplitDomain[0] != 'ADMIN'){
					// clear default plugin layout
					plugInLayout = "";
					for(var i=0;i<mySplitDomain.length;i++){
						var plugInName = plugInState + mySplitDomain[i] + plugInModule;
						//alert("This is plugInName : "+plugInName);
						for(var j=0;j<mySplitPlugIn.length;j++){
							if(mySplitPlugIn[j] == plugInName){
								isPlugInLayout = true;
								break;
							}
						}
						if(isPlugInLayout){
							if(i==0 || plugInLayout==""){
								plugInLayout = plugInName;
							}else{
								plugInLayout += "," + plugInName;						
							}
						}
						isPlugInLayout = false;
					}
				}
	
				getInitialParams();
			},		
			failure:function(){
				Ext.Msg.alert('Message', "Fail to initial the Plug In layout.");												
			}
		});
	};

	var removePanelIntoSetupLayoutPanel=function(event, toolEl, panel){
		if(Ext.getCmp('column0').items.length +  Ext.getCmp('column1').items.length +Ext.getCmp('column2').items.length> 1){
			var ownerPanel = panel.up();
			ownerPanel.setWidth(LeftPanelWidth);
			ownerPanel.collapse();
			ownerPanel.collapsible=false; 
			ownerPanel.ownerCt.remove(ownerPanel,false);
			Ext.getCmp('SetLayout').add(ownerPanel);
			Ext.getCmp('SetLayout').doLayout();	        		        		
		}else{
			Ext.Msg.alert('Message', 'You must select at least one panel.');	        		
		}	
	};
	