USE [ICIS_STG_S]
/****** Object:  StoredProcedure [dbo].[icis_Replace_UnpermittedFacility]    Script Date: 01/19/2012 11:01:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE procedure [dbo].[icis_Replace_UnpermittedFacility]
( @start_date datetime =null , @output varchar(max) out)
as

-- icis_Replace_UnpermittedFacility @start_date=null, @output=NULL

declare @Activity_id varchar(4000), @FACILITY_INTEREST_ID varchar(4000), @xml_file xml --, @output varchar(max)

declare     @NAAS_ID varchar(100),     @Author varchar(100),     @Organization varchar(100),     @ContactInfo varchar(100), 	@Email_Address  varchar(100)


set @NAAS_ID = 'XXX'
set @Author = 'XXX'
set @Organization = 'XXX'
set @ContactInfo = 'XXX Strret, XXX City, XXX State XXX Zip'
set @Email_Address = 'XXX@address.com'


if len(@start_date) =0
	begin
		set  @start_date =null
	end

        select @xml_file = (
		select --top 20
		(select 'R' as TransactionType, getdate() as TransactionTimestamp for xml path('TransactionHeader'), type)
		,(
		select 
		 v_UnpermittedFacility.PermitIdentifier
		, v_UnpermittedFacility.FacilitySiteName
		, v_UnpermittedFacility.LocationAddressText
		, v_UnpermittedFacility.SupplementalLocationText
		, v_UnpermittedFacility.LocalityName
		, v_UnpermittedFacility.LocationStateCode
		, v_UnpermittedFacility.LocationZipCode
		, CASE WHEN LEN(ISNULL(v_UnpermittedFacility.LocationCountryCode,'')) =0 THEN 'US' ELSE UPPER(LocationCountryCode) END AS LocationCountryCode
		, v_UnpermittedFacility.OrganizationDUNSNumber
		, v_UnpermittedFacility.StateFacilityIdentifier
		, v_UnpermittedFacility.StateRegionCode
		, v_UnpermittedFacility.FacilityCongressionalDistrictNumber
		, v_UnpermittedFacility.FacilitySmallBusinessIndicator
		, v_UnpermittedFacility.FacilityTypeofOwnershipCode
		, v_UnpermittedFacility.FederalFacilityIdentificationNumber
		, v_UnpermittedFacility.FederalAgencyCode
		, v_UnpermittedFacility.FacilityEnvironmentalJusticeCode
		, v_UnpermittedFacility.TribalLandCode
		, v_UnpermittedFacility.ConstructionProjectName
		, v_UnpermittedFacility.ConstructionProjectLatitudeMeasure
		, v_UnpermittedFacility.ConstructionProjectLongitudeMeasure
--		, '' as SICCodeDetails
--		, '' as NAICSCodeDetails
		, (select distinct SIC_Code AS SICCode,
	        PRIMARY_INDICATOR_FLAG AS SICPrimaryIndicatorCode
			from dbo.XREF_FACILITY_INTEREST_SIC
			where ICIS_FACILITY_INTEREST_ID = a.ICIS_FACILITY_INTEREST_ID
			FOR XML PATH('SICCodeDetails'), TYPE) --as SICCodeDetails
		, (select distinct NAICS_Code AS NAICSCode,
	        PRIMARY_INDICATOR_FLAG AS NAICSPrimaryIndicatorCode
			from dbo.XREF_FACILITY_INTEREST_NAICS
			where ICIS_FACILITY_INTEREST_ID = a.ICIS_FACILITY_INTEREST_ID
			FOR XML PATH('NAICSCodeDetails'), TYPE) --as SICCodeDetails
		, v_UnpermittedFacility.SectionTownshipRange
		, v_UnpermittedFacility.FacilityComments
		, v_UnpermittedFacility.FacilityUserDefinedField1
		, v_UnpermittedFacility.FacilityUserDefinedField2
		, v_UnpermittedFacility.FacilityUserDefinedField3
		, v_UnpermittedFacility.FacilityUserDefinedField4
		, v_UnpermittedFacility.FacilityUserDefinedField5
--		, '' as FacilityContact
		,(select 
			v_Contact.AffiliationTypeText,
			v_Contact.FirstName,
			v_Contact.MiddleName,
			v_Contact.LastName,
			v_Contact.IndividualTitleText,
			v_Contact.OrganizationFormalName,
			v_Contact.StateCode,
			v_Contact.RegionCode,
--			NULL as Telephone,
			(select case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
					case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) =0  then null else replace(replace(icis_phone.telephone_nmbr,'-',''),' ','') end as TelephoneNumber,
					case when len(replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','') end as TelephoneExtensionNumber
				from icis_phone inner join icis_contact_phone on icis_phone.phone_id = icis_contact_phone.phone_id
				where icis_contact_phone.contact_id = v_contact.contact_id
				for xml path('Telephone'),type) ,
		 	v_Contact.ElectronicAddressText,
			v_Contact.StartDateOfContactAssociation,
			v_Contact.EndDateOfContactAssociation
		from 
			(select distinct
				--XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID
				xref_activity_contact.activity_id
				, icis_contact.contact_id
				, xref_activity_contact.affiliation_type_code  as  AffiliationTypeText  --,  xref_activity_address.affiliation_type_code, xref_facility_interest_address.affiliation_type_code, xref_facility_interest_contact.affiliation_type_code, xr
				,  icis_contact.first_name as  FirstName
				,  icis_contact.middle_name as  MiddleName
				,  icis_contact.last_name as  LastName
				,  icis_contact.title as  IndividualTitleText
				,  icis_contact.organization_formal_name as  OrganizationFormalName -- , icis_address.organization_formal_name, icis_limit_trade_partner.organization_formal_name
				,  icis_contact.state_code as  StateCode
				,  icis_contact.region_code as  RegionCode
--				,  '' as Telephone
                ,  NULL AS Telephone
--				,  icis_contact_electronic_addr.electronic_address_text as  ElectronicAddressText  -- , icis_address_electronic_addr.electronic_address_text, icis_trade_partner_e_address.electronic_address_text
				, (select top 1  electronic_address_text from icis_contact_electronic_addr where contact_id = icis_contact.contact_id)  as  ElectronicAddressText
				,  convert(varchar(10), xref_activity_contact.begin_date, 120) as  StartDateOfContactAssociation  -- , xref_permit_feature_contact.begin_date, xref_prog_report_sw_contact.begin_date, xref_prog_rpt_ms4_contact.begin_date
				,  convert(varchar(10), xref_activity_contact.end_date, 120) as  EndDateOfContactAssociation  --  , xref_facility_interest_contact.end_date, xref_permit_feature_contact.end_date,  xref_prog_report_sw_contact.end_date, xref_prog_rpt_ms4_contact.end_date
				--select *
				--from dbo.XREF_FACILITY_INTEREST_CONTACT inner join xref_activity_contact on XREF_FACILITY_INTEREST_CONTACT.contact_id = xref_activity_contact.contact_id
				from dbo.XREF_ACTIVITY_FACILITY_INT  inner join xref_activity_contact on XREF_ACTIVITY_FACILITY_INT.activity_id = xref_activity_contact.activity_id
				   left join icis_contact on xref_activity_contact.contact_id = icis_contact.contact_id
				where xref_activity_contact.ACTIVITY_ID = v_UnpermittedFacility.ACTIVITY_ID and dbo.XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID = v_UnpermittedFacility.ICIS_FACILITY_INTEREST_ID
				) v_Contact
		for xml path('Contact') ,type
		) as FacilityContact
--		, '' as FacilityAddress
		,(select 
				v_Address.AffiliationTypeText,
				v_Address.OrganizationFormalName,
				v_Address.OrganizationDUNSNumber,
				v_Address.MailingAddressText,
				v_Address.SupplementalAddressText,
				v_Address.MailingAddressCityName,
				v_Address.MailingAddressStateCode,
				v_Address.MailingAddressZipCode,
				v_Address.CountyName,
				v_Address.MailingAddressCountryCode,
				v_Address.DivisionName,
				v_Address.LocationProvince,
--				 null as Telephone,
			(select case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) >0 then icis_phone.phone_type_code else null end as  TelephoneNumberTypeCode,
					case when len(replace(replace(icis_phone.telephone_nmbr,'-',''),' ','')) =0  then null else replace(replace(icis_phone.telephone_nmbr,'-',''),' ','') end as TelephoneNumber,
					case when len(replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','')) =0 then null else replace(replace(icis_phone.telephone_extension_nmbr,'-',''),' ','') end as TelephoneExtensionNumber
					from icis_phone inner join icis_address_phone on icis_phone.phone_id = icis_address_phone.phone_id
					where icis_address_phone.address_id = v_address.address_id
					for xml path('Telephone'),type)  ,
				v_Address.ElectronicAddressText,
				v_Address.StartDateOfAddressAssociation,
				v_Address.EndDateOfAddressAssociation
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
				where xref_facility_interest_address.ICIS_FACILITY_INTEREST_ID = v_UnpermittedFacility.ICIS_FACILITY_INTEREST_ID
				) v_Address
		for xml path('Address') ,type
		) as FacilityAddress
--		, '' as GeographicCoordinates
		,case when len(rtrim(a.GEOCODE_LONGITUDE)) = 0 then null else a.GEOCODE_LONGITUDE end as 'GeographicCoordinates/LatitudeMeasure'
		,case when len(rtrim(a.GEOCODE_LATITUDE)) = 0 then null else a.GEOCODE_LATITUDE end as  'GeographicCoordinates/LongitudeMeasure'
		,case when len(rtrim(a.HORIZONTAL_ACCURACY_MEASURE)) = 0 then null else a.HORIZONTAL_ACCURACY_MEASURE  end as  'GeographicCoordinates/HorizontalAccuracyMeasure'
		,case when len(rtrim(a.GEOMETRIC_TYPE_CODE)) = 0 then null else a.GEOMETRIC_TYPE_CODE  end as  'GeographicCoordinates/GeometricTypeCode'
		,case when len(rtrim(a.HORIZONTAL_COLLECT_METHOD_CODE)) = 0 then null else a.HORIZONTAL_COLLECT_METHOD_CODE  end as  'GeographicCoordinates/HorizontalCollectionMethodCode'
		,case when len(rtrim(a.HORIZONTAL_REF_DATUM_CODE)) = 0 then null else a.HORIZONTAL_REF_DATUM_CODE  end as  'GeographicCoordinates/HorizontalReferenceDatumCode'
		,case when len(rtrim(a.REFERENCE_POINT_CODE)) = 0 then null else a.REFERENCE_POINT_CODE  end as  'GeographicCoordinates/ReferencePointCode'
		,case when len(rtrim(a.SOURCE_MAP_SCALE_NMBR)) = 0 then null else a.SOURCE_MAP_SCALE_NMBR  end as  'GeographicCoordinates/SourceMapScaleNumber'
	from 
        (select 
			XREF_ACTIVITY_FACILITY_INT.Activity_id
			,icis_facility_interest.ICIS_FACILITY_INTEREST_ID
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
            where dbo.icis_facility_interest.ICIS_FACILITY_INTEREST_ID = a.ICIS_FACILITY_INTEREST_ID and dbo.XREF_ACTIVITY_FACILITY_INT.activity_id =b.activity_id
		 ) v_UnpermittedFacility 
		where v_UnpermittedFacility.ICIS_FACILITY_INTEREST_ID = a.ICIS_FACILITY_INTEREST_ID
		for xml path('UnpermittedFacility'), type
		)  -- as UnpermittedFacilityData
		from dbo.icis_facility_interest a inner join dbo.XREF_ACTIVITY_FACILITY_INT b
				 on  a.ICIS_FACILITY_INTEREST_ID = b.ICIS_FACILITY_INTEREST_ID 
                  inner join icis_activity c on b.activity_id = c.activity_id
        where c.stg_data_create_date >= (select min(stg_data_create_date) from icis_activity where stg_data_create_date >= cast(isnull(@start_date, stg_data_create_date)  as datetime))
-- for test        and a.icis_facility_interest_id = 114754

		for xml path('UnpermittedFacilityData'))


				set @output = ''
				set @output = ISNULL(@output,'') + '<Document xmlns="http://www.exchangenetwork.net/schema/icis/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">'
					set @output = @output + '<Header>'
						set @output = @output + '<Id>' + isnull(@NAAS_ID,'') + '</Id>'
						set @output = @output + '<Author>' + isnull(@Author,'') + '</Author>'
						set @output = @output + '<Organization>' + isnull(@Organization,'') + '</Organization>'
						set @output = @output + '<Title>Unpermitted Facility Submission</Title>'
						set @output = @output + '<CreationTime>' + convert(VARCHAR(50),current_timestamp,126) + '</CreationTime>'
						set @output = @output + '<DataService>ICIS-NPDES</DataService>'
						set @output = @output + '<ContactInfo>' + isnull(@ContactInfo,'') + '</ContactInfo>'
						set @output = @output + '<Property>'
							set @output = @output + '<name>e-mail</name>'
							set @output = @output + '<value>' + isnull(@Email_Address,'') + '</value>'
						set @output = @output + '</Property>'
						set @output = @output + '<Property>'
							set @output = @output + '<name>Source</name>'
							set @output = @output + '<value>NetDMR</value>'
						set @output = @output + '</Property>'
					set @output = @output + '</Header>'
					set @output = @output + '<Payload Operation="UnpermittedFacilitySubmission">'


set @output = @output + cast( @xml_file as varchar(max))  + '</Payload></Document>'

--select cast(@output as xml)
GO