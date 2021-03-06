USE [ICIS_STG_S]
GO
/****** Object:  StoredProcedure [dbo].[ICIS_ComplianceMonitoring_replace_call_sp]    Script Date: 05/02/2012 18:09:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_ComplianceMonitoring_replace_call_sp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ICIS_ComplianceMonitoring_replace_call_sp]
GO
/****** Object:  StoredProcedure [dbo].[ICIS_ComplianceMonitoring_replace_State]    Script Date: 05/02/2012 18:09:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_ComplianceMonitoring_replace_State]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ICIS_ComplianceMonitoring_replace_State]
GO
/****** Object:  StoredProcedure [dbo].[ICIS_ComplianceMonitoring_replace_Federal]    Script Date: 05/02/2012 18:09:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_ComplianceMonitoring_replace_Federal]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ICIS_ComplianceMonitoring_replace_Federal]
GO
/****** Object:  View [dbo].[v_xml_ComplianceMonitoring_Federal]    Script Date: 05/02/2012 18:09:34 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_xml_ComplianceMonitoring_Federal]'))
DROP VIEW [dbo].[v_xml_ComplianceMonitoring_Federal]
GO
/****** Object:  View [dbo].[v_xml_ComplianceMonitoring_State]    Script Date: 05/02/2012 18:09:34 ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_xml_ComplianceMonitoring_State]'))
DROP VIEW [dbo].[v_xml_ComplianceMonitoring_State]
GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_header]    Script Date: 05/02/2012 18:09:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_Permit_replace_header]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[ICIS_Permit_replace_header]
GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_header]    Script Date: 05/02/2012 18:09:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_Permit_replace_header]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ICIS_Permit_replace_header]
(  @output varchar(max) output)
as
/*******************************

select * from v_xml_MasterGeneralPermit where activity_id = ''1200039323''
select * from v_xml_BasicPermit where activity_id = ''1200039323''

execute [ICIS_Permit_replace]  ''R''

get submit operation from dbo.ref_ICIS_submit_operation
select * from dbo.icis_activity   (Activity_Name, Activity_Type_code,  Activity_Status_Code)

***************************************/


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
--    declare @output varchar(max) ;
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
declare     @NAAS_ID varchar(100),     @Author varchar(100),     @Organization varchar(100),     @ContactInfo varchar(100), 	@Email_Address  varchar(100)

