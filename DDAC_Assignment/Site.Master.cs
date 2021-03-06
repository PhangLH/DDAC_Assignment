﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;

namespace DDAC_Assignment
{
    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;

        protected void Page_Init(object sender, EventArgs e)
        {
            //// The code below helps to protect against XSRF attacks
            //var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            //Guid requestCookieGuidValue;
            //if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            //{
            //    // Use the Anti-XSRF token from the cookie
            //    _antiXsrfTokenValue = requestCookie.Value;
            //    Page.ViewStateUserKey = _antiXsrfTokenValue;
            //}
            //else
            //{
            //    // Generate a new Anti-XSRF token and save to the cookie
            //    _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            //    Page.ViewStateUserKey = _antiXsrfTokenValue;

            //    var responseCookie = new HttpCookie(AntiXsrfTokenKey)
            //    {
            //        HttpOnly = true,
            //        Value = _antiXsrfTokenValue
            //    };
            //    if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            //    {
            //        responseCookie.Secure = true;
            //    }
            //    Response.Cookies.Set(responseCookie);
            //}

            //Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    // Set Anti-XSRF token
            //    ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            //    ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            //}
            //else
            //{
            //    // Validate the Anti-XSRF token
            //    if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
            //        || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
            //    {
            //        throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
            //    }
            //}
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //var myId = Session["UserId"];
            //var myEmail = Session["UserEmail"];
            //var currentId = Request.QueryString["id"];

            //if (!myId.Equals("") && !currentId.Equals(""))
            //{
            //    if (myId.Equals(currentId))
            //    {
            //    }
            //}

            object loggedIn = "";
            try
            {
                loggedIn = Session["LoggedIn"];
            }
            catch (Exception ex)
            {
                Session["LoggedIn"] = "";
            }
            finally
            {
                if (loggedIn.ToString().Equals("true"))
                {
                    var myRole = Session["UserRole"];
                    //customer role
                    if (myRole.Equals("1"))
                    {
                        btn_receivedLogHistory.Visible = false;
                        btn_manageShip.Visible = false;
                        btn_manageContainer.Visible = false;
                        btn_manageUser.Visible = false;
                        btn_managePort.Visible = false;
                        btn_staffReceival.Visible = false;
                        btn_staffApproval.Visible = false;
                        btn_checkMyShipping.Visible = true;
                        btn_newShipping.Visible = true;
                        btn_login.Visible = false;
                        btn_register.Visible = false;
                        btn_logout.Visible = true;
                        btn_manageaccount.Visible = true;
                    }
                    //staff role
                    else if (myRole.Equals("2"))
                    {
                        btn_receivedLogHistory.Visible = true;
                        btn_manageShip.Visible = false;
                        btn_manageContainer.Visible = false;
                        btn_manageUser.Visible = false;
                        btn_managePort.Visible = false;
                        btn_staffReceival.Visible = true;
                        btn_staffApproval.Visible = true;
                        btn_checkMyShipping.Visible = false;
                        btn_newShipping.Visible = false;
                        btn_login.Visible = false;
                        btn_register.Visible = false;
                        btn_logout.Visible = true;
                        btn_manageaccount.Visible = true;
                    }
                    //admin role
                    else if (myRole.Equals("3"))
                    {
                        btn_receivedLogHistory.Visible = false;
                        btn_manageShip.Visible = true;
                        btn_manageContainer.Visible = true;
                        btn_manageUser.Visible = true;
                        btn_managePort.Visible = true;
                        btn_staffReceival.Visible = false;
                        btn_staffApproval.Visible = false;
                        btn_checkMyShipping.Visible = false;
                        btn_newShipping.Visible = false;
                        btn_login.Visible = false;
                        btn_register.Visible = false;
                        btn_logout.Visible = true;
                        btn_manageaccount.Visible = true;
                    }
                }
                //else
                //{
                //    var userid = Session["UserId"];
                //    if (!userid.Equals(""))
                //    {
                //        //error message
                //        Type cstype = this.GetType();
                //        ClientScriptManager cs = Page.ClientScript;
                //        if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                //        {
                //            String cstext = "alert('Session timeout! Please login to the application again');";
                //            cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                //        }
                //        Response.Redirect("~/Account/Logout", false);
                //    }
                //}
            }            
        }

        protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        {
            //Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        //protected void logout_ServerClick(object sender, EventArgs e)
        //{
        //    Session["UserId"] = "";
        //    Session["UserEmail"] = "";
        //    Session["UserRole"] = "";
        //    Session["LoggedIn"] = "false";
        //    Response.Redirect("/Account/Login",false);
        //}
    }

}