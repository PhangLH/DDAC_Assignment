using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Configuration;

namespace DDAC_Assignment.Admin
{
    public partial class RegisterStaffUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            //var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            //var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
            //IdentityResult result = manager.Create(user, Password.Text);
            //if (result.Succeeded)
            //{
            //    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
            //    //string code = manager.GenerateEmailConfirmationToken(user.Id);
            //    //string callbackUrl = IdentityHelper.GetUserConfirmationRedirectUrl(code, user.Id, Request);
            //    //manager.SendEmail(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>.");

            //    signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
            //    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //}
            //else 
            //{
            //    ErrorMessage.Text = result.Errors.FirstOrDefault();
            //}

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string createCustomerUser = "INSERT INTO Users (use_email,use_password,rol_id,use_name,use_contactno) VALUES (@email,@password,@roleId,@name,@contactno);";
            SqlCommand cmdCreateCustomerUser = new SqlCommand(createCustomerUser, conn);
            cmdCreateCustomerUser.Parameters.Add("@email", SqlDbType.NVarChar);
            cmdCreateCustomerUser.Parameters["@email"].Value = Email.Text;
            cmdCreateCustomerUser.Parameters.Add("@password", SqlDbType.NVarChar);
            cmdCreateCustomerUser.Parameters["@password"].Value = Password.Text;
            cmdCreateCustomerUser.Parameters.Add("@roleId", SqlDbType.Int);
            cmdCreateCustomerUser.Parameters["@roleId"].Value = WebConfigurationManager.AppSettings["StaffRoleID"];
            cmdCreateCustomerUser.Parameters.Add("@name", SqlDbType.NVarChar);
            cmdCreateCustomerUser.Parameters["@name"].Value = tboxName.Text;
            cmdCreateCustomerUser.Parameters.Add("@contactno", SqlDbType.Char);
            cmdCreateCustomerUser.Parameters["@contactno"].Value = tboxContactNo.Text;

            conn.Open();
            int success = cmdCreateCustomerUser.ExecuteNonQuery();
            conn.Close();

            //not success
            if (success == 0)
            {
            }
            //success
            else
            {
                Response.Redirect("~/Admin/CheckUser", false);
            }
        }
    }
}