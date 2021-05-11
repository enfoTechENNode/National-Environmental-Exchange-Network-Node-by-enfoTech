using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Node.Lib.UI.WebUtils;
using Node.Lib.Utility;

namespace Node.Lib.UI.Elements
{
    /// <summary>
    /// GridViewField used by <see cref="EAF.Lib.UI.Elements.GridViewView"/>
    /// </summary>
    public class GridViewField
    {
        /// <summary>
        /// Assembly Type
        /// </summary>
        public enum AssemblyType
        {
            /// <summary>
            /// System.Web.dll
            /// </summary>
            System,
            /// <summary>
            /// EAF.Lib.UI.dll
            /// </summary>
            EAF
        }

        private Hashtable fieldAtts = new Hashtable();
        private AssemblyType asmType = AssemblyType.System;
		//private string fieldType = "";
		//private DataControlField fieldControl = null;
		//private GridViewAgent gvAgent = null;
		private string fieldType;
		private DataControlField fieldControl;
		private GridViewAgent gvAgent;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="agent">GridViewAgent object.</param>
        public GridViewField(GridViewAgent agent)
        {
            this.gvAgent = agent;
        }

        /// <summary>
        /// Assembly type of the control
        /// </summary>
        public AssemblyType AsmType
        {
            get { return this.asmType; }
            set { this.asmType = value; }
        }

        /// <summary>
        /// Get or set control type. Ex: TextBox, Label...
        /// </summary>
        public string FieldType
        {
            get { return this.fieldType; }
            set { this.fieldType = value; }
        }

        /// <summary>
        /// attributes list of the control
        /// </summary>
        public Hashtable FieldAttributes
        {
            get { return this.fieldAtts; }
            set { this.fieldAtts = value; }
        }

        /// <summary>
        /// Control object
        /// </summary>
		public DataControlField FieldControl
        {
            get { return this.fieldControl; }
			set { this.fieldControl = value; }
        }

        /// <summary>
        /// Instantiate the control.
        /// </summary>
        public void CreateField(object userControl)
        {
            DataControlField c = null;
            if (this.AsmType == AssemblyType.System)
				c = (DataControlField)WebUtility.SystemWebControlAssembly.CreateInstance("System.Web.UI.WebControls." + this.fieldType, true);
            else if (this.AsmType == AssemblyType.EAF)
				c = (DataControlField)WebUtility.EAFWebControlAssembly.CreateInstance("EAF.Lib.UI.WebControls." + this.fieldType, true);

            // set properties of control			
            if (c != null)
            {
                ICollection props = this.fieldAtts.Keys;
                Type ct = c.GetType();
                foreach (string prop in props)
                {
                    if (prop.Contains("-"))
                    {
                        string[] style = prop.Split('-');
                        if (style.Length == 2 && style[1] != "")
                        {
                            if (style[1] == "ItemStyle") ReflectionHelper.SetPropertyValue(c.ItemStyle, style[1], this.fieldAtts[prop]);
                            if (style[1] == "HeaderStyle") ReflectionHelper.SetPropertyValue(c.HeaderStyle, style[1], this.fieldAtts[prop]);
                            if (style[1] == "FooterStyle") ReflectionHelper.SetPropertyValue(c.FooterStyle, style[1], this.fieldAtts[prop]);
                        }
                    }
                    else
                    {
                        ReflectionHelper.SetPropertyValue(c, prop, this.fieldAtts[prop]);
                    }

					if (prop.StartsWith("On"))
						ReflectionHelper.AddEventHandler(c, prop.Substring(2), userControl, "" + this.fieldAtts[prop]);
                }
            }
			this.fieldControl = c;
        }
    }
}
