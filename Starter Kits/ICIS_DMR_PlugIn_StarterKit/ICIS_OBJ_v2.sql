/****** Object:  UserDefinedFunction [dbo].[fn_get_Decimal8float]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[fn_get_Decimal8float] (@value varchar(20) )
returns varchar(20)
as
begin

declare  @full_len int, @decimal_len int, @decimal_ind int
select @decimal_ind = charindex('.', @value)
	if  (@decimal_ind > 0)
			begin
				select  @full_len = len(@value) 
				select @decimal_len = len(substring(@value, charindex('.', @value)+1, len(@value)))
				if @decimal_len > 8
			    set @value =  cast(@value as numeric(20,8))
			end			 
	 
			return @value
end
GO

/****** Object:  StoredProcedure [dbo].[icis_dmr_replace_insert2]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[icis_dmr_replace_insert2] (@transaction_id varchar(100) , @error  varchar(max) output)
as
DECLARE @output varchar(max) 
--		,@error  varchar(max) 
begin
	SET CONCAT_NULL_YIELDS_NULL OFF

	begin 
		declare reports cursor for		
        select a.submission_id_int,a.transaction_id,a.station_id,a.monitor_pt_id,a.monitor_pt_version,a.permit_no,a.station_no,a.report_designator,a.mp_end_date,a.rpt_sign_date,a.PrincipalExecutiveOfficerFirstName,a.PrincipalExecutiveOfficerLastName,a.PrincipalExecutiveOfficerTitle,a.PrincipalExecutiveOfficerTelephone,a.SignatoryFirstName,a.SignatoryLastName,a.certifier_phone,a.comment,a.no_discharge_site,a.received_dttm 
               from icis_dmr_report a 
		declare @reports_fetch_status int
		declare @submission_id_int int
		declare @internal_trans_id int
		declare @station_id int
		declare @monitor_pt_id int
		declare @monitor_pt_version int
		declare @permit_no char(20)
		declare @station_no char(10)
		declare @report_designator char(5)
		declare @mp_end_date datetime
		declare @rpt_sign_date datetime
		declare @peofn VARCHAR(1000)
		declare @peoln VARCHAR(1000)
		declare @peoti VARCHAR(1000)
		declare @peote VARCHAR(1000)
		declare @sfn VARCHAR(1000)
		declare @sln VARCHAR(1000)
		declare @certifier_phone VARCHAR(15)
		declare @comment VARCHAR(1000)
		declare @no_discharge_site char(1)
		declare @received_dttm datetime
		
		declare @results_fetch_status int
		declare @par_code char(5)
		declare @stage_code varchar(2)
		declare @LimitSeasonNumber VARCHAR(5)
		declare @sample_type VARCHAR(20)
		declare @sample_freq VARCHAR(20)
		declare @excursion VARCHAR(20)
		declare @conc_unit VARCHAR(20)
		declare @load_unit VARCHAR(20)
		declare @load_1 VARCHAR(20)
		declare @load_1_nodi VARCHAR(20)
		declare @load_2 VARCHAR(20)
		declare @load_2_nodi VARCHAR(20)
		declare @conc_1 VARCHAR(20)
		declare @conc_1_nodi VARCHAR(20)
		declare @conc_2 VARCHAR(20)
		declare @conc_2_nodi VARCHAR(20)
		declare @conc_3 VARCHAR(20)
		declare @conc_3_nodi VARCHAR(20)



--20111102------------------------------------------------------------------------------------------------------------------------------------------

/*** update excursion ***/
/*
update icis_dmr_numeric_results
set excursion = null
where excursion =0
*/

delete from icis_dmr_report where permit_no LIKE 'NEC%'


delete from icis_dmr_numeric_results  
where cast(submission_id_int as varchar(50)) + '.' + cast(transaction_id as varchar(50)) + '.' + cast(monitor_pt_id as varchar(50)) + '.'+  cast(monitor_pt_version as varchar(50)) 
in 
(select cast(submission_id_int as varchar(50)) + '.' + cast(transaction_id as varchar(50)) + '.' + cast(monitor_pt_id as varchar(50)) + '.'+  cast(monitor_pt_version as varchar(50)) from icis_dmr_report where permit_no LIKE 'NEC%')

/** modified 20110510****/
update dbo.icis_dmr_numeric_results
set LimitSeasonNumber = -1

update icis_dmr_numeric_results
set excursion = (case when (len(rtrim(ltrim(load_1))) > 0 or   len(rtrim(ltrim(load_2))) > 0)
					   or (len(rtrim(ltrim(conc_1))) > 0 or  len(rtrim(ltrim(conc_2))) > 0 or len(rtrim(ltrim(conc_3))) > 0)
					then isnull(excursion, 1) 
					else  '0'
					end )
					

