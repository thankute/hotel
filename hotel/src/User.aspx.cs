using hotel.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hotel.src
{
    public partial class User : System.Web.UI.Page
    {
        CommonFnx fn = new CommonFnx();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hotelCS"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getGuest();
            }
        }

        public void getGuest()
        {
            DataTable dt = fn.Fetch("select * from guest where status = 0");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void onRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell = e.Row.Cells[4];
                if (statusCell.Text == "0")
                {
                    statusCell.Text = "Nam";
                }
                else if (statusCell.Text == "1")
                {
                    statusCell.Text = "Nữ";
                }
                else
                {
                    statusCell.Text = "-";
                }

            }
        }


        protected void AddBtnClick(object sender, EventArgs e)
        {
            ClearText();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", "openModal();", true);
        }

        protected void onSubmit(object sender, EventArgs e)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query;
            SqlCommand cmd;


            if (!string.IsNullOrEmpty(hiddenStatus.Value))
            {
                string id = hiddenStatus.Value;
                query = "update guest set fullname=@fullname, mobile=@mobile, gender=@gender, address=@address, passport=@passport,email=@email where ID=@id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
            }
            else
            {
                query = "insert into guest(fullname,mobile,gender,address,passport,email) VALUES (@fullname,@mobile,@gender,@address,@passport,@email)";
                cmd = new SqlCommand(query, con);
            }

            cmd.Parameters.AddWithValue("@fullname", txtFullname.Text);
            cmd.Parameters.AddWithValue("@mobile", txtMobile.Text);
            cmd.Parameters.AddWithValue("@gender", ddlGender.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@passport", txtPassport.Text);
            cmd.Parameters.AddWithValue("@email", txtEmail.Text);


            int rowAff = cmd.ExecuteNonQuery();
            con.Close();

            if (rowAff > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeModal", "closeModal();", true);
                if (!string.IsNullOrEmpty(hiddenStatus.Value))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastSuccess", "successToast('Chỉnh sửa thành công!');", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastSuccess", "successToast();", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeModal", "closeModal();", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastFailed", "failedToast();", true);
            }
            getGuest();
            ClearText();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            int result = Int32.Parse(id);
            if (e.CommandName == "DeleteRow")
            {
                deleteCommand(result);
            }
            if (e.CommandName == "EditRow")
            {
                hiddenStatus.Value = id;
                editCommand(result);
            }
        }


        protected void deleteCommand(int id)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "update guest set status=1 where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            getGuest();
        }

        protected void editCommand(int id)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "select * from guest where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                txtFullname.Text = dataReader["fullname"].ToString();
                txtEmail.Text = dataReader["email"].ToString();
                txtAddress.Text = dataReader["address"].ToString();
                txtMobile.Text = dataReader["mobile"].ToString();
                txtPassport.Text = dataReader["passport"].ToString();
                ddlGender.SelectedValue = dataReader["gender"].ToString();
            }
            dataReader.Close();
            con.Close();

            modalTitle.Text = "Chỉnh sửa Nguòi dùng";
            submitButton.Text = "Lưu";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", "openModal();", true);
        }

        public void ClearText()
        {
            hiddenStatus.Value = "";
            modalTitle.Text = "Thêm mới Người dùng";
            submitButton.Text = "Thêm mới";
            txtFullname.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txtPassport.Text = "";
            ddlGender.SelectedValue = "0";
        }

    }
}