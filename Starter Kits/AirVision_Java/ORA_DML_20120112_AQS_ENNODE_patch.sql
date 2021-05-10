--**************************************************************************************************
--* Product Name:       -- ENNODE (Java)
--* Client(s):          -- ALL
--**************************************************************************************************
--* Description:        -- Insert table SYS_CONFIG data . 
--* Modified: 		-- Enfotech 20120112
--**************************************************************************************************
--**************************************************************************************************

DECLARE 
	IMAX_ID NUMBER := 0;
	ICOUNT NUMBER := 0;

BEGIN

/* INSERT SYS_CONFIG */

  SELECT count(*) INTO ICOUNT from SYS_CONFIG WHERE CONFIG_NAME = 'AQSTemplate_v2.xml';  
  IF ICOUNT = 0 THEN	
	  /* INSERT DEFAULT GETSERVICES.CONFIG FILE WITH SAMPLE DATA*/
	SELECT (MAX(config_id)+1) INTO IMAX_ID FROM sys_config;		
  INSERT INTO sys_config (config_id,config_name,config_xml,config_type_cd,status_cd,created_dttm,created_by,updated_dttm,updated_by,CONFIG_CLOB)
  VALUES (25,'AQSTemplate_v2.xml',XMLType.createXML('<hdr:Document id="AirVision" xsi:schemaLocation="http://www.exchangenetwork.net/schema/header/2 http://www.exchangenetwork.net/schema/header/2/header_v2.0.xsd" xmlns:hdr="http://www.exchangenetwork.net/schema/header/2" xmlns:ds="http://www.w3.org/2000/09/xmldsig#" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
	<hdr:Header>
		<hdr:AuthorName/>
		<hdr:OrganizationName/>
		<hdr:DocumentTitle/>
		<hdr:CreationDateTime/>
		<hdr:DataFlowName/>
		<hdr:ApplicationUserIdentifier/>
		<hdr:Property>
			<hdr:PropertyName>AQS.ScreeningGroup</hdr:PropertyName><hdr:PropertyValue/>
		</hdr:Property>
		<hdr:Property>
			<hdr:PropertyName>AQS.FinalProcessingStep</hdr:PropertyName><hdr:PropertyValue/>
		</hdr:Property>
		<hdr:Property>
			<hdr:PropertyName>AQS.PayloadType</hdr:PropertyName><hdr:PropertyValue/>
		</hdr:Property>
		<hdr:Property>
			<hdr:PropertyName>AQS.SchemaVersion</hdr:PropertyName><hdr:PropertyValue/>
		</hdr:Property>
	</hdr:Header>
	<hdr:Payload operation="Load"/>
</hdr:Document>'),'XML','A',sysdate,'system',sysdate,'system',null);
  END IF;

COMMIT;
END;   
/
