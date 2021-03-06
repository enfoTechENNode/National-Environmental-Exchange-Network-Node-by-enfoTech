USE [Node2008]
GO
/****** Object:  StoredProcedure [dbo].[SP_GET_DEDL_XML]    Script Date: 12/22/2011 11:02:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[USP_GET_DEDL_XML] (@sp_output varchar(max) output)
	-- Add the parameters for the stored procedure here
AS
BEGIN

	declare @DEDLExisted int
	declare @ConfigID as int 
	declare @DEDLXML as varchar(max)  

	SET NOCOUNT ON;
	
	set @DEDLXML = '<?xml version="1.0" encoding="UTF-8"?><DataElementList xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.exchangenetwork.net/schema/dedl/1" xmlns="http://www.exchangenetwork.net/schema/dedl/1" />'
	
	set @DEDLExisted = (select count(*) from sys_config where config_name='DEDL.XML')
	
	if (@DEDLExisted = 0)
	begin
	
		set @ConfigID = (select Last_used_number from sys_sequence_no where table_name = 'sys_config' and column_name='config_id')
		set @ConfigID = @ConfigID +1
		
		INSERT INTO SYS_CONFIG([CONFIG_ID],[CONFIG_NAME],[CONFIG_XML]
           ,[CONFIG_TYPE_CD],[STATUS_CD],[CREATED_DTTM],[CREATED_BY],[UPDATED_DTTM],[UPDATED_BY])
		VALUES(@ConfigID,'DEDL.XML',@DEDLXML,'XML','A',getdate(),'system',getdate(),'System')
		
		update sys_sequence_no set Last_used_number = @ConfigID where table_name = 'sys_config' and column_name='config_id'
		
	end
	
	set @sp_output = (select config_xml from SYS_CONFIG where CONFIG_NAME = 'DEDL.xml')

END
