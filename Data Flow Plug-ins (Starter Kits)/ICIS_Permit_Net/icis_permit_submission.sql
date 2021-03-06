/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_header]    Script Date: 04/10/2012 15:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_header]
(  @output varchar(max) output)
as
/*******************************

select * from v_xml_MasterGeneralPermit where activity_id = '1200039323'
select * from v_xml_BasicPermit where activity_id = '1200039323'

execute [ICIS_Permit_replace]  'R'

get submit operation from dbo.ref_ICIS_submit_operation
select * from dbo.icis_activity   (Activity_Name, Activity_Type_code,  Activity_Status_Code)

***************************************/


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
--    declare @output varchar(max) ;
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
declare     @NAAS_ID varchar(100),     @Author varchar(100),     @Organization varchar(100),     @ContactInfo varchar(100), 	@Email_Address  varchar(100)

set @NAAS_ID = 'SSH'
set @Author = 'Enfotech'
set @Organization = 'Enfotech'
set @ContactInfo = '1388 How Lane, North Brunswick, NJ08902'
set @Email_Address = 'ben_chang@enfotech.com'



				set @output = ''
				set @output = ISNULL(@output,'') + '<Document xmlns="http://www.exchangenetwork.net/schema/icis/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">'
					set @output = @output + '<Header>'
						set @output = @output + '<Id>' + isnull(@NAAS_ID,'') + '</Id>'
						set @output = @output + '<Author>' + isnull(@Author,'') + '</Author>'
						set @output = @output + '<Organization>' + isnull(@Organization,'') + '</Organization>'
--						set @output = @output + '<Title>Basic Permit Submission</Title>'
--						set @output = @output + '<CreationTime>' + convert(VARCHAR(50),current_timestamp,126) + '</CreationTime>'
						set @output = @output + '<CreationTime>' + convert(varchar(20), getdate(), 126)  + '0Z' + '</CreationTime>'
						set @output = @output + '<DataService>ICIS-NPDES</DataService>'
						set @output = @output + '<ContactInfo>' + isnull(@ContactInfo,'') + '</ContactInfo>'
						set @output = @output + '<Property>'
							set @output = @output + '<name>e-mail</name>'
							set @output = @output + '<value>' + isnull(@Email_Address,'') + '</value>'
						set @output = @output + '</Property>'
						set @output = @output + '<Property>'
							set @output = @output + '<name>Source</name>'
							set @output = @output + '<value>FullBatch</value>'
						set @output = @output + '</Property>'
					set @output = @output + '</Header>'



end
GO
/****** Object:  View [dbo].[v_xml_MasterGeneralPermit]    Script Date: 04/10/2012 15:03:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop view [v_xml_MasterGeneralPermit1]
CREATE view [dbo].[v_xml_MasterGeneralPermit]
as
select 
b.*
	,(
	select 
--	(select  'R' as TransactionType , convert(varchar(20), getdate(), 126)  + '0Z' as TransactionTimestamp
--	for xml path('TransactionHeader'), type)	
--	, (
     (
		select 
		 PermitIdentifier
		, PermitTypeCode
		, isnull(AgencyTypeCode, 'ST6') as AgencyTypeCode
		, case when PermitStatusCode = 'NON' then 'NON' else null end as PermitStatusCode
		/*
					, PermitIssueDate
					, PermitEffectiveDate
					, PermitExpirationDate

		*/
--/* 20120227  can not modify existing permit effective date 
		, convert(varchar(10), cast(PermitIssueDate as datetime) , 126) as  PermitIssueDate
		, convert(varchar(10), cast(PermitEffectiveDate as datetime), 126) as  PermitEffectiveDate
		, convert(varchar(10), cast(PermitExpirationDate as datetime), 126) as  PermitExpirationDate
--*/
		, ReissuancePriorityPermitIndicator
		, BacklogReasonText
		, PermitIssuingOrganizationTypeName
		--, '' as OtherPermits
				,  ( select other_external_permit_nmbr from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherPermitIdentifier'
				,  ( select organization_name from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherOrganizationName'
				,  ( select identifier_context_desc from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherPermitIdentifierContextName'

		--, '' as AssociatedPermit 
				,  (select  distinct Master_external_permit_nmbr from icis_permit where activity_id = a.activity_id) as 'AssociatedPermit/AssociatedPermitIdentifier' 
				,  (select case when Master_external_permit_nmbr is not null then 'APR' else null end  from icis_permit where activity_id = a.activity_id) as 'AssociatedPermit/AssociatedPermitReasonCode'

		--, '' as SICCodeDetails
				, (select distinct  sic_code as SICCode, 'Y' as SICPrimaryIndicatorCode  from xref_activity_sic where activity_id = a.activity_id AND LEN(SIC_CODE) >0
				   for xml path('SICCodeDetails'), type)

		, PermitAppealedIndicator
		, PermitUserDefinedDataElement1Text
		, PermitUserDefinedDataElement2Text
		, PermitUserDefinedDataElement3Text
		, PermitUserDefinedDataElement4Text
		, PermitUserDefinedDataElement5Text
--		, PermitCommentsText
		, CASE WHEN LEN(RTRIM(PermitCommentsText)) > 0 THEN PermitCommentsText ELSE NULL END AS PermitCommentsText 
		--, '' as PermitContact
					,	(select 
						 xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_contact.affiliation_type_code,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr

						, substring(isnull(icis_contact.first_name ,'Unknown'), 1, 30) as  FirstName
						, substring(isnull(icis_contact.middle_name ,'Unknown'), 1, 20) as  MiddleName
						, substring(isnull(icis_contact.last_name ,'Unknown'), 1, 30) as LastName
						, substring(isnull(icis_contact.title ,'Unknown'), 1, 30) as IndividualTitleText 


						,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
						,  icis_contact.state_code as  StateCode
						,  icis_contact.region_code as  RegionCode
						,  null as Telephone

						,  (SELECT electronic_address_text FROM  icis_contact_electronic_addr where contact_id = xref_activity_contact.contact_id ) as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
						,  convert(varchar(10), cast(xref_activity_contact.begin_date  as datetime) , 126) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
						,  convert(varchar(10), cast(xref_activity_contact.end_date  as datetime) , 126) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
						from xref_activity_contact left join icis_contact on icis_contact.contact_id = xref_activity_contact.contact_id
						where xref_activity_contact.activity_id = v_MasterGeneralPermit.activity_id  --'1200007474'
						for xml path('Contact'), type) as PermitContact 
				, '060'  as GeneralPermitIndustrialCategory  --v_MasterGeneralPermit.GeneralPermitIndustrialCategory
				, v_MasterGeneralPermit.PermitName as PermitName -- 'NPDES Stormwater Runoff MGP'
				, isnull(v_MasterGeneralPermit.COMPONENT_TYPE_CODE,'BIO') as PermitComponentTypeCode
		from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.perm_industrial_cat_code  as GeneralPermitIndustrialCategory        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit."  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
				, icis_permit.permit_name  as PermitName        --- Required "No"  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
                , (select max(COMPONENT_TYPE_CODE) from dbo.XREF_PERM_COMPONENT_TYPE where Activity_id =  icis_permit.Activity_id) as COMPONENT_TYPE_CODE
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='NGP'  ) v_MasterGeneralPermit 
		where v_MasterGeneralPermit.activity_id = a.activity_id
		for xml path('MasterGeneralPermit'), type
	) 
	from (select 
			icis_permit.Activity_id
			,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
			, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
			, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
			, icis_permit.perm_industrial_cat_code  as GeneralPermitIndustrialCategory        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit."  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
			, icis_permit.permit_name  as PermitName        --- Required "No"  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
			from dbo.Icis_permit 
			where Icis_permit.Permit_Type_Code='NGP' ) a
	WHERE a.ACTIVITY_ID = b.activity_id
	for xml path ('')
--	for xml path ('MasterGeneralPermitData')
	)   as instance_xml
	,(
	select 
	(select  'D' as TransactionType , convert(varchar(30), getdate(), 126)  as TransactionTimestamp
	for xml path('TransactionHeader'), type)	
	, (
		select 
		 PermitIdentifier
		from dbo.Icis_permit 
		where Icis_permit.Permit_Type_Code='NGP' 
		and activity_id = a.activity_id
		for xml path('MasterGeneral'), type
	) 
	from (select 
			icis_permit.Activity_id
			,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
			, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
			, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
			, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
			, icis_permit.perm_industrial_cat_code  as GeneralPermitIndustrialCategory        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit."  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
			, icis_permit.permit_name  as PermitName        --- Required "No"  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
			from dbo.Icis_permit 
			where Icis_permit.Permit_Type_Code='NGP'  ) a
	WHERE a.ACTIVITY_ID = b.activity_id
	for xml path ('MasterGeneralPermitData')
	)  as del_instance_xml
from (select 
		icis_permit.Activity_id
		,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
		, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
		, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
		, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
		, icis_permit.perm_industrial_cat_code  as GeneralPermitIndustrialCategory        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit."  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
		, icis_permit.permit_name  as PermitName        --- Required "No"  Repeatable No -- Master General Permit Parent : MasterGeneralPermit
		from dbo.Icis_permit 
		where Icis_permit.Permit_Type_Code='NGP'   ) b
/*** 20120227  ****/
    --                 and activity_id in (select activity_id from dbo.XREF_ACTIVITY_ADDRESS where activity_id =3 )) b
