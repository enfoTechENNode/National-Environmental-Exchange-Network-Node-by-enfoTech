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

namespace Node.Lib.UI.Elements
{
    /// <summary>
    /// GridViewView (Data) class
    /// </summary>
    [Serializable]
    public class GridViewView
    {
        private string id = "";
        private ArrayList actionFields = new ArrayList();
        private ArrayList dataFields = new ArrayList();
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public GridViewView()
        {}
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="viewID">View ID which you want to create</param>
        public GridViewView(string viewID)
        {
            this.id = viewID;
        }

        /// <summary>
        /// Get or set View ID.
        /// </summary>
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        //***********************************************************************
        // public methods
        //***********************************************************************

        /// <summary>
        /// Create fields of the GridView
        /// </summary>
        public void CreateFields(Control userControl)
        {
			AssignData(userControl);
        }

        /// <summary>
        /// Get or set list of <see cref="EAF.Lib.UI.Elements.GridViewField"/>.
        /// </summary>
        public ArrayList GridViewActionFields
        {
            get { return this.actionFields; }
            set { this.actionFields = value; }
        }

        /// <summary>
        /// Get or set list of <see cref="EAF.Lib.UI.Elements.GridViewField"/>.
        /// </summary>
        public ArrayList GridViewDataFields
        {
            get { return this.dataFields; }
            set { this.dataFields = value; }
        }

        /// <summary>
        /// Create fields for gridview
        /// </summary>
		private void AssignData(Control userControl)
        {
            for (int i = 0; i < this.GridViewActionFields.Count; i++)
            {
                GridViewField gvField = (GridViewField)this.GridViewActionFields[i];
				gvField.CreateField(userControl);
            }

            for (int j = 0; j < this.GridViewDataFields.Count; j++)
            {
                GridViewField gvField = (GridViewField)this.GridViewDataFields[j];
				gvField.CreateField(userControl);
            }
        }
    }
}