set @NAAS_ID = ''SSH''
set @Author = ''Enfotech''
set @Organization = ''Enfotech''
set @ContactInfo = ''1388 How Lane, North Brunswick, NJ08902''
set @Email_Address = ''ben_chang@enfotech.com''



				set @output = ''''
				set @output = ISNULL(@output,'''') + ''<Document xmlns="http://www.exchangenetwork.net/schema/icis/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">''
					set @output = @output + ''<Header>''
						set @output = @output + ''<Id>'' + isnull(@NAAS_ID,'''') + ''</Id>''
						set @output = @output + ''<Author>'' + isnull(@Author,'''') + ''</Author>''
						set @output = @output + ''<Organization>'' + isnull(@Organization,'''') + ''</Organization>''
--						set @output = @output + ''<Title>Basic Permit Submission</Title>''
--						set @output = @output + ''<CreationTime>'' + convert(VARCHAR(50),current_timestamp,126) + ''</CreationTime>''
						set @output = @output + ''<CreationTime>'' + convert(varchar(20), getdate(), 126)  + ''0Z'' + ''</CreationTime>''
						set @output = @output + ''<DataService>ICIS-NPDES</DataService>''
						set @output = @output + ''<ContactInfo>'' + isnull(@ContactInfo,'''') + ''</ContactInfo>''
						set @output = @output + ''<Property>''
							set @output = @output + ''<name>e-mail</name>''
							set @output = @output + ''<value>'' + isnull(@Email_Address,'''') + ''</value>''
						set @output = @output + ''</Property>''
						set @output = @output + ''<Property>''
							set @output = @output + ''<name>Source</name>''
							set @output = @output + ''<value>FullBatch</value>''
						set @output = @output + ''</Property>''
					set @output = @output + ''</Header>''


/*

				set @output = ''''
				set @output = ISNULL(@output,'''') + ''<Document xmlns="http://www.exchangenetwork.net/schema/icis/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">''
					set @output = @output + ''<Header>''
						set @output = @output + ''<Id>SSH</Id>''
						set @output = @output + ''<Author>Jeffery Jones</Author>''
						set @output = @output + ''<Organization>MI Department of Environmental Quality</Organization>''
						set @output = @output + ''<Title>Discharge Monitoring Report Submission</Title>''
						set @output = @output + ''<CreationTime>'' + convert(varchar(20), getdate(), 126)  + ''0Z'' + ''</CreationTime>''
						set @output = @output + ''<DataService>ICIS</DataService>''
						set @output = @output + ''<ContactInfo>Constitution Hall, 525 W. Allegan, 2nd Floor, Lansing, MI 48933, (517) 335-4125, JONESJ24@michigan.gov</ContactInfo>''
						set @output = @output + ''<Property>''
							set @output = @output + ''<name>e-mail</name>''
							set @output = @output + ''<value>BEN_CHANG@ENFOTECH.COM</value>''
						set @output = @output + ''</Property>''
						set @output = @output + ''<Property>''
							set @output = @output + ''<name>Source</name>''
							set @output = @output + ''<value>FullBatch</value>''
						set @output = @output + ''</Property>''
					set @output = @output + ''</Header>''
  --     set @output_header =  @output      
*/      
end


' 
END
GO
/****** Object:  View [dbo].[v_xml_ComplianceMonitoring_State]    Script Date: 05/02/2012 18:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_xml_ComplianceMonitoring_State]'))
EXEC dbo.sp_executesql @statement = N'CREATE view [dbo].[v_xml_ComplianceMonitoring_State] as
select distinct
b.*
	,(


		select 
		 v_ComplianceMonitoring_State.PermitIdentifier
		,v_ComplianceMonitoring_State.ComplianceMonitoringCategoryCode  
		,v_ComplianceMonitoring_State.ComplianceMonitoringDate
		,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate
--		,v_ComplianceMonitoring_State.ComplianceInspectionTypeCode
        ,(select isnull(comp_monitor_type_code, ''CEI'') as ComplianceInspectionTypeCode from  xref_comp_monitor_comp_m_type where XREF_COMP_MONITOR_COMP_M_TYPE.ACTIVITY_ID = v_ComplianceMonitoring_State.ACTIVITY_ID for xml path(''''), type) 
		,v_ComplianceMonitoring_State.ComplianceMonitoringActivityName
		,v_ComplianceMonitoring_State.BiomonitoringInspectionMethod
--		,v_ComplianceMonitoring_State.ComplianceMonitoringActionReasonCode
--,(select top 1 activity_purpose_code from ICIS.REF_ACTIVITY_PURPOSE where activity_type_code = isnull(xref_comp_monitor_comp_m_type.comp_monitor_type_code , ''CEI'')) as ComplianceMonitoringActionReasonCode
        ,(select top 1 activity_purpose_code as ComplianceMonitoringActionReasonCode from ICIS.REF_ACTIVITY_PURPOSE inner join (select isnull(comp_monitor_type_code, ''CEI'') comp_monitor_type_code from  xref_comp_monitor_comp_m_type where XREF_COMP_MONITOR_COMP_M_TYPE.ACTIVITY_ID = v_ComplianceMonitoring_State.ACTIVITY_ID) a on  REF_ACTIVITY_PURPOSE.activity_type_code = a.comp_monitor_type_code for xml path(''''),type)
--		,v_ComplianceMonitoring_State.ComplianceMonitoringAgencyTypeCode
--Repeat--------, xref_activity_agency_type.agency_type_code  as ComplianceMonitoringAgencyTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
        , (select agency_type_code as ComplianceMonitoringAgencyTypeCode from xref_activity_agency_type where v_ComplianceMonitoring_State.ACTIVITY_ID = XREF_ACTIVITY_AGENCY_TYPE.ACTIVITY_ID for xml path(''''),type)
		,v_ComplianceMonitoring_State.ComplianceMonitoringAgencyCode
--		,v_ComplianceMonitoring_State.ProgramCode
--Repeat--------, xref_activity_program.program_code  as ProgramCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
        , (select program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID for xml path(''''),type)
		,v_ComplianceMonitoring_State.StateStatuteViolatedName
		,v_ComplianceMonitoring_State.EPAAssistanceIndicator
		,v_ComplianceMonitoring_State.StateFederalJointIndicator
		,v_ComplianceMonitoring_State.JointInspectionReasonCode
		,v_ComplianceMonitoring_State.LeadParty
		,v_ComplianceMonitoring_State.NumberDaysPhysicallyConductingActivity
		,v_ComplianceMonitoring_State.NumberHoursPhysicallyConductingActivity
		,v_ComplianceMonitoring_State.ComplianceMonitoringActionOutcomeCode
		,v_ComplianceMonitoring_State.InspectionRatingCode
		--,NationalPrioritiesCode
		--,MultimediaIndicator
		--,FederalFacilityIndicator
		--,FederalFacilityIndicatorComment
		,v_ComplianceMonitoring_State.InspectionUserDefinedField1
		,v_ComplianceMonitoring_State.InspectionUserDefinedField2
		,v_ComplianceMonitoring_State.InspectionUserDefinedField3
		,v_ComplianceMonitoring_State.InspectionUserDefinedField4
		,v_ComplianceMonitoring_State.InspectionUserDefinedField5
		,v_ComplianceMonitoring_State.InspectionUserDefinedField6
		,v_ComplianceMonitoring_State.InspectionCommentText
				--, '''' as PermitContact
							,	(select 
								 xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_contact.affiliation_type_code,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr
		/*
								, case when len(isnull(icis_contact.First_Name ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.First_Name, 1,charindex('' '',icis_contact.First_Name)-1)  ,''Unknown''), 1, 30) end as FirstName 
								, case when len(isnull(icis_contact.Middle_Name ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.Middle_Name, 1,charindex('' '',icis_contact.Middle_Name)-1)  ,''Unknown''), 1, 20) end  as MiddleName 
								, case when len(isnull(icis_contact.Last_Name ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.Last_Name, 1,charindex('' '',icis_contact.Last_Name)-1)  ,''Unknown''), 1, 30) end  as LastName 
								, case when len(isnull(icis_contact.title ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.title, 1,charindex('' '',icis_contact.Title)-1)  ,''Unknown''), 1, 30) end  as IndividualTitleText 
		*/

								, substring(isnull(icis_contact.first_name ,''Unknown''), 1, 30) as  FirstName
								, substring(isnull(icis_contact.middle_name ,''Unknown''), 1, 20) as  MiddleName
								, substring(isnull(icis_contact.last_name ,''Unknown''), 1, 30) as LastName
								, substring(isnull(icis_contact.title ,''Unknown''), 1, 30) as IndividualTitleText 


								,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
								,  icis_contact.state_code as  StateCode
								,  icis_contact.region_code as  RegionCode
								,  null as Telephone
		/*
										,(select case when len(replace(replace(icis_phone.telephone_nmbr,''-'',''''),'' '','''')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
												case when len(replace(replace(icis_phone.telephone_nmbr,''-'',''''),'' '','''')) =0  then null else replace(replace(icis_phone.telephone_nmbr,''-'',''''),'' '','''') end as TelephoneNumber,
												case when len(replace(replace(icis_phone.telephone_extension_nmbr,''-'',''''),'' '','''')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,''-'',''''),'' '','''') end as TelephoneExtensionNumber
											from icis_phone inner join icis_contact_phone on icis_phone.phone_id = icis_contact_phone.phone_id
											where icis_contact_phone.contact_id = xref_activity_contact.contact_id
											for xml path(''Telephone''),type) 
		*/

		--						,  (SELECT electronic_address_text FROM  icis_contact_electronic_addr where contact_id = xref_activity_contact.contact_id ) as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
								,  convert(varchar(10), cast(xref_activity_contact.begin_date  as datetime) , 126) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
								,  convert(varchar(10), cast(xref_activity_contact.end_date  as datetime) , 126) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
								from xref_activity_contact left join icis_contact on icis_contact.contact_id = xref_activity_contact.contact_id
								where xref_activity_contact.activity_id = v_ComplianceMonitoring_State.activity_id  --''1200007474''
								for xml path(''Contact''), type) as InspectionContact 
               --, ComponentInspection

--                , case when  (select top 1 program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASPCC'', 1,7) + ''%'') in (''CWASPCC'') then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as SPCCInspection
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASTMC'', 1,7) + ''%'' ) in (''CWASTMC'') then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as StormWaterConstructionNonConstructionInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAFRP'', 1,7) + ''%'' ) in (''CWAFRP'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as FRPInspection
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASTMM'', 1,7) + ''%'' ) in (''CWASTMM'')  then  substring(convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120),1,4) else null end as ''StormWaterMS4Inspection/MS4AnnualExpenditureYear''
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASTMN'', 1,7) + ''%'' ) in (''CWASTMN'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as ''StormWaterConstructionNonConstructionInspection/StormWaterUnpermittedConstructionInspection/EstimatedStartDate''
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAOPA'', 1,7) + ''%'' ) in (''CWAOPA'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as OPAInspection
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAPRTRT'', 1,7) + ''%'' ) in (''CWAPRTRT'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as ''PretreatmentInspection/SUODate''
 
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASSO'', 1,7) + ''%'' ) in (''CWASSO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as ''SSOInspection/SSOEventDate''
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWACSO'', 1,7) + ''%'' ) in (''CWACSO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as ''CSOInspection/CSOEventDate''
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWACAFO'', 1,7) + ''%'' ) in (''CWACAFO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as ''CAFOInspection/CAFODesignationDate''
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAVOACO'', 1,7) + ''%'' ) in (''CWAVOACO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as VOACOInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAPAPC'', 1,7) + ''%'' ) in (''CWAPAPC'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as PAPCInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAOTHR'', 1,7) + ''%'' ) in (''CWAOTHR'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as OTHRInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASPILL'', 1,7) + ''%'' ) in (''CWASPILL'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as SPILLInspection

--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAOUPD'', 1,7) + ''%'') in (''CWAOUPD'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as OUPDInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_State.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAWTL'', 1,7) + ''%'' ) in (''CWAWTL'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_State.ComplianceMonitoringStartDate) , 120) else null end as WTLInspection

		from 
		(select distinct 
			icis_comp_monitor.activity_id
			,isnull((select max(EXTERNAL_PERMIT_NMBR) from icis_Permit where activity_id =icis_comp_monitor.activity_id),
					isnull((select max(pgm_sys_id) from icis_facility_interest a inner join dbo.XREF_ACTIVITY_FACILITY_INT b on a.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID where b.activity_id = icis_activity.activity_id )
							,case when icis_activity.activity_name  like ''%(Permit %'' then substring(substring(icis_activity.activity_name , charindex(''(Permit '',icis_activity.activity_name) + len(''(Permit '') + 1, 15), 1, 9)
			else ''Unknown'' end)) as PermitIdentifier
			-- else ''Unknown'' + cast( row_number() over (order by icis_comp_monitor.activity_id asc) as varchar(2)) end) as PermitIdentifier
			--,(select max(EXTERNAL_PERMIT_NMBR) from icis_Activity where activity_id =icis_comp_monitor.activity_id) as permit_no
			,isnull(ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE,''COM'') as ComplianceMonitoringCategoryCode  -- select * from ICIS.REF_COMP_MONITOR_CATEGORY  (COM, IND, ALT)
			, convert(varchar(10), icis_activity.ACTUAL_END_DATE , 120) as ComplianceMonitoringDate
			, convert(varchar(10),isnull(icis_activity.actual_begin_date, icis_activity.ACTUAL_END_DATE - isnull(Floor(ICIS_COMP_MONITOR.TOTAL_HOURS/24),0)), 120)  as ComplianceMonitoringStartDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--, icis_activity.ACTUAL_END_DATE as ComplianceMonitoringDate
			--, isnull(icis_activity.actual_begin_date, icis_activity.ACTUAL_END_DATE - isnull(Floor(ICIS_COMP_MONITOR.TOTAL_HOURS/24),0))  as ComplianceMonitoringStartDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat--------, isnull(xref_comp_monitor_comp_m_type.comp_monitor_type_code , ''CEI'') as ComplianceInspectionTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_activity.activity_name  as ComplianceMonitoringActivityName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.biomonitoring_method_code  as BiomonitoringInspectionMethod        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--, icis_activity_purpose.activity_purpose_code  as ComplianceMonitoringActionReasonCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			--,(select top 1 activity_purpose_code from ICIS.REF_ACTIVITY_PURPOSE where activity_type_code = isnull(xref_comp_monitor_comp_m_type.comp_monitor_type_code , ''CEI'')) as ComplianceMonitoringActionReasonCode
			--Repeat--------,  ''ADT''  as ComplianceMonitoringActionReasonCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat--------, xref_activity_agency_type.agency_type_code  as ComplianceMonitoringAgencyTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.agency_code  as ComplianceMonitoringAgencyCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat--------, xref_activity_program.program_code  as ProgramCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.state_statute_text  as StateStatuteViolatedName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_activity.epa_assist_flag  as EPAAssistanceIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.joint_inspection_flag  as StateFederalJointIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.joint_inspection_purpose_code  as JointInspectionReasonCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.joint_lead_flag  as LeadParty        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.nmbr_of_day  as NumberDaysPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, cast(ceiling(icis_comp_monitor.total_hours) as numeric(15,0))  as NumberHoursPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.activity_outcome_code  as ComplianceMonitoringActionOutcomeCode        --- Required "Yes"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.insp_rating_code  as InspectionRatingCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat--------, null as NationalPrioritiesCode
			, null as MultimediaIndicator
			, icis_regional_data.string1  as InspectionUserDefinedField1        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.string2  as InspectionUserDefinedField2        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.string3  as InspectionUserDefinedField3        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.date1  as InspectionUserDefinedField4        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.date2  as InspectionUserDefinedField5        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.string4  as InspectionUserDefinedField6        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.comp_monitor_text  as InspectionCommentText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring

			--, xref_comp_mon_cafo_viol_type.cafo_violation_type_code  as CAFOInspectionViolationTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : CAFOInspection
			--, ICIS_CONTACT.state_code  as StateCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : Contact
			--, icis_comp_monitor_sw_ind.swppp_eval_basis_code  as SWPPPEvaluationBasisCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
			--, icis_comp_monitor_sw_ind.swppp_evaluation_date  as SWPPPEvaluationDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
			--, icis_comp_monitor_sw_ind.swppp_evaluation_text  as SWPPPEvaluationDescriptionText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
			FROM    ICIS_COMP_MONITOR      inner JOIN
								  XREF_COMP_MONITOR_COMP_M_TYPE ON XREF_COMP_MONITOR_COMP_M_TYPE.ACTIVITY_ID = ICIS_COMP_MONITOR.ACTIVITY_ID LEFT OUTER JOIN
								  ICIS_ACTIVITY ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_ACTIVITY.ACTIVITY_ID LEFT OUTER JOIN
								  XREF_ACTIVITY_PROGRAM ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_PROGRAM.ACTIVITY_ID LEFT OUTER JOIN
								  XREF_ACTIVITY_AGENCY_TYPE ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_AGENCY_TYPE.ACTIVITY_ID LEFT OUTER JOIN
								  ICIS_REGIONAL_DATA ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_REGIONAL_DATA.ACTIVITY_ID 
			/*
								  CROSS JOIN
								  icis_comp_monitor_sw_ind CROSS JOIN
								  xref_comp_mon_cafo_viol_type CROSS JOIN
								  ICIS_CONTACT CROSS JOIN
								  icis_activity_purpose
			*/
			where xref_activity_agency_type.agency_type_code = ''STA''
			and ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE = ''COM'') v_ComplianceMonitoring_State
        where v_ComplianceMonitoring_State.activity_id = b.activity_id 
		for xml path(''ComplianceMonitoring'')
	)   as instance_xml
from 
(select distinct 
icis_comp_monitor.activity_id
,isnull((select max(EXTERNAL_PERMIT_NMBR) from icis_Permit where activity_id =icis_comp_monitor.activity_id),
		isnull((select max(pgm_sys_id) from icis_facility_interest a inner join dbo.XREF_ACTIVITY_FACILITY_INT b on a.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID where b.activity_id = icis_activity.activity_id )
				,case when icis_activity.activity_name  like ''%(Permit %'' then substring(substring(icis_activity.activity_name , charindex(''(Permit '',icis_activity.activity_name) + len(''(Permit '') + 1, 15), 1, 9)
else ''Unknown'' end)) as PermitIdentifier
-- else ''Unknown'' + cast( row_number() over (order by icis_comp_monitor.activity_id asc) as varchar(2)) end) as PermitIdentifier
--,(select max(EXTERNAL_PERMIT_NMBR) from icis_Activity where activity_id =icis_comp_monitor.activity_id) as permit_no
,isnull(ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE,''COM'') as ComplianceMonitoringCategoryCode  -- select * from ICIS.REF_COMP_MONITOR_CATEGORY  (COM, IND, ALT)
, convert(varchar(10), icis_activity.ACTUAL_END_DATE , 120) as ComplianceMonitoringDate
, convert(varchar(10),isnull(icis_activity.actual_begin_date, icis_activity.ACTUAL_END_DATE - isnull(Floor(ICIS_COMP_MONITOR.TOTAL_HOURS/24),0)), 120)  as ComplianceMonitoringStartDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--, icis_activity.ACTUAL_END_DATE as ComplianceMonitoringDate
--, isnull(icis_activity.actual_begin_date, icis_activity.ACTUAL_END_DATE - isnull(Floor(ICIS_COMP_MONITOR.TOTAL_HOURS/24),0))  as ComplianceMonitoringStartDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat--------, isnull(xref_comp_monitor_comp_m_type.comp_monitor_type_code , ''CEI'') as ComplianceInspectionTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_activity.activity_name  as ComplianceMonitoringActivityName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.biomonitoring_method_code  as BiomonitoringInspectionMethod        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--, icis_activity_purpose.activity_purpose_code  as ComplianceMonitoringActionReasonCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
--,(select top 1 activity_purpose_code from ICIS.REF_ACTIVITY_PURPOSE where activity_type_code = isnull(xref_comp_monitor_comp_m_type.comp_monitor_type_code , ''CEI'')) as ComplianceMonitoringActionReasonCode
--Repeat--------,  ''ADT''  as ComplianceMonitoringActionReasonCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat--------, xref_activity_agency_type.agency_type_code  as ComplianceMonitoringAgencyTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.agency_code  as ComplianceMonitoringAgencyCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat--------, xref_activity_program.program_code  as ProgramCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.state_statute_text  as StateStatuteViolatedName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_activity.epa_assist_flag  as EPAAssistanceIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.joint_inspection_flag  as StateFederalJointIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.joint_inspection_purpose_code  as JointInspectionReasonCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.joint_lead_flag  as LeadParty        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.nmbr_of_day  as NumberDaysPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, cast(ceiling(icis_comp_monitor.total_hours) as numeric(15,0))  as NumberHoursPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.activity_outcome_code  as ComplianceMonitoringActionOutcomeCode        --- Required "Yes"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.insp_rating_code  as InspectionRatingCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat--------, null as NationalPrioritiesCode
, null as MultimediaIndicator
, icis_regional_data.string1  as InspectionUserDefinedField1        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.string2  as InspectionUserDefinedField2        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.string3  as InspectionUserDefinedField3        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.date1  as InspectionUserDefinedField4        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.date2  as InspectionUserDefinedField5        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.string4  as InspectionUserDefinedField6        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.comp_monitor_text  as InspectionCommentText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring

--, xref_comp_mon_cafo_viol_type.cafo_violation_type_code  as CAFOInspectionViolationTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : CAFOInspection
--, ICIS_CONTACT.state_code  as StateCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : Contact
--, icis_comp_monitor_sw_ind.swppp_eval_basis_code  as SWPPPEvaluationBasisCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
--, icis_comp_monitor_sw_ind.swppp_evaluation_date  as SWPPPEvaluationDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
--, icis_comp_monitor_sw_ind.swppp_evaluation_text  as SWPPPEvaluationDescriptionText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
FROM    ICIS_COMP_MONITOR      inner JOIN
                      XREF_COMP_MONITOR_COMP_M_TYPE ON XREF_COMP_MONITOR_COMP_M_TYPE.ACTIVITY_ID = ICIS_COMP_MONITOR.ACTIVITY_ID LEFT OUTER JOIN
                      ICIS_ACTIVITY ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_ACTIVITY.ACTIVITY_ID LEFT OUTER JOIN
                      XREF_ACTIVITY_PROGRAM ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_PROGRAM.ACTIVITY_ID LEFT OUTER JOIN
                      XREF_ACTIVITY_AGENCY_TYPE ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_AGENCY_TYPE.ACTIVITY_ID LEFT OUTER JOIN
                      ICIS_REGIONAL_DATA ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_REGIONAL_DATA.ACTIVITY_ID 
/*
                      CROSS JOIN
                      icis_comp_monitor_sw_ind CROSS JOIN
                      xref_comp_mon_cafo_viol_type CROSS JOIN
                      ICIS_CONTACT CROSS JOIN
                      icis_activity_purpose
*/
where xref_activity_agency_type.agency_type_code = ''STA''
and ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE = ''COM'') b


'
GO
/****** Object:  View [dbo].[v_xml_ComplianceMonitoring_Federal]    Script Date: 05/02/2012 18:09:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[v_xml_ComplianceMonitoring_Federal]'))
EXEC dbo.sp_executesql @statement = N'
--select  cast(b.instance_xml as xml), b.* from [dbo].[v_xml_ComplianceMonitoring_Federal] b

CREATE view [dbo].[v_xml_ComplianceMonitoring_Federal] as
select distinct
b.*
	,(
		select 
		 v_ComplianceMonitoring_Federal.ProgramSystemAcronym 
		,v_ComplianceMonitoring_Federal.ProgramSystemIdentifier -- (Permit_NO)
		,v_ComplianceMonitoring_Federal.FederalStatuteCode 
		,v_ComplianceMonitoring_Federal.ComplianceMonitoringActivityTypeCode
---------------------------------------------------------------------------------------------------------
		,v_ComplianceMonitoring_Federal.ComplianceMonitoringCategoryCode  
		,v_ComplianceMonitoring_Federal.ComplianceMonitoringDate
		,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate
		,v_ComplianceMonitoring_Federal.ComplianceInspectionTypeCode
		,v_ComplianceMonitoring_Federal.ComplianceMonitoringActivityName
		,v_ComplianceMonitoring_Federal.BiomonitoringInspectionMethod
		--,ComplianceMonitoringActionReasonCode
		, (select top 1 activity_purpose_code as ComplianceMonitoringActionReasonCode from ICIS.REF_ACTIVITY_PURPOSE where ACTIVITY_TYPE_CODE = ''INS'' and Status_flag = ''A'' for xml path(''''),type)
		--,v_ComplianceMonitoring_Federal.ComplianceMonitoringAgencyTypeCode
        , (select agency_type_code as ComplianceMonitoringAgencyTypeCode from xref_activity_agency_type where v_ComplianceMonitoring_Federal.ACTIVITY_ID = XREF_ACTIVITY_AGENCY_TYPE.ACTIVITY_ID for xml path(''''),type)
		,v_ComplianceMonitoring_Federal.ComplianceMonitoringAgencyCode
		--,v_ComplianceMonitoring_Federal.ProgramCode
        , (select program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID for xml path(''''),type)
		,v_ComplianceMonitoring_Federal.StateStatuteViolatedName
		,v_ComplianceMonitoring_Federal.EPAAssistanceIndicator
		,v_ComplianceMonitoring_Federal.StateFederalJointIndicator
		,v_ComplianceMonitoring_Federal.JointInspectionReasonCode
		,v_ComplianceMonitoring_Federal.LeadParty
		,v_ComplianceMonitoring_Federal.NumberDaysPhysicallyConductingActivity
		,v_ComplianceMonitoring_Federal.NumberHoursPhysicallyConductingActivity
		,v_ComplianceMonitoring_Federal.ComplianceMonitoringActionOutcomeCode
		,v_ComplianceMonitoring_Federal.InspectionRatingCode
		--,NationalPrioritiesCode
		--,MultimediaIndicator
		--,FederalFacilityIndicator
		--,FederalFacilityIndicatorComment
		,v_ComplianceMonitoring_Federal.InspectionUserDefinedField1
		,v_ComplianceMonitoring_Federal.InspectionUserDefinedField2
		,v_ComplianceMonitoring_Federal.InspectionUserDefinedField3
		,v_ComplianceMonitoring_Federal.InspectionUserDefinedField4
		,v_ComplianceMonitoring_Federal.InspectionUserDefinedField5
		,v_ComplianceMonitoring_Federal.InspectionUserDefinedField6
		,v_ComplianceMonitoring_Federal.InspectionCommentText
				--, '''' as InspectionContact
							,	(select 
								 xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_contact.affiliation_type_code,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr
		/*
								, case when len(isnull(icis_contact.First_Name ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.First_Name, 1,charindex('' '',icis_contact.First_Name)-1)  ,''Unknown''), 1, 30) end as FirstName 
								, case when len(isnull(icis_contact.Middle_Name ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.Middle_Name, 1,charindex('' '',icis_contact.Middle_Name)-1)  ,''Unknown''), 1, 20) end  as MiddleName 
								, case when len(isnull(icis_contact.Last_Name ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.Last_Name, 1,charindex('' '',icis_contact.Last_Name)-1)  ,''Unknown''), 1, 30) end  as LastName 
								, case when len(isnull(icis_contact.title ,'''')) =0 then ''Unknown'' else  substring(isnull(substring(icis_contact.title, 1,charindex('' '',icis_contact.Title)-1)  ,''Unknown''), 1, 30) end  as IndividualTitleText 
		*/

								, substring(isnull(icis_contact.first_name ,''Unknown''), 1, 30) as  FirstName
								, substring(isnull(icis_contact.middle_name ,''Unknown''), 1, 20) as  MiddleName
								, substring(isnull(icis_contact.last_name ,''Unknown''), 1, 30) as LastName
								, substring(isnull(icis_contact.title ,''Unknown''), 1, 30) as IndividualTitleText 


								,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
								,  icis_contact.state_code as  StateCode
								,  icis_contact.region_code as  RegionCode
								,  null as Telephone
		/*
										,(select case when len(replace(replace(icis_phone.telephone_nmbr,''-'',''''),'' '','''')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
												case when len(replace(replace(icis_phone.telephone_nmbr,''-'',''''),'' '','''')) =0  then null else replace(replace(icis_phone.telephone_nmbr,''-'',''''),'' '','''') end as TelephoneNumber,
												case when len(replace(replace(icis_phone.telephone_extension_nmbr,''-'',''''),'' '','''')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,''-'',''''),'' '','''') end as TelephoneExtensionNumber
											from icis_phone inner join icis_contact_phone on icis_phone.phone_id = icis_contact_phone.phone_id
											where icis_contact_phone.contact_id = xref_activity_contact.contact_id
											for xml path(''Telephone''),type) 
		*/

		--						,  (SELECT electronic_address_text FROM  icis_contact_electronic_addr where contact_id = xref_activity_contact.contact_id ) as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
								,  convert(varchar(10), cast(xref_activity_contact.begin_date  as datetime) , 126) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
								,  convert(varchar(10), cast(xref_activity_contact.end_date  as datetime) , 126) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
								from xref_activity_contact left join icis_contact on icis_contact.contact_id = xref_activity_contact.contact_id
								where xref_activity_contact.activity_id = v_ComplianceMonitoring_Federal.activity_id  --''1200007474''
								for xml path(''Contact''), type) as InspectionContact 
----------------------------------------------------------------------------------------------------------------------
--		, null as InspectionGovernmentContact
        , (select ''LTC'' as AffiliationTypeText, ''jdoe@acmeindustries.com'' as ElectronicAddressText
           for xml path(''InspectionGovernmentContact''), type)

		/*
							<AffiliationTypeText>LTC</AffiliationTypeText>
							<ElectronicAddressText>jdoe@acmeindustries.com</ElectronicAddressText>
		*/
		, null as ComplianceMonitoringPlannedStartDate
		, null as ComplianceMonitoringPlannedEndDate
		, null as EPARegion
		, null as LawSectionCode
--        , (select ''311''   for xml path(''LawSectionCode''), type)
		, null as ComplianceMonitoringMediaTypeCode
		, null as RegionalPriorityCode
--        , (select ''72''   for xml path(''RegionalPriorityCode''), type)
		, null as SICCode
--        , (select ''311''   for xml path(''SICCode''), type)
		, null as NAICSCode
--        , (select ''311''   for xml path(''NAICSCode''), type)
		, null as InspectionConclusionDataSheet
		/*
							<DeficienciesObservedIndicator>Y</DeficienciesObservedIndicator>
							<DeficiencyObservedCode>FLW</DeficiencyObservedCode>
							<DeficiencyCommunicatedFacilityIndicator>Y</DeficiencyCommunicatedFacilityIndicator>
							<FacilityActionObservedIndicator>Y</FacilityActionObservedIndicator>
							<CorrectiveActionCode>4</CorrectiveActionCode>
							<AirPollutantCode>2739</AirPollutantCode>
							<WaterPollutantCode>2739</WaterPollutantCode>
							<NationalPolicyGeneralAssistanceIndicator>Y</NationalPolicyGeneralAssistanceIndicator>
							<NationalPolicySiteSpecificAssistanceIndicator>Y</NationalPolicySiteSpecificAssistanceIndicator>
		*/
		, null as Subactivity
		/*
							<SubactivityTypeCode>IRSF</SubactivityTypeCode>
							<SubactivityPlannedDate>2005-12-31</SubactivityPlannedDate>
							<SubactivityDate>2005-12-31</SubactivityDate>
		*/
		, null as Citation
		/*
							<CitationTitle>Sample Text</CitationTitle>
							<CitationPart>Sample Text</CitationPart>
							<CitationSection>Sample Text</CitationSection>
		*/

               --, ComponentInspection

--                , case when  (select top 1 program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASPCC'', 1,7) + ''%'') in (''CWASPCC'') then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as SPCCInspection
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASTMC'', 1,7) + ''%'' ) in (''CWASTMC'') then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as StormWaterConstructionNonConstructionInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAFRP'', 1,7) + ''%'' ) in (''CWAFRP'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as FRPInspection
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASTMM'', 1,7) + ''%'' ) in (''CWASTMM'')  then  substring(convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120),1,4) else null end as ''StormWaterMS4Inspection/MS4AnnualExpenditureYear''
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASTMN'', 1,7) + ''%'' ) in (''CWASTMN'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as ''StormWaterConstructionNonConstructionInspection/StormWaterUnpermittedConstructionInspection/EstimatedStartDate''
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAOPA'', 1,7) + ''%'' ) in (''CWAOPA'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as OPAInspection
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAPRTRT'', 1,7) + ''%'' ) in (''CWAPRTRT'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as ''PretreatmentInspection/SUODate''
 
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASSO'', 1,7) + ''%'' ) in (''CWASSO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as ''SSOInspection/SSOEventDate''
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWACSO'', 1,7) + ''%'' ) in (''CWACSO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as ''CSOInspection/CSOEventDate''
				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWACAFO'', 1,7) + ''%'' ) in (''CWACAFO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as ''CAFOInspection/CAFODesignationDate''
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAVOACO'', 1,7) + ''%'' ) in (''CWAVOACO'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as VOACOInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAPAPC'', 1,7) + ''%'' ) in (''CWAPAPC'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as PAPCInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAOTHR'', 1,7) + ''%'' ) in (''CWAOTHR'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as OTHRInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWASPILL'', 1,7) + ''%'' ) in (''CWASPILL'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as SPILLInspection

--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAOUPD'', 1,7) + ''%'') in (''CWAOUPD'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as OUPDInspection
--				 , case when  (select top 1  program_code as ProgramCode from xref_activity_program where v_ComplianceMonitoring_Federal.ACTIVITY_ID = xref_activity_program.ACTIVITY_ID and xref_activity_program.Program_Code like substring(''CWAWTL'', 1,7) + ''%'' ) in (''CWAWTL'')  then  convert(varchar(10), DATEadd(day, -15,v_ComplianceMonitoring_Federal.ComplianceMonitoringStartDate) , 120) else null end as WTLInspection


		from 
		(select 
			icis_comp_monitor.activity_id
			, (select max(PGM_SYS_ACRNM) from icis_facility_interest a inner join dbo.XREF_ACTIVITY_FACILITY_INT b on a.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID where b.activity_id = icis_activity.activity_id ) as ProgramSystemAcronym
			, isnull((select max(pgm_sys_id) from icis_facility_interest a inner join dbo.XREF_ACTIVITY_FACILITY_INT b on a.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID where b.activity_id = icis_activity.activity_id ) ,
					  (select max(EXTERNAL_PERMIT_NMBR) from icis_Permit where activity_id =icis_comp_monitor.activity_id))as ProgramSystemIdentifier 
			, (select max(statute_code) from xref_activity_law_section a where a.activity_id = icis_activity.activity_id ) as FederalStatuteCode
			/*
			,isnull((select max(EXTERNAL_PERMIT_NMBR) from icis_Permit where activity_id =icis_comp_monitor.activity_id),
			case when icis_activity.activity_name  like ''%(Permit %'' then substring(substring(icis_activity.activity_name , charindex(''(Permit '',icis_activity.activity_name) + len(''(Permit '') + 1, 15), 1, 9)
			else ''Unknown'' end) as PermitIdentifier
			-- else ''Unknown'' + cast( row_number() over (order by icis_comp_monitor.activity_id asc) as varchar(2)) end) as PermitIdentifier
			--,(select max(EXTERNAL_PERMIT_NMBR) from icis_Activity where activity_id =icis_comp_monitor.activity_id) as permit_no
			*/
			,icis_activity.activity_type_code as ComplianceMonitoringActivityTypeCode
			,isnull(ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE,''COM'') as ComplianceMonitoringCategoryCode  -- select * from ICIS.REF_COMP_MONITOR_CATEGORY  (COM, IND, ALT)
			, convert(varchar(10), icis_activity.ACTUAL_END_DATE , 120) as ComplianceMonitoringDate
			, convert(varchar(10),isnull(icis_activity.actual_begin_date, icis_activity.ACTUAL_END_DATE - isnull(Floor(ICIS_COMP_MONITOR.TOTAL_HOURS/24),0)), 120)  as ComplianceMonitoringStartDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, xref_comp_monitor_comp_m_type.comp_monitor_type_code  as ComplianceInspectionTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_activity.activity_name  as ComplianceMonitoringActivityName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.biomonitoring_method_code  as BiomonitoringInspectionMethod        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat , icis_activity_purpose.activity_purpose_code  as ComplianceMonitoringActionReasonCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat , xref_activity_agency_type.agency_type_code  as ComplianceMonitoringAgencyTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.agency_code  as ComplianceMonitoringAgencyCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat , xref_activity_program.program_code  as ProgramCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.state_statute_text  as StateStatuteViolatedName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_activity.epa_assist_flag  as EPAAssistanceIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, case when icis_comp_monitor.joint_inspection_flag = ''E'' then ''S'' else  icis_comp_monitor.joint_inspection_flag end  as StateFederalJointIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.joint_inspection_purpose_code  as JointInspectionReasonCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.joint_lead_flag  as LeadParty        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.nmbr_of_day  as NumberDaysPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, cast(icis_comp_monitor.total_hours as numeric(15,0))  as NumberHoursPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.activity_outcome_code  as ComplianceMonitoringActionOutcomeCode        --- Required "Yes"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.insp_rating_code  as InspectionRatingCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			--Repeat , null as NationalPrioritiesCode
			, null as MultimediaIndicator
			, icis_regional_data.string1  as InspectionUserDefinedField1        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.string2  as InspectionUserDefinedField2        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.string3  as InspectionUserDefinedField3        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.date1  as InspectionUserDefinedField4        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.date2  as InspectionUserDefinedField5        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_regional_data.string4  as InspectionUserDefinedField6        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
			, icis_comp_monitor.comp_monitor_text  as InspectionCommentText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring

			--, xref_comp_mon_cafo_viol_type.cafo_violation_type_code  as CAFOInspectionViolationTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : CAFOInspection
			--, ICIS_CONTACT.state_code  as StateCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : Contact
			--, icis_comp_monitor_sw_ind.swppp_eval_basis_code  as SWPPPEvaluationBasisCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
			--, icis_comp_monitor_sw_ind.swppp_evaluation_date  as SWPPPEvaluationDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
			--, icis_comp_monitor_sw_ind.swppp_evaluation_text  as SWPPPEvaluationDescriptionText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection

			-----------------------------------------------------------------------------------------------------------------------
			, null as InspectionGovernmentContact
			/*
								<AffiliationTypeText>LTC</AffiliationTypeText>
								<ElectronicAddressText>jdoe@acmeindustries.com</ElectronicAddressText>
			*/
			, null as ComplianceMonitoringPlannedStartDate
			, null as ComplianceMonitoringPlannedEndDate
			, null as EPARegion
			, null as LawSectionCode
			, null as ComplianceMonitoringMediaTypeCode
			, null as RegionalPriorityCode
			, null as SICCode
			, null as NAICSCode
			, null as InspectionConclusionDataSheet
			/*
								<DeficienciesObservedIndicator>Y</DeficienciesObservedIndicator>
								<DeficiencyObservedCode>FLW</DeficiencyObservedCode>
								<DeficiencyCommunicatedFacilityIndicator>Y</DeficiencyCommunicatedFacilityIndicator>
								<FacilityActionObservedIndicator>Y</FacilityActionObservedIndicator>
								<CorrectiveActionCode>4</CorrectiveActionCode>
								<AirPollutantCode>2739</AirPollutantCode>
								<WaterPollutantCode>2739</WaterPollutantCode>
								<NationalPolicyGeneralAssistanceIndicator>Y</NationalPolicyGeneralAssistanceIndicator>
								<NationalPolicySiteSpecificAssistanceIndicator>Y</NationalPolicySiteSpecificAssistanceIndicator>
			*/
			, null as Subactivity
			/*
								<SubactivityTypeCode>IRSF</SubactivityTypeCode>
								<SubactivityPlannedDate>2005-12-31</SubactivityPlannedDate>
								<SubactivityDate>2005-12-31</SubactivityDate>
			*/
			, null as Citation
			/*
								<CitationTitle>Sample Text</CitationTitle>
								<CitationPart>Sample Text</CitationPart>
								<CitationSection>Sample Text</CitationSection>
			*/


			FROM    ICIS_COMP_MONITOR      LEFT JOIN
								  XREF_COMP_MONITOR_COMP_M_TYPE ON XREF_COMP_MONITOR_COMP_M_TYPE.ACTIVITY_ID = ICIS_COMP_MONITOR.ACTIVITY_ID LEFT OUTER JOIN
								  ICIS_ACTIVITY ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_ACTIVITY.ACTIVITY_ID LEFT OUTER JOIN
								  XREF_ACTIVITY_PROGRAM ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_PROGRAM.ACTIVITY_ID LEFT OUTER JOIN
								  XREF_ACTIVITY_AGENCY_TYPE ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_AGENCY_TYPE.ACTIVITY_ID LEFT OUTER JOIN
								  ICIS_REGIONAL_DATA ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_REGIONAL_DATA.ACTIVITY_ID 
			/*
								  CROSS JOIN
								  icis_comp_monitor_sw_ind CROSS JOIN
								  xref_comp_mon_cafo_viol_type CROSS JOIN
								  ICIS_CONTACT CROSS JOIN
								  icis_activity_purpose
			*/
			where xref_activity_agency_type.agency_type_code = ''EPA''
			and ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE = ''COM''
		) v_ComplianceMonitoring_Federal
        where v_ComplianceMonitoring_Federal.activity_id = b.activity_id
		for xml path(''FederalComplianceMonitoring'')
	)   as instance_xml
