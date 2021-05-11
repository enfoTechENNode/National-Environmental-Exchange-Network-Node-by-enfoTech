using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Footer : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected string GetBuildYear()
    {
        string sText = System.Reflection.Assembly.LoadFrom(this.Request.PhysicalApplicationPath + @"\Bin\Node.Core2.dll").GetName().Version.ToString();
        string[] sTexts = sText.Split('.');
        int iDays = int.Parse(sTexts[2]);
        DateTime ndateStart = DateTime.Parse("2000/01/01");
        ndateStart = ndateStart.AddDays(iDays);
        return ndateStart.Year.ToString();

    }
}