update icis_dmr_numeric_results
set load_1 = (case when (UPPER(load_1) = 'Y'  or UPPER(load_1) = 'YES') THEN '1' WHEN (UPPER(load_1) ='N' or UPPER(load_1) = 'NO') THEN '0' ELSE load_1 END)
,load_2 = (case when (UPPER(load_2) = 'Y'  or UPPER(load_2) = 'YES') THEN '1' WHEN (UPPER(load_2) ='N' or UPPER(load_2) = 'NO') THEN '0' ELSE load_2 END)
,CONC_1 = (case when (UPPER(CONC_1) = 'Y'  or UPPER(CONC_1) = 'YES') THEN '1' WHEN (UPPER(CONC_1) ='N' or UPPER(CONC_1) = 'NO') THEN '0' ELSE CONC_1 END)
,CONC_2 = (case when (UPPER(CONC_2) = 'Y'  or UPPER(CONC_2) = 'YES') THEN '1' WHEN (UPPER(CONC_2) ='N' or UPPER(CONC_2) = 'NO') THEN '0' ELSE CONC_2 END)
,CONC_3 = (case when (UPPER(CONC_3) = 'Y'  or UPPER(CONC_3) = 'YES') THEN '1' WHEN (UPPER(CONC_3) ='N' or UPPER(CONC_3) = 'NO') THEN '0' ELSE CONC_3 END)



update icis_dmr_numeric_results
set load_1 = cast(round(load_1,8) as numeric(38,8))  
where isnumeric(load_1) =1

update icis_dmr_numeric_results
set load_2 = cast(round(load_2,8) as numeric(38,8)) 
where isnumeric(load_2) =1


--SELECT * FROM icis_dmr_numeric_results

/*** replace station_no (DWN, CML)  ****/

update icis_dmr_report 
set station_no = replace(replace(replace( station_no, 'DWN',''), 'CML',''),'-','')




/******   update nodi  ********/

update a
set load_1_nodi = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.load_1_nodi = b.LEGACY_CODE
where b.new_table_name = 'ref_nodi' and a.load_1_nodi is not null



update a
set load_2_nodi = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.load_2_nodi = b.LEGACY_CODE
where b.new_table_name = 'ref_nodi' and a.load_2_nodi is not null



update a
set conc_1_nodi = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.conc_1_nodi = b.LEGACY_CODE
where b.new_table_name = 'ref_nodi' and a.conc_1_nodi is not null


update a
set conc_2_nodi = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.conc_2_nodi = b.LEGACY_CODE
where b.new_table_name = 'ref_nodi' and a.conc_2_nodi is not null


update a
set conc_3_nodi = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.conc_3_nodi = b.LEGACY_CODE
where b.new_table_name = 'ref_nodi' and a.conc_3_nodi is not null





update a
set par_code = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.par_code collate  SQL_Latin1_General_CP1_CI_AS = b.LEGACY_CODE
where b.new_table_name = 'ref_parameter' and a.par_code is not null



update a
set conc_unit = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.conc_unit collate  SQL_Latin1_General_CP1_CI_AS = b.LEGACY_CODE
where b.new_table_name = 'ref_unit' and a.conc_unit is not null


update a
set load_unit = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.load_unit collate  SQL_Latin1_General_CP1_CI_AS = b.LEGACY_CODE
where b.new_table_name = 'ref_unit' and a.load_unit is not null


update a
set sample_freq = b.new_code
---select a.*, b.LEGACY_CODE
from icis_dmr_numeric_results a join dbo.convert_pcs_to_icis b
on a.sample_freq collate  SQL_Latin1_General_CP1_CI_AS = b.LEGACY_CODE
where b.new_table_name = 'ref_frequency_of_analysis' and a.sample_freq is not null