from 
(
select 
icis_comp_monitor.activity_id
, (select max(PGM_SYS_ACRNM) from icis_facility_interest a inner join dbo.XREF_ACTIVITY_FACILITY_INT b on a.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID where b.activity_id = icis_activity.activity_id ) as ProgramSystemAcronym
, isnull((select max(pgm_sys_id) from icis_facility_interest a inner join dbo.XREF_ACTIVITY_FACILITY_INT b on a.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID where b.activity_id = icis_activity.activity_id ) ,
          (select max(EXTERNAL_PERMIT_NMBR) from icis_Permit where activity_id =icis_comp_monitor.activity_id))as ProgramSystemIdentifier 
, (select max(statute_code) from xref_activity_law_section a where a.activity_id = icis_activity.activity_id ) as FederalStatuteCode
/*
,isnull((select max(EXTERNAL_PERMIT_NMBR) from icis_Permit where activity_id =icis_comp_monitor.activity_id),
case when icis_activity.activity_name  like ''%(Permit %'' then substring(substring(icis_activity.activity_name , charindex(''(Permit '',icis_activity.activity_name) + len(''(Permit '') + 1, 15), 1, 9)
else ''Unknown'' end) as PermitIdentifier
-- else ''Unknown'' + cast( row_number() over (order by icis_comp_monitor.activity_id asc) as varchar(2)) end) as PermitIdentifier
--,(select max(EXTERNAL_PERMIT_NMBR) from icis_Activity where activity_id =icis_comp_monitor.activity_id) as permit_no
*/
,icis_activity.activity_type_code as ComplianceMonitoringActivityTypeCode
,isnull(ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE,''COM'') as ComplianceMonitoringCategoryCode  -- select * from ICIS.REF_COMP_MONITOR_CATEGORY  (COM, IND, ALT)
, convert(varchar(10), icis_activity.ACTUAL_END_DATE , 120) as ComplianceMonitoringDate
, convert(varchar(10),isnull(icis_activity.actual_begin_date, icis_activity.ACTUAL_END_DATE - isnull(Floor(ICIS_COMP_MONITOR.TOTAL_HOURS/24),0)), 120)  as ComplianceMonitoringStartDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, xref_comp_monitor_comp_m_type.comp_monitor_type_code  as ComplianceInspectionTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_activity.activity_name  as ComplianceMonitoringActivityName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.biomonitoring_method_code  as BiomonitoringInspectionMethod        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat , icis_activity_purpose.activity_purpose_code  as ComplianceMonitoringActionReasonCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat , xref_activity_agency_type.agency_type_code  as ComplianceMonitoringAgencyTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.agency_code  as ComplianceMonitoringAgencyCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat , xref_activity_program.program_code  as ProgramCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.state_statute_text  as StateStatuteViolatedName        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_activity.epa_assist_flag  as EPAAssistanceIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, case when icis_comp_monitor.joint_inspection_flag = ''E'' then ''S'' else  icis_comp_monitor.joint_inspection_flag end  as StateFederalJointIndicator        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.joint_inspection_purpose_code  as JointInspectionReasonCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.joint_lead_flag  as LeadParty        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.nmbr_of_day  as NumberDaysPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, cast(icis_comp_monitor.total_hours as numeric(15,0))  as NumberHoursPhysicallyConductingActivity        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.activity_outcome_code  as ComplianceMonitoringActionOutcomeCode        --- Required "Yes"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.insp_rating_code  as InspectionRatingCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
--Repeat , null as NationalPrioritiesCode
, null as MultimediaIndicator
, icis_regional_data.string1  as InspectionUserDefinedField1        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.string2  as InspectionUserDefinedField2        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.string3  as InspectionUserDefinedField3        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.date1  as InspectionUserDefinedField4        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.date2  as InspectionUserDefinedField5        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_regional_data.string4  as InspectionUserDefinedField6        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring
, icis_comp_monitor.comp_monitor_text  as InspectionCommentText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : ComplianceMonitoring

--, xref_comp_mon_cafo_viol_type.cafo_violation_type_code  as CAFOInspectionViolationTypeCode        --- Required "No"  Repeatable Yes -- Compliance Monitoring Parent : CAFOInspection
--, ICIS_CONTACT.state_code  as StateCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : Contact
--, icis_comp_monitor_sw_ind.swppp_eval_basis_code  as SWPPPEvaluationBasisCode        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
--, icis_comp_monitor_sw_ind.swppp_evaluation_date  as SWPPPEvaluationDate        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection
--, icis_comp_monitor_sw_ind.swppp_evaluation_text  as SWPPPEvaluationDescriptionText        --- Required "No"  Repeatable No -- Compliance Monitoring Parent : SWConstructionIndustrialInspection

-----------------------------------------------------------------------------------------------------------------------
, null as InspectionGovernmentContact
/*
					<AffiliationTypeText>LTC</AffiliationTypeText>
					<ElectronicAddressText>jdoe@acmeindustries.com</ElectronicAddressText>
*/
, null as ComplianceMonitoringPlannedStartDate
, null as ComplianceMonitoringPlannedEndDate
, null as EPARegion
, null as LawSectionCode
, null as ComplianceMonitoringMediaTypeCode
, null as RegionalPriorityCode
, null as SICCode
, null as NAICSCode
, null as InspectionConclusionDataSheet
/*
					<DeficienciesObservedIndicator>Y</DeficienciesObservedIndicator>
					<DeficiencyObservedCode>FLW</DeficiencyObservedCode>
					<DeficiencyCommunicatedFacilityIndicator>Y</DeficiencyCommunicatedFacilityIndicator>
					<FacilityActionObservedIndicator>Y</FacilityActionObservedIndicator>
					<CorrectiveActionCode>4</CorrectiveActionCode>
					<AirPollutantCode>2739</AirPollutantCode>
					<WaterPollutantCode>2739</WaterPollutantCode>
					<NationalPolicyGeneralAssistanceIndicator>Y</NationalPolicyGeneralAssistanceIndicator>
					<NationalPolicySiteSpecificAssistanceIndicator>Y</NationalPolicySiteSpecificAssistanceIndicator>
*/
, null as Subactivity
/*
					<SubactivityTypeCode>IRSF</SubactivityTypeCode>
					<SubactivityPlannedDate>2005-12-31</SubactivityPlannedDate>
					<SubactivityDate>2005-12-31</SubactivityDate>
*/
, null as Citation
/*
					<CitationTitle>Sample Text</CitationTitle>
					<CitationPart>Sample Text</CitationPart>
					<CitationSection>Sample Text</CitationSection>
*/


FROM    ICIS_COMP_MONITOR      LEFT JOIN
                      XREF_COMP_MONITOR_COMP_M_TYPE ON XREF_COMP_MONITOR_COMP_M_TYPE.ACTIVITY_ID = ICIS_COMP_MONITOR.ACTIVITY_ID LEFT OUTER JOIN
                      ICIS_ACTIVITY ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_ACTIVITY.ACTIVITY_ID LEFT OUTER JOIN
                      XREF_ACTIVITY_PROGRAM ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_PROGRAM.ACTIVITY_ID LEFT OUTER JOIN
                      XREF_ACTIVITY_AGENCY_TYPE ON ICIS_COMP_MONITOR.ACTIVITY_ID = XREF_ACTIVITY_AGENCY_TYPE.ACTIVITY_ID LEFT OUTER JOIN
                      ICIS_REGIONAL_DATA ON ICIS_COMP_MONITOR.ACTIVITY_ID = ICIS_REGIONAL_DATA.ACTIVITY_ID 
/*
                      CROSS JOIN
                      icis_comp_monitor_sw_ind CROSS JOIN
                      xref_comp_mon_cafo_viol_type CROSS JOIN
                      ICIS_CONTACT CROSS JOIN
                      icis_activity_purpose
*/
where xref_activity_agency_type.agency_type_code = ''EPA''
and ICIS_COMP_MONITOR.COMP_MONITOR_CATEGORY_CODE = ''COM''
) b
--where b.ProgramSystemIdentifier = ''OK0027049''



