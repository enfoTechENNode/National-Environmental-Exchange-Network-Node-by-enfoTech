USE [master]
GO
/****** Object:  Database [MFL_STG]    Script Date: 12/22/2011 15:12:12 ******/
CREATE DATABASE [MFL_STG] ON  PRIMARY 
( NAME = N'MFL_STG', FILENAME = N'G:\MSSQL\DATA\MFL_STG.mdf' , SIZE = 102400KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MFL_STG_log', FILENAME = N'G:\MSSQL\DATA\MFL_STG_log.ldf' , SIZE = 14272KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MFL_STG].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MFL_STG] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [MFL_STG] SET ANSI_NULLS OFF
GO
ALTER DATABASE [MFL_STG] SET ANSI_PADDING OFF
GO
ALTER DATABASE [MFL_STG] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [MFL_STG] SET ARITHABORT OFF
GO
ALTER DATABASE [MFL_STG] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [MFL_STG] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [MFL_STG] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [MFL_STG] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [MFL_STG] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [MFL_STG] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [MFL_STG] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [MFL_STG] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [MFL_STG] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [MFL_STG] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [MFL_STG] SET  DISABLE_BROKER
GO
ALTER DATABASE [MFL_STG] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [MFL_STG] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [MFL_STG] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [MFL_STG] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [MFL_STG] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [MFL_STG] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [MFL_STG] SET  READ_WRITE
GO
ALTER DATABASE [MFL_STG] SET RECOVERY SIMPLE
GO
ALTER DATABASE [MFL_STG] SET  MULTI_USER
GO
ALTER DATABASE [MFL_STG] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [MFL_STG] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'MFL_STG', N'ON'
GO
USE [MFL_STG]
GO
/****** Object:  User [MFL_STG]    Script Date: 12/22/2011 15:12:12 ******/
CREATE USER [MFL_STG] FOR LOGIN [MFL_STG] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ENFOTECH\lia]    Script Date: 12/22/2011 15:12:12 ******/
CREATE USER [ENFOTECH\lia] FOR LOGIN [ENFOTECH\lia] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[_REF_FACILITY_SITE_TYPE]    Script Date: 12/22/2011 15:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_REF_FACILITY_SITE_TYPE](
	[FACILITY_SITE_TYPE_RID] [int] NOT NULL,
	[FACILITY_SITE_TYPE_CD] [varchar](20) NOT NULL,
	[FACILITY_SITE_TYPE_NAME] [varchar](50) NOT NULL,
	[FACILITY_SITE_TYPE_DESC] [varchar](500) NULL,
	[STATUS_CD] [varchar](20) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL,
	[UPDATED_BY] [varchar](20) NOT NULL,
 CONSTRAINT [FACILITY_SITE_TYPE_PK] PRIMARY KEY NONCLUSTERED 
(
	[FACILITY_SITE_TYPE_RID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[_FAC_SUB_SYSTEM]    Script Date: 12/22/2011 15:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_FAC_SUB_SYSTEM](
	[SUB_SYSTEM_RID] [int] IDENTITY(1,1) NOT NULL,
	[SUB_SYSTEM_NAME] [varchar](20) NOT NULL,
	[SUB_SYSTEM_DESC] [varchar](50) NULL,
	[STATUS_CD] [varchar](20) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL,
	[UPDATED_BY] [varchar](20) NOT NULL,
	[DB_CONNECTION] [varchar](500) NULL,
	[DB_PROCEDURE] [varchar](500) NULL,
 CONSTRAINT [SUB_SYSTEM_PK] PRIMARY KEY NONCLUSTERED 
(
	[SUB_SYSTEM_RID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FAC_MASTER_FACILITY_LIST]    Script Date: 12/22/2011 15:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAC_MASTER_FACILITY_LIST](
	[FACILITY_MASTER_RID] [int] IDENTITY(45243,1) NOT NULL,
	[FACILITY_SITE_NAME] [varchar](100) NOT NULL,
	[FACILITY_SITE_TYPE_NAME] [varchar](50) NULL,
	[FEDERAL_FACILITY_IND] [varchar](20) NULL,
	[LEGISLATIVE_DISTRICT_NUM] [varchar](50) NULL,
	[HUC_CODE] [varchar](50) NULL,
	[STATUS_CD] [varchar](20) NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL,
	[UPDATED_BY] [varchar](20) NOT NULL,
	[FACILITY_LATITUDE] [varchar](50) NULL,
	[FACILITY_LONGITUDE] [varchar](50) NULL,
	[FACILITY_ELEVATION] [varchar](50) NULL,
	[SOURCE_MAP_SCALE_NUM] [varchar](50) NULL,
	[HORIZONTAL_ACCURACY_MEASURE] [varchar](50) NULL,
	[HORIZONTAL_COLLECTION_RID] [int] NULL,
	[HORIZONTAL_REF_DATUM_RID] [int] NULL,
	[GEOGRAPHIC_REFERENCE_POINT] [varchar](100) NULL,
	[GEOGRAPHIC_LOCATION_COMMENTS] [varchar](500) NULL,
	[VERTICAL_COLLECTION_RID] [int] NULL,
	[VERTICAL_ACCURACY_MEASURE] [varchar](50) NULL,
	[VERTICAL_REF_DATUM_RID] [int] NULL,
	[VERIFICATION_METHOD] [varchar](100) NULL,
	[DATA_COLLECT_DATE] [smalldatetime] NULL,
	[LOC_LOCATION_ADDRESS] [varchar](500) NULL,
	[LOC_SUPPLEMENTAL_LOCATION] [varchar](500) NULL,
	[LOC_LOCALITY_NAME] [varchar](50) NULL,
	[LOC_COUNTY_CODE] [varchar](50) NULL,
	[LOC_STATE_CODE] [varchar](50) NULL,
	[LOC_ZIP_CODE] [varchar](50) NULL,
	[LOC_TRIBAL_LAND_NAME] [varchar](50) NULL,
	[LOC_DESCRIPTION] [varchar](500) NULL,
	[MAIL_MAILING_ADDRESS] [varchar](500) NULL,
	[MAIL_SUPPLEMENTAL_ADDRESS] [varchar](500) NULL,
	[MAIL_CITY_NAME] [varchar](50) NULL,
	[MAIL_STATE_CODE] [varchar](50) NULL,
	[MAIL_ZIP_CODE] [varchar](50) NULL,
	[MAIL_COUNTRY_NAME] [varchar](50) NULL,
 CONSTRAINT [FACILITY_MASTER_PK] PRIMARY KEY NONCLUSTERED 
(
	[FACILITY_MASTER_RID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FAC_MASTER_ENV_INTEREST]    Script Date: 12/22/2011 15:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAC_MASTER_ENV_INTEREST](
	[MASTER_ENV_INTEREST_RID] [int] IDENTITY(1,1) NOT NULL,
	[FACILITY_MASTER_RID] [int] NOT NULL,
	[ENV_INTEREST_TYPE] [varchar](50) NULL,
	[ENV_INTEREST_DESC] [varchar](500) NULL,
	[ENV_INTEREST_START_DATE] [datetime] NULL,
	[ENV_INTEREST_END_DATE] [datetime] NULL,
	[STATUS_CD] [varchar](20) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL,
	[UPDATED_BY] [varchar](20) NOT NULL,
	[SUB_SYSTEM_NAME] [varchar](50) NULL,
	[ENV_INTEREST_STATUS_CD] [varchar](20) NULL,
 CONSTRAINT [MASTER_ENV_INTEREST_PK] PRIMARY KEY NONCLUSTERED 
(
	[MASTER_ENV_INTEREST_RID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FAC_MASTER_AFFILIATION]    Script Date: 12/22/2011 15:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAC_MASTER_AFFILIATION](
	[MASTER_AFFILICATION_RID] [int] IDENTITY(1,1) NOT NULL,
	[MASTER_ENV_INTEREST_RID] [int] NOT NULL,
	[AFFILIATION_TYPE] [varchar](50) NULL,
	[AFFILIATION_START_DATE] [datetime] NULL,
	[AFFILIATION_END_DATE] [datetime] NULL,
	[EMAIL_ADDRESS] [varchar](50) NULL,
	[TELEPHONE_NUM] [varchar](50) NULL,
	[PHONE_EXTENSION] [varchar](50) NULL,
	[FAX_NUMBER] [varchar](50) NULL,
	[ALTERNATE_TELEPHONE_NUM] [varchar](50) NULL,
	[INDIVIDUAL_TITLE] [varchar](50) NULL,
	[INDIVIDUAL_FULL_NAME] [varchar](50) NULL,
	[ORGANIZATION_IDENTIFIER] [varchar](100) NULL,
	[ORGANIZATION_FORMAL_NAME] [varchar](100) NULL,
	[STATUS_CD] [varchar](20) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL,
	[UPDATED_BY] [varchar](20) NOT NULL,
 CONSTRAINT [MASTER_AFFILICATION_PK] PRIMARY KEY NONCLUSTERED 
(
	[MASTER_AFFILICATION_RID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FAC_MASTER_SIC_LIST]    Script Date: 12/22/2011 15:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAC_MASTER_SIC_LIST](
	[MASTER_ENV_INTEREST_RID] [int] NOT NULL,
	[SIC_CODE] [varchar](20) NOT NULL,
	[SIC_PRIMARY_IND] [varchar](10) NULL,
	[STATUS_CD] [varchar](20) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL,
	[UPDATED_BY] [varchar](20) NOT NULL,
 CONSTRAINT [MASTER_ENV_SIC_PK] PRIMARY KEY NONCLUSTERED 
(
	[MASTER_ENV_INTEREST_RID] ASC,
	[SIC_CODE] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FAC_MASTER_NAICS_LIST]    Script Date: 12/22/2011 15:12:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FAC_MASTER_NAICS_LIST](
	[MASTER_ENV_INTEREST_RID] [int] NOT NULL,
	[NAICS_CODE] [varchar](50) NOT NULL,
	[NAICS_PRIMARY_IND] [varchar](10) NULL,
	[STATUS_CD] [varchar](20) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL,
	[CREATED_BY] [varchar](20) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL,
	[UPDATED_BY] [varchar](20) NOT NULL,
 CONSTRAINT [MASTER_ENV_NAICS_PK] PRIMARY KEY NONCLUSTERED 
(
	[NAICS_CODE] ASC,
	[MASTER_ENV_INTEREST_RID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_get_facility_profile]    Script Date: 12/22/2011 15:12:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure  [dbo].[usp_get_facility_profile] (@start_date datetime, @fp varchar(max) out)
as 
---Declare @tmp Table( xml_file varchar(max))

declare @xml_file xml
;WITH XMLNAMESPACES ('uri' as facid, 'urm' as gml)

					select  @xml_file = 
					(select 
					(select --top 1
					--master_list.FACILITY_MASTER_RID,
					(select 
					    case when len(isnull(FACILITY_MASTER_RID,'')) != 0 then  'ODEQ|ADDR' else null end as "facid:FacilitySiteIdentifier/@FacilitySiteIdentifierContext"
					  , FACILITY_MASTER_RID as "facid:FacilitySiteIdentifier" 
					  , FACILITY_SITE_NAME as "facid:FacilitySiteName"
/*20111207
--						<facid:FacilitySiteType>
							, (select isnull(FACILITY_SITE_TYPE_CD,'') from REF_FACILITY_SITE_TYPE where FACILITY_SITE_TYPE_RID = FAC_MASTER_FACILITY_LIST.FACILITY_SITE_TYPE_RID) as "facid:FacilitySiteType/facid:FacilitySiteTypeCode"
							, isnull(FACILITY_SITE_TYPE_RID,'') as "facid:FacilitySiteType/facid:FacilitySiteTypeCodeListIdentifier"
							, (select isnull(isnull(FACILITY_SITE_TYPE_DESC,FACILITY_SITE_TYPE_NAME),'')  from REF_FACILITY_SITE_TYPE where FACILITY_SITE_TYPE_RID = FAC_MASTER_FACILITY_LIST.FACILITY_SITE_TYPE_RID) as "facid:FacilitySiteType/facid:FacilitySiteTypeName"
--						</facid:FacilitySiteType>
*/
							, isnull(substring(FACILITY_SITE_TYPE_NAME, 1, 20), '') as "facid:FacilitySiteType/facid:FacilitySiteTypeCode"
							, '' as "facid:FacilitySiteType/facid:FacilitySiteTypeCodeListIdentifier"
							, isnull(FACILITY_SITE_TYPE_NAME ,'') as "facid:FacilitySiteType/facid:FacilitySiteTypeName"

					  , FEDERAL_FACILITY_IND as "facid:FederalFacilityIndicator"
					from  dbo.FAC_MASTER_FACILITY_LIST  
					where FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
					for xml path(''),root('uri'), type) as "facid:FacilitySiteIdentity"
					,(  select  
						LOC_LOCATION_ADDRESS as "facid:LocationAddressText"
					  , LOC_SUPPLEMENTAL_LOCATION as "facid:SupplementalLocationText"
					  , LOC_LOCALITY_NAME as "facid:LocalityName"
--						<facid:StateIdentity>
							, LOC_STATE_CODE as "facid:StateIdentity/facid:StateCode"
							, null as "facid:StateIdentity/facid:StateCodeListIdentifier"
							, LOC_STATE_CODE as "facid:StateIdentity/facid:StateName"
--						</facid:StateIdentity>
--                       , case when len(isnull(LOC_ZIP_CODE,'')) !=0 then 'ODEQ|ADDR' else null end as "facid:AddressPostalCode/@AddressPostalCodeContext"
					   , LOC_ZIP_CODE as "facid:AddressPostalCode"
--						<facid:CountryIdentity>
							, 'USA' as "facid:CountryIdentity/facid:CountryCode"
							, null as "facid:CountryIdentity/facid:CountryCodeListIdentifier"
							, 'United States of America' as "facid:CountryIdentity/facid:CountryName"
--						</facid:CountryIdentity>
--						<facid:CountyIdentity>
							, LOC_COUNTY_CODE as "facid:CountyIdentity/facid:CountyCode"
							, LOC_COUNTY_CODE as "facid:CountyIdentity/facid:CountyCodeListIdentifier"
							, LOC_COUNTY_CODE as "facid:CountyIdentity/facid:CountyName"
--						</facid:CountyIdentity>
						, LOC_TRIBAL_LAND_NAME as "facid:TribalLandName"
						, 'N' as "facid:TribalLandIndicator"
						, LOC_DESCRIPTION as "facid:LocationDescriptionText"
						from  dbo.FAC_MASTER_FACILITY_LIST  
						where FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
						for xml path(''),root('uri'), type) as "facid:LocationAddress"
					,(  select  
						MAIL_MAILING_ADDRESS as "facid:MailingAddressText"
					  , MAIL_SUPPLEMENTAL_ADDRESS as "facid:SupplementalAddressText"
					  , MAIL_CITY_NAME as "facid:MailingAddressCityName"
--						<facid:StateIdentity>
							, MAIL_STATE_CODE as "facid:StateIdentity/facid:StateCode"
							, null as "facid:StateIdentity/facid:StateCodeListIdentifier"
							, MAIL_STATE_CODE as "facid:StateIdentity/facid:StateName"
--						</facid:StateIdentity>
--                       , case when len(isnull(MAIL_ZIP_CODE,'')) !=0 then 'ODEQ|ADDR' else null end as "facid:AddressPostalCode/@AddressPostalCodeContext"
					   , MAIL_ZIP_CODE as "facid:AddressPostalCode"
--						<facid:CountryIdentity>
							, 'USA' as "facid:CountryIdentity/facid:CountryCode"
							, null as "facid:CountryIdentity/facid:CountryCodeListIdentifier"
							, 'United States of America' as "facid:CountryIdentity/facid:CountryName"
--						</facid:CountryIdentity>
						from  dbo.FAC_MASTER_FACILITY_LIST  
						where FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
						for xml path(''),root('uri'), type) as "facid:MailingAddress"		
					, null as "facid:CongressionalDistrictNumberCode"	
					, case when len(rtrim(master_list.LEGISLATIVE_DISTRICT_NUM)) =0 then null else rtrim(master_list.LEGISLATIVE_DISTRICT_NUM) end  as "facid:LegislativeDistrictNumberCode"	
					, case when len(rtrim(master_list.HUC_CODE)) =0 then null else rtrim(master_list.HUC_CODE) end as "facid:HUCCode"			
					
					-------------------------------------------------------------------------
--					select * from dbo.tmp_FRS_Mapping where element like '%pos%'
--					select * from dbo.tmp_FRS_Mapping where table_name like '%MASTER_FACILITY_LIST%' order by element
/*
					
					,(  select  
						FACILITY_ELEVATION as "gml:Point/gml:pos/@srsName"
					  , null as "gml:Point/gml:pos/@srsDimension"
					  , cast(FACILITY_LATITUDE AS varchar(20)) +'-' + cast(FACILITY_LONGITUDE as varchar(20)) as "gml:Point/gml:pos"
	
					  , HORIZONTAL_ACCURACY_MEASURE as "facid:HorizontalAccuracyMeasure/facid:MeasureValue"
					  , null as "facid:HorizontalAccuracyMeasure/facid:MeasureUnit/facid:MeasureUnitCode"
					  , null as "facid:HorizontalAccuracyMeasure/facid:MeasureUnit/facid:MeasureUnitCodeListIdentifier"
					  , null as "facid:HorizontalAccuracyMeasure/facid:MeasureUnit/facid:MeasureUnitName"
					  
					  
					  , null as "facid:HorizontalAccuracyMeasure/facid:MeasurePrecisionText"
					  , null as "facid:HorizontalAccuracyMeasure/facid:ResultQualifier/facid:ResultQualifierCode"
					  , null as "facid:HorizontalAccuracyMeasure/facid:ResultQualifier/facid:ResultQualifierCodeListIdentifier"
					  , null as "facid:HorizontalAccuracyMeasure/facid:ResultQualifier/facid:ResultQualifierName"


					  , null as "facid:HorizontalCollectionMethod/facid:MethodIdentifierCode"
					  , null as "facid:HorizontalCollectionMethod/facid:MethodIdentifierCodeListIdentifier"
					  , null as "facid:HorizontalCollectionMethod/facid:MethodName"
					  , null as "facid:HorizontalCollectionMethod/facid:MethodDescriptionText"
					  , null as "facid:HorizontalCollectionMethod/facid:MethodDeviationsText"
						from  dbo.FAC_MASTER_FACILITY_LIST  
						where FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
						for xml path(''),root('uri'), type) as "facid:FacilityGeographicLocationList"							
*/
----------select * from dbo.tmp_FRS_Mapping order by element
					,(  select 					
							'OKDEQ'  as "facid:DataSource/facid:OriginatingPartnerName"
-- 20111207							,  (select isnull(SUB_SYSTEM_NAME,'') from dbo.FAC_SUB_SYSTEM where SUB_SYSTEM_RID = FAC_MASTER_ENV_INTEREST.SUB_SYSTEM_RID)  as "facid:DataSource/facid:InformationSystemAcronymName"  
                            , 'Facility Profile' as "facid:DataSource/facid:InformationSystemAcronymName"  
							,  isnull(convert(varchar(10),FAC_MASTER_ENV_INTEREST.UPDATED_DATE,120) ,'') as "facid:DataSource/facid:LastUpdatedDate"  
					 
							,  FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID  as "facid:EnvironmentalInterestIdentifier"
							,  isnull(FAC_MASTER_ENV_INTEREST.ENV_INTEREST_TYPE,'')  as "facid:EnvironmentalInterestTypeText"
							,  convert(varchar(10),FAC_MASTER_ENV_INTEREST.ENV_INTEREST_START_DATE, 120) as "facid:EnvironmentalInterestStartDate"
							,  '' as  "facid:EnvironmentalInterestStartDateQualifierText"
							,  convert(varchar(10),FAC_MASTER_ENV_INTEREST.ENV_INTEREST_END_DATE,120)  as "facid:EnvironmentalInterestEndDateQualifierText"
							,  'N' as  "facid:EnvironmentalInterestActiveIndicator"
							,  '' as  "facid:AgencyType/facid:AgencyTypeCode"
							,  '' as  "facid:AgencyType/facid:AgencyTypeCodeListIdentifier"
							,  '' as  "facid:AgencyType/facid:AgencyTypeName"
							
							,(select distinct
		--MOD 20111107						case when ISNUMERIC( rtrim(FAC_MASTER_NAICS_LIST.NAICS_CODE)) = 0 or len(rtrim(isnull(FAC_MASTER_NAICS_LIST.NAICS_CODE,''))) =0  then null  else rtrim(FAC_MASTER_NAICS_LIST.NAICS_CODE) end  as "facid:FacilityNAICSCode"							
		--mod 20111107						, case when ISNUMERIC( rtrim(FAC_MASTER_NAICS_LIST.NAICS_CODE)) = 0 or len(rtrim(isnull(FAC_MASTER_NAICS_LIST.NAICS_CODE,''))) =0  then null else  'Primary' end as "facid:FacilityNAICSPrimaryIndicator"
								case when ISNUMERIC( rtrim(FAC_MASTER_NAICS_LIST.NAICS_CODE)) = 1 and len(rtrim(isnull(FAC_MASTER_NAICS_LIST.NAICS_CODE,''))) =4  then rtrim(FAC_MASTER_NAICS_LIST.NAICS_CODE)  else null end  as "facid:FacilityNAICSCode"							
								, case when ISNUMERIC( rtrim(FAC_MASTER_NAICS_LIST.NAICS_CODE)) = 1 and  len(rtrim(isnull(FAC_MASTER_NAICS_LIST.NAICS_CODE,''))) =4 then case when NAICS_PRIMARY_IND ='Y'  then 'Primary'  else 'Unknown' end  else  null  end as "facid:FacilityNAICSPrimaryIndicator"
								from dbo.FAC_MASTER_NAICS_LIST
								where  FAC_MASTER_NAICS_LIST.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path('facid:FacilityNAICS'), root('uri'), type
							) as "facid:NAICSList"

							,(select distinct
		--mod 20111107						case when isnumeric(rtrim(replace(FAC_MASTER_SIC_LIST.SIC_CODE,' ',''))) = 0 or len(rtrim(isnull(FAC_MASTER_SIC_LIST.SIC_CODE,''))) =0  then  null   else substring(isnull(rtrim(replace(FAC_MASTER_SIC_LIST.SIC_CODE,' ','')),''), 1, 4)  end  as "facid:SICCode"							
		--mod 20111107						, case when isnumeric(rtrim(replace(FAC_MASTER_SIC_LIST.SIC_CODE,' ',''))) = 0 or len(rtrim(isnull(FAC_MASTER_SIC_LIST.SIC_CODE,''))) =0 then null  else 'Primary' end  as "facid:SICPrimaryIndicator"
								case when ISNUMERIC( rtrim(FAC_MASTER_SIC_LIST.SIC_CODE)) = 1 and len(rtrim(isnull(FAC_MASTER_SIC_LIST.SIC_CODE,''))) =4  then rtrim(FAC_MASTER_SIC_LIST.SIC_CODE)  else null end   as "facid:SICCode"						
								, case when ISNUMERIC( rtrim(FAC_MASTER_SIC_LIST.SIC_CODE)) = 1 and  len(rtrim(isnull(FAC_MASTER_SIC_LIST.SIC_CODE,''))) =4 then case when SIC_PRIMARY_IND ='Y'  then 'Primary'  else 'Unknown' end  else  null  end as "facid:SICPrimaryIndicator"
								from dbo.FAC_MASTER_SIC_LIST
								where  FAC_MASTER_SIC_LIST.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path('facid:FacilitySIC'), root('uri'),type
							) as "facid:SICList"

							,(select distinct
								  FAC_MASTER_AFFILIATION.MASTER_AFFILICATION_RID  as "facid:AffiliateIdentifier"							
								,  isnull(FAC_MASTER_AFFILIATION.AFFILIATION_TYPE,'')  as "facid:Affiliation/facid:AffiliationTypeText"
								,  convert(varchar(10),FAC_MASTER_AFFILIATION.AFFILIATION_START_DATE,120) as "facid:Affiliation/facid:AffiliationStartDate"
								,  convert(varchar(10),FAC_MASTER_AFFILIATION.AFFILIATION_END_DATE,120)  as "facid:Affiliation/facid:AffiliationEndDate"
								,  ''  as "facid:Affiliation/facid:AffiliationStatusText"
--								,  FAC_MASTER_AFFILIATION.STATUS_CD  as "facid:Affiliation/facid:AffiliationStatusText"
								,  null as  "facid:Affiliation/facid:AffiliationStatusDetermineDate"
								
								from dbo.FAC_MASTER_AFFILIATION
								where  FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path('facid:FacilityAffiliation'), root('uri'),type
							) as "facid:AffiliationList"
/*						
							,(select 
								  '' as  "facid:AlternativeIdentificationIdentifier"
								,  '' as  "facid:AlternativeIdentificationTypeText"
								
								from 
								where 
								for xml path('facid:AlternativeIdentification'), type
							) as "facid:AlternativeIdentificationList"	
*/							
							,(select distinct
						           isnull(FAC_MASTER_AFFILIATION.EMAIL_ADDRESS,'')  as "facid:ElectronicAddressText"
								,  case when len(isnull(FAC_MASTER_AFFILIATION.EMAIL_ADDRESS,'')) = 0 then null else 'Email' end  as "facid:ElectronicAddressTypeName"
								from dbo.FAC_MASTER_AFFILIATION 
								where  FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path('facid:ElectronicAddress'), root('uri'),type
							) as "facid:ElectronicAddressList"	
					   from  dbo.FAC_MASTER_ENV_INTEREST  
					   where FAC_MASTER_ENV_INTEREST.FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
					   for xml path('facid:EnvironmentalInterest'),root('uri'), type) as "facid:EnvironmentalInterestList"

/*
							,(select distinct
								 isnull(FAC_MASTER_NAICS_LIST.NAICS_CODE,'')  as "facid:FacilityNAICSCode"							
								from dbo.FAC_MASTER_NAICS_LIST inner join dbo.FAC_MASTER_ENV_INTEREST  on FAC_MASTER_NAICS_LIST.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								where  FAC_MASTER_ENV_INTEREST.FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
								for xml path(''), root('uri'), type
							) as "facid:FacilityNAICSList"

							,(select distinct
								  isnull(FAC_MASTER_SIC_LIST.SIC_CODE,'')  as "facid:SICCode"							
								from dbo.FAC_MASTER_SIC_LIST inner join dbo.FAC_MASTER_ENV_INTEREST  on FAC_MASTER_SIC_LIST.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								where  FAC_MASTER_ENV_INTEREST.FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
								for xml path(''), root('uri'),type
							) as "facid:SICList"
*/
/*
							,  (select 'OKDEQ'  as "facid:OriginatingPartnerName"
                                    ,isnull(SUB_SYSTEM_NAME,'')  as "facid:InformationSystemAcronymName"
									from dbo.FAC_SUB_SYSTEM inner join dbo.FAC_MASTER_ENV_INTEREST  
                                    on FAC_SUB_SYSTEM.SUB_SYSTEM_RID = FAC_MASTER_ENV_INTEREST.SUB_SYSTEM_RID 
								where  FAC_MASTER_ENV_INTEREST.FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
								for xml path('facid:DataSource'), root('uri'),type
								)  
*/
                             , 'OKDEQ'  as "facid:DataSource/facid:OriginatingPartnerName"
                             , 'Facility Profile' as  "facid:DataSource/facid:InformationSystemAcronymName"
                             , convert(varchar(10),getdate(),120)  as "facid:DataSource/facid:LastUpdatedDate"
/*
							,(select distinct
								  isnull(FAC_MASTER_AFFILIATION.MASTER_AFFILICATION_RID,'')  as "facid:AffiliateIdentifier"							
								from dbo.FAC_MASTER_AFFILIATION inner join dbo.FAC_MASTER_ENV_INTEREST  on FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								where  FAC_MASTER_ENV_INTEREST.FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
								for xml path('facid:FacilityAffiliation'), root('uri'),type
							) as "facid:AffiliationList"
*/
/*
							,  '' as  "facid:AlternativeIdentificationIdentifier"
								,  '' as  "facid:AlternativeIdentificationTypeText"
								
								from 
								where 
								for xml path('facid:AlternativeIdentification'), type
							) as "facid:AlternativeIdentificationList"	
*/
--                             , '' as "facid:AlternativeNameList/facid:AlternativeName/facid:AlternativeNameText"
  --                           , '' as "facid:AlternativeNameList/facid:AlternativeName/facid:AlternativeNameTypeText"

					from dbo.FAC_MASTER_FACILITY_LIST master_list 
--						inner join dbo.FAC_MASTER_ENV_INTEREST  INTEREST on INTEREST.FACILITY_MASTER_RID = master_list.FACILITY_MASTER_RID
					where master_list.FACILITY_MASTER_RID in (select FACILITY_MASTER_RID from dbo.FAC_MASTER_FACILITY_LIST where updated_date > @start_date)
					-- ('60340','75447')
				--	 (select   a.facility_master_rid from dbo.FAC_MASTER_FACILITY_LIST  a inner join dbo.FAC_MASTER_ENV_INTEREST  b on a.FACILITY_MASTER_RID = b.FACILITY_MASTER_RID   where a.CREATED_DATE > @start_date) 
					for xml path('facid:Facility') ,root('uri'), type) as "facid:FacilityList" 


					,(select  distinct
					             FAC_MASTER_AFFILIATION.MASTER_AFFILICATION_RID  as "facid:AffiliateIdentifier" 
						        ,  case when len(isnull(replace(FAC_MASTER_AFFILIATION.TELEPHONE_NUM,' ',''),''))=0 then '' else rtrim(replace(FAC_MASTER_AFFILIATION.TELEPHONE_NUM,' ','')) end  as "facid:TelephonicList/facid:Telephonic/facid:TelephoneNumberText"
								,  case when len(isnull(replace(FAC_MASTER_AFFILIATION.TELEPHONE_NUM,' ',''),''))=0 then '' else 'Telephone' end  as  "facid:TelephonicList/facid:Telephonic/facid:TelephoneNumberTypeName"
								,  case when len(isnull(replace(FAC_MASTER_AFFILIATION.PHONE_EXTENSION,' ',''),''))=0 then '' else rtrim(replace(FAC_MASTER_AFFILIATION.PHONE_EXTENSION,' ','')) end  as "facid:TelephonicList/facid:Telephonic/facid:TelephoneExtensionNumberText"
						        ,  case when len(isnull(replace(FAC_MASTER_AFFILIATION.EMAIL_ADDRESS,' ',''),''))=0 then '' else rtrim(replace(FAC_MASTER_AFFILIATION.EMAIL_ADDRESS,' ','')) end   as "facid:ElectronicAddressList/facid:ElectronicAddress/facid:ElectronicAddressText"
								,  case when len(isnull(replace(FAC_MASTER_AFFILIATION.EMAIL_ADDRESS,' ',''),''))=0 then '' else 'Email' end   as "facid:ElectronicAddressList/facid:ElectronicAddress/facid:ElectronicAddressTypeName"


						        , case when len(isnull(replace(FAC_MASTER_AFFILIATION.Organization_Identifier,' ',''),''))=0 then '' else 'ODEQ|PartyAffiliation' end as "facid:OrganizationIdentity/facid:OrganizationIdentifier/@OrganizationIdentifierContext"
						        , case when len(isnull(replace(FAC_MASTER_AFFILIATION.Organization_Identifier,' ',''),''))=0 then '' else replace(FAC_MASTER_AFFILIATION.Organization_Identifier,' ','') end   as "facid:OrganizationIdentity/facid:OrganizationIdentifier"
								, case when len(isnull(replace(FAC_MASTER_AFFILIATION.Organization_Identifier,' ',''),''))=0 then '' else rtrim(FAC_MASTER_AFFILIATION.ORGANIZATION_FORMAL_NAME) end as "facid:OrganizationIdentity/facid:OrganizationFormalName"


						        ,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),''))=0 then '' else rtrim(b.MAIL_MAILING_ADDRESS) end    as "facid:MailingAddress/facid:MailingAddressText"
								,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),''))=0 then '' else rtrim(b.MAIL_SUPPLEMENTAL_ADDRESS) end     as "facid:MailingAddress/facid:SupplementalAddressText"
								,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),''))=0 then '' else rtrim(b.MAIL_CITY_NAME) end   as "facid:MailingAddress/facid:MailingAddressCityName"
								,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),''))=0 then '' else rtrim(b.MAIL_STATE_CODE) end  as "facid:MailingAddress/facid:StateIdentity/facid:StateCode"
								,  '' as  "facid:MailingAddress/facid:StateIdentity/facid:StateCodeListIdentifier"
								,  '' as  "facid:MailingAddress/facid:StateIdentity/facid:StateName"
	--							,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),'')) =0 then '' else  'ODEQ|PartyAffiliation' end  as "facid:MailingAddress/facid:AddressPostalCode/@facid:AddressPostalCodeContext"
								,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),'')) =0 then '' else rtrim(b.MAIL_ZIP_CODE) end  as "facid:MailingAddress/facid:AddressPostalCode"
								,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),''))=0 then '' else 'USA' end as "facid:MailingAddress/facid:CountryIdentity/facid:CountryCode"
								, '' as  "facid:MailingAddress/facid:CountryIdentity/facid:CountryCodeListIdentifier"
								,  case when len(isnull(replace(b.MAIL_MAILING_ADDRESS,' ',''),''))=0 then '' else 'United States Of America' end as "facid:MailingAddress/facid:CountryIdentity/facid:CountryName"

								from dbo.FAC_MASTER_AFFILIATION  inner join FAC_MASTER_ENV_INTEREST a on FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = a.MASTER_ENV_INTEREST_RID
								inner join  FAC_MASTER_FACILITY_LIST b on a.facility_master_rid = b.facility_master_rid
								where a.FACILITY_MASTER_RID in (select FACILITY_MASTER_RID from dbo.FAC_MASTER_FACILITY_LIST where updated_date > @start_date)
								--('60340','75447')
					    
					