--------------------------------------------------------------------------------------------------------------------------------------------
	update icis_dmr_numeric_results
	set load_1 = cast(cast(load_1 as float) as numeric(20,10))
	where load_1 like '%E-%'


	update icis_dmr_numeric_results
	set load_2 = cast(cast(load_2 as float) as numeric(20,10))
	where load_2 like '%E-%'



	   /********  start loop   *****/


	--	set @output = '<?xml version="1.0" encoding="UTF-8"?>'
		set @output = ''



	   /********  start loop per submission_id_int  inwert dmr data into dbo.icis_dmr_report_data  *****/
		begin try
		 
				open reports
				set @output = ''

				fetch next from reports into	@submission_id_int,
												@internal_trans_id,
												@station_id,
												@monitor_pt_id,
												@monitor_pt_version,
												@permit_no,
												@station_no,
												@report_designator,
												@mp_end_date,
												@rpt_sign_date,
												@peofn,
												@peoln,
												@peoti,
												@peote,
												@sfn,
												@sln,
												@certifier_phone,
												@comment,
												@no_discharge_site,
												@received_dttm
				set @reports_fetch_status = @@fetch_status

				while @reports_fetch_status = 0
				begin
		/******** modified  ********/
					delete from dbo.icis_dmr_report_data where submission_id_int = @submission_id_int -- and transaction_id = @internal_trans_id

					set @output = @output + '<DischargeMonitoringReportData>'
						set @output = @output + '<TransactionHeader>'
							set @output = @output + '<TransactionType>R</TransactionType>'
							set @output = @output + '<TransactionTimestamp>' + convert(VARCHAR(50),current_timestamp,126) + '</TransactionTimestamp>'
						set @output = @output + '</TransactionHeader>'

				
						set @output = @output + '<DischargeMonitoringReport>'
							set @output = @output + '<PermitIdentifier>' + RTRIM(LTRIM(@permit_no)) + '</PermitIdentifier>'
							set @output = @output + '<PermittedFeatureIdentifier>' + RTRIM(LTRIM(@station_no)) + '</PermittedFeatureIdentifier>'
							set @output = @output + '<LimitSetDesignator>' + RTRIM(LTRIM(@report_designator)) + '</LimitSetDesignator>'
							set @output = @output + '<MonitoringPeriodEndDate>' + convert(VARCHAR(10),@mp_end_date,126) + '</MonitoringPeriodEndDate>'
						if @rpt_sign_date is not null
						begin
							set @output = @output + '<SignatureDate>' + convert(VARCHAR(10),@rpt_sign_date,126) + '</SignatureDate>'
						end
						if @peofn is not null
						begin
							set @output = @output + '<PrincipalExecutiveOfficerFirstName>' + @peofn + '</PrincipalExecutiveOfficerFirstName>'
						end
						if @peoln is not null
						begin
							set @output = @output + '<PrincipalExecutiveOfficerLastName>' + @peoln + '</PrincipalExecutiveOfficerLastName>'
						end
						if @peoti is not null
						begin
							set @output = @output + '<PrincipalExecutiveOfficerTitle>' + @peoti + '</PrincipalExecutiveOfficerTitle>'
						end
						if @peote is not null
						begin
							set @output = @output + '<PrincipalExecutiveOfficerTelephone>' + @peote + '</PrincipalExecutiveOfficerTelephone>'
						end
						if @sfn is not null
						begin
							set @output = @output + '<SignatoryFirstName>' + @sfn + '</SignatoryFirstName>'
						end
						if @sln is not null
						begin
							set @output = @output + '<SignatoryLastName>' + @sln + '</SignatoryLastName>'
						end
						if @certifier_phone is not null
						begin
							set @output = @output + '<SignatoryTelephone>' + @certifier_phone + '</SignatoryTelephone>'
						end
						if @comment is not null
						begin
							set @output = @output + '<ReportCommentText>' + @comment + '</ReportCommentText>'
						end
						if @no_discharge_site <> 'N'
						begin
							set @output = @output + '<DMRNoDischargeIndicator>' + @no_discharge_site + '</DMRNoDischargeIndicator>'
							set @output = @output + '<DMRNoDischargeReceivedDate>' + convert(VARCHAR(10),@received_dttm,126) + '</DMRNoDischargeReceivedDate>'
						end
						else
						begin
							declare results cursor for
								select	par_code,
										stage_code,
										limitSeasonNumber,
										sample_type,
										sample_freq,
										excursion,
										conc_unit,
										load_unit,
										ISNULL((case when isnumeric(load_1) = 0 then load_1  END), dbo.fn_get_Decimal8float(load_1)) as load_1,  --MODIFIED 20111021   FROM load_1 AS load_1 ,
										load_1_nodi,
										ISNULL((case when isnumeric(load_2) = 0 then load_2  END), dbo.fn_get_Decimal8float(load_2)) as load_2,  --MODIFIED 20111021   FROM load_1 AS load_1 , 
										load_2_nodi,
										ISNULL((case when isnumeric(conc_1) = 0 then conc_1  END), dbo.fn_get_Decimal8float(conc_1)) as conc_1,  --MODIFIED 20111021   FROM load_1 AS load_1 ,
										conc_1_nodi,
										ISNULL((case when isnumeric(conc_2) = 0 then conc_2  END), dbo.fn_get_Decimal8float(conc_2)) as conc_2,  --MODIFIED 20111021   FROM load_1 AS load_1 ,
										conc_2_nodi,
										ISNULL((case when isnumeric(conc_3) = 0 then conc_3  END), dbo.fn_get_Decimal8float(conc_3)) as conc_3,  --MODIFIED 20111021   FROM load_1 AS load_1 , 
										conc_3_nodi 
									from icis_dmr_numeric_results
									where submission_id_int = @submission_id_int
										and transaction_id = @internal_trans_id
										and monitor_pt_id = @monitor_pt_id
										and monitor_pt_version = @monitor_pt_version
                                        and (len(rtrim(ltrim(isnull(load_1,'')))) + len(rtrim(ltrim(isnull(load_2,'')))) + len(rtrim(ltrim(isnull(conc_1,'')))) + len(rtrim(ltrim(isnull(conc_2,'')))) + len(rtrim(ltrim(isnull(conc_3,'')))) ) !=0  --MODIFIED 20111018

							open results

							fetch next from results into	@par_code,
															@stage_code,
															@LimitSeasonNumber,
															@sample_type,
															@sample_freq,
															@excursion,
															@conc_unit,
															@load_unit,
															@load_1,
															@load_1_nodi,
															@load_2,
															@load_2_nodi,
															@conc_1,
															@conc_1_nodi,
															@conc_2,
															@conc_2_nodi,
															@conc_3,
															@conc_3_nodi

							set @results_fetch_status = @@fetch_status

							while @results_fetch_status = 0
							begin
								set @output = @output + '<ReportParameter>'
										set @output = @output + '<ParameterCode>' + @par_code + '</ParameterCode>'
										set @output = @output + '<MonitoringSiteDescriptionCode>' + replace(@stage_code,'&','&amp;') + '</MonitoringSiteDescriptionCode>'
										set @output = @output + '<LimitSeasonNumber>' + @LimitSeasonNumber + '</LimitSeasonNumber>'
									if @load_1 is not null or @load_2 is not null or @conc_1 is not null or @conc_2 is not null or @conc_3 is not null
									begin
										set @output = @output + '<ReportSampleTypeText>' + @sample_type + '</ReportSampleTypeText>'
										set @output = @output + '<ReportingFrequencyCode>' + @sample_freq + '</ReportingFrequencyCode>'
										set @output = @output + '<ReportNumberOfExcursions>' + convert(varchar(5),@excursion) + '</ReportNumberOfExcursions>'
									end
									if @conc_unit is not null and (@conc_1 is not null or @conc_2 is not null or @conc_3 is not null)
									begin
										set @output = @output + '<ConcentrationNumericReportUnitMeasureCode>' + @conc_unit + '</ConcentrationNumericReportUnitMeasureCode>'
									end
									if @load_unit is not null and (@load_1 is not null or @load_2 is not null)
									begin
										set @output = @output + '<QuantityNumericReportUnitMeasureCode>' + @load_unit + '</QuantityNumericReportUnitMeasureCode>'
									end
									if @load_1 is not null --or @load_1_nodi is not null
									begin
										set @output = @output + '<NumericReport>'
										set @output = @output + '<NumericReportCode>Q1</NumericReportCode>'
										set @output = @output + '<NumericReportReceivedDate>' + convert(VARCHAR(10),@received_dttm,126) + '</NumericReportReceivedDate>'
										if @load_1_nodi is not null
										begin
											set @output = @output + '<NumericReportNoDischargeIndicator>' + @load_1_nodi + '</NumericReportNoDischargeIndicator>'
										end

    									else -- if left(@load_1,1) in ('<','>')
		/*
										begin
											set @output = @output + '<NumericConditionQuantity>' + right(@load_1,len(@load_1) - 1) + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @LOAD_1 LIKE '<%'  then '&lt;'   when @LOAD_1 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end else
		*/
										begin
											set @output = @output + '<NumericConditionQuantity>' +  LTRIM(REPLACE(REPLACE(@load_1,'<',''),'>',''))   + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @LOAD_1 LIKE '<%'  then '&lt;'   when @LOAD_1 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end
										set @output = @output + '</NumericReport>'
									end
									if @load_2 is not null --or @load_2_nodi is not null
									begin
										set @output = @output + '<NumericReport>'
											set @output = @output + '<NumericReportCode>Q2</NumericReportCode>'
											set @output = @output + '<NumericReportReceivedDate>' + convert(VARCHAR(10),@received_dttm,126) + '</NumericReportReceivedDate>'
										if @load_2_nodi is not null
										begin
											set @output = @output + '<NumericReportNoDischargeIndicator>' + @load_2_nodi + '</NumericReportNoDischargeIndicator>'
										end
										else -- if left(@load_2,1) in ('<','>')
		/*
										begin
											set @output = @output + '<NumericConditionQuantity>' + right(@load_2,len(@load_2) - 1) + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @LOAD_2 LIKE '<%'  then '&lt;'   when @LOAD_2 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end else
		*/
										begin
											set @output = @output + '<NumericConditionQuantity>' + LTRIM(REPLACE(REPLACE(@load_2,'<',''),'>',''))  + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @LOAD_2 LIKE '<%'  then '&lt;'   when @LOAD_2 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end
										set @output = @output + '</NumericReport>'
									end
									if @conc_1 is not null --or @conc_1_nodi is not null
									begin
										set @output = @output + '<NumericReport>'
											set @output = @output + '<NumericReportCode>C1</NumericReportCode>'
											set @output = @output + '<NumericReportReceivedDate>' + convert(VARCHAR(10),@received_dttm,126) + '</NumericReportReceivedDate>'
										if @conc_1_nodi is not null
										begin
											set @output = @output + '<NumericReportNoDischargeIndicator>' + @conc_1_nodi + '</NumericReportNoDischargeIndicator>'
										end
										else -- if left(@conc_1,1) in ('<','>')
		/*
										begin
											set @output = @output + '<NumericConditionQuantity>' + right(@conc_1,len(@conc_1) - 1) + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @CONC_1 LIKE '<%'  then '&lt;'   when @CONC_1 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end else
		*/
										begin
											set @output = @output + '<NumericConditionQuantity>' + LTRIM(REPLACE(REPLACE(@CONC_1,'<',''),'>','')) + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @CONC_1 LIKE '<%'  then '&lt;'   when @CONC_1 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end
										set @output = @output + '</NumericReport>'
									end
									if @conc_2 is not null --or @conc_2_nodi is not null
									begin
										set @output = @output + '<NumericReport>'
											set @output = @output + '<NumericReportCode>C2</NumericReportCode>'
											set @output = @output + '<NumericReportReceivedDate>' + convert(VARCHAR(10),@received_dttm,126) + '</NumericReportReceivedDate>'
										if @conc_2_nodi is not null
										begin
											set @output = @output + '<NumericReportNoDischargeIndicator>' + @conc_2_nodi + '</NumericReportNoDischargeIndicator>'
										end 
										else --if left(@conc_2,1) in ('<','>')
		/*
										begin
											set @output = @output + '<NumericConditionQuantity>' + right(@conc_2,len(@conc_2) - 1) + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @CONC_2 LIKE '<%'  then '&lt;'   when @CONC_2 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end else
		*/
										begin
											set @output = @output + '<NumericConditionQuantity>' + LTRIM(REPLACE(REPLACE(@CONC_2,'<',''),'>',''))  + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case   when @CONC_2 LIKE '<%'  then '&lt;'   when @CONC_2 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end
										set @output = @output + '</NumericReport>'
									end
									if @conc_3 is not null --or @conc_3_nodi is not null
									begin
										set @output = @output + '<NumericReport>'
											set @output = @output + '<NumericReportCode>C3</NumericReportCode>'
											set @output = @output + '<NumericReportReceivedDate>' + convert(VARCHAR(10),@received_dttm,126) + '</NumericReportReceivedDate>'
										if @conc_3_nodi is not null
										begin
											set @output = @output + '<NumericReportNoDischargeIndicator>' + @conc_3_nodi + '</NumericReportNoDischargeIndicator>'
										end
										else -- if left(@conc_3,1) in ('<','>')
		/*
										begin
											set @output = @output + '<NumericConditionQuantity>' + right(@conc_3,len(@conc_3) - 1) + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case  when @CONC_3 LIKE '<%'  then '&lt;'   when @CONC_3 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end else
		*/
										begin
											set @output = @output + '<NumericConditionQuantity>' + LTRIM(REPLACE(REPLACE(@CONC_3,'<',''),'>',''))  + '</NumericConditionQuantity>'
											set @output = @output + '<NumericConditionQualifier>' + case  when @CONC_3 LIKE '<%'  then '&lt;'   when @CONC_3 LIKE '>%'  then '&gt;'   ELSE '='    END + '</NumericConditionQualifier>'
										end
										set @output = @output + '</NumericReport>'
									end
									set @output = @output + '</ReportParameter>'

								fetch next from results into	@par_code,
															@stage_code,
															@LimitSeasonNumber,
															@sample_type,
															@sample_freq,
															@excursion,
															@conc_unit,
															@load_unit,
															@load_1,
															@load_1_nodi,
															@load_2,
															@load_2_nodi,
															@conc_1,
															@conc_1_nodi,
															@conc_2,
															@conc_2_nodi,
															@conc_3,
															@conc_3_nodi

								set @results_fetch_status = @@fetch_status
							end

							close results
							deallocate results
						end

						set @output = @output + '</DischargeMonitoringReport>'
					set @output = @output + '</DischargeMonitoringReportData>'
		 
					insert into  icis_dmr_report_data(submission_id_int, transaction_id, dmr_data, [dmr_data_xml]  , CREATED_BY , CREATED_DATE)
					values(@submission_id_int, @transaction_id, @output , null ,Suser_Sname(), getdate())

