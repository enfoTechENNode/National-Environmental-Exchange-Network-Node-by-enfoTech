create or replace procedure ICIS_R_UNPERMITTEDFACILITY
( p_start_date IN VARCHAR2 
, p_output OUT CLOB)
AS

v_start_date DATE;
v_Activity_id VARCHAR2(4000);
v_FACILITY_INTEREST_ID VARCHAR2(4000); 
v_xml_file xmlTYPE;
v_NAAS_ID VARCHAR2(100);
v_Author VARCHAR2(100);
v_Organization VARCHAR2(100);
v_ContactInfo VARCHAR2(100);
v_Email_Address  VARCHAR2(100);

v_YEAR NUMBER(4,0);
v_MONTH NUMBER(2,0);
v_DAY NUMBER(2,0);
v_HOUR NUMBER(2,0);
v_MINUTE NUMBER(2,0);
v_SECOND NUMBER(2,0);

BEGIN

v_NAAS_ID:='XXX';
v_Author:='XXX';
v_Organization:='XXX';
v_ContactInfo:='XXX Strret, XXX City, XXX State XXX Zip';
v_Email_Address:='XXXv_address.com';


IF (p_start_date IS NOT NULL OR UPPER(TRIM(p_start_date))<>'NULL') THEN

    IF LENGTH(TRIM(TRANSLATE(p_start_date,' :0123456789',' '))) IS NOT NULL THEN
        RAISE_APPLICATION_ERROR(-20999, 'THE VALUE FOR PARAMTER LAST UPDATE DATE ('||p_start_date||') CONTAINS INVALID CHARACTER, PLEASE RE-ENTER WITH "YYYYMMDD HH:MI:SS" 24 HOUR FORMAT.');
      RETURN;
    ELSIF (LENGTH(TRIM(p_start_date))<>17 OR 
           SUBSTR(TRIM(p_start_date),9,1)<>' ' OR 
           SUBSTR(TRIM(p_start_date),12,1)<>':' OR 
           SUBSTR(TRIM(p_start_date),15,1)<>':'
          ) THEN
      RAISE_APPLICATION_ERROR(-20999, '* THE VALUE FOR PARAMTER LAST UPDATE DATE ('||p_start_date||') IS NOT VALID, PLEASE RE-ENTER WITH "YYYYMMDD HH:MI:SS" 24 HOUR FORMAT.');
      RETURN;
    ELSE  
      v_YEAR:=TO_NUMBER(SUBSTR(TRIM(p_start_date),1,4));
      v_MONTH:=TO_NUMBER(SUBSTR(TRIM(p_start_date),5,2));
      v_DAY:=TO_NUMBER(SUBSTR(TRIM(p_start_date),7,2));
      IF (v_MONTH BETWEEN 1 AND 12) AND
         ((v_MONTH IN (1,3,5,7,8,10,12) AND v_DAY BETWEEN 1 AND 31) OR
          (v_MONTH IN (4,6,9,11) AND v_DAY BETWEEN 1 AND 30) OR
          (v_MONTH=2 AND MOD(v_YEAR,4)=0 AND v_DAY BETWEEN 1 AND 29) OR
          (v_MONTH=2 AND MOD(v_YEAR,4)>0 AND v_DAY BETWEEN 1 AND 28)) THEN 
        NULL;
      ELSE
        RAISE_APPLICATION_ERROR(-20999, '** THE VALUE FOR PARAMTER LAST UPDATE DATE ('||p_start_date||') IS NOT VALID, PLEASE RE-ENTER WITH "YYYYMMDD HH:MISS" 24-HOUR FORMAT.');
        RETURN;
      END IF;
      v_HOUR:=TO_NUMBER(SUBSTR(TRIM(p_start_date),10,2));
      v_MINUTE:=TO_NUMBER(SUBSTR(TRIM(p_start_date),13,2));
      v_SECOND:=TO_NUMBER(SUBSTR(TRIM(p_start_date),16,2));
      IF NOT ((v_HOUR BETWEEN 0 AND 23) AND (v_MINUTE BETWEEN 0 AND 59) AND (v_SECOND BETWEEN 0 AND 59)) THEN 
        RAISE_APPLICATION_ERROR(-20999, '*** THE VALUE FOR PARAMTER LAST UPDATE TIME ('||p_start_date||') IS NOT VALID, PLEASE RE-ENTER WITH "YYYYMMDD HH:MI:SS" 24-HOUR FORMAT.');
        RETURN;
      END IF;
      v_start_date:=TO_DATE(p_start_date,'YYYYMMDD HH24:MI:SS');
    END IF;
    
