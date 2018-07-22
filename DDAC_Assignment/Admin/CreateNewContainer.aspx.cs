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
    public partial class CreateNewContainer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void CreateContainer_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string createContainer = "INSERT INTO Container (con_name,con_price,con_size) VALUES (@name,@price,@size);";
            SqlCommand cmdCreateContainer = new SqlCommand(createContainer, conn);
            cmdCreateContainer.Parameters.Add("@name", SqlDbType.NVarChar);
            cmdCreateContainer.Parameters["@name"].Value = tboxName.Text;
            cmdCreateContainer.Parameters.Add("@price", SqlDbType.Float);
            cmdCreateContainer.Parameters["@price"].Value = tboxPrice.Text;
            cmdCreateContainer.Parameters.Add("@size", SqlDbType.Int);
            cmdCreateContainer.Parameters["@size"].Value = tboxSize.Text;

            conn.Open();
            int success = cmdCreateContainer.ExecuteNonQuery();
            conn.Close();

            //not success
            if (success == 0)
            {
            }
            //success
            else
            {
                Response.Redirect("/Admin/CheckContainer", false);
            }
        }
    }
}