--					values(@submission_id_int, @transaction_id, @output , cast(@output as xml) ,Suser_Sname(), getdate())


					set @output = ''


					fetch next from reports into	@submission_id_int,
													@internal_trans_id,
													@station_id,
													@monitor_pt_id,
													@monitor_pt_version,
													@permit_no,
													@station_no,
													@report_designator,
													@mp_end_date,
													@rpt_sign_date,
													@peofn,
													@peoln,
													@peoti,
													@peote,
													@sfn,
													@sln,
													@certifier_phone,
													@comment,
													@no_discharge_site,
													@received_dttm
					set @reports_fetch_status = @@fetch_status
				end

				close reports
				deallocate reports


			--insert into icis_xml(outresult)  values(CAST(@output AS XML));

			--SELECT CAST(@output AS XML)
			insert into his_icis_dmr (submission_id_int,transaction_id,station_id,monitor_pt_id,monitor_pt_version,permit_no,station_no,report_designator,mp_end_date,rpt_sign_date,PrincipalExecutiveOfficerFirstName,PrincipalExecutiveOfficerLastName,PrincipalExecutiveOfficerTitle,PrincipalExecutiveOfficerTelephone,SignatoryFirstName,SignatoryLastName,certifier_phone,comment,no_discharge_site,received_dttm,par_code,stage_code,LimitSeasonNumber,sample_type,sample_freq,excursion,conc_unit,load_unit,load_1_prefix,load_1,load_1_nodi,load_2_prefix,load_2,load_2_nodi,conc_1_prefix,conc_1,conc_1_nodi,conc_2_prefix,conc_2,conc_2_nodi,conc_3_prefix,conc_3,conc_3_nodi,submit_trans_id,epa_trans_id,CREATED_BY,CREATED_DATE,UPDATED_BY,UPDATED_DATE)
			select 
			 a.submission_id_int, a.transaction_id, a.station_id, a.monitor_pt_id, a.monitor_pt_version, a.permit_no, a.station_no, a.report_designator, a.mp_end_date, a.rpt_sign_date, a.PrincipalExecutiveOfficerFirstName, a.PrincipalExecutiveOfficerLastName, a.PrincipalExecutiveOfficerTitle, a.PrincipalExecutiveOfficerTelephone, a.SignatoryFirstName, a.SignatoryLastName, a.certifier_phone, a.comment, a.no_discharge_site, a.received_dttm
			,  b.par_code, b.stage_code, b.LimitSeasonNumber, b.sample_type, b.sample_freq, b.excursion, b.conc_unit, b.load_unit, b.load_1_prefix, b.load_1, b.load_1_nodi, b.load_2_prefix, b.load_2, b.load_2_nodi, b.conc_1_prefix, b.conc_1, b.conc_1_nodi, b.conc_2_prefix, b.conc_2, b.conc_2_nodi, b.conc_3_prefix, b.conc_3, b.conc_3_nodi, @transaction_id, null, original_login(), getdate(), original_login(), getdate()
			from dbo.icis_dmr_report a 
									   inner join  icis_dmr_numeric_results b
										on a.submission_id_int = b.submission_id_int
											and a.transaction_id = b.transaction_id
											and a.monitor_pt_id = b.monitor_pt_id
											and a.monitor_pt_version = b.monitor_pt_version


			  
            set @error = ''



		end try

       /*******  if detect unformatted xml file, stop procedure ***/

		BEGIN CATCH