--WHERE a.ACTIVITY_ID IN ('1200039357')
GO
/****** Object:  View [dbo].[v_xml_GeneralPermit]    Script Date: 04/10/2012 15:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop view [v_xml_GeneralPermit1]
CREATE view [dbo].[v_xml_GeneralPermit]
as
select 
b.*
	,(
	select 
--	(select  'R' as TransactionType , convert(varchar(20), getdate(), 126)  + '0Z' as TransactionTimestamp
--	for xml path('TransactionHeader'), type)	
--	, (
     (
		select 
		 PermitIdentifier
		,  (select  distinct Master_external_permit_nmbr from icis_permit where activity_id = a.activity_id) as 'AssociatedGeneralPermitIdentifier' 
		, PermitTypeCode
		, isnull(AgencyTypeCode, 'ST6') as AgencyTypeCode
		, case when PermitStatusCode = 'NON' then 'NON' else null end as PermitStatusCode

--/* 20120227  can not modify existing permit effective date 
		, convert(varchar(10), cast(PermitIssueDate as datetime) , 126) as  PermitIssueDate
		, convert(varchar(10), cast(PermitEffectiveDate as datetime), 126) as  PermitEffectiveDate
		, convert(varchar(10), cast(PermitExpirationDate as datetime), 126) as  PermitExpirationDate
--*/
		, ReissuancePriorityPermitIndicator
		, BacklogReasonText
		, PermitIssuingOrganizationTypeName
		--, '' as OtherPermits
				,  ( select other_external_permit_nmbr from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherPermitIdentifier'
				,  ( select organization_name from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherOrganizationName'
				,  ( select identifier_context_desc from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherPermitIdentifierContextName'
/*
		--, '' as AssociatedPermit 
				,  (select  distinct Master_external_permit_nmbr from icis_permit where activity_id = a.activity_id) as 'AssociatedPermit/AssociatedPermitIdentifier' 
				,  (select case when Master_external_permit_nmbr is not null then 'APR' else null end  from icis_permit where activity_id = a.activity_id) as 'AssociatedPermit/AssociatedPermitReasonCode'
*/
		--, '' as SICCodeDetails
				, (select distinct  sic_code as SICCode, 'Y' as SICPrimaryIndicatorCode  from xref_activity_sic where activity_id = a.activity_id AND LEN(SIC_CODE) >0
				   for xml path('SICCodeDetails'), type)

		, PermitAppealedIndicator
		, PermitUserDefinedDataElement1Text
		, PermitUserDefinedDataElement2Text
		, PermitUserDefinedDataElement3Text
		, PermitUserDefinedDataElement4Text
		, PermitUserDefinedDataElement5Text
--		, PermitCommentsText
		, CASE WHEN LEN(RTRIM(PermitCommentsText)) > 0 THEN PermitCommentsText ELSE NULL END AS PermitCommentsText 
        , MajorMinorRatingCode
--		, case when (select count(*) from XREF_PERM_COMPONENT_TYPE where component_type_code = 'POTW' AND ACTIVITY_ID = A.ACTIVITY_ID) =0 THEN NULL ELSE MajorMinorRatingCode END AS  MajorMinorRatingCode
		, TotalApplicationDesignFlowNumber
		, TotalApplicationAverageFlowNumber
		--, '' as Facility
		,(
					select --top 20
--					(select 'R' as TransactionType, getdate() as TransactionTimestamp for xml path('TransactionHeader'), type),
					(
					select 
--					 v_Basic_Permit.PermitIdentifier,
					 v_Basic_Permit.FacilitySiteName
					, v_Basic_Permit.LocationAddressText
					, v_Basic_Permit.SupplementalLocationText
					, v_Basic_Permit.LocalityName
					, v_Basic_Permit.LocationStateCode
					, v_Basic_Permit.LocationZipCode
					, CASE WHEN LEN(ISNULL(v_Basic_Permit.LocationCountryCode,'')) =0 THEN 'US' ELSE UPPER(LocationCountryCode) END AS LocationCountryCode
					, v_Basic_Permit.OrganizationDUNSNumber
					, v_Basic_Permit.StateFacilityIdentifier
					, v_Basic_Permit.StateRegionCode
					, v_Basic_Permit.FacilityCongressionalDistrictNumber
					, v_Basic_Permit.FacilitySmallBusinessIndicator
					, v_Basic_Permit.FacilityTypeofOwnershipCode
					, v_Basic_Permit.FederalFacilityIdentificationNumber
					, v_Basic_Permit.FederalAgencyCode
					, v_Basic_Permit.FacilityEnvironmentalJusticeCode
					, v_Basic_Permit.TribalLandCode
					, v_Basic_Permit.ConstructionProjectName
					, v_Basic_Permit.ConstructionProjectLatitudeMeasure
					, v_Basic_Permit.ConstructionProjectLongitudeMeasure
			--		, '' as SICCodeDetails
			--		, '' as NAICSCodeDetails
					, (select distinct SIC_Code AS SICCode,
						PRIMARY_INDICATOR_FLAG AS SICPrimaryIndicatorCode
						from dbo.XREF_FACILITY_INTEREST_SIC
						where ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID
						FOR XML PATH('SICCodeDetails'), TYPE) --as SICCodeDetails
					, (select distinct NAICS_Code AS NAICSCode,
						PRIMARY_INDICATOR_FLAG AS NAICSPrimaryIndicatorCode
						from dbo.XREF_FACILITY_INTEREST_NAICS
						where ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID
						FOR XML PATH('NAICSCodeDetails'), TYPE) --as SICCodeDetails
					, v_Basic_Permit.SectionTownshipRange
					, v_Basic_Permit.FacilityComments
					, v_Basic_Permit.FacilityUserDefinedField1
					, v_Basic_Permit.FacilityUserDefinedField2
					, v_Basic_Permit.FacilityUserDefinedField3
					, v_Basic_Permit.FacilityUserDefinedField4
					, v_Basic_Permit.FacilityUserDefinedField5
			--		, '' as FacilityContact

					,(select 
						v_Contact.AffiliationTypeText,
                       
                        substring(isnull(v_Contact.MiddleName ,'Unknown'), 1, 30) as  FirstName,
						substring(isnull(v_Contact.MiddleName ,'Unknown'), 1, 20) as  MiddleName,
						substring(isnull(v_Contact.LastName ,'Unknown'), 1, 30) as LastName,
						substring(isnull(v_Contact.IndividualTitleText ,'Unknown'), 1, 30) as IndividualTitleText ,


						(select case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
								case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) =0  then null else replace(replace(icis_phone.telephone_nmbr,'-',''),' ','') end as TelephoneNumber,
								case when len(replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','') end as TelephoneExtensionNumber
							from icis_phone inner join icis_contact_phone on icis_phone.phone_id = icis_contact_phone.phone_id
							where icis_contact_phone.contact_id = v_contact.contact_id
							for xml path('Telephone'),type) 

					from 
						(select distinct
							--XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID
							xref_activity_contact.activity_id
							, icis_contact.contact_id
							, xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr

							, substring(isnull(icis_contact.first_name ,'Unknown'), 1, 30) as  FirstName
							, substring(isnull(icis_contact.middle_name ,'Unknown'), 1, 20) as  MiddleName
							, substring(isnull(icis_contact.last_name ,'Unknown'), 1, 30) as LastName
							, substring(isnull(icis_contact.title ,'Unknown'), 1, 30) as IndividualTitleText 

							,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
							,  icis_contact.state_code as  StateCode
							,  icis_contact.region_code as  RegionCode
							,  '' as Telephone
			--				,  NULL AS Telephone
			--				,  icis_contact_electronic_addr.electronic_address_text as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
							, (select top 1  electronic_address_text from icis_contact_electronic_addr where contact_id = icis_contact.contact_id)  as  ElectronicAddressText
							,  convert(varchar(10), xref_activity_contact.begin_date, 120) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
							,  convert(varchar(10), xref_activity_contact.end_date, 120) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
							--select *
							--from dbo.XREF_FACILITY_INTEREST_CONTACT inner join xref_activity_contact on XREF_FACILITY_INTEREST_CONTACT.contact_id = xref_activity_contact.contact_id
							from dbo.XREF_ACTIVITY_FACILITY_INT  inner join xref_activity_contact on XREF_ACTIVITY_FACILITY_INT.activity_id = xref_activity_contact.activity_id
							   left join icis_contact on xref_activity_contact.contact_id = icis_contact.contact_id
							where xref_activity_contact.ACTIVITY_ID = v_Basic_Permit.ACTIVITY_ID and dbo.XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID = v_Basic_Permit.ICIS_FACILITY_INTEREST_ID
							) v_Contact
					for xml path('Contact') ,type
					) as FacilityContact
			--		, '' as FacilityAddress
					,(select 
							v_Address.AffiliationTypeText,
							v_Address.OrganizationFormalName,
							v_Address.OrganizationDUNSNumber,
							v_Address.MailingAddressText,
							case when len(isnull(rtrim(ltrim(v_Address.SupplementalAddressText)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.SupplementalAddressText)) end as SupplementalAddressText,
							case when len(isnull(rtrim(ltrim(v_Address.MailingAddressCityName)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.MailingAddressCityName)) end as MailingAddressCityName,
							isnull(v_Address.MailingAddressStateCode, 'MI') as MailingAddressStateCode,
							case when len(isnull(rtrim(ltrim(v_Address.MailingAddressZipCode)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.MailingAddressZipCode)) end as  MailingAddressZipCode,
							case when len(isnull(rtrim(ltrim(v_Address.CountyName)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.CountyName)) end as CountyName,
							case when len(isnull(rtrim(ltrim(v_Address.MailingAddressCountryCode)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.MailingAddressCountryCode)) end as MailingAddressCountryCode ,
							v_Address.DivisionName,
							v_Address.LocationProvince,
							 null as Telephone
					from 
						(select distinct xref_facility_interest_address.address_id
							, xref_facility_interest_address.ICIS_FACILITY_INTEREST_ID
							, xref_facility_interest_address.affiliation_type_code as  AffiliationTypeText   --  ,  xref_activity_address.affiliation_type_code, xref_activity_contact.affiliation_type_code,  xref_facility_interest_contact.affiliation_type_code, xr
							,  icis_address.organization_formal_name as  OrganizationFormalName   --  ,  icis_contact.organization_formal_name, icis_limit_trade_partner.organization_formal_name
							,  icis_address.organization_duns_nmbr as  OrganizationDUNSNumber   --  , icis_facility_interest.organization_duns_nmbr, icis_limit_trade_partner.organization_duns_nmbr
							,  icis_address.street_address as  MailingAddressText   -- , icis_limit_trade_partner.street_address
							,  icis_address.supplemental_address_text as  SupplementalAddressText   -- , icis_limit_trade_partner.supplemental_address_text
							,  icis_address.city as  MailingAddressCityName   -- , icis_limit_trade_partner.city
							,  icis_address.state_code as  MailingAddressStateCode    -- , icis_limit_trade_partner.state_code
							,  icis_address.zip as  MailingAddressZipCode   --  , icis_limit_trade_partner.zip
							,  icis_address.county as  CountyName  --  , icis_limit_trade_partner.county
							,  icis_address.country_code as  MailingAddressCountryCode    --  , icis_limit_trade_partner.country_code
							,  icis_address.division_name as  DivisionName   --  , icis_limit_trade_partner.division_name
							,  icis_address.province as  LocationProvince   --  , icis_limit_trade_partner.province
							, '' Telephone
							,  (select electronic_address_text from icis_address_electronic_addr where address_id = xref_facility_interest_address.address_id) as  ElectronicAddressText   --,  icis_contact_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
							,  convert(varchar(10), xref_facility_interest_address.begin_date , 120) as  StartDateOfAddressAssociation -- ,  xref_activity_address.begin_date , xref_prog_report_sw_address.begin_date, xref_prog_rpt_ms4_address.begin_date 
							,  convert(varchar(10),xref_facility_interest_address.end_date, 120) as EndDateOfAddressAssociation   --  xref_activity_address.end_date , xref_prog_report_sw_address.end_date, xref_prog_rpt_ms4_address.end_date
							from xref_facility_interest_address left join icis_ADDRESS on xref_facility_interest_address.ADDRESS_id = icis_ADDRESS.ADDRESS_id
							where xref_facility_interest_address.ICIS_FACILITY_INTEREST_ID = v_Basic_Permit.ICIS_FACILITY_INTEREST_ID
							) v_Address
					for xml path('Address') ,type
					) as FacilityAddress
			--		, '' as GeographicCoordinates
					,case when len(rtrim(fi.GEOCODE_LONGITUDE)) = 0 then null else fi.GEOCODE_LONGITUDE end as 'GeographicCoordinates/LatitudeMeasure'
					,case when len(rtrim(fi.GEOCODE_LATITUDE)) = 0 then null else fi.GEOCODE_LATITUDE end as  'GeographicCoordinates/LongitudeMeasure'
					,case when len(rtrim(fi.HORIZONTAL_ACCURACY_MEASURE)) = 0 then null else fi.HORIZONTAL_ACCURACY_MEASURE  end as  'GeographicCoordinates/HorizontalAccuracyMeasure'
					,case when len(rtrim(fi.GEOMETRIC_TYPE_CODE)) = 0 then null else fi.GEOMETRIC_TYPE_CODE  end as  'GeographicCoordinates/GeometricTypeCode'
					,case when len(rtrim(fi.HORIZONTAL_COLLECT_METHOD_CODE)) = 0 then null else fi.HORIZONTAL_COLLECT_METHOD_CODE  end as  'GeographicCoordinates/HorizontalCollectionMethodCode'
					,case when len(rtrim(fi.HORIZONTAL_REF_DATUM_CODE)) = 0 then null else fi.HORIZONTAL_REF_DATUM_CODE  end as  'GeographicCoordinates/HorizontalReferenceDatumCode'
					,case when len(rtrim(fi.REFERENCE_POINT_CODE)) = 0 then null else fi.REFERENCE_POINT_CODE  end as  'GeographicCoordinates/ReferencePointCode'
					,case when len(rtrim(fi.SOURCE_MAP_SCALE_NMBR)) = 0 then null else fi.SOURCE_MAP_SCALE_NMBR  end as  'GeographicCoordinates/SourceMapScaleNumber'
				from 
					(select 
						XREF_ACTIVITY_FACILITY_INT.Activity_id
						,icis_facility_interest.ICIS_FACILITY_INTEREST_ID
			--			, 'MIU'+ substring(cast(icis_facility_interest.zip as varchar) , 1, 2) + replicate('0', 4-len(cast(row_number() over (order by icis_facility_interest.facility_name asc) as varchar))) + cast(row_number() over (order by icis_facility_interest.facility_name asc) as varchar) PermitIdentifier
			--			, 'MIU'+ substring(cast(icis_facility_interest.zip as varchar) , 1, 2) + replicate('0', 4 - len(cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)))) +cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)) as  PermitIdentifier
						, isnull((select max(EXTERNAL_PERMIT_NMBR) from dbo.ICIS_PERMIT where Activity_ID = XREF_ACTIVITY_FACILITY_INT.Activity_ID and PERMIT_TYPE_CODE =  'UFT'),
							('MIU'+ substring(cast(icis_facility_interest.zip as varchar) , 1, 2) + replicate('0', 4 - len(cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)))) +cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)))) as PermitIdentifier 
						, icis_facility_interest.facility_name  as FacilitySiteName        --- Required "Yes when adding or replacing basic, GPCF permits and unpermitted facilities "  Repeatable No -- Common Parent : Facility, UnpermittedFacility
						, icis_facility_interest.location_address  as LocationAddressText        --- Required "Yes when adding or replacing basic, GPCF permits and unpermitted facilities "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.supplemental_address_text,'')) =0 then null else icis_facility_interest.supplemental_address_text end  as SupplementalLocationText        --- Required "No"  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.city  as LocalityName        --- Required "Yes for adding basic, GPCF permits and unpermitted facilities for adding basic, GPCF permits and unpermitted facilities No for all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.state_code  as LocationStateCode        --- Required "Yes for adding basic, GPCF permits and unpermitted facilities for adding basic, GPCF permits and unpermitted facilities No for all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, rtrim(icis_facility_interest.zip)  as LocationZipCode        --- Required "Yes for adding basic, GPCF permits and unpermitted facilities for adding basic, GPCF permits and unpermitted facilities No for all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.country_code  as LocationCountryCode        --- Required "Yes"  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.organization_duns_nmbr  as OrganizationDUNSNumber        --- , icis_limit_trade_partner.organization_duns_nmbr , icis_address.organization_duns_nmbr Required "No"  Repeatable No -- Common Parent : Address EffluentTradePartnerAddress UnpermittedFacility
						, icis_facility_interest.state_facility_id  as StateFacilityIdentifier        --- Required "No"  Repeatable No -- Common Parent : Facility, UnpermittedFacility
						, icis_facility_interest.state_region  as StateRegionCode        --- Required "No"  Repeatable No -- Common Parent : Facility, UnpermittedFacility
						, icis_facility_interest.congressional_dist_num  as FacilityCongressionalDistrictNumber        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, null  as FacilityClassification        --- xref_fac_int_classification.classification_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
						, icis_facility_interest.small_business_flag  as FacilitySmallBusinessIndicator        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, null  as PolicyCode        --- xref_facility_interest_policy.policy_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
						, null  as OriginatingProgramsCode        --- xref_facility_interest_program.program_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.facility_type_code,'')) =0 then null else icis_facility_interest.facility_type_code  end as FacilityTypeofOwnershipCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.federal_facility_id,'')) =0 then null else icis_facility_interest.federal_facility_id end  as FederalFacilityIdentificationNumber        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.federal_agency_code,'')) =0 then null else  icis_facility_interest.federal_agency_code end  as FederalAgencyCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.environmental_justice_code,'')) =0 then null else  icis_facility_interest.environmental_justice_code end  as FacilityEnvironmentalJusticeCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.tribal_land_r_code,'')) =0 then null else  icis_facility_interest.tribal_land_r_code end  as TribalLandCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.construction_project_name,'')) =0 then null else  icis_facility_interest.construction_project_name end  as ConstructionProjectName        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.construction_project_lat,'')) =0 then null else  icis_facility_interest.construction_project_lat end  as ConstructionProjectLatitudeMeasure        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.construction_project_long,'')) =0 then null else  icis_facility_interest.construction_project_long end  as ConstructionProjectLongitudeMeasure        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.section_township_range,'')) =0 then null else  icis_facility_interest.section_township_range end  as SectionTownshipRange        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.comment_text,'')) =0 then null else  icis_facility_interest.comment_text end  as FacilityComments        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf1,'')) =0 then null else icis_facility_interest.udf1 end as FacilityUserDefinedField1        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf2,'')) =0 then null else icis_facility_interest.udf2 end   as FacilityUserDefinedField2        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf3,'')) =0 then null else icis_facility_interest.udf3 end   as FacilityUserDefinedField3        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf4,'')) =0 then null else icis_facility_interest.udf4 end   as FacilityUserDefinedField4        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf5,'')) =0 then null else icis_facility_interest.udf5 end   as FacilityUserDefinedField5        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, null  as PermitCommentsText        --- icis_permit.comment_text Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
						--from dbo.icis_facility_interest
						from dbo.icis_facility_interest  inner join dbo.XREF_ACTIVITY_FACILITY_INT 
								 on  icis_facility_interest.ICIS_FACILITY_INTEREST_ID = XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID
						where dbo.icis_facility_interest.ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID and dbo.XREF_ACTIVITY_FACILITY_INT.activity_id =b.activity_id
					 ) v_Basic_Permit 
					where v_Basic_Permit.ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID
					for xml path('Facility'), type
					)  -- as UnpermittedFacilityData
					from dbo.icis_facility_interest fi inner join dbo.XREF_ACTIVITY_FACILITY_INT b
							 on  fi.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID 
							  inner join icis_activity c on b.activity_id = c.activity_id
					where c.activity_id = a.activity_id
                   for xml path(''), type)

		, convert(varchar(10), cast(ApplicationReceivedDate  as datetime) , 126) as ApplicationReceivedDate
		, convert(varchar(10), cast(PermitApplicationCompletionDate  as datetime) , 126) as PermitApplicationCompletionDate
		, NewSourceIndicator
		--, '' as ComplianceTrackingStatus

				, (select TOP 1 case when Icis_perm_comp_status.permit_comp_status_flag ='N' then 'I' 
                               when Icis_perm_comp_status.permit_comp_status_flag = 'Y' then 'A' 
                               else Icis_perm_comp_status.permit_comp_status_flag end  as StatusCode
					, convert(varchar(10), cast(Icis_perm_comp_status.new_status_begin_date as datetime) , 126) as StatusStartDate
					, Icis_perm_comp_status.status_reason_text as StatusReason 
					from  (
							select 
							a.*, 
							case 
								   when b.PermitEffectiveDate  > a.status_begin_date then  b.PermitEffectiveDate 
								   else  a.status_begin_date end as new_status_begin_date
							from dbo.Icis_perm_comp_status a  inner join (select distinct icis_permit.Activity_id,PermitEffectiveDate from dbo.Icis_permit 
																				where Icis_permit.Permit_Type_Code='GPC')  b 
							on a.activity_id = b.activity_id
							) Icis_perm_comp_status 
					where Icis_perm_comp_status.perm_comp_status_id = (select max(perm_comp_status_id) from Icis_perm_comp_status where activity_id = Icis_perm_comp_status.activity_id)
                    order by cast(Icis_perm_comp_status.new_status_begin_date as datetime) desc
					for xml path('ComplianceTrackingStatus'), type )



		--, '' as EffluentGuidelineCode
				, (SELECT distinct perm_effluent_code as EffluentGuidelineCode FROM dbo.Icis_perm_effluent_guide 
					WHERE ACTIVITY_ID =  a.ACTIVITY_ID
					for xml path('') , type)      
		  
		, PermitStateWaterBodyCode
		, PermitStateWaterBodyName
		, FederalGrantIndicator
		, DMRCognizantOfficial
--		, replace(DMRCognizantOfficialTelephoneNumber,'-','') as DMRCognizantOfficialTelephoneNumber
		, case when len(replace(rtrim(ltrim(DMRCognizantOfficialTelephoneNumber)),'-','')) =0 then null else replace(rtrim(ltrim(DMRCognizantOfficialTelephoneNumber)),'-','') end  as DMRCognizantOfficialTelephoneNumber
		--, '' as PermitContact
					,	(select 
						 xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_contact.affiliation_type_code,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr
/*
						, case when len(isnull(icis_contact.First_Name ,'')) =0 then 'Unknown' else  substring(isnull(substring(icis_contact.First_Name, 1,charindex(' ',icis_contact.First_Name)-1)  ,'Unknown'), 1, 30) end as FirstName 
						, case when len(isnull(icis_contact.Middle_Name ,'')) =0 then 'Unknown' else  substring(isnull(substring(icis_contact.Middle_Name, 1,charindex(' ',icis_contact.Middle_Name)-1)  ,'Unknown'), 1, 20) end  as MiddleName 
						, case when len(isnull(icis_contact.Last_Name ,'')) =0 then 'Unknown' else  substring(isnull(substring(icis_contact.Last_Name, 1,charindex(' ',icis_contact.Last_Name)-1)  ,'Unknown'), 1, 30) end  as LastName 
						, case when len(isnull(icis_contact.title ,'')) =0 then 'Unknown' else  substring(isnull(substring(icis_contact.title, 1,charindex(' ',icis_contact.Title)-1)  ,'Unknown'), 1, 30) end  as IndividualTitleText 
*/

						, substring(isnull(icis_contact.first_name ,'Unknown'), 1, 30) as  FirstName
						, substring(isnull(icis_contact.middle_name ,'Unknown'), 1, 20) as  MiddleName
						, substring(isnull(icis_contact.last_name ,'Unknown'), 1, 30) as LastName
						, substring(isnull(icis_contact.title ,'Unknown'), 1, 30) as IndividualTitleText 


						,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
						,  icis_contact.state_code as  StateCode
						,  icis_contact.region_code as  RegionCode
						,  null as Telephone
/*
								,(select case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
										case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) =0  then null else replace(replace(icis_phone.telephone_nmbr,'-',''),' ','') end as TelephoneNumber,
										case when len(replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','') end as TelephoneExtensionNumber
									from icis_phone inner join icis_contact_phone on icis_phone.phone_id = icis_contact_phone.phone_id
									where icis_contact_phone.contact_id = xref_activity_contact.contact_id
									for xml path('Telephone'),type) 
*/

						,  (SELECT electronic_address_text FROM  icis_contact_electronic_addr where contact_id = xref_activity_contact.contact_id ) as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
						,  convert(varchar(10), cast(xref_activity_contact.begin_date  as datetime) , 126) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
						,  convert(varchar(10), cast(xref_activity_contact.end_date  as datetime) , 126) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
						from xref_activity_contact left join icis_contact on icis_contact.contact_id = xref_activity_contact.contact_id
						where xref_activity_contact.activity_id = v_GeneralPermit.activity_id  --'1200007474'
						for xml path('Contact'), type) as PermitContact 
		--, '' as PermitAddress
					,	(select 
						 xref_activity_address.affiliation_type_code as  AffiliationTypeText   --  ,  xref_activity_address.affiliation_type_code, xref_activity_contact.affiliation_type_code,  xref_facility_interest_contact.affiliation_type_code, xr
						,  icis_address.organization_formal_name as  OrganizationFormalName   --  ,  icis_contact.organization_formal_name, icis_limit_trade_partner.organization_formal_name
						,  icis_address.organization_duns_nmbr as  OrganizationDUNSNumber   --  , icis_facility_interest.organization_duns_nmbr, icis_limit_trade_partner.organization_duns_nmbr
						,  icis_address.street_address as  MailingAddressText   -- , icis_limit_trade_partner.street_address
/*
						,  isnull(icis_address.supplemental_address_text, 'Unknown') as  SupplementalAddressText   -- , icis_limit_trade_partner.supplemental_address_text
						,  isnull(icis_address.city, 'Unknonw') as  MailingAddressCityName   -- , icis_limit_trade_partner.city
						,  isnull(icis_address.state_code, 'MI') as  MailingAddressStateCode    -- , icis_limit_trade_partner.state_code
						,  isnull(icis_address.zip, 'Unknonw') as  MailingAddressZipCode   --  , icis_limit_trade_partner.zip
						,  isnull(icis_address.county, 'Unknonw') as  CountyName  --  , icis_limit_trade_partner.county

*/
						,case when len(isnull(rtrim(ltrim(icis_address.supplemental_address_text)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.supplemental_address_text)) end as SupplementalAddressText
						,case when len(isnull(rtrim(ltrim(icis_address.city)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.city)) end as MailingAddressCityName
						,isnull(icis_address.state_code, 'MI') as MailingAddressStateCode
						,case when len(isnull(rtrim(ltrim(icis_address.zip)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.zip)) end as  MailingAddressZipCode
						,case when len(isnull(rtrim(ltrim(icis_address.county)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.county)) end as CountyName
		--				,  icis_address.country_code as  MailingAddressCountryCode    --  , icis_limit_trade_partner.country_code
						,  icis_address.division_name as  DivisionName   --  , icis_limit_trade_partner.division_name
						,  icis_address.province as  LocationProvince   --  , icis_limit_trade_partner.province
						,  null as Telephone
/*
								,(select case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
										case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) =0  then null else replace(replace(icis_phone.telephone_nmbr,'-',''),' ','') end as TelephoneNumber,
										case when len(replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','') end as TelephoneExtensionNumber
									from icis_phone inner join icis_address_phone on icis_phone.phone_id = icis_address_phone.phone_id
									where icis_address_phone.address_id = xref_activity_address.address_id
									for xml path('Telephone'),type) 
*/
						,  (select electronic_address_text from icis_address_electronic_addr where address_id = xref_activity_address.address_id) as  ElectronicAddressText   --,  icis_contact_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
						,  convert(varchar(10), cast(xref_activity_address.begin_date   as datetime) , 126) as  StartDateOfAddressAssociation -- ,  xref_activity_address.begin_date , xref_prog_report_sw_address.begin_date, xref_prog_rpt_ms4_address.begin_date 
						,  convert(varchar(10), cast(xref_activity_address.end_date  as datetime) , 126) as EndDateOfAddressAssociation   --  xref_activity_address.end_date , xref_prog_report_sw_address.end_date, xref_prog_rpt_ms4_address.end_date
						from xref_activity_address left join icis_ADDRESS on xref_activity_address.ADDRESS_id = icis_ADDRESS.ADDRESS_id
						where xref_activity_address.activity_id = v_GeneralPermit.activity_id 
						for xml path('Address'), type) as PermitAddress
		, SignificantIUIndicator
		, ReceivingPermitIdentifier 
		from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : GeneralPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='GPC') v_GeneralPermit 
		where activity_id = a.activity_id
		for xml path('GeneralPermit'), type
	) 
	from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : GeneralPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='GPC') a
	WHERE a.ACTIVITY_ID = b.activity_id
	for xml path ('')
--	for xml path ('GeneralPermitData')
	)   as instance_xml
	,(
	select 
	(select  'D' as TransactionType , convert(varchar(30), getdate(), 126)  as TransactionTimestamp
	for xml path('TransactionHeader'), type)	
	, (
		select 
		 PermitIdentifier
		from dbo.Icis_permit 
		where Icis_permit.Permit_Type_Code='GPC' 
		and activity_id = a.activity_id
		for xml path('GeneralPermit'), type
	) 
	from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : GeneralPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='GPC') a
	WHERE a.ACTIVITY_ID = b.activity_id
	for xml path ('GeneralPermitData')
	)  as del_instance_xml
