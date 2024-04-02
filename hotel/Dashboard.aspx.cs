using hotel.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hotel
{
    public partial class Dashboard : System.Web.UI.Page
    {

        CommonFnx fn = new CommonFnx();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                getRoomType();
            }
        }

        public void getRoomType()
        {
            DataTable dt = fn.Fetch("Select * from room_type");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }




        protected void onLogoutClick(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("Login.aspx");
        }

        protected void openPanel(object sender, EventArgs e)
        {
            Panel1.Visible = true;
        }

        protected void closePanel(object sender, EventArgs e)
        {
            Panel1.Visible = false;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "Script", "myFunction();", true);
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}