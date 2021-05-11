// open favoriteLinks window    
   var openConfigurationWin=function(url){
		if(!configurationWin){
			configurationIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'configurationIframe'
			});		
			configurationWin = Ext.create('Ext.window.Window', {
				id:'configurationWin',
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
				items : [configurationIframe]
			});
		}
		if(!configurationWin.isHidden()){
			configurationIframe.load(url);				
		}else{
			configurationWin.on('show',function(){
				configurationIframe.load(url);				
			});
		}
		configurationWin.show('configurationPortlet');

   };

