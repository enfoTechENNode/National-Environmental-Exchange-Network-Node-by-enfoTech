<?xml version="1.0" encoding="ISO-8859-1"?>
<xsl:stylesheet version="1.0"  xmlns:TRI="http://www.exchangenetwork.net/schema/TRI/5" xmlns:sc="urn:us:net:exchangenetwork:sc:1:0"  xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
<xsl:output method="html"/>
<xsl:template match="/">

<html>
	<head>	
		<title>TRIME Form</title>
		<style type="text/css"> 							

			p {font-family: Arial}
			.fieldLabel {
				font-family: Arial, Helvetica, sans-serif;
				font-size: 12px;
				font-weight: bold;
        		padding-right: 5px;
			}
			 .response {
				font-family: Arial, Helvetica, sans-serif;
				font-size: 14px;
				color: #000000;
				font-weight: normal;
			}
			.partTitle {
				font-family: Arial, Helvetica, sans-serif;
				font-size: 16px;
				color: #333333;
				border-top: 1px none #000066;
				border-right: 1px none #000066;
				border-bottom: 1px dotted #000066;
				border-left: 1px none #000066;
				font-weight: bold;
        		padding-right: 5px;
			}            
			.sectionTitle {
				font-family: Arial, Helvetica, sans-serif;
				font-size: 14px;	
				color: #663333;
				font-weight: bold;
        		padding-right: 5px;
			}
			 .sectionNumber {
				font-family: Arial, Helvetica, sans-serif;
				font-size: 12px;
				font-weight: bold;
				color: #666666;
				border: none;
				font-style: italic;
			}
			.sourceReductionTitle {
				font-family: Arial, Helvetica, sans-serif;
				font-size: 12px;
				font-style: normal;
				font-weight: bold;
				color: #663333;
				border-top: 1px none;
				border-right: 1px none;
				border-bottom: 1px dashed;
				border-left: 1px none;
        		padding-right: 5px;
			}
		</style> 
	</head>
		
