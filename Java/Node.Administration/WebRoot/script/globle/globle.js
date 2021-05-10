	 // define globle parameter	    
	var ver_1 = 'VER_11';
	var ver_2 = 'VER_20';
	var version = ver_2;
    var LeftPanelWidth = 170;
    var transactionLogSearchWin,transactionLogMaxWin,transactionLogDetailWin,transactionLogIframe,
    domainSearchWin,domainMaxWin,domainEditWin,domainOperationsEditWin,newDomainWin,domainIframe,domainOperationsIframe,newDomainIframe,
    scheduledTasksLogSearchWin,scheduledTasksLogMaxWin,scheduledTasksLogDetailWin,scheduledTasksLogIframe,
    notificationLogSearchWin,notificationLogMaxWin,notificationLogDetailWin,notificationLogIframe,
    documentSearchWin,documentMaxWin,documentEditWin,documentIframe,uploadDocumentWin,
    favoriteLinksWin,favoriteLinksIframe,
    configurationWin,configurationIframe,
    dataWizardConfigWin,dataWizardConfigIframe,
    statusSearchWin,statusMaxWin,statusDetailWin,statusIframe,
    getServicesWin,getServicesUploadIframe,
    operationMgrWin,uploadOperationTemplateWin,operationMgrSubmitWin,operationMgrSubmitViewWin,uploadOperationWin,uploadOperationValidateWin,operationMgrFilterSetWin,operationMgrFilterConditionWin,operationMgrViewStyleSheetIframe,operationMgrViewStyleSheetWin,operationMgrValidateIframe,operationMgrValidateWin,operationMgrGenerateWin,operationMgrManagerWin,
    restServiceWin;
    var rowNum = 20;
	var topNum = 4;
	var pageSize = 25;
	var startIndexList = 0;
	var startIndex = 0;
	var myPortal1;

	// some fix information for operation search dropdown list
	var operationTypeData = [
		{'operationType':'SCHEDULED_TASK'},
		{'operationType':'WEB_SERVICE'}
    ];

	var opWebserviceData = [
	    {'opWebservice':'AUTHENTICATE'},
	    {'opWebservice':'DOWNLOAD'},
	    {'opWebservice':'EXECUTE'},
	    {'opWebservice':'GETSERVICES'},
	    {'opWebservice':'GETSTATUS'},
	    {'opWebservice':'NODEPING'},
	    {'opWebservice':'NOTIFY'},
	    {'opWebservice':'QUERY'},
	    {'opWebservice':'SOLICIT'},
	    {'opWebservice':'SUBMIT'}
    ];
	
 	//show or hide grids
 	var transactionLogGridShow = 'small';
 	var scheduledTasksLogGridShow = 'small';
	var domainGridShow = 'small'; 	
	var documentGridShow = 'small'; 	
	var notificationLogGridShow = 'small'; 
	var	statusGridShow = 'small';
	
 	// some fix information for domain search dropdown list
    var domainStatusData = [
        {'domainStatus':'Running'}, 
        {'domainStatus':'Stopped'}, 
        {'domainStatus':'Active'}, 
        {'domainStatus':'Inactive'}
    ];
    
 	// some fix information for Registration get services dropdown list 	
 	var nodeVersionIdentifierData = [
 	                                 {'nodeVersionIdentifier':'1.1'}, 
 	                                 {'nodeVersionIdentifier':'2.0'}
 	                                ]; 
    var nodeDeploymentTypeCodeData = [
                                      {'nodeDeploymentTypeCode':'Development'}, 
                                      {'nodeDeploymentTypeCode':'Test'}, 
                                      {'nodeDeploymentTypeCode':'Production'}
                                     ];
    var nodeStatusData = [
                          {'nodeStatus':'Operational'}, 
                          {'nodeStatus':'Busy'}, 
                          {'nodeStatus':'Offline'}, 
                          {'nodeStatus':'Unknown'}
                         ];
 	var methodNameData = [['Query'], ['Solicit'], ['Submit'], ['Execute']];
 	var RequiredData = [
 	                    {'AllowMultiSelect':'true'}, 
 	                    {'AllowMultiSelect':'false'}
 	                   ];
    var EncodingData = [['Base64'], ['ZIP'], ['Encrypt'], ['Digest'], ['XML'], ['None']];
    // WI 21296
    var Element_DataSourceType = [
                                  {'DataSourceType':'DBMS'}, 
                                  {'DataSourceType':'SOAP'}, 
                                  {'DataSourceType':'HTTP'}
                                 ];
    
 	// some fix information for operation list dropdown list 	
 	var EnableData = [
 	                  {'enabled':'True'}, 
 	                  {'enabled':'False'}
 	                 ];
 	var VersionData = [
 	                   {'enabled':'VER_11'}, 
 	                   {'enabled':'VER_20'}
 	                  ];

 	// some fix information for operationMgr list dropdown list 	
 	var sign = [
 	            {'sign':'='}, 
 	            {'sign':'>'}, 
 	            {'sign':'<'}, 
 	            {'sign':'>='}, 
 	            {'sign':'<='}, 
 	            {'sign':'like'}
 	            ];
 	var style = [
 	             {'style':'string'}, 
 	             {'style':'number'}
 	            ];

    // Plug in layout
   	var plugInState = '';
   	var plugInModule = '';
   	var plugInLayout = '';
    
    var NodeHeader = '<table id="NodeHeader" border="0" bgcolor="#0055A6" width="100%">'
	       + '<tr valign="top">'
	       +    '<td colspan="2" bgcolor="#0055A6" height="58px" width="300px"><a href="http://www.enfotech.com/"><img src="../../images/Header/header.gif" style="border-width:0px;" /></a></td>'
	       +  '</tr>'
	       +'<tr class="HeaderText" valign="top" style="background-color: #396EA0">'
	       +     '<td width="6px"></td>'
	       +    '<td><a href="Login.do?logout=true" style="color:White; ">Logout</a></td>'
	       +   '<td align="right"><a href="http://www.enfotech.com/" target="_blank" style="color:White; ">enfoTech & Consulting, Inc. Web Policy</a> - <a href="http://www.enfotech.com/enfoWebApp/pages/company/Contact.aspx" style="color:White;" target="_blank">Contact Us</a></td>'
	       +  '<td width="6px"></td>'
	       + '</tr>'
	       +'</table>';

    var NodeFooter = '<table id="NodeFooter" border="0" width="100%" height="100%">'
   		+'<tr>'
    	+'<td align="left"><font style="font-family:Arial; font-size:8pt; color:#666666;">'
	    + 'Copyright © 2013 by enfoTech & Consulting Inc. All Rights Reserved. </font>'
	    + '</td>'
	    + '<td class="logo" align="right" valign="center"><a href="http://www.enfotech.com" target="_blank"><img id="ctl00_footerContent_footer_Image1" src="/Node.Administration/images/TailLogo.gif" /></a></td>'
	    +'</tr>'
	    +'</table>';

    // define the interval of status refresh
 	var statusRefreshInterval = 60000; // 6 seconds
 	
 	// define time out for loading data by milliseconds 
 	var gridTimeout = 3000000;  //50 minutes