'
GO
/****** Object:  StoredProcedure [dbo].[ICIS_ComplianceMonitoring_replace_Federal]    Script Date: 05/02/2012 18:09:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_ComplianceMonitoring_replace_Federal]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ICIS_ComplianceMonitoring_replace_Federal]
( @TransactionType varchar(100) =  null, @Activity_id varchar(100) =null,   @output varchar(max) output)
as


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
    declare @Activity_CD varchar(10)



/********************
v_xml_ComplianceMonitoring
*********************/

set @output = ''''
if exists(select * from v_xml_ComplianceMonitoring_Federal  )
   begin
	if @Activity_id is not null
		begin
			select  * into #tmp_activity from dbo.split (@activity_id, '','')
				if exists(select * from v_xml_ComplianceMonitoring_Federal where activity_id in (select data as activity_id from #tmp_activity) )
					begin
							declare  submit_cur cursor for 
							select Activity_id , ''<FederalComplianceMonitoringData><TransactionHeader><TransactionType>''+ (select  case when  isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') in (''R'',''N'',''C'',''D'',''X'') then isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') else ''R'' end from icis_activity where activity_id = v_xml_ComplianceMonitoring_Federal.activity_id) + ''</TransactionType><TransactionTimestamp>'' + convert(varchar(20), getdate(), 126)  + ''0Z'' +''</TransactionTimestamp></TransactionHeader>'' + cast(instance_xml as varchar(max)) +  ''</FederalComplianceMonitoringData>''  as  instance_xml from dbo.v_xml_ComplianceMonitoring_Federal 
							   where activity_id in (select data as activity_id from #tmp_activity)
	                end
                else 
                    goto case1
		end	    
	else
		begin 
			  declare  submit_cur cursor for 
							select Activity_id , ''<FederalComplianceMonitoringData><TransactionHeader><TransactionType>''+ (select  case when  isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') in (''R'',''N'',''C'',''D'',''X'') then isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') else ''R'' end  from icis_activity where activity_id = v_xml_ComplianceMonitoring_Federal.activity_id) + ''</TransactionType><TransactionTimestamp>'' + convert(varchar(20), getdate(), 126)  + ''0Z'' +''</TransactionTimestamp></TransactionHeader>'' + cast(instance_xml as varchar(max)) +  ''</FederalComplianceMonitoringData>''  as  instance_xml from dbo.v_xml_ComplianceMonitoring_Federal 
                            where instance_xml is not null
		end

       begin 
		            
					set @submit_str = '''' ;
					set @instance_str = '''' ;

						open submit_cur

						  fetch next from submit_cur
							into @Activity_id , @instance_str 


							   WHILE @@FETCH_STATUS = 0
								begin  
								   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'''')) 
								
								  fetch next from submit_cur
									into @Activity_id , @instance_str 
								end
						CLOSE submit_cur;
						DEALLOCATE submit_cur;
      end  

   end
