<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(Object sender, EventArgs e) {
        // Code that runs on application startup

        Microsoft.Web.Services2.SoapInputFilterCollection input =
            Microsoft.Web.Services2.Configuration.WebServicesConfiguration.FilterConfiguration.InputFilters;
        input.Remove(typeof(Microsoft.Web.Services2.Policy.PolicyVerificationInputFilter));
        input.Remove(typeof(Microsoft.Web.Services2.Referral.ReferralInputFilter));
        input.Remove(typeof(Microsoft.Web.Services2.Security.SecurityInputFilter));
        Microsoft.Web.Services2.SoapOutputFilterCollection output =
            Microsoft.Web.Services2.Configuration.WebServicesConfiguration.FilterConfiguration.OutputFilters;
        output.Remove(typeof(Microsoft.Web.Services2.Policy.PolicyEnforcementOutputFilter));
        output.Remove(typeof(Microsoft.Web.Services2.Referral.ReferralOutputFilter));
        output.Remove(typeof(Microsoft.Web.Services2.Security.SecurityOutputFilter));
        
        System.Net.ServicePointManager.Expect100Continue = false;
        
        try
        {
            Node.Core.Biz.Objects.SystemConfiguration config = new Node.Core.Biz.Objects.SystemConfiguration();
            Node.Core.Phrase.LoggerPath = System.Configuration.ConfigurationManager.AppSettings[Node.Core.Phrase.LOG_PATH_KEY];
            Node.Core.Phrase.LoggerLevel = config.GetLoggingLevel("WebServices");
        }
        catch (Exception ex)
        {
            Node.Core.Logging.Logger logger = new Node.Core.Logging.Logger(System.AppDomain.CurrentDomain.BaseDirectory + "FatalExceptionLog.txt", Node.Core.Logging.Logger.LEVEL_DEBUG);
            logger.Log(ex);
        }
        //clear the data flow engine process log
        DataFlow.Component.Interface.IActionProcess process = new Node.Core.Biz.Manageable.DllManager().GetActionProcess();
        process.NodeURI = new Node.Core.Biz.Objects.SystemConfiguration().GetNodeAddress();
        process.Initialize();
        //end of init.        

        if (!string.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["TLS"]))
        {
            switch (System.Configuration.ConfigurationManager.AppSettings["TLS"].ToUpper())
            {
                case "TLS11":
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolTypeExtensions.Tls11;
                    break;
                case "TLS12":
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolTypeExtensions.Tls12;
                    break;
                default:
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolTypeExtensions.SystemDefault;
                    break;
            }
        }
        
    }
    
    void Application_End(Object sender, EventArgs e) {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(Object sender, EventArgs e) { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(Object sender, EventArgs e) {
        // Code that runs when a new session is started

    }

    void Session_End(Object sender, EventArgs e) {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    void Application_BeginRequest(Object sender, EventArgs e)
    {
        HttpContext context = HttpContext.Current;
        //Node.Core.Logging.Logger logger = new Node.Core.Logging.Logger(System.AppDomain.CurrentDomain.BaseDirectory + "FatalExceptionLog.txt", Node.Core.Logging.Logger.LEVEL_DEBUG);
        if (!context.Request.ContentType.Contains("action="))
        {
            context.Request.ContentType = context.Request.ContentType + "; action=\"\"";
        }

        if (context.Request.ContentType.Contains("Multipart/Related;"))
        {
            context.Request.ContentType = context.Request.ContentType.Replace("Multipart/Related;", "multipart/related;");
        }

        if (context.Request.ContentType.Contains("startinfo="))
        {
            context.Request.ContentType =  context.Request.ContentType.Replace("startinfo=", "start-info=");
        }
                
    }
    
       
</script>
