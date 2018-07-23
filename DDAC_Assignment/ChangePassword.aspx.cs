using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDAC_Assignment
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ChangePassword_Click(object sender, EventArgs e)
        {
            var oldPassword = "";
            int currentId = int.Parse(Session["UserId"].ToString());

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            //check old and new password
            string checkChangePassword = "SELECT use_password FROM Users WHERE use_id = @id;";
            SqlCommand cmdCheckChangePassword = new SqlCommand(checkChangePassword, conn);
            cmdCheckChangePassword.Parameters.Add("@id", SqlDbType.Int);
            cmdCheckChangePassword.Parameters["@id"].Value = currentId;
            conn.Open();
            SqlDataReader dr = cmdCheckChangePassword.ExecuteReader();
            if (dr.Read())
            {
                oldPassword = dr["use_password"].ToString();
                dr.Close();
            }
            conn.Close();

            if (oldPassword.Equals(ConfirmPassword.Text))
            {
                //error message
                Type cstype = this.GetType();
                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                {
                    String cstext = "alert('Invalid Change! Make sure new password is different with old password');";
                    cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                }
            }
            else
            {
                string changePassword = "UPDATE Users SET use_password = @password WHERE use_id = @id;";
                SqlCommand cmdChangePassword = new SqlCommand(changePassword, conn);
                cmdChangePassword.Parameters.Add("@password", SqlDbType.NVarChar);
                cmdChangePassword.Parameters["@password"].Value = ConfirmPassword.Text;
                cmdChangePassword.Parameters.Add("@id", SqlDbType.Int);
                cmdChangePassword.Parameters["@id"].Value = currentId;

                conn.Open();
                int success = cmdChangePassword.ExecuteNonQuery();
                conn.Close();

                //not success
                if (success == 0)
                {
                }
                //success
                else
                {
                    Response.Redirect("/Account/Login", false);
                }
            }
        }
    }
}