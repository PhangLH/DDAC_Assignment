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
    public partial class StaffReceival : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckEmptyTable();
        }

        protected void gvStaffReceival_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var command = e.CommandName;
            if (command.Equals("Receive"))
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int selectedId = int.Parse(gvStaffReceival.Rows[rowIndex].Cells[0].Text);
                var status = WebConfigurationManager.AppSettings["receivedStatusName"];


                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string receivalShippingRequest = "UPDATE ShippingRequest SET sr_status = @status WHERE sr_id = @id;";
                SqlCommand cmdReceivalShippingRequest = new SqlCommand(receivalShippingRequest, conn);

                cmdReceivalShippingRequest.Parameters.Add("@id", SqlDbType.Int);
                cmdReceivalShippingRequest.Parameters["@id"].Value = selectedId;
                cmdReceivalShippingRequest.Parameters.Add("@status", SqlDbType.NVarChar);
                cmdReceivalShippingRequest.Parameters["@status"].Value = status;

                conn.Open();
                int success = cmdReceivalShippingRequest.ExecuteNonQuery();
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
            int rowCount = gvStaffReceival.Rows.Count;
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