from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : GeneralPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : GeneralPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : GeneralPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : GeneralPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='GPC'  ) b
/*** 20120227  ****/
    --                 and activity_id in (select activity_id from dbo.XREF_ACTIVITY_ADDRESS where activity_id =3 )) b
--WHERE a.ACTIVITY_ID IN ('1200039357')



GO
/****** Object:  View [dbo].[v_xml_BasicPermit]    Script Date: 04/10/2012 15:03:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop view [v_xml_BasicPermit1]
CREATE view [dbo].[v_xml_BasicPermit]
as
select 
b.*
	,(
	select 
--	(select  'R' as TransactionType , convert(varchar(20), getdate(), 126)  + '0Z' as TransactionTimestamp
--	for xml path('TransactionHeader'), type)	
--	, (
     (
		select 
		 PermitIdentifier
		, PermitTypeCode
		, isnull(AgencyTypeCode, 'ST6') as AgencyTypeCode
		, case when PermitStatusCode = 'NON' then 'NON' else null end as PermitStatusCode
		/*
					, PermitIssueDate
					, PermitEffectiveDate
					, PermitExpirationDate

		*/
--/* 20120227  can not modify existing permit effective date 
		, convert(varchar(10), cast(PermitIssueDate as datetime) , 126) as  PermitIssueDate
		, convert(varchar(10), cast(PermitEffectiveDate as datetime), 126) as  PermitEffectiveDate
		, convert(varchar(10), cast(PermitExpirationDate as datetime), 126) as  PermitExpirationDate
--*/
		, ReissuancePriorityPermitIndicator
		, BacklogReasonText
		, PermitIssuingOrganizationTypeName
		--, '' as OtherPermits
				,  ( select other_external_permit_nmbr from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherPermitIdentifier'
				,  ( select organization_name from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherOrganizationName'
				,  ( select identifier_context_desc from Icis_other_permit where activity_id = a.activity_id) as  'OtherPermits/OtherPermitIdentifierContextName'

		--, '' as AssociatedPermit 
				,  (select  distinct Master_external_permit_nmbr from icis_permit where activity_id = a.activity_id) as 'AssociatedPermit/AssociatedPermitIdentifier' 
				,  (select case when Master_external_permit_nmbr is not null then 'APR' else null end  from icis_permit where activity_id = a.activity_id) as 'AssociatedPermit/AssociatedPermitReasonCode'

		--, '' as SICCodeDetails
				, (select distinct  sic_code as SICCode, 'Y' as SICPrimaryIndicatorCode  from xref_activity_sic where activity_id = a.activity_id AND LEN(SIC_CODE) >0
				   for xml path('SICCodeDetails'), type)

		, PermitAppealedIndicator
		, PermitUserDefinedDataElement1Text
		, PermitUserDefinedDataElement2Text
		, PermitUserDefinedDataElement3Text
		, PermitUserDefinedDataElement4Text
		, PermitUserDefinedDataElement5Text
--		, PermitCommentsText
		, CASE WHEN LEN(RTRIM(PermitCommentsText)) > 0 THEN PermitCommentsText ELSE NULL END AS PermitCommentsText 
        , MajorMinorRatingCode
--		, case when (select count(*) from XREF_PERM_COMPONENT_TYPE where component_type_code = 'POTW' AND ACTIVITY_ID = A.ACTIVITY_ID) =0 THEN NULL ELSE MajorMinorRatingCode END AS  MajorMinorRatingCode
		, TotalApplicationDesignFlowNumber
		, TotalApplicationAverageFlowNumber
		--, '' as Facility
		,(
					select --top 20
--					(select 'R' as TransactionType, getdate() as TransactionTimestamp for xml path('TransactionHeader'), type),
					(
					select 
--					 v_Basic_Permit.PermitIdentifier,
					 v_Basic_Permit.FacilitySiteName
					, v_Basic_Permit.LocationAddressText
					, v_Basic_Permit.SupplementalLocationText
					, v_Basic_Permit.LocalityName
					, v_Basic_Permit.LocationStateCode
					, v_Basic_Permit.LocationZipCode
					, CASE WHEN LEN(ISNULL(v_Basic_Permit.LocationCountryCode,'')) =0 THEN 'US' ELSE UPPER(LocationCountryCode) END AS LocationCountryCode
					, v_Basic_Permit.OrganizationDUNSNumber
					, v_Basic_Permit.StateFacilityIdentifier
					, v_Basic_Permit.StateRegionCode
					, v_Basic_Permit.FacilityCongressionalDistrictNumber
					, v_Basic_Permit.FacilitySmallBusinessIndicator
					, v_Basic_Permit.FacilityTypeofOwnershipCode
					, v_Basic_Permit.FederalFacilityIdentificationNumber
					, v_Basic_Permit.FederalAgencyCode
					, v_Basic_Permit.FacilityEnvironmentalJusticeCode
					, v_Basic_Permit.TribalLandCode
					, v_Basic_Permit.ConstructionProjectName
					, v_Basic_Permit.ConstructionProjectLatitudeMeasure
					, v_Basic_Permit.ConstructionProjectLongitudeMeasure
			--		, '' as SICCodeDetails
			--		, '' as NAICSCodeDetails
					, (select distinct SIC_Code AS SICCode,
						PRIMARY_INDICATOR_FLAG AS SICPrimaryIndicatorCode
						from dbo.XREF_FACILITY_INTEREST_SIC
						where ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID
						FOR XML PATH('SICCodeDetails'), TYPE) --as SICCodeDetails
					, (select distinct NAICS_Code AS NAICSCode,
						PRIMARY_INDICATOR_FLAG AS NAICSPrimaryIndicatorCode
						from dbo.XREF_FACILITY_INTEREST_NAICS
						where ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID
						FOR XML PATH('NAICSCodeDetails'), TYPE) --as SICCodeDetails
					, v_Basic_Permit.SectionTownshipRange
					, v_Basic_Permit.FacilityComments
					, v_Basic_Permit.FacilityUserDefinedField1
					, v_Basic_Permit.FacilityUserDefinedField2
					, v_Basic_Permit.FacilityUserDefinedField3
					, v_Basic_Permit.FacilityUserDefinedField4
					, v_Basic_Permit.FacilityUserDefinedField5
			--		, '' as FacilityContact

					,(select 
						v_Contact.AffiliationTypeText,
/*
						case when len(isnull(v_Contact.FirstName ,'')) =0 then 'Unknown' else substring(isnull(substring(v_Contact.FirstName, 1,charindex(' ',v_Contact.FirstName)-1)  ,'Unknown'), 1, 30) end as FirstName ,
						case when len(isnull(v_Contact.MiddleName ,'')) =0 then 'Unknown' else substring(isnull(substring(v_Contact.MiddleName, 1,charindex(' ',v_Contact.MiddleName)-1)  ,'Unknown'), 1, 20) end as MiddleName ,
						case when len(isnull(v_Contact.LastName ,'')) =0 then 'Unknown' else substring(isnull(substring(v_Contact.LastName, 1,charindex(' ',v_Contact.LastName)-1)  ,'Unknown'), 1, 30) end as LastName ,
						case when len(isnull(v_Contact.IndividualTitleText ,'')) =0 then 'Unknown' else substring(isnull(substring(v_Contact.IndividualTitleText, 1,charindex(' ',v_Contact.IndividualTitleText)-1)  ,'Unknown'), 1, 30) end as IndividualTitleText ,
*/
                       
                        substring(isnull(v_Contact.MiddleName ,'Unknown'), 1, 30) as  FirstName,
						substring(isnull(v_Contact.MiddleName ,'Unknown'), 1, 20) as  MiddleName,
						substring(isnull(v_Contact.LastName ,'Unknown'), 1, 30) as LastName,
						substring(isnull(v_Contact.IndividualTitleText ,'Unknown'), 1, 30) as IndividualTitleText ,

						(select case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
								case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) =0  then null else replace(replace(icis_phone.telephone_nmbr,'-',''),' ','') end as TelephoneNumber,
								case when len(replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','') end as TelephoneExtensionNumber
							from icis_phone inner join icis_contact_phone on icis_phone.phone_id = icis_contact_phone.phone_id
							where icis_contact_phone.contact_id = v_contact.contact_id
							for xml path('Telephone'),type) 
					from 
						(select distinct
							--XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID
							xref_activity_contact.activity_id
							, icis_contact.contact_id
							, xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr

							, substring(isnull(icis_contact.first_name ,'Unknown'), 1, 30) as  FirstName
							, substring(isnull(icis_contact.middle_name ,'Unknown'), 1, 20) as  MiddleName
							, substring(isnull(icis_contact.last_name ,'Unknown'), 1, 30) as LastName
							, substring(isnull(icis_contact.title ,'Unknown'), 1, 30) as IndividualTitleText 

							,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
							,  icis_contact.state_code as  StateCode
							,  icis_contact.region_code as  RegionCode
							,  '' as Telephone
			--				,  NULL AS Telephone
			--				,  icis_contact_electronic_addr.electronic_address_text as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
							, (select top 1  electronic_address_text from icis_contact_electronic_addr where contact_id = icis_contact.contact_id)  as  ElectronicAddressText
							,  convert(varchar(10), xref_activity_contact.begin_date, 120) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
							,  convert(varchar(10), xref_activity_contact.end_date, 120) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
							--select *
							--from dbo.XREF_FACILITY_INTEREST_CONTACT inner join xref_activity_contact on XREF_FACILITY_INTEREST_CONTACT.contact_id = xref_activity_contact.contact_id
							from dbo.XREF_ACTIVITY_FACILITY_INT  inner join xref_activity_contact on XREF_ACTIVITY_FACILITY_INT.activity_id = xref_activity_contact.activity_id
							   left join icis_contact on xref_activity_contact.contact_id = icis_contact.contact_id
							where xref_activity_contact.ACTIVITY_ID = v_Basic_Permit.ACTIVITY_ID and dbo.XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID = v_Basic_Permit.ICIS_FACILITY_INTEREST_ID
							) v_Contact
					for xml path('Contact') ,type
					) as FacilityContact
			--		, '' as FacilityAddress
					,(select 
							v_Address.AffiliationTypeText,
							v_Address.OrganizationFormalName,
							v_Address.OrganizationDUNSNumber,
							v_Address.MailingAddressText,
							case when len(isnull(rtrim(ltrim(v_Address.SupplementalAddressText)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.SupplementalAddressText)) end as SupplementalAddressText,
							case when len(isnull(rtrim(ltrim(v_Address.MailingAddressCityName)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.MailingAddressCityName)) end as MailingAddressCityName,
							isnull(v_Address.MailingAddressStateCode, 'MI') as MailingAddressStateCode,
							case when len(isnull(rtrim(ltrim(v_Address.MailingAddressZipCode)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.MailingAddressZipCode)) end as  MailingAddressZipCode,
							case when len(isnull(rtrim(ltrim(v_Address.CountyName)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.CountyName)) end as CountyName,
							case when len(isnull(rtrim(ltrim(v_Address.MailingAddressCountryCode)),'')) = 0 then 'Unknown' else rtrim(ltrim(v_Address.MailingAddressCountryCode)) end as MailingAddressCountryCode ,
							v_Address.DivisionName,
							v_Address.LocationProvince,
							 null as Telephone
/*

						(select case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
								case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) =0  then null else replace(replace(icis_phone.telephone_nmbr,'-',''),' ','') end as TelephoneNumber,
								case when len(replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','') end as TelephoneExtensionNumber
								from icis_phone inner join icis_address_phone on icis_phone.phone_id = icis_address_phone.phone_id
								where icis_address_phone.address_id = v_address.address_id
								for xml path('Telephone'),type)  
*/
			--				v_Address.ElectronicAddressText,
			--				v_Address.StartDateOfAddressAssociation,
			--				v_Address.EndDateOfAddressAssociation
					from 
						(select distinct xref_facility_interest_address.address_id
							, xref_facility_interest_address.ICIS_FACILITY_INTEREST_ID
							, xref_facility_interest_address.affiliation_type_code as  AffiliationTypeText   --  ,  xref_activity_address.affiliation_type_code, xref_activity_contact.affiliation_type_code,  xref_facility_interest_contact.affiliation_type_code, xr
							,  icis_address.organization_formal_name as  OrganizationFormalName   --  ,  icis_contact.organization_formal_name, icis_limit_trade_partner.organization_formal_name
							,  icis_address.organization_duns_nmbr as  OrganizationDUNSNumber   --  , icis_facility_interest.organization_duns_nmbr, icis_limit_trade_partner.organization_duns_nmbr
							,  icis_address.street_address as  MailingAddressText   -- , icis_limit_trade_partner.street_address
							,  icis_address.supplemental_address_text as  SupplementalAddressText   -- , icis_limit_trade_partner.supplemental_address_text
							,  icis_address.city as  MailingAddressCityName   -- , icis_limit_trade_partner.city
							,  icis_address.state_code as  MailingAddressStateCode    -- , icis_limit_trade_partner.state_code
							,  icis_address.zip as  MailingAddressZipCode   --  , icis_limit_trade_partner.zip
							,  icis_address.county as  CountyName  --  , icis_limit_trade_partner.county
							,  icis_address.country_code as  MailingAddressCountryCode    --  , icis_limit_trade_partner.country_code
							,  icis_address.division_name as  DivisionName   --  , icis_limit_trade_partner.division_name
							,  icis_address.province as  LocationProvince   --  , icis_limit_trade_partner.province
							, '' Telephone
							,  (select electronic_address_text from icis_address_electronic_addr where address_id = xref_facility_interest_address.address_id) as  ElectronicAddressText   --,  icis_contact_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
							,  convert(varchar(10), xref_facility_interest_address.begin_date , 120) as  StartDateOfAddressAssociation -- ,  xref_activity_address.begin_date , xref_prog_report_sw_address.begin_date, xref_prog_rpt_ms4_address.begin_date 
							,  convert(varchar(10),xref_facility_interest_address.end_date, 120) as EndDateOfAddressAssociation   --  xref_activity_address.end_date , xref_prog_report_sw_address.end_date, xref_prog_rpt_ms4_address.end_date
							from xref_facility_interest_address left join icis_ADDRESS on xref_facility_interest_address.ADDRESS_id = icis_ADDRESS.ADDRESS_id
							where xref_facility_interest_address.ICIS_FACILITY_INTEREST_ID = v_Basic_Permit.ICIS_FACILITY_INTEREST_ID
							) v_Address
					for xml path('Address') ,type
					) as FacilityAddress
			--		, '' as GeographicCoordinates
					,case when len(rtrim(fi.GEOCODE_LONGITUDE)) = 0 then null else fi.GEOCODE_LONGITUDE end as 'GeographicCoordinates/LatitudeMeasure'
					,case when len(rtrim(fi.GEOCODE_LATITUDE)) = 0 then null else fi.GEOCODE_LATITUDE end as  'GeographicCoordinates/LongitudeMeasure'
					,case when len(rtrim(fi.HORIZONTAL_ACCURACY_MEASURE)) = 0 then null else fi.HORIZONTAL_ACCURACY_MEASURE  end as  'GeographicCoordinates/HorizontalAccuracyMeasure'
					,case when len(rtrim(fi.GEOMETRIC_TYPE_CODE)) = 0 then null else fi.GEOMETRIC_TYPE_CODE  end as  'GeographicCoordinates/GeometricTypeCode'
					,case when len(rtrim(fi.HORIZONTAL_COLLECT_METHOD_CODE)) = 0 then null else fi.HORIZONTAL_COLLECT_METHOD_CODE  end as  'GeographicCoordinates/HorizontalCollectionMethodCode'
					,case when len(rtrim(fi.HORIZONTAL_REF_DATUM_CODE)) = 0 then null else fi.HORIZONTAL_REF_DATUM_CODE  end as  'GeographicCoordinates/HorizontalReferenceDatumCode'
					,case when len(rtrim(fi.REFERENCE_POINT_CODE)) = 0 then null else fi.REFERENCE_POINT_CODE  end as  'GeographicCoordinates/ReferencePointCode'
					,case when len(rtrim(fi.SOURCE_MAP_SCALE_NMBR)) = 0 then null else fi.SOURCE_MAP_SCALE_NMBR  end as  'GeographicCoordinates/SourceMapScaleNumber'
				from 
					(select 
						XREF_ACTIVITY_FACILITY_INT.Activity_id
						,icis_facility_interest.ICIS_FACILITY_INTEREST_ID
			--			, 'MIU'+ substring(cast(icis_facility_interest.zip as varchar) , 1, 2) + replicate('0', 4-len(cast(row_number() over (order by icis_facility_interest.facility_name asc) as varchar))) + cast(row_number() over (order by icis_facility_interest.facility_name asc) as varchar) PermitIdentifier
			--			, 'MIU'+ substring(cast(icis_facility_interest.zip as varchar) , 1, 2) + replicate('0', 4 - len(cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)))) +cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)) as  PermitIdentifier
						, isnull((select max(EXTERNAL_PERMIT_NMBR) from dbo.ICIS_PERMIT where Activity_ID = XREF_ACTIVITY_FACILITY_INT.Activity_ID and PERMIT_TYPE_CODE =  'UFT'),
							('MIU'+ substring(cast(icis_facility_interest.zip as varchar) , 1, 2) + replicate('0', 4 - len(cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)))) +cast((icis_facility_interest.icis_facility_interest_id % 10000) as varchar(4)))) as PermitIdentifier 
						, icis_facility_interest.facility_name  as FacilitySiteName        --- Required "Yes when adding or replacing basic, GPCF permits and unpermitted facilities "  Repeatable No -- Common Parent : Facility, UnpermittedFacility
						, icis_facility_interest.location_address  as LocationAddressText        --- Required "Yes when adding or replacing basic, GPCF permits and unpermitted facilities "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.supplemental_address_text,'')) =0 then null else icis_facility_interest.supplemental_address_text end  as SupplementalLocationText        --- Required "No"  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.city  as LocalityName        --- Required "Yes for adding basic, GPCF permits and unpermitted facilities for adding basic, GPCF permits and unpermitted facilities No for all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.state_code  as LocationStateCode        --- Required "Yes for adding basic, GPCF permits and unpermitted facilities for adding basic, GPCF permits and unpermitted facilities No for all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, rtrim(icis_facility_interest.zip)  as LocationZipCode        --- Required "Yes for adding basic, GPCF permits and unpermitted facilities for adding basic, GPCF permits and unpermitted facilities No for all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.country_code  as LocationCountryCode        --- Required "Yes"  Repeatable No -- Address Parent : Facility, UnpermittedFacility
						, icis_facility_interest.organization_duns_nmbr  as OrganizationDUNSNumber        --- , icis_limit_trade_partner.organization_duns_nmbr , icis_address.organization_duns_nmbr Required "No"  Repeatable No -- Common Parent : Address EffluentTradePartnerAddress UnpermittedFacility
						, icis_facility_interest.state_facility_id  as StateFacilityIdentifier        --- Required "No"  Repeatable No -- Common Parent : Facility, UnpermittedFacility
						, icis_facility_interest.state_region  as StateRegionCode        --- Required "No"  Repeatable No -- Common Parent : Facility, UnpermittedFacility
						, icis_facility_interest.congressional_dist_num  as FacilityCongressionalDistrictNumber        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, null  as FacilityClassification        --- xref_fac_int_classification.classification_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
						, icis_facility_interest.small_business_flag  as FacilitySmallBusinessIndicator        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, null  as PolicyCode        --- xref_facility_interest_policy.policy_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
						, null  as OriginatingProgramsCode        --- xref_facility_interest_program.program_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.facility_type_code,'')) =0 then null else icis_facility_interest.facility_type_code  end as FacilityTypeofOwnershipCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.federal_facility_id,'')) =0 then null else icis_facility_interest.federal_facility_id end  as FederalFacilityIdentificationNumber        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.federal_agency_code,'')) =0 then null else  icis_facility_interest.federal_agency_code end  as FederalAgencyCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.environmental_justice_code,'')) =0 then null else  icis_facility_interest.environmental_justice_code end  as FacilityEnvironmentalJusticeCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.tribal_land_r_code,'')) =0 then null else  icis_facility_interest.tribal_land_r_code end  as TribalLandCode        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.construction_project_name,'')) =0 then null else  icis_facility_interest.construction_project_name end  as ConstructionProjectName        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.construction_project_lat,'')) =0 then null else  icis_facility_interest.construction_project_lat end  as ConstructionProjectLatitudeMeasure        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.construction_project_long,'')) =0 then null else  icis_facility_interest.construction_project_long end  as ConstructionProjectLongitudeMeasure        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.section_township_range,'')) =0 then null else  icis_facility_interest.section_township_range end  as SectionTownshipRange        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.comment_text,'')) =0 then null else  icis_facility_interest.comment_text end  as FacilityComments        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf1,'')) =0 then null else icis_facility_interest.udf1 end as FacilityUserDefinedField1        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf2,'')) =0 then null else icis_facility_interest.udf2 end   as FacilityUserDefinedField2        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf3,'')) =0 then null else icis_facility_interest.udf3 end   as FacilityUserDefinedField3        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf4,'')) =0 then null else icis_facility_interest.udf4 end   as FacilityUserDefinedField4        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, case when len(isnull(icis_facility_interest.udf5,'')) =0 then null else icis_facility_interest.udf5 end   as FacilityUserDefinedField5        --- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
						, null  as PermitCommentsText        --- icis_permit.comment_text Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
						--from dbo.icis_facility_interest
						from dbo.icis_facility_interest  inner join dbo.XREF_ACTIVITY_FACILITY_INT 
								 on  icis_facility_interest.ICIS_FACILITY_INTEREST_ID = XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID
						where dbo.icis_facility_interest.ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID and dbo.XREF_ACTIVITY_FACILITY_INT.activity_id =b.activity_id
					 ) v_Basic_Permit 
					where v_Basic_Permit.ICIS_FACILITY_INTEREST_ID = fi.ICIS_FACILITY_INTEREST_ID
					for xml path('Facility'), type
					)  -- as UnpermittedFacilityData
					from dbo.icis_facility_interest fi inner join dbo.XREF_ACTIVITY_FACILITY_INT b
							 on  fi.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID 
							  inner join icis_activity c on b.activity_id = c.activity_id
					where c.activity_id = a.activity_id
                   for xml path(''), type)

		, convert(varchar(10), cast(ApplicationReceivedDate  as datetime) , 126) as ApplicationReceivedDate
		, convert(varchar(10), cast(PermitApplicationCompletionDate  as datetime) , 126) as PermitApplicationCompletionDate
		, NewSourceIndicator
		--, '' as ComplianceTrackingStatus

				, (select TOP 1 case when Icis_perm_comp_status.permit_comp_status_flag ='N' then 'I' 
                               when Icis_perm_comp_status.permit_comp_status_flag = 'Y' then 'A' 
                               else Icis_perm_comp_status.permit_comp_status_flag end  as StatusCode
					, convert(varchar(10), cast(Icis_perm_comp_status.new_status_begin_date as datetime) , 126) as StatusStartDate
					, Icis_perm_comp_status.status_reason_text as StatusReason 
					from  (
							select 
							a.*, 
							case 
								   when b.PermitEffectiveDate  > a.status_begin_date then  b.PermitEffectiveDate 
								   else  a.status_begin_date end as new_status_begin_date
							from dbo.Icis_perm_comp_status a  inner join (select distinct icis_permit.Activity_id,PermitEffectiveDate from dbo.Icis_permit 
																				where Icis_permit.Permit_Type_Code='NPD')  b 
							on a.activity_id = b.activity_id
							) Icis_perm_comp_status 
					where Icis_perm_comp_status.perm_comp_status_id = (select max(perm_comp_status_id) from Icis_perm_comp_status where activity_id = Icis_perm_comp_status.activity_id)
                    order by cast(Icis_perm_comp_status.new_status_begin_date as datetime) desc
					for xml path('ComplianceTrackingStatus'), type )


		/*
		,  (select permit_comp_status_flag from Icis_perm_comp_status  where perm_comp_status_id = (select max(perm_comp_status_id) from Icis_perm_comp_status where activity_id = a.activity_id)) as  'ComplianceTrackingStatus/StatusCode'
		,  (select case when permit_comp_status_flag is not null or permit_comp_status_flag != 'null' then status_begin_date else null end  from Icis_perm_comp_status  where  perm_comp_status_id = (select max(perm_comp_status_id) from Icis_perm_comp_status where activity_id = a.activity_id)) as  'ComplianceTrackingStatus/StatusStartDate'
		,  (select case when permit_comp_status_flag is not null or permit_comp_status_flag != 'null' then status_reason_text else null end from Icis_perm_comp_status  where perm_comp_status_id = (select max(perm_comp_status_id) from Icis_perm_comp_status where activity_id = a.activity_id)) as  'ComplianceTrackingStatus/StatusReason'
		*/

		--, '' as EffluentGuidelineCode
				, (SELECT distinct perm_effluent_code as EffluentGuidelineCode FROM dbo.Icis_perm_effluent_guide 
					WHERE ACTIVITY_ID =  a.ACTIVITY_ID
					for xml path('') , type)      
		  
		, PermitStateWaterBodyCode
		, PermitStateWaterBodyName
		, FederalGrantIndicator
		, DMRCognizantOfficial
