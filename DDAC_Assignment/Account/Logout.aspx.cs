﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC_Assignment.Account
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = "";
            Session["UserEmail"] = "";
            Session["UserRole"] = "";
            Session["LoggedIn"] = "false";
            Response.Redirect("/Account/Login", false);
        }
    }
}