--			set @error = cast(ERROR_NUMBER() as varchar) + ', ' +  cast(ERROR_PROCEDURE() as varchar) + ' submission_id: ' + cast(@submission_id_int as varchar) +  '  Error Detected;   ' +   cast(ERROR_MESSAGE() as varchar)  +  '   record as:  ' + @output
--			set @error = '  Error !! error_no: ' --+ cast(ERROR_NUMBER() as varchar) --  + ', on stored procedure: ' +  cast(ERROR_PROCEDURE() as varchar) + ' line no : '  + cast(ERROR_LINE() as varchar)   +  ';  when parsing submission_id: ' + cast(@submission_id_int as varchar) +  '  Error message;   ' +   cast(ERROR_MESSAGE() as varchar)  +  '   record as:  ' + @output


			delete from icis_dmr_report_data
	

			set @error = '  Error !! error_no: ' + cast(ERROR_NUMBER() as varchar) + ', on stored procedure: ' +  cast(ERROR_PROCEDURE() as varchar) + ' line no : '  + cast(ERROR_LINE() as varchar)   +  ';  when parsing submission_id: ' + cast(@submission_id_int as varchar) +  '  Error message;   ' +   cast(ERROR_MESSAGE() as varchar)  +  '   record as:  ' + @output

		END CATCH

	end