--		, replace(DMRCognizantOfficialTelephoneNumber,'-','') as DMRCognizantOfficialTelephoneNumber
		, case when len(replace(rtrim(ltrim(DMRCognizantOfficialTelephoneNumber)),'-','')) =0 then null else replace(rtrim(ltrim(DMRCognizantOfficialTelephoneNumber)),'-','') end  as DMRCognizantOfficialTelephoneNumber
		--, '' as PermitContact
					,	(select 
						 xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_contact.affiliation_type_code,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr

						, substring(isnull(icis_contact.first_name ,'Unknown'), 1, 30) as  FirstName
						, substring(isnull(icis_contact.middle_name ,'Unknown'), 1, 20) as  MiddleName
						, substring(isnull(icis_contact.last_name ,'Unknown'), 1, 30) as LastName
						, substring(isnull(icis_contact.title ,'Unknown'), 1, 30) as IndividualTitleText 


						,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
						,  icis_contact.state_code as  StateCode
						,  icis_contact.region_code as  RegionCode
						,  null as Telephone

						,  (SELECT electronic_address_text FROM  icis_contact_electronic_addr where contact_id = xref_activity_contact.contact_id ) as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
						,  convert(varchar(10), cast(xref_activity_contact.begin_date  as datetime) , 126) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
						,  convert(varchar(10), cast(xref_activity_contact.end_date  as datetime) , 126) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
						from xref_activity_contact left join icis_contact on icis_contact.contact_id = xref_activity_contact.contact_id
						where xref_activity_contact.activity_id = v_BasicPermit.activity_id  --'1200007474'
						for xml path('Contact'), type) as PermitContact 
		--, '' as PermitAddress
					,	(select 
						 xref_activity_address.affiliation_type_code as  AffiliationTypeText   --  ,  xref_activity_address.affiliation_type_code, xref_activity_contact.affiliation_type_code,  xref_facility_interest_contact.affiliation_type_code, xr
						,  icis_address.organization_formal_name as  OrganizationFormalName   --  ,  icis_contact.organization_formal_name, icis_limit_trade_partner.organization_formal_name
						,  icis_address.organization_duns_nmbr as  OrganizationDUNSNumber   --  , icis_facility_interest.organization_duns_nmbr, icis_limit_trade_partner.organization_duns_nmbr
						,  icis_address.street_address as  MailingAddressText   -- , icis_limit_trade_partner.street_address

						,case when len(isnull(rtrim(ltrim(icis_address.supplemental_address_text)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.supplemental_address_text)) end as SupplementalAddressText
						,case when len(isnull(rtrim(ltrim(icis_address.city)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.city)) end as MailingAddressCityName
						,isnull(icis_address.state_code, 'MI') as MailingAddressStateCode
						,case when len(isnull(rtrim(ltrim(icis_address.zip)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.zip)) end as  MailingAddressZipCode
						,case when len(isnull(rtrim(ltrim(icis_address.county)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.county)) end as CountyName
		--				,  icis_address.country_code as  MailingAddressCountryCode    --  , icis_limit_trade_partner.country_code
						,  icis_address.division_name as  DivisionName   --  , icis_limit_trade_partner.division_name
						,  icis_address.province as  LocationProvince   --  , icis_limit_trade_partner.province
						,  null as Telephone

						,  (select electronic_address_text from icis_address_electronic_addr where address_id = xref_activity_address.address_id) as  ElectronicAddressText   --,  icis_contact_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
						,  convert(varchar(10), cast(xref_activity_address.begin_date   as datetime) , 126) as  StartDateOfAddressAssociation -- ,  xref_activity_address.begin_date , xref_prog_report_sw_address.begin_date, xref_prog_rpt_ms4_address.begin_date 
						,  convert(varchar(10), cast(xref_activity_address.end_date  as datetime) , 126) as EndDateOfAddressAssociation   --  xref_activity_address.end_date , xref_prog_report_sw_address.end_date, xref_prog_rpt_ms4_address.end_date
						from xref_activity_address left join icis_ADDRESS on xref_activity_address.ADDRESS_id = icis_ADDRESS.ADDRESS_id
						where xref_activity_address.activity_id = v_BasicPermit.activity_id 
						for xml path('Address'), type) as PermitAddress
		, SignificantIUIndicator
		, ReceivingPermitIdentifier 
		from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='NPD') v_BasicPermit 
		where activity_id = a.activity_id
		for xml path('BasicPermit'), type
	) 
	from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='NPD') a
	WHERE a.ACTIVITY_ID = b.activity_id
	for xml path ('')
--	for xml path ('BasicPermitData')
	)   as instance_xml
	,(
	select 
	(select  'D' as TransactionType , convert(varchar(30), getdate(), 126)  as TransactionTimestamp
	for xml path('TransactionHeader'), type)	
	, (
		select 
		 PermitIdentifier
		from dbo.Icis_permit 
		where Icis_permit.Permit_Type_Code='NPD' 
		and activity_id = a.activity_id
		for xml path('BasicPermit'), type
	) 
	from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='NPD') a
	WHERE a.ACTIVITY_ID = b.activity_id
	for xml path ('BasicPermitData')
	)  as del_instance_xml
from (select 
				icis_permit.Activity_id
				,Icis_permit.[EXTERNAL_PERMIT_NMBR] as PermitIdentifier
				, Icis_permit.permit_type_code  as PermitTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.agency_type_code  as AgencyTypeCode        --- Required "Yes when adding a permit; Cannot be blanked out when changing a permit"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.permit_status_code  as PermitStatusCode        --- Required "No.  Once it is submitted it cannot be blanked out."  Repeatable No -- Permit Parent : BasicPermit GeneralPermitCoveredFacility
				, icis_permit.issue_date  as PermitIssueDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.effective_date  as PermitEffectiveDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.expiration_date  as PermitExpirationDate        --- Required "No"  Repeatable No -- Common Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.reissuance_priority  as ReissuancePriorityPermitIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.backlog_reason  as BacklogReasonText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.issuing_agency  as PermitIssuingOrganizationTypeName        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.appeal_flag  as PermitAppealedIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf1  as PermitUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf2  as PermitUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf3  as PermitUserDefinedDataElement3Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf4  as PermitUserDefinedDataElement4Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.udf5  as PermitUserDefinedDataElement5Text        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit
				, icis_permit.comment_text  as PermitCommentsText        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
				, icis_permit.major_rating_nmbr  as MajorMinorRatingCode        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit, GeneralPermit
				, icis_permit.total_design_flow_nmbr  as TotalApplicationDesignFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.actual_average_flow_nmbr  as TotalApplicationAverageFlowNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.app_received_date  as ApplicationReceivedDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.complete_app_received_date  as PermitApplicationCompletionDate        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.new_source_flag  as NewSourceIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, (SELECT perm_effluent_code FROM dbo.Icis_perm_effluent_guide WHERE ACTIVITY_ID =  Icis_permit.ACTIVITY_ID)  as EffluentGuidelineCode        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body  as PermitStateWaterBodyCode        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.state_water_body_name  as PermitStateWaterBodyName        --- Required "No"  Repeatable Yes -- Permit Parent : BasicPermit GeneralPermit
				, Icis_permit.federal_grant_flag  as FederalGrantIndicator        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.dmr_cognizant_official  as DMRCognizantOfficial        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, replace(icis_permit.dmr_cognizant_offcl_telephone,'-','')  as DMRCognizantOfficialTelephoneNumber        --- Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit
				, icis_permit.iu_significant_flag  as SignificantIUIndicator        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				, icis_permit.receiving_potw_id  as ReceivingPermitIdentifier        --- Required "No"  Repeatable No -- Basic Permit Parent : BasicPermit
				from dbo.Icis_permit 
				where Icis_permit.Permit_Type_Code='NPD'  ) b
