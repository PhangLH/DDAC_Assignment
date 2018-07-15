using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC_Assignment
{
    public partial class NewShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlStartPort.Items[0].Attributes.Add("disabled", "disabled");
            ddlEndPort.Items[0].Attributes.Add("disabled", "disabled");
        }

        protected void Next_Click(object sender, EventArgs e)
        {
        }
    }
}