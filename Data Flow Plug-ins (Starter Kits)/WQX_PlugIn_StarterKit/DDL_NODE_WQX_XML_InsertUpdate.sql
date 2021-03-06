set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO

IF EXISTS(SELECT NAME 
			FROM SYSOBJECTS 
		   WHERE NAME = N'WQX_XML_InsertUpdate' AND TYPE = 'P')
	DROP PROCEDURE WQX_XML_InsertUpdate
GO

CREATE PROCEDURE [dbo].[WQX_XML_InsertUpdate] 
-- =============================================
-- Author:				enfoTech
-- Create date:			5/19/2009
-- Last Update date:	09/30/2009  [SHC] Updated to default the LAST_RUN_DATE to use the last operation date
-- Description:			Generates Insert-Update version of XML for Node WQX Data Flow Plug-in
-- =============================================
@v_LAST_RUN_DATE datetime = NULL
AS
BEGIN

SET NOCOUNT ON;

DECLARE @intLOG_MSG_ID int;
DECLARE @intOCCURRENCE int;
DECLARE @strOrg varchar(max);  --ORG
DECLARE @xmlOrg xml;           --ORG
DECLARE @MyOrgUID int;         --ORG
DECLARE @strProj varchar(max);   --PROJ
DECLARE @strMon varchar(max);  --MONLOC
DECLARE @strBioIndex varchar(max);  --BIO INDX
DECLARE @strActivity varchar(max);  --ACTIVITY
DECLARE @strActGrp varchar(max);   -- ACTGROUP
DECLARE @strWQX varchar(max);
DECLARE @xmlWQX xml;

DECLARE @PRJ_QAPP_APPROVED_YN varchar(max);
DECLARE @PRJ_QAPP_APPROVAL_AGENCY_NAME varchar(max);

SELECT @PRJ_QAPP_APPROVED_YN = 'PRJ_QAPP_APPROVED_YN'
SELECT @PRJ_QAPP_APPROVAL_AGENCY_NAME = 'PRJ_QAPP_APPROVAL_AGENCY_NAME'

if @v_LAST_RUN_DATE IS NULL
	SELECT @v_LAST_RUN_DATE = Node2008.dbo.NODE_OPERATION_LOG.START_DTTM
	  FROM Node2008.dbo.NODE_OPERATION_LOG 
	  LEFT OUTER JOIN Node2008.dbo.NODE_OPERATION 
			ON Node2008.dbo.NODE_OPERATION_LOG.OPERATION_ID = Node2008.dbo.NODE_OPERATION.OPERATION_ID
	  LEFT OUTER JOIN Node2008.dbo.NODE_OPERATION_LOG_STATUS
			ON Node2008.dbo.NODE_OPERATION_LOG_STATUS.OPERATION_LOG_ID = Node2008.dbo.NODE_OPERATION_LOG.OPERATION_ID
	 WHERE rtrim(Node2008.dbo.NODE_OPERATION.OPERATION_NAME) = 'GenerateAndSubmitWQX'
	   AND Node2008.dbo.NODE_OPERATION_LOG_STATUS.STATUS_CD = 'Completed'

--**********************************************************
--************ORGANIZATIONS ********************************
--**********************************************************
--that either have MONLOC, PROJ, or ACTIVITY/RESULT (or ORG itself) updated
SELECT top 1 rtrim(ORGANIZATION.ORG_UID) AS OrgUID, 
                rtrim(ORGANIZATION.ORG_ID) AS OrganizationIdentifier,
                rtrim(ORGANIZATION.ORG_NAME) AS OrganizationFormalName, 
                rtrim(cast(ORGANIZATION.ORG_DESC AS varchar(max))) AS OrganizationDescriptionText,
				rtrim(ORGANIZATION.TRB_UID) AS TribalCode, 
				rtrim(ORG_ELECTRONIC_ADDRESS.ORGEA_TEXT) as ElectronicAddressText, 
				rtrim(ELECTRONIC_ADDRESS_TYPE.EATYP_NAME) as ElectronicAddressTypeName, 
				rtrim(ORG_PHONE.ORGPH_NUM) as TelephoneNumberText,
				rtrim(PHONE_TYPE.PHTYP_NAME) as TelephoneNumberTypeName, 
				rtrim(ORG_PHONE.ORGPH_EXT) as TelephoneExtensionNumberText, 
				rtrim(ADDRESS_TYPE.ADDTYP_NAME) as AddressTypeName, 
				rtrim(ORG_ADDRESS.ORGADD_ADDRESS) as  AddressText,
				rtrim(ORG_ADDRESS.ORGADD_ADDRESS_SUPPLEMENTAL) AS SupplementalAddressText, 
				rtrim(ORG_ADDRESS.ORGADD_LOCALITY_NAME) as LocalityName, 
				rtrim(STATE.ST_CD) as StateCode, 
				rtrim(ORG_ADDRESS.ORGADD_POSTAL_CD) as PostalCode,
				rtrim(COUNTRY.CNTRY_CD) as CountryCode,
				rtrim(COUNTY.CNTY_FIPS_CD) as CountyCode 
