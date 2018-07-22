using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DDAC_Assignment.Admin
{
    public partial class CheckPort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckEmptyTable();
        }

        protected void gvCheckPort_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var command = e.CommandName;
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            int selectedId = int.Parse(gvCheckPort.Rows[rowIndex].Cells[0].Text);
            if (command.Equals("DeleteRow"))
            {
                string deletePort = "DELETE FROM Port WHERE por_id = @id;";
                SqlCommand cmdDeletePort = new SqlCommand(deletePort, conn);
                cmdDeletePort.Parameters.Add("@id", SqlDbType.Int);
                cmdDeletePort.Parameters["@id"].Value = selectedId;

                conn.Open();
                int success = cmdDeletePort.ExecuteNonQuery();
                conn.Close();

                //not success
                if (success == 0)
                {
                }
                //success
                else
                {
                    string deleteShippingRequest = "DELETE FROM ShippingRequest WHERE sr_startportid = @id or sr_endportid = @id;";
                    SqlCommand cmdDeleteShippingRequest = new SqlCommand(deleteShippingRequest, conn);
                    cmdDeleteShippingRequest.Parameters.Add("@id", SqlDbType.Int);
                    cmdDeleteShippingRequest.Parameters["@id"].Value = selectedId;

                    conn.Open();
                    cmdDeleteShippingRequest.ExecuteNonQuery();
                    conn.Close();

                    Response.Redirect(Request.RawUrl);
                }
            }
            else if (command.Equals("UpdateRow"))
            {
                SqlCommand cmdUpdatePort = new SqlCommand("SELECT CONVERT(varchar(10), por_id) + ',' + por_name + ',' + por_latitude + ',' + por_longitude as por_info FROM Port WHERE por_id = @id;", conn);
                cmdUpdatePort.Parameters.Add("@id", SqlDbType.Int);
                cmdUpdatePort.Parameters["@id"].Value = selectedId;
                conn.Open();
                SqlDataReader dr = cmdUpdatePort.ExecuteReader();

                if (dr.Read())
                {
                    var myInfoStr = dr["por_info"].ToString();
                    Session["PortUpdateInfo"] = myInfoStr;
                    Response.Redirect("~/Admin/UpdatePort", false);
                    dr.Close();
                }
                conn.Close();
            }
        }

        private void CheckEmptyTable()
        {
            int rowCount = gvCheckPort.Rows.Count;
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