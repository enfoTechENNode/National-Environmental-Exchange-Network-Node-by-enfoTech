// open DataWizardConfiguration window    
   var openDataWizardConfigWin=function(url){
	   if(version==ver_2){
			if(!dataWizardConfigWin){
				dataWizardConfigIframe = Ext.create('Ext.ux.IFrame',{  
					id : 'dataWizardConfigIframe'
				});		
				dataWizardConfigWin = new Ext.Window({
					id:'dataWizardConfigWin',
					layout      : 'fit',
					width       : 600,
					height      : 200,
					minWidth 	: 300,
					miniHeight	: 200,
					closeAction :'hide',
					maximizable:true,
					autoScroll:true,
					modal:false,
					plain       : true,
					constrainHeader:true,
					items : [dataWizardConfigIframe]
				});
			}
			if(!dataWizardConfigWin.isHidden()){
				dataWizardConfigIframe.load(url);			
			}else{
				dataWizardConfigWin.on('show',function(){
					dataWizardConfigIframe.load(url);
				});
			}
			dataWizardConfigWin.show('configurationPortlet');		   
	   }else if(version==ver_1){
	   		Ext.Msg.alert('Message', 'This is Node 2.0 function.');
	   }

   };

