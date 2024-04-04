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

namespace hotel.src
{
    public partial class Booking : System.Web.UI.Page
    {
        CommonFnx fn = new CommonFnx();

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["hotelCS"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getBooking();
                getRoom();
                getGuest();
            }
        }

        public void getBooking()
        {
            DataTable dt = fn.Fetch("select booking.*, room.room_number, guest.fullname, guest.mobile from booking join room on room.ID = booking.room_id join guest on guest.ID = booking.guest_id");
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void getRoom()
        {
            DataTable dt = fn.Fetch("Select * from room where isDeleted=0");
            ddlRoom.DataSource = dt;
            ddlRoom.DataTextField = "room_number";
            ddlRoom.DataValueField = "ID";
            ddlRoom.DataBind();
            ddlRoom.Items.Insert(0, new ListItem("Chọn phòng", "0"));
        }

        protected void getGuest()
        {
            DataTable dt = fn.Fetch("Select * from guest where status=0");
            ddlGuest.DataSource = dt;
            ddlGuest.DataTextField = "fullname";
            ddlGuest.DataValueField = "ID";
            ddlGuest.DataBind();
            ddlGuest.Items.Insert(0, new ListItem("Chọn khách", "0"));
        }

        protected void onRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TableCell statusCell = e.Row.Cells[8];
                if (statusCell.Text == "0")
                {
                    statusCell.Text = "Chờ nhận phòng";
                    statusCell.ForeColor = Color.RoyalBlue;
                }
                else if (statusCell.Text == "1")
                {
                    statusCell.Text = "Đang sử dụng";
                    statusCell.ForeColor = Color.Green;
                }
                else if (statusCell.Text == "2")
                {
                    statusCell.Text = "Đã checkout";
                    statusCell.ForeColor = Color.Gray;
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
            submitButton.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", "openModal();", true);
        }


        protected void onRoomSelected(object sender, EventArgs e)
        {
           
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            string query = "select rt.base_price as price from room r join room_type rt on r.room_type_id = rt.ID where r.ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", ddlRoom.SelectedIndex.ToString());

            var price = (Int32)cmd.ExecuteScalar();
            con.Close();
            if(price != null)
            {
                txtTotalPrice.Text = price.ToString();
            }
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
                query = "update booking set room_id=@room_id, guest_id=@guest_id,checkin_date=@checkin_date,checkout_date=@checkout_date, num_adults=@num_adults,num_children=@num_children,status=@status,total_price=@total_price where ID=@id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
            }
            else
            {
                query = "insert into booking (room_id,guest_id,checkin_date,checkout_date, num_adults,num_children,status,total_price) VALUES (@room_id, @guest_id,@checkin_date, @checkout_date,@num_adults,@num_children,@status,@total_price)";
                cmd = new SqlCommand(query, con);
            }

            cmd.Parameters.AddWithValue("@room_id", ddlRoom.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@guest_id", ddlGuest.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@checkin_date", txtCheckin.Text);
            cmd.Parameters.AddWithValue("@checkout_date", txtCheckout.Text);
            cmd.Parameters.AddWithValue("@num_adults", txtNumAdults.Text);
            cmd.Parameters.AddWithValue("@num_children", txtNumChildren.Text);
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@total_price", txtTotalPrice.Text);

            int rowAff = cmd.ExecuteNonQuery();

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

                if(ddlStatus.SelectedValue.ToString() == "1")
                {
                    string roomQuery = "update room set status=1 where ID=@id";
                    SqlCommand roomCMD = new SqlCommand(roomQuery, con);
                    roomCMD.Parameters.AddWithValue("@id", ddlRoom.SelectedValue.ToString());
                    roomCMD.ExecuteNonQuery();
                }
                if (ddlStatus.SelectedValue.ToString() == "2")
                {
                    string roomQuery = "update room set status=2 where ID=@id";
                    SqlCommand roomCMD = new SqlCommand(roomQuery, con);
                    roomCMD.Parameters.AddWithValue("@id", ddlRoom.SelectedValue.ToString());
                    roomCMD.ExecuteNonQuery();
                }

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "closeModal", "closeModal();", true);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastFailed", "failedToast();", true);
            }
            con.Close();
            getBooking();
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
            string query = "delete from room_type where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            con.Close();
            getBooking();
        }

        protected void editCommand(int id)
        {

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            string query = "select * from booking where ID=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader dataReader = cmd.ExecuteReader();

            if (dataReader.Read())
            {
                if (dataReader["status"].ToString() == "2")
                {
                    ddlRoom.Enabled = false;
                    ddlGuest.Enabled = false;
                    txtCheckin.Enabled = false;
                    txtCheckout.Enabled = false;
                    txtNumAdults.Enabled = false;
                    txtNumChildren.Enabled = false;
                    ddlStatus.Enabled = false;
                    submitButton.Visible = false;
                } else
                {
                    ddlRoom.Enabled = true;
                    ddlGuest.Enabled = true;
                    txtCheckin.Enabled = true;
                    txtCheckout.Enabled = true;
                    txtNumAdults.Enabled = true;
                    txtNumChildren.Enabled = true;
                    ddlStatus.Enabled = true;
                    submitButton.Visible = true;
                }
                ddlRoom.SelectedValue = dataReader["room_id"].ToString();
                ddlGuest.SelectedValue = dataReader["guest_id"].ToString();
                txtCheckin.Text = Convert.ToDateTime(dataReader["checkin_date"]).ToString("yyyy-MM-dd");
                txtCheckout.Text = Convert.ToDateTime(dataReader["checkout_date"]).ToString("yyyy-MM-dd");
                txtNumAdults.Text = dataReader["num_adults"].ToString();
                txtNumChildren.Text = dataReader["num_children"].ToString();
                ddlStatus.SelectedValue = dataReader["status"].ToString();
                txtTotalPrice.Text = dataReader["total_price"].ToString();
            }
            dataReader.Close();
            con.Close();

            modalTitle.Text = "Chỉnh sửa Đơn đặt phòng";
            submitButton.Text = "Lưu";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "OpenModalScript", "openModal();", true);
        }

        protected void ClearText()
        {
            modalTitle.Text = "Thêm mới Đơn đặt Phòng";
            submitButton.Text = "Thêm mới";
            ddlRoom.SelectedValue = "0";
            ddlGuest.SelectedValue = "0";
            txtCheckin.Text = "";
            txtCheckout.Text = "";
            txtNumAdults.Text = "";
            txtNumChildren.Text = "";
            ddlStatus.SelectedValue = "0";
            txtTotalPrice.Text = "";
            ddlRoom.Enabled = true;
            ddlGuest.Enabled = true;
            txtCheckin.Enabled = true;
            txtCheckout.Enabled = true;
            txtNumAdults.Enabled = true;
            txtNumChildren.Enabled = true;
            ddlStatus.Enabled = true;
            submitButton.Visible = true;
        }


    }
}