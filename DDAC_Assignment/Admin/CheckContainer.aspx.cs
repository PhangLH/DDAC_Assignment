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
    public partial class CheckContainer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckEmptyTable();
        }

        protected void gvCheckContainer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = int.Parse(e.CommandArgument.ToString());
            int selectedId = int.Parse(gvCheckContainer.Rows[rowIndex].Cells[0].Text);

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string deleteContainer = "DELETE FROM Container WHERE con_id = @id;";
            SqlCommand cmdDeleteContainer = new SqlCommand(deleteContainer, conn);

            cmdDeleteContainer.Parameters.Add("@id", SqlDbType.Int);
            cmdDeleteContainer.Parameters["@id"].Value = selectedId;

            conn.Open();
            int success = cmdDeleteContainer.ExecuteNonQuery();
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

        private void CheckEmptyTable()
        {
            int rowCount = gvCheckContainer.Rows.Count;
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