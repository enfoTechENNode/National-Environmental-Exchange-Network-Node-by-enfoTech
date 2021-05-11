
USE [TRI_PROD]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  add MISCELLANEOUS_INFORMATION_TEXT column in sys_report table   ******/

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SYS_REPORT' AND COLUMN_NAME = 'MISCELLANEOUS_INFORMATION_TEXT')
BEGIN
	alter table SYS_REPORT add MISCELLANEOUS_INFORMATION_TEXT NVARCHAR(1000) NULL;
END;

/****** Object:  copy 4.0 records to 5.0      ******/
delete from DE_FLOW_TRANSFORMER where VERSION = '5_0';

SELECT	identity(int,1,1) as TRANSFORMER_ID, 5 as FLOW_ID, '5_0' as VERSION, STATUS,
        REPLACE(TRANSFORMER_NAME,'4_0','5_0') as TRANSFORMER_NAME, XSLT, 
        DESCRIPTION, getdate()as CREATED_DTTM,'longd' as CREATED_BY, 
        getdate() as UPDATED_DTTM, 'longd' as UPDATED_BY
INTO #DE_FLOW_TRANSFORMER      
FROM DE_FLOW_TRANSFORMER  
WHERE VERSION = '4_0';
 
INSERT INTO DE_FLOW_TRANSFORMER  
SELECT	(select max(TRANSFORMER_ID) from DE_FLOW_TRANSFORMER) + TRANSFORMER_ID, 5, '5_0', STATUS,
        TRANSFORMER_NAME, XSLT, DESCRIPTION, getdate(),'longd', getdate(), 'longd'
        FROM #DE_FLOW_TRANSFORMER;  

drop table #DE_FLOW_TRANSFORMER;


/****** Object:  create a new table SYS_POTW_WASTE_QUANTITY    ******/
drop table SYS_POTW_WASTE_QUANTITY;

CREATE TABLE [dbo].[SYS_POTW_WASTE_QUANTITY](
	[POTW_WASTE_QUANTITY_KEY] [int] IDENTITY(1,1) NOT NULL,
	[REPORT_KEY] [int] NOT NULL,
	[POTW_SEQUENCE_NUMBER] [nvarchar](10) NULL,
	[WASTE_QUANTITY_MEASURE] [nvarchar](10) NULL,
    [WASTE_QUANTITY_CATASTROPHIC_MEASURE] [nvarchar](10) NULL,
	[WASTE_QUANTITY_RANGE_CODE] [nvarchar](10) NULL,
    [WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE] [nvarchar](10) NULL,
	[WASTE_QUANTITY_NA_INDICATOR] [nvarchar](10) NULL,
	[QUANTITY_BASIS_ESTIMATION_CODE] [nvarchar](10) NULL,
	[QUANTITY_BASIS_ESTIMATION_NA_INDICATOR] [nvarchar](10) NULL,
	[TOXIC_EQUIVALENCY_1_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_2_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_3_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_4_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_5_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_6_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_7_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_8_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_9_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_10_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_11_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_12_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_13_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_14_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_15_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_16_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_17_VALUE] [nvarchar](11) NULL,
	[TOXIC_EQUIVALENCY_NA_INDICATOR] [nvarchar](10) NULL,
    [QUANTITY_DISPOSED_LANDFILL_PERCENT_VALUE] [nvarchar](10) NULL,
    [QUANTITY_DISPOSED_OTHER_PERCENT_VALUE] [nvarchar](10) NULL,
    [QUANTITY_TREATED_PERCENT_VALUE] [nvarchar](10) NULL,

 CONSTRAINT [PK_SYS_POTW_WASTE_QUANTITY] PRIMARY KEY CLUSTERED 
(
	[POTW_WASTE_QUANTITY_KEY] ASC
) ON [PRIMARY]
) ON [PRIMARY];


/****** Object:  insert a style sheet record in  DE_FLOW_TRANSFORMER for 5.0   ******/
delete from DE_FLOW_TRANSFORMER where TRANSFORMER_NAME='TRI_REPORT_POTW_WASTE_QUANTITY_5_0';
INSERT INTO DE_FLOW_TRANSFORMER  
SELECT	(select max(TRANSFORMER_ID) from DE_FLOW_TRANSFORMER) + 1, 5, '5_0', 'A', 
'TRI_REPORT_POTW_WASTE_QUANTITY_5_0', 
'<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
	<xsl:template match="/">
		<xsl:apply-templates />
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:POTWWasteQuantity">
					<SYS_POTW_WASTE_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../REPORT_KEY"/>
						</REPORT_KEY>
						<POTW_SEQUENCE_NUMBER>
							<xsl:value-of select="TRI:POTWSequenceNumber"/>
						</POTW_SEQUENCE_NUMBER>
						<WASTE_QUANTITY_MEASURE>
							<xsl:value-of select="TRI:WasteQuantityMeasure"/>
						</WASTE_QUANTITY_MEASURE>
						<WASTE_QUANTITY_CATASTROPHIC_MEASURE>
							<xsl:value-of select="TRI:WasteQuantityCatastrophicMeasure"/>
						</WASTE_QUANTITY_CATASTROPHIC_MEASURE>
						<WASTE_QUANTITY_RANGE_CODE>
							<xsl:value-of select="TRI:WasteQuantityRangeCode"/>
						</WASTE_QUANTITY_RANGE_CODE>
						<WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
							<xsl:value-of select="TRI:WasteQuantityRangeNumericBasisValue"/>
						</WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
						<WASTE_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:WasteQuantityNAIndicator"/>
						</WASTE_QUANTITY_NA_INDICATOR>
						<QUANTITY_BASIS_ESTIMATION_CODE>
							<xsl:value-of select="TRI:QuantityBasisEstimationCode"/>
						</QUANTITY_BASIS_ESTIMATION_CODE>
						<QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
							<xsl:value-of select="TRI:QuantityBasisEstimationNAIndicator"/>
						</QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
						<QUANTITY_DISPOSED_LANDFILL_PERCENT_VALUE>
							<xsl:value-of select="TRI:QuantityDisposedLandfillPercentValue"/>
						</QUANTITY_DISPOSED_LANDFILL_PERCENT_VALUE>
						<QUANTITY_DISPOSED_OTHER_PERCENT_VALUE>
							<xsl:value-of select="TRI:QuantityDisposedOtherPercentValue"/>
						</QUANTITY_DISPOSED_OTHER_PERCENT_VALUE>
						<QUANTITY_TREATED_PERCENT_VALUE>
							<xsl:value-of select="TRI:QuantityTreatedPercentValue"/>
						</QUANTITY_TREATED_PERCENT_VALUE>
					</SYS_POTW_WASTE_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>', 
'TRI_REPORT_POTW_WASTE_QUANTITY.xslt', getdate(),'longd',getdate(),'longd';