INTO  [#OrganizationRS]
FROM  ORGANIZATION 
LEFT OUTER JOIN PROJECT ON ORGANIZATION.ORG_UID = PROJECT.ORG_UID 
LEFT OUTER JOIN MONITORING_LOCATION on ORGANIZATION.ORG_UID = MONITORING_LOCATION.ORG_UID
LEFT OUTER JOIN ACTIVITY on ORGANIZATION.ORG_UID = ACTIVITY.ORG_UID
LEFT OUTER JOIN TRIBE ON TRIBE.TRB_UID = ORGANIZATION.TRB_UID
LEFT OUTER JOIN ORG_ELECTRONIC_ADDRESS ON ORGANIZATION.ORG_UID = ORG_ELECTRONIC_ADDRESS.ORG_UID
LEFT OUTER JOIN ELECTRONIC_ADDRESS_TYPE ON ORG_ELECTRONIC_ADDRESS.EATYP_UID = ELECTRONIC_ADDRESS_TYPE.EATYP_UID
LEFT OUTER JOIN ORG_PHONE ON ORGANIZATION.ORG_UID = ORG_PHONE.ORG_UID
LEFT OUTER JOIN PHONE_TYPE ON PHONE_TYPE.PHTYP_UID = ORG_PHONE.PHTYP_UID
LEFT OUTER JOIN ORG_ADDRESS ON ORGANIZATION.ORG_UID = ORG_ADDRESS.ORG_UID
LEFT OUTER JOIN ADDRESS_TYPE ON ORG_ADDRESS.ADDTYP_UID = ADDRESS_TYPE.ADDTYP_UID
LEFT OUTER JOIN STATE ON ORG_ADDRESS.ST_UID = STATE.ST_UID
LEFT OUTER JOIN COUNTRY ON ORG_ADDRESS.CNTRY_UID = COUNTRY.CNTRY_UID
LEFT OUTER JOIN COUNTY ON ORG_ADDRESS.CNTY_UID = COUNTY.CNTY_UID
where (ORG_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE
or PRJ_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE
or MLOC_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE
or ACT_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE)
ORDER BY OrganizationIdentifier

select @strOrg =(
 SELECT OrganizationIdentifier AS "OrganizationDescription/OrganizationIdentifier", 
        OrganizationFormalName AS "OrganizationDescription/OrganizationFormalName", 
        OrganizationDescriptionText AS "OrganizationDescription/OrganizationDescriptionText", 
        TribalCode AS "OrganizationDescription/TribalCode", 
        ElectronicAddressText as "ElectronicAddress/ElectronicAddressText", 
        ElectronicAddressTypeName as "ElectronicAddress/ElectronicAddressTypeName", 
        TelephoneNumberText as "Telephonic/TelephoneNumberText",
        TelephoneNumberTypeName as "Telephonic/TelephoneNumberTypeName", 
        TelephoneExtensionNumberText as "Telephonic/TelephoneExtensionNumberText", 
        AddressTypeName as "OrganizationAddress/AddressTypeName", 
        AddressText as  "OrganizationAddress/AddressText",
        SupplementalAddressText AS "OrganizationAddress/SupplementalAddressText", 
        LocalityName as "OrganizationAddress/LocalityName", 
        StateCode as "OrganizationAddress/StateCode", 
        PostalCode as "OrganizationAddress/PostalCode",
        CountryCode as "OrganizationAddress/CountryCode",
        CountyCode as "OrganizationAddress/CountyCode" 
  from #OrganizationRS  for xml path('')
 )

select @MyOrgUID = OrgUID from #OrganizationRS;

--**********************************************************
--************PROJECTS      ********************************
--**********************************************************
set @strProj = ''
select @strProj = (
select distinct rtrim(PRJ_ID) as "Project/ProjectIdentifier",
				  isnull(rtrim(PRJ_NAME), '') as "Project/ProjectName", 
				  isnull(rtrim(CAST(PRJ_DESC as varchar(max))),'')  as "Project/ProjectDescriptionText",
				  NULL as "Project/SamplingDesignTypeCode",
				  (SELECT CASE WHEN rtrim(@PRJ_QAPP_APPROVED_YN) = 'Y' THEN 'true' ELSE 'false' END as "Project/QAPPApprovedIndicator" 
					FROM PROJECT 
					WHERE EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
				  WHERE TABLE_NAME = 'PROJECT' AND COLUMN_NAME = @PRJ_QAPP_APPROVED_YN)),
				  (SELECT isnull(rtrim(@PRJ_QAPP_APPROVAL_AGENCY_NAME),'') as "Project/QAPPApprovalAgencyName" 
					FROM PROJECT 
					WHERE EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
				  WHERE TABLE_NAME = 'PROJECT' AND COLUMN_NAME = @PRJ_QAPP_APPROVAL_AGENCY_NAME)),
				  NULL AS "Project/AttachedBinaryObject/BinaryObjectFileName",
				  NULL AS "Project/AttachedBinaryObject/BinaryObjectFileTypeCode" 
	from PROJECT 
    where ORG_UID = @MyOrgUID AND PRJ_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE
    for xml path('')
)
--ORDER BY PRJ_ID