/*					
							(select distinct
						           case when len(replace(TELEPHONE_NUM,' ',''))=0 then null else rtrim(replace(TELEPHONE_NUM,' ','')) end  as "facid:TelephoneNumberText"
								,  case when len(replace(TELEPHONE_NUM,' ',''))=0 then null else 'Telephone' end  as  "facid:TelephoneNumberTypeName"
								,  PHONE_EXTENSION  as "facid:TelephoneExtensionNumberText"
								from dbo.FAC_MASTER_AFFILIATION  
								where  FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path('facid:Telephonic'), root('uri'),type
							) as "facid:TelephonicList"	
*/		
/*												
							,(select distinct
						           case when len(replace(EMAIL_ADDRESS,' ',''))=0 then null else rtrim(replace(EMAIL_ADDRESS,' ','')) end   as "facid:ElectronicAddressText"
								,  case when len(replace(EMAIL_ADDRESS,' ',''))=0 then null else 'Email' end   as "facid:ElectronicAddressTypeName"
								from dbo.FAC_MASTER_AFFILIATION 
								where  FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path('facid:ElectronicAddress'), root('uri'),type
							) as "facid:ElectronicAddressList"	
*/
/*							
							,(select distinct
						          case when len(replace(Organization_Identifier,' ','')) =0 then null else 'ODEQ|PartyAffiliation' end as "facid:OrganizationIdentifier/@OrganizationIdentifierContext"
						        , case when len(replace(Organization_Identifier,' ','')) =0 then null else replace(Organization_Identifier,' ','') end   as "facid:OrganizationIdentifier"
								, case when len(replace(Organization_Identifier,' ','')) =0 then null else rtrim(ORGANIZATION_FORMAL_NAME) end as "facid:OrganizationFormalName"
								from dbo.FAC_MASTER_AFFILIATION 
								where  FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path(''), root('uri'),type
							) as "facid:OrganizationIdentity"	
*/								
/*																											
							,(select distinct
						          b.MAIL_MAILING_ADDRESS   as "facid:MailingAddressText"
								,  b.MAIL_SUPPLEMENTAL_ADDRESS   as "facid:SupplementalAddressText"
								,  b.MAIL_CITY_NAME  as "facid:MailingAddressCityName"
								,  b.MAIL_STATE_CODE as "facid:StateIdentity/facid:StateCode"
								,  null as  "facid:StateIdentity/facid:StateCodeListIdentifier"
								,  null as  "facid:StateIdentity/facid:StateName"
								,  case when len(b.MAIL_ZIP_CODE) !=0 then 'ODEQ|PartyAffiliation' else null end  as "facid:AddressPostalCode/@facid:AddressPostalCodeContext"
								,  b.MAIL_ZIP_CODE  as "facid:AddressPostalCode"
								,  'USA'  as "facid:CountryIdentity/facid:CountryCode"
								, null as  "facid:CountryIdentity/facid:CountryCodeListIdentifier"
								,  'United States Of America'  as "facid:CountryIdentity/facid:CountryName"
								from dbo.FAC_MASTER_AFFILIATION  inner join FAC_MASTER_ENV_INTEREST a on FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = a.MASTER_ENV_INTEREST_RID
								inner join  FAC_MASTER_FACILITY_LIST b on a.facility_master_rid = b.facility_master_rid
								where FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = FAC_MASTER_ENV_INTEREST.MASTER_ENV_INTEREST_RID
								for xml path(''), root('uri'),type
							) as "facid:MailingAddress"				
																		
					   from  dbo.FAC_MASTER_ENV_INTEREST  
					where FACILITY_MASTER_RID in ('60340','75447')
					--(select    a.facility_master_rid from dbo.FAC_MASTER_FACILITY_LIST  a inner join dbo.FAC_MASTER_ENV_INTEREST  b on a.FACILITY_MASTER_RID = b.FACILITY_MASTER_RID   where a.CREATED_DATE > @start_date  ) 
*/
					   for xml path('facid:Affiliate'),root('uri'), type) as "facid:AffiliateList"

					for xml path('facid:FacilityDetails') ,root('uri') 	, type) 