/*** 20120227  ****/
    --                 and activity_id in (select activity_id from dbo.XREF_ACTIVITY_ADDRESS where activity_id =3 )) b
--WHERE a.ACTIVITY_ID IN ('1200039357')




GO
/****** Object:  View [dbo].[v_xml_Limits]    Script Date: 04/10/2012 15:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[v_xml_Limits]
as
select distinct
b.*
	,(
	select 
--	(select  'R' as TransactionType , convert(varchar(20), getdate(), 126)  + '0Z' as TransactionTimestamp
--	for xml path('TransactionHeader'), type)	
--	, (
     (
		select 
		 PermitIdentifier
		, PermittedFeatureIdentifier
		, LimitSetDesignator
		, ParameterCode
		, MonitoringSiteDescriptionCode
		, LimitSeasonNumber
		, convert(varchar(10), cast(LimitStartDate as datetime) , 126) as LimitStartDate
		, convert(varchar(10), cast(LimitEndDate as datetime) , 126) as LimitEndDate
		, LimitTypeCode 
		--, MonthLimitApplies
				, (select month_code as MonthLimitApplies from xref_limit_month where limit_id = v_limits.limit_id order by xref_limit_month_id
				   for xml path(''), type) 
		, SampleTypeText
		, FrequencyOfAnalysisCode
		, EligibleForBurdenReduction
		, LimitStayTypeCode
		, convert(varchar(10), cast(StayStartDate as datetime) , 126) as StayStartDate
		, convert(varchar(10), cast(StayEndDate as datetime) , 126) as StayEndDate
		, StayReasonText
		, CalculateViolationsIndicator
		, EnforcementActionIdentifier
		, FinalOrderIdentifier
		, BasisOfLimit
		, LimitModificationTypeCode
		, convert(varchar(10), cast(LimitModificationEffectiveDate as datetime) , 126) as LimitModificationEffectiveDate
		, LimitsUserDefinedField1
		, LimitsUserDefinedField2
		, LimitsUserDefinedField3
		, ConcentrationNumericConditionUnitMeasureCode
		, QuantityNumericConditionUnitMeasureCode
			,(select distinct
				value_type_code  as  NumericConditionText
			,   limit_value_nmbr  as  NumericConditionQuantity
			,	statistical_base_code as NumericConditionStatisticalBaseCode
			,   case when len(rtrim(ltrim(isnull(value_qualifier_code,'')))) =0  then '=' else rtrim(ltrim(isnull(value_qualifier_code,''))) end as NumericConditionQualifier
			,	optional_monitoring_flag as NumericConditionOptionalMonitoringIndicator
			,	stay_value_nmbr as NumericConditionStayValue
			from icis_limit_value where limit_id = v_limits.limit_id
			for xml path('NumericCondition') , type)
		--select *
		from 
			(select distinct
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			, icis_limit_set.limit_set_id
			, icis_limit.limit_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr) as PermittedFeatureIdentifier        --- Icis_perm_feature.perm_feature_nmbr Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, rtrim(Icis_limit_set.limit_set_designator)  as LimitSetDesignator        --- Icis_limit_set.limit_set_designator Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit.parameter_code)  as ParameterCode        --- , Icis_dmr_form_parameter.parameter_code  Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, rtrim(icis_limit.monitoring_location_code)  as MonitoringSiteDescriptionCode        --- Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			--, Icis_limit.limit_season_id  as LimitSeasonNumber        --- Required "Yes"  Repeatable No -- Key Elements Parent : EffluentTradePartner Limits, ParameterLimits ReportParameter DMRViolation DischargeMonitoringReportViolation DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, 0  as LimitSeasonNumber
			, icis_limit.limit_begin_date  as LimitStartDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit DischargeMonitoringReportIdentifier, LimitSegmentIdentifier
			, icis_limit.limit_end_date  as LimitEndDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit LimitSegmentIdentifier
			, Icis_limit.limit_type_code  as LimitTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
			, ''   as MonthLimitApplies        --- xref_limit_month.month_code Required "Yes when adding a record No for all others"  Repeatable Yes, up to 12 times -- Limits Parent : Limits, Limit
			, icis_limit.sample_type_code  as SampleTypeText        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.frequency_of_analysis_code  as FrequencyOfAnalysisCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.burden_reduction_flag  as EligibleForBurdenReduction        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.stay_type_code  as LimitStayTypeCode        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_begin_date  as StayStartDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_end_date  as StayEndDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_reason_text  as StayReasonText        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_value_calc_flag  as CalculateViolationsIndicator        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, (select enf_identifier from icis_enforcement where activity_id = Icis_perm_feature.perm_feature_id) as EnforcementActionIdentifier        --- icis_enforcement.enf_identifier  Required "Yes"  Repeatable No -- Key Elements Parent : LinkageToEnforcementAction ComplianceSchedule EnforcementActionViolationKey FormalEnforcementAction InformalEnforcementAction Limits, Limit Milestone LinkageToEnforcementAction
			, (select enf_conclusion_nmbr from icis_enf_conclusion where activity_id = Icis_perm_feature.perm_feature_id) as  FinalOrderIdentifier
			, icis_limit.basis_of_limit_code  as BasisOfLimit        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.modification_type_code  as LimitModificationTypeCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.modification_effective_date  as LimitModificationEffectiveDate        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf1  as LimitsUserDefinedField1        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf2  as LimitsUserDefinedField2        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf3  as LimitsUserDefinedField3        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'C%') as ConcentrationNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code  Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
			, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'Q%')  as QuantityNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
			from Icis_perm_feature left join Icis_limit_set 
				 on Icis_perm_feature.perm_feature_id = Icis_limit_set.perm_feature_id  
			---inner join dbo.ICIS_LIMIT_SET_STATUS on  ICIS_LIMIT_SET_STATUS.LIMIT_SET_ID = Icis_limit_set.LIMIT_SET_ID
			left join dbo.icis_limit
			 on  Icis_limit_set.limit_set_id = icis_limit.limit_set_id and Icis_limit_set.activity_id  = icis_limit.activity_id 
			where  limit_id  in (select limit_id from dbo.ICIS_LIMIT_VALUE)) v_Limits
		where limit_id = a.limit_id and Activity_id= a.Activity_id and PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and LimitSetDesignator = a.LimitSetDesignator and ParameterCode = a.ParameterCode and LimitSeasonNumber = a.LimitSeasonNumber and LimitStartDate =a.LimitStartDate and LimitEndDate = a.LimitEndDate
		for xml path('Limits'), type
		) 
		from 
		(select distinct
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			, icis_limit_set.limit_set_id
			, icis_limit.limit_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr) as PermittedFeatureIdentifier        --- Icis_perm_feature.perm_feature_nmbr Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, rtrim(Icis_limit_set.limit_set_designator)  as LimitSetDesignator        --- Icis_limit_set.limit_set_designator Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit.parameter_code)  as ParameterCode        --- , Icis_dmr_form_parameter.parameter_code  Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, rtrim(icis_limit.monitoring_location_code)  as MonitoringSiteDescriptionCode        --- Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			--, Icis_limit.limit_season_id  as LimitSeasonNumber        --- Required "Yes"  Repeatable No -- Key Elements Parent : EffluentTradePartner Limits, ParameterLimits ReportParameter DMRViolation DischargeMonitoringReportViolation DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, 0  as LimitSeasonNumber
			, icis_limit.limit_begin_date  as LimitStartDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit DischargeMonitoringReportIdentifier, LimitSegmentIdentifier
			, icis_limit.limit_end_date  as LimitEndDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit LimitSegmentIdentifier
			, Icis_limit.limit_type_code  as LimitTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
			, ''   as MonthLimitApplies        --- xref_limit_month.month_code Required "Yes when adding a record No for all others"  Repeatable Yes, up to 12 times -- Limits Parent : Limits, Limit
			, icis_limit.sample_type_code  as SampleTypeText        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.frequency_of_analysis_code  as FrequencyOfAnalysisCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.burden_reduction_flag  as EligibleForBurdenReduction        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.stay_type_code  as LimitStayTypeCode        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_begin_date  as StayStartDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_end_date  as StayEndDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_reason_text  as StayReasonText        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_value_calc_flag  as CalculateViolationsIndicator        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, (select enf_identifier from icis_enforcement where activity_id = Icis_perm_feature.perm_feature_id) as EnforcementActionIdentifier        --- icis_enforcement.enf_identifier  Required "Yes"  Repeatable No -- Key Elements Parent : LinkageToEnforcementAction ComplianceSchedule EnforcementActionViolationKey FormalEnforcementAction InformalEnforcementAction Limits, Limit Milestone LinkageToEnforcementAction
			, (select enf_conclusion_nmbr from icis_enf_conclusion where activity_id = Icis_perm_feature.perm_feature_id) as  FinalOrderIdentifier
			, icis_limit.basis_of_limit_code  as BasisOfLimit        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.modification_type_code  as LimitModificationTypeCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.modification_effective_date  as LimitModificationEffectiveDate        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf1  as LimitsUserDefinedField1        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf2  as LimitsUserDefinedField2        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf3  as LimitsUserDefinedField3        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'C%') as ConcentrationNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code  Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
			, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'Q%')  as QuantityNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
			from Icis_perm_feature left join Icis_limit_set 
				 on Icis_perm_feature.perm_feature_id = Icis_limit_set.perm_feature_id  
			---inner join dbo.ICIS_LIMIT_SET_STATUS on  ICIS_LIMIT_SET_STATUS.LIMIT_SET_ID = Icis_limit_set.LIMIT_SET_ID
			left join dbo.icis_limit
			 on  Icis_limit_set.limit_set_id = icis_limit.limit_set_id and Icis_limit_set.activity_id  = icis_limit.activity_id 
			where  limit_id  in (select limit_id from dbo.ICIS_LIMIT_VALUE)) a
		WHERE a.limit_id = b.limit_id and b.Activity_id= a.Activity_id and b.PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and b.LimitSetDesignator = a.LimitSetDesignator and b.ParameterCode = a.ParameterCode and b.LimitSeasonNumber = a.LimitSeasonNumber and b.LimitStartDate =a.LimitStartDate and b.LimitEndDate = a.LimitEndDate
		for xml path ('')
--		for xml path ('LimitsData')
	)   as instance_xml
	,(
		select 
		(select  'D' as TransactionType , convert(varchar(30), getdate(), 126)  as TransactionTimestamp
		for xml path('TransactionHeader'), type)	
		, (
			select 
				 PermitIdentifier
				, PermittedFeatureIdentifier
				, LimitSetDesignator
				, ParameterCode
				, MonitoringSiteDescriptionCode
				, LimitSeasonNumber
				, convert(varchar(10), cast(LimitStartDate as datetime) , 126) as LimitStartDate
				, convert(varchar(10), cast(LimitEndDate as datetime) , 126) as LimitEndDate
			from   
			(select distinct
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			, icis_limit_set.limit_set_id
			, icis_limit.limit_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr) as PermittedFeatureIdentifier        --- Icis_perm_feature.perm_feature_nmbr Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, rtrim(Icis_limit_set.limit_set_designator)  as LimitSetDesignator        --- Icis_limit_set.limit_set_designator Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit.parameter_code)  as ParameterCode        --- , Icis_dmr_form_parameter.parameter_code  Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, rtrim(icis_limit.monitoring_location_code)  as MonitoringSiteDescriptionCode        --- Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			--, Icis_limit.limit_season_id  as LimitSeasonNumber        --- Required "Yes"  Repeatable No -- Key Elements Parent : EffluentTradePartner Limits, ParameterLimits ReportParameter DMRViolation DischargeMonitoringReportViolation DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, 0  as LimitSeasonNumber
			, icis_limit.limit_begin_date  as LimitStartDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit DischargeMonitoringReportIdentifier, LimitSegmentIdentifier
			, icis_limit.limit_end_date  as LimitEndDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit LimitSegmentIdentifier
			, Icis_limit.limit_type_code  as LimitTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit

			from Icis_perm_feature left join Icis_limit_set 
				 on Icis_perm_feature.perm_feature_id = Icis_limit_set.perm_feature_id  
			---inner join dbo.ICIS_LIMIT_SET_STATUS on  ICIS_LIMIT_SET_STATUS.LIMIT_SET_ID = Icis_limit_set.LIMIT_SET_ID
			left join dbo.icis_limit
			 on  Icis_limit_set.limit_set_id = icis_limit.limit_set_id and Icis_limit_set.activity_id  = icis_limit.activity_id 
			where  limit_id  in (select limit_id from dbo.ICIS_LIMIT_VALUE)) v_Limits
			where limit_id = a.limit_id and Activity_id= a.Activity_id and PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and LimitSetDesignator = a.LimitSetDesignator and ParameterCode = a.ParameterCode and LimitSeasonNumber = a.LimitSeasonNumber and LimitStartDate =a.LimitStartDate and LimitEndDate = a.LimitEndDate
			for xml path('Limits'), type
		) 
		from 
		(select distinct
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			, icis_limit_set.limit_set_id
			, icis_limit.limit_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr) as PermittedFeatureIdentifier        --- Icis_perm_feature.perm_feature_nmbr Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, rtrim(Icis_limit_set.limit_set_designator)  as LimitSetDesignator        --- Icis_limit_set.limit_set_designator Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit.parameter_code)  as ParameterCode        --- , Icis_dmr_form_parameter.parameter_code  Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, rtrim(icis_limit.monitoring_location_code)  as MonitoringSiteDescriptionCode        --- Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			--, Icis_limit.limit_season_id  as LimitSeasonNumber        --- Required "Yes"  Repeatable No -- Key Elements Parent : EffluentTradePartner Limits, ParameterLimits ReportParameter DMRViolation DischargeMonitoringReportViolation DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
			, 0  as LimitSeasonNumber
			, icis_limit.limit_begin_date  as LimitStartDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit DischargeMonitoringReportIdentifier, LimitSegmentIdentifier
			, icis_limit.limit_end_date  as LimitEndDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit LimitSegmentIdentifier
			, Icis_limit.limit_type_code  as LimitTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
			, ''   as MonthLimitApplies        --- xref_limit_month.month_code Required "Yes when adding a record No for all others"  Repeatable Yes, up to 12 times -- Limits Parent : Limits, Limit
			, icis_limit.sample_type_code  as SampleTypeText        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.frequency_of_analysis_code  as FrequencyOfAnalysisCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.burden_reduction_flag  as EligibleForBurdenReduction        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.stay_type_code  as LimitStayTypeCode        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_begin_date  as StayStartDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_end_date  as StayEndDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_reason_text  as StayReasonText        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, icis_limit.stay_value_calc_flag  as CalculateViolationsIndicator        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
			, (select enf_identifier from icis_enforcement where activity_id = Icis_perm_feature.perm_feature_id) as EnforcementActionIdentifier        --- icis_enforcement.enf_identifier  Required "Yes"  Repeatable No -- Key Elements Parent : LinkageToEnforcementAction ComplianceSchedule EnforcementActionViolationKey FormalEnforcementAction InformalEnforcementAction Limits, Limit Milestone LinkageToEnforcementAction
			, (select enf_conclusion_nmbr from icis_enf_conclusion where activity_id = Icis_perm_feature.perm_feature_id) as  FinalOrderIdentifier
			, icis_limit.basis_of_limit_code  as BasisOfLimit        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.modification_type_code  as LimitModificationTypeCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.modification_effective_date  as LimitModificationEffectiveDate        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf1  as LimitsUserDefinedField1        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf2  as LimitsUserDefinedField2        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, icis_limit.udf3  as LimitsUserDefinedField3        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
			, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'C%') as ConcentrationNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code  Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
			, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'Q%')  as QuantityNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
			from Icis_perm_feature left join Icis_limit_set 
				 on Icis_perm_feature.perm_feature_id = Icis_limit_set.perm_feature_id  
			---inner join dbo.ICIS_LIMIT_SET_STATUS on  ICIS_LIMIT_SET_STATUS.LIMIT_SET_ID = Icis_limit_set.LIMIT_SET_ID
			left join dbo.icis_limit
			 on  Icis_limit_set.limit_set_id = icis_limit.limit_set_id and Icis_limit_set.activity_id  = icis_limit.activity_id 
			where  limit_id  in (select limit_id from dbo.ICIS_LIMIT_VALUE)) a
		WHERE a.limit_id = b.limit_id and b.Activity_id= a.Activity_id and b.PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and b.LimitSetDesignator = a.LimitSetDesignator and b.ParameterCode = a.ParameterCode and b.LimitSeasonNumber = a.LimitSeasonNumber and b.LimitStartDate =a.LimitStartDate and b.LimitEndDate = a.LimitEndDate
		for xml path ('LimitsData')
	)  as del_instance_xml
from 
(select distinct
Icis_perm_feature.activity_id
, Icis_perm_feature.perm_feature_id
, icis_limit_set.limit_set_id
, icis_limit.limit_id
,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
, rtrim(Icis_perm_feature.perm_feature_nmbr) as PermittedFeatureIdentifier        --- Icis_perm_feature.perm_feature_nmbr Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
, rtrim(Icis_limit_set.limit_set_designator)  as LimitSetDesignator        --- Icis_limit_set.limit_set_designator Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
, rtrim(icis_limit.parameter_code)  as ParameterCode        --- , Icis_dmr_form_parameter.parameter_code  Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
, rtrim(icis_limit.monitoring_location_code)  as MonitoringSiteDescriptionCode        --- Required "Yes"  Repeatable No -- Key Elements Parent : ReportParameter DMRViolation EffluentTradePartner DischargeMonitoringReportViolation Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
--, Icis_limit.limit_season_id  as LimitSeasonNumber        --- Required "Yes"  Repeatable No -- Key Elements Parent : EffluentTradePartner Limits, ParameterLimits ReportParameter DMRViolation DischargeMonitoringReportViolation DischargeMonitoringReportIdentifier, DMRParameterIdentifier, LimitSegmentIdentifier and Par
, 0  as LimitSeasonNumber
, icis_limit.limit_begin_date  as LimitStartDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit DischargeMonitoringReportIdentifier, LimitSegmentIdentifier
, icis_limit.limit_end_date  as LimitEndDate        --- Required "Yes"  Repeatable No -- Common Parent : EffluentTradePartner Limits, Limit LimitSegmentIdentifier
, Icis_limit.limit_type_code  as LimitTypeCode        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
, ''   as MonthLimitApplies        --- xref_limit_month.month_code Required "Yes when adding a record No for all others"  Repeatable Yes, up to 12 times -- Limits Parent : Limits, Limit
, icis_limit.sample_type_code  as SampleTypeText        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.frequency_of_analysis_code  as FrequencyOfAnalysisCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.burden_reduction_flag  as EligibleForBurdenReduction        --- Required "Yes when adding a record No for all others"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.stay_type_code  as LimitStayTypeCode        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
, icis_limit.stay_begin_date  as StayStartDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
, icis_limit.stay_end_date  as StayEndDate        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
, icis_limit.stay_reason_text  as StayReasonText        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
, icis_limit.stay_value_calc_flag  as CalculateViolationsIndicator        --- Required "No"  Repeatable No -- Stays Parent : Limits, Limit
, (select enf_identifier from icis_enforcement where activity_id = Icis_perm_feature.perm_feature_id) as EnforcementActionIdentifier        --- icis_enforcement.enf_identifier  Required "Yes"  Repeatable No -- Key Elements Parent : LinkageToEnforcementAction ComplianceSchedule EnforcementActionViolationKey FormalEnforcementAction InformalEnforcementAction Limits, Limit Milestone LinkageToEnforcementAction
, (select enf_conclusion_nmbr from icis_enf_conclusion where activity_id = Icis_perm_feature.perm_feature_id) as  FinalOrderIdentifier
, icis_limit.basis_of_limit_code  as BasisOfLimit        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.modification_type_code  as LimitModificationTypeCode        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.modification_effective_date  as LimitModificationEffectiveDate        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.udf1  as LimitsUserDefinedField1        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.udf2  as LimitsUserDefinedField2        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, icis_limit.udf3  as LimitsUserDefinedField3        --- Required "No"  Repeatable No -- Limits Parent : Limits, Limit
, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'C%') as ConcentrationNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code  Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
, (select max(unit_code) from Icis_limit_value where limit_id = icis_limit.limit_id and value_type_code like 'Q%')  as QuantityNumericConditionUnitMeasureCode        --- Icis_limit_value.unit_code Required "Yes when adding a Limit No for all others"  Repeatable No -- Limits Parent : ReportParameter
from Icis_perm_feature left join Icis_limit_set 
     on Icis_perm_feature.perm_feature_id = Icis_limit_set.perm_feature_id  
---inner join dbo.ICIS_LIMIT_SET_STATUS on  ICIS_LIMIT_SET_STATUS.LIMIT_SET_ID = Icis_limit_set.LIMIT_SET_ID
left join dbo.icis_limit
 on  Icis_limit_set.limit_set_id = icis_limit.limit_set_id and Icis_limit_set.activity_id  = icis_limit.activity_id 
where  limit_id  in (select limit_id from dbo.ICIS_LIMIT_VALUE)) b

 --WHERE a.ACTIVITY_ID IN ('1200039357')
GO
/****** Object:  View [dbo].[v_xml_LimitSet]    Script Date: 04/10/2012 15:03:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE view [dbo].[v_xml_LimitSet]
as
select distinct
b.*
	,(
	select 
--	(select  'R' as TransactionType , convert(varchar(20), getdate(), 126)  + '0Z' as TransactionTimestamp
--	for xml path('TransactionHeader'), type)	
--	, (
     (
		select 
		 PermitIdentifier
        , PermittedFeatureIdentifier  
		, LimitSetDesignator
		, LimitSetType
		, LimitSetNameText
		, DMRPrePrintCommentsText
		, AgencyReviewer
		, LimitSetUserDefinedDataElement1Text
		, LimitSetUserDefinedDataElement2Text
		--, LimitSetMonthsApplicable
			, ( select distinct month_code as LimitSetMonthsApplicable from xref_limit_set_month where limit_set_id = v_LimitSet.limit_set_id
			for xml path(''), type)
		--, LimitSetStatus
			,(select distinct 
				status_flag as	LimitSetStatusIndicator
				,convert(varchar(10), cast(status_begin_date	as datetime) , 126) as LimitSetStatusStartDate
				,status_change_reason_text	as LimitSetStatusReasonText
--				from icis_limit_set_status where limit_set_id = v_LimitSet.limit_set_id
				from icis_limit_set_status where  LIMIT_SET_STATUS_ID IN (SELECT MAX(LIMIT_SET_STATUS_ID) FROM icis_limit_set_status WHERE limit_set_id = v_LimitSet.limit_set_id)
				for xml path('LimitSetStatus'), type)
		--, LimitSetSchedule
				,(select distinct 
					case when len(cast(NMBR_OF_REPORT as varchar)) =1 then '0' + cast(NMBR_OF_REPORT as varchar) else NMBR_OF_REPORT end as NumberUnitsReportPeriodInteger
                  , case when len(cast(NMBR_OF_SUBMISSION as varchar)) =1 then '0' + cast(NMBR_OF_SUBMISSION as varchar) else NMBR_OF_REPORT end as NumberSubmissionUnitsInteger
--                   NMBR_OF_REPORT  as NumberUnitsReportPeriodInteger
--				  , NMBR_OF_SUBMISSION as NumberSubmissionUnitsInteger
				  , convert(varchar(10), cast(INITIAL_MONITORING_DATE as datetime) , 126) as InitialMonitoringDate
				  , convert(varchar(10), cast(INITIAL_DMR_DUE_DATE as datetime) , 126) as InitialDMRDueDate
				  , MODIFICATION_TYPE_CODE as LimitSetModificationTypeCode
				  , convert(varchar(10), cast(MODIFICATION_EFFECTIVE_DATE as datetime) , 126) as LimitSetModificationEffectiveDate
				  from icis_limit_set_schedule where LIMIT_SET_ID = v_LimitSet.limit_set_id
				  for xml path('LimitSetSchedule'), type)
		from 
			(select 
			DISTINCT 
			Icis_perm_feature.activity_id
			, rtrim(Icis_perm_feature.perm_feature_id) as perm_feature_id
			, icis_limit_set.limit_set_ID
			,(select rtrim(EXTERNAL_PERMIT_NMBR) as EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr)  as PermittedFeatureIdentifier
			, rtrim(Icis_limit_set.limit_set_designator)    as LimitSetDesignator        --- Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit_set.limit_set_type_code)  as LimitSetType        --- Required "Yes"  Repeatable No -- LimitSet Parent : LimitSet
			, case when len(rtrim(icis_limit_set.limit_set_name )) !=0  then rtrim(icis_limit_set.limit_set_name ) else null end as LimitSetNameText        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.dmr_comment_text)) !=0  then rtrim(icis_limit_set.dmr_comment_text ) else null end   as DMRPrePrintCommentsText        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.agency_reviewer )) !=0  then rtrim(icis_limit_set.agency_reviewer ) else null end  as AgencyReviewer        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.udf1 )) !=0  then rtrim(icis_limit_set.udf1 ) else null end  as LimitSetUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.udf2 )) !=0  then rtrim(icis_limit_set.udf2 ) else null end  as LimitSetUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			from dbo.Icis_perm_feature left join  dbo.icis_limit_set on Icis_perm_feature.perm_feature_id = icis_limit_set.perm_feature_id) v_LimitSet 
		where LIMIT_SET_ID = a.limit_set_id and Activity_id= a.Activity_id and PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and LimitSetDesignator = a.LimitSetDesignator and LimitSetType = a.LimitSetType
		for xml path('LimitSet'), type
		) 
		from 
			(select 
			DISTINCT 
			Icis_perm_feature.activity_id
			, rtrim(Icis_perm_feature.perm_feature_id) as perm_feature_id
			, icis_limit_set.limit_set_ID
			,(select rtrim(EXTERNAL_PERMIT_NMBR) as EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr)  as PermittedFeatureIdentifier
			, rtrim(Icis_limit_set.limit_set_designator)    as LimitSetDesignator        --- Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit_set.limit_set_type_code)  as LimitSetType        --- Required "Yes"  Repeatable No -- LimitSet Parent : LimitSet
			, case when len(rtrim(icis_limit_set.limit_set_name )) !=0  then rtrim(icis_limit_set.limit_set_name ) else null end as LimitSetNameText        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.dmr_comment_text)) !=0  then rtrim(icis_limit_set.dmr_comment_text ) else null end   as DMRPrePrintCommentsText        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.agency_reviewer )) !=0  then rtrim(icis_limit_set.agency_reviewer ) else null end  as AgencyReviewer        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.udf1 )) !=0  then rtrim(icis_limit_set.udf1 ) else null end  as LimitSetUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.udf2 )) !=0  then rtrim(icis_limit_set.udf2 ) else null end  as LimitSetUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			from dbo.Icis_perm_feature left join  dbo.icis_limit_set on Icis_perm_feature.perm_feature_id = icis_limit_set.perm_feature_id) a
		WHERE a.Limit_Set_id = b.Limit_Set_id and b.Activity_id= a.Activity_id and b.PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and b.LimitSetDesignator = a.LimitSetDesignator and b.LimitSetType = a.LimitSetType
--		for xml path ('LimitSetData')
        for xml path ('')
	)   as instance_xml

	,(
	select 
	(select  'D' as TransactionType , convert(varchar(30), getdate(), 126)  as TransactionTimestamp
	for xml path('TransactionHeader'), type)	
	, (
		select distinct
		 PermitIdentifier
        , PermittedFeatureIdentifier  
		, LimitSetDesignator
		, LimitSetType
		from 
		(select 
			DISTINCT 
			Icis_perm_feature.activity_id
			, rtrim(Icis_perm_feature.perm_feature_id) as perm_feature_id
			, icis_limit_set.limit_set_ID
			,(select rtrim(EXTERNAL_PERMIT_NMBR) as EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr)  as PermittedFeatureIdentifier
			, rtrim(Icis_limit_set.limit_set_designator)    as LimitSetDesignator        --- Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit_set.limit_set_type_code)  as LimitSetType        --- Required "Yes"  Repeatable No -- LimitSet Parent : LimitSet
			from dbo.Icis_perm_feature left join  dbo.icis_limit_set on Icis_perm_feature.perm_feature_id = icis_limit_set.perm_feature_id) v_LimitSet
		where Limit_Set_id = a.Limit_Set_id and Activity_id= a.Activity_id and PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and LimitSetDesignator = a.LimitSetDesignator and LimitSetType = a.LimitSetType
		for xml path('LimitsSet'), type
	) 
	from 
	(select 
			DISTINCT 
			Icis_perm_feature.activity_id
			, rtrim(Icis_perm_feature.perm_feature_id) as perm_feature_id
			, icis_limit_set.limit_set_ID
			,(select rtrim(EXTERNAL_PERMIT_NMBR) as EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr)  as PermittedFeatureIdentifier
			, rtrim(Icis_limit_set.limit_set_designator)    as LimitSetDesignator        --- Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit_set.limit_set_type_code)  as LimitSetType        --- Required "Yes"  Repeatable No -- LimitSet Parent : LimitSet
			, case when len(rtrim(icis_limit_set.limit_set_name )) !=0  then rtrim(icis_limit_set.limit_set_name ) else null end as LimitSetNameText        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.dmr_comment_text)) !=0  then rtrim(icis_limit_set.dmr_comment_text ) else null end   as DMRPrePrintCommentsText        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.agency_reviewer )) !=0  then rtrim(icis_limit_set.agency_reviewer ) else null end  as AgencyReviewer        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.udf1 )) !=0  then rtrim(icis_limit_set.udf1 ) else null end  as LimitSetUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.udf2 )) !=0  then rtrim(icis_limit_set.udf2 ) else null end  as LimitSetUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			from dbo.Icis_perm_feature left join  dbo.icis_limit_set on Icis_perm_feature.perm_feature_id = icis_limit_set.perm_feature_id) a
	WHERE a.Limit_Set_id = b.Limit_Set_id and b.Activity_id= a.Activity_id and b.PermittedFeatureIdentifier = a.PermittedFeatureIdentifier  and b.LimitSetDesignator = a.LimitSetDesignator and b.LimitSetType = a.LimitSetType
	for xml path ('LimitsSetData')
	)  as del_instance_xml
from 
(select 
	DISTINCT 
			Icis_perm_feature.activity_id
			, rtrim(Icis_perm_feature.perm_feature_id) as perm_feature_id
			, icis_limit_set.limit_set_ID
			,(select rtrim(EXTERNAL_PERMIT_NMBR) as EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, rtrim(Icis_perm_feature.perm_feature_nmbr)  as PermittedFeatureIdentifier
			, rtrim(Icis_limit_set.limit_set_designator)    as LimitSetDesignator        --- Required "Yes"  Repeatable No -- Key Elements Parent : DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits DischargeMonitoringReportIdentifier, DMRParamete
			, rtrim(icis_limit_set.limit_set_type_code)  as LimitSetType        --- Required "Yes"  Repeatable No -- LimitSet Parent : LimitSet
			, case when len(rtrim(icis_limit_set.limit_set_name )) !=0  then rtrim(icis_limit_set.limit_set_name ) else null end as LimitSetNameText        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.dmr_comment_text)) !=0  then rtrim(icis_limit_set.dmr_comment_text ) else null end   as DMRPrePrintCommentsText        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.agency_reviewer )) !=0  then rtrim(icis_limit_set.agency_reviewer ) else null end  as AgencyReviewer        --- Required "No"  Repeatable No -- Limit Set Parent : ScheduledLimitSet, UnscheduledLimitSet
			, case when len(rtrim(icis_limit_set.udf1 )) !=0  then rtrim(icis_limit_set.udf1 ) else null end  as LimitSetUserDefinedDataElement1Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			, case when len(rtrim(icis_limit_set.udf2 )) !=0  then rtrim(icis_limit_set.udf2 ) else null end  as LimitSetUserDefinedDataElement2Text        --- Required "No"  Repeatable No -- Limit Set Parent : LimitSet
			from dbo.Icis_perm_feature left join  dbo.icis_limit_set on Icis_perm_feature.perm_feature_id = icis_limit_set.perm_feature_id) b
--WHERE a.ACTIVITY_ID IN ('1200039357')
GO
/****** Object:  View [dbo].[v_xml_PermittedFeature]    Script Date: 04/10/2012 15:03:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


--drop view [v_xml_PermittedFeature1]
CREATE view [dbo].[v_xml_PermittedFeature]
as
select distinct
b.*
,(
	select 
--	(select  'R' as TransactionType , convert(varchar(20), getdate(), 126)  + '0Z' as TransactionTimestamp
--	for xml path('TransactionHeader'), type)	
--	, (
     (

	select 
 PermitIdentifier
, PermittedFeatureIdentifier
, PermittedFeatureTypeCode
---, '' AS PermittedFeatureCharacteristics
, (select characteristic_code as PermittedFeatureCharacteristics from xref_perm_feature_character where PERM_FEATURE_ID = v_PermittedFeature.PERM_FEATURE_ID
   for xml path(''),TYPE)        
, PermittedFeatureDescription
--, '' as PermittedFeatureTreatmentTypeCode
, (SELECT treatment_type_code as PermittedFeatureTreatmentTypeCode FROM xref_perm_feature_treatment where PERM_FEATURE_ID = v_PermittedFeature.PERM_FEATURE_ID
   for xml path(''),TYPE)        
, PermittedFeatureDesignFlowNumber
, PermittedFeatureActualAverageFlowNumber
, PermittedFeatureStateWaterBodyCode
, PermittedFeatureStateWaterBodyName
, RTRIM(LTRIM(REPLACE(PermittedFeatureUserDefinedDataElement1,' ',''))) AS PermittedFeatureUserDefinedDataElement1
, RTRIM(LTRIM(REPLACE(PermittedFeatureUserDefinedDataElement2,' ',''))) AS PermittedFeatureUserDefinedDataElement2
, FieldSize
, IsSiteOwnByFacility
, IsSystemLinedWithLeachate
, DoesUnitHaveDailyCover
, PropertyBoundaryDistance
, IsRequiredNitrateGroundWater
, WellNumber
--, GeographicCoordinates
 ,( select LATITUDE_MEASURE as LatitudeMeasure, LONGITUDE_MEASURE as LongitudeMeasure, HORIZONTAL_ACCURACY_MEASURE  as HorizontalAccuracyMeasure,GEOMETRIC_TYPE_CODE as GeometricTypeCode, HORIZONTAL_COLLECT_METHOD_CODE as HorizontalCollectionMethodCode
 , HORIZONTAL_REF_DATUM_CODE as HorizontalReferenceDatumCode, REFERENCE_POINT_CODE as ReferencePointCode, SOURCE_MAP_SCALE_NMBR as SourceMapScaleNumber
 from dbo.ICIS_Perm_Feature_Coord  where Perm_Feature_ID = v_PermittedFeature.Perm_Feature_ID
 for xml path('GeographicCoordinates'), type)              
, SourcePermittedFeatureDetailText
--, '' as SiteOwnerContact
			,	(select 
	--			 xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_contact.affiliation_type_code,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr
                'SOA' as  AffiliationTypeText
				, substring(isnull(icis_contact.first_name ,'Unknown'), 1, 30) as  FirstName
				, substring(isnull(icis_contact.middle_name ,'Unknown'), 1, 20) as  MiddleName
				, substring(isnull(icis_contact.last_name ,'Unknown'), 1, 30) as LastName
				, substring(isnull(icis_contact.title ,'Unknown'), 1, 30) as IndividualTitleText 
				,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
				,  icis_contact.state_code as  StateCode
				,  icis_contact.region_code as  RegionCode
				, null  as Telephone
/*
						,(select  phone_type_code as TelephoneNumberTypeCode, case when  len(rtrim(isnull(telephone_nmbr,''))) !=0  then rtrim(replace(telephone_nmbr,'-','')) else null end as TelephoneNumber, case when  len(rtrim(isnull(telephone_extension_nmbr,''))) !=0 then rtrim(isnull(telephone_extension_nmbr,'')) else null end as TelephoneExtensionNumber
						 from dbo.icis_contact_phone where CONTACT_ID = xref_activity_contact.contact_id
						 for xml path('Telephone'), type) 
*/
				,  (SELECT electronic_address_text FROM  icis_contact_electronic_addr where contact_id = xref_activity_contact.contact_id ) as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
				,  convert(varchar(10), cast(xref_activity_contact.begin_date as datetime) , 126) as   StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
				,  convert(varchar(10), cast(xref_activity_contact.end_date as datetime) , 126) as   EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
				from xref_activity_contact left join icis_contact on icis_contact.contact_id = xref_activity_contact.contact_id
                     left join dbo.icis_contact_phone on  icis_contact.contact_id = icis_contact_phone.contact_id
                     left join dbo.icis_phone on icis_contact_phone.phone_id = icis_phone.phone_id
				where xref_activity_contact.activity_id = v_PermittedFeature.activity_id  --'1200007474'
				for xml path('Contact'), type) as SiteOwnerContact 
