Ext.onReady(function(){

	// initial all tips
  		Ext.QuickTips.init();

	node_1Iframe = Ext.create('Ext.ux.IFrame',{  
		id : 'node_1Iframe'
	});	
		
	node_2Iframe = Ext.create('Ext.ux.IFrame',{  
		id : 'node_2Iframe'
	});		

	// set up layout
	 	var HeaderPanel = Ext.create('Ext.panel.Panel',{
            region:'north',
            id:'HeaderPanel',
            split:false,
            border:false, 
           	hideBorders:true,
            collapsible: false,
            margins:'0 0 0 0',
            layout:'fit',
            height:60,
            html: Ext.get('header').getHTML()
//            html:            	
//        		'<div id = "header" style=" border:1px solid #ffffff; background-color:#0055A6;">'
//				+'    <table border="0" cellpadding="0" cellspacing="0" bgcolor="#0055A6" width="100%">'
//				+'        <tr valign="top">'
//				+'            <td><a href="http://www.enfotech.com/"><img src="../../images/Header/header.gif" style="border-width:0px;" /></a></td>'
//				+'        </tr>'
//				+'    </table>'
//				+'</div>'           	
        });
        
        var LeftPanel = Ext.create('Ext.panel.Panel',{
                region:'west',
                id:'LeftPanel',
                title:'Web Services',
                split:true,
                width: LeftPanelWidth,
                minSize: 130,
                maxSize: 400,
                collapsible: true,
                margins:'0 0 0 2',
                layout:'anchor',
                layoutConfig:{
                    animate:true
                },
                items: [{
                	xtype:'panel',
                	header :false,
                    contentEl: 'LeftManu',
                    border:false,
                    iconCls:'nav'
                }]
            });
              
		var node1 = Ext.create('Ext.panel.Panel',{
		           id: "node1",
		           title: 'Node 1.1',
		           layout:'fit',
		           autoScroll:true,
		           initEvents : function(){
						Ext.getCmp('node_1Iframe').load('/Node.Client/Page/WebMethods/NodePing.do?version='+ver_1);
				   },
		           items: [node_1Iframe]
		     });
		     
		var node2 = Ext.create('Ext.panel.Panel',{
	              id: "node2",
	              title: 'Node 2.0',
		          layout:'fit',
	              autoScroll:true,
		          initEvents : function(){
						Ext.getCmp('node_2Iframe').load('/Node.Client/Page/WebMethods/NodePing.do?version='+ver_2);
				  },
		          items: [node_2Iframe]
	     	});
		          
	   var Centerbanner = Ext.create('Ext.panel.Panel',{
		    region:'north',
            id:'Centerbanner',
            split:false,
            border:false, 
           	hideBorders:true,
            collapsible: false,
            margins:'0 0 0 0',
            layout:'fit',
            height:30,
            html: Ext.get('centerbanner').getHTML()
//            html:
//            	'<div id = "centerbanner">'
//            	+'    <table border="0" cellpadding="0" cellspacing="0" width="100%">'
//            	+'        <tr valign="top">'
//            	+'            <td><a href="http://www.enfotech.com/"><img src="../../images/Header/Node_gen.gif" style="border-width:0px;" /></a></td>'
//            	+'        </tr>'
//            	+'    </table>'
//            	+'</div>'
	   });
		var CenterPanelTab = Ext.create('Ext.tab.Panel',{
		              region:'center',
		              id:'CenterPanelTab',
		              deferredRender:false,
		              activeTab:0,
					  listeners: {
					    'tabchange':function(tabPanel,tab){
								if(tab.getId()=='node1'){
									Ext.getCmp('node1').show();			
									Ext.getCmp('node2').hide();			
									version=ver_1;
								}else{
									Ext.getCmp('node2').show();			
									Ext.getCmp('node1').hide();			
									version=ver_2;
								}  	
						}
					  }, 	                    
		              items:[node1,node2]
		      });
		      
		var CenterPanel = Ext.create('Ext.panel.Panel',{
		    region:'center',
            id:'CenterPanel',
            layout:'border',
            items: [Centerbanner,CenterPanelTab]
        });
           
		var viewport = Ext.create('Ext.container.Viewport',{
		      layout:'border',
		      items:[HeaderPanel,LeftPanel,CenterPanel]
		  	});
		  	
		viewport.doLayout();
});

