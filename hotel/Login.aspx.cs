using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Security;
using System.Data;

namespace hotel
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Obsolete]
        protected void onSubmit(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtUserPass.Text;
            if(FormsAuthentication.Authenticate(username, password))
            {
                FormsAuthentication.RedirectFromLoginPage(username, true);
            } else
            {
                lblMsg.Text = "Tài khoản hoặc mật khẩu không đúng";
            }
        }
    }
}