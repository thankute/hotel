using hotel.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace hotel.src
{
    public partial class Room : System.Web.UI.Page
    {
        CommonFnx fn = new CommonFnx();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hotelCS"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getRoom();
                getRoomType();
            }
        }

        public void getRoom()
        {
            DataTable dt = fn.Fetch("select r.ID, r.room_number, r.room_name, r.description, r.status, rt.name AS room_type, rt.base_price as price from room r join room_type rt on r.room_type_id = rt.ID");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void getRoomType()
        {
            DataTable dt = fn.Fetch("Select * from room_type");
            ddlRoomType.DataSource = dt;
            ddlRoomType.DataTextField = "name";
            ddlRoomType.DataValueField = "ID";
            ddlRoomType.DataBind();
            ddlRoomType.Items.Insert(0, "Chọn loại phòng");
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
                query = "update room set room_number=@roomNumber, room_name=@roomName, description=@description, room_type_id=@roomTypeId, status=@status where ID=@id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
            }
            else
            {
                query = "insert into room(room_number, room_name, description, room_type_id, status) VALUES (@roomNumber, @roomName, @description, @roomTypeId, @status)";
                cmd = new SqlCommand(query, con);
            }

            cmd.Parameters.AddWithValue("@roomNumber", txtRoomType.Text);
            cmd.Parameters.AddWithValue("@roomName", txtBasePrice.Text);
            cmd.Parameters.AddWithValue("@description", txtBasePrice.Text);
            cmd.Parameters.AddWithValue("@roomTypeId", txtBasePrice.Text);
            cmd.Parameters.AddWithValue("@status", txtBasePrice.Text);


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
            getRoom();
            modalTitle.Text = "Thêm mới Phòng";
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
            string query = "delete from room where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            getRoom();
        }

        protected void editCommand(int id)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "select * from room where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                txtRoomType.Text = dataReader["name"].ToString();
                txtBasePrice.Text = dataReader["base_price"].ToString();
            }
            dataReader.Close();
            con.Close();

            modalTitle.Text = "Chỉnh sửa Phòng";
            submitButton.Text = "Lưu";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", "openModal();", true);
        }
    }
}