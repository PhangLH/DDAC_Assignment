using System;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Owin;
using DDAC_Assignment.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;

namespace DDAC_Assignment.Account
{
    public partial class Login : Page
    {
        //private bool rememberMe;
        private bool isPersistent = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["UserId"] = "";
            Session["UserEmail"] = "";
            Session["UserRole"] = "";
            Session["LoggedIn"] = "false";

            //RegisterHyperLink.NavigateUrl = "Register";
            //// Enable this once you have account confirmation enabled for password reset functionality
            ////ForgotPasswordHyperLink.NavigateUrl = "Forgot";
            //OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            //if (!String.IsNullOrEmpty(returnUrl))
            //{
            //    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            //}
        }

        protected void LogIn(object sender, EventArgs e)
        {
            //if (IsValid)
            //{

                //// Validate the user password
                //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                //var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                //// This doen't count login failures towards account lockout
                //// To enable password failures to trigger lockout, change to shouldLockout: true
                //var result = signinManager.PasswordSignIn(Email.Text, Password.Text, RememberMe.Checked, shouldLockout: false);

                //switch (result)
                //{
                //    case SignInStatus.Success:
                //        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                //        break;
                //    case SignInStatus.LockedOut:
                //        Response.Redirect("/Account/Lockout");
                //        break;
                //    case SignInStatus.RequiresVerification:
                //        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn?ReturnUrl={0}&RememberMe={1}",
                //                                        Request.QueryString["ReturnUrl"],
                //                                        RememberMe.Checked),
                //                          true);
                //        break;
                //    case SignInStatus.Failure:
                //    default:
                //        FailureText.Text = "Invalid login attempt";
                //        ErrorMessage.Visible = true;
                //        break;
                //}





            SqlConnection conn = new SqlConnection();
            try
            {
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                conn.Open();

                SqlCommand cmdLoginCheck = new SqlCommand("SELECT use_id,use_email,use_password,rol_id FROM Users WHERE use_email=@email and use_password=@password", conn);
                cmdLoginCheck.Parameters.Add(new SqlParameter("@email", Email.Text));
                cmdLoginCheck.Parameters.Add(new SqlParameter("@password", Password.Text));
                SqlDataReader dr = cmdLoginCheck.ExecuteReader();

                if (dr.Read())
                {
                    var myIdstr = dr["use_id"].ToString();
                    int myId = int.Parse(myIdstr);
                    var myRole = dr["rol_id"].ToString();
                    conn.Close();
                    dr.Close();

                    Session["UserId"] = myId;
                    Session["UserEmail"] = Email.Text;
                    Session["UserRole"] = myRole;
                    Session["LoggedIn"] = "true";

                    //remember me
                    //if (this.RememberMe != null && this.RememberMe.Checked == true)
                    //{
                    //    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1,
                    //    Email.Text,
                    //    DateTime.Now,
                    //    DateTime.Now.AddMinutes(30),
                    //    isPersistent,
                    //    Email.Text,
                    //    FormsAuthentication.FormsCookiePath);

                    //    Encrypt the ticket.
                    //    string encTicket = FormsAuthentication.Encrypt(ticket);

                    //    Create the cookie.
                    //   Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                    //    Redirect back to original URL.
                    //   Response.Redirect(FormsAuthentication.GetRedirectUrl(Email.Text, isPersistent));
                    //}
                    //Customer login
                    if (myRole.Equals("1"))
                    {
                        Response.Redirect("/CheckMyShipping",false);
                    }
                    //Staff login
                    else if (myRole.Equals("2"))
                    {
                        Response.Redirect("~/Staff/StaffApproval", false);
                    }
                    //Admin login
                    else if (myRole.Equals("3"))
                    {
                        Response.Redirect("~/Admin/CheckUser", false);
                    }
                }
                else
                {
                    //error message
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('Invalid User! Try again with VALID username and password');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }
                if (!dr.IsClosed)
                    dr.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
                conn.Dispose();
            }



            //}
        }
    }
}