else 
			begin      
					   set @submit_str = ''''
			end


	set @output = (case when  len(@submit_str) >0 then ''<Payload Operation="FederalComplianceMonitoringSubmission">'' + @output + @submit_str  +''</Payload>''
                    else @output + @submit_str end)
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''''
	set @output = @output + @submit_str  
   end


end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ICIS_ComplianceMonitoring_replace_State]    Script Date: 05/02/2012 18:09:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_ComplianceMonitoring_replace_State]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ICIS_ComplianceMonitoring_replace_State]
( @TransactionType varchar(100) =  null, @Activity_id varchar(100) =null,   @output varchar(max) output)
as


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
    declare @Activity_CD varchar(10)



/********************
v_xml_ComplianceMonitoring
*********************/

set @output = ''''
if exists(select * from v_xml_ComplianceMonitoring_State  )
   begin
	if @Activity_id is not null
		begin
			select  * into #tmp_activity from dbo.split (@activity_id, '','')
				if exists(select * from v_xml_ComplianceMonitoring_State where activity_id in (select data as activity_id from #tmp_activity) )
					begin
							declare  submit_cur cursor for 
							select Activity_id , ''<ComplianceMonitoringData><TransactionHeader><TransactionType>''+ (select  case when  isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') in (''R'',''N'',''C'',''D'',''X'') then isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') else ''R'' end   from icis_activity where activity_id = v_xml_ComplianceMonitoring_State.activity_id) + ''</TransactionType><TransactionTimestamp>'' + convert(varchar(20), getdate(), 126)  + ''0Z'' +''</TransactionTimestamp></TransactionHeader>'' + cast(instance_xml as varchar(max)) +  ''</ComplianceMonitoringData>''  as  instance_xml from dbo.v_xml_ComplianceMonitoring_State 
							   where activity_id in (select data as activity_id from #tmp_activity)
	                end
                else 
                    goto case1
		end	    
	else
		begin 
			  declare  submit_cur cursor for 
							select Activity_id , ''<ComplianceMonitoringData><TransactionHeader><TransactionType>''+ (select  case when  isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') in (''R'',''N'',''C'',''D'',''X'') then isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), ''R'') else ''R'' end   from icis_activity where activity_id = v_xml_ComplianceMonitoring_State.activity_id) + ''</TransactionType><TransactionTimestamp>'' + convert(varchar(20), getdate(), 126)  + ''0Z'' +''</TransactionTimestamp></TransactionHeader>'' + cast(instance_xml as varchar(max)) +  ''</ComplianceMonitoringData>''  as  instance_xml from dbo.v_xml_ComplianceMonitoring_State 
                            where instance_xml is not null
		end

       begin 
		            
					set @submit_str = '''' ;
					set @instance_str = '''' ;

						open submit_cur

						  fetch next from submit_cur
							into @Activity_id , @instance_str 


							   WHILE @@FETCH_STATUS = 0
								begin  
								   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'''')) 
								
								  fetch next from submit_cur
									into @Activity_id , @instance_str 
								end
						CLOSE submit_cur;
						DEALLOCATE submit_cur;
      end  

   end