end
GO
/****** Object:  View [dbo].[v_ICIS_DMR_REQUIRE_SUBMIT_FROM_NODE]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_ICIS_DMR_REQUIRE_SUBMIT_FROM_NODE]
AS 
SELECT DISTINCT 
SUBMISSION_ID_INT, SUBMIT_TRANS_ID, EPA_TRANS_ID, PERMIT_NO , (cast(STATION_NO as varchar(10)) + cast(REPORT_DESIGNATOR as varchar(10))) as Mon_pt_Label , SUBSTRING(CONVERT(VARCHAR, MP_END_DATE , 120),1, 10) AS MP_END_DATE, PAR_CODE, STAGE_CODE, LIMITSEASONNUMBER as SEASON_ID
, CASE SUBMIT_STATUS WHEN 'S' THEN 'Submitted' WHEN 'C' THEN 'Sucess' when 'P' then 'Pending' when 'E' then 'Error' else 'Submit Fail' end  AS [STATUS], CREATED_DATE
FROM his_icis_dmr 
WHERE CAST(SUBMISSION_ID_INT AS VARCHAR(100)) + '.' + PERMIT_NO  +'.' + STATION_NO +'.' + PAR_CODE +'.' + CONVERT(VARCHAR, CREATED_DATE, 120)  IN 
(SELECT CAST(SUBMISSION_ID_INT AS VARCHAR(100))+ '.' + PERMIT_NO  +'.' + STATION_NO +'.' + PAR_CODE +'.' + CONVERT(VARCHAR, MAX(CREATED_DATE), 120) 
FROM his_icis_dmr 
GROUP BY SUBMISSION_ID_INT, PERMIT_NO,STATION_NO, PAR_CODE )
--AND LEN(SUBMIT_TRANS_ID) =0
AND EPA_TRANS_ID is  null
GO
/****** Object:  View [dbo].[v_ICIS_DMR_MASTER]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[v_ICIS_DMR_MASTER]
AS 
SELECT DISTINCT 
SUBMISSION_ID_INT, SUBMIT_TRANS_ID, EPA_TRANS_ID, PERMIT_NO , (cast(STATION_NO as varchar(10)) + cast(REPORT_DESIGNATOR as varchar(10))) as Mon_pt_Label , SUBSTRING(CONVERT(VARCHAR, MP_END_DATE , 120),1, 10) AS MP_END_DATE, PAR_CODE, STAGE_CODE, LIMITSEASONNUMBER as SEASON_ID
, CASE SUBMIT_STATUS WHEN 'S' THEN 'Submitted' WHEN 'C' THEN 'Sucess' when 'P' then 'Pending' when 'E' then 'Error' else 'Submit Fail' end  AS [STATUS], CREATED_DATE
FROM his_icis_dmr 
WHERE CAST(SUBMISSION_ID_INT AS VARCHAR(100)) + '.' + PERMIT_NO  +'.' + STATION_NO +'.' + PAR_CODE +'.' + CONVERT(VARCHAR, CREATED_DATE, 120)  IN 
(SELECT CAST(SUBMISSION_ID_INT AS VARCHAR(100))+ '.' + PERMIT_NO  +'.' + STATION_NO +'.' + PAR_CODE +'.' + CONVERT(VARCHAR, MAX(CREATED_DATE), 120) 
FROM his_icis_dmr 
GROUP BY SUBMISSION_ID_INT, PERMIT_NO,STATION_NO, PAR_CODE )
AND EPA_TRANS_ID is not null


--WHERE SUBMIT_STATUS = 'S'
--SELECT * FROM his_icis_dmr 
--SELECT * FROM v_ICIS_DMR_MASTER
--DROP VIEW v_ICIS_DMR_MASTER

/*
update a
set epa_trans_id = b.supplied_trans_id
FROM dbo.his_icis_dmr a join [NODE].[Node2008].[dbo].[NODE_OPERATION_MANAGER] b
on a.submit_trans_id = b.trans_id
where a.epa_trans_id is null and len(a.submit_trans_id) !=0 
*/
GO
/****** Object:  View [dbo].[v_ICIS_DMR_REQUIRE_SUBMIT_FROM_NMS]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create VIEW [dbo].[v_ICIS_DMR_REQUIRE_SUBMIT_FROM_NMS]
AS 
SELECT DISTINCT 
SUBMISSION_ID_INT, SUBMIT_TRANS_ID, EPA_TRANS_ID, PERMIT_NO , (cast(STATION_NO as varchar(10)) + cast(REPORT_DESIGNATOR as varchar(10))) as Mon_pt_Label , SUBSTRING(CONVERT(VARCHAR, MP_END_DATE , 120),1, 10) AS MP_END_DATE, PAR_CODE, STAGE_CODE, LIMITSEASONNUMBER as SEASON_ID
, CASE SUBMIT_STATUS WHEN 'S' THEN 'Submitted' WHEN 'C' THEN 'Sucess' when 'P' then 'Pending' when 'E' then 'Error' else 'Submit Fail' end  AS [STATUS], CREATED_DATE
FROM his_icis_dmr 
WHERE CAST(SUBMISSION_ID_INT AS VARCHAR(100)) + '.' + PERMIT_NO  +'.' + STATION_NO +'.' + PAR_CODE +'.' + CONVERT(VARCHAR, CREATED_DATE, 120)  IN 
(SELECT CAST(SUBMISSION_ID_INT AS VARCHAR(100))+ '.' + PERMIT_NO  +'.' + STATION_NO +'.' + PAR_CODE +'.' + CONVERT(VARCHAR, MAX(CREATED_DATE), 120) 
FROM his_icis_dmr 
GROUP BY SUBMISSION_ID_INT, PERMIT_NO,STATION_NO, PAR_CODE )
AND LEN(SUBMIT_TRANS_ID) =0
AND EPA_TRANS_ID is  null
GO

/****** Object:  StoredProcedure [dbo].[icis_dmr_replace2]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[icis_dmr_replace2] (
	@transaction_id varchar(100), 
	@id Varchar(10),
	@Author Varchar(100),
	@Organization  Varchar(100),
	@Title Varchar(100),
	@DataService Varchar(20),
	@ContactInfo  Varchar(300),
	@email Varchar(100),
	@output varchar(max) OUTPUT)
as
begin

	declare @dmr_data varchar(max),
            @reports_fetch_status int ,
			@return_value varchar(max),
			@error varchar(max),
            @output_header varchar(max),
            @output_content varchar(max),
            @output_footer varchar(max)

set @return_value = ''
set @output_header =''
set @output_content=''
set @output_footer =''


/* Generate XML for each submission */

