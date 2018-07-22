using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace DDAC_Assignment.Admin
{
    public partial class UpdatePort : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlStaff.Items[0].Attributes.Add("disabled", "disabled");

            if (!IsPostBack)
            {
                var updateInfo = Session["PortUpdateInfo"];
                var updateName = updateInfo.ToString().Split(',')[1];
                var updateLat = updateInfo.ToString().Split(',')[2];
                var updateLong = updateInfo.ToString().Split(',')[3];

                tboxName.Text = updateName;
                tboxLat.Text = updateLat;
                tboxLong.Text = updateLong;
            }
        }

        protected void UpdatePort_Click(object sender, EventArgs e)
        {
            try
            {
                var updateInfo = Session["PortUpdateInfo"];
                int updateID = int.Parse(updateInfo.ToString().Split(',')[0]);

                var name = tboxName.Text;

                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                string updatePort = "UPDATE Port SET por_name = @name,por_latitude = @latitude,por_longitude = @longitude,use_id = @userid WHERE por_id = @portid;";
                SqlCommand cmdUpdatePort = new SqlCommand(updatePort, conn);
                cmdUpdatePort.Parameters.Add("@name", SqlDbType.NVarChar);
                cmdUpdatePort.Parameters["@name"].Value = tboxName.Text;
                cmdUpdatePort.Parameters.Add("@latitude", SqlDbType.NVarChar);
                cmdUpdatePort.Parameters["@latitude"].Value = tboxLat.Text;
                cmdUpdatePort.Parameters.Add("@longitude", SqlDbType.NVarChar);
                cmdUpdatePort.Parameters["@longitude"].Value = tboxLong.Text;
                cmdUpdatePort.Parameters.Add("@userid", SqlDbType.Int);
                cmdUpdatePort.Parameters["@userid"].Value = int.Parse(ddlStaff.SelectedValue);
                cmdUpdatePort.Parameters.Add("@portid", SqlDbType.Int);
                cmdUpdatePort.Parameters["@portid"].Value = updateID;

                conn.Open();
                int success = cmdUpdatePort.ExecuteNonQuery();
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
            catch (Exception ex) { }
            
        }
    }
}