/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission">
				<xsl:for-each select="TRI:Report">
					<SYS_REPORT>
						<REPORT_KEY>
							<xsl:value-of select="REPORT_KEY"/>
						</REPORT_KEY>
						<FACILITY_KEY>
							<xsl:value-of select="../TRI:Facility/FACILITY_KEY"/>
						</FACILITY_KEY>
						<REPORT_ID>
							<xsl:value-of select="sc:ReportIdentifier"/>
						</REPORT_ID>
						<REPORT_POSTMARK_DATE>
							<xsl:value-of select="TRI:ReportMetaData/TRI:ReportPostmarkDate"/>
						</REPORT_POSTMARK_DATE>
						<REPORT_RECEIVED_DATE>
							<xsl:value-of select="TRI:ReportMetaData/TRI:ReportReceivedDate"/>
						</REPORT_RECEIVED_DATE>
						<REPORT_ORIGINAL_POSTMARK_DATE>
							<xsl:value-of select="TRI:ReportMetaData/TRI:ReportOriginalPostmarkDate"/>
						</REPORT_ORIGINAL_POSTMARK_DATE>
						<REPORT_ORIGINAL_RECEIVED_DATE>
							<xsl:value-of select="TRI:ReportMetaData/TRI:ReportOriginalReceivedDate"/>
						</REPORT_ORIGINAL_RECEIVED_DATE>
						<REPORT_SUBMISSION_METHOD_CODE>
							<xsl:value-of select="TRI:ReportMetaData/TRI:ReportSubmissionMethodCode"/>
						</REPORT_SUBMISSION_METHOD_CODE>
						<EPA_PASSED_VALIDATION_INDICATOR>
							<xsl:value-of select="TRI:ReportMetaData/TRI:EPAPassedValidationIndicator"/>
						</EPA_PASSED_VALIDATION_INDICATOR>
						<EPA_PROCESSING_STATUS_CODE>
							<xsl:value-of select="TRI:ReportMetaData/TRI:EPAProcessingStatusCode"/>
						</EPA_PROCESSING_STATUS_CODE>
						<UNALTERED_REPORT_INDICATOR>
							<xsl:value-of select="TRI:ReportMetaData/TRI:UnalteredReportIndicator"/>
						</UNALTERED_REPORT_INDICATOR>
						<REPORT_TYPE_CODE>
							<xsl:value-of select="TRI:ReportType/TRI:ReportTypeCode"/>
						</REPORT_TYPE_CODE>
						<REPORT_TYPE_CODELIST_IDENTIFIER>
							<xsl:value-of select="TRI:ReportType/sc:ReportTypeCodeListIdentifier"/>
						</REPORT_TYPE_CODELIST_IDENTIFIER>
						<REPORT_TYPE_NAME>
							<xsl:value-of select="TRI:ReportType/sc:ReportTypeName"/>
						</REPORT_TYPE_NAME>
						<SUBMISSION_REPORTING_YEAR>
							<xsl:value-of select="TRI:SubmissionReportingYear"/>
						</SUBMISSION_REPORTING_YEAR>
						<REPORT_DUE_DATE>
							<xsl:value-of select="sc:ReportDueDate"/>
						</REPORT_DUE_DATE>
						<REVISION_INDICATOR>
							<xsl:value-of select="sc:RevisionIndicator"/>
						</REVISION_INDICATOR>
						<CHEMICAL_TRADE_SECRET_INDICATOR>
							<xsl:value-of select="TRI:ChemicalTradeSecretIndicator"/>
						</CHEMICAL_TRADE_SECRET_INDICATOR>
						<SUBMISSION_SANITIZED_INDICATOR>
							<xsl:value-of select="TRI:SubmissionSanitizedIndicator"/>
						</SUBMISSION_SANITIZED_INDICATOR>
						<CERTIFIER_NAME>
							<xsl:value-of select="TRI:CertifierName"/>
						</CERTIFIER_NAME>
						<CERTIFIER_TITLE_TEXT>
							<xsl:value-of select="TRI:CertifierTitleText"/>
						</CERTIFIER_TITLE_TEXT>
						<CERTIFICATION_SIGNED_DATE>
							<xsl:value-of select="TRI:CertificationSignedDate"/>
						</CERTIFICATION_SIGNED_DATE>
						<SUBMISSION_ENTIRE_FACILITY_INDICATOR>
							<xsl:value-of select="TRI:SubmissionEntireFacilityIndicator"/>
						</SUBMISSION_ENTIRE_FACILITY_INDICATOR>
						<SUBMISSION_PARTIAL_FACILITY_INDICATOR>
							<xsl:value-of select="TRI:SubmissionPartialFacilityIndicator"/>
						</SUBMISSION_PARTIAL_FACILITY_INDICATOR>
						<SUBMISSION_FEDERAL_FACILITY_INDICATOR>
							<xsl:value-of select="TRI:SubmissionFederalFacilityIndicator"/>
						</SUBMISSION_FEDERAL_FACILITY_INDICATOR>
						<SUBMISSION_GOCO_FACILITY_INDICATOR>
							<xsl:value-of select="TRI:SubmissionGOCOFacilityIndicator"/>
						</SUBMISSION_GOCO_FACILITY_INDICATOR>
						<INDIVIDUAL_IDENTIFIER>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:IndividualIdentifier"/>
						</INDIVIDUAL_IDENTIFIER>
						<INDIVIDUAL_TITLE_TEXT>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:IndividualTitleText"/>
						</INDIVIDUAL_TITLE_TEXT>
						<NAME_PREFIX_TEXT>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:NamePrefixText"/>
						</NAME_PREFIX_TEXT>
						<INDIVIDUAL_FULL_NAME>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:IndividualFullName"/>
						</INDIVIDUAL_FULL_NAME>
						<FIRST_NAME>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:FirstName"/>
						</FIRST_NAME>
						<MIDDLE_NAME>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:MiddleName"/>
						</MIDDLE_NAME>
						<LAST_NAME>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:LastName"/>
						</LAST_NAME>
						<NAME_SUFFIX_TEXT>
							<xsl:value-of select="TRI:TechnicalContactNameText/sc:NameSuffixText"/>
						</NAME_SUFFIX_TEXT>
						<TECHNICAL_CONTACT_PHONE_TEXT>
							<xsl:value-of select="TRI:TechnicalContactPhoneText"/>
						</TECHNICAL_CONTACT_PHONE_TEXT>
						<TECHNICAL_CONTACT_EMAIL_ADDRESS_TEXT>
							<xsl:value-of select="TRI:TechnicalContactEmailAddressText"/>
						</TECHNICAL_CONTACT_EMAIL_ADDRESS_TEXT>
						<PUBLIC_CONTACT_IDENTIFIER>
							<xsl:value-of select="TRI:PublicContactNameText/sc:IndividualIdentifier"/>
						</PUBLIC_CONTACT_IDENTIFIER>
						<PUBLIC_CONTACT_TITLE_TEXT>
							<xsl:value-of select="TRI:PublicContactNameText/sc:IndividualTitleText"/>
						</PUBLIC_CONTACT_TITLE_TEXT>
						<PUBLIC_CONTACT_NAME_PREFIX_TEXT>
							<xsl:value-of select="TRI:PublicContactNameText/sc:NamePrefixText"/>
						</PUBLIC_CONTACT_NAME_PREFIX_TEXT>
						<PUBLIC_CONTACT_FULL_NAME>
							<xsl:value-of select="TRI:PublicContactNameText/sc:IndividualFullName"/>
						</PUBLIC_CONTACT_FULL_NAME>
						<PUBLIC_CONTACT_FIRST_NAME>
							<xsl:value-of select="TRI:PublicContactNameText/sc:FirstName"/>
						</PUBLIC_CONTACT_FIRST_NAME>
						<PUBLIC_CONTACT_MIDDLE_NAME>
							<xsl:value-of select="TRI:PublicContactNameText/sc:MiddleName"/>
						</PUBLIC_CONTACT_MIDDLE_NAME>
						<PUBLIC_CONTACT_LAST_NAME>
							<xsl:value-of select="TRI:PublicContactNameText/sc:LastName"/>
						</PUBLIC_CONTACT_LAST_NAME>
						<PUBLIC_CONTACT_NAME_SUFFIX_TEXT>
							<xsl:value-of select="TRI:PublicContactNameText/sc:NameSuffixText"/>
						</PUBLIC_CONTACT_NAME_SUFFIX_TEXT>
						<PUBLIC_CONTACT_PHONE_TEXT>
							<xsl:value-of select="TRI:PublicContactPhoneText"/>
						</PUBLIC_CONTACT_PHONE_TEXT>
						<PUBLIC_CONTACT_EMAIL_ADDRESS_TEXT>
							<xsl:value-of select="TRI:PublicContactEmailAddressText"/>
						</PUBLIC_CONTACT_EMAIL_ADDRESS_TEXT>
						<CHEMICAL_REPORT_REVISION_CODE_1>
							<xsl:value-of select="TRI:ChemicalReportRevisionCode[1]"/>
						</CHEMICAL_REPORT_REVISION_CODE_1>
						<CHEMICAL_REPORT_REVISION_CODE_2>
							<xsl:value-of select="TRI:ChemicalReportRevisionCode[2]"/>
						</CHEMICAL_REPORT_REVISION_CODE_2>
						<CHEMICAL_REPORT_WITHDRAWAL_CODE_1>
							<xsl:value-of select="TRI:ChemicalReportWithdrawalCode[1]"/>
						</CHEMICAL_REPORT_WITHDRAWAL_CODE_1>
						<CHEMICAL_REPORT_WITHDRAWAL_CODE_2>
							<xsl:value-of select="TRI:ChemicalReportWithdrawalCode[2]"/>
						</CHEMICAL_REPORT_WITHDRAWAL_CODE_2>
						<CHEMICAL_ANCILLARY_USAGE_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalAncillaryUsageIndicator"/>
						</CHEMICAL_ANCILLARY_USAGE_INDICATOR>
						<CHEMICAL_ARTICLE_COMPONENT_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalArticleComponentIndicator"/>
						</CHEMICAL_ARTICLE_COMPONENT_INDICATOR>
						<CHEMICAL_BYPRODUCT_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalByproductIndicator"/>
						</CHEMICAL_BYPRODUCT_INDICATOR>
						<CHEMICAL_FORMULATION_COMPONENT_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalFormulationComponentIndicator"/>
						</CHEMICAL_FORMULATION_COMPONENT_INDICATOR>
						<CHEMICAL_IMPORTED_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalImportedIndicator"/>
						</CHEMICAL_IMPORTED_INDICATOR>
						<CHEMICAL_MANUFACTURE_AID_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalManufactureAidIndicator"/>
						</CHEMICAL_MANUFACTURE_AID_INDICATOR>
						<CHEMICAL_MANUFACTURE_IMPURITY_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalManufactureImpurityIndicator"/>
						</CHEMICAL_MANUFACTURE_IMPURITY_INDICATOR>
						<CHEMICAL_PROCESS_IMPURITY_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalProcessImpurityIndicator"/>
						</CHEMICAL_PROCESS_IMPURITY_INDICATOR>
						<CHEMICAL_PROCESSING_AID_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalProcessingAidIndicator"/>
						</CHEMICAL_PROCESSING_AID_INDICATOR>
						<CHEMICAL_PRODUCED_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalProducedIndicator"/>
						</CHEMICAL_PRODUCED_INDICATOR>
						<CHEMICAL_REACTANT_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalReactantIndicator"/>
						</CHEMICAL_REACTANT_INDICATOR>
						<CHEMICAL_REPACKAGING_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalRepackagingIndicator"/>
						</CHEMICAL_REPACKAGING_INDICATOR>
						<CHEMICAL_SALES_DISTRIBUTION_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalSalesDistributionIndicator"/>
						</CHEMICAL_SALES_DISTRIBUTION_INDICATOR>
						<CHEMICAL_USED_PROCESSED_INDICATOR>
							<xsl:value-of select="TRI:ChemicalActivitiesAndUses/TRI:ChemicalUsedProcessedIndicator"/>
						</CHEMICAL_USED_PROCESSED_INDICATOR>
						<MAXIMUM_CHEMICAL_AMOUNT_CODE>
							<xsl:value-of select="TRI:MaximumChemicalAmountCode"/>
						</MAXIMUM_CHEMICAL_AMOUNT_CODE>
						<WASTE_QUANTITY_MEASURE>
						</WASTE_QUANTITY_MEASURE>
						<WASTE_QUANTITY_CATASTROPHIC_MEASURE>
						</WASTE_QUANTITY_CATASTROPHIC_MEASURE>
						<WASTE_QUANTITY_RANGE_CODE>
						</WASTE_QUANTITY_RANGE_CODE>
						<WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
						</WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
						<WASTE_QUANTITY_NA_INDICATOR>
						</WASTE_QUANTITY_NA_INDICATOR>
						<QUANTITY_BASIS_ESTIMATION_CODE>
						</QUANTITY_BASIS_ESTIMATION_CODE>
						<QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
						</QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_1_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_1_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_2_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_2_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_3_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_3_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_4_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_4_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_5_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_5_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_6_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_6_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_7_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_7_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_8_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_8_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_9_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_9_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_10_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_10_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_11_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_11_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_12_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_12_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_13_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_13_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_14_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_14_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_15_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_15_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_16_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_16_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_17_VALUE>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_17_VALUE>
						<POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_NA_INDICATOR>
						</POTW_WASTE_QUANTITY_TOXIC_EQUIVALENCY_NA_INDICATOR>
						<QUANTITY_DISPOSED_LANDFILL_PERCENT_VALUE>
						</QUANTITY_DISPOSED_LANDFILL_PERCENT_VALUE>
						<QUANTITY_DISPOSED_OTHER_PERCENT_VALUE>
						</QUANTITY_DISPOSED_OTHER_PERCENT_VALUE>
						<QUANTITY_TREATED_PERCENT_VALUE>
						</QUANTITY_TREATED_PERCENT_VALUE>
						<WASTE_TREATMENT_NA_INDICATOR>
							<xsl:value-of select="TRI:WasteTreatmentNAIndicator"/>
						</WASTE_TREATMENT_NA_INDICATOR>
						<ENERGY_RECOVERY_NA_INDICATOR>
							<xsl:value-of select="TRI:OnsiteRecoveryProcess/TRI:EnergyRecoveryNAIndicator"/>
						</ENERGY_RECOVERY_NA_INDICATOR>
						<ONSITE_RECYCLING_NA_INDICATOR>
							<xsl:value-of select="TRI:OnsiteRecyclingProcess/TRI:OnsiteRecyclingNAIndicator"/>
						</ONSITE_RECYCLING_NA_INDICATOR>
						<ONE_TIME_RELEASE_QUANTITY>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:OneTimeReleaseQuantity"/>
						</ONE_TIME_RELEASE_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_1_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_2_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_3_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_4_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_5_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_6_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_7_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_8_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_9_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_10_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_11_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_12_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_13_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_14_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_15_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_16_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_17_VALUE>
						<SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</SOURCE_REDUCTION_QUANTITY_TOXIC_EQUIVALENCY_NA_INDICATOR>
						<ONE_TIME_RELEASE_NA_INDICATOR>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:OneTimeReleaseNAIndicator"/>
						</ONE_TIME_RELEASE_NA_INDICATOR>
						<PRODUCTION_RATIO_MEASURE>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ProductionRatioMeasure"/>
						</PRODUCTION_RATIO_MEASURE>
						<PRODUCTION_RATIO_NA_INDICATOR>
							<xsl:value-of select="TRI:SourceReductionQuantity/TRI:ProductionRatioNAIndicator"/>
						</PRODUCTION_RATIO_NA_INDICATOR>
						<SOURCE_REDUCTION_NA_INDICATOR>
							<xsl:value-of select="TRI:SourceReductionNAIndicator"/>
						</SOURCE_REDUCTION_NA_INDICATOR>
						<SUBMISSION_ADDITIONAL_DATA_INDICATOR>
							<xsl:value-of select="TRI:SubmissionAdditionalDataIndicator"/>
						</SUBMISSION_ADDITIONAL_DATA_INDICATOR>
						<OPTIONAL_INFORMATION_TEXT>
							<xsl:value-of select="TRI:OptionalInformationText"/>
						</OPTIONAL_INFORMATION_TEXT>
                        <MISCELLANEOUS_INFORMATION_TEXT>
							<xsl:value-of select="TRI:MiscellaneousInformationText"/>
						</MISCELLANEOUS_INFORMATION_TEXT>
					</SYS_REPORT>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_FACILITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Facility">
				<SYS_FACILITY>
					<FACILITY_KEY>
						<xsl:value-of select="FACILITY_KEY"/>
					</FACILITY_KEY>
					<FACILITY_ID>
						<xsl:value-of select="TRI:FacilityIdentifier"/>
					</FACILITY_ID>
					<FACILITY_ACCESS_CODE>
						<xsl:value-of select="TRI:FacilityAccessDetails/TRI:FacilityAccessCode"/>
					</FACILITY_ACCESS_CODE>
					<PRIOR_YEAR_TECHNICAL_CONTACT_NAME_TEXT>
						<xsl:value-of select="TRI:FacilityAccessDetails/TRI:PriorYearTechnicalContactDetails/TRI:PriorYearTechnicalContactNameText"/>
					</PRIOR_YEAR_TECHNICAL_CONTACT_NAME_TEXT>
					<PRIOR_YEAR_TECHNICAL_CONTACT_TELEPHONE_NUMBER_TEXT>
						<xsl:value-of select="TRI:FacilityAccessDetails/TRI:PriorYearTechnicalContactDetails/TRI:PriorYearTechnicalContactTelephoneNumberText"/>
					</PRIOR_YEAR_TECHNICAL_CONTACT_TELEPHONE_NUMBER_TEXT>
					<FACILITY_SITE_NAME>
						<xsl:value-of select="sc:FacilitySiteName"/>
					</FACILITY_SITE_NAME>
					<LOCATION_ADDRESS_TEXT>
						<xsl:value-of select="sc:LocationAddress/sc:LocationAddressText"/>
					</LOCATION_ADDRESS_TEXT>
					<SUPPLEMENTAL_LOCATION_TEXT>
						<xsl:value-of select="sc:LocationAddress/sc:SupplementalLocationText"/>
					</SUPPLEMENTAL_LOCATION_TEXT>
					<LOCALITY_NAME>
						<xsl:value-of select="sc:LocationAddress/sc:LocalityName"/>
					</LOCALITY_NAME>
					<STATE_CODELIST_IDENTIFIER>
						<xsl:value-of select="sc:LocationAddress/sc:StateIdentity/sc:StateCodeListIdentifier"/>
					</STATE_CODELIST_IDENTIFIER>
					<STATE_CODE>
						<xsl:value-of select="sc:LocationAddress/sc:StateIdentity/sc:StateCode"/>
					</STATE_CODE>
					<STATE_NAME>
						<xsl:value-of select="sc:LocationAddress/sc:StateIdentity/sc:StateName"/>
					</STATE_NAME>
					<ADDRESS_POSTAL_CODE>
						<xsl:value-of select="sc:LocationAddress/sc:AddressPostalCode"/>
					</ADDRESS_POSTAL_CODE>
					<COUNTRY_CODELIST_IDENTIFIER>
						<xsl:value-of select="sc:LocationAddress/sc:CountryIdentity/sc:CountryCodeListIdentifier"/>
					</COUNTRY_CODELIST_IDENTIFIER>
					<COUNTRY_CODE>
						<xsl:value-of select="sc:LocationAddress/sc:CountryIdentity/sc:CountryCode"/>
					</COUNTRY_CODE>
					<COUNTRY_NAME>
						<xsl:value-of select="sc:LocationAddress/sc:CountryIdentity/sc:CountryName"/>
					</COUNTRY_NAME>
					<COUNTY_CODELIST_IDENTIFIER>
						<xsl:value-of select="sc:LocationAddress/sc:CountyIdentity/sc:CountyCodeListIdentifier"/>
					</COUNTY_CODELIST_IDENTIFIER>
					<COUNTY_CODE>
						<xsl:value-of select="sc:LocationAddress/sc:CountyIdentity/sc:CountyCode"/>
					</COUNTY_CODE>
					<COUNTY_NAME>
						<xsl:value-of select="sc:LocationAddress/sc:CountyIdentity/sc:CountyName"/>
					</COUNTY_NAME>
					<TRIBAL_CODELIST_IDENTIFIER>
						<xsl:value-of select="sc:LocationAddress/sc:TribalIdentity/sc:TribalCodeListIdentifier"/>
					</TRIBAL_CODELIST_IDENTIFIER>
					<TRIBAL_CODE>
						<xsl:value-of select="sc:LocationAddress/sc:TribalIdentity/sc:TribalCode"/>
					</TRIBAL_CODE>
					<TRIBAL_NAME>
						<xsl:value-of select="sc:LocationAddress/sc:TribalIdentity/sc:TribalName"/>
					</TRIBAL_NAME>
					<TRIBAL_LAND_NAME>
						<xsl:value-of select="sc:LocationAddress/sc:TribalLandName"/>
					</TRIBAL_LAND_NAME>
					<TRIBAL_LAND_INDICATOR>
						<xsl:value-of select="sc:LocationAddress/sc:TribalLandIndicator"/>
					</TRIBAL_LAND_INDICATOR>
					<LOCATION_DESCRIPTION_TEXT>
						<xsl:value-of select="sc:LocationAddress/sc:LocationDescriptionText"/>
					</LOCATION_DESCRIPTION_TEXT>
					<MAILING_FACILITY_SITE_NAME>
						<xsl:value-of select="TRI:MailingFacilitySiteName"/>
					</MAILING_FACILITY_SITE_NAME>
					<MAILING_ADDRESS_TEXT>
						<xsl:value-of select="TRI:MailingAddress/sc:MailingAddressText"/>
					</MAILING_ADDRESS_TEXT>
					<SUPPLEMENTAL_MAILING_ADDRESS_TEXT>
						<xsl:value-of select="TRI:MailingAddress/sc:SupplementalAddressText"/>
					</SUPPLEMENTAL_MAILING_ADDRESS_TEXT>
					<MAILING_ADDRESS_CITY_NAME>
						<xsl:value-of select="TRI:MailingAddress/sc:MailingAddressCityName"/>
					</MAILING_ADDRESS_CITY_NAME>
					<MAILING_ADDRESS_POSTAL_CODE>
						<xsl:value-of select="TRI:MailingAddress/sc:AddressPostalCode"/>
					</MAILING_ADDRESS_POSTAL_CODE>
					<PROVINCE_NAME_TEXT>
						<xsl:value-of select="TRI:MailingAddress/TRI:ProvinceNameText"/>
					</PROVINCE_NAME_TEXT>
					<MAILING_ADDRESS_STATE_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:MailingAddress/sc:StateIdentity/sc:StateCodeListIdentifier"/>
					</MAILING_ADDRESS_STATE_CODELIST_IDENTIFIER>
					<MAILING_ADDRESS_STATE_CODE>
						<xsl:value-of select="TRI:MailingAddress/sc:StateIdentity/sc:StateCode"/>
					</MAILING_ADDRESS_STATE_CODE>
					<MAILING_ADDRESS_STATE_NAME>
						<xsl:value-of select="TRI:MailingAddress/sc:StateIdentity/sc:StateName"/>
					</MAILING_ADDRESS_STATE_NAME>
					<MAILING_ADDRESS_COUNTRY_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:MailingAddress/sc:CountryIdentity/sc:CountryCodeListIdentifier"/>
					</MAILING_ADDRESS_COUNTRY_CODELIST_IDENTIFIER>
					<MAILING_ADDRESS_COUNTRY_CODE>
						<xsl:value-of select="TRI:MailingAddress/sc:CountryIdentity/sc:CountryCode"/>
					</MAILING_ADDRESS_COUNTRY_CODE>
					<MAILING_ADDRESS_COUNTRY_NAME>
						<xsl:value-of select="TRI:MailingAddress/sc:CountryIdentity/sc:CountryName"/>
					</MAILING_ADDRESS_COUNTRY_NAME>
					<LATITUDE_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:LatitudeMeasure"/>
					</LATITUDE_MEASURE>
					<LONGITUDE_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:LongitudeMeasure"/>
					</LONGITUDE_MEASURE>
					<SOURCE_MAP_SCALE_NUMBER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:SourceMapScaleNumber"/>
					</SOURCE_MAP_SCALE_NUMBER>
					<HOR_MEASURE_VALUE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:MeasureValue"/>
					</HOR_MEASURE_VALUE>
					<HOR_MEASURE_UNIT_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:MeasureUnit/sc:MeasureUnitCode"/>
					</HOR_MEASURE_UNIT_CODE>
					<HOR_MEASURE_UNIT_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:MeasureUnit/sc:MeasureUnitCodeListIdentifier"/>
					</HOR_MEASURE_UNIT_CODELIST_IDENTIFIER>
					<HOR_MEASURE_UNIT_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:MeasureUnit/sc:MeasureUnitName"/>
					</HOR_MEASURE_UNIT_NAME>
					<HOR_MEASURE_PRECISION_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:MeasurePrecisionText"/>
					</HOR_MEASURE_PRECISION_TEXT>
					<HOR_RESULT_QUALIFIER_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:ResultQualifier/sc:ResultQualifierCode"/>
					</HOR_RESULT_QUALIFIER_CODE>
					<HOR_RESULT_QUALIFIER_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:ResultQualifier/sc:ResultQualifierCodeListIdentifier"/>
					</HOR_RESULT_QUALIFIER_CODELIST_IDENTIFIER>
					<HOR_RESULT_QUALIFIER_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalAccuracyMeasure/sc:ResultQualifier/sc:ResultQualifierName"/>
					</HOR_RESULT_QUALIFIER_NAME>
					<HOR_METHOD_IDENTIFIER_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalCollectionMethod/sc:MethodIdentifierCode"/>
					</HOR_METHOD_IDENTIFIER_CODE>
					<HOR_METHOD_IDENTIFIER_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalCollectionMethod/sc:MethodIdentifierCodeListIdentifier"/>
					</HOR_METHOD_IDENTIFIER_CODELIST_IDENTIFIER>
					<HOR_METHOD_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalCollectionMethod/sc:MethodName"/>
					</HOR_METHOD_NAME>
					<HOR_METHOD_DESCRIPTION_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalCollectionMethod/sc:MethodDescriptionText"/>
					</HOR_METHOD_DESCRIPTION_TEXT>
					<HOR_METHOD_DEVIATIONS_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalCollectionMethod/sc:MethodDeviationsText"/>
					</HOR_METHOD_DEVIATIONS_TEXT>
					<GEOGRAPHIC_REFERENCE_POINT_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:GeographicReferencePoint/sc:GeographicReferencePointCode"/>
					</GEOGRAPHIC_REFERENCE_POINT_CODE>
					<REFERENCE_POINT_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:GeographicReferencePoint/sc:GeographicReferencePointCodeListIdentifier"/>
					</REFERENCE_POINT_CODELIST_IDENTIFIER>
					<GEOGRAPHIC_REFERENCE_POINT_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:GeographicReferencePoint/sc:GeographicReferencePointName"/>
					</GEOGRAPHIC_REFERENCE_POINT_NAME>
					<HORIZONTAL_REFERENCE_DATUM_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalReferenceDatum/sc:GeographicReferenceDatumCode"/>
					</HORIZONTAL_REFERENCE_DATUM_CODE>
					<HORIZONTAL_REFERENCE_DATUM_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalReferenceDatum/sc:GeographicReferenceDatumCodeListIdentifier"/>
					</HORIZONTAL_REFERENCE_DATUM_CODELIST_IDENTIFIER>
					<HORIZONTAL_REFERENCE_DATUM_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:HorizontalReferenceDatum/sc:GeographicReferenceDatumName"/>
					</HORIZONTAL_REFERENCE_DATUM_NAME>
					<DATA_COLLECTION_DATE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:DataCollectionDate"/>
					</DATA_COLLECTION_DATE>
					<LOCATION_COMMENTS_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:LocationCommentsText"/>
					</LOCATION_COMMENTS_TEXT>
					<VER_MEASURE_VALUE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:MeasureValue"/>
					</VER_MEASURE_VALUE>
					<VER_MEASURE_UNIT_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:MeasureUnit/sc:MeasureUnitCode"/>
					</VER_MEASURE_UNIT_CODE>
					<VER_MEASURE_UNIT_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:MeasureUnit/sc:MeasureUnitCodeListIdentifier"/>
					</VER_MEASURE_UNIT_CODELIST_IDENTIFIER>
					<VER_MEASURE_UNIT_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:MeasureUnit/sc:MeasureUnitName"/>
					</VER_MEASURE_UNIT_NAME>
					<VER_MEASURE_PRECISION_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:MeasurePrecisionText"/>
					</VER_MEASURE_PRECISION_TEXT>
					<VER_RESULT_QUALIFIER_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:ResultQualifier/sc:ResultQualifierCode"/>
					</VER_RESULT_QUALIFIER_CODE>
					<VER_RESULT_QUALIFIER_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:ResultQualifier/sc:ResultQualifierCodeListIdentifier"/>
					</VER_RESULT_QUALIFIER_CODELIST_IDENTIFIER>
					<VER_RESULT_QUALIFIER_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalMeasure/sc:ResultQualifier/sc:ResultQualifierName"/>
					</VER_RESULT_QUALIFIER_NAME>
					<VER_METHOD_IDENTIFIER_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalCollectionMethod/sc:MethodIdentifierCode"/>
					</VER_METHOD_IDENTIFIER_CODE>
					<VER_METHOD_IDENTIFIER_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalCollectionMethod/sc:MethodIdentifierCodeListIdentifier"/>
					</VER_METHOD_IDENTIFIER_CODELIST_IDENTIFIER>
					<VER_METHOD_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalCollectionMethod/sc:MethodName"/>
					</VER_METHOD_NAME>
					<VER_METHOD_DESCRIPTION_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalCollectionMethod/sc:MethodDescriptionText"/>
					</VER_METHOD_DESCRIPTION_TEXT>
					<VER_METHOD_DEVIATIONS_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalCollectionMethod/sc:MethodDeviationsText"/>
					</VER_METHOD_DEVIATIONS_TEXT>
					<GEOGRAPHIC_REFERENCE_DATUM_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalReferenceDatum/sc:GeographicReferenceDatumCode"/>
					</GEOGRAPHIC_REFERENCE_DATUM_CODE>
					<GEOGRAPHIC_REFERENCE_DATUM_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalReferenceDatum/sc:GeographicReferenceDatumCodeListIdentifier"/>
					</GEOGRAPHIC_REFERENCE_DATUM_CODELIST_IDENTIFIER>
					<GEOGRAPHIC_REFERENCE_DATUM_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerticalReferenceDatum/sc:GeographicReferenceDatumName"/>
					</GEOGRAPHIC_REFERENCE_DATUM_NAME>
					<VERIFICATION_METHOD_IDENTIFIER_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerificationMethod/sc:MethodIdentifierCode"/>
					</VERIFICATION_METHOD_IDENTIFIER_CODE>
					<VERIFICATION_METHOD_IDENTIFIER_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerificationMethod/sc:MethodIdentifierCodeListIdentifier"/>
					</VERIFICATION_METHOD_IDENTIFIER_CODELIST_IDENTIFIER>
					<VERIFICATION_METHOD_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerificationMethod/sc:MethodName"/>
					</VERIFICATION_METHOD_NAME>
					<VERIFICATION_METHOD_DESCRIPTION_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerificationMethod/sc:MethodDescriptionText"/>
					</VERIFICATION_METHOD_DESCRIPTION_TEXT>
					<VERIFICATION_METHOD_DEVIATIONS_TEXT>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:VerificationMethod/sc:MethodDeviationsText"/>
					</VERIFICATION_METHOD_DEVIATIONS_TEXT>
					<COORDINATE_DATA_SOURCE_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:CoordinateDataSource/sc:CoordinateDataSourceCode"/>
					</COORDINATE_DATA_SOURCE_CODE>
					<COORDINATE_DATA_SOURCE_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:CoordinateDataSource/sc:CoordinateDataSourceCodeListIdentifier"/>
					</COORDINATE_DATA_SOURCE_CODELIST_IDENTIFIER>
					<COORDINATE_DATA_SOURCE_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:CoordinateDataSource/sc:CoordinateDataSourceName"/>
					</COORDINATE_DATA_SOURCE_NAME>
					<GEOMETRIC_TYPE_CODE>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:GeometricType/sc:GeometricTypeCode"/>
					</GEOMETRIC_TYPE_CODE>
					<GEOMETRIC_TYPE_CODELIST_IDENTIFIER>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:GeometricType/sc:GeometricTypeCodeListIdentifier"/>
					</GEOMETRIC_TYPE_CODELIST_IDENTIFIER>
					<GEOMETRIC_TYPE_NAME>
						<xsl:value-of select="TRI:GeographicLocationDescription/sc:GeometricType/sc:GeometricTypeName"/>
					</GEOMETRIC_TYPE_NAME>
					<LATITUDE_DEGREE_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/TRI:LatitudeDegreeMeasure"/>
					</LATITUDE_DEGREE_MEASURE>
					<LATITUDE_MINUTE_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/TRI:LatitudeMinuteMeasure"/>
					</LATITUDE_MINUTE_MEASURE>
					<LATITUDE_SECOND_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/TRI:LatitudeSecondMeasure"/>
					</LATITUDE_SECOND_MEASURE>
					<LONGITUDE_DEGREE_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/TRI:LongitudeDegreeMeasure"/>
					</LONGITUDE_DEGREE_MEASURE>
					<LONGITUDE_MINUTE_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/TRI:LongitudeMinuteMeasure"/>
					</LONGITUDE_MINUTE_MEASURE>
					<LONGITUDE_SECOND_MEASURE>
						<xsl:value-of select="TRI:GeographicLocationDescription/TRI:LongitudeSecondMeasure"/>
					</LONGITUDE_SECOND_MEASURE>
					<PARENT_COMPANY_NAME_NA_INDICATOR>
						<xsl:value-of select="TRI:ParentCompanyNameNAIndicator"/>
					</PARENT_COMPANY_NAME_NA_INDICATOR>
					<PARENT_COMPANY_NAME_TEXT>
						<xsl:value-of select="TRI:ParentCompanyNameText"/>
					</PARENT_COMPANY_NAME_TEXT>
					<PARENT_DUN_BRADSTREET_CODE>
						<xsl:value-of select="TRI:ParentDunBradstreetCode"/>
					</PARENT_DUN_BRADSTREET_CODE>
				</SYS_FACILITY>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_FACILITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_FACILITY_DUN_BRADSTREET_CODE_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Facility">
        <xsl:for-each select="TRI:FacilityDunBradstreetCode">
          <SYS_FACILITY_DUN_BRADSTREET_CODE>
            <FACILITY_KEY>
              <xsl:value-of select="../FACILITY_KEY" />
            </FACILITY_KEY>
            <FACILITY_DUN_BRADSTREET_CODE>
              <xsl:value-of select="." />
            </FACILITY_DUN_BRADSTREET_CODE>
          </SYS_FACILITY_DUN_BRADSTREET_CODE>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_FACILITY_DUN_BRADSTREET_CODE_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_FACILITY_NAICS_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Facility">
        <xsl:for-each select="TRI:FacilityNAICS">
          <SYS_FACILITY_NAICS>
            <FACILITY_KEY>
              <xsl:value-of select="../FACILITY_KEY" />
            </FACILITY_KEY>
            <FACILITY_NAICS>
              <xsl:value-of select="sc:NAICSCode" />
            </FACILITY_NAICS>
            <NAICS_PRIMARY_INDICATOR>
              <xsl:value-of select="sc:NAICSPrimaryIndicator" />
            </NAICS_PRIMARY_INDICATOR>
          </SYS_FACILITY_NAICS>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_FACILITY_NAICS_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_FACILITY_NPDES_IDENTIFICATION_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Facility">
        <xsl:for-each select="TRI:NPDESIdentificationNumber">
          <SYS_NPDES_IDENTIFICATION>
            <FACILITY_KEY>
              <xsl:value-of select="../FACILITY_KEY" />
            </FACILITY_KEY>
            <NPDES_IDENTIFICATION_NUMBER>
              <xsl:value-of select="." />
            </NPDES_IDENTIFICATION_NUMBER>
          </SYS_NPDES_IDENTIFICATION>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_FACILITY_NPDES_IDENTIFICATION_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_FACILITY_RCRA_IDENTIFICATION_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Facility">
        <xsl:for-each select="TRI:RCRAIdentificationNumber">
          <SYS_RCRA_IDENTIFICATION>
            <FACILITY_KEY>
              <xsl:value-of select="../FACILITY_KEY" />
            </FACILITY_KEY>
            <RCRA_IDENTIFICATION_NUMBER>
              <xsl:value-of select="." />
            </RCRA_IDENTIFICATION_NUMBER>
          </SYS_RCRA_IDENTIFICATION>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_FACILITY_RCRA_IDENTIFICATION_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_FACILITY_SIC_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Facility">
        <xsl:for-each select="TRI:FacilitySIC">
          <SYS_FACILITY_SIC>
            <FACILITY_KEY>
              <xsl:value-of select="../FACILITY_KEY" />
            </FACILITY_KEY>
            <FACILITY_SIC>
              <xsl:value-of select="sc:SICCode" />
            </FACILITY_SIC>
            <SIC_PRIMARY_INDICATOR>
              <xsl:value-of select="sc:SICPrimaryIndicator" />
            </SIC_PRIMARY_INDICATOR>
          </SYS_FACILITY_SIC>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_FACILITY_SIC_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_FACILITY_UIC_IDENTIFICATION_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Facility">
        <xsl:for-each select="TRI:UICIdentificationNumber">
          <SYS_UIC_IDENTIFICATION>
            <FACILITY_KEY>
              <xsl:value-of select="../FACILITY_KEY" />
            </FACILITY_KEY>
            <UIC_IDENTIFICATION_NUMBER>
              <xsl:value-of select="." />
            </UIC_IDENTIFICATION_NUMBER>
          </SYS_UIC_IDENTIFICATION>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_FACILITY_UIC_IDENTIFICATION_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_CHEMICAL_IDENTIFICATION_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:ChemicalIdentification">
          <SYS_CHEMICAL_IDENTIFICATION>
            <REPORT_KEY>
              <xsl:value-of select="../REPORT_KEY" />
            </REPORT_KEY>
            <CAS_NUMBER>
              <xsl:value-of select="sc:CASNumber" />
            </CAS_NUMBER>
            <CHEMICAL_NAME_TEXT>
              <xsl:value-of select="TRI:ChemicalNameText" />
            </CHEMICAL_NAME_TEXT>
            <CHEMICAL_MIXTURE_NAME_TEXT>
              <xsl:value-of select="TRI:ChemicalMixtureNameText" />
            </CHEMICAL_MIXTURE_NAME_TEXT>
            <CHEMICAL_IDENTIFIER>
              <xsl:value-of select="sc:EPAChemicalIdentifier" />
            </CHEMICAL_IDENTIFIER>
            <CHEMICAL_REGISTRY_NAME>
              <xsl:value-of select="sc:EPAChemicalRegistryName" />
            </CHEMICAL_REGISTRY_NAME>
            <CHEMICAL_REGISTRY_NAME_CONTEXT>
              <xsl:value-of select="sc:EPAChemicalRegistryNameContext" />
            </CHEMICAL_REGISTRY_NAME_CONTEXT>
            <DIOXIN_DISTRIBUTION_1_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution1Percent" />
            </DIOXIN_DISTRIBUTION_1_PERCENT>
            <DIOXIN_DISTRIBUTION_2_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution2Percent" />
            </DIOXIN_DISTRIBUTION_2_PERCENT>
            <DIOXIN_DISTRIBUTION_3_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution3Percent" />
            </DIOXIN_DISTRIBUTION_3_PERCENT>
            <DIOXIN_DISTRIBUTION_4_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution4Percent" />
            </DIOXIN_DISTRIBUTION_4_PERCENT>
            <DIOXIN_DISTRIBUTION_5_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution5Percent" />
            </DIOXIN_DISTRIBUTION_5_PERCENT>
            <DIOXIN_DISTRIBUTION_6_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution6Percent" />
            </DIOXIN_DISTRIBUTION_6_PERCENT>
            <DIOXIN_DISTRIBUTION_7_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution7Percent" />
            </DIOXIN_DISTRIBUTION_7_PERCENT>
            <DIOXIN_DISTRIBUTION_8_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution8Percent" />
            </DIOXIN_DISTRIBUTION_8_PERCENT>
            <DIOXIN_DISTRIBUTION_9_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution9Percent" />
            </DIOXIN_DISTRIBUTION_9_PERCENT>
            <DIOXIN_DISTRIBUTION_10_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution10Percent" />
            </DIOXIN_DISTRIBUTION_10_PERCENT>
            <DIOXIN_DISTRIBUTION_11_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution11Percent" />
            </DIOXIN_DISTRIBUTION_11_PERCENT>
            <DIOXIN_DISTRIBUTION_12_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution12Percent" />
            </DIOXIN_DISTRIBUTION_12_PERCENT>
            <DIOXIN_DISTRIBUTION_13_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution13Percent" />
            </DIOXIN_DISTRIBUTION_13_PERCENT>
            <DIOXIN_DISTRIBUTION_14_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution14Percent" />
            </DIOXIN_DISTRIBUTION_14_PERCENT>
            <DIOXIN_DISTRIBUTION_15_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution15Percent" />
            </DIOXIN_DISTRIBUTION_15_PERCENT>
            <DIOXIN_DISTRIBUTION_16_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution16Percent" />
            </DIOXIN_DISTRIBUTION_16_PERCENT>
            <DIOXIN_DISTRIBUTION_17_PERCENT>
              <xsl:value-of select="TRI:DioxinDistribution17Percent" />
            </DIOXIN_DISTRIBUTION_17_PERCENT>
            <DIOXIN_DISTRIBUTION_NA_INDICATOR>
              <xsl:value-of select="TRI:DioxinDistributionNAIndicator" />
            </DIOXIN_DISTRIBUTION_NA_INDICATOR>
          </SYS_CHEMICAL_IDENTIFICATION>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_CHEMICAL_IDENTIFICATION_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_OFFSITE_ENERGY_RECOVERY_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OffsiteEnergyRecoveryQuantity">
					<SYS_OFFSITE_ENERGY_RECOVERY_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_OFFSITE_ENERGY_RECOVERY_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_OFFSITE_ENERGY_RECOVERY_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_OFFSITE_OTHER_DISPOSAL_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OffsiteOtherDisposalQuantity">
					<SYS_OFFSITE_OTHER_DISPOSAL_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_OFFSITE_OTHER_DISPOSAL_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_OFFSITE_OTHER_DISPOSAL_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_OFFSITE_RECYCLED_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OffsiteRecycledQuantity">
					<SYS_OFFSITE_RECYCLED_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_OFFSITE_RECYCLED_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_OFFSITE_RECYCLED_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_OFFSITE_TREATED_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OffsiteTreatedQuantity">
					<SYS_OFFSITE_TREATED_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_OFFSITE_TREATED_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_OFFSITE_TREATED_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_OFFSITE_UIC_DISPOSAL_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OffsiteUICDisposalQuantity">
					<SYS_OFFSITE_UIC_DISPOSAL_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_OFFSITE_UIC_DISPOSAL_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_OFFSITE_UIC_DISPOSAL_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_ENERGY_RECOVERY_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OnsiteEnergyRecoveryQuantity">
					<SYS_ONSITE_ENERGY_RECOVERY_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_ONSITE_ENERGY_RECOVERY_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_ENERGY_RECOVERY_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_OTHER_DISPOSAL_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OnsiteOtherDisposalQuantity">
					<SYS_ONSITE_OTHER_DISPOSAL_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_ONSITE_OTHER_DISPOSAL_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_OTHER_DISPOSAL_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_RECOVERY_PROCESS_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:OnsiteRecoveryProcess/TRI:EnergyRecoveryMethodCode">
          <SYS_ONSITE_RECOVERY_PROCESS>
            <REPORT_KEY>
              <xsl:value-of select="../../REPORT_KEY" />
            </REPORT_KEY>
            <ENERGY_RECOVERY_METHOD_CODE>
              <xsl:value-of select="." />
            </ENERGY_RECOVERY_METHOD_CODE>
          </SYS_ONSITE_RECOVERY_PROCESS>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_RECOVERY_PROCESS_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_RECYCLED_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OnsiteRecycledQuantity">
					<SYS_ONSITE_RECYCLED_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_ONSITE_RECYCLED_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_RECYCLED_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_RECYCLING_PROCESS_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:OnsiteRecyclingProcess/TRI:OnsiteRecyclingMethodCode">
          <SYS_ONSITE_RECYCLING_PROCESS>
            <REPORT_KEY>
              <xsl:value-of select="../../REPORT_KEY" />
            </REPORT_KEY>
            <ONSITE_RECYCLING_METHOD_CODE>
              <xsl:value-of select="." />
            </ONSITE_RECYCLING_METHOD_CODE>
          </SYS_ONSITE_RECYCLING_PROCESS>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_RECYCLING_PROCESS_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_RELEASE_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
	<xsl:template match="/">
	 	<xsl:apply-templates />
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:OnsiteReleaseQuantity">
					<SYS_ONSITE_RELEASE_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../REPORT_KEY"/>
						</REPORT_KEY>
						<ENVIRONMENTAL_MEDIUM_CODE>
							<xsl:value-of select="TRI:EnvironmentalMediumCode"/>
						</ENVIRONMENTAL_MEDIUM_CODE>
						<WASTE_QUANTITY_MEASURE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure"/>
						</WASTE_QUANTITY_MEASURE>
						<WASTE_QUANTITY_CATASTROPHIC_MEASURE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityCatastrophicMeasure"/>
						</WASTE_QUANTITY_CATASTROPHIC_MEASURE>
						<WASTE_QUANTITY_RANGE_CODE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode"/>
						</WASTE_QUANTITY_RANGE_CODE>
						<WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeNumericBasisValue"/>
						</WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
						<WASTE_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator"/>
						</WASTE_QUANTITY_NA_INDICATOR>
						<QUANTITY_BASIS_ESTIMATION_CODE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode"/>
						</QUANTITY_BASIS_ESTIMATION_CODE>
						<QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationNAIndicator"/>
						</QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
						<WATER_SEQUENCE_NUMBER>
							<xsl:value-of select="TRI:WaterStream/TRI:WaterSequenceNumber"/>
						</WATER_SEQUENCE_NUMBER>
						<STREAM_NAME>
							<xsl:value-of select="TRI:WaterStream/TRI:StreamName"/>
						</STREAM_NAME>
						<RELEASE_STORM_WATER_PERCENT>
							<xsl:value-of select="TRI:WaterStream/TRI:ReleaseStormWaterPercent"/>
						</RELEASE_STORM_WATER_PERCENT>
						<RELEASE_STORM_WATER_NA_INDICATOR>
							<xsl:value-of select="TRI:WaterStream/TRI:ReleaseStormWaterNAIndicator"/>
						</RELEASE_STORM_WATER_NA_INDICATOR>
					</SYS_ONSITE_RELEASE_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_RELEASE_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_TREATED_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OnsiteTreatedQuantity">
					<SYS_ONSITE_TREATED_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_ONSITE_TREATED_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_TREATED_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_ONSITE_UIC_DISPOSAL_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:SourceReductionQuantity/TRI:OnsiteUICDisposalQuantity">
					<SYS_ONSITE_UIC_DISPOSAL_QUANTITY>
						<REPORT_KEY>
							<xsl:value-of select="../../REPORT_KEY"/>
						</REPORT_KEY>
						<YEAR_OFFSET_MEASURE>
							<xsl:value-of select="TRI:YearOffsetMeasure"/>
						</YEAR_OFFSET_MEASURE>
						<TOTAL_QUANTITY>
							<xsl:value-of select="TRI:TotalQuantity"/>
						</TOTAL_QUANTITY>
						<CALCULATOR_ROUNDING_HINT_NUMBER>
							<xsl:value-of select="TRI:CalculatorRoundingHintNumber"/>
						</CALCULATOR_ROUNDING_HINT_NUMBER>
						<TOTAL_QUANTITY_NA_INDICATOR>
							<xsl:value-of select="TRI:TotalQuantityNAIndicator"/>
						</TOTAL_QUANTITY_NA_INDICATOR>
						<TOXIC_EQUIVALENCY_1_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
						</TOXIC_EQUIVALENCY_1_VALUE>
						<TOXIC_EQUIVALENCY_2_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
						</TOXIC_EQUIVALENCY_2_VALUE>
						<TOXIC_EQUIVALENCY_3_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
						</TOXIC_EQUIVALENCY_3_VALUE>
						<TOXIC_EQUIVALENCY_4_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
						</TOXIC_EQUIVALENCY_4_VALUE>
						<TOXIC_EQUIVALENCY_5_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
						</TOXIC_EQUIVALENCY_5_VALUE>
						<TOXIC_EQUIVALENCY_6_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
						</TOXIC_EQUIVALENCY_6_VALUE>
						<TOXIC_EQUIVALENCY_7_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
						</TOXIC_EQUIVALENCY_7_VALUE>
						<TOXIC_EQUIVALENCY_8_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
						</TOXIC_EQUIVALENCY_8_VALUE>
						<TOXIC_EQUIVALENCY_9_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
						</TOXIC_EQUIVALENCY_9_VALUE>
						<TOXIC_EQUIVALENCY_10_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
						</TOXIC_EQUIVALENCY_10_VALUE>
						<TOXIC_EQUIVALENCY_11_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
						</TOXIC_EQUIVALENCY_11_VALUE>
						<TOXIC_EQUIVALENCY_12_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
						</TOXIC_EQUIVALENCY_12_VALUE>
						<TOXIC_EQUIVALENCY_13_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
						</TOXIC_EQUIVALENCY_13_VALUE>
						<TOXIC_EQUIVALENCY_14_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
						</TOXIC_EQUIVALENCY_14_VALUE>
						<TOXIC_EQUIVALENCY_15_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
						</TOXIC_EQUIVALENCY_15_VALUE>
						<TOXIC_EQUIVALENCY_16_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
						</TOXIC_EQUIVALENCY_16_VALUE>
						<TOXIC_EQUIVALENCY_17_VALUE>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
						</TOXIC_EQUIVALENCY_17_VALUE>
						<TOXIC_EQUIVALENCY_NA_INDICATOR>
							<xsl:value-of select="TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
						</TOXIC_EQUIVALENCY_NA_INDICATOR>
					</SYS_ONSITE_UIC_DISPOSAL_QUANTITY>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_ONSITE_UIC_DISPOSAL_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_REPLACED_REPORT_IDENTIFIER_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="sc:ReplacedReportIdentifier">
          <SYS_REPLACED_REPORT_IDENTIFIER>
            <REPORT_KEY>
              <xsl:value-of select="../REPORT_KEY" />
            </REPORT_KEY>
            <REPLACED_REPORT_IDENTIFIER>
              <xsl:value-of select="." />
            </REPLACED_REPORT_IDENTIFIER>
          </SYS_REPLACED_REPORT_IDENTIFIER>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_REPLACED_REPORT_IDENTIFIER_5_0';

