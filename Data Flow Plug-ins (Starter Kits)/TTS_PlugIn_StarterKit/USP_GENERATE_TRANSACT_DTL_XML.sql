USE [Node2008]
GO
/****** Object:  StoredProcedure [dbo].[USP_GENERATE_TRANSACT_DTL_XML]    Script Date: 12/21/2011 17:41:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
ALTER PROCEDURE         [dbo].[USP_GENERATE_TRANSACT_DTL_XML]
( @p_TRANS_ID VARCHAR(100) = null
,  @p_RESULT varchar(max) OUT)
AS

--**************************************************************************************************
--* Object Name:        -- USP_GENERATE_TRANSACT_DTL_XML
--* Product Name:       -- EN-Node
--* Platform:           -- MS SQL
--* Client(s):          -- All
--* Description:        -- To generate XML file containing transaction details
--* Created by:         -- enfoTech Consulting, Inc./Doug Timms
--* Modified            -- 12/9/2010
--**************************************************************************************************
--declare  @p_RESULT  VARCHAR(max)
declare @v_Header varchar(max);
declare @v_CLOBtemp varchar(max);
declare @v_XML xml;

BEGIN 
set @v_XML =
		(
		select      [LOG].TRANS_ID as TransactionId,
					  op.OPERATION_NAME as DataflowName,
					  ws.WEB_SERVICE_NAME as TransactionType,
					  (select STATUS_CD from NODE_OPERATION_LOG_STATUS 
						  where OPERATION_LOG_ID = [LOG].OPERATION_LOG_ID 
						  and OPERATION_LOG_STATUS_ID = 
							 (select max(OPERATION_LOG_STATUS_ID) from NODE_OPERATION_LOG_STATUS where OPERATION_LOG_ID = [LOG].OPERATION_LOG_ID)) as TransactionStatus,
					  (select substring([MESSAGE],1,100) from NODE_OPERATION_LOG_STATUS 
						  where OPERATION_LOG_ID = [LOG].OPERATION_LOG_ID 
						  and OPERATION_LOG_STATUS_ID = 
							 (select max(OPERATION_LOG_STATUS_ID) from NODE_OPERATION_LOG_STATUS where OPERATION_LOG_ID = [LOG].OPERATION_LOG_ID)) as StatusDescription,
					  replace(convert(varchar(19), [LOG].CREATED_DTTM,120),' ','T') as [CreationDateTime],
					  USER_NAME as UserId,
							 (SELECT 'StatusUpdate' AS ActivityName,
									 STATUS_CD AS ActivityStatus,
									 substring([MESSAGE],1,50) AS [MESSAGE],
									 replace(convert(varchar(19),CREATED_DTTM,120) ,' ','T') AS [Timestamp]
					   --        select top 5 *              
							   FROM NODE_OPERATION_LOG_STATUS sts
							   WHERE sts.OPERATION_LOG_ID = [LOG].OPERATION_LOG_ID
							   for xml path('Activity'), type
							 ) as ActivityList,
							 (SELECT [FILE_ID] AS DocumentId,
									 [FILE_NAME] AS DocumentName,
									 [FILE_NAME] AS FileName,
									 [FILE_TYPE] AS DocumentType
							   FROM NODE_FILE_CABIN doc
							   WHERE doc.TRANS_ID = [LOG].TRANS_ID 
							   for xml path('Document'), type
							 ) as DocumentList

			from NODE_OPERATION_LOG [LOG] , NODE_OPERATION op, NODE_WEB_SERVICE ws
			where op.OPERATION_ID = [LOG].OPERATION_ID
			and op.WEB_SERVICE_ID = ws.WEB_SERVICE_ID
			and [LOG].TRANS_ID = @p_TRANS_ID
			for xml path('')

		 )
	if (@v_XML is null)
	begin
		set @p_RESULT = (Select  '<?xml version="1.0" encoding="UTF-8"?><TransactionDetail xmlns="http://www.exchangenetwork.net/schema/tts/1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"/>')
	end
	else
	begin
		set @p_RESULT = (Select  '<?xml version="1.0" encoding="UTF-8"?><TransactionDetail xmlns="http://www.exchangenetwork.net/schema/tts/1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">' +
				   cast(@v_XML as varchar(max)) +'</TransactionDetail>')
    end 
END 