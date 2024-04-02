using hotel.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hotel.src
{
    public partial class RoomType : System.Web.UI.Page
    {
        CommonFnx fn = new CommonFnx();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hotelCS"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

        protected void AddBtnClick(object sender, EventArgs e)
        {
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
                query = "update room_type set name=@name, base_price=@price where ID=@id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
            } else
            {
                query = "insert into room_type (name, base_price) VALUES (@name, @price)";
                cmd = new SqlCommand(query, con);
            }

            cmd.Parameters.AddWithValue("@name", txtRoomType.Text);
            cmd.Parameters.AddWithValue("@price", txtBasePrice.Text);

            int rowAff = cmd.ExecuteNonQuery();
            con.Close();

            if(rowAff > 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeModal", "closeModal();", true);
                if (!string.IsNullOrEmpty(hiddenStatus.Value))
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastSuccess", "successToast('Chỉnh sửa thành công!');", true);
                } else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastSuccess", "successToast();", true);
                }
            } else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeModal", "closeModal();", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastFailed", "failedToast();", true);
            }
            getRoomType();
            modalTitle.Text = "Thêm mới Loại Phòng";
            submitButton.Text = "Thêm mới";
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string id = e.CommandArgument.ToString();
            int result = Int32.Parse(id);
            if (e.CommandName == "DeleteRow")
            {
                deleteCommand(result);
            }
            if(e.CommandName == "EditRow")
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
            string query = "delete from room_type where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            getRoomType();
        }

        protected void editCommand(int id)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "select * from room_type where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if(dataReader.Read())
            {
                txtRoomType.Text = dataReader["name"].ToString();
                txtBasePrice.Text = dataReader["base_price"].ToString();

            }
            dataReader.Close();
            con.Close();

            modalTitle.Text = "Chỉnh sửa Loại Phòng";
            submitButton.Text = "Lưu";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", "openModal();", true);
        }

    }
}