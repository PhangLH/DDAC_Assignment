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
    public partial class CreateNewPort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlStaff.Items[0].Attributes.Add("disabled", "disabled");
        }

        protected void CreatePort_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string createPort = "INSERT INTO Port (por_name,por_latitude,por_longitude,use_id) VALUES (@name,@latitude,@longitude,@userid);";
            SqlCommand cmdCreatePort = new SqlCommand(createPort, conn);
            cmdCreatePort.Parameters.Add("@name", SqlDbType.NVarChar);
            cmdCreatePort.Parameters["@name"].Value = tboxName.Text;
            cmdCreatePort.Parameters.Add("@latitude", SqlDbType.Float);
            cmdCreatePort.Parameters["@latitude"].Value = tboxLat.Text;
            cmdCreatePort.Parameters.Add("@longitude", SqlDbType.Float);
            cmdCreatePort.Parameters["@longitude"].Value = tboxLong.Text;
            cmdCreatePort.Parameters.Add("@userid", SqlDbType.Int);
            cmdCreatePort.Parameters["@userid"].Value = int.Parse(ddlStaff.SelectedValue);

            conn.Open();
            int success = cmdCreatePort.ExecuteNonQuery();
            conn.Close();

            //not success
            if (success == 0)
            {
            }
            //success
            else
            {
                Response.Redirect("/Admin/CheckPort", false);
            }
        }
    }
}