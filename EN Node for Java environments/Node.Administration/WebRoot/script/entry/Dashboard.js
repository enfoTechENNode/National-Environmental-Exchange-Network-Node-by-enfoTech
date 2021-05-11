Ext.onReady(function(){

	Ext.QuickTips.init();

	Ext.create('Ext.app.Portal',
			{id:'nodePortal'
			});
	Ext.getCmp('LeftPanel').hide();
	getPlugInLayout();

});

