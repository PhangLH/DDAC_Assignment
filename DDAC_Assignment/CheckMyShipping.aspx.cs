using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DDAC_Assignment
{
    public partial class CheckMyShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckEmptyTable();
        }

        protected void gvCheckShipping_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var command = e.CommandName;
            if (command.Equals("DeleteRow"))
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int selectedId = int.Parse(gvCheckShipping.Rows[rowIndex].Cells[0].Text);
                var status = gvCheckShipping.Rows[rowIndex].Cells[1].Text;

                if (!status.Equals("PendingApproval"))
                {
                    //error message
                    Type cstype = this.GetType();
                    ClientScriptManager cs = Page.ClientScript;
                    if (!cs.IsStartupScriptRegistered(cstype, "PopupScript"))
                    {
                        String cstext = "alert('Invalid Delete! only Shipping with PendingApproval status can be deleted!');";
                        cs.RegisterStartupScript(cstype, "PopupScript", cstext, true);
                    }
                }
                else
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    string deleteShippingRequest = "DELETE FROM ShippingRequest WHERE sr_id = @id;";
                    SqlCommand cmdDeleteShippingRequest = new SqlCommand(deleteShippingRequest, conn);

                    cmdDeleteShippingRequest.Parameters.Add("@id", SqlDbType.Int);
                    cmdDeleteShippingRequest.Parameters["@id"].Value = selectedId;

                    conn.Open();
                    int success = cmdDeleteShippingRequest.ExecuteNonQuery();
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
            //var startPortName = gvCheckShipping.Rows[rowIndex].Cells[6].Text;
            //var endPortName = gvCheckShipping.Rows[rowIndex].Cells[7].Text;
            //var shipName = gvCheckShipping.Rows[rowIndex].Cells[8].Text;
            //var conName = gvCheckShipping.Rows[rowIndex].Cells[9].Text;
            //var desc = gvCheckShipping.Rows[rowIndex].Cells[3].Text;
            //var price = gvCheckShipping.Rows[rowIndex].Cells[5].Text;
        }

        private void CheckEmptyTable()
        {
            int rowCount = gvCheckShipping.Rows.Count;
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