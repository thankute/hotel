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
using System.Drawing;
using System.Web.Services;

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
            DataTable dt = fn.Fetch("select r.ID, r.room_number, r.room_name, r.description, r.status, rt.name AS room_type, rt.base_price as price from room r join room_type rt on r.room_type_id = rt.ID where r.isDeleted=0");
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
            ddlRoomType.Items.Insert(0, new ListItem("Chọn loại phòng", "0"));
        }

        protected void onRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell = e.Row.Cells[5];
                if(statusCell.Text == "0")
                {
                    statusCell.Text = "Trống";
                    statusCell.ForeColor = Color.Green;
                } else if(statusCell.Text == "1")
                {
                    statusCell.Text = "Đang sử dụng";
                    statusCell.ForeColor = Color.Red;
                } else if(statusCell.Text == "2")
                {
                    statusCell.Text = "Đang dọn dẹp";
                    statusCell.ForeColor = Color.RoyalBlue;
                } else
                {
                    statusCell.Text = "-";
                }

            }
        }


        protected void searchSubmit(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "select r.ID, r.room_number, r.room_name, r.description, r.status, rt.name AS room_type, rt.base_price as price from room r join room_type rt on r.room_type_id = rt.ID where r.isDeleted=0 AND r.room_number LIKE @room_number";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@room_number", "%" + txtSearchRoomNumber.Text + "%" );
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            GridView1.DataSource = dt;
            GridView1.DataBind();
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
                    query = "update room set room_number=@roomNumber, room_name=@roomName, description=@description, room_type_id=@roomTypeId, status=@status where ID=@id";
                    cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", id);
                }
                else
                {
                    query = "insert into room(room_number, room_name, description, room_type_id, status) VALUES (@roomNumber, @roomName, @description, @roomTypeId, @status)";
                    cmd = new SqlCommand(query, con);
                }

                cmd.Parameters.AddWithValue("@roomNumber", txtRoomNumber.Text);
                cmd.Parameters.AddWithValue("@roomName", txtRoomName.Text);
                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@roomTypeId", ddlRoomType.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue.ToString());


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
            string query = "update room set isDeleted=1 where ID=@id";
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
                txtRoomNumber.Text = dataReader["room_number"].ToString();
                txtRoomName.Text = dataReader["room_name"].ToString();
                txtDescription.Text = dataReader["description"].ToString();
                ddlRoomType.SelectedValue = dataReader["room_type_id"].ToString();
                ddlStatus.SelectedValue = dataReader["status"].ToString();
            }
            dataReader.Close();
            con.Close();

            modalTitle.Text = "Chỉnh sửa Phòng";
            submitButton.Text = "Lưu";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", "openModal();", true);
        }

        public void ClearText()
        {
            hiddenStatus.Value = "";
            modalTitle.Text = "Thêm mới Phòng";
            submitButton.Text = "Thêm mới";
            txtRoomNumber.Text = "";
            txtRoomName.Text = "";
            txtDescription.Text = "";
            ddlRoomType.SelectedValue = "0";
            ddlStatus.SelectedValue = "0";

        }

    }
}