begin
	EXEC	@return_value = [dbo].[icis_dmr_replace_insert2]	@transaction_id  ,  @error output
end

--set @return_value = 2
--select @return_value

--end

if (len(@return_value) > 1)
	begin
		set @output = ''
		select  @return_value
	end
else 
	begin
			begin try 
--				declare reports_all cursor for		

-- select dmr_data from icis_dmr_report_data WHERE  transaction_id='20111108-11'
	--  DMR XML Header
            begin 
				set @output_header = ''
				set @output_header = ISNULL(@output_header,'') + '<Document xmlns="http://www.exchangenetwork.net/schema/icis/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">'
					set @output_header = @output_header + '<Header>'
						set @output_header = @output_header + '<Id>' + @id + '</Id>'
						set @output_header = @output_header + '<Author>' + @Author +'</Author>'
						set @output_header = @output_header + '<Organization>' + @Organization + '</Organization>'
						set @output_header = @output_header + '<Title>'+ @Title+'</Title>'
						set @output_header = @output_header + '<CreationTime>' + convert(VARCHAR(50),current_timestamp,126) + '</CreationTime>'
						set @output_header = @output_header + '<DataService>'+ @DataService+ '</DataService>'
						set @output_header = @output_header + '<ContactInfo>' + @ContactInfo +'</ContactInfo>'
						set @output_header = @output_header + '<Property>'
							set @output_header = @output_header + '<name>e-mail</name>'
							set @output_header = @output_header + '<value>' + @email + '</value>'
						set @output_header = @output_header + '</Property>'
						set @output_header = @output_header + '<Property>'
							set @output_header = @output_header + '<name>Source</name>'
							set @output_header = @output_header + '<value>NetDMR</value>'
						set @output_header = @output_header + '</Property>'
					set @output_header = @output_header + '</Header>'
					set @output_header = @output_header + '<Payload Operation="DischargeMonitoringReportSubmission">'


			   /********  start loop  concat nemeric DMR recordS  *****/
               set @output_content = ''

				update icis_dmr_report_data
				set dmr_data = ISNULL(replace(replace(            
								   substring(dmr_data, 1, len(dmr_data)) 
								   ,'''','&apos;'), '"','&quot;') ,'')
				WHERE  transaction_id=@transaction_id  and len(rtrim(dmr_data)) > 0 and dmr_data is not null 
				--and dmr_data is not null 

				update icis_dmr_report_data
				set dmr_data = REPLACE(REPLACE(REPLACE( dmr_data,'<NumericConditionQuantity>E</NumericConditionQuantity><NumericConditionQualifier>=</NumericConditionQualifier>', '<NumericConditionQuantity>0</NumericConditionQuantity><NumericConditionQualifier>E</NumericConditionQualifier>') ,
																			  '<NumericConditionQuantity>T</NumericConditionQuantity><NumericConditionQualifier>=</NumericConditionQualifier>', '<NumericConditionQuantity>0</NumericConditionQuantity><NumericConditionQualifier>T</NumericConditionQualifier>') ,
																			  '<NumericConditionQuantity>X</NumericConditionQuantity><NumericConditionQualifier>=</NumericConditionQualifier>', '<NumericConditionQuantity>0</NumericConditionQuantity><NumericConditionQualifier>X</NumericConditionQualifier>' )
				WHERE  transaction_id=@transaction_id  and len(rtrim(dmr_data)) > 0 and dmr_data is not null 





/*
					open reports_all

					fetch next from reports_all into @dmr_data

					while  @@FETCH_STATUS = 0
					begin
						--add this line 
						set  @dmr_data = substring(@dmr_data, 1, len(@dmr_data))
                         
			           /**** Replace string having quote *********/
						set @output =( ISNULL(@output,'') + ISNULL(replace(replace(@dmr_data,'''','&apos;'), '"','&quot;') ,''))
           --             select @dmr_data
		   --  			  select @output

						fetch next from reports_all into	@dmr_data
					end

					close reports_all
					deallocate reports_all
*/

   /**** modified 20111117  ***/

         
				select   @output_content = (@output_content + a.dmr_data)
                from
                (
                select  dmr_data 
				from icis_dmr_report_data WHERE  transaction_id= @transaction_id  and len(rtrim(dmr_data)) > 0 and dmr_data is not null 
                ) a 
           

    -- DMR XML FOOTER
				set @output_footer = '</Payload></Document>'
            end
                if (len(@output_content) >0)
					set @output =   (@output_header + @output_content + @output_footer)
				else 
                    set @output = ''      

  select cast(@output as xml)



/* 20111107 */
				delete from dbo.icis_dmr_report where submission_id_int in (select submission_id_int from dbo.icis_dmr_report_data where transaction_id = @transaction_id) 
				delete from dbo.icis_dmr_numeric_results where submission_id_int in (select submission_id_int from dbo.icis_dmr_report_data where transaction_id = @transaction_id) 
			    delete from dbo.icis_dmr_report_data where transaction_id = @transaction_id

			end try 
			begin catch
				 set @output = ''
                 select  'Stored procedure: ' +  cast(ERROR_PROCEDURE() as varchar) + ' can not complete !!'

    		end catch


	end
end

GO