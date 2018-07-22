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
    public partial class CreateNewShip : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CreateShip_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string createShip = "INSERT INTO Ship (shi_desc,shi_name,shi_price) VALUES (@desc,@name,@price);";
            SqlCommand cmdCreateShip = new SqlCommand(createShip, conn);
            cmdCreateShip.Parameters.Add("@desc", SqlDbType.NVarChar);
            cmdCreateShip.Parameters["@desc"].Value = tboxDesc.Text;
            cmdCreateShip.Parameters.Add("@name", SqlDbType.NVarChar);
            cmdCreateShip.Parameters["@name"].Value = tboxName.Text;
            cmdCreateShip.Parameters.Add("@price", SqlDbType.Float);
            cmdCreateShip.Parameters["@price"].Value = tboxPrice.Text;

            conn.Open();
            int success = cmdCreateShip.ExecuteNonQuery();
            conn.Close();

            //not success
            if (success == 0)
            {
            }
            //success
            else
            {
                Response.Redirect("/Admin/CheckShip", false);
            }
        }
    }
}