// open favoriteLinks window 
	Ext.define('restServiceModel', {
        extend: 'Ext.data.Model',
        fields: [
            {name: 'header', mapping: 'header'},
            {name: 'content', mapping: 'content'}
        ]
    });
	
	var openRestServiceWin=function(url){
		Ext.Ajax.request({
		    url: 'Configurations.do?method=handleRestServiceIntroduction',
		    success: function(response){
		        var text = response.responseText;
		        if(text.indexOf('Administrator') == -1){
		    		if(!restServiceWin){
		    			restServiceWin = Ext.create('Ext.window.Window', {
		    				id:'restServiceWin',
		    				layout      : 'fit',
		    				width       : 550,
		    				height      : 450,
		    				minWidth 	: 300,
		    				miniHeight	: 300,
		    				closeAction :'hide',
		    				maximizable:true,
		    				autoScroll:true,
		    				modal:false,
		    				plain       : true,
		    				constrainHeader:true,
		    				items : [{
		    			        xtype: 'form',
		    			        layout: 'form',
		    			        collapsible: false,
		    			        id: 'restServiceIntroductionForm',
		    			        url: 'Configurations.do?method=handleRestServiceIntroduction',
		    			        frame: true,
		    			        title: 'REST Page Configuration',
		    			        bodyPadding: '5 5 0',
		    			        width: 350,
		    			        fieldDefaults: {
		    			            msgTarget: 'side',
		    			            labelWidth: 75
		    			        },
		    			        defaultType: 'textfield',
		    			        reader : Ext.create('Ext.data.reader.Json', {
		    			            model: 'restServiceModel'
		    			        }),
		    			        writer:Ext.create('Ext.data.writer.Json', {
		    			            model: 'restServiceModel',
		    		            	writeAllFields: true,
		    		                nameProperty: 'mapping'
		    			        }),
		    			        items: [{
		    			            fieldLabel: 'Header',
		    			            name: 'header',
		    			            allowBlank: false
		    			        },{
		    			        	xtype: 'textarea',
		    			            fieldLabel: 'Content',
		    			            name: 'content',
		    			            height :320,
		    			            allowBlank: false
		    			        }],

		    			        buttons: [{
		    			            text: 'Save',
		    			            handler: function() {
		    			                var form = this.up('form').getForm();
		    			                if (form.isValid()) {
		    			                	if(!Ext.Msg.isVisible('loading')) Ext.Msg.wait('loading');
		    			                    form.submit({
		    			                    	params: {
		    			                    		act: 'save'
		    			                        },
		    			                        success: function(form, action) {
		    			                        	if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');
		    			                        	Ext.Msg.alert('Success', action.result.msg);
		    			                        },
		    			                        failure: function(form, action) {
		    			                        	if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');
		    			                        	Ext.Msg.alert('Failed', action.result.msg);
		    			                        }
		    			                    });
		    			                }else{
		    			                	Ext.Msg.alert('Warning', 'Please fill the header and content before save.');
		    			                }
		    			            }
		    			        },{
		    			            text: 'Undo Changes',
		    			            handler: function() {
		    		                	if(!Ext.Msg.isVisible('loading')) Ext.Msg.wait('loading');
		    			                this.up('form').getForm().load({
		    			                        success: function(form, action) {
		    			                        	if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');
		    			                        },
		    			                        failure: function(form, action) {
		    			                        	if(Ext.Msg.isVisible('loading')) Ext.Msg.close('loading');
		    			                        }
		    			                });
		    			            }
		    			        },{
		    			            text: 'Clear',
		    			            handler: function() {
		    			                this.up('form').getForm().reset();
		    			            }
		    			        }]
		    				}]
		    			});
		    		}
		    		Ext.getCmp('restServiceIntroductionForm').load();
		    		restServiceWin.show('configurationPortlet');		        	
		        }else{
		        	Ext.Msg.alert('Failed', text);
		        }
		    }
		});

   };

