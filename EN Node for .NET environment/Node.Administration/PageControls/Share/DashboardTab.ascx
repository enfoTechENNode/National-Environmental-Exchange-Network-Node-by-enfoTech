<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashboardTab.ascx.cs" Inherits="PageControls_Share_DashboardTab" %>
<%@ Import Namespace="Node.Lib.UI.Elements" %>

<div id="hb">
  <table  cellspacing="0" cellpadding="0" style="height:46px;">
    <tr>
      <% 
      bool currentTabOn = false;
      string onTab = null;
      Response.Write("<td>");
      Response.Write("<img style=\"vertical-align:bottom;\" src=\"" + ResolveUrl("~/App_Images/Node/Header/Node_Home.gif") + "\" alt=\"Node Home\""+"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>");
      if (headTabs != null && headTabs.Count > 0)
      {
          for (int i = 0; i < this.headTabs.Count; i++)
          {
              string tab = (string)this.headTabs[i];

              currentTabOn = false;
              if (IsTabFocus(tab))
              {
                  currentTabOn = true;
                  onTab = tab;
              }

              if (currentTabOn)
              {
                  Response.Write("<td>");
                  if (i == 0)
                  {
                      Response.Write("<a href=\"" + ResolveUrl("~/Pages/Main/Home.aspx?ver=1") + "\" title=\"" + "Node 1.1" + "\">");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HTOn_End_L.gif") + "\" alt = \"\"/>");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HI_Node11_On.gif") + "\"alt = \"Node 1.1\"/>");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HTOn_Mid_R.gif") + "\"alt = \"\"/>");
                  }
                  else
                  {
                      Response.Write("<a href=\"" + ResolveUrl("~/Pages/Main/Home.aspx?ver=2") + "\" title=\"" + "Node 2.0" + "\">");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HI_Node20_On.gif") + "\" alt = \"Node 2.0\"/>");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HTOn_End_R.gif") + "\" alt = \"\"/>");
                  }
                  Response.Write("</a>");
                  Response.Write("</td>");
              }
              else
              {
                  Response.Write("<td>");
                  if (i == 0)
                  {
                      Response.Write("<a href=\"" + ResolveUrl("~/Pages/Main/Home.aspx?ver=1") + "\" title=\"" + "Node 1.1" + "\">");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HTOff_End_L.gif") + "\" alt = \"\"/>");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HI_Node11_Off.gif") + "\" alt = \"Node 1.1\"/>");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HTOn_Mid_L.gif") + "\" alt = \"\"/>");
                  }
                  else
                  {
                      Response.Write("<a href=\"" + ResolveUrl("~/Pages/Main/Home.aspx?ver=2") + "\" title=\"" + "Node 2.0" + "\">");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HI_Node20_Off.gif") + "\" alt = \"Node 2.0\"/>");
                      Response.Write("<img src=\"" + ResolveUrl("~/App_Images/Node/Header/HTOff_End_R.gif") + "\" alt = \"\"/>");
                  }
                  Response.Write("</a>");
                  Response.Write("</td>");
              }
          }
      }
      %>
      </tr>
  </table>
</div>
  