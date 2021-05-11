package Node.Biz.Administration;

import java.util.ArrayList;

import Node.Phrase;
import Node.DB.DBManager;
import Node.DB.Interfaces.Configuration.IConfigurationMgr;

import com.enfotech.basecomponent.typelib.xml.XmlDocument;
import com.enfotech.basecomponent.typelib.xml.XmlNode;
import com.enfotech.basecomponent.typelib.xml.XmlNodeList;
import com.enfotech.bizmodule.ep.engine.mail.TemplateEntity;
import com.enfotech.bizmodule.ep.engine.mail.TemplateManager;

public class EmailTemplate extends TemplateManager{
	/**
	 * 
	 */
	private static final long serialVersionUID = 8468798854929072549L;
	public static final String EMAILTEMPLATE_FILENAME = "EmailTemplate";
	public static final String EMAILTEMPLATE_UIC_SUCCESS = "UIC_SUCCESS";
	public static final String EMAILTEMPLATE_UIC_FAIL = "UIC_FAIL";

	private XmlDocument xmlDoc;
	private String appRoot = "";

	public String[] GetAllTemplateName() throws Exception
	{
		String[] names = null;

		try
		{
			XmlNodeList nodes = xmlDoc.SelectNodes(".//TemplateName");
			if(nodes == null || nodes.Count()<=0)
				return names;
			names = new String[nodes.Count()];
			for(int i=0; i<nodes.Count(); i++)
				names[i]=nodes.ItemOf(i).GetInnerText();
		}
		catch(Exception e)
		{
			throw e;
		}

		return names;
	}

	public String[] GetAllActiveTemplateName() throws Exception
	{
		String[] names = null;
		ArrayList nameList = new ArrayList();
		try
		{
			XmlNodeList nodes = xmlDoc.SelectNodes(".//Template");
			if(nodes == null || nodes.Count()<=0)
				return names;
			XmlNode templateNode = null;
			XmlNode statusNode = null;
			for(int i=0; i<nodes.Count(); i++){
				templateNode = nodes.ItemOf(i);
				statusNode = templateNode.Attributes().GetNamedItem("status");
				if(statusNode != null && statusNode.GetValue().equalsIgnoreCase("A")){
					nameList.add(templateNode.SelectSingleNode(".//TemplateName").GetInnerText());
				}
			}
			if(nameList != null && !nameList.isEmpty()){
				names = new String[nameList.size()];
				for(int i=0; i<nameList.size(); i++){
					names[i] = nameList.get(i)+"";    		  
				}
			}
		}
		catch(Exception e)
		{
			throw e;
		}

		return names;
	}

	public boolean UpdateTemplate(TemplateEntity template, String userId) throws Exception
	{
		boolean isOK = false;

		try
		{
			if(template == null) return isOK = true;

			XmlNode node = xmlDoc.SelectSingleNode("/EmailTemplate/Template[TemplateName='"+template.Name+"']");
			if(node == null)
				throw new Exception("Template ["+template.Name+"] cannot be found in template file.");

			//update template
			XmlNode tempNode = node.SelectSingleNode(".//TemplateTo");
			tempNode.SetInnerText(template.To);
			tempNode = node.SelectSingleNode(".//TemplateSubject");
			tempNode.SetInnerText(template.Subject);
			tempNode = node.SelectSingleNode(".//TemplateContent");
			tempNode.SetInnerText(template.Content);

			java.io.ByteArrayOutputStream out = new java.io.ByteArrayOutputStream();
			xmlDoc.Save(out, 5);
			byte[] content = out.toByteArray();
			out.close();
			out = null;

			IConfigurationMgr config = DBManager.GetConfigurationMgr(Phrase.TaskLoggerName);
			isOK = config.SaveGeneralConfigFile(template.Name, content);
		}
		catch(Exception e)
		{
			throw e;
		}

		return isOK;
	}
}
