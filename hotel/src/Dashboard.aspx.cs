using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hotel.src
{
    public partial class Dashboard : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hotelCS"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getBooking();
                getGuest();
                getRoom();
            }
        }

        public void getBooking()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "Select COUNT(*) from booking";
            SqlCommand cmd = new SqlCommand(query, con);
            var count = (Int32)cmd.ExecuteScalar();
            txtBooking.Text = count.ToString();
            con.Close();
        }
        public void getRoom()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "Select COUNT(*) from room";
            SqlCommand cmd = new SqlCommand(query, con);
            var count = (Int32)cmd.ExecuteScalar();
            txtRoom.Text = count.ToString();
            con.Close();
        }
        public void getGuest()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "Select COUNT(*) from guest";
            SqlCommand cmd = new SqlCommand(query, con);
            var count = (Int32)cmd.ExecuteScalar();
            txtUser.Text = count.ToString();
            con.Close();
        }
    }
}