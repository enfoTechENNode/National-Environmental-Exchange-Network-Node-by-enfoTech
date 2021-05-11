DEDL Dataflow overview: 
--------------------------
 Creates a solicit service that when it is run will grab DEDL information from the Node,
 (add dynamic SQL domain values if applicable), and then submit the DEDL file to EPA's ENDS registry.



Installation instructions: 
--------------------------

 PERFORMED BY ORACLE DBA:

 1. Run the Oracle database script ORA_SP_GET_DEDL_XML_ALL_ENNODE.sql on the Node database.


 PERFORMED BY NODE ADMIN:

 2. Update the GenerateAndSubmitDEDL_DataWizardConfig.xml with the correct local settings: 
          - NodeURI
          - authUser
          - authPassword
 3. Log into the Node Administration console. Under Node Domain's, navigate to "NODE" domain, 
    Click "Go to Operations". Create a new Operation called "GenerateAndSubmitDEDL"
          - Operation Type = 'WEB_SERVICE'
          - Using Data Flow Wizard = 'yes'
          - Web Service = 'SOLICIT' 
          - Include in Publishing = 'No'
    Then click Save.

 4. Back at main Administration dashboard, click "Data Wizard Configuration". Select 
    GenerateAndSubmitDEDL from drop-down and browser for GenerateAndSubmitDEDL_DataWizardConfig.xml then click
    Upload.



How and When to Execute this Plug-In:
-------------------------------------
 Run this any time you want to sent a new set of DEDL information to EPA. 
 
 1. Run the Node Client application. 
 2. Authenticate to your own node
 3. Click on Solicit, select GererateAndSubmitDEDL, then click "Solicit".
 4. You can monitor the dataflow in Node Administration Utility. If it runs successfully, new DEDL file is submitted to EPA.
    A copy of file is saved in Node File Cabin.