--select @strProj = @strProj + replace(replace((@strProj), '&lt;', '<'),'&gt;', '>')

SELECT @PRJ_QAPP_APPROVED_YN = 'PRJ_QAPP_APPROVED_YN'
SELECT @PRJ_QAPP_APPROVAL_AGENCY_NAME = 'PRJ_QAPP_APPROVAL_AGENCY_NAME'
--****************************************************************
--************MONITORING_LOCATION ********************************
--****************************************************************
set @strMon = ''
select @strMon = (
Select distinct rtrim(MLOC_ID) as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationIdentifier",
       isnull(rtrim(MLOC_NAME),'')as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationName",
            (SELECT rtrim(MLTYP_NAME)
             FROM MONITORING_LOCATION_TYPE
             WHERE (MLTYP_UID = MONITORING_LOCATION.MLTYP_UID))  AS "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationTypeName", 
       isnull(rtrim(cast(MLOC_DESC as varchar(max))), '') as "MonitoringLocation/MonitoringLocationIdentity/MonitoringLocationDescriptionText",
       rtrim(MLOC_HUC_8)  as "MonitoringLocation/MonitoringLocationIdentity/HUCEightDigitCode",
       rtrim(MLOC_HUC_12) AS "MonitoringLocation/MonitoringLocationIdentity/HUCTwelveDigitCode",
       rtrim(MLOC_TRIBAL_LAND_YN) AS "MonitoringLocation/MonitoringLocationIdentity/TribalLandIndicator", 
       rtrim(MLOC_TRIBAL_LAND_NAME) AS "MonitoringLocation/MonitoringLocationIdentity/TribalLandName",
       NULL AS "MonitoringLocation/MonitoringLocationIdentity/AlternateMonitoringLocationIdentity/MonitoringLocationIdentifier",
       NULL AS "MonitoringLocation/MonitoringLocationIdentity/AlternateMonitoringLocationIdentity/MonitoringLocationIdentifierContext",
	   ISNULL(rtrim(MLOC_LATITUDE), 0) as "MonitoringLocation/MonitoringLocationGeospatial/LatitudeMeasure",
       ISNULL(rtrim(MLOC_LONGITUDE), 0) as "MonitoringLocation/MonitoringLocationGeospatial/LongitudeMeasure",
       rtrim(MLOC_SOURCE_MAP_SCALE) AS "MonitoringLocation/MonitoringLocationGeospatial/SourceMapScaleNumeric", 
       --rtrim(MLOC_HORIZONTAL_ACCURACY) AS "MonitoringLocation/MonitoringLocationGeospatial/HorizontalAccuracyMeasure/MeasureValue",
--             (SELECT rtrim(MSUNT_CD)
--              FROM MEASUREMENT_UNIT
--              WHERE (MSUNT_UID = MONITORING_LOCATION.MSUNT_UID_HORIZONTAL_ACCURACY))  AS "MonitoringLocation/MonitoringLocationGeospatial/HorizontalAccuracyMeasure/MeasureUnitCode", 
             (SELECT distinct rtrim(HORIZONTAL_COLLECTION_METHOD.HCMTH_NAME)
              FROM HORIZONTAL_COLLECTION_METHOD, MONITORING_LOCATION
              WHERE (HORIZONTAL_COLLECTION_METHOD.HCMTH_UID = MONITORING_LOCATION.HCMTH_UID)) AS "MonitoringLocation/MonitoringLocationGeospatial/HorizontalCollectionMethodName", 
             (SELECT rtrim(HRDAT_NAME)
              FROM HORIZONTAL_REFERENCE_DATUM
              WHERE (HRDAT_UID = MONITORING_LOCATION.HRDAT_UID))  AS "MonitoringLocation/MonitoringLocationGeospatial/HorizontalCoordinateReferenceSystemDatumName",
       CASE WHEN MLOC_VERTICAL_MEASURE IS NULL THEN NULL ELSE rtrim(MLOC_VERTICAL_MEASURE) END AS "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureValue", 
--       CASE WHEN MLOC_VERTICAL_MEASURE IS NULL THEN NULL ELSE (select rtrim(MSUNT_CD) from MEASUREMENT_UNIT where MSUNT_UID = MONITORING_LOCATION.MSUNT_UID_VERTICAL_MEASURE) END AS "MonitoringLocation/MonitoringLocationGeospatial/VerticalMeasure/MeasureUnitCode", 
             (SELECT rtrim(VCMTH_NAME)
              FROM VERTICAL_COLLECTION_METHOD
              WHERE (VCMTH_UID = MONITORING_LOCATION.VCMTH_UID))  AS "MonitoringLocation/MonitoringLocationGeospatial/VerticalCollectionMethodName", 
             (SELECT rtrim(VRDAT_NAME)
              FROM VERTICAL_REFERENCE_DATUM
              WHERE (VRDAT_UID = MONITORING_LOCATION.VRDAT_UID))  AS "MonitoringLocation/MonitoringLocationGeospatial/VerticalCoordinateReferenceSystemDatumName",  
       'US' as  "MonitoringLocation/MonitoringLocationGeospatial/CountryCode", 
       'OK' as "MonitoringLocation/MonitoringLocationGeospatial/StateCode", 
             (SELECT rtrim(CNTY_FIPS_CD)
              FROM COUNTY
              WHERE (CNTY_UID = MONITORING_LOCATION.CNTY_UID))  as "MonitoringLocation/MonitoringLocationGeospatial/CountyCode" 
       from MONITORING_LOCATION WHERE ORG_UID = @MyOrgUID AND MLOC_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE for xml path('')
    )