/*

--	select *        
 --                   as fp_file
 --                   into #tt
					from dbo.FAC_MASTER_FACILITY_LIST FAC_MASTER
					where FACILITY_MASTER_RID in (select top 5   a.facility_master_rid from dbo.FAC_MASTER_FACILITY_LIST  a inner join dbo.FAC_MASTER_ENV_INTEREST  b on a.FACILITY_MASTER_RID = b.FACILITY_MASTER_RID) 
					for xml path('facid:FacilityDetails') ,root('uri') 	, type) 
*/					
				
select @fp = cast(@xml_file as varchar(max))



---select * from dbo.FAC_MASTER_FACILITY_LIST where CREATED_DATE > '2011-10-30'
/*
				
select distinct
						          case when len(replace(FAC_MASTER_AFFILIATION.Organization_Identifier,' ','')) =0 then null else 'ODEQ|PartyAffiliation' end as "facid:OrganizationIdentifier/@OrganizationIdentifierContext"
						        , case when len(replace(FAC_MASTER_AFFILIATION.Organization_Identifier,' ','')) =0 then null else replace(Organization_Identifier,' ','') end   as "facid:OrganizationIdentifier"
								, case when len(replace(FAC_MASTER_AFFILIATION.Organization_Identifier,' ','')) =0 then null else rtrim(ORGANIZATION_FORMAL_NAME) end as "facid:OrganizationFormalName"




select b.FACILITY_MASTER_RID, FAC_MASTER_AFFILIATION.* 			


select distinct
						           case when len(replace(FAC_MASTER_AFFILIATION.TELEPHONE_NUM,' ',''))=0 then null else rtrim(replace(TELEPHONE_NUM,' ','')) end  as "facid:TelephoneNumberText"
								,  case when len(replace(FAC_MASTER_AFFILIATION.TELEPHONE_NUM,' ',''))=0 then null else 'Telephone' end  as  "facid:TelephoneNumberTypeName"
								,  FAC_MASTER_AFFILIATION.PHONE_EXTENSION  as "facid:TelephoneExtensionNumberText"
										
from dbo.FAC_MASTER_AFFILIATION  inner join FAC_MASTER_ENV_INTEREST a on FAC_MASTER_AFFILIATION.MASTER_ENV_INTEREST_RID = a.MASTER_ENV_INTEREST_RID
								inner join  FAC_MASTER_FACILITY_LIST b on a.facility_master_rid = b.facility_master_rid								
where b.FACILITY_MASTER_RID in ('60340','75447')
	
where b.FACILITY_MASTER_RID in (select    a.facility_master_rid from dbo.FAC_MASTER_FACILITY_LIST  a inner join dbo.FAC_MASTER_ENV_INTEREST  b on a.FACILITY_MASTER_RID = b.FACILITY_MASTER_RID   where a.CREATED_DATE > '2010-10-30') 

select * from dbo.FAC_MASTER_AFFILIATION
*/
GO
/****** Object:  StoredProcedure [dbo].[usp_get_facility_profile_xml]    Script Date: 12/22/2011 15:12:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[usp_get_facility_profile_xml] (@start_date datetime, @output varchar(max) out)
as

DECLARE	@return_value int,		@fp varchar(max), @header varchar(max)

set @header = '<?xml version="1.0" encoding="UTF-8"?>
<hdr:Document xmlns:hdr="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:ds="http://www.w3.org/2000/09/xmldsig#" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" Id="id001">
    <Header>
        <Author>Glenn Angell</Author>
        <Organization>Maine DEP</Organization>
        <Title>
			Maine DEP Facility Update
		</Title>
        <CreationTime>2011-09-22T10:38:55</CreationTime>
        <ContactInfo>glenn.b.angell@maine.gov</ContactInfo>
        <Notification>glenn.b.angell@maine.gov</Notification>
        <Sensitivity>Low</Sensitivity>
        <!--<Property>
			<name>StateCode</name>
			<value>41</value>
		</Property>-->
    </Header>
    <Payload Operation="FacilityAddUpdate">'
            



EXEC	@return_value = [dbo].[usp_get_facility_profile]
		@start_date = @start_date,
		@fp = @fp OUTPUT



select @fp = replace(replace(REPLACE(@fp,'<uri xmlns:gml="urm" xmlns:facid="uri">',''),'</uri>',''),'<facid:FacilityDetails>', '<facid:FacilityDetails schemaVersion="3.0" xsi:schemaLocation="http://www.exchangenetwork.net/schema/facilityid/3 http://www.windsorsolutions.biz/xsd/FACID/FacilityID/index.xsd" xmlns:facid="http://www.exchangenetwork.net/schema/facilityid/3" xmlns:gml="http://www.opengis.net/gml">')

select @output =  @header + @fp + '</Payload>' + '</hdr:Document>' 


--select cast(@header + @fp + '</Payload>' + '</hdr:Document>' as XML)
GO
/****** Object:  Default [DF__REF_FACIL__STATU__37A5467C]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_REF_FACILITY_SITE_TYPE] ADD  DEFAULT ('A') FOR [STATUS_CD]
GO
/****** Object:  Default [DF__REF_FACIL__CREAT__38996AB5]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_REF_FACILITY_SITE_TYPE] ADD  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
/****** Object:  Default [DF__REF_FACIL__CREAT__398D8EEE]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_REF_FACILITY_SITE_TYPE] ADD  DEFAULT ('SYS') FOR [CREATED_BY]
GO
/****** Object:  Default [DF__REF_FACIL__UPDAT__3A81B327]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_REF_FACILITY_SITE_TYPE] ADD  DEFAULT (getdate()) FOR [UPDATED_DATE]
GO
/****** Object:  Default [DF__REF_FACIL__UPDAT__3B75D760]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_REF_FACILITY_SITE_TYPE] ADD  DEFAULT ('SYS') FOR [UPDATED_BY]
GO
/****** Object:  Default [DF__FAC_SUB_S__STATU__3F466844]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_FAC_SUB_SYSTEM] ADD  CONSTRAINT [DF__FAC_SUB_S__STATU__3F466844]  DEFAULT ('A') FOR [STATUS_CD]
GO
/****** Object:  Default [DF__FAC_SUB_S__CREAT__403A8C7D]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_FAC_SUB_SYSTEM] ADD  CONSTRAINT [DF__FAC_SUB_S__CREAT__403A8C7D]  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
/****** Object:  Default [DF__FAC_SUB_S__CREAT__412EB0B6]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_FAC_SUB_SYSTEM] ADD  CONSTRAINT [DF__FAC_SUB_S__CREAT__412EB0B6]  DEFAULT ('SYS') FOR [CREATED_BY]
GO
/****** Object:  Default [DF__FAC_SUB_S__UPDAT__4222D4EF]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_FAC_SUB_SYSTEM] ADD  CONSTRAINT [DF__FAC_SUB_S__UPDAT__4222D4EF]  DEFAULT (getdate()) FOR [UPDATED_DATE]
GO
/****** Object:  Default [DF__FAC_SUB_S__UPDAT__4316F928]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[_FAC_SUB_SYSTEM] ADD  CONSTRAINT [DF__FAC_SUB_S__UPDAT__4316F928]  DEFAULT ('SYS') FOR [UPDATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__09DE7BCC]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_FACILITY_LIST] ADD  CONSTRAINT [DF__FAC_MASTE__CREAT__09DE7BCC]  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__0AD2A005]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_FACILITY_LIST] ADD  CONSTRAINT [DF__FAC_MASTE__CREAT__0AD2A005]  DEFAULT ('SYS') FOR [CREATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__0BC6C43E]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_FACILITY_LIST] ADD  CONSTRAINT [DF__FAC_MASTE__UPDAT__0BC6C43E]  DEFAULT (getdate()) FOR [UPDATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__0CBAE877]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_FACILITY_LIST] ADD  CONSTRAINT [DF__FAC_MASTE__UPDAT__0CBAE877]  DEFAULT ('SYS') FOR [UPDATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__FACIL__0DAF0CB0]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_FACILITY_LIST] ADD  CONSTRAINT [DF__FAC_MASTE__FACIL__0DAF0CB0]  DEFAULT ('A') FOR [FACILITY_LONGITUDE]
GO
/****** Object:  Default [DF__FAC_MASTE__STATU__03317E3D]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST] ADD  CONSTRAINT [DF__FAC_MASTE__STATU__03317E3D]  DEFAULT ('ACTIVE') FOR [STATUS_CD]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__0425A276]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST] ADD  CONSTRAINT [DF__FAC_MASTE__CREAT__0425A276]  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__0519C6AF]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST] ADD  CONSTRAINT [DF__FAC_MASTE__CREAT__0519C6AF]  DEFAULT ('SYS') FOR [CREATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__060DEAE8]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST] ADD  CONSTRAINT [DF__FAC_MASTE__UPDAT__060DEAE8]  DEFAULT (getdate()) FOR [UPDATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__07020F21]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST] ADD  CONSTRAINT [DF__FAC_MASTE__UPDAT__07020F21]  DEFAULT ('SYS') FOR [UPDATED_BY]
GO
/****** Object:  Default [DF_FAC_MASTER_ENV_INTEREST_SUB_SYSTEM_NAME]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST] ADD  CONSTRAINT [DF_FAC_MASTER_ENV_INTEREST_SUB_SYSTEM_NAME]  DEFAULT ('Facility Profile') FOR [SUB_SYSTEM_NAME]
GO
/****** Object:  Default [DF__FAC_MASTE__STATU__7C8480AE]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_AFFILIATION] ADD  CONSTRAINT [DF__FAC_MASTE__STATU__7C8480AE]  DEFAULT ('A') FOR [STATUS_CD]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__7D78A4E7]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_AFFILIATION] ADD  CONSTRAINT [DF__FAC_MASTE__CREAT__7D78A4E7]  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__7E6CC920]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_AFFILIATION] ADD  CONSTRAINT [DF__FAC_MASTE__CREAT__7E6CC920]  DEFAULT ('SYS') FOR [CREATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__7F60ED59]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_AFFILIATION] ADD  CONSTRAINT [DF__FAC_MASTE__UPDAT__7F60ED59]  DEFAULT (getdate()) FOR [UPDATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__00551192]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_AFFILIATION] ADD  CONSTRAINT [DF__FAC_MASTE__UPDAT__00551192]  DEFAULT ('SYS') FOR [UPDATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__STATU__2F10007B]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_SIC_LIST] ADD  DEFAULT ('A') FOR [STATUS_CD]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__300424B4]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_SIC_LIST] ADD  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__30F848ED]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_SIC_LIST] ADD  DEFAULT ('SYS') FOR [CREATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__31EC6D26]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_SIC_LIST] ADD  DEFAULT (getdate()) FOR [UPDATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__32E0915F]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_SIC_LIST] ADD  DEFAULT ('SYS') FOR [UPDATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__STATU__276EDEB3]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_NAICS_LIST] ADD  DEFAULT ('A') FOR [STATUS_CD]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__286302EC]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_NAICS_LIST] ADD  DEFAULT (getdate()) FOR [CREATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__CREAT__29572725]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_NAICS_LIST] ADD  DEFAULT ('SYS') FOR [CREATED_BY]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__2A4B4B5E]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_NAICS_LIST] ADD  DEFAULT (getdate()) FOR [UPDATED_DATE]
GO
/****** Object:  Default [DF__FAC_MASTE__UPDAT__2B3F6F97]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_NAICS_LIST] ADD  DEFAULT ('SYS') FOR [UPDATED_BY]
GO
/****** Object:  ForeignKey [FAC_MASTER_FACILITY_LIST_FK1]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST]  WITH CHECK ADD  CONSTRAINT [FAC_MASTER_FACILITY_LIST_FK1] FOREIGN KEY([FACILITY_MASTER_RID])
REFERENCES [dbo].[FAC_MASTER_FACILITY_LIST] ([FACILITY_MASTER_RID])
GO
ALTER TABLE [dbo].[FAC_MASTER_ENV_INTEREST] CHECK CONSTRAINT [FAC_MASTER_FACILITY_LIST_FK1]
GO
/****** Object:  ForeignKey [FAC_MASTER_AFFILIIATION_FK1]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_AFFILIATION]  WITH CHECK ADD  CONSTRAINT [FAC_MASTER_AFFILIIATION_FK1] FOREIGN KEY([MASTER_ENV_INTEREST_RID])
REFERENCES [dbo].[FAC_MASTER_ENV_INTEREST] ([MASTER_ENV_INTEREST_RID])
GO
ALTER TABLE [dbo].[FAC_MASTER_AFFILIATION] CHECK CONSTRAINT [FAC_MASTER_AFFILIIATION_FK1]
GO
/****** Object:  ForeignKey [MASTER_ENV_INTEREST_FK]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_SIC_LIST]  WITH CHECK ADD  CONSTRAINT [MASTER_ENV_INTEREST_FK] FOREIGN KEY([MASTER_ENV_INTEREST_RID])
REFERENCES [dbo].[FAC_MASTER_ENV_INTEREST] ([MASTER_ENV_INTEREST_RID])
GO
ALTER TABLE [dbo].[FAC_MASTER_SIC_LIST] CHECK CONSTRAINT [MASTER_ENV_INTEREST_FK]
GO
/****** Object:  ForeignKey [MASTER_ENV_INTEREST_FK2]    Script Date: 12/22/2011 15:12:14 ******/
ALTER TABLE [dbo].[FAC_MASTER_NAICS_LIST]  WITH CHECK ADD  CONSTRAINT [MASTER_ENV_INTEREST_FK2] FOREIGN KEY([MASTER_ENV_INTEREST_RID])
REFERENCES [dbo].[FAC_MASTER_ENV_INTEREST] ([MASTER_ENV_INTEREST_RID])
GO
ALTER TABLE [dbo].[FAC_MASTER_NAICS_LIST] CHECK CONSTRAINT [MASTER_ENV_INTEREST_FK2]
GO