/****** Object:  update XSLT in TRI_REPORT_SOURCE_REDUCTION_ACTIVITY_5_0 for TRI_FACILITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:SourceReductionActivity">
          <SYS_SOURCE_REDUCTION_ACTIVITY>
            <REPORT_KEY>
              <xsl:value-of select="../REPORT_KEY" />
            </REPORT_KEY>
            <SOURCE_REDUCTION_ACTIVITY_KEY>
              <xsl:value-of select="SOURCE_REDUCTION_ACTIVITY_KEY" />
            </SOURCE_REDUCTION_ACTIVITY_KEY>
            <SOURCE_REDUCTION_SEQUENCE_NUMBER>
              <xsl:value-of select="TRI:SourceReductionSequenceNumber" />
            </SOURCE_REDUCTION_SEQUENCE_NUMBER>
            <SOURCE_REDUCTION_ACTIVITY_CODE>
              <xsl:value-of select="TRI:SourceReductionActivityCode" />
            </SOURCE_REDUCTION_ACTIVITY_CODE>
          </SYS_SOURCE_REDUCTION_ACTIVITY>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_SOURCE_REDUCTION_ACTIVITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_SOURCE_REDUCTION_METHOD_CODE_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:SourceReductionActivity">
          <xsl:for-each select="TRI:SourceReductionMethodCode">
            <SYS_SOURCE_REDUCTION_METHOD_CODE>
              <SOURCE_REDUCTION_ACTIVITY_KEY>
                <xsl:value-of select="../SOURCE_REDUCTION_ACTIVITY_KEY" />
              </SOURCE_REDUCTION_ACTIVITY_KEY>
              <SOURCE_REDUCTION_METHOD_CODE>
                <xsl:value-of select="." />
              </SOURCE_REDUCTION_METHOD_CODE>
            </SYS_SOURCE_REDUCTION_METHOD_CODE>
          </xsl:for-each>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_SOURCE_REDUCTION_METHOD_CODE_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_TRANSFER_LOCATION_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:TransferLocation">
          <SYS_TRANSFER_LOCATION>
            <REPORT_KEY>
              <xsl:value-of select="../REPORT_KEY" />
            </REPORT_KEY>
            <TRANSFER_LOCATION_KEY>
              <xsl:value-of select="TRANSFER_LOCATION_KEY" />
            </TRANSFER_LOCATION_KEY>
            <TRANSFER_LOCATION_SEQUENCE_NUMBER>
              <xsl:value-of select="TRI:TransferLocationSequenceNumber" />
            </TRANSFER_LOCATION_SEQUENCE_NUMBER>
            <POTW_INDICATOR>
              <xsl:value-of select="TRI:POTWIndicator" />
            </POTW_INDICATOR>
            <FACILITY_SITE_NAME>
              <xsl:value-of select="sc:FacilitySiteName" />
            </FACILITY_SITE_NAME>
            <LOCATION_ADDRESS_TEXT>
              <xsl:value-of select="sc:LocationAddress/sc:LocationAddressText" />
            </LOCATION_ADDRESS_TEXT>
            <SUPPLEMENTAL_LOCATION_TEXT>
              <xsl:value-of select="sc:LocationAddress/sc:SupplementalLocationText" />
            </SUPPLEMENTAL_LOCATION_TEXT>
            <LOCALITY_NAME>
              <xsl:value-of select="sc:LocationAddress/sc:LocalityName" />
            </LOCALITY_NAME>
            <STATE_CODELIST_IDENTIFIER>
              <xsl:value-of select="sc:LocationAddress/sc:StateIdentity/sc:StateCodeListIdentifier" />
            </STATE_CODELIST_IDENTIFIER>
            <STATE_CODE>
              <xsl:value-of select="sc:LocationAddress/sc:StateIdentity/sc:StateCode" />
            </STATE_CODE>
            <STATE_NAME>
              <xsl:value-of select="sc:LocationAddress/sc:StateIdentity/sc:StateName" />
            </STATE_NAME>
            <ADDRESS_POSTAL_CODE>
              <xsl:value-of select="sc:LocationAddress/sc:AddressPostalCode" />
            </ADDRESS_POSTAL_CODE>
            <COUNTRY_CODELIST_IDENTIFIER>
              <xsl:value-of select="sc:LocationAddress/sc:CountryIdentity/sc:CountryCodeListIdentifier" />
            </COUNTRY_CODELIST_IDENTIFIER>
            <COUNTRY_CODE>
              <xsl:value-of select="sc:LocationAddress/sc:CountryIdentity/sc:CountryCode" />
            </COUNTRY_CODE>
            <COUNTRY_NAME>
              <xsl:value-of select="sc:LocationAddress/sc:CountryIdentity/sc:CountryName" />
            </COUNTRY_NAME>
            <COUNTY_CODELIST_IDENTIFIER>
              <xsl:value-of select="sc:LocationAddress/sc:CountyIdentity/sc:CountyCodeListIdentifier" />
            </COUNTY_CODELIST_IDENTIFIER>
            <COUNTY_CODE>
              <xsl:value-of select="sc:LocationAddress/sc:CountyIdentity/sc:CountyCode" />
            </COUNTY_CODE>
            <COUNTY_NAME>
              <xsl:value-of select="sc:LocationAddress/sc:CountyIdentity/sc:CountyName" />
            </COUNTY_NAME>
            <TRIBAL_CODELIST_IDENTIFIER>
              <xsl:value-of select="sc:LocationAddress/sc:TribalIdentity/sc:TribalCodeListIdentifier" />
            </TRIBAL_CODELIST_IDENTIFIER>
            <TRIBAL_CODE>
              <xsl:value-of select="sc:LocationAddress/sc:TribalIdentity/sc:TribalCode" />
            </TRIBAL_CODE>
            <TRIBAL_NAME>
              <xsl:value-of select="sc:LocationAddress/sc:TribalIdentity/sc:TribalName" />
            </TRIBAL_NAME>
            <TRIBAL_LAND_NAME>
              <xsl:value-of select="sc:LocationAddress/sc:TribalLandName" />
            </TRIBAL_LAND_NAME>
            <TRIBAL_LAND_INDICATOR>
              <xsl:value-of select="sc:LocationAddress/sc:TribalLandIndicator" />
            </TRIBAL_LAND_INDICATOR>
            <LOCATION_DESCRIPTION_TEXT>
              <xsl:value-of select="sc:LocationAddress/sc:LocationDescriptionText" />
            </LOCATION_DESCRIPTION_TEXT>
            <CONTROLLED_LOCATION_INDICATOR>
              <xsl:value-of select="TRI:ControlledLocationIndicator" />
            </CONTROLLED_LOCATION_INDICATOR>
            <RCRA_IDENTIFICATION_NUMBER>
              <xsl:value-of select="TRI:RCRAIdentificationNumber" />
            </RCRA_IDENTIFICATION_NUMBER>
          </SYS_TRANSFER_LOCATION>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_TRANSFER_LOCATION_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_TRANSFER_QUANTITY_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="xml"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template match="/enh:Document/Header"/>
	<xsl:template match="/enh:Document/Payload/TRIME_Metadata"/>
	<xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
		<NewDataSet>
			<xsl:for-each select="TRI:Submission/TRI:Report">
				<xsl:for-each select="TRI:TransferLocation">
					<xsl:for-each select="TRI:TransferQuantity">
						<SYS_TRANSFER_QUANTITY>
							<TRANSFER_LOCATION_KEY>
								<xsl:value-of select="../TRANSFER_LOCATION_KEY"/>
							</TRANSFER_LOCATION_KEY>
							<TRANSFER_SEQUENCE_NUMBER>
								<xsl:value-of select="TRI:TransferSequenceNumber"/>
							</TRANSFER_SEQUENCE_NUMBER>
							<WASTE_QUANTITY_MEASURE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:WasteQuantityMeasure"/>
							</WASTE_QUANTITY_MEASURE>
							<WASTE_QUANTITY_CATASTROPHIC_MEASURE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:WasteQuantityCatastrophicMeasure"/>
							</WASTE_QUANTITY_CATASTROPHIC_MEASURE>
							<WASTE_QUANTITY_RANGE_CODE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:WasteQuantityRangeCode"/>
							</WASTE_QUANTITY_RANGE_CODE>
							<WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:WasteQuantityRangeNumericBasisValue"/>
							</WASTE_QUANTITY_RANGE_NUMERIC_BASIS_VALUE>
							<WASTE_QUANTITY_NA_INDICATOR>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:WasteQuantityNAIndicator"/>
							</WASTE_QUANTITY_NA_INDICATOR>
							<QUANTITY_BASIS_ESTIMATION_CODE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:QuantityBasisEstimationCode"/>
							</QUANTITY_BASIS_ESTIMATION_CODE>
							<QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:QuantityBasisEstimationNAIndicator"/>
							</QUANTITY_BASIS_ESTIMATION_NA_INDICATOR>
							<TOXIC_EQUIVALENCY_1_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency1Value"/>
							</TOXIC_EQUIVALENCY_1_VALUE>
							<TOXIC_EQUIVALENCY_2_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency2Value"/>
							</TOXIC_EQUIVALENCY_2_VALUE>
							<TOXIC_EQUIVALENCY_3_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency3Value"/>
							</TOXIC_EQUIVALENCY_3_VALUE>
							<TOXIC_EQUIVALENCY_4_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency4Value"/>
							</TOXIC_EQUIVALENCY_4_VALUE>
							<TOXIC_EQUIVALENCY_5_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency5Value"/>
							</TOXIC_EQUIVALENCY_5_VALUE>
							<TOXIC_EQUIVALENCY_6_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency6Value"/>
							</TOXIC_EQUIVALENCY_6_VALUE>
							<TOXIC_EQUIVALENCY_7_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency7Value"/>
							</TOXIC_EQUIVALENCY_7_VALUE>
							<TOXIC_EQUIVALENCY_8_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency8Value"/>
							</TOXIC_EQUIVALENCY_8_VALUE>
							<TOXIC_EQUIVALENCY_9_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency9Value"/>
							</TOXIC_EQUIVALENCY_9_VALUE>
							<TOXIC_EQUIVALENCY_10_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency10Value"/>
							</TOXIC_EQUIVALENCY_10_VALUE>
							<TOXIC_EQUIVALENCY_11_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency11Value"/>
							</TOXIC_EQUIVALENCY_11_VALUE>
							<TOXIC_EQUIVALENCY_12_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency12Value"/>
							</TOXIC_EQUIVALENCY_12_VALUE>
							<TOXIC_EQUIVALENCY_13_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency13Value"/>
							</TOXIC_EQUIVALENCY_13_VALUE>
							<TOXIC_EQUIVALENCY_14_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency14Value"/>
							</TOXIC_EQUIVALENCY_14_VALUE>
							<TOXIC_EQUIVALENCY_15_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency15Value"/>
							</TOXIC_EQUIVALENCY_15_VALUE>
							<TOXIC_EQUIVALENCY_16_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency16Value"/>
							</TOXIC_EQUIVALENCY_16_VALUE>
							<TOXIC_EQUIVALENCY_17_VALUE>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalency17Value"/>
							</TOXIC_EQUIVALENCY_17_VALUE>
							<TOXIC_EQUIVALENCY_NA_INDICATOR>
								<xsl:value-of select="TRI:TransferWasteQuantity/TRI:ToxicEquivalencyIdentification/TRI:ToxicEquivalencyNAIndicator"/>
							</TOXIC_EQUIVALENCY_NA_INDICATOR>
							<WASTE_MANAGEMENT_TYPE_CODE>
								<xsl:value-of select="TRI:WasteManagementTypeCode"/>
							</WASTE_MANAGEMENT_TYPE_CODE>
						</SYS_TRANSFER_QUANTITY>
					</xsl:for-each>
				</xsl:for-each>
			</xsl:for-each>
		</NewDataSet>
	</xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_TRANSFER_QUANTITY_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_VALIDATION_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:ReportMetaData/TRI:ReportValidation">
          <SYS_REPORT_VALIDATION>
            <REPORT_KEY>
              <xsl:value-of select="../../REPORT_KEY" />
            </REPORT_KEY>
            <VALIDATION_ENTITY_NAME_TEXT>
              <xsl:value-of select="TRI:ValidationEntityNameText" />
            </VALIDATION_ENTITY_NAME_TEXT>
            <VALIDATION_MESSAGE_CODE>
              <xsl:value-of select="TRI:ValidationMessageCode" />
            </VALIDATION_MESSAGE_CODE>
            <VALIDATION_MESSAGE_TEXT>
              <xsl:value-of select="TRI:ValidationMessageText" />
            </VALIDATION_MESSAGE_TEXT>
            <EPA_ERROR_SEVERITY_CODE>
              <xsl:value-of select="TRI:EPAErrorSeverityCode" />
            </EPA_ERROR_SEVERITY_CODE>
          </SYS_REPORT_VALIDATION>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_VALIDATION_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_WASTE_TREATMENT_DETAILS_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:WasteTreatmentDetails">
          <SYS_WASTE_TREATMENT_DETAILS>
            <REPORT_KEY>
              <xsl:value-of select="../REPORT_KEY" />
            </REPORT_KEY>
            <WASTE_TREATMENT_DETAILS_KEY>
              <xsl:value-of select="WASTE_TREATMENT_DETAILS_KEY" />
            </WASTE_TREATMENT_DETAILS_KEY>
            <WASTE_STREAM_SEQUENCE_NUMBER>
              <xsl:value-of select="TRI:WasteStreamSequenceNumber" />
            </WASTE_STREAM_SEQUENCE_NUMBER>
            <WASTE_STREAM_TYPE_CODE>
              <xsl:value-of select="TRI:WasteStreamTypeCode" />
            </WASTE_STREAM_TYPE_CODE>
            <INFLUENT_CONCENTRATION_RANGE_CODE>
              <xsl:value-of select="TRI:InfluentConcentrationRangeCode" />
            </INFLUENT_CONCENTRATION_RANGE_CODE>
            <TREATMENT_EFFICIENCY_ESTIMATE_PERCENT>
              <xsl:value-of select="TRI:TreatmentEfficiencyEstimatePercent" />
            </TREATMENT_EFFICIENCY_ESTIMATE_PERCENT>
            <TREATMENT_EFFICIENCY_RANGE_CODE>
              <xsl:value-of select="TRI:TreatmentEfficiencyRangeCode" />
            </TREATMENT_EFFICIENCY_RANGE_CODE>
            <TREATMENT_EFFICIENCY_NA_INDICATOR>
              <xsl:value-of select="TRI:TreatmentEfficiencyNAIndicator" />
            </TREATMENT_EFFICIENCY_NA_INDICATOR>
            <OPERATING_DATA_INDICATOR>
              <xsl:value-of select="TRI:OperatingDataIndicator" />
            </OPERATING_DATA_INDICATOR>
          </SYS_WASTE_TREATMENT_DETAILS>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_WASTE_TREATMENT_DETAILS_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_REPORT_WASTE_TREATMENT_METHOD_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission/TRI:Report">
        <xsl:for-each select="TRI:WasteTreatmentDetails">
          <xsl:for-each select="TRI:WasteTreatmentMethod">
            <SYS_WASTE_TREATMENT_METHOD>
              <WASTE_TREATMENT_DETAILS_KEY>
                <xsl:value-of select="../WASTE_TREATMENT_DETAILS_KEY" />
              </WASTE_TREATMENT_DETAILS_KEY>
              <WASTE_TREATMENT_SEQUENCE_NUMBER>
                <xsl:value-of select="TRI:WasteTreatmentSequenceNumber" />
              </WASTE_TREATMENT_SEQUENCE_NUMBER>
              <WASTE_TREATMENT_METHOD_CODE>
                <xsl:value-of select="TRI:WasteTreatmentMethodCode" />
              </WASTE_TREATMENT_METHOD_CODE>
            </SYS_WASTE_TREATMENT_METHOD>
          </xsl:for-each>
        </xsl:for-each>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_REPORT_WASTE_TREATMENT_METHOD_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_SUBMISSION_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission">
        <SYS_TRI_SUBMISSION>
          <TRI_SUBMISSION_IDENTIFIER>
            <xsl:value-of select="TRI:TRISubmissionIdentifier" />
          </TRI_SUBMISSION_IDENTIFIER>
          <FACILITY_KEY>
            <xsl:value-of select="TRI:Facility/FACILITY_KEY" />
          </FACILITY_KEY>
          <TRANSACTION_TYPE>
            <xsl:text>REPLACE</xsl:text>
          </TRANSACTION_TYPE>
          <STATUS>
            <xsl:text>RECEIVED</xsl:text>
          </STATUS>
        </SYS_TRI_SUBMISSION>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_SUBMISSION_5_0';

/****** Object:  update XSLT in DE_FLOW_TRANSFORMER for TRI_SUBMISSION_TYPE_5_0   ******/
update DE_FLOW_TRANSFORMER set XSLT ='<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:enh="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0" xmlns:msxsl="urn:schemas-microsoft-com:xslt" xmlns:var="http://schemas.microsoft.com/BizTalk/2003/var" exclude-result-prefixes="msxsl var" version="1.0">
  <xsl:output omit-xml-declaration="yes" version="1.0" method="xml" />
  <xsl:template match="/">
    <xsl:apply-templates />
  </xsl:template>
  <xsl:template match="/enh:Document/Header" />
  <xsl:template match="/enh:Document/Payload/TRIME_Metadata" />
  <xsl:template name="TRI" match="/enh:Document/Payload/TRI:TRI | /TRI:TRI">
    <NewDataSet>
      <xsl:for-each select="TRI:Submission">
        <SYS_TRI_SUBMISSION_TYPE>
          <FACILITY_KEY>
            <xsl:value-of select="TRI:Facility/FACILITY_KEY" />
          </FACILITY_KEY>
          <TRI_SUBMISSION_ID>
            <xsl:choose>
              <xsl:when test="TRI:TRISubmissionIdentifier != ''''">
                <xsl:value-of select="TRI:TRISubmissionIdentifier" />
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>Unknown TRI Submission Identifier</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </TRI_SUBMISSION_ID>
          <TRI_SUBMISSION_TYPE>
            <xsl:choose>
              <xsl:when test="substring(TRI:TRISubmissionIdentifier,1,7)=''TRI0001''">
                <xsl:text>TRIME CD</xsl:text>
              </xsl:when>
              <xsl:when test="substring(TRI:TRISubmissionIdentifier,1,7) = ''TRI0002''">
                <xsl:text>TRIME Web</xsl:text>
              </xsl:when>
              <xsl:when test="substring(TRI:TRISubmissionIdentifier,1,6) = ''TRIDPC''">
                <xsl:text>Paper</xsl:text>
              </xsl:when>
              <xsl:when test="substring(TRI:TRISubmissionIdentifier,1,5) = ''DISK_''">
                <xsl:text>Disk</xsl:text>
              </xsl:when>
              <xsl:otherwise>
                <xsl:text>Unknown TRI Submission Identifier</xsl:text>
              </xsl:otherwise>
            </xsl:choose>
          </TRI_SUBMISSION_TYPE>
          <USER_CREATE>
            <xsl:text>Submission Type Stylesheet</xsl:text>
          </USER_CREATE>
          <USER_UPDATE>
            <xsl:text>Submission Type Stylesheet</xsl:text>
          </USER_UPDATE>
        </SYS_TRI_SUBMISSION_TYPE>
      </xsl:for-each>
    </NewDataSet>
  </xsl:template>
</xsl:stylesheet>' 
where TRANSFORMER_NAME = 'TRI_SUBMISSION_TYPE_5_0';