--, '' as SiteOwnerAddress
			,	(select 
--				 xref_activity_address.affiliation_type_code as  AffiliationTypeText   --  ,  xref_activity_address.affiliation_type_code, xref_activity_contact.affiliation_type_code,  xref_facility_interest_contact.affiliation_type_code, xr
                'SOA' as  AffiliationTypeText
				,  icis_address.organization_formal_name as  OrganizationFormalName   --  ,  icis_contact.organization_formal_name, icis_limit_trade_partner.organization_formal_name
				,  icis_address.organization_duns_nmbr as  OrganizationDUNSNumber   --  , icis_facility_interest.organization_duns_nmbr, icis_limit_trade_partner.organization_duns_nmbr
				,  icis_address.street_address as  MailingAddressText   -- , icis_limit_trade_partner.street_address
/*
				,  isnull(icis_address.supplemental_address_text, 'Unknown') as  SupplementalAddressText   -- , icis_limit_trade_partner.supplemental_address_text
				,  isnull(icis_address.city, 'Unknown') as  MailingAddressCityName   -- , icis_limit_trade_partner.city
				,  isnull(icis_address.state_code,'MI') as  MailingAddressStateCode    -- , icis_limit_trade_partner.state_code
				,  isnull(icis_address.zip,'0000') as  MailingAddressZipCode   --  , icis_limit_trade_partner.zip
				,  isnull(icis_address.county,'Unknown')   as  CountyName  --  , icis_limit_trade_partner.county
*/
				,case when len(isnull(rtrim(ltrim(icis_address.supplemental_address_text)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.supplemental_address_text)) end as SupplementalAddressText
				,case when len(isnull(rtrim(ltrim(icis_address.city)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.city)) end as MailingAddressCityName
				,isnull(icis_address.state_code, 'MI') as MailingAddressStateCode
				,case when len(isnull(rtrim(ltrim(icis_address.zip)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.zip)) end as  MailingAddressZipCode
				,case when len(isnull(rtrim(ltrim(icis_address.county)),'')) = 0 then 'Unknown' else rtrim(ltrim(icis_address.county)) end as CountyName

--				,  icis_address.country_code as  MailingAddressCountryCode    --  , icis_limit_trade_partner.country_code
				,  icis_address.division_name as  DivisionName   --  , icis_limit_trade_partner.division_name
				,  icis_address.province as  LocationProvince   --  , icis_limit_trade_partner.province
				,  null as Telephone
/*
						,(select  phone_type_code as TelephoneNumberTypeCode, case when  len(rtrim(isnull(telephone_nmbr,''))) !=0  then rtrim(replace(telephone_nmbr,'-','')) else null end as TelephoneNumber, case when  len(rtrim(isnull(telephone_extension_nmbr,''))) !=0 then rtrim(isnull(telephone_extension_nmbr,'')) else null end as TelephoneExtensionNumber
						 from dbo.icis_address_phone 
                              left join dbo.icis_phone on icis_address_phone.phone_id = icis_phone.phone_id
                         where address_id = xref_activity_address.address_id
						 for xml path('Telephone'), type) 
*/
				,  (select electronic_address_text from icis_address_electronic_addr where address_id = xref_activity_address.address_id) as  ElectronicAddressText   --,  icis_contact_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
				,  convert(varchar(10), cast(xref_activity_address.begin_date  as datetime) , 126) as   StartDateOfAddressAssociation -- ,  xref_activity_address.begin_date , xref_prog_report_sw_address.begin_date, xref_prog_rpt_ms4_address.begin_date 
				,  convert(varchar(10), cast(xref_activity_address.end_date as datetime) , 126) as  EndDateOfAddressAssociation   --  xref_activity_address.end_date , xref_prog_report_sw_address.end_date, xref_prog_rpt_ms4_address.end_date
				from xref_activity_address left join icis_ADDRESS on xref_activity_address.ADDRESS_id = icis_ADDRESS.ADDRESS_id
				where xref_activity_address.activity_id = v_PermittedFeature.activity_id 
				for xml path('Address'), type) as SiteOwnerAddress

		from 
		(select 
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, Icis_perm_feature.perm_feature_nmbr  as PermittedFeatureIdentifier        --- Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, icis_perm_feature.perm_feature_type_code  as PermittedFeatureTypeCode        --- Required "Yes"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, '' AS PermittedFeatureCharacteristics
			--, (select characteristic_code from xref_perm_feature_character where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID) as PermittedFeatureCharacteristics        --- Required "No"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.perm_feature_text  as PermittedFeatureDescription        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' as PermittedFeatureTreatmentTypeCode
			--, (SELECT treatment_type_code FROM xref_perm_feature_treatment where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID)  as PermittedFeatureTreatmentTypeCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.design_flow_nmbr  as PermittedFeatureDesignFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.actual_average_flow_nmbr  as PermittedFeatureActualAverageFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body  as PermittedFeatureStateWaterBodyCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body_name  as PermittedFeatureStateWaterBodyName        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf1  as PermittedFeatureUserDefinedDataElement1        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf2  as PermittedFeatureUserDefinedDataElement2        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.field_size  as FieldSize        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.owned_by_facility_flag  as IsSiteOwnByFacility        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.leachate_collection_flag  as IsSystemLinedWithLeachate        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.daily_cover_flag  as DoesUnitHaveDailyCover        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.distance_to_boundary  as PropertyBoundaryDistance        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.nitrate_monitoring_flag  as IsRequiredNitrateGroundWater        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.well_nmbr  as WellNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' AS  GeographicCoordinates
			, icis_perm_feature.source_perm_fea_details  as SourcePermittedFeatureDetailText        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' AS SiteOwnerContact
			, '' AS SiteOwnerAddress
			from dbo.icis_perm_feature ) v_PermittedFeature 
		where Perm_Feature_ID = a.Perm_Feature_ID
		for xml path('PermittedFeature'), type
		) 
		from 
		(select 
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, Icis_perm_feature.perm_feature_nmbr  as PermittedFeatureIdentifier        --- Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, icis_perm_feature.perm_feature_type_code  as PermittedFeatureTypeCode        --- Required "Yes"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, '' AS PermittedFeatureCharacteristics
			--, (select characteristic_code from xref_perm_feature_character where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID) as PermittedFeatureCharacteristics        --- Required "No"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.perm_feature_text  as PermittedFeatureDescription        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' as PermittedFeatureTreatmentTypeCode
			--, (SELECT treatment_type_code FROM xref_perm_feature_treatment where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID)  as PermittedFeatureTreatmentTypeCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.design_flow_nmbr  as PermittedFeatureDesignFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.actual_average_flow_nmbr  as PermittedFeatureActualAverageFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body  as PermittedFeatureStateWaterBodyCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body_name  as PermittedFeatureStateWaterBodyName        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf1  as PermittedFeatureUserDefinedDataElement1        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf2  as PermittedFeatureUserDefinedDataElement2        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.field_size  as FieldSize        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.owned_by_facility_flag  as IsSiteOwnByFacility        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.leachate_collection_flag  as IsSystemLinedWithLeachate        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.daily_cover_flag  as DoesUnitHaveDailyCover        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.distance_to_boundary  as PropertyBoundaryDistance        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.nitrate_monitoring_flag  as IsRequiredNitrateGroundWater        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.well_nmbr  as WellNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.source_perm_fea_details  as SourcePermittedFeatureDetailText        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			from dbo.icis_perm_feature ) a
		WHERE a.perm_feature_id = b.perm_feature_id
--		for xml path ('PermittedFeatureData')
		for xml path ('')
	)   as instance_xml
	,(
	select 
	(select  'D' as TransactionType , convert(varchar(30), getdate(), 126)  as TransactionTimestamp
	for xml path('TransactionHeader'), type)	
	, (
		select 
		 PermitIdentifier
		, PermittedFeatureIdentifier
		, PermittedFeatureTypeCode
		from 
		(select 
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, Icis_perm_feature.perm_feature_nmbr  as PermittedFeatureIdentifier        --- Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, icis_perm_feature.perm_feature_type_code  as PermittedFeatureTypeCode        --- Required "Yes"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			from dbo.icis_perm_feature ) v_PermittedFeature
		where Perm_Feature_ID = a.Perm_Feature_ID
		for xml path('PermittedFeature'), type
	) 
	from 
	(select 
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, Icis_perm_feature.perm_feature_nmbr  as PermittedFeatureIdentifier        --- Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, icis_perm_feature.perm_feature_type_code  as PermittedFeatureTypeCode        --- Required "Yes"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, '' AS PermittedFeatureCharacteristics
			--, (select characteristic_code from xref_perm_feature_character where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID) as PermittedFeatureCharacteristics        --- Required "No"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.perm_feature_text  as PermittedFeatureDescription        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' as PermittedFeatureTreatmentTypeCode
			--, (SELECT treatment_type_code FROM xref_perm_feature_treatment where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID)  as PermittedFeatureTreatmentTypeCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.design_flow_nmbr  as PermittedFeatureDesignFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.actual_average_flow_nmbr  as PermittedFeatureActualAverageFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body  as PermittedFeatureStateWaterBodyCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body_name  as PermittedFeatureStateWaterBodyName        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf1  as PermittedFeatureUserDefinedDataElement1        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf2  as PermittedFeatureUserDefinedDataElement2        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.field_size  as FieldSize        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.owned_by_facility_flag  as IsSiteOwnByFacility        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.leachate_collection_flag  as IsSystemLinedWithLeachate        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.daily_cover_flag  as DoesUnitHaveDailyCover        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.distance_to_boundary  as PropertyBoundaryDistance        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.nitrate_monitoring_flag  as IsRequiredNitrateGroundWater        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.well_nmbr  as WellNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' AS  GeographicCoordinates
			, icis_perm_feature.source_perm_fea_details  as SourcePermittedFeatureDetailText        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' AS SiteOwnerContact
			, '' AS SiteOwnerAddress
			from dbo.icis_perm_feature ) a
	WHERE a.Perm_Feature_ID = b.Perm_Feature_ID
--	for xml path ('PermittedFeatureData')
	for xml path ('')
	) as del_instance_xml
from 
(select 
			Icis_perm_feature.activity_id
			, Icis_perm_feature.perm_feature_id
			,(select EXTERNAL_PERMIT_NMBR from icis_permit where activity_id = Icis_perm_feature.activity_id ) as  PermitIdentifier
			, Icis_perm_feature.perm_feature_nmbr  as PermittedFeatureIdentifier        --- Required "Yes"  Repeatable No -- Common Parent : CSOEventReport CSOInspection DischargeMonitoringReport DMRViolation DMRProgramReportLinkage EffluentTradePartner DischargeMonitoringReportViolation LimitSet Limits, ParameterLimits PermittedFeature Di
			, icis_perm_feature.perm_feature_type_code  as PermittedFeatureTypeCode        --- Required "Yes"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, '' AS PermittedFeatureCharacteristics
			--, (select characteristic_code from xref_perm_feature_character where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID) as PermittedFeatureCharacteristics        --- Required "No"  Repeatable Yes -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.perm_feature_text  as PermittedFeatureDescription        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' as PermittedFeatureTreatmentTypeCode
			--, (SELECT treatment_type_code FROM xref_perm_feature_treatment where PERM_FEATURE_ID = Icis_perm_feature.PERM_FEATURE_ID)  as PermittedFeatureTreatmentTypeCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.design_flow_nmbr  as PermittedFeatureDesignFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.actual_average_flow_nmbr  as PermittedFeatureActualAverageFlowNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body  as PermittedFeatureStateWaterBodyCode        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.state_water_body_name  as PermittedFeatureStateWaterBodyName        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf1  as PermittedFeatureUserDefinedDataElement1        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.udf2  as PermittedFeatureUserDefinedDataElement2        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.field_size  as FieldSize        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.owned_by_facility_flag  as IsSiteOwnByFacility        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.leachate_collection_flag  as IsSystemLinedWithLeachate        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.daily_cover_flag  as DoesUnitHaveDailyCover        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.distance_to_boundary  as PropertyBoundaryDistance        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.nitrate_monitoring_flag  as IsRequiredNitrateGroundWater        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, icis_perm_feature.well_nmbr  as WellNumber        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' AS  GeographicCoordinates
			, icis_perm_feature.source_perm_fea_details  as SourcePermittedFeatureDetailText        --- Required "No"  Repeatable No -- Permitted Feature Parent : PermittedFeature
			, '' AS SiteOwnerContact
			, '' AS SiteOwnerAddress
			from dbo.icis_perm_feature ) b
--WHERE a.ACTIVITY_ID IN ('1200039357')
GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_BasicPermit]    Script Date: 04/10/2012 15:03:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_BasicPermit]
( @TransactionType varchar(100) =  null, @Activity_id varchar(100) =null,   @output varchar(max) output)
as


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
    declare @Activity_CD varchar(10)