--****************************************************************
--************BIO INDEX            *******************************
--****************************************************************
--set @strBioIndex = ''
--select @strBioIndex = (
--Select rtrim(BHIDX_ID) as "IndexIdentifier",
--                rtrim(IDXTYP_ID) AS "IndexType/IndexTypeIdentifier", 
--                rtrim(IDXTYP_CONTEXT) AS "IndexType/IndexTypeIdentifierContext", 
--                rtrim(IDXTYP_NAME) AS "IndexType/IndexTypeName", 
--                rtrim(IDXTYP_SCALE) AS "IndexType/IndexTypeScaleText", 
--                rtrim(BHIDX_SCORE) AS "IndexScoreNumeric", 
--                rtrim(BHIDX_QUALIFIER_CD) AS "IndexQualifierCode", 
--                rtrim(BHIDX_COMMENT) AS "IndexCommentText", 
--                rtrim(BHIDX_CALCULATED_DATE) AS "IndexCalculatedDate",
--                rtrim(MLOC_ID) AS "MonitoringLocationIdentifier"
--       from BIOLOGICAL_HABITAT_INDEX a, MONITORING_LOCATION b, INDEX_TYPE c
--       WHERE a.ORG_UID = @MyOrgUID 
--             AND a.MLOC_UID = b.MLOC_UID
--			 AND a.IDXTYP_UID = c.IDXTYP_UID
--             AND BHIDX_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE 
--			 AND BHIDX_ID IS NOT NULL 
--             AND BHIDX_SCORE IS NOT NULL
--		     AND IDXTYP_CONTEXT IS NOT NULL
--			 AND IDXTYP_NAME IS NOT NULL
--       for xml path('BiologicalHabitatIndex')
--    )