else 
			begin      
					   set @submit_str = ''''
			end


	set @output = (case when  len(@submit_str) >0 then ''<Payload Operation="ComplianceMonitoringSubmission">'' + @output + @submit_str  +''</Payload>''
                    else @output + @submit_str end)
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''''
	set @output = @output + @submit_str  
   end


end
' 
END
GO
/****** Object:  StoredProcedure [dbo].[ICIS_ComplianceMonitoring_replace_call_sp]    Script Date: 05/02/2012 18:09:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ICIS_ComplianceMonitoring_replace_call_sp]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE procedure [dbo].[ICIS_ComplianceMonitoring_replace_call_sp]
(
  @Submit_All_Ind  char(1) = ''N''
, @Activity_id varchar(100) =null
, @FederalComplianceMonitoring_TransType char(1) =null
, @StateComplianceMonitoring_TransType char(1) =null
, @Out varchar(max) output
)
/****************************************************************************************
exec dbo.[ICIS_Permit_replace_call_sp] ''2,3,7,8,10''
default @Submit_All_Ind = ''N''
        only submit the payload with asigned value on the indicate, but could be multiple payload
        This Assigned value will overright the Actyvity_type_cd in ICIS_ACTIVITY.
        if only assign @Activity_id (deliminated by '','') 
            it will collect all payload related to the @Activity_ids  

when @Submit_All_Ind = ''Y'' will collect all payload in the tables, like wildcard

*******************************************************************************************/
as
begin
---  select * from icis_activity
		declare   @output_header varchar(max)
		declare  @TransactionType varchar(100) -- , @Activity_id varchar(100),@Out varchar(max)
		declare @output_FederalComplianceMonitoring varchar(max), @output_StateComplianceMonitoring varchar(max)

		--set @TransactionType = ''R''  
		--set @Activity_id = null

