USE [master]
GO
/****** Object:  Database [ICIS_STG]    Script Date: 05/29/2012 16:05:06 ******/
CREATE DATABASE [ICIS_STG] ON  PRIMARY 
( NAME = N'ICIS_DMR_STG', FILENAME = N'E:\MSSQL\Data\ICIS_STG.mdf' , SIZE = 245760KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ICIS_DMR_STG_log', FILENAME = N'E:\MSSQL\Data\ICIS_STG_1.ldf' , SIZE = 427392KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
EXEC dbo.sp_dbcmptlevel @dbname=N'ICIS_STG', @new_cmptlevel=90
GO


USE [ICIS_STG]
GO
/****** Object:  User [SOM\RajaS]    Script Date: 05/29/2012 16:06:44 ******/
/****** Object:  User [nodeadmin]    Script Date: 05/29/2012 16:06:44 ******/
CREATE USER [nodeadmin] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [ICIS_user]    Script Date: 05/29/2012 16:06:44 ******/
/****** Object:  View [dbo].[v_dmr_validate_null]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
---select * from [v_dmr_validate]
CREATE view [dbo].[v_dmr_validate_null] as
select a.submission_id_int, a.transaction_id
, isnull(a.permit_no, 'XXXXXXXXXXXXXXXXXXXX') as permit_no 
, isnull(a.station_no , 'XXXXXXXXXXXXXXXXXXXX') as station_no 
, isnull(a.report_designator, 'XXXXXXXXXXXXXXXXXXXX') as report_designator 
, isnull(convert(varchar, a.mp_end_date, 120), 'XXXXXXXXXXXXXXXXXXXX') as mp_end_date
, isnull(convert(varchar, a.received_dttm, 120) , 'XXXXXXXXXXXXXXXXXXXX') as received_dttm
, '' as stage_code
, '0' as LimitSeasonNumber
, '' as sample_type
, '' as  sample_freq
, '' as load_conc_value
, '' as load_conc_unit
, created_dttm, created_by, updated_dttm,updated_by
from (select 
 a.submission_id_int, a.transaction_id
, case when rtrim(ltrim(a.station_id)) ='' then null else a.station_id end   AS station_id
, case when rtrim(ltrim(cast(a.monitor_pt_id as varchar(100)))) ='' then null else cast(a.monitor_pt_id as varchar(100) ) end   AS monitor_pt_id
, case when rtrim(ltrim(a.monitor_pt_version)) ='' then null else a.monitor_pt_version end  AS monitor_pt_version
, case when rtrim(ltrim(a.permit_no)) ='' then null else a.permit_no end   AS permit_no
, case when rtrim(ltrim(a.station_no)) ='' then null else a.station_no end  AS station_no
, case when rtrim(ltrim(a.report_designator)) ='' then null else a.report_designator end  AS report_designator
, case when rtrim(ltrim(a.mp_end_date)) ='' then null else a.mp_end_date end  AS mp_end_date
--, a.rpt_sign_date
--, a.PrincipalExecutiveOfficerFirstName
--, a.PrincipalExecutiveOfficerLastName
--, a.PrincipalExecutiveOfficerTitle
--, a.PrincipalExecutiveOfficerTelephone
--, a.SignatoryFirstName
--, a.SignatoryLastName
--, a.certifier_phone
--, a.comment
, case when rtrim(ltrim(a.no_discharge_site)) ='' then null else a.no_discharge_site end as no_discharge_site
, case when rtrim(ltrim(a.received_dttm)) ='' then null else a.received_dttm end  AS  received_dttm
, case when rtrim(ltrim(b.par_code)) ='' then null else b.par_code end    AS  par_code
, case when rtrim(ltrim(b.stage_code)) ='' then null else b.stage_code end    AS stage_code
, case when rtrim(ltrim(cast(b.LimitSeasonNumber as varchar(100)))) ='' then null else b.LimitSeasonNumber end    AS   LimitSeasonNumber
, case when rtrim(ltrim(b.sample_type)) ='' then null else b.sample_type end   AS sample_type
, case when rtrim(ltrim(b.sample_freq)) ='' then null else b.sample_freq end    AS  sample_freq
, case when rtrim(ltrim(b.excursion)) ='' then null else b.excursion end  AS   excursion
, case when rtrim(ltrim(b.conc_unit)) ='' then null else b.conc_unit end  AS   conc_unit
, case when rtrim(ltrim(b.load_unit)) ='' then null else b.load_unit end  AS  load_unit 
--, b.load_1_prefix  
, case when rtrim(ltrim(b.load_1)) ='' then null else b.load_1 end  AS  load_1
--, b.load_1_nodi
--, b.load_2_prefix 
, case when rtrim(ltrim(b.load_2)) ='' then null else b.load_2 end  AS   load_2
--, b.load_2_nodi
--, b.conc_1_prefix
, case when rtrim(ltrim(b.conc_1)) ='' then null else b.conc_1 end  AS  conc_1
--, b.conc_1_nodi
--, b.conc_2_prefix
, case when rtrim(ltrim(b.conc_2)) ='' then null else b.conc_2 end   AS  conc_2
--, b.conc_2_nodi
--, b.conc_3_prefix
, case when rtrim(ltrim(b.conc_3)) ='' then null else b.conc_3 end   AS   conc_3
--, b.conc_3_nodi  
, a.created_dttm, a.created_by, a.updated_dttm,a.updated_by
from dbo.icis_dmr_report a inner join  icis_dmr_numeric_results b
							on a.submission_id_int = b.submission_id_int
								and a.transaction_id = b.transaction_id
								and a.monitor_pt_id = b.monitor_pt_id
								and a.monitor_pt_version = b.monitor_pt_version) a
where a.no_discharge_site is not null
and ( a.permit_no is null or a.station_no is null or a.report_designator is null or a.mp_end_date is null or a.received_dttm is null)
union
select a.submission_id_int, a.transaction_id
, isnull(a.permit_no, 'XXXXXXXXXXXXXXXXXXXX') as permit_no 
, isnull(a.station_no , 'XXXXXXXXXXXXXXXXXXXX') as station_no 
, isnull(a.report_designator, 'XXXXXXXXXXXXXXXXXXXX') as report_designator 
, isnull(convert(varchar, a.mp_end_date, 120), 'XXXXXXXXXXXXXXXXXXXX') as mp_end_date
, isnull(convert(varchar, a.received_dttm, 120) , 'XXXXXXXXXXXXXXXXXXXX') as received_dttm
, isnull(a.stage_code, 'XXXXXXXXXXXXXXXXXXXX') as stage_code
, isnull(cast(a.LimitSeasonNumber as varchar), 'XXXXXXXXXXXXXXXXXXXX') as LimitSeasonNumber
, isnull(a.sample_type, 'XXXXXXXXXXXXXXXXXXXX') as sample_type
, isnull(a.sample_freq, 'XXXXXXXXXXXXXXXXXXXX') as  sample_freq
, case when (len(a.load_1) + len(a.load_2) + len(a.conc_1) + len(a.conc_2) + len(a.conc_3)) =0 then 'XXXXXXXXXXXXXXXXXXXX' 
  else isnull('load_1:' + cast(a.load_1 as varchar),'') +   isnull( ' ' + 'load_2:' + cast(a.load_2 as varchar),'') +  isnull( ' '+'conc_1:' + cast(a.conc_1 as varchar),'') +  isnull(' '+ 'conc_2:' + cast(a.conc_2 as varchar),'') + isnull(' ' + 'conc_3:' + cast(a.conc_3 as varchar),'') end as load_conc_value
, case when (len(a.load_unit) +  len(a.conc_unit))  =0 then 'XXXXXXXXXXXXXXXXXXXX' 
  else isnull('load_unit:' + a.load_unit,'') + isnull(' ' +  'conc_unit:' + a.conc_unit ,'') end as load_conc_unit
, created_dttm, created_by, updated_dttm,updated_by
from (select 
 a.submission_id_int, a.transaction_id
, case when rtrim(ltrim(a.station_id)) ='' then null else a.station_id end   AS station_id
, case when rtrim(ltrim(a.monitor_pt_id)) ='' then null else a.monitor_pt_id end   AS monitor_pt_id
, case when rtrim(ltrim(a.monitor_pt_version)) ='' then null else a.monitor_pt_version end  AS monitor_pt_version
, case when rtrim(ltrim(a.permit_no)) ='' then null else a.permit_no end   AS permit_no
, case when rtrim(ltrim(a.station_no)) ='' then null else a.station_no end  AS station_no
, case when rtrim(ltrim(a.report_designator)) ='' then null else a.report_designator end  AS report_designator
, case when rtrim(ltrim(a.mp_end_date)) ='' then null else a.mp_end_date end  AS mp_end_date
--, a.rpt_sign_date
--, a.PrincipalExecutiveOfficerFirstName
--, a.PrincipalExecutiveOfficerLastName
--, a.PrincipalExecutiveOfficerTitle
--, a.PrincipalExecutiveOfficerTelephone
--, a.SignatoryFirstName
--, a.SignatoryLastName
--, a.certifier_phone
--, a.comment
, case when rtrim(ltrim(a.no_discharge_site)) ='' then null else a.no_discharge_site end as no_discharge_site
, case when rtrim(ltrim(a.received_dttm)) ='' then null else a.received_dttm end  AS  received_dttm
, case when rtrim(ltrim(b.par_code)) ='' then null else b.par_code end    AS  par_code
, case when rtrim(ltrim(b.stage_code)) ='' then null else b.stage_code end    AS stage_code
, case when rtrim(ltrim(b.LimitSeasonNumber)) ='' then null else b.LimitSeasonNumber end    AS   LimitSeasonNumber
, case when rtrim(ltrim(b.sample_type)) ='' then null else b.sample_type end   AS sample_type
, case when rtrim(ltrim(b.sample_freq)) ='' then null else b.sample_freq end    AS  sample_freq
, case when rtrim(ltrim(b.excursion)) ='' then null else b.excursion end  AS   excursion
, case when rtrim(ltrim(b.conc_unit)) ='' then null else b.conc_unit end  AS   conc_unit
, case when rtrim(ltrim(b.load_unit)) ='' then null else b.load_unit end  AS  load_unit 
--, b.load_1_prefix  
, case when rtrim(ltrim(b.load_1)) ='' then null else b.load_1 end  AS  load_1
--, b.load_1_nodi
--, b.load_2_prefix 
, case when rtrim(ltrim(b.load_2)) ='' then null else b.load_2 end  AS   load_2
--, b.load_2_nodi
--, b.conc_1_prefix
, case when rtrim(ltrim(b.conc_1)) ='' then null else b.conc_1 end  AS  conc_1
--, b.conc_1_nodi
--, b.conc_2_prefix
, case when rtrim(ltrim(b.conc_2)) ='' then null else b.conc_2 end   AS  conc_2
--, b.conc_2_nodi
--, b.conc_3_prefix
, case when rtrim(ltrim(b.conc_3)) ='' then null else b.conc_3 end   AS   conc_3
--, b.conc_3_nodi  
, a.created_dttm, a.created_by, a.updated_dttm,a.updated_by
from dbo.icis_dmr_report a inner join  icis_dmr_numeric_results b
							on a.submission_id_int = b.submission_id_int
								and a.transaction_id = b.transaction_id
								and a.monitor_pt_id = b.monitor_pt_id
								and a.monitor_pt_version = b.monitor_pt_version) a
where a.no_discharge_site is null
and ( a.permit_no is null or a.station_no is null or a.report_designator is null or a.mp_end_date is null or a.received_dttm is null
      or a.stage_code is null or a.LimitSeasonNumber is null or sample_type is null or sample_freq is null
      or ((len(a.load_1) + len(a.load_2) + len(a.conc_1) + len(a.conc_2) + len(a.conc_3)) =0 ) or  ((len(a.load_unit) +  len(a.conc_unit))  =0 ))



/*

select * from dbo.icis_dmr_report

select * from icis_dmr_numeric_results

select * from  dbo.WATER_02_ICIS_PREP


delete from dbo.icis_dmr_report

delete from icis_dmr_numeric_results

delete from  dbo.WATER_02_ICIS_PREP
*/
GO
/****** Object:  Table [dbo].[convert_pcs_to_icis]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[convert_pcs_to_icis](
	[LEGACY_TABLE_NAME] [nvarchar](255) NULL,
	[LEGACY_CODE] [nvarchar](255) NULL,
	[LEGACY_CODE_DESC] [nvarchar](255) NULL,
	[NEW_TABLE_NAME] [nvarchar](255) NULL,
	[NEW_CODE] [nvarchar](255) NULL,
	[NEW_CODE_DESC] [nvarchar](255) NULL,
	[NEW_CODE_SHORT_DESC] [nvarchar](255) NULL,
	[STATUS_FLAG] [nvarchar](255) NULL,
	[COMMENTS] [nvarchar](255) NULL,
	[LAST_UPDATED_DATE] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[his_icis_dmr]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[his_icis_dmr](
	[submission_id_int] [int] NOT NULL,
	[transaction_id] [int] NOT NULL,
	[station_id] [int] NOT NULL,
	[monitor_pt_id] [int] NOT NULL,
	[monitor_pt_version] [varchar](20) NOT NULL,
	[permit_no] [varchar](100) NULL,
	[station_no] [varchar](10) NOT NULL,
	[report_designator] [varchar](1) NULL,
	[mp_end_date] [datetime] NULL,
	[rpt_sign_date] [datetime] NULL,
	[PrincipalExecutiveOfficerFirstName] [varchar](50) NULL,
	[PrincipalExecutiveOfficerLastName] [varchar](50) NULL,
	[PrincipalExecutiveOfficerTitle] [varchar](20) NULL,
	[PrincipalExecutiveOfficerTelephone] [varchar](20) NULL,
	[SignatoryFirstName] [varchar](50) NULL,
	[SignatoryLastName] [varchar](50) NULL,
	[certifier_phone] [varchar](80) NULL,
	[comment] [varchar](2000) NULL,
	[no_discharge_site] [char](10) NULL,
	[received_dttm] [datetime] NULL,
	[par_code] [varchar](20) NULL,
	[stage_code] [varchar](10) NULL,
	[LimitSeasonNumber] [int] NULL,
	[sample_type] [varchar](100) NULL,
	[sample_freq] [varchar](100) NULL,
	[excursion] [varchar](10) NULL,
	[conc_unit] [varchar](10) NULL,
	[load_unit] [varchar](10) NULL,
	[load_1_prefix] [char](5) NOT NULL,
	[load_1] [varchar](50) NULL,
	[load_1_nodi] [varchar](5) NULL,
	[load_2_prefix] [char](5) NOT NULL,
	[load_2] [varchar](50) NULL,
	[load_2_nodi] [varchar](5) NULL,
	[conc_1_prefix] [char](5) NOT NULL,
	[conc_1] [varchar](20) NULL,
	[conc_1_nodi] [varchar](5) NULL,
	[conc_2_prefix] [char](5) NOT NULL,
	[conc_2] [varchar](20) NULL,
	[conc_2_nodi] [varchar](5) NULL,
	[conc_3_prefix] [char](5) NOT NULL,
	[conc_3] [varchar](20) NULL,
	[conc_3_nodi] [varchar](5) NULL,
	[submit_trans_id] [varchar](100) NOT NULL,
	[epa_trans_id] [varchar](100) NULL,
	[CREATED_BY] [varchar](30) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL DEFAULT (getdate()),
	[UPDATED_BY] [varchar](30) NOT NULL,
	[UPDATED_DATE] [datetime] NOT NULL DEFAULT (getdate()),
	[SUBMIT_STATUS] [char](1) NOT NULL DEFAULT ('S')
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[icis_dmr_numeric_results]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[icis_dmr_numeric_results](
	[submission_id_int] [int] NOT NULL,
	[transaction_id] [int] NOT NULL,
	[monitor_pt_id] [int] NOT NULL,
	[monitor_pt_version] [varchar](20) NOT NULL,
	[par_code] [varchar](20) NULL,
	[stage_code] [varchar](10) NULL,
	[LimitSeasonNumber] [int] NULL,
	[sample_type] [varchar](100) NULL,
	[sample_freq] [varchar](100) NULL,
	[excursion] [varchar](10) NULL,
	[conc_unit] [varchar](10) NULL,
	[load_unit] [varchar](10) NULL,
	[load_1_prefix] [char](5) NOT NULL,
	[load_1] [varchar](50) NULL,
	[load_1_nodi] [varchar](5) NULL,
	[load_2_prefix] [char](5) NOT NULL,
	[load_2] [varchar](50) NULL,
	[load_2_nodi] [varchar](5) NULL,
	[conc_1_prefix] [char](5) NOT NULL,
	[conc_1] [varchar](20) NULL,
	[conc_1_nodi] [varchar](5) NULL,
	[conc_2_prefix] [char](5) NOT NULL,
	[conc_2] [varchar](20) NULL,
	[conc_2_nodi] [varchar](5) NULL,
	[conc_3_prefix] [char](5) NOT NULL,
	[conc_3] [varchar](20) NULL,
	[conc_3_nodi] [varchar](5) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[icis_dmr_report]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[icis_dmr_report](
	[submission_id_int] [int] NOT NULL,
	[transaction_id] [int] NOT NULL,
	[station_id] [int] NOT NULL,
	[monitor_pt_id] [int] NOT NULL,
	[monitor_pt_version] [varchar](20) NOT NULL,
	[permit_no] [varchar](100) NULL,
	[station_no] [varchar](10) NOT NULL,
	[report_designator] [varchar](1) NULL,
	[mp_end_date] [datetime] NULL,
	[rpt_sign_date] [datetime] NULL,
	[PrincipalExecutiveOfficerFirstName] [varchar](50) NULL,
	[PrincipalExecutiveOfficerLastName] [varchar](50) NULL,
	[PrincipalExecutiveOfficerTitle] [varchar](20) NULL,
	[PrincipalExecutiveOfficerTelephone] [varchar](20) NULL,
	[SignatoryFirstName] [varchar](50) NULL,
	[SignatoryLastName] [varchar](50) NULL,
	[certifier_phone] [varchar](80) NULL,
	[comment] [varchar](2000) NULL,
	[no_discharge_site] [char](10) NULL,
	[received_dttm] [datetime] NULL,
	[created_dttm] [datetime] NOT NULL,
	[created_by] [char](25) NOT NULL,
	[updated_dttm] [datetime] NOT NULL,
	[updated_by] [char](25) NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[icis_dmr_report_data]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[icis_dmr_report_data](
	[submission_id_int] [int] NOT NULL,
	[transaction_id] [varchar](200) NOT NULL,
	[dmr_data] [varchar](max) NULL,
	[dmr_data_xml] [xml] NULL,
	[CREATED_BY] [varchar](30) NOT NULL,
	[CREATED_DATE] [datetime] NOT NULL DEFAULT (getdate())
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[icis_dmr_validate_null]    Script Date: 05/29/2012 16:06:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[icis_dmr_validate_null](
	[submission_id_int] [int] NOT NULL,
	[internal_trans_id] [int] NOT NULL,
	[permit_no] [varchar](100) NOT NULL,
	[station_no] [varchar](10) NOT NULL,
	[report_designator] [varchar](1) NOT NULL,
	[mp_end_date] [varchar](30) NOT NULL,
	[received_dttm] [varchar](30) NOT NULL,
	[stage_code] [varchar](10) NOT NULL,
	[LimitSeasonNumber] [varchar](30) NOT NULL,
	[sample_type] [varchar](100) NOT NULL,
	[sample_freq] [varchar](100) NOT NULL,
	[load_conc_value] [varchar](500) NOT NULL,
	[load_conc_unit] [varchar](100) NOT NULL,
	[created_dttm] [datetime] NOT NULL,
	[created_by] [char](100) NOT NULL,
	[updated_dttm] [datetime] NOT NULL,
	[updated_by] [char](100) NOT NULL,
	[transaction_id] [varchar](100) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

