<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" version="1.0" xmlns:wqx="http://www.exchangenetwork.net/schema/wqx/2" xmlns:hdr="http://www.exchangenetwork.net/schema/v1.0/ExchangeNetworkDocument.xsd">
	<xsl:output omit-xml-declaration="yes" version="1.0" method="html"/>
	<xsl:template match="/">
		<xsl:apply-templates/>
	</xsl:template>
	<xsl:template name="WQXTransform" match="/">
		<xsl:variable name="InventorySelected">true</xsl:variable>

<html lang="en" xml:lang="en" >
	<head>
		<title>Water Quality Data (converted from XML to HTML)</title>
	</head>
	<body>
	<h1 style="font-family: Verdana">Water Quality Data</h1>
<table width="95%" cellspacing="1" cellpadding="3" border="0" style="font-family: Verdana;font-size:11px;border-top-width: 0px;border-bottom: solid 0px #666666;
	border-left: solid 0px #2b5dac;
	border-right: solid 0px #2b5dac;">
<tr style="background-color: #e0e0e0;font-weight:bold;font-size:12px"><th colspan="9">Projects</th></tr>
<tr style="background-color: #e0e0e0;font-weight:bold">
	<td>Project ID</td>
	<td>Project Name</td>
	<td>Project Description</td>
</tr>
<xsl:for-each select="hdr:Document/hdr:Payload/wqx:WQX/wqx:Organization/wqx:Project">	
<tr style="padding: 2px 2px 2px 2px;background-color: #eeeeee;">
	<td><xsl:value-of select="wqx:ProjectIdentifier"/></td>
	<td><xsl:value-of select="wqx:ProjectName"/></td>
	<td><xsl:value-of select="wqx:ProjectDescriptionText"/></td>
</tr>
</xsl:for-each>
</table>
<p></p>
<table width="95%" cellspacing="1" cellpadding="3" border="0" style="font-family: Verdana;font-size:11px;border-top-width: 0px;border-bottom: solid 0px #666666;
	border-left: solid 0px #2b5dac;
	border-right: solid 0px #2b5dac;">
<tr style="background-color: #e0e0e0;font-weight:bold;font-size:12px"><th colspan="9">Monitoring Locations</th></tr>
<tr style="background-color: #e0e0e0;font-weight:bold">
	<td>Mon. Loc. ID</td>
	<td>Mon. Loc. Name</td>
	<td>Description</td>
	<td>Latitude</td>
	<td>Longitude</td>
	<td>Horizontal Collection Method</td>
	<td>Horizontal Reference Datum</td>
	<td>State</td>
	<td>County</td>
</tr>
<xsl:for-each select="hdr:Document/hdr:Payload/wqx:WQX/wqx:Organization/wqx:MonitoringLocation">	
<tr style="padding: 2px 2px 2px 2px;background-color: #eeeeee;">
	<td><xsl:value-of select="wqx:MonitoringLocationIdentity/wqx:MonitoringLocationIdentifier"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationIdentity/wqx:MonitoringLocationName"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationIdentity/wqx:MonitoringLocationDescriptionText"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationGeospatial/wqx:LatitudeMeasure"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationGeospatial/wqx:LongitudeMeasure"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationGeospatial/wqx:HorizontalCollectionMethodName"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationGeospatial/wqx:HorizontalCoordinateReferenceSystemDatumName"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationGeospatial/wqx:StateCode"/></td>
	<td><xsl:value-of select="wqx:MonitoringLocationGeospatial/wqx:CountyCode"/></td>
</tr>
</xsl:for-each>
</table>
<p></p>
<table width="95%" cellspacing="1" cellpadding="3" border="0" style="font-family: Verdana;font-size:11px;border-top-width: 0px;border-bottom: solid 0px #666666;
	border-left: solid 0px #2b5dac;
	border-right: solid 0px #2b5dac;">
<tr style="background-color: #e0e0e0;font-weight:bold;font-size:12px"><th colspan="9">Activities and Results</th></tr>
<tr style="background-color: #e0e0e0;font-weight:bold">
	<td>Activity ID</td>
	<td>Activity Type</td>
	<td>Activity Media</td>
	<td>Activity Start Date</td>
	<td>Activity Start Time</td>
	<td>Project Identifier</td>
	<td>Monitoring Location Identifier</td>
	<td>Sample Collection Method ID</td>
	<td>Sample Collection Method Name</td>
</tr>
<xsl:for-each select="hdr:Document/hdr:Payload/wqx:WQX/wqx:Organization/wqx:Activity">	
<tr style="padding: 2px 2px 2px 2px;background-color: #eeeeee;">
	<td><xsl:value-of select="wqx:ActivityDescription/wqx:ActivityIdentifier"/></td>
	<td><xsl:value-of select="wqx:ActivityDescription/wqx:ActivityTypeCode"/></td>
	<td><xsl:value-of select="wqx:ActivityDescription/wqx:ActivityMediaName"/></td>
	<td><xsl:value-of select="wqx:ActivityDescription/wqx:ActivityStartDate"/></td>
	<td><xsl:value-of select="wqx:ActivityDescription/wqx:ActivityStartTime/wqx:Time"/></td>
	<td><xsl:value-of select="wqx:ActivityDescription/wqx:ProjectIdentifier"/></td>
	<td><xsl:value-of select="wqx:ActivityDescription/wqx:MonitoringLocationIdentifier"/></td>
	<td><xsl:value-of select="wqx:SampleDescription/wqx:SampleCollectionMethod/wqx:MethodIdentifier"/></td>
	<td><xsl:value-of select="wqx:SampleDescription/wqx:SampleCollectionMethod/wqx:MethodName"/></td>
</tr>
</xsl:for-each>
</table>

</body>
</html>		
	</xsl:template>
</xsl:stylesheet>