/********************
v_xml_BasicPermit
*********************/

set @output = ''
if exists(select * from v_xml_BasicPermit  )
   begin
	if @Activity_id is not null
		begin
			select  * into #tmp_activity from dbo.split (@activity_id, ',')
				if exists(select * from v_xml_BasicPermit where activity_id in (select data as activity_id from #tmp_activity) )
					begin
							declare  submit_cur cursor for 
							select Activity_id , '<Payload Operation="BasicPermitSubmission"><BasicPermitData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_BasicPermit.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</BasicPermitData></Payload>'  as  instance_xml from dbo.v_xml_BasicPermit 
							   where activity_id in (select data as activity_id from #tmp_activity)
	                end
                else 
                    goto case1
		end	    
	else
		begin 
			  declare  submit_cur cursor for 
							select Activity_id , '<Payload Operation="BasicPermitSubmission"><BasicPermitData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_BasicPermit.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</BasicPermitData></Payload>'  as  instance_xml from dbo.v_xml_BasicPermit 
                            where instance_xml is not null
		end

       begin 
		            
					set @submit_str = '' ;
					set @instance_str = '' ;

						open submit_cur

						  fetch next from submit_cur
							into @Activity_id , @instance_str 


							   WHILE @@FETCH_STATUS = 0
								begin  
								   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'')) 
								
								  fetch next from submit_cur
									into @Activity_id , @instance_str 
								end
						CLOSE submit_cur;
						DEALLOCATE submit_cur;
      end  

   end
else 
			begin      
					   set @submit_str = ''
			end


	set @output = @output + @submit_str  
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''
	set @output = @output + @submit_str  
   end


end
GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_Limits]    Script Date: 04/10/2012 15:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_Limits]
(@TransactionType varchar(100) = null , @Activity_id varchar(100) =null  , @output varchar(max) output)
as


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
	declare @Activity_CD varchar(10)


/********************
v_xml_Limits
*********************/

--select * from v_xml_Limits where activity_id =3

set @output = ''
if exists(select * from v_xml_Limits  )
   begin
	if @Activity_id is not null
		begin
			select  * into #tmp_activity from dbo.split (@activity_id, ',')
				if exists(select * from v_xml_Limits where activity_id in (select data as activity_id from #tmp_activity) )
					begin
							declare  submit_cur cursor for 
										select Activity_id , '<Payload Operation="LimitsSubmission"><LimitsData><TransactionHeader><TransactionType>'+ (select  isnull(isnull(@TransactionType, replace(ACTIVITY_TYPE_CODE,'R','C')), 'C')  from icis_activity where activity_id = v_xml_Limits.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</LimitsData></Payload>'  as  instance_xml 
										from dbo.v_xml_Limits 
										where activity_id in (select data as activity_id from #tmp_activity)
                    end
                else 
                    goto case1
		end	    
	else
		begin 
			  declare  submit_cur cursor for 
									select Activity_id , '<Payload Operation="LimitsSubmission"><LimitsData><TransactionHeader><TransactionType>'+ (select isnull(isnull(@TransactionType, replace(ACTIVITY_TYPE_CODE,'R','C')), 'C') from icis_activity where activity_id = v_xml_Limits.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</LimitsData></Payload>'  as  instance_xml 
									from dbo.v_xml_Limits 
                                    where instance_xml is not null
		end

       begin 
		            
					set @submit_str = '' ;
					set @instance_str = '' ;

								open submit_cur

								  fetch next from submit_cur
									into @Activity_id , @instance_str 


									   WHILE @@FETCH_STATUS = 0
										begin  
										   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'')) 
										
										  fetch next from submit_cur
											into @Activity_id , @instance_str 
										end
								CLOSE submit_cur;
								DEALLOCATE submit_cur;
      end  

   end
else 
			begin      
					   set @submit_str = ''
			end


	set @output = @output + @submit_str  
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''
	set @output = @output + @submit_str  
   end


end
GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_LimitSet]    Script Date: 04/10/2012 15:03:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_LimitSet]
(@TransactionType varchar(100) = null  , @Activity_id varchar(100) =null  , @output varchar(max) output)
as



begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
--    declare @output varchar(max) ;
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
    declare @Activity_CD varchar(10)


set @output = ''
/********************
v_xml_LimitSet
*********************/
if exists(select * from v_xml_LimitSet  )
   begin
		if @Activity_id is not null
			begin
					select  * into #tmp_activity from dbo.split (@activity_id, ',')
					if exists(select * from v_xml_LimitSet where activity_id in (select data as activity_id from #tmp_activity) )
						begin
							declare  submit_cur cursor for 
								select Activity_id , '<Payload Operation="LimitSetSubmission"><LimitSetData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_LimitSet.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</LimitSetData></Payload>'  as  instance_xml from dbo.v_xml_LimitSet 
								   where activity_id in (select data as activity_id from #tmp_activity)
						end
					else 
						GOTO  case1;
			end	    
		else
			begin 
					declare  submit_cur cursor for 
								select Activity_id , '<Payload Operation="LimitSetSubmission"><LimitSetData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_LimitSet.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</LimitSetData></Payload>'  as  instance_xml from dbo.v_xml_LimitSet 
								where instance_xml is not null
			end
   



				
				set @submit_str = '' ;
				set @instance_str = '' ;

							open submit_cur

							  fetch next from submit_cur
								into @Activity_id , @instance_str 


								   WHILE @@FETCH_STATUS = 0
									begin  
									   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'')) 
									
									  fetch next from submit_cur
										into @Activity_id , @instance_str 
									end
							CLOSE submit_cur;
							DEALLOCATE submit_cur;



        end
	else 
			begin      
					   set @submit_str = ''
			end



	set @output = @output + @submit_str  
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''
	set @output = @output + @submit_str  
   end


end
GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_GeneralPermit]    Script Date: 04/10/2012 15:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_GeneralPermit]
( @TransactionType varchar(100) =  null, @Activity_id varchar(100) =null,   @output varchar(max) output)
as


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
    declare @Activity_CD varchar(10)