--***********************************************************
--************ACTIVITY       ********************************
--***********************************************************
set @strActivity = ''
select @strActivity = (
select  rtrim(ACTIVITY.ACT_ID) as "ActivityDescription/ActivityIdentifier", 
             (SELECT rtrim(ACTYP_CD)
              FROM ACTIVITY_TYPE
              WHERE (ACTYP_UID = ACTIVITY.ACTYP_UID))  AS "ActivityDescription/ActivityTypeCode", 
             (SELECT rtrim(ACMED_NAME)
              FROM ACTIVITY_MEDIA
              WHERE (ACMED_UID = ACTIVITY.ACMED_UID))  AS "ActivityDescription/ActivityMediaName", 
             (SELECT rtrim(AMSUB_NAME)
              FROM ACTIVITY_MEDIA_SUBDIVISION
              WHERE (AMSUB_UID = ACTIVITY.AMSUB_UID))  AS "ActivityDescription/ActivityMediaSubdivisionName", 
         LEFT(CONVERT(VARCHAR, ISNULL(ACTIVITY.ACT_START_DATE, ACTIVITY.ACT_END_DATE), 120), 10) AS "ActivityDescription/ActivityStartDate",  
		 RIGHT(CONVERT(VARCHAR, ACTIVITY.ACT_START_TIME, 120), 8) AS "ActivityDescription/ActivityStartTime/Time", 
             CASE WHEN ACT_START_TIME IS NOT NULL THEN 
             CASE WHEN TMZONE_UID_START_TIME IS NOT NULL THEN 
              (SELECT rtrim(TMZONE_CD) FROM TIME_ZONE
              WHERE (TMZONE_UID = ACTIVITY.TMZONE_UID_START_TIME)) ELSE 'CST' END ELSE NULL END AS "ActivityDescription/ActivityStartTime/TimeZoneCode", 
         LEFT(CONVERT(VARCHAR, ACTIVITY.ACT_END_DATE, 120), 10) as "ActivityDescription/ActivityEndDate", 
         RIGHT(CONVERT(VARCHAR, ACTIVITY.ACT_END_TIME, 120), 8) AS "ActivityDescription/ActivityEndTime/Time" , 
             CASE WHEN ACT_END_TIME IS NOT NULL THEN 
             CASE WHEN TMZONE_UID_END_TIME IS NOT NULL THEN 
              (SELECT rtrim(TMZONE_CD) FROM TIME_ZONE
              WHERE (TMZONE_UID = ACTIVITY.TMZONE_UID_END_TIME)) ELSE 'CST' END ELSE NULL END AS "ActivityDescription/ActivityEndTime/TimeZoneCode", 
             (SELECT rtrim(RELDPTH_NAME)
              FROM RELATIVE_DEPTH
              WHERE (RELDPTH_UID = ACTIVITY.RELDPTH_UID))  AS "ActivityDescription/ActivityRelativeDepthName", 
         rtrim(ACT_DEPTH_HEIGHT) AS "ActivityDescription/ActivityDepthHeightMeasure/MeasureValue" , 
         CASE WHEN ACTIVITY.ACT_DEPTH_HEIGHT IS NULL THEN NULL ELSE (SELECT rtrim(MSUNT_CD) FROM MEASUREMENT_UNIT WHERE (MSUNT_UID = ACTIVITY.MSUNT_UID_DEPTH_HEIGHT)) END AS "ActivityDescription/ActivityDepthHeightMeasure/MeasureUnitCode" ,
         rtrim(ACT_DEPTH_HEIGHT_TOP) AS "ActivityDescription/ActivityTopDepthHeightMeasure/MeasureValue" , 
         CASE WHEN ACTIVITY.ACT_DEPTH_HEIGHT_TOP IS NULL THEN NULL ELSE (SELECT rtrim(MSUNT_CD) FROM MEASUREMENT_UNIT WHERE (MSUNT_UID = ACTIVITY.MSUNT_UID_DEPTH_HEIGHT_TOP)) END AS "ActivityDescription/ActivityTopDepthHeightMeasure/MeasureUnitCode" ,
         rtrim(ACT_DEPTH_HEIGHT_BOTTOM) AS "ActivityDescription/ActivityBottomDepthHeightMeasure/MeasureValue" , 
         CASE WHEN ACTIVITY.ACT_DEPTH_HEIGHT_BOTTOM IS NULL THEN NULL ELSE (SELECT rtrim(MSUNT_CD) FROM MEASUREMENT_UNIT WHERE (MSUNT_UID = ACTIVITY.MSUNT_UID_DEPTH_HEIGHT_BOTTOM)) END AS "ActivityDescription/ActivityBottomDepthHeightMeasure/MeasureUnitCode" ,
         rtrim(ACT_DEPTH_ALTITUDE_REF_POINT) as "ActivityDescription/ActivityDepthAltitudeReferencePointText", 
				(SELECT top 1 rtrim(aa.PRJ_ID) 
                  FROM PROJECT aa, ACTIVITY_PROJECT bb 
                  where aa.PRJ_UID = bb.PRJ_UID and bb.ACT_UID = ACTIVITY.ACT_UID) AS "ActivityDescription/ProjectIdentifier",
         'Kaw Nation' as "ActivityDescription/ActivityConductingOrganizationText",
             (SELECT rtrim(MLOC_ID)
              FROM MONITORING_LOCATION
              WHERE (MLOC_UID = ACTIVITY.MLOC_UID))  AS "ActivityDescription/MonitoringLocationIdentifier", 
         cast(ACT_COMMENTS as varchar(max)) as "ActivityDescription/ActivityCommentText",
		 rtrim(ACT_LOC_LATITUDE) AS "ActivityLocation/LatitudeMeasure",
		 CASE WHEN ACT_LOC_LATITUDE IS NOT NULL THEN rtrim(ACT_LOC_LONGITUDE) ELSE NULL END AS "ActivityLocation/LongitudeMeasure",
		 CASE WHEN ACT_LOC_LATITUDE IS NOT NULL THEN rtrim(ACT_LOC_SOURCE_MAP_SCALE) ELSE NULL END AS "ActivityLocation/SourceMapScaleNumeric",
--		 CASE WHEN ACT_LOC_LATITUDE IS NOT NULL THEN rtrim(ACT_HORIZONTAL_ACCURACY) ELSE NULL END AS "ActivityLocation/HorizontalAccuracyMeasure/MeasureValue",
--         CASE WHEN ACT_LOC_LATITUDE IS NOT NULL THEN (SELECT rtrim(MSUNT_CD)
--              FROM MEASUREMENT_UNIT
--              WHERE (MSUNT_UID = ACTIVITY.MSUNT_UID_HORIZONTAL_ACCURACY)) ELSE NULL END AS "ActivityLocation/HorizontalAccuracyMeasure/MeasureUnitCode", 
             CASE WHEN ACT_LOC_LATITUDE IS NOT NULL THEN --only populate if lat/long are supplied
             CASE WHEN HCMTH_UID IS NULL THEN 'Unknown' ELSE 
               (SELECT rtrim(HCMTH_NAME)
               FROM HORIZONTAL_COLLECTION_METHOD
               WHERE (HCMTH_UID = ACTIVITY.HCMTH_UID)) END ELSE NULL END AS "ActivityLocation/HorizontalCollectionMethodName", 
             CASE WHEN ACT_LOC_LATITUDE IS NOT NULL THEN   --only populate if lat/long are supplied
             CASE WHEN HCMTH_UID IS NULL THEN 'Other' ELSE 
             (SELECT rtrim(HRDAT_NAME)
              FROM HORIZONTAL_REFERENCE_DATUM
              WHERE (HRDAT_UID = ACTIVITY.HRDAT_UID)) END ELSE NULL END AS "ActivityLocation/HorizontalCoordinateReferenceSystemDatumName",
--             (SELECT rtrim(ASMBLG_NAME)
--              FROM ASSEMBLAGE
--              WHERE (ASMBLG_UID = ACTIVITY.ASMBLG_UID))  AS "BiologicalActivityDescription/AssemblageSampledName",
		 rtrim(ACT_SAM_COLLECT_METH_ID) AS "SampleDescription/SampleCollectionMethod/MethodIdentifier",
		 rtrim(ACT_SAM_COLLECT_METH_CONTEXT) AS "SampleDescription/SampleCollectionMethod/MethodIdentifierContext",
		 rtrim(ACT_SAM_COLLECT_METH_NAME) AS "SampleDescription/SampleCollectionMethod/MethodName",
		 rtrim(ACT_SAM_COLLECT_METH_QUAL_TYPE) AS "SampleDescription/SampleCollectionMethod/MethodQualifierTypeName",
		 rtrim(cast(ACT_SAM_COLLECT_METH_DESC AS varchar(max))) AS "SampleDescription/SampleCollectionMethod/MethodDescriptionText",
             (SELECT rtrim(SCEQP_NAME)
              FROM SAMPLE_COLLECTION_EQUIP
              WHERE (SCEQP_UID = ACTIVITY.SCEQP_UID))  AS "SampleDescription/SampleCollectionEquipmentName",
		 rtrim(cast(ACT_SAM_COLLECT_EQUIP_COMMENTS AS varchar(max))) AS "SampleDescription/SampleCollectionEquipmentCommentText",
		 rtrim(ACT_SAM_PREP_METH_ID) AS "SampleDescription/SamplePreparation/SamplePreparationMethod/MethodIdentifier",
		 rtrim(ACT_SAM_PREP_METH_CONTEXT) AS "SampleDescription/SamplePreparation/SamplePreparationMethod/MethodIdentifierContext",
		 rtrim(ACT_SAM_PREP_METH_NAME) AS "SampleDescription/SamplePreparation/SamplePreparationMethod/MethodName",
		 rtrim(ACT_SAM_PREP_METH_QUAL_TYPE) AS "SampleDescription/SamplePreparation/SamplePreparationMethod/MethodQualifierTypeName",
		 rtrim(cast(ACT_SAM_PREP_METH_DESC AS varchar(max))) AS "SampleDescription/SamplePreparation/SamplePreparationMethod/MethodDescriptionText",

			--****************************	
			--RESULT BEGINS HERE			
			--****************************	
			(SELECT rtrim(RES_DATA_LOGGER_LINE) AS "ResultDescription/DataLoggerLineName",
                       (SELECT rtrim(RDCND_NAME)
						FROM RESULT_DETECTION_CONDITION
						WHERE (RDCND_UID = RESULT.RDCND_UID))  AS "ResultDescription/ResultDetectionConditionText", 
                       (SELECT rtrim(CHR_NAME)
						FROM CHARACTERISTIC
						WHERE (CHR_UID = RESULT.CHR_UID))  AS "ResultDescription/CharacteristicName",
--                       (SELECT rtrim(MTHSPC_NAME)
--						FROM METHOD_SPECIATION
--						WHERE (MTHSPC_UID = RESULT.MTHSPC_UID))  AS "ResultDescription/MethodSpeciationName",
                       (SELECT rtrim(SMFRC_NAME)
						FROM SAMPLE_FRACTION
						WHERE (SMFRC_UID = RESULT.SMFRC_UID))  AS "ResultDescription/ResultSampleFractionText",
					rtrim(RES_MEASURE) AS "ResultDescription/ResultMeasure/ResultMeasureValue",
                       (SELECT rtrim(MSUNT_CD)
						FROM MEASUREMENT_UNIT
						WHERE (MSUNT_UID = RESULT.MSUNT_UID_MEASURE))  AS "ResultDescription/ResultMeasure/MeasureUnitCode",
                       (SELECT rtrim(RMQLF_CD)
						FROM RESULT_MEASURE_QUALIFIER
						WHERE (RMQLF_UID = RESULT.RMQLF_UID))  AS "ResultDescription/ResultMeasure/MeasureQualifierCode",
                       (SELECT rtrim(RESSTA_NAME)
						FROM RESULT_STATUS
						WHERE (RESSTA_UID = RESULT.RESSTA_UID))  AS "ResultDescription/ResultStatusIdentifier",
                       (SELECT rtrim(RSBAS_CD)
						FROM RESULT_STATISTICAL_BASE
						WHERE (RSBAS_UID = RESULT.RSBAS_UID))  AS "ResultDescription/StatisticalBaseCode",
                       (SELECT rtrim(RVTYP_NAME)
						FROM RESULT_VALUE_TYPE
						WHERE (RVTYP_UID = RESULT.RVTYP_UID))  AS "ResultDescription/ResultValueTypeName",
                       (SELECT rtrim(RWBAS_NAME)
						FROM RESULT_WEIGHT_BASIS
						WHERE (RWBAS_UID = RESULT.RWBAS_UID))  AS "ResultDescription/ResultWeightBasisText",
                       (SELECT rtrim(RTIMB_NAME)
						FROM RESULT_TIME_BASIS
						WHERE (RTIMB_UID = RESULT.RTIMB_UID))  AS "ResultDescription/ResultTimeBasisText",
                       (SELECT rtrim(RTMPB_NAME)
						FROM RESULT_TEMPERATURE_BASIS
						WHERE (RTMPB_UID = RESULT.RTMPB_UID))  AS "ResultDescription/ResultTemperatureBasisText",
					rtrim(RES_PARTICLE_SIZE_BASIS) AS "ResultDescription/ResultParticleSizeBasisText",
					rtrim(RES_MEASURE_PRECISION) AS "ResultDescription/DataQuality/PrecisionValue",
					rtrim(RES_MEASURE_BIAS) AS "ResultDescription/DataQuality/BiasValue",
					rtrim(RES_MEASURE_CONF_INTERVAL) AS "ResultDescription/DataQuality/ConfidenceIntervalValue",
					rtrim(RES_MEASURE_UPPER_CONF_LIMIT) AS "ResultDescription/DataQuality/UpperConfidenceLimitValue",
					rtrim(RES_MEASURE_LOWER_CONF_LIMIT) AS "ResultDescription/DataQuality/LowerConfidenceLimitValue",
					rtrim(cast(RES_COMMENTS AS varchar(max))) AS "ResultDescription/ResultCommentText",
					CASE WHEN MSUNT_UID_DEPTH_HEIGHT IS NULL THEN NULL ELSE rtrim(RES_DEPTH_HEIGHT) END AS "ResultDescription/ResultDepthHeightMeasure/MeasureValue",
                    CASE WHEN RES_DEPTH_HEIGHT IS NULL THEN NULL ELSE 
                       (SELECT rtrim(MSUNT_CD)
						FROM MEASUREMENT_UNIT
						WHERE (MSUNT_UID = RESULT.MSUNT_UID_DEPTH_HEIGHT)) END AS "ResultDescription/ResultDepthHeightMeasure/MeasureUnitCode",
					rtrim(RES_DEPTH_ALTITUDE_REF_POINT) AS "ResultDescription/ResultDepthAltitudeReferencePointText",
--					rtrim(RES_SAMPLING_POINT_NAME) AS "ResultDescription/ResultSamplingPointName",
--                    CASE WHEN (TAX_UID IS NULL or BIOINT_UID IS NULL) THEN NULL ELSE 
--                       (SELECT rtrim(BIOINT_NAME)
--						FROM BIOLOGICAL_INTENT
--						WHERE (BIOINT_UID = RESULT.BIOINT_UID)) END AS "BiologicalResultDescription/BiologicalIntentName",
----					CASE WHEN (TAX_UID IS NULL or BIOINT_UID IS NULL) THEN NULL ELSE rtrim(RES_BIO_INDIVIDUAL_ID) END AS "BiologicalResultDescription/BiologicalIndividualIdentifier",
--                    CASE WHEN (TAX_UID IS NULL or BIOINT_UID IS NULL) THEN NULL ELSE 
--                       (SELECT rtrim(TAX_NAME)
--						FROM TAXON
--						WHERE (TAX_UID = RESULT.TAX_UID)) END AS "BiologicalResultDescription/SubjectTaxonomicName",
--					CASE WHEN (TAX_UID IS NULL or BIOINT_UID IS NULL) THEN NULL ELSE rtrim(RES_SPECIES_ID) END AS "BiologicalResultDescription/UnidentifiedSpeciesIdentifier",
--                    CASE WHEN (TAX_UID IS NULL or BIOINT_UID IS NULL) THEN NULL ELSE 
--                       (SELECT rtrim(STANT_NAME)
--						FROM SAMPLE_TISSUE_ANATOMY
--						WHERE (STANT_UID = RESULT.STANT_UID)) END AS "BiologicalResultDescription/SampleTissueAnatomyName",
						(SELECT rtrim(ANLMTH_ID)
						FROM ANALYTICAL_METHOD
						WHERE (ANLMTH_UID = RESULT.ANLMTH_UID))  AS "ResultAnalyticalMethod/MethodIdentifier",
						(SELECT rtrim(AMCTX_CD)
						FROM ANALYTICAL_METHOD, ANALYTICAL_METHOD_CONTEXT
						WHERE (ANALYTICAL_METHOD.AMCTX_UID = ANALYTICAL_METHOD_CONTEXT.AMCTX_UID and ANLMTH_UID = RESULT.ANLMTH_UID))  AS "ResultAnalyticalMethod/MethodIdentifierContext",
						(SELECT rtrim(ANLMTH_NAME)
						FROM ANALYTICAL_METHOD
						WHERE (ANLMTH_UID = RESULT.ANLMTH_UID))  AS "ResultAnalyticalMethod/MethodName",
						(SELECT rtrim(ANLMTH_QUAL_TYPE)
						FROM ANALYTICAL_METHOD
						WHERE (ANLMTH_UID = RESULT.ANLMTH_UID))  AS "ResultAnalyticalMethod/MethodQualifierTypeName",
						(SELECT rtrim(cast(ANLMTH_DESC AS varchar(max)))
						FROM ANALYTICAL_METHOD
						WHERE (ANLMTH_UID = RESULT.ANLMTH_UID))  AS "ResultAnalyticalMethod/MethodDescriptionText",
					rtrim(RES_LAB_NAME) AS "ResultLabInformation/LaboratoryName",
 				    LEFT(CONVERT(VARCHAR, RES_LAB_ANALYSIS_START_DATE, 120), 10) as "ResultLabInformation/AnalysisStartDate", 
					RIGHT(CONVERT(VARCHAR, RES_LAB_ANALYSIS_START_TIME, 120), 8) AS "ResultLabInformation/AnalysisStartTime/Time" , 
					 CASE WHEN RES_LAB_ANALYSIS_START_TIME IS NOT NULL THEN 
					 CASE WHEN TMZONE_UID_LAB_ANALYSIS_START IS NOT NULL THEN 
   					    (SELECT rtrim(TMZONE_CD) FROM TIME_ZONE
					     WHERE (TMZONE_UID = TMZONE_UID_LAB_ANALYSIS_START)) ELSE 'CST' END ELSE NULL END AS "ResultLabInformation/AnalysisStartTime/TimeZoneCode", 
 				    LEFT(CONVERT(VARCHAR, RES_LAB_ANALYSIS_END_DATE, 120), 10) as "ResultLabInformation/AnalysisEndDate", 
					RIGHT(CONVERT(VARCHAR, RES_LAB_ANALYSIS_END_TIME, 120), 8) AS "ResultLabInformation/AnalysisEndTime/Time" , 
					 CASE WHEN RES_LAB_ANALYSIS_END_TIME IS NOT NULL THEN 
					 CASE WHEN TMZONE_UID_LAB_ANALYSIS_END IS NOT NULL THEN 
   					    (SELECT rtrim(TMZONE_CD) FROM TIME_ZONE
					     WHERE (TMZONE_UID = TMZONE_UID_LAB_ANALYSIS_END)) ELSE 'CST' END ELSE NULL END AS "ResultLabInformation/AnalysisEndTime/TimeZoneCode",
						(SELECT rtrim(RLCOM_CD)
						FROM RESULT_LAB_COMMENT
						WHERE (RLCOM_UID = RESULT.RLCOM_UID))  AS "ResultLabInformation/ResultLaboratoryCommentCode"
                  FROM RESULT 
                  where ACT_UID = ACTIVITY.ACT_UID for xml path('Result'),type ) 
        from ACTIVITY, ACTIVITY_PROJECT  
		where  ACTIVITY.ACT_UID = ACTIVITY_PROJECT.ACT_UID 
         and ACT_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE
		for xml path('Activity')
    )


