USE [Node2008]
GO
/****** Object:  StoredProcedure [dbo].[USP_GENERATE_TRANSACT_SVC_XML]    Script Date: 12/21/2011 17:44:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
Create PROCEDURE         [dbo].[USP_GENERATE_TRANSACT_SVC_XML]
( @p_TRANS_ID VARCHAR(100) =null
, @p_DATAFLOW VARCHAR(100) =null
, @p_STATUS VARCHAR(100) =null
, @p_TYPE VARCHAR(100) =null
, @p_USERID VARCHAR(100) =null
, @p_RECIPIENTS VARCHAR(100) =null
, @p_ORGANIZATION VARCHAR(100) =null
, @p_TO_DATE VARCHAR(100)
, @p_FROM_DATE VARCHAR(100)
, @p_RESULT  varchar(max) OUT
)
AS
declare @v_XML xml;
begin
	if (@p_TRANS_ID = '')
	begin
		set @p_TRANS_ID = null
	end
	if (@p_DATAFLOW = '')
	begin
		set @p_DATAFLOW = null
	end
	if (@p_STATUS = '')
	begin
		set @p_STATUS = null
	end
	if (@p_TYPE = '')
	begin
		set @p_TYPE = null
	end
	if (@p_USERID = '')
	begin
		set @p_USERID = null
	end
	if (@p_RECIPIENTS = '')
	begin
		set @p_RECIPIENTS = null
	end
	if (@p_ORGANIZATION = '')
	begin
		set @p_ORGANIZATION = null
	end
	
	set @v_XML =
		(select TRANS_ID as TransactionId,
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
			  convert(varchar(10),LOG.CREATED_DTTM,120) as CreationDateTime,
			  [USER_NAME] as UserId
		from NODE_OPERATION_LOG [LOG], NODE_OPERATION op, NODE_WEB_SERVICE ws
		where op.OPERATION_ID = [LOG].OPERATION_ID
		and op.WEB_SERVICE_ID = ws.WEB_SERVICE_ID
		and [LOG].TRANS_ID  = isnull(@p_TRANS_ID, [LOG].TRANS_ID )   -- LIKE CASE when len(@p_TRANS_ID)<2 then '%%' else @p_TRANS_ID end
		and op.OPERATION_NAME = isnull(@p_dataflow, op.OPERATION_NAME )   -- LIKE CASE when len(@p_dataflow)<2 then '%%' else @p_dataflow end
		and [LOG].[USER_NAME] = isnull(@p_USERID, [LOG].[USER_NAME] ) --LIKE CASE when len(@p_USERID)<2 then '%%' else @p_USERID end
		and ws.WEB_SERVICE_NAME = isnull(@p_TYPE, ws.WEB_SERVICE_NAME ) --LIKE CASE when len(@p_TYPE)<2 then '%%' else @p_TYPE end
		--    and OPERATION_LOG_ID in (select OPERATION_LOG_ID from NODE_OPERATION_LOG_STATUS where OPERATION_LOG_ID = [LOG].OPERATION_LOG_ID and OPERATION_LOG_STATUS_ID = 
		--                     (select max(OPERATION_LOG_STATUS_ID) from NODE_OPERATION_LOG_STATUS where OPERATION_LOG_ID = [LOG].OPERATION_LOG_ID and STATUS_CD = @p_STATUS))
		and [LOG].CREATED_DTTM >= convert(varchar(10),@p_FROM_DATE,120)
		and [LOG].CREATED_DTTM <= convert(varchar(10),@p_TO_DATE,120)
		for xml path('Transaction'))

	if (@v_XMl is null)
	begin
		set @p_RESULT = (select '<?xml version="1.0" encoding="UTF-8"?><TransactionList xmlns="http://www.exchangenetwork.net/schema/tts/1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"/>')
	end
	else
	begin
		set @p_RESULT = (select '<?xml version="1.0" encoding="UTF-8"?><TransactionList xmlns="http://www.exchangenetwork.net/schema/tts/1" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">' +
			(cast(@v_XML as varchar(max))) + '</TransactionList>')
	end
end
