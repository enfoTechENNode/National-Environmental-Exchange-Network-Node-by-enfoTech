
INSERT INTO SYS_CONFIG
           (CONFIG_ID,CONFIG_NAME,CONFIG_XML,CONFIG_TYPE_CD,STATUS_CD,CREATED_DTTM,CREATED_BY,UPDATED_DTTM,UPDATED_BY)
			select top 1 max(config_id)+1,'AQSTemplate_v2.xml'
           ,'<hdr:Document id="AirVision" xsi:schemaLocation="http://www.exchangenetwork.net/schema/header/2 http://www.exchangenetwork.net/schema/header/2/header_v2.0.xsd" xmlns:hdr="http://www.exchangenetwork.net/schema/header/2" xmlns:ds="http://www.w3.org/2000/09/xmldsig#" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
			<hdr:Header><hdr:AuthorName/><hdr:OrganizationName/><hdr:DocumentTitle/><hdr:CreationDateTime/><hdr:DataFlowName/><hdr:ApplicationUserIdentifier/>
			<hdr:Property><hdr:PropertyName>AQS.ScreeningGroup</hdr:PropertyName><hdr:PropertyValue/></hdr:Property>
			<hdr:Property><hdr:PropertyName>AQS.FinalProcessingStep</hdr:PropertyName><hdr:PropertyValue/></hdr:Property>
			<hdr:Property><hdr:PropertyName>AQS.PayloadType</hdr:PropertyName><hdr:PropertyValue/></hdr:Property>
			<hdr:Property><hdr:PropertyName>AQS.SchemaVersion</hdr:PropertyName><hdr:PropertyValue/></hdr:Property></hdr:Header>
			<hdr:Payload operation="Load"/></hdr:Document>'
           ,'XML','A',getdate(),'SYSTEM',getdate(),'SYSTEM'
           from sys_config ;

update sys_sequence_no set LAST_USED_NUMBER =(select max(config_id) from sys_config)
where TABLE_NAME = 'SYS_CONFIG' and COLUMN_NAME = 'CONFIG_ID';

GO