ELSIF (p_start_date IS NULL OR UPPER(TRIM(p_start_date))='NULL') THEN
    v_start_date:=NULL;
END IF;



-- UnpermittedFacilityData
SELECT XMLElement("Payload",
                  XMLATTRIBUTES('UnpermittedFacilitySubmission' AS "Operation"),
                  (SELECT XMLAgg(XMLElement("UnpermittedFacilityData",
-- UnpermittedFacilityData/TransactionHeader
                                 (SELECT XMLAgg(XMLElement("TransactionHeader",
                                                XMLForest('R' AS "TransactionType",
                                                          TO_CHAR(SYSDATE,'YYYY-MM-DD')||'T'||TO_CHAR(SYSDATE,'HH24:MI:SS') AS "TransactionTimestamp")))
                                    FROM DUAL), 
-- UnpermittedFacilityData/UnpermittedFacility
                                 (SELECT XMLAgg(XMLElement("UnpermittedFacility",
                                                XMLForest(UnpermittedFacility.PermitIdentIFier AS "PermitIdentIFier", 
                                                          UnpermittedFacility.FacilitySiteName AS "FacilitySiteName", 
                                                          UnpermittedFacility.LocationAddressText AS "LocationAddressText", 
                                                          UnpermittedFacility.SupplementalLocationText AS "SupplementalLocationText", 
                                                          UnpermittedFacility.LocalityName AS "LocalityName", 
                                                          UnpermittedFacility.LocationStateCode AS "LocationStateCode", 
                                                          UnpermittedFacility.LocationZipCode AS "LocationZipCode", 
                                                          CASE WHEN LENGTH(NVL(UnpermittedFacility.LocationCountryCode,''))=0 THEN 'US' ELSE UPPER(LocationCountryCode) END AS "LocationCountryCode", 
                                                          UnpermittedFacility.OrganizationDUNSNumber AS "OrganizationDUNSNumber", 
                                                          UnpermittedFacility.StateFacilityIdentIFier AS "StateFacilityIdentIFier", 
                                                          UnpermittedFacility.StateRegionCode AS "StateRegionCode", 
                                                          UnpermittedFacility.congressional_dist_num AS "FacilityCongressionalDistrictNumber", 
                                                          UnpermittedFacility.small_business_flag AS "FacilitySmallBusinessIndicator", 
                                                          UnpermittedFacility.FacilityTypeofOwnershipCode AS "FacilityTypeofOwnershipCode", 
                                                          UnpermittedFacility.FED_ID_NO AS "FederalFacilityIdentIFicationNumber", 
                                                          UnpermittedFacility.FederalAgencyCode AS "FederalAgencyCode", 
                                                          UnpermittedFacility.FAC_ENV_JUSTICE_CD AS "FacilityEnvironmentalJusticeCode", 
                                                          UnpermittedFacility.TribalLandCode AS "TribalLandCode", 
                                                          UnpermittedFacility.ConstructionProjectName AS "ConstructionProjectName", 
                                                          UnpermittedFacility.ConstructionProjectLat AS "ConstructionProjectLatitudeMeasure", 
                                                          UnpermittedFacility.ConstructionProjectLong AS "ConstructionProjectLongitudeMeasure", 
-- UnpermittedFacilityData/UnpermittedFacility/SICCodeDetails
                                                          (SELECT XMLForest(SIC_Code AS "SICCode",
                                                                            PRIMARY_INDICATOR_FLAG AS "SICPrimaryIndicatorCode")
                                                             FROM (SELECT DISTINCT SIC_Code, PRIMARY_INDICATOR_FLAG, ICIS_FACILITY_INTEREST_ID FROM XREF_FACILITY_INTEREST_SIC) XREF_FACILITY_INTEREST_SIC
                                                            WHERE ICIS_FACILITY_INTEREST_ID=UnpermittedFacility.ICIS_FACILITY_INTEREST_ID) AS "SICCodeDetails",
-- UnpermittedFacilityData/UnpermittedFacility/NAICSCodeDetails
                                                          (SELECT XMLForest(NAICS_Code AS "NAICSCode",
                                                                            PRIMARY_INDICATOR_FLAG AS "NAICSPrimaryIndicatorCode")
                                                             FROM (SELECT DISTINCT NAICS_Code, PRIMARY_INDICATOR_FLAG, ICIS_FACILITY_INTEREST_ID FROM XREF_FACILITY_INTEREST_NAICS) XREF_FACILITY_INTEREST_NAICS
                                                            WHERE ICIS_FACILITY_INTEREST_ID=UnpermittedFacility.ICIS_FACILITY_INTEREST_ID) AS "NAICSCodeDetails", 
                                                          UnpermittedFacility.SectionTownshipRange AS "SectionTownshipRange", 
                                                          UnpermittedFacility.FacilityComments AS "FacilityComments", 
                                                          UnpermittedFacility.FacilityUserDefinedField1 AS "FacilityUserDefinedField1", 
                                                          UnpermittedFacility.FacilityUserDefinedField2 AS "FacilityUserDefinedField2", 
                                                          UnpermittedFacility.FacilityUserDefinedField3 AS "FacilityUserDefinedField3", 
                                                          UnpermittedFacility.FacilityUserDefinedField4 AS "FacilityUserDefinedField4", 
                                                          UnpermittedFacility.FacilityUserDefinedField5 AS "FacilityUserDefinedField5",
-- UnpermittedFacilityData/UnpermittedFacility/FacilityContact
                                                          (SELECT XMLAgg(XMLElement("Contact",
                                                                         XMLForest(xref_activity_contact.affiliation_type_code AS "AffiliationTypeText", 
                                                                                   icis_contact.first_name AS "FirstName", 
                                                                                   icis_contact.middle_name AS "MiddleName", 
                                                                                   icis_contact.last_name AS "LastName", 
                                                                                   icis_contact.title AS "IndividualTitleText", 
                                                                                   icis_contact.organization_formal_name AS "OrganizationFormalName", 
                                                                                   icis_contact.state_code AS "StateCode", 
                                                                                   icis_contact.region_code AS "RegionCode",
      -- UnpermittedFacilityData/UnpermittedFacility/FacilityContact/Telephone
                                                                                   (SELECT XMLForest(CASE WHEN LENGTH(REPLACE(REPLACE(icis_phone.telephone_nmbr,'-',''),' ','')) >0 THEN icis_phone.phone_type_code ELSE NULL END AS "TelephoneNumberTypeCode",
                                                                                                     CASE WHEN LENGTH(REPLACE(REPLACE(icis_phone.telephone_nmbr,'-',''),' ',''))=0 THEN NULL ELSE REPLACE(REPLACE(icis_phone.telephone_nmbr,'-',''),' ','') END AS "TelephoneNumber",
                                                                                                     CASE WHEN LENGTH(REPLACE(REPLACE(icis_phone.telephone_extension_nmbr,'-',''),' ',''))=0 THEN NULL ELSE REPLACE(REPLACE(icis_phone.telephone_extension_nmbr,'-',''),' ','') END AS "TelephoneExtensionNumber") 
                                                                                      FROM icis_phone 
                                                                                           INNER JOIN icis_contact_phone on icis_phone.phone_id=icis_contact_phone.phone_id
                                                                                     WHERE icis_contact_phone.contact_id=xref_activity_contact.contact_id) AS "Telephone",
                                                                                   icis_contact_electronic_addr.electronic_address_text AS "ElectronicAddressText",
                                                                                   TO_CHAR(xref_activity_contact.begin_date,'YYYY-MM-DD') AS "StartDateOfContactAssociation",
                                                                                   TO_CHAR(xref_activity_contact.end_date,'YYYY-MM-DD') AS "EndDateOfContactAssociation")))
                                                             FROM XREF_ACTIVITY_FACILITY_INT  
                                                                  INNER JOIN xref_activity_contact on XREF_ACTIVITY_FACILITY_INT.activity_id=xref_activity_contact.activity_id
                                                                  LEFT JOIN icis_contact on xref_activity_contact.contact_id=icis_contact.contact_id
                                                                  LEFT JOIN icis_contact_electronic_addr ON icis_contact_electronic_addr.contact_id=icis_contact.contact_id 
                                                            WHERE xref_activity_contact.ACTIVITY_ID=UnpermittedFacility.ACTIVITY_ID 
                                                              AND XREF_ACTIVITY_FACILITY_INT.ICIS_FACILITY_INTEREST_ID=UnpermittedFacility.ICIS_FACILITY_INTEREST_ID) AS "FacilityContact",
-- UnpermittedFacilityData/UnpermittedFacility/FacilityAddress
                                                          (SELECT XMLAgg(XMLElement("Address",
                                                                         XMLForest(xref_facility_interest_address.affiliation_type_code AS "AffiliationTypeText", 
                                                                                   icis_address.organization_formal_name AS "OrganizationFormalName", 
                                                                                   icis_address.organization_duns_nmbr AS "OrganizationDUNSNumber", 
                                                                                   icis_address.street_address AS "MailingAddressText", 
                                                                                   icis_address.supplemental_address_text AS "SupplementalAddressText", 
                                                                                   icis_address.city AS "MailingAddressCityName", 
                                                                                   icis_address.state_code AS "MailingAddressStateCode", 
                                                                                   icis_address.zip AS "MailingAddressZipCode", 
                                                                                   icis_address.county AS "CountyName", 
                                                                                   icis_address.country_code AS "MailingAddressCountryCode", 
                                                                                   icis_address.division_name AS "DivisionName", 
                                                                                   icis_address.province AS "LocationProvince",
-- UnpermittedFacilityData/UnpermittedFacility/FacilityAddress/Telephone
                                                                                   (SELECT XMLAgg(XMLElement("Telephone",
                                                                                                  XMLForest(CASE WHEN LENGTH(REPLACE(REPLACE(icis_phone.telephone_nmbr,'-',''),' ','')) >0 THEN icis_phone.phone_type_code ELSE NULL END AS "TelephoneNumberTypeCode",
                                                                                                            CASE WHEN LENGTH(REPLACE(REPLACE(icis_phone.telephone_nmbr,'-',''),' ',''))=0 THEN NULL ELSE REPLACE(REPLACE(icis_phone.telephone_nmbr,'-',''),' ','') END AS "TelephoneNumber",
                                                                                                            CASE WHEN LENGTH(REPLACE(REPLACE(icis_phone.telephone_extension_nmbr,'-',''),' ',''))=0 THEN NULL ELSE REPLACE(REPLACE(icis_phone.telephone_extension_nmbr,'-',''),' ','') END AS "TelephoneExtensionNumber")))
                                                                                      FROM icis_phone 
                                                                                           INNER JOIN icis_address_phone on icis_phone.phone_id=icis_address_phone.phone_id
                                                                                     WHERE icis_address_phone.address_id=icis_ADDRESS.address_id) AS "Telephone", 
                                                                                   icis_address_electronic_addr.electronic_address_text AS "ElectronicAddressText",
                                                                                   TO_CHAR(xref_facility_interest_address.begin_date,'YYYY-MM-DD') AS "StartDateOfAddressAssociation",
                                                                                   TO_CHAR(xref_facility_interest_address.end_date,'YYYY-MM-DD') AS "EndDateOfAddressAssociation")))
                                                                              FROM xref_facility_interest_address 
                                                                                   INNER JOIN icis_ADDRESS on xref_facility_interest_address.ADDRESS_id=icis_ADDRESS.ADDRESS_id
                                                                                   LEFT JOIN icis_address_electronic_addr ON icis_address_electronic_addr.address_id=xref_facility_interest_address.address_id
                                                                             WHERE xref_facility_interest_address.ICIS_FACILITY_INTEREST_ID=UnpermittedFacility.ICIS_FACILITY_INTEREST_ID ) AS "FacilityAddress", 
-- UnpermittedFacilityData/UnpermittedFacility/GeographicCoordinates
                                                          (SELECT XMLForest(CASE WHEN LENGTH(TRIM(UnpermittedFacility.GEOCODE_LONGITUDE))=0 THEN NULL ELSE UnpermittedFacility.GEOCODE_LONGITUDE END AS "LatitudeMeasure",
                                                                            CASE WHEN LENGTH(TRIM(UnpermittedFacility.GEOCODE_LATITUDE))=0 THEN NULL ELSE UnpermittedFacility.GEOCODE_LATITUDE END AS "LongitudeMeasure",
                                                                            CASE WHEN LENGTH(TRIM(UnpermittedFacility.HORIZONTAL_ACCURACY_MEASURE))=0 THEN NULL ELSE UnpermittedFacility.HORIZONTAL_ACCURACY_MEASURE  END AS "HorizontalAccuracyMeasure",
                                                                            CASE WHEN LENGTH(TRIM(UnpermittedFacility.GEOMETRIC_TYPE_CODE))=0 THEN NULL ELSE UnpermittedFacility.GEOMETRIC_TYPE_CODE  END AS "GeometricTypeCode",
                                                                            CASE WHEN LENGTH(TRIM(UnpermittedFacility.HORIZONTAL_COLLECT_METHOD_CODE))=0 THEN NULL ELSE UnpermittedFacility.HORIZONTAL_COLLECT_METHOD_CODE  END AS "HorizontalCollectionMethodCode",
                                                                            CASE WHEN LENGTH(TRIM(UnpermittedFacility.HORIZONTAL_REF_DATUM_CODE))=0 THEN NULL ELSE UnpermittedFacility.HORIZONTAL_REF_DATUM_CODE  END AS "HorizontalReferenceDatumCode",
                                                                            CASE WHEN LENGTH(TRIM(UnpermittedFacility.REFERENCE_POINT_CODE))=0 THEN NULL ELSE UnpermittedFacility.REFERENCE_POINT_CODE  END AS "ReferencePointCode",
                                                                            CASE WHEN LENGTH(TRIM(UnpermittedFacility.SOURCE_MAP_SCALE_NMBR))=0 THEN NULL ELSE UnpermittedFacility.SOURCE_MAP_SCALE_NMBR  END AS "SourceMapScaleNumber")
                                                             FROM DUAL
                                                            WHERE LENGTH(TRIM(UnpermittedFacility.GEOCODE_LONGITUDE))<>0
                                                               OR LENGTH(TRIM(UnpermittedFacility.GEOCODE_LATITUDE))<>0 
                                                               OR LENGTH(TRIM(UnpermittedFacility.HORIZONTAL_ACCURACY_MEASURE))<>0
                                                               OR LENGTH(TRIM(UnpermittedFacility.GEOMETRIC_TYPE_CODE))<>0
                                                               OR LENGTH(TRIM(UnpermittedFacility.HORIZONTAL_COLLECT_METHOD_CODE))<>0
                                                               OR LENGTH(TRIM(UnpermittedFacility.HORIZONTAL_REF_DATUM_CODE))<>0
                                                               OR LENGTH(TRIM(UnpermittedFacility.REFERENCE_POINT_CODE))<>0
                                                               OR LENGTH(TRIM(UnpermittedFacility.SOURCE_MAP_SCALE_NMBR))<>0 ) AS "GeographicCoordinates"
                                                        )))
                                                        from dual
                                          )))              
                     FROM (SELECT B.Activity_id
                                , A.ICIS_FACILITY_INTEREST_ID
                                , NVL((SELECT MAX(EXTERNAL_PERMIT_NMBR) FROM ICIS_PERMIT WHERE Activity_ID=B.Activity_ID AND PERMIT_TYPE_CODE= 'UFT')
                                      , ('MIU'||SUBSTR(A.zip,1,2)||SUBSTR(A.icis_facility_interest_id,LENGTH(A.icis_facility_interest_id)-3,4))) AS PermitIdentIFier
                                , A.facility_name AS FacilitySiteName -- Required "Yes when adding or replacing basic, GPCF permits AND unpermitted facilities "  Repeatable No -- Common Parent : Facility, UnpermittedFacility
                                , A.location_address AS LocationAddressText -- Required "Yes when adding or replacing basic, GPCF permits AND unpermitted facilities "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.supplemental_address_text,''))=0 THEN NULL ELSE A.supplemental_address_text END AS SupplementalLocationText  -- Required "No"  Repeatable No -- Address Parent : Facility, UnpermittedFacility
                                , A.city AS LocalityName -- Required "Yes FOR adding basic, GPCF permits AND unpermitted facilities FOR adding basic, GPCF permits AND unpermitted facilities No FOR all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
                                , A.state_code AS LocationStateCode -- Required "Yes FOR adding basic, GPCF permits AND unpermitted facilities FOR adding basic, GPCF permits AND unpermitted facilities No FOR all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
                                , TRIM(A.zip) AS LocationZipCode -- Required "Yes FOR adding basic, GPCF permits AND unpermitted facilities FOR adding basic, GPCF permits AND unpermitted facilities No FOR all others "  Repeatable No -- Address Parent : Facility, UnpermittedFacility
                                , A.country_code AS LocationCountryCode  -- Required "Yes"  Repeatable No -- Address Parent : Facility, UnpermittedFacility
                                , A.organization_duns_nmbr AS OrganizationDUNSNumber  -- , icis_limit_trade_partner.organization_duns_nmbr , icis_address.organization_duns_nmbr Required "No"  Repeatable No -- Common Parent : Address EffluentTradePartnerAddress UnpermittedFacility
                                , A.state_facility_id AS StateFacilityIdentIFier  -- Required "No"  Repeatable No -- Common Parent : Facility, UnpermittedFacility
                                , A.state_region AS StateRegionCode  -- Required "No"  Repeatable No -- Common Parent : Facility, UnpermittedFacility
                                , A.congressional_dist_num --AS FacilityCongressionalDistrictNumber  -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , NULL AS FacilityClassIFication -- xref_fac_int_classIFication.classIFication_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
                                , A.small_business_flag --AS FacilitySmallBusinessIndicator  -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , NULL AS PolicyCode -- xref_facility_interest_policy.policy_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
                                , NULL AS OriginatingProgramsCode -- xref_facility_interest_program.program_code Required "No"  Repeatable Yes -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.facility_type_code,''))=0 THEN NULL ELSE A.facility_type_code END AS FacilityTypeofOwnershipCode -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.federal_facility_id,''))=0 THEN NULL ELSE A.federal_facility_id END AS FED_ID_NO -- FederalFacilityIdentIFicationNumber  -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.federal_agency_code,''))=0 THEN NULL ELSE A.federal_agency_code END AS FederalAgencyCode -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.environmental_justice_code,''))=0 THEN NULL ELSE A.environmental_justice_code END AS FAC_ENV_JUSTICE_CD --FacilityEnvironmentalJusticeCode  -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.tribal_land_r_code,''))=0 THEN NULL ELSE A.tribal_land_r_code END AS TribalLandCode -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.construction_project_name,''))=0 THEN NULL ELSE A.construction_project_name END AS ConstructionProjectName -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.construction_project_lat,''))=0 THEN NULL ELSE A.construction_project_lat END AS ConstructionProjectLat -- ConstructionProjectLatitudeMeasure  -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.construction_project_long,''))=0 THEN NULL ELSE A.construction_project_long END AS ConstructionProjectLong -- ConstructionProjectLongitudeMeasure  -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.section_township_range,''))=0 THEN NULL ELSE A.section_township_range END AS SectionTownshipRange -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.comment_text,''))=0 THEN NULL ELSE A.comment_text END AS FacilityComments -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.udf1,''))=0 THEN NULL ELSE A.udf1 END AS FacilityUserDefinedField1 -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.udf2,''))=0 THEN NULL ELSE A.udf2 END AS FacilityUserDefinedField2 -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.udf3,''))=0 THEN NULL ELSE A.udf3 END AS FacilityUserDefinedField3 -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.udf4,''))=0 THEN NULL ELSE A.udf4 END AS FacilityUserDefinedField4 -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , CASE WHEN LENGTH(NVL(A.udf5,''))=0 THEN NULL ELSE A.udf5 END AS FacilityUserDefinedField5 -- Required "No"  Repeatable No -- Facility Parent : Facility, UnpermittedFacility
                                , NULL AS PermitCommentsText -- icis_permit.comment_text Required "No"  Repeatable No -- Permit Parent : BasicPermit GeneralPermit MasterGeneralPermit UnpermittedFacility
                                , A.GEOCODE_LONGITUDE
                                , A.GEOCODE_LATITUDE
                                , A.HORIZONTAL_ACCURACY_MEASURE
                                , A.GEOMETRIC_TYPE_CODE
                                , A.HORIZONTAL_COLLECT_METHOD_CODE
                                , A.HORIZONTAL_REF_DATUM_CODE
                                , A.REFERENCE_POINT_CODE
                                , A.SOURCE_MAP_SCALE_NMBR
                             FROM icis_facility_interest a
                                  INNER JOIN XREF_ACTIVITY_FACILITY_INT b on A.ICIS_FACILITY_INTEREST_ID=b.ICIS_FACILITY_INTEREST_ID 
                                  INNER JOIN icis_activity c on b.activity_id=c.activity_id
                            WHERE c.stg_data_create_date>=(SELECT MIN(stg_data_create_date)
                                                             FROM icis_activity
                                                            WHERE stg_data_create_date>=NVL(v_start_date, stg_data_create_date)) ) UnpermittedFacility 
                          
  --FROM DUAL
) -- UnpermittedFacilityData
) -- Payload
  INTO v_xml_file
  FROM DUAL;

