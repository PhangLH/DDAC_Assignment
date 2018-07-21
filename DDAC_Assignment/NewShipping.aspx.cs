using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Device.Location;
using System.Web.Configuration;

namespace DDAC_Assignment
{
    public partial class NewShipping : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlStartPort.Items[0].Attributes.Add("disabled", "disabled");
            ddlEndPort.Items[0].Attributes.Add("disabled", "disabled");
        }

        protected void Create_Click(object sender, EventArgs e)
        {
            var status = WebConfigurationManager.AppSettings["pendingStatusName"];
            int startportid = int.Parse(ddlStartPort.SelectedValue.Split(',')[2]);
            int endportid = int.Parse(ddlEndPort.SelectedValue.Split(',')[2]);
            var desc = tboxDesc.Text;
            DateTime currentDateTime = DateTime.Now;
            double price = double.Parse(tboxPrice.Text);
            int shipid = int.Parse(ddlShip.SelectedValue.Split(',')[1]);
            int containerid = int.Parse(ddlContainer.SelectedValue.Split(',')[1]);
            int userid = int.Parse(Session["UserId"].ToString());

            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string createShippingRequest = "INSERT INTO ShippingRequest (sr_status,sr_startportid,sr_endportid,sr_desc,sr_creationdatetime,sr_price,shi_id,use_id,con_id) " +
                "VALUES (@status,@startportid,@endportid,@desc,@creationdatetime,@price,@shipid,@userid,@containerid);";
            SqlCommand cmdCreateShippingRequest = new SqlCommand(createShippingRequest, conn);

            cmdCreateShippingRequest.Parameters.Add("@status", SqlDbType.NVarChar);
            cmdCreateShippingRequest.Parameters["@status"].Value = status;
            cmdCreateShippingRequest.Parameters.Add("@startportid", SqlDbType.Int);
            cmdCreateShippingRequest.Parameters["@startportid"].Value = startportid;
            cmdCreateShippingRequest.Parameters.Add("@endportid", SqlDbType.Int);
            cmdCreateShippingRequest.Parameters["@endportid"].Value = endportid;
            cmdCreateShippingRequest.Parameters.Add("@desc", SqlDbType.NVarChar);
            cmdCreateShippingRequest.Parameters["@desc"].Value = desc;
            cmdCreateShippingRequest.Parameters.Add("@creationdatetime", SqlDbType.DateTime);
            cmdCreateShippingRequest.Parameters["@creationdatetime"].Value = currentDateTime;
            cmdCreateShippingRequest.Parameters.Add("@price", SqlDbType.Float);
            cmdCreateShippingRequest.Parameters["@price"].Value = price;
            cmdCreateShippingRequest.Parameters.Add("@shipid", SqlDbType.Int);
            cmdCreateShippingRequest.Parameters["@shipid"].Value = shipid;
            cmdCreateShippingRequest.Parameters.Add("@userid", SqlDbType.Int);
            cmdCreateShippingRequest.Parameters["@userid"].Value = userid;
            cmdCreateShippingRequest.Parameters.Add("@containerid", SqlDbType.Int);
            cmdCreateShippingRequest.Parameters["@containerid"].Value = containerid;

            conn.Open();
            int success = cmdCreateShippingRequest.ExecuteNonQuery();
            conn.Close();

            //not success
            if (success == 0)
            {
            }
            //success
            else
            {
                Response.Redirect("/CheckMyShipping.aspx", false);
            }
        }

        protected void ddlContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        protected void ddlShip_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        private void UpdatePrice()
        {
            double currentPrice = 0.00;

            if (!ddlStartPort.SelectedIndex.Equals(0) && !ddlEndPort.SelectedIndex.Equals(0) && !ddlEndPort.SelectedIndex.Equals(ddlStartPort.SelectedIndex))
            {
                var startLocation = ddlStartPort.SelectedValue;
                var endLocation = ddlEndPort.SelectedValue;

                double startLat = double.Parse(startLocation.Split(',')[0]);
                double startLong = double.Parse(startLocation.Split(',')[1]);

                double endLat = double.Parse(endLocation.Split(',')[0]);
                double endLong = double.Parse(endLocation.Split(',')[1]);

                var sCoord = new GeoCoordinate(startLat, startLong);
                var eCoord = new GeoCoordinate(endLat, endLong);

                double price = sCoord.GetDistanceTo(eCoord) / 1000;
                double finalPrice = Math.Round(price, 2);

                currentPrice = currentPrice + finalPrice;
            }

            if (!ddlContainer.SelectedIndex.Equals(0))
            {
                double conPrice = double.Parse(ddlContainer.SelectedValue.Split(',')[0]);
                currentPrice = currentPrice + conPrice;
            }

            if (!ddlShip.SelectedIndex.Equals(0))
            {
                double shipPrice = double.Parse(ddlShip.SelectedValue.Split(',')[0]);
                currentPrice = currentPrice + shipPrice;
            }

            tboxPrice.Text = currentPrice.ToString();
        }

        protected void ddlStartPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }

        protected void ddlEndPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePrice();
        }
    }
}