using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace DDAC_Assignment.Admin
{
    public partial class CheckShip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckEmptyTable();
        }

        protected void gvCheckShip_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            var command = e.CommandName;
            if (command.Equals("DeleteRow"))
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int selectedId = int.Parse(gvCheckShip.Rows[rowIndex].Cells[0].Text);

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string deleteShip = "DELETE FROM Ship WHERE shi_id = @id;";
                SqlCommand cmdDeleteShip = new SqlCommand(deleteShip, conn);

                cmdDeleteShip.Parameters.Add("@id", SqlDbType.Int);
                cmdDeleteShip.Parameters["@id"].Value = selectedId;

                conn.Open();
                int success = cmdDeleteShip.ExecuteNonQuery();
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
            int rowCount = gvCheckShip.Rows.Count;
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