p_output:='<Document xmlns="http://www.exchangenetwork.net/schema/icis/2" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">';
p_output:=p_output||'<Header>';
p_output:=p_output||'<Id>'||NVL(v_NAAS_ID,'')||'</Id>';
p_output:=p_output||'<Author>'||NVL(v_Author,'')||'</Author>';
p_output:=p_output||'<Organization>'||NVL(v_Organization,'')||'</Organization>';
p_output:=p_output||'<Title>Unpermitted Facility Submission</Title>';
p_output:=p_output||'<CreationTime>'||TO_CHAR(SYSDATE,'YYYY-MM-DD')||'T'||TO_CHAR(SYSDATE,'HH24:MI:SS')||'</CreationTime>';
p_output:=p_output||'<DataService>ICIS-NPDES</DataService>';
p_output:=p_output||'<ContactInfo>'||NVL(v_ContactInfo,'')||'</ContactInfo>';
p_output:=p_output||'<Property>';
p_output:=p_output||'<name>e-mail</name>';
p_output:=p_output||'<value>'||NVL(v_Email_Address,'')||'</value>';
p_output:=p_output||'</Property>';
p_output:=p_output||'<Property>';
p_output:=p_output||'<name>Source</name>';
p_output:=p_output||'<value>NetDMR</value>';
p_output:=p_output||'</Property>';
p_output:=p_output||'</Header>';
p_output:=p_output||'<Payload Operation="UnpermittedFacilitySubmission">';

p_output:=p_output||v_xml_file.GETCLOBVAL()||'</Payload></Document>';

DELETE FROM test;
INSERT INTO test(TEST_CLOB) VALUES (p_output);

END;
/