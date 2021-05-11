// open favoriteLinks window    
   var openFavoriteLinksWin=function(url){
		if(!favoriteLinksWin){
			favoriteLinksIframe = Ext.create('Ext.ux.IFrame',{  
				id : 'favoriteLinksIframe'
			});		
			favoriteLinksWin = new Ext.Window({
				id:'favoriteLinksWin',
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
				items : [favoriteLinksIframe]
			});
		}
		if(!favoriteLinksWin.isHidden()){
			favoriteLinksIframe.load(url);			
		}else{
			favoriteLinksWin.on('show',function(){
				favoriteLinksIframe.load(url);
			});
		}
		favoriteLinksWin.show('favoriteLinksPortlet');

   };