if @SUBMIT_ALL_IND = ''''  set @SUBMIT_ALL_IND =''Y''
if @Activity_id = ''''  set @Activity_id =null
if @FederalComplianceMonitoring_TransType = ''''  set @FederalComplianceMonitoring_TransType =null
if @StateComplianceMonitoring_TransType = ''''  set @StateComplianceMonitoring_TransType =null


if @Submit_All_Ind not in  ( ''N'', ''Y'') select ''@Submit_All_Ind values not in ( ''''Y'''', ''''N'''')''
if @FederalComplianceMonitoring_TransType not in  ( ''R'', ''D'') select ''@FederalComplianceMonitoring_TransType values not in ( ''''N'''', ''''C'''')''
if @StateComplianceMonitoring_TransType not in  ( ''N'', ''R'', ''D'') select ''@StateComplianceMonitoring_TransType values not in ( ''''N'''', ''''C'''')''



       set @output_header =''''
       set @output_FederalComplianceMonitoring =''''
       set @output_StateComplianceMonitoring =''''


if ((@Submit_All_Ind =''N'')  )
	begin
		if (len( isnull(@FederalComplianceMonitoring_TransType,'''') + isnull(@StateComplianceMonitoring_TransType,'''') ) > 0)  
					begin
							exec  [ICIS_Permit_replace_header]   @output_header output

						 if (@FederalComplianceMonitoring_TransType is not null)
							begin
								exec  [ICIS_ComplianceMonitoring_replace_Federal]  @FederalComplianceMonitoring_TransType ,@Activity_id, @output_FederalComplianceMonitoring output
							end


						 if (@StateComplianceMonitoring_TransType is not null)
							begin
								exec  [ICIS_ComplianceMonitoring_replace_State]  @StateComplianceMonitoring_TransType ,@Activity_id, @output_StateComplianceMonitoring output
							end

							if (len(isnull(@output_FederalComplianceMonitoring,'''')) + len(isnull(@output_StateComplianceMonitoring,''''))>0)
							begin
							set @Out = (isnull(rtrim(@output_header),'''') + 
										isnull(rtrim(@output_FederalComplianceMonitoring),'''') + 
										isnull(rtrim(@output_StateComplianceMonitoring),'''')    
										) + ''</Document>''
							end  
					        
							set @Out = isnull(@Out,'''')
							-- select cast(@Out as xml)
				   end 
		else 

              if (@Activity_id is null)
				   begin
					  set @Out = ''''
					  -- select cast(@Out as xml)
				   end 
              else 
						begin
								exec  [ICIS_Permit_replace_header]   @output_header output

								exec  [ICIS_ComplianceMonitoring_replace_Federal]  @FederalComplianceMonitoring_TransType ,@Activity_id, @output_FederalComplianceMonitoring output

								exec  [ICIS_ComplianceMonitoring_replace_State]  @StateComplianceMonitoring_TransType ,@Activity_id, @output_StateComplianceMonitoring output

								if (len(isnull(@output_FederalComplianceMonitoring,'''')) + len(isnull(@output_StateComplianceMonitoring,''''))>0)
								begin
								set @Out = (isnull(rtrim(@output_header),'''') + 
											isnull(rtrim(@output_FederalComplianceMonitoring),'''') + 
											isnull(rtrim(@output_StateComplianceMonitoring ),'''') 
											) + ''</Document>''
								end  
									set @Out = isnull(@Out,'''')
								-- select cast(@Out as xml)
					   end 
    end
					
else if ((@Submit_All_Ind =''Y'') )

		begin
								exec  [ICIS_Permit_replace_header]   @output_header output

								exec  [ICIS_ComplianceMonitoring_replace_Federal]  @FederalComplianceMonitoring_TransType ,@Activity_id, @output_FederalComplianceMonitoring output

								exec  [ICIS_ComplianceMonitoring_replace_State]  @StateComplianceMonitoring_TransType ,@Activity_id, @output_StateComplianceMonitoring output

								if (len(isnull(@output_FederalComplianceMonitoring,'''')) + len(isnull(@output_StateComplianceMonitoring,''''))>0)
								begin
								set @Out = (isnull(rtrim(@output_header),'''') + 
											isnull(rtrim(@output_FederalComplianceMonitoring),'''') + 
											isnull(rtrim(@output_StateComplianceMonitoring ),'''')   
											) + ''</Document>''
								end  
									set @Out = isnull(@Out,'''')
		       --  select cast(@Out as xml)
	   end

end' 
END
GO
