using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC_Assignment.Admin
{
    public partial class CheckUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckEmptyTable();
        }

        protected void gvCheckUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var command = e.CommandName;
            if (command.Equals("DeleteRow"))
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int selectedId = int.Parse(gvCheckUser.Rows[rowIndex].Cells[0].Text);
                int selectedRole = int.Parse(gvCheckUser.Rows[rowIndex].Cells[3].Text);
                var status = WebConfigurationManager.AppSettings["receivedStatusName"];
                int existCount = 0;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                //check exist sr for the deleting user
                string checkCountSR = "SELECT * FROM ShippingRequest WHERE use_id = @id";
                SqlCommand cmdCheckCountSR = new SqlCommand(checkCountSR, conn);
                cmdCheckCountSR.Parameters.Add("@id", SqlDbType.Int);
                cmdCheckCountSR.Parameters["@id"].Value = selectedId;
                conn.Open();
                SqlDataReader dr = cmdCheckCountSR.ExecuteReader();
                if (dr.Read())
                {
                    existCount++;
                    
                    dr.Close();
                }
                conn.Close();

                //delete the user
                string deleteUser = "DELETE FROM Users WHERE use_id = @id;";
                SqlCommand cmdDeleteUser = new SqlCommand(deleteUser, conn);
                cmdDeleteUser.Parameters.Add("@id", SqlDbType.Int);
                cmdDeleteUser.Parameters["@id"].Value = selectedId;
                conn.Open();
                int success = cmdDeleteUser.ExecuteNonQuery();
                conn.Close();

                //not success
                if (success == 0)
                {
                }
                //success
                else
                {
                    if (selectedRole.Equals(1))
                    {
                        //if no sr for the selected user
                        if (existCount == 0)
                        {
                            Response.Redirect(Request.RawUrl);
                        }
                        //if have sr for the selected user
                        else
                        {
                            string deleteShippingRequest = "DELETE FROM ShippingRequest WHERE use_id = @id;";
                            SqlCommand cmdDeleteShippingRequest = new SqlCommand(deleteShippingRequest, conn);
                            cmdDeleteShippingRequest.Parameters.Add("@id", SqlDbType.Int);
                            cmdDeleteShippingRequest.Parameters["@id"].Value = selectedId;
                            conn.Open();
                            int successDeleteSR = cmdDeleteShippingRequest.ExecuteNonQuery();
                            conn.Close();

                            //not success
                            if (successDeleteSR == 0)
                            {
                            }
                            //success
                            else
                            {
                                Response.Redirect(Request.RawUrl);
                            }
                        } 
                    }
                    else
                    {
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
        }

        private void CheckEmptyTable()
        {
            int rowCount = gvCheckUser.Rows.Count;
            if (rowCount.Equals(0))
            {
                lblEmptyTable.Visible = true;
            }
            else
            {
                lblEmptyTable.Visible = false;
            }
        }
    }
}