<body style="font-family: arial">
<xsl:for-each select="//TRI:Report">

	<div align="right">
    <span class="fieldLabel">TRI Facility ID Number: <xsl:value-of  select="//TRI:Facility/TRI:FacilityIdentifier"/></span><br />
    <span class="fieldLabel">Toxic Chemical, Category or Generic Name: 
      <xsl:choose>
        <xsl:when test="TRI:ChemicalIdentification/TRI:ChemicalNameText='NA'">
            <xsl:value-of  select="TRI:ChemicalIdentification/TRI:ChemicalMixtureNameText"/>
        </xsl:when>
        <xsl:otherwise>
            <xsl:value-of select="TRI:ChemicalIdentification/TRI:ChemicalNameText"/>
        </xsl:otherwise>
      </xsl:choose>
    </span>
  </div>  
  
  <xsl:choose>
    <xsl:when test="sc:RevisionIndicator = 'Y'"><br />
		<span class="fieldLabel" style="color:red">
			Revision<br />
			This form has been revised for the following reason(s):
      <xsl:for-each select="TRI:ChemicalReportRevisionCode">
        <xsl:variable name="revCode"><xsl:value-of select="."/></xsl:variable>
        <br /><xsl:value-of select="$revCode"/> -
        <xsl:if test="$revCode='RR1'"> New Monitoring Data</xsl:if>
        <xsl:if test="$revCode='RR2'"> New Emission Factor(s)</xsl:if>
        <xsl:if test="$revCode='RR3'"> New Chemical Concentration Data</xsl:if>
        <xsl:if test="$revCode='RR4'"> Recalculation(s)</xsl:if>
        <xsl:if test="$revCode='RR5'"> Other Reason(s)</xsl:if>
      </xsl:for-each>
		</span>
	</xsl:when>
  </xsl:choose>
  
  <xsl:choose>
  <xsl:when test="TRI:ChemicalReportWithdrawalCode">
      <span class="fieldLabel" style="color:red">
			<br /><br />Withdrawal<br />
			This form has been marked as withdrawn for the following reason(s):
      <xsl:for-each select="TRI:ChemicalReportWithdrawalCode">
        <xsl:variable name="wdCode"><xsl:value-of select="."/></xsl:variable>
        <br /><xsl:value-of select="$wdCode"/> -
        <xsl:if test="$wdCode='WT1'"> Did not meet the reporting threshold for manufacturing, processing, or otherwise use</xsl:if>
        <xsl:if test="$wdCode='WT2'"> Did not meet the reporting threshold for number of employees</xsl:if>
        <xsl:if test="$wdCode='WT3'"> Not in a covered NAICS code</xsl:if>
        <xsl:if test="$wdCode='WO1'"> Other Reason(s)</xsl:if>
      </xsl:for-each>
		</span>
  </xsl:when>
  </xsl:choose>
  

	<hr></hr>

	<xsl:if test="TRI:ReportType/TRI:ReportTypeCode = 'TRI_FORM_A'">	
		<strong>FORM A</strong>
		<p class="partTitle">Part I. FACILITY IDENTIFICATION INFORMATION </p>
			 <p> <span class="sectionTitle" >SECTION 1. REPORTING YEAR :</span> <xsl:value-of  select="TRI:SubmissionReportingYear"/></p>
			 <p class="sectionTitle">SECTION 2. TRADE SECRET INFORMATION</p>
		                
			 <span class="sectionNumber">2.1</span><span class="fieldLabel"><br />Are you claiming the toxic chemical identified on page 2 trade secret?: </span>
				 <xsl:choose>
					<xsl:when test="TRI:ChemicalTradeSecretIndicator = 'false'">No</xsl:when>
					<xsl:otherwise>Yes</xsl:otherwise>
				 </xsl:choose>          
		           			
				    <p class="sectionTitle">SECTION 3. CERTIFICATION</p>                 
				<p> <span class="fieldLabel">Name of owner/operator or senior management official: </span><xsl:value-of  select="TRI:CertifierName"/><br />  
				    <span class="fieldLabel">Title: </span><xsl:value-of  select="TRI:CertifierTitleText"/></p>				
				    <p class="sectionTitle">SECTION 4. FACILITY IDENTIFICATION </p>
					<span class="sectionNumber">4.1</span><br />
					<span class="fieldLabel">TRI Facility ID Number:</span><xsl:value-of  select="//TRI:Facility/TRI:FacilityIdentifier"/><br />
					<span class="fieldLabel">Facility or Establishment Name:</span><xsl:value-of  select="//TRI:Facility/sc:FacilitySiteName"/><br />
					<span class="fieldLabel">Address:</span> <xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:LocationAddressText"/><br />
					<span class="fieldLabel">City/State/Zip:</span> <xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:LocalityName"/>, <xsl:value-of select="//TRI:Facility/sc:LocationAddress/sc:StateIdentity/sc:StateName"/>,<xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:AddressPostalCode"/><br /> 
					<span class="fieldLabel">County:</span> <xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:CountyIdentity/sc:CountyName"/>			
					<p><span class="fieldLabel">Facility or Establishment Name or Mailing Address: </span><xsl:value-of select="//TRI:Facility/TRI:MailingFacilitySiteName"/><br />			
          <xsl:choose>
            <xsl:when test="//TRI:Facility/TRI:MailingFacilitySiteName != 'NA'">
                <span class="fieldLabel">Mailing Address: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:MailingAddressText"/><br />         
            
                <xsl:choose>
                  <xsl:when test="normalize-space(//TRI:Facility/TRI:MailingAddress/TRI:ProvinceNameText)" >
                    <span class="fieldLabel">City/Province/Postal Code: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:MailingAddressCityName"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/TRI:ProvinceNameText"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:AddressPostalCode"/><br />
                  </xsl:when>
                  <xsl:otherwise>
                    <span class="fieldLabel">City/State/Zip Code: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:MailingAddressCityName"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:StateIdentity/sc:StateName"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:AddressPostalCode"/><br />
                  </xsl:otherwise>
                </xsl:choose>
                
               <xsl:choose>
                <xsl:when test="normalize-space(//TRI:Facility/TRI:MailingAddress/sc:CountryIdentity)">
                    <span class="fieldLabel">Country: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:CountryIdentity/sc:CountryName"/>
                </xsl:when>
               </xsl:choose>
            </xsl:when>
          </xsl:choose>	
         </p>
				 <p> <span class="sectionNumber">4.2</span><span class="fieldLabel"><br />This report contains information for: </span><ul>		
						<xsl:choose>
							<xsl:when test="TRI:SubmissionPartialFacilityIndicator = 'true'"><li>Part of a facility</li></xsl:when>
						</xsl:choose>
						<xsl:choose>
							<xsl:when test="TRI:SubmissionPartialFacilityIndicator = 'false'"><li>An entire facility</li></xsl:when>
						</xsl:choose>
						<xsl:choose>			
							<xsl:when test="TRI:SubmissionFederalFacilityIndicator = 'Y'"><li>A Federal Facility</li></xsl:when>	
						</xsl:choose>	
						<xsl:choose>			
							<xsl:when test="TRI:SubmissionGOCOFacilityIndicator = 'true'"><li>GOCO</li></xsl:when>			
						 </xsl:choose> </ul></p>     
		
				 <p><span class="sectionNumber">4.3</span><span class="fieldLabel"><br />Technical Contact Name: </span> <xsl:value-of  select="TRI:TechnicalContactNameText"/><br />
					<span class="fieldLabel">Email Address: </span><xsl:value-of  select="TRI:TechnicalContactEmailAddressText"/><br />
					 <span class="fieldLabel">Telephone Number:</span> <xsl:value-of  select="TRI:TechnicalContactPhoneText"/></p>
        
        <xsl:choose><xsl:when test="//TRI:Report/TRI:SubmissionReportingYear >= '2007'">   
            <p><span class="sectionNumber">4.4</span><span class="fieldLabel"><br/>Public Contact Name:</span><xsl:value-of  select="TRI:PublicContactNameText"/><br/>
                <span class="fieldLabel">Email Address: </span><xsl:value-of  select="TRI:PublicContactEmailAddressText"/><br />        
                <span class="fieldLabel">Telephone Number:</span><xsl:value-of  select="TRI:PublicContactPhoneText"/>
            </p>
        </xsl:when></xsl:choose>
				      
		    			<xsl:for-each select="//TRI:Facility">     
                  <xsl:choose>
                  <xsl:when test="//TRI:Report/TRI:SubmissionReportingYear = '2005'">           
                    <p class="fieldLabel"><span class="sectionNumber">4.5</span><br />SIC Code (s) (4 digits):</p>
                    <xsl:for-each select="TRI:FacilitySIC">        
                      <xsl:choose>
                        <xsl:when test="sc:SICPrimaryIndicator = 'Primary'"><xsl:value-of  select="sc:SICCode"/> (Primary)<br /></xsl:when>
                        <xsl:otherwise><xsl:value-of  select="sc:SICCode"/><br /></xsl:otherwise>
                      </xsl:choose>
                    </xsl:for-each>
                   </xsl:when>
                   <xsl:otherwise>
                    <p class="fieldLabel"><span class="sectionNumber">4.5</span><br />NAICS Code (s) (6 digits):</p>
                    <xsl:for-each select="TRI:FacilityNAICS">        
                      <xsl:choose>
                        <xsl:when test="sc:NAICSPrimaryIndicator = 'Primary'"><xsl:value-of  select="sc:NAICSCode"/> (Primary)<br /></xsl:when>
                        <xsl:otherwise><xsl:value-of  select="sc:NAICSCode"/><br /></xsl:otherwise>
                      </xsl:choose>
                    </xsl:for-each>
                   </xsl:otherwise>                   
                  </xsl:choose>
		        	 		<p><span class="sectionNumber">4.7</span><br /><span class="fieldLabel"> Dun and Bradstreet Number:</span>
							 <xsl:choose>
								<xsl:when test="TRI:FacilityPrimaryDunBradstreetIndicator = 'true'">Primary: <xsl:value-of  select="TRI:FacilityDunBradstreetCode"/></xsl:when>
								<xsl:otherwise><xsl:value-of  select="TRI:FacilityDunBradstreetCode"/></xsl:otherwise>
							</xsl:choose></p>
				   
				   		<p><span class="sectionTitle">SECTION 5. PARENT COMPANY INFORMATION </span> </p>
				   		<span class="sectionNumber">5.1</span><br /><span class="fieldLabel"> Name of Parent Company:</span><xsl:value-of  select="TRI:ParentCompanyNameText"/>                   
			     			<p><span class="sectionNumber">5.2</span><br /><span class="fieldLabel"> Parent Company's Dun &amp; Bradstreet Number: </span><xsl:value-of  select="TRI:ParentDunBradstreetCode"/></p>
		    			</xsl:for-each>  
										
					<!-- First -->
						
					<p class="partTitle">PART II. TOXIC CHEMICAL RELEASE INVENTORY REPORTING FORM</p>					
					<p class="sectionTitle">SECTION 1. TOXIC CHEMICAL IDENTITY </p> 
					<xsl:for-each select="TRI:ChemicalIdentification">			
						<p> <span class="sectionNumber">1.1</span><br /><span class="fieldLabel"> CAS Number: </span><xsl:value-of  select="sc:CASNumber"/></p>			
						<p> <span class="sectionNumber">1.2</span><br />
						    <span class="fieldLabel">Toxic Chemical or Chemical Category Name: </span><xsl:value-of  select="TRI:ChemicalNameText"/></p>
						<p> <span class="sectionNumber">1.3</span><br />
				    		<span class="fieldLabel">Generic Chemical Name: </span>NA</p>
            <p> <span class="sectionNumber">1.4</span><br />
				    <span class="fieldLabel">Distribution of Each Member of the Dioxin and Dioxin-like Compounds Category:</span><br/>
                NA
            </p>
            <p class="sectionTitle"> SECTION 2. MIXTURE COMPONENT IDENTITY</p> 
            <p>  <span class="sectionNumber">2.1</span><br /><span class="fieldLabel">Generic Chemical Name Provided by Supplier:</span><xsl:value-of select="TRI:ChemicalMixtureNameText"/></p>
				
				  </xsl:for-each>
	</xsl:if>
	<xsl:if test="TRI:ReportType/TRI:ReportTypeCode = 'TRI_FORM_R'">	
    <xsl:variable name="formUnits">
      <xsl:choose>
        <xsl:when test="TRI:ChemicalIdentification/sc:CASNumber='N150'">grams/year</xsl:when>
        <xsl:otherwise>pounds/year</xsl:otherwise>
      </xsl:choose>
    </xsl:variable>
	
    <strong>FORM R</strong>
    
		 <p class="partTitle">Part I. FACILITY IDENTIFICATION INFORMATION </p>
		 <p> <span class="sectionTitle" >SECTION 1. REPORTING YEAR :</span> <xsl:value-of  select="TRI:SubmissionReportingYear"/></p>
		 <p class="sectionTitle">SECTION 2. TRADE SECRET INFORMATION</p>

		 <span class="sectionNumber">2.1</span><span class="fieldLabel"><br />Are you claiming the toxic chemical identified on page 2 trade secret?: </span>
		 <xsl:choose>
			<xsl:when test="TRI:ChemicalTradeSecretIndicator = 'false'">No</xsl:when>
			<xsl:otherwise>Yes</xsl:otherwise>
		 </xsl:choose>          
           			
		    <p class="sectionTitle">SECTION 3. CERTIFICATION</p>                 
		    <p> <span class="fieldLabel">Name of owner/operator or senior management official: </span><xsl:value-of  select="TRI:CertifierName"/><br />  
		    <span class="fieldLabel">Title: </span><xsl:value-of  select="TRI:CertifierTitleText"/></p>				
		    <p class="sectionTitle">SECTION 4. FACILITY IDENTIFICATION </p>
			<span class="sectionNumber">4.1</span><br />
			<span class="fieldLabel">TRI Facility ID Number:</span><xsl:value-of  select="//TRI:Facility/TRI:FacilityIdentifier"/><br />
			<span class="fieldLabel">Facility or Establishment Name:</span><xsl:value-of  select="//TRI:Facility/sc:FacilitySiteName"/><br />
			<span class="fieldLabel">Address:</span> <xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:LocationAddressText"/><br />
			<span class="fieldLabel">City/State/Zip:</span> <xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:LocalityName"/>, <xsl:value-of select="//TRI:Facility/sc:LocationAddress/sc:StateIdentity/sc:StateName"/>,   <xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:AddressPostalCode"/><br /> 
			<span class="fieldLabel">County:</span> <xsl:value-of  select="//TRI:Facility/sc:LocationAddress/sc:CountyIdentity/sc:CountyName"/>			
			<p><span class="fieldLabel">Facility or Establishment Name or Mailing Address: </span><xsl:value-of select="//TRI:Facility/TRI:MailingFacilitySiteName"/><br />			
      <xsl:choose>
            <xsl:when test="//TRI:Facility/TRI:MailingFacilitySiteName != 'NA'">
                <span class="fieldLabel">Mailing Address: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:MailingAddressText"/><br />         
            
                <xsl:choose>
                  <xsl:when test="normalize-space(//TRI:Facility/TRI:MailingAddress/TRI:ProvinceNameText)" >
                    <span class="fieldLabel">City/Province/Postal Code: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:MailingAddressCityName"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/TRI:ProvinceNameText"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:AddressPostalCode"/><br />
                  </xsl:when>
                  <xsl:otherwise>
                    <span class="fieldLabel">City/State/Zip Code: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:MailingAddressCityName"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:StateIdentity/sc:StateName"/>, <xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:AddressPostalCode"/><br />
                  </xsl:otherwise>
                </xsl:choose>
                
               <xsl:choose>
                <xsl:when test="normalize-space(//TRI:Facility/TRI:MailingAddress/sc:CountryIdentity)">
                    <span class="fieldLabel">Country: </span><xsl:value-of  select="//TRI:Facility/TRI:MailingAddress/sc:CountryIdentity/sc:CountryName"/>
                </xsl:when>
               </xsl:choose>
            </xsl:when>
       </xsl:choose>	
       </p>
		 <p> <span class="sectionNumber">4.2</span><span class="fieldLabel"><br />This report contains information for: </span><ul>		
				<xsl:choose>
					<xsl:when test="TRI:SubmissionPartialFacilityIndicator = 'true'"><li>Part of a facility</li></xsl:when>
				</xsl:choose>
				<xsl:choose>
					<xsl:when test="TRI:SubmissionPartialFacilityIndicator = 'false'"><li>An entire facility</li></xsl:when>
				</xsl:choose>
				<xsl:choose>			
					<xsl:when test="TRI:SubmissionFederalFacilityIndicator = 'Y'"><li>A Federal Facility</li></xsl:when>	
				</xsl:choose>	
				<xsl:choose>			
					<xsl:when test="TRI:SubmissionGOCOFacilityIndicator = 'true'"><li>GOCO</li></xsl:when>			
				 </xsl:choose> </ul></p>     

		 <p><span class="sectionNumber">4.3</span><span class="fieldLabel"><br />Technical Contact Name: </span> <xsl:value-of  select="TRI:TechnicalContactNameText"/><br />
			<span class="fieldLabel">Email Address: </span><xsl:value-of  select="TRI:TechnicalContactEmailAddressText"/><br />
			<span class="fieldLabel">Telephone Number:</span> <xsl:value-of  select="TRI:TechnicalContactPhoneText"/>
     </p>

			<p><span class="sectionNumber">4.4</span><span class="fieldLabel"><br/>Public Contact Name:</span><xsl:value-of  select="TRI:PublicContactNameText"/><br/>
      <xsl:choose><xsl:when test="//TRI:Report/TRI:SubmissionReportingYear >= '2007'">
          <span class="fieldLabel">Email Address: </span><xsl:value-of  select="TRI:PublicContactEmailAddressText"/><br />
      </xsl:when></xsl:choose>
			   <span class="fieldLabel">Telephone Number:</span><xsl:value-of  select="TRI:PublicContactPhoneText"/>
      </p>
      
    			<xsl:for-each select="//TRI:Facility">        
          			<xsl:choose>
                  <xsl:when test="//TRI:Report/TRI:SubmissionReportingYear = '2005'">           
                    <p class="fieldLabel"><span class="sectionNumber">4.5</span><br />SIC Code (s) (4 digits):</p>
                    <xsl:for-each select="TRI:FacilitySIC">        
                      <xsl:choose>
                        <xsl:when test="sc:SICPrimaryIndicator = 'Primary'"><xsl:value-of  select="sc:SICCode"/> (Primary)<br /></xsl:when>
                        <xsl:otherwise><xsl:value-of  select="sc:SICCode"/><br /></xsl:otherwise>
                      </xsl:choose>
                    </xsl:for-each>
                   </xsl:when>
                   <xsl:otherwise>
                    <p class="fieldLabel"><span class="sectionNumber">4.5</span><br />NAICS Code (s) (6 digits):</p>
                    <xsl:for-each select="TRI:FacilityNAICS">        
                      <xsl:choose>
                        <xsl:when test="sc:NAICSPrimaryIndicator = 'Primary'"><xsl:value-of  select="sc:NAICSCode"/> (Primary)<br /></xsl:when>
                        <xsl:otherwise><xsl:value-of  select="sc:NAICSCode"/><br /></xsl:otherwise>
                      </xsl:choose>
                    </xsl:for-each>
                   </xsl:otherwise>                   
                  </xsl:choose>
       
        	 		<p><span class="sectionNumber">4.7</span><br /><span class="fieldLabel"> Dun and Bradstreet Number:</span>
					 <xsl:choose>
						<xsl:when test="TRI:FacilityPrimaryDunBradstreetIndicator = 'true'">Primary: <xsl:value-of  select="TRI:FacilityDunBradstreetCode"/></xsl:when>
						<xsl:otherwise>
							<xsl:for-each select="TRI:FacilityDunBradstreetCode">
								<xsl:value-of  select="."/>,
							</xsl:for-each>
						</xsl:otherwise>
					</xsl:choose></p>
		   
		   		<p><span class="sectionTitle">SECTION 5. PARENT COMPANY INFORMATION </span> </p>
		   		<span class="sectionNumber">5.1</span><br /><span class="fieldLabel"> Name of Parent Company:</span><xsl:value-of  select="TRI:ParentCompanyNameText"/>                   
	     			<p><span class="sectionNumber">5.2</span><br /><span class="fieldLabel"> Parent Company's Dun &amp; Bradstreet Number: </span><xsl:value-of  select="TRI:ParentDunBradstreetCode"/></p>
    			</xsl:for-each>  
								
			<!-- First -->
				
			<p class="partTitle">PART II. TOXIC CHEMICAL RELEASE INVENTORY REPORTING FORM</p>					
			<p class="sectionTitle">SECTION 1. TOXIC CHEMICAL IDENTITY </p> 
			<xsl:for-each select="TRI:ChemicalIdentification">			
				<p> <span class="sectionNumber">1.1</span><br /><span class="fieldLabel"> CAS Number: </span><xsl:value-of  select="sc:CASNumber"/></p>			
				<p> <span class="sectionNumber">1.2</span><br />
				    <span class="fieldLabel">Toxic Chemical or Chemical Category Name: </span><xsl:value-of  select="TRI:ChemicalNameText"/></p>
				<p> <span class="sectionNumber">1.3</span><br />
				    <span class="fieldLabel">Generic Chemical Name: </span>NA</p>
				<p> <span class="sectionNumber">1.4</span><br />
				    <span class="fieldLabel">Distribution of Each Member of the Dioxin and Dioxin-like Compounds Category:</span><br/>
          <xsl:choose>
            <xsl:when test="TRI:DioxinDistributionNAIndicator= 'false'">            			
              <span class="fieldLabel">1</span><xsl:value-of  select="TRI:DioxinDistribution1Percent"/><br/>
              <span class="fieldLabel">2</span><xsl:value-of  select="TRI:DioxinDistribution2Percent"/> <br/>
              <span class="fieldLabel">3</span><xsl:value-of  select="TRI:DioxinDistribution3Percent"/><br/>
              <span class="fieldLabel">4</span><xsl:value-of  select="TRI:DioxinDistribution4Percent"/><br/>
              <span class="fieldLabel">5</span><xsl:value-of  select="TRI:DioxinDistribution5Percent"/><br/>
              <span class="fieldLabel">6</span><xsl:value-of  select="TRI:DioxinDistribution6Percent"/><br/>
              <span class="fieldLabel">7</span><xsl:value-of  select="TRI:DioxinDistribution7Percent"/><br/>
              <span class="fieldLabel">8</span><xsl:value-of  select="TRI:DioxinDistribution8Percent"/><br/>
              <span class="fieldLabel">9</span><xsl:value-of  select="TRI:DioxinDistribution9Percent"/><br/>
              <span class="fieldLabel">10</span><xsl:value-of  select="TRI:DioxinDistribution10Percent"/><br/>
              <span class="fieldLabel">11</span><xsl:value-of  select="TRI:DioxinDistribution11Percent"/><br/>
              <span class="fieldLabel">12</span><xsl:value-of  select="TRI:DioxinDistribution12Percent"/><br/>
              <span class="fieldLabel">13</span><xsl:value-of  select="TRI:DioxinDistribution13Percent"/><br/>
              <span class="fieldLabel">14</span><xsl:value-of  select="TRI:DioxinDistribution14Percent"/><br/>
              <span class="fieldLabel">15</span><xsl:value-of  select="TRI:DioxinDistribution15Percent"/><br/>
              <span class="fieldLabel">16</span><xsl:value-of  select="TRI:DioxinDistribution16Percent"/><br/>
              <span class="fieldLabel">17</span><xsl:value-of  select="TRI:DioxinDistribution17Percent"/>
            </xsl:when>
            <xsl:otherwise>NA</xsl:otherwise>	
          </xsl:choose>	
				</p>
   			</xsl:for-each>
   			
			<!-- second -->
			
			<p class="sectionTitle"> SECTION 2. MIXTURE COMPONENT IDENTITY</p> 
			<p>  <span class="sectionNumber">2.1</span><br /><span class="fieldLabel">Generic Chemical Name Provided by Supplier:</span><xsl:value-of select="TRI:ChemicalIdentification/TRI:ChemicalMixtureNameText"/></p>
				
			<!-- Third -->
				
			<xsl:for-each select="TRI:ChemicalActivitiesAndUses">						
				<p> <span class="sectionTitle">SECTION 3. ACTIVITIES AND USES OF THE TOXIC CHEMICAL AT THE FACILITY </span></p>
				    <span class="sectionNumber">3.1 </span><br />
            <span class="fieldLabel">Manufacture the toxic chemical:</span>
				<ul>			
					<xsl:choose>
						<xsl:when test="TRI:ChemicalProducedIndicator = 'true'"><li>Produce</li></xsl:when>		
					</xsl:choose>
					<xsl:choose>					
						<xsl:when test="TRI:ChemicalImportedIndicator = 'true'"><li>Import</li></xsl:when>														
					</xsl:choose>		
          <xsl:choose>
          <xsl:when test="(TRI:ChemicalProducedIndicator = 'true') or (TRI:ChemicalImportedIndicator = 'true')">
          <br />
					<span class="fieldLabel">If produce or import: </span>
          <ul>				
					<xsl:choose>
						<xsl:when test="TRI:ChemicalUsedProcessedIndicator = 'true'"><li>For on-site use/processing</li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalSalesDistributionIndicator = 'true'"><li>For sale/distribution </li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalByproductIndicator = 'true'"><li>As a byproduct</li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalManufactureImpurityIndicator = 'true'"><li>As an impurity</li></xsl:when>	
					</xsl:choose>		
          </ul>
          </xsl:when>
        </xsl:choose>
        </ul>   
					<span class="sectionNumber">3.2 </span><br />
          <span class="fieldLabel">Process the toxic chemical: </span>
				<ul>							
					<xsl:choose>
						<xsl:when test="TRI:ChemicalReactantIndicator = 'true'"><li>As a reactant</li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalFormulationComponentIndicator= 'true'"><li>As a formulation component</li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalArticleComponentIndicator= 'true'"><li>As an article component</li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalRepackagingIndicator= 'true'"><li>Repackaging</li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalProcessImpurityIndicator= 'true'"><li>As an impurity</li></xsl:when>	
					</xsl:choose>
        </ul>
					<span class="sectionNumber">3.3 </span><br />
          <span class="fieldLabel">Otherwise use the toxic chemical: </span>
				<ul>										
					<xsl:choose>
						<xsl:when test="TRI:ChemicalProcessingAidIndicator = 'true'"><li>As a chemical processing aid </li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalManufactureAidIndicator = 'true'"><li>As a manufacturing aid</li></xsl:when>	
					</xsl:choose>
					<xsl:choose>
						<xsl:when test="TRI:ChemicalAncillaryUsageIndicator= 'true'"><li> Ancillary or other use </li></xsl:when>	
					</xsl:choose>
				</ul>										
			</xsl:for-each>
			
			<!-- Fourth -->	
			<p class="sectionTitle">SECTION 4. MAXIMUM AMOUNT OF THE TOXIC CHEMICAL ONSITE AT ANY TIME DURING THE CALENDAR YEAR </p> 
			 <span class="sectionNumber">4.1</span><br /><xsl:value-of select="TRI:MaximumChemicalAmountCode"/>  
				
			<!-- Fifth -->
			<p class="sectionTitle">SECTION 5.QUANTITY OF THE TOXIC CHEMICAL ENTERING EACH ENVIRONMENTAL MEDIUM ONSITE</p><br /> 
			<xsl:for-each select="TRI:OnsiteReleaseQuantity"> 	
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'AIR FUG'">
						<span class="fieldLabel">5.1 Fugitive or non-point air emissions</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
						 		NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release (<xsl:value-of select="$formUnits"/>):</span>
									<xsl:choose>
										<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
										<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
										</xsl:otherwise>
									</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />			
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity"> 	
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'AIR STACK'">
						<span class="fieldLabel">5.2 Stack or point air emissions</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span>
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <span class="fieldLabel">5.3 Discharges to receiving streams or water bodies</span><br />
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>						
					<xsl:when test="TRI:EnvironmentalMediumCode = 'WATER'">								
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Stream or Water Body Name</span>: <xsl:value-of select="TRI:WaterStream/TRI:StreamName" /><br />
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span>
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate: </span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><br />
								<span class="fieldLabel">% from Stormwater: </span>
								<xsl:choose>
									<xsl:when test="TRI:WaterStream/TRI:ReleaseStormWaterNAIndicator = 'true'">NA</xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:WaterStream/TRI:ReleaseStormWaterPercent" />
									</xsl:otherwise>
								</xsl:choose>		
								<br /><br />
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <hr />
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'UNINJ I'">
						<span class="fieldLabel">5.4.1 Underground Injection onsite to Class I Wells</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span> 
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'UNINJ IIV'">
						<span class="fieldLabel">5.4.2 Underground Injection onsite to Class II-V Wells</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span> 
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'RCRA C'">
						<span class="fieldLabel">5.5.1A RCRA Subtitle C landfills</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span> 
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'OTH LANDF'">
						<span class="fieldLabel">5.5.1B Other Landfills</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span> 
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'LAND TREA'">
						<span class="fieldLabel">5.5.2 Land treatment/application farming</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span>
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'SI 5.5.3A'">
						<span class="fieldLabel">5.5.3A RCRA Subtitle C surface impoundments</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span>
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'SI 5.5.3B'">
						<span class="fieldLabel">5.5.3B Other surface impoundments</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span> 
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" /><hr />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
      </xsl:for-each>
      <xsl:for-each select="TRI:OnsiteReleaseQuantity">
				<xsl:choose>
					<xsl:when test="TRI:EnvironmentalMediumCode = 'OTH DISP'">
						<span class="fieldLabel">5.5.4 Other disposal</span><br />
						<xsl:choose>
							<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
								NA<br /><hr/>
							</xsl:when>
							<xsl:otherwise>
								<span class="fieldLabel">Total Release: (<xsl:value-of select="$formUnits"/>)</span>
								<xsl:choose>
									<xsl:when test="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
								<span class="fieldLabel">Basis of Estimate:</span><xsl:value-of select="TRI:OnsiteWasteQuantity/TRI:QuantityBasisEstimationCode" />						
							</xsl:otherwise>
						</xsl:choose>
					</xsl:when>
				</xsl:choose>
			</xsl:for-each>
			<br /><hr />		
			<!-- sec 6 -->
			
			<p class="sectionTitle">SECTION 6. TRANSFERS OF THE TOXIC CHEMICAL IN WASTES TO OFF-SITE LOCATIONS </p> 								
			<p> <span class="sectionNumber">6.1</span><br />
			    <span class="fieldLabel">DISCHARGES TO PUBLICLY OWNED TREATMENT WORKS (POTWs) </span></p> 			
			<p> <span class="sectionNumber">6.1.A</span><br />
			    <span class="fieldLabel">Total Quantity Transferred to POTWs and Basis of Estimate </span></p>
			    <xsl:choose>
					<xsl:when test="TRI:POTWWasteQuantity/TRI:WasteQuantityNAIndicator = 'true'">
						NA<br /><hr/>
					</xsl:when>
					<xsl:otherwise>
						<p> <span class="sectionNumber">6.1.A.1</span><br />
						    <span class="fieldLabel">Total Transfers (<xsl:value-of select="$formUnits"/>): </span>
						    <xsl:choose>
									<xsl:when test="TRI:POTWWasteQuantity/TRI:WasteQuantityMeasure >= '0'"><xsl:value-of select="TRI:POTWWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
									<xsl:otherwise>
										<xsl:value-of select="TRI:POTWWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
									</xsl:otherwise>
								</xsl:choose>
						</p> 			
						<p> <span class="sectionNumber">6.1.A.2</span><br />
						    <span class="fieldLabel">Basis of Estimate:</span><xsl:value-of  select="TRI:POTWWasteQuantity/TRI:QuantityBasisEstimationCode"/></p> 
					</xsl:otherwise>
			    </xsl:choose>
							
			<xsl:for-each select="TRI:TransferLocation">					
				<xsl:if test="TRI:POTWIndicator = 'true'">				
					 <span class="fieldLabel">POTW Name:</span> <xsl:value-of  select="sc:FacilitySiteName"/>													
						<br/>   
						<span class="fieldLabel">POTW Address:</span> <xsl:value-of  select="sc:LocationAddress/sc:LocationAddressText"/>
						<br/> 
						<span class="fieldLabel">City: </span><xsl:value-of  select="sc:LocationAddress/sc:LocalityName"/>
						<br/>  
						<span class="fieldLabel">State: </span><xsl:value-of  select="sc:LocationAddress/sc:StateIdentity/sc:StateName"/>
						<br/>  
						<span class="fieldLabel">County: </span> <xsl:value-of  select="sc:LocationAddress/sc:CountyIdentity/sc:CountyName"/> 
						<br/>  
					  <span class="fieldLabel">Zip: </span> <xsl:value-of  select="sc:LocationAddress/sc:AddressPostalCode"/><hr />
				</xsl:if>
			</xsl:for-each>
		
			<p class="sectionTitle">SECTION 6.2 TRANSFERS TO OTHER OFF-SITE LOCATIONS </p> 					
			<span class="sectionNumber">6.2.1</span><br />

			<xsl:for-each select="TRI:TransferLocation">				
				<xsl:if test="TRI:POTWIndicator = 'false'">					

				    <span class="fieldLabel">Off-Site EPA Identification Number (RCRA ID No.):</span> <xsl:value-of  select="TRI:RCRAIdentificationNumber"/><br/>
				    <span class="fieldLabel">Off-Site Location Name: </span><xsl:value-of  select="sc:FacilitySiteName"/><br/> 
				    <span class="fieldLabel">Off-Site Address: </span><xsl:value-of  select="sc:LocationAddress/sc:LocationAddressText"/><br/> 
				    <span class="fieldLabel">City:</span>  <xsl:value-of  select="sc:LocationAddress/sc:LocalityName"/><br/>  
				    <span class="fieldLabel">State:</span> <xsl:value-of  select="sc:LocationAddress/sc:StateIdentity/sc:StateName"/><br/>  
				    <span class="fieldLabel">County:</span> <xsl:value-of  select="sc:LocationAddress/sc:CountyIdentity/sc:CountyName"/><br/>  
				    <span class="fieldLabel">Zip: </span><xsl:value-of  select="sc:LocationAddress/sc:AddressPostalCode"/><br/> 
				    <span class="fieldLabel">Country (Non-US):</span> <xsl:value-of  select="sc:LocationAddress/sc:CountryIdentity/sc:CountryName"/><br/>
				    <span class="fieldLabel">Is location under control of reporting facility or parent company?</span>
        <xsl:choose>
            <xsl:when test="sc:FacilitySiteName = 'NA'">
                
            </xsl:when>
            <xsl:otherwise>
                <xsl:choose>
                    <xsl:when test="TRI:ControlledLocationIndicator = 'false'">No</xsl:when>
                    <xsl:otherwise>Yes</xsl:otherwise>
                </xsl:choose>
            </xsl:otherwise>
        </xsl:choose>
				</xsl:if>
				<xsl:for-each select="TRI:TransferQuantity"><p>
					<span class="fieldLabel">Total Transfers (<xsl:value-of select="$formUnits"/>): </span>
					<xsl:choose>
						<xsl:when test="TRI:TransferWasteQuantity/TRI:WasteQuantityMeasure[.!='']"><xsl:value-of select="TRI:TransferWasteQuantity/TRI:WasteQuantityMeasure" /><br /></xsl:when>
						<xsl:otherwise>
							<xsl:value-of select="TRI:TransferWasteQuantity/TRI:WasteQuantityRangeCode" /><br />
						</xsl:otherwise>
					</xsl:choose>
					<span class="fieldLabel">Basis of Estimate:</span> <xsl:value-of  select="TRI:TransferWasteQuantity/TRI:QuantityBasisEstimationCode"/> <br />
	        			<span class="fieldLabel">Type of Waste Treatment/Disposal/Recycling/Energy Recovery:</span><xsl:value-of  select="TRI:WasteManagementTypeCode"/></p>
	        		</xsl:for-each>	
			</xsl:for-each>					
											
			<hr/>
			<p class="sectionTitle" span="sectionTitle"> SECTION 7A. ONSITE WASTE TREATMENT METHODS AND EFFICIENCY </p>

			<!--start table -->
			<xsl:choose>
				<xsl:when test="TRI:WasteTreatmentNAIndicator = 'true'">No on-site waste treatment is applied to any waste stream containing the toxic chemical or chemical category.
				</xsl:when>
				<xsl:otherwise>
					<xsl:for-each select="TRI:WasteTreatmentDetails">					
						<span class="fieldLabel">General Waste Stream:</span> <xsl:value-of select="TRI:WasteStreamTypeCode"/>
						<br/>
						<xsl:for-each select="TRI:WasteTreatmentMethod">	
							<br/><span class="fieldLabel">Waste Treatment Method(s) Sequence #<xsl:value-of select="TRI:WasteTreatmentSequenceNumber"/>:</span> <xsl:value-of select="TRI:WasteTreatmentMethodCode"/> 
						</xsl:for-each>
					<br/><span class="fieldLabel">Waste Treatment Efficiency Estimate:</span> <xsl:value-of select="TRI:TreatmentEfficiencyRangeCode"/>
					<hr/>

					</xsl:for-each>		
				</xsl:otherwise>
			</xsl:choose>						

		<p class="sectionTitle">SECTION 7B. ON-SITE ENERGY RECOVERY PROCESSES </p> 
		<xsl:for-each select="TRI:OnsiteRecoveryProcess">								
			<xsl:choose>
				<xsl:when test="TRI:EnergyRecoveryNAIndicator = 'true'">No on-site energy recovery is applied to any waste stream containing the toxic chemical or chemical category.</xsl:when>
				<xsl:otherwise>
					<span class="fieldLabel">Energy Recovery Methods:</span>
					<xsl:for-each select="TRI:EnergyRecoveryMethodCode">								
				 		<xsl:value-of  select="."/>,
					</xsl:for-each>
				</xsl:otherwise>
			</xsl:choose>
		</xsl:for-each>													
										
		<p class="sectionTitle">SECTION 7C. ON-SITE RECYCLING PROCESSES </p> 
		<xsl:for-each select="TRI:OnsiteRecyclingProcess">				
			<xsl:choose>
				<xsl:when test="TRI:OnsiteRecyclingNAIndicator = 'true'">No on-site recycling is applied to any waste stream containing the toxic chemical or chemical category.</xsl:when>
				<xsl:otherwise>
					<span class="fieldLabel">Recycling Methods:</span>
					<xsl:for-each select="TRI:OnsiteRecyclingMethodCode">								
						<xsl:value-of  select="."/>, 
					</xsl:for-each>
				</xsl:otherwise>
			</xsl:choose>					
		</xsl:for-each>																	
				<!-- section 8 -->
	<p class="sectionTitle">SECTION 8. SOURCE REDUCTION AND RECYCLING ACTIVITIES</p>
														
	<xsl:for-each select="TRI:SourceReductionQuantity">					
		<p class="sectionNumber">  8.1a  </p>
		<span class="sourceReductionTitle">Total on-site disposal to Class I Underground Injection Wells, RCRA 
		Subtitle C landfills, and other landfills</span><br />
		<xsl:for-each select="TRI:OnsiteUICDisposalQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>): </span>NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/>
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>):</span> NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>				
					
	<p class="sectionNumber">  8.1b  </p>
	<span class="sourceReductionTitle">Total other on-site disposal or other releases</span><br />
	<xsl:for-each select="TRI:OnsiteOtherDisposalQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>)</span>:<xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>):</span> NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>				
					
	<p class="sectionNumber">  8.1c  </p>
	 <span class="sourceReductionTitle">Total off-site disposal to Class I Underground Injection Wells, RCRA Subtitle C landfills, and other landfills</span><br />
	<xsl:for-each select="TRI:OffsiteUICDisposalQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>):</span> NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>	
					
					
	<p class="sectionNumber">  8.1d  </p>
	<span class="sourceReductionTitle">Total other off-site disposal or other releases</span><br />
	<xsl:for-each select="TRI:OffsiteOtherDisposalQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>): </span>NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">SecondFollowing Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>	
										
	<p class="sectionNumber">  8.2  </p>
	<span class="sourceReductionTitle">Quantity used for energy recovery onsite</span><br />
	<xsl:for-each select="TRI:OnsiteEnergyRecoveryQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>): </span>NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>						
					
	<p class="sectionNumber">    8.3  </p>
	<span class="sourceReductionTitle">Quantity used for energy recovery offsite</span><br />
	<xsl:for-each select="TRI:OffsiteEnergyRecoveryQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>): </span>NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>	
						
					
					
	<p class="sectionNumber">    8.4  </p>
	<span class="sourceReductionTitle">Quantity recycled onsite</span><br />
	<xsl:for-each select="TRI:OnsiteRecycledQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>): </span>NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>	
																
	<p class="sectionNumber">   8.5  </p>
	<span class="sourceReductionTitle">Quantity recycled offsite</span><br />
	<xsl:for-each select="TRI:OffsiteRecycledQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>): </span>NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>	
										
	<p class="sectionNumber">    8.6  </p>
	<span class="sourceReductionTitle">Quantity treated onsite</span><br />
	<xsl:for-each select="TRI:OnsiteTreatedQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>):</span> NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>): </span>NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>	
															
	<p class="sectionNumber">    8.7  </p>
	<span class="sourceReductionTitle">Quantity treated offsite:</span><br />
	<xsl:for-each select="TRI:OffsiteTreatedQuantity">
		<xsl:choose>
			<xsl:when test="TRI:YearOffsetMeasure = '-1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br />
						<span class="fieldLabel">Prior Year (<xsl:value-of select="$formUnits"/>):</span><xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '0'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> NA
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Current Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '1'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Following Year (<xsl:value-of select="$formUnits"/>):</span> NA
				</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>
			<xsl:when test="TRI:YearOffsetMeasure = '2'">	
				<xsl:choose>		
					<xsl:when test="TRI:TotalQuantityNAIndicator = 'true'">
						<br/> 
						<span class="fieldLabel">Second Following Year (<xsl:value-of select="$formUnits"/>):</span> NA 
					</xsl:when>
					<xsl:otherwise>
						<br/> 
						<span class="fieldLabel">Second Following Reporting Year (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of  select="TRI:TotalQuantity"/> 
					</xsl:otherwise>
				</xsl:choose>
			</xsl:when>		
		</xsl:choose>
	</xsl:for-each>	
						
					
		<p class="sectionNumber"> 8.8 </p>
     <xsl:choose>
       <xsl:when test="TRI:OneTimeReleaseNAIndicator = 'true'">
          NA<br />
       </xsl:when>
       <xsl:otherwise>
          <span class="fieldLabel">Quantity released to the environment as a result of remedial actions,
					catastrophic events, or one-time events not associated with production
					processes (<xsl:value-of select="$formUnits"/>):</span> <xsl:value-of select="TRI:OneTimeReleaseQuantity"/> <br/>
       </xsl:otherwise>
     </xsl:choose>		 

		<p class="sectionNumber"> 8.9 </p>
			<xsl:choose>
				<xsl:when test="TRI:ProductionRatioNAIndicator = 'true'">
					NA<br />
				</xsl:when>
				<xsl:otherwise>
		 			<span class="fieldLabel">Production ratio or activity index:</span><xsl:value-of select="TRI:ProductionRatioMeasure"/> <br/>
		 		</xsl:otherwise>
		 	</xsl:choose>

	</xsl:for-each>	

		<xsl:choose>
			<xsl:when test="TRI:SourceReductionNAIndicator = 'true'">
				<p><span class="sectionNumber">8.10</span><br/></p>
				<span class="fieldLabel">Source Reduction Activity: NA</span> 
			</xsl:when>
			<xsl:otherwise>
				<p><span class="sectionNumber">8.10.1</span><br /></p>
				<xsl:for-each select="TRI:SourceReductionActivity">	
					<span class="fieldLabel">Source Reduction Activity Code:</span> <xsl:value-of  select="TRI:SourceReductionActivityCode"/><br />
					<span class="fieldLabel">Methods to Identify Activities:</span><xsl:for-each select="TRI:SourceReductionMethodCode">
					<xsl:value-of  select="."/>, 	 
					</xsl:for-each>
				<hr />
				</xsl:for-each>	
			</xsl:otherwise>
		</xsl:choose>

		<p class="sectionNumber">  8.11 </p>
		<span class="fieldLabel">Wish to submit additional optional information on source reduction, recycling, or pollution control activities?:</span> 
		<xsl:choose>
			<xsl:when test="TRI:SubmissionAdditionalDataIndicator = 'true'">Yes</xsl:when>
		</xsl:choose>						

	<br/>

		<p><span class="fieldLabel">Additional optional information on source reduction, recycling, or pollution control activities:</span><br />

		<xsl:value-of  select="TRI:OptionalInformationText"/></p>
	</xsl:if>
</xsl:for-each>				
</body>
					
</html>



<!-- end sixth page -->

</xsl:template>
</xsl:stylesheet>