/********************
v_xml_GeneralPermit
*********************/

set @output = ''
if exists(select * from v_xml_GeneralPermit  )
   begin
	if @Activity_id is not null
		begin
			select  * into #tmp_activity from dbo.split (@activity_id, ',')
				if exists(select * from v_xml_GeneralPermit where activity_id in (select data as activity_id from #tmp_activity) )
					begin
							declare  submit_cur cursor for 
							select Activity_id , '<Payload Operation="GeneralPermitSubmission"><GeneralPermitData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_GeneralPermit.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</GeneralPermitData></Payload>'  as  instance_xml from dbo.v_xml_GeneralPermit 
							   where activity_id in (select data as activity_id from #tmp_activity)
	                end
                else 
                    goto case1
		end	    
	else
		begin 
			  declare  submit_cur cursor for 
							select Activity_id , '<Payload Operation="GeneralPermitSubmission"><GeneralPermitData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_GeneralPermit.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</GeneralPermitData></Payload>'  as  instance_xml from dbo.v_xml_GeneralPermit 
                            where instance_xml is not null
		end

       begin 
		            
					set @submit_str = '' ;
					set @instance_str = '' ;

						open submit_cur

						  fetch next from submit_cur
							into @Activity_id , @instance_str 


							   WHILE @@FETCH_STATUS = 0
								begin  
								   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'')) 
								
								  fetch next from submit_cur
									into @Activity_id , @instance_str 
								end
						CLOSE submit_cur;
						DEALLOCATE submit_cur;
      end  

   end
else 
			begin      
					   set @submit_str = ''
			end


	set @output = @output + @submit_str  
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''
	set @output = @output + @submit_str  
   end


end
GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_PermittedFeature]    Script Date: 04/10/2012 15:03:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_PermittedFeature]
(@TransactionType varchar(100) = null , @Activity_id varchar(100) =null  , @output varchar(max) output)
as



begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
--    declare @output varchar(max) ;
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
    declare @Activity_CD varchar(10)


/********************
v_xml_PermittedFeature
*********************/

set @output = ''
if exists(select * from v_xml_PermittedFeature  )
   begin
	if @Activity_id is not null
		begin
			select  * into #tmp_activity from dbo.split (@activity_id, ',')
				if exists(select * from v_xml_PermittedFeature where activity_id in (select data as activity_id from #tmp_activity) )
					begin
							declare  submit_cur cursor for 
							select Activity_id , perm_feature_id, '<Payload Operation="PermittedFeatureSubmission"><PermittedFeatureData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_PermittedFeature.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</PermittedFeatureData></Payload>'  as  instance_xml 
                            from dbo.v_xml_PermittedFeature 
							   where activity_id in (select data as activity_id from #tmp_activity)   
	                end
                else 
                    goto case1
		end	    
	else
		begin 
			  declare  submit_cur cursor for 
							select Activity_id , perm_feature_id, '<Payload Operation="PermittedFeatureSubmission"><PermittedFeatureData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_PermittedFeature.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</PermittedFeatureData></Payload>'  as  instance_xml 
                            from dbo.v_xml_PermittedFeature 
                                    where instance_xml is not null and perm_feature_id is not null
		end

       begin 
		            
					set @submit_str = '' ;
					set @instance_str = '' ;

						open submit_cur

						  fetch next from submit_cur
							into @Activity_id , @perm_feature_id, @instance_str 


							   WHILE @@FETCH_STATUS = 0
								begin  
								   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'')) 
								
								  fetch next from submit_cur
									into @Activity_id , @perm_feature_id, @instance_str 
								end
						CLOSE submit_cur;
						DEALLOCATE submit_cur;
      end  

   end
else 
			begin      
					   set @submit_str = ''
			end


	set @output = @output + @submit_str  
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''
	set @output = @output + @submit_str  
   end


end





-------------------------------------------------------------------------


GO
/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_MasterGeneralPermit]    Script Date: 04/10/2012 15:03:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_MasterGeneralPermit]
(  @TransactionType varchar(100) = null, @Activity_id varchar(100) =null,  @output varchar(max) output)
as


begin
	declare @submit_str varchar(max),   @instance_str varchar(max)
	declare @sqlcmd varchar(2000);
    declare @perm_feature_id int
    declare @Activity_CD varchar(10)



/********************
v_xml_MasterGeneralPermit
*********************/
set @output = ''
if exists(select * from v_xml_MasterGeneralPermit )
   begin
	if @Activity_id is not null
		begin
			select  * into #tmp_activity from dbo.split (@activity_id, ',')
				if exists(select * from v_xml_MasterGeneralPermit where activity_id in (select data as activity_id from #tmp_activity) )
					begin
						declare  submit_cur cursor for 
									select Activity_id , '<Payload Operation="MasterGeneralPermitSubmission"><MasterGeneralPermitData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_MasterGeneralPermit.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</MasterGeneralPermitData></Payload>'  as  instance_xml from dbo.v_xml_MasterGeneralPermit 
									   where activity_id in (select data as activity_id from #tmp_activity)
                    end
                else 
                    goto case1
		end	    
	else
		begin 
			  declare  submit_cur cursor for 
							select Activity_id , '<Payload Operation="MasterGeneralPermitSubmission"><MasterGeneralPermitData><TransactionHeader><TransactionType>'+ (select   isnull(isnull(@TransactionType, ACTIVITY_TYPE_CODE), 'R')  from icis_activity where activity_id = v_xml_MasterGeneralPermit.activity_id) + '</TransactionType><TransactionTimestamp>' + convert(varchar(20), getdate(), 126)  + '0Z' +'</TransactionTimestamp></TransactionHeader>' + cast(instance_xml as varchar(max)) +  '</MasterGeneralPermitData></Payload>'  as  instance_xml from dbo.v_xml_MasterGeneralPermit 
                                    where instance_xml is not null
		end

       begin 
		            
					set @submit_str = '' ;
					set @instance_str = '' ;

								open submit_cur

								  fetch next from submit_cur
									into @Activity_id , @instance_str 


									   WHILE @@FETCH_STATUS = 0
										begin  
										   set @submit_str = rtrim(@submit_str) + rtrim(isnull(@instance_str,'')) 
										
										  fetch next from submit_cur
											into @Activity_id , @instance_str 
										end
								CLOSE submit_cur;
								DEALLOCATE submit_cur;
      end  

   end
else 
			begin      
					   set @submit_str = ''
			end


	set @output = @output + @submit_str  
--	select cast(@output as xml)


case1: 
   begin 	
	set @submit_str = ''
	set @output = @output + @submit_str  
   end


end
GO





/****** Object:  StoredProcedure [dbo].[ICIS_Permit_replace_call_sp]    Script Date: 04/10/2012 15:56:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[ICIS_Permit_replace_call_sp]
(
  @Submit_All_Ind  char(1) = 'Y'
, @Activity_id varchar(100) =null
, @MasterGeneralPermit_TransType char(1) =null
, @GeneralPermit_TransType char(1) =null
, @BasicPermit_TransType char(1) =null
, @PermittedFeature_TransType char(1) =null
, @LimitSet_TransType char(1) =null
, @Limits_TransType char(1) =null
, @Out varchar(max) output
)
/****************************************************************************************
exec dbo.[ICIS_Permit_replace_call_sp] '2,3,7,8,10'
default @Submit_All_Ind = 'N'
        only submit the payload with asigned value on the indicate, but could be multiple payload
        This Assigned value will overright the Actyvity_type_cd in ICIS_ACTIVITY.
        if only assign @Activity_id (deliminated by ',') 
            it will collect all payload related to the @Activity_ids  

when @Submit_All_Ind = 'Y' will collect all payload in the tables, like wildcard

*******************************************************************************************/
as
begin
---  select * from icis_activity
		declare   @output_header varchar(max), @output_MasterGeneralPermit varchar(max), @output_BasicPermit varchar(max),  @output_GeneralPermit varchar(max)
		declare @output_PermittedFeature varchar(max), @output_LimitSet varchar(max), @output_Limits varchar(max)
		declare  @TransactionType varchar(100) -- , @Activity_id varchar(100),@Out varchar(max)

		--set @TransactionType = 'R'  
		--set @Activity_id = null

if @SUBMIT_ALL_IND = ''  set @SUBMIT_ALL_IND ='Y'
if @Activity_id = ''  set @Activity_id =null
if @MasterGeneralPermit_TransType = ''  set @MasterGeneralPermit_TransType =null
if @GeneralPermit_TransType = ''  set @GeneralPermit_TransType =null
if @BasicPermit_TransType = ''  set @BasicPermit_TransType =null
if @PermittedFeature_TransType = ''  set @PermittedFeature_TransType =null
if @LimitSet_TransType = ''  set @LimitSet_TransType =null
if @Limits_TransType = ''  set @Limits_TransType =null


if @Submit_All_Ind not in  ( 'N', 'Y') select '@Submit_All_Ind values not in ( ''Y'', ''N'')'
if @MasterGeneralPermit_TransType not in  ( 'N', 'R', 'D') select '@MasterGeneralPermit_TransType values not in ( ''N'', ''R'', ''D'')'
if @GeneralPermit_TransType not in  ( 'N', 'R', 'D') select '@GeneralPermit_TransType values not in ( ''N'', ''R'', ''D'')'
if @BasicPermit_TransType not in  ('N', 'R', 'D') select '@BasicPermit_TransType values not in ( ''N'', ''R'', ''D'')'
if @PermittedFeature_TransType not in  ( 'N', 'R', 'D') select '@PermittedFeature_TransType values not in ( ''N'', ''R'', ''D'')'
if @LimitSet_TransType not in  ( 'N', 'R', 'D') select '@LimitSet_TransType values not in ( ''N'', ''R'', ''D'')'
if @Limits_TransType not in  ( 'N', 'C') select '@Limits_TransType values not in ( ''N'', ''C'')'


       set @output_header =''
       set @output_MasterGeneralPermit =''
       set @output_BasicPermit =''
       set @output_GeneralPermit =''
       set @output_PermittedFeature =''
       set @output_LimitSet =''
       set @output_Limits =''


if ((@Submit_All_Ind ='N')  )
	begin
		if (len( isnull(@MasterGeneralPermit_TransType,'') + isnull(@BasicPermit_TransType,'') + isnull(@GeneralPermit_TransType,'') +
			isnull(@PermittedFeature_TransType,'') + isnull(@LimitSet_TransType,'') + isnull(@Limits_TransType,'')) > 0)  
					begin
							exec  [ICIS_Permit_replace_header]   @output_header output

						 if (@MasterGeneralPermit_TransType is not null)
							begin
								exec  [ICIS_Permit_replace_MasterGeneralPermit]  @MasterGeneralPermit_TransType ,@Activity_id, @output_MasterGeneralPermit output
							end

						 if (@BasicPermit_TransType is not null)
							begin
								exec  [ICIS_Permit_replace_BasicPermit] @BasicPermit_TransType ,@Activity_id, @output_BasicPermit output
							end 
			
						 if (@GeneralPermit_TransType is not null)
							begin
								exec  [ICIS_Permit_replace_GeneralPermit] @GeneralPermit_TransType ,@Activity_id, @output_GeneralPermit output
							end
			
						 if (@PermittedFeature_TransType is not null)
							begin
								exec  [ICIS_Permit_replace_PermittedFeature] @PermittedFeature_TransType ,@Activity_id, @output_PermittedFeature output
							end

						 if (@LimitSet_TransType is not null)
							begin
								exec  [ICIS_Permit_replace_LimitSet] @LimitSet_TransType ,@Activity_id, @output_LimitSet output
							end 
			 
						if (@Limits_TransType is not null)
							begin
								exec  [ICIS_Permit_replace_Limits] @Limits_TransType ,@Activity_id, @output_Limits output
							end 

							if ((len(isnull(@output_MasterGeneralPermit,'')) + len(isnull(@output_BasicPermit,'')) + len(isnull(@output_GeneralPermit,'')) + len(isnull(@output_PermittedFeature,'')) + len(isnull(@output_LimitSet,'')) + len(isnull(@output_Limits,''))) >0)
							begin
							set @Out = (isnull(rtrim(@output_header),'') + 
										isnull(rtrim(@output_MasterGeneralPermit),'') + 
										isnull(rtrim(@output_BasicPermit ),'') +   
										isnull(rtrim(@output_GeneralPermit ),'') + 
										isnull(rtrim(@output_PermittedFeature ),'') +  
										isnull(rtrim(@output_LimitSet ),'') +  
										isnull(rtrim(@output_Limits),'') ) + '</Document>'
							end  
					        
							set @Out = isnull(@Out,'')
	--						select cast(@Out as xml)
				   end 
		else 

              if (@Activity_id is null)
				   begin
					  set @Out = ''
	--				  select cast(@Out as xml)
				   end 
              else 
						begin
								exec  [ICIS_Permit_replace_header]   @output_header output

								exec  [ICIS_Permit_replace_MasterGeneralPermit]  @MasterGeneralPermit_TransType ,@Activity_id, @output_MasterGeneralPermit output

								exec  [ICIS_Permit_replace_BasicPermit] @BasicPermit_TransType ,@Activity_id, @output_BasicPermit output

								exec  [ICIS_Permit_replace_GeneralPermit] @GeneralPermit_TransType ,@Activity_id, @output_GeneralPermit output

								exec  [ICIS_Permit_replace_PermittedFeature] @PermittedFeature_TransType ,@Activity_id, @output_PermittedFeature output

								exec  [ICIS_Permit_replace_LimitSet] @LimitSet_TransType ,@Activity_id, @output_LimitSet output

								exec  [ICIS_Permit_replace_Limits] @Limits_TransType ,@Activity_id, @output_Limits output


								if ((len(isnull(@output_MasterGeneralPermit,'')) + len(isnull(@output_BasicPermit,'')) + len(isnull(@output_GeneralPermit,'')) + len(isnull(@output_PermittedFeature,'')) + len(isnull(@output_LimitSet,'')) + len(isnull(@output_Limits,''))) >0)
								begin
								set @Out = (isnull(rtrim(@output_header),'') + 
											isnull(rtrim(@output_MasterGeneralPermit),'') + 
											isnull(rtrim(@output_BasicPermit ),'') +   
											isnull(rtrim(@output_GeneralPermit ),'') + 
											isnull(rtrim(@output_PermittedFeature ),'') +  
											isnull(rtrim(@output_LimitSet ),'') +  
											isnull(rtrim(@output_Limits),'') ) + '</Document>'
								end  
								set @Out = isnull(@Out,'')
	--							select cast(@Out as xml)
					   end 
    end
					
else if ((@Submit_All_Ind ='Y') )

		begin
				exec  [ICIS_Permit_replace_header]   @output_header output

				exec  [ICIS_Permit_replace_MasterGeneralPermit]  @MasterGeneralPermit_TransType ,@Activity_id, @output_MasterGeneralPermit output

				exec  [ICIS_Permit_replace_BasicPermit] @BasicPermit_TransType ,@Activity_id, @output_BasicPermit output

				exec  [ICIS_Permit_replace_GeneralPermit] @GeneralPermit_TransType ,@Activity_id, @output_GeneralPermit output

				exec  [ICIS_Permit_replace_PermittedFeature] @PermittedFeature_TransType ,@Activity_id, @output_PermittedFeature output

				exec  [ICIS_Permit_replace_LimitSet] @LimitSet_TransType ,@Activity_id, @output_LimitSet output

				exec  [ICIS_Permit_replace_Limits] @Limits_TransType ,@Activity_id, @output_Limits output


				if ((len(isnull(@output_MasterGeneralPermit,'')) + len(isnull(@output_BasicPermit,'')) + len(isnull(@output_GeneralPermit,'')) + len(isnull(@output_PermittedFeature,'')) + len(isnull(@output_LimitSet,'')) + len(isnull(@output_Limits,''))) >0)
				begin
				set @Out = (isnull(rtrim(@output_header),'') + 
							isnull(rtrim(@output_MasterGeneralPermit),'') + 
							isnull(rtrim(@output_BasicPermit ),'') +   
							isnull(rtrim(@output_GeneralPermit ),'') + 
							isnull(rtrim(@output_PermittedFeature ),'') +  
							isnull(rtrim(@output_LimitSet ),'') +  
							isnull(rtrim(@output_Limits),'') ) + '</Document>'
				end  
				set @Out = isnull(@Out,'')
--		        select cast(@Out as xml)
	   end

end