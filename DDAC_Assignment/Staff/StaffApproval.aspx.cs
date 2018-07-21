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

namespace DDAC_Assignment.Staff
{
    public partial class StaffApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckEmptyTable();
        }

        protected void gvStaffApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var command = e.CommandName;
            if (command.Equals("Approve"))
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int selectedId = int.Parse(gvStaffApproval.Rows[rowIndex].Cells[0].Text);
                var status = WebConfigurationManager.AppSettings["approvedStatusName"];


                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string approveShippingRequest = "UPDATE ShippingRequest SET sr_status = @status WHERE sr_id = @id;";
                SqlCommand cmdApproveShippingRequest = new SqlCommand(approveShippingRequest, conn);

                cmdApproveShippingRequest.Parameters.Add("@id", SqlDbType.Int);
                cmdApproveShippingRequest.Parameters["@id"].Value = selectedId;
                cmdApproveShippingRequest.Parameters.Add("@status", SqlDbType.NVarChar);
                cmdApproveShippingRequest.Parameters["@status"].Value = status;

                conn.Open();
                int success = cmdApproveShippingRequest.ExecuteNonQuery();
                conn.Close();

                //not success
                if (success == 0)
                {
                }
                //success
                else
                {
                    Response.Redirect(Request.RawUrl);
                }
            }
        }

        private void CheckEmptyTable()
        {
            int rowCount = gvStaffApproval.Rows.Count;
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