--****************************************************************
--************ACTIVITY_GROUP      ********************************
--****************************************************************
set @strActGrp = ''
select @strActGrp = (
Select rtrim(ACTGRP_ID) as "ActivityGroupIdentifier",
                rtrim(ACTGRP_NAME) AS "ActivityGroupName", 
                 (SELECT rtrim(AGTYP_NAME)
                  FROM ACTIVITY_GROUP_TYPE
                  WHERE (AGTYP_UID = a.AGTYP_UID))  AS "ActivityGroupTypeCode", 
                (SELECT distinct rtrim(aa.ACT_ID) AS "ActivityIdentifier"
                  FROM ACTIVITY aa, ACTIVITY_GROUP_DETAIL bb 
                  where aa.ACT_UID = bb.ACT_UID and bb.ACTGRP_UID = a.ACTGRP_UID for xml path(''),type )  
       from ACTIVITY_GROUP a WHERE a.ORG_UID = @MyOrgUID AND ACTGRP_LAST_CHANGE_DATE >= @v_LAST_RUN_DATE for xml path('ActivityGroup')
    )

-- *************************************************************************
-- ***************** PUT ALL PIECES TOGETHER *******************************
-- *************************************************************************
select @strWQX = '<?xml version="1.0" encoding="UTF-8"?>
<Document Id="MICH21_WQX_' + convert(varchar, getdate(), 112) + '" xmlns="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd">
	<Header>
		<Author>Jason Smith</Author>
		<Organization>MICH21_WQX</Organization>
		<Title>WQX</Title>
		<CreationTime>' + LEFT(CONVERT(varchar, getdate(), 120), 10) + 'T' + RIGHT(convert(varchar, getdate(), 120), 8) + '</CreationTime>
		<ContactInfo>SmithJ18@michigan.gov</ContactInfo>
	</Header>
	<Payload Operation="Update-Insert">
<WQX xmlns="http://www.exchangenetwork.net/schema/wqx/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xsi:schemaLocation="http://www.exchangenetwork.net/schema/wqx/2 
http://www.exchangenetwork.net/schema/WQX/2/0/WQX_WQX_v2.0.xsd">
	<Organization>' + @strOrg + isnull(@strProj,'') + isnull(@strMon,'') + isnull(@strBioIndex,'') + isnull(@strActivity,'') + isnull(@strActGrp,'') + '</Organization></WQX></Payload></Document>'

select @xmlWQX = cast((@strWQX) as xml)
select @xmlWQX
--
--insert into TRANSACTION_LOG (TRLOG_UID, TRLOG_START_TIME) 
--	select max(TRLOG_UID)+1, getdate() from TRANSACTION_LOG

END


