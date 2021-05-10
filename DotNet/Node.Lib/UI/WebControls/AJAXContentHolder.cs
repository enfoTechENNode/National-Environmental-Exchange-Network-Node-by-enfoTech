using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Lib.UI.WebUtils;

namespace Node.Lib.UI.WebControls
{
	
	
	public class AjaxContentHolder : PlaceHolder
	{
		public delegate void CallBackActionHandler(AjaxContentEventArgs args);
		public event CallBackActionHandler CallBackAction;
		protected virtual void OnCallBackAction(AjaxContentEventArgs args)
		{
			if (CallBackAction != null)
			{
				CallBackAction(args);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			HttpRequest request = this.Page.Request;
			HttpResponse response = this.Page.Response;

			if (""+request["cb"] == this.ClientID)
			{
				response.Clear();

				AjaxContentEventArgs args = new AjaxContentEventArgs();
				args.Parameter = "" + request["p"];
				args.Output = new HtmlTextWriter(response.Output);

				//for (int i = 0; i < this.Controls.Count; i++)
				//{
				//    Control c = this.Controls[i];
				//    if (c is AjaxContentTemplate)
				//    {
				//        args.ContentHolder = (PlaceHolder)c;
				//        break;
				//    }
				//}

				
				OnCallBackAction(args);
				//this.RenderControl(args.Output);
				response.End();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);

			PageUtility.RegisterUtilsScript(this.Page);
			PageUtility.RegisterAjaxScript(this.Page);

			if (!this.Page.ClientScript.IsClientScriptBlockRegistered(this.ClientID+"_Callback"))
			{
				StringBuilder s = new StringBuilder();
				s.AppendLine("<script type=\"text/javascript\" language=\"javascript\"><!--");

				s.AppendLine("var " + this.ClientID + "Obj = new AjaxContentHolder('" + this.ClientID + "','" + this.Page.Request.Path + "?cb=" + this.ClientID + "');");

				//s.AppendLine("function " + this.ClientID + "Obj(){return this;};");
				//s.AppendLine(this.ClientID + "Obj.callback=function(parm){");
				//s.AppendLine("document.getElementById('ajaxCntDiv_" + this.ClientID + "').innerHTML = document.getElementById('ajaxWaitDiv_" + this.ClientID + "').innerHTML;");
				//s.AppendLine("Utils.loadAjaxDiv('ajaxCntDiv_" + this.ClientID + "', '" + this.Page.Request.Path + "?cb=" + this.ClientID + "',parm);");
				//s.AppendLine("}");

				s.AppendLine("--></script>");

				this.Page.ClientScript.RegisterClientScriptBlock(this.Page.GetType(), this.ClientID + "_Callback", s.ToString());
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="output"></param>
		protected override void Render(HtmlTextWriter output)
		{
			output.Write("<div style=\"height:100%;\">");

			bool foundMainCnt = false;
			bool foundWaitCnt = false;

			for (int i = 0; i < this.Controls.Count; i++)
			{
				Control c = this.Controls[i];
				if (c is AjaxContentTemplate && !foundMainCnt)
				{
					output.Write("<div id=\"ajaxCntDiv_"+this.ClientID+"\">");
					c.RenderControl(output);
					output.Write("</div>");
					foundMainCnt = true;
				}
				else if (c is AjaxWaitingTemplate && !foundWaitCnt)
				{
					output.Write("<div id=\"ajaxWaitDiv_" + this.ClientID + "\" style=\"display:none;\">");
					c.RenderControl(output);
					output.Write("</div>");
					foundWaitCnt = true;
				}
			}

			if (!foundWaitCnt)
			{
				output.Write("<div id=\"ajaxWaitDiv_" + this.ClientID + "\" style=\"padding:20px; height:100%; vertical-align:middle; display:none;\">");
				output.Write("<span style=\"font:bold 8pt Tahoma,Arial;\">Loading......</span> ");
				output.Write("<img align=\"middle\" src=\"" + this.Page.Request.ApplicationPath + Properties.Settings.Default.ImageBase + "spinner.gif\" alt=\"\" />");
				output.Write("</div>");
			}

			output.Write("</div>");
		}
	}
}
