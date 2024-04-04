<%@ Page Title="" Language="C#" MasterPageFile="~/CMSLayout.Master" AutoEventWireup="true" CodeBehind="Booking.aspx.cs" Inherits="hotel.src.Booking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function successToast(message) {
            var x = document.getElementById("toastSuccess");
            x.className += " show";
            x.innerHTML = message || "Thêm mới thành công!";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        }
        function failedToast() {
            var x = document.getElementById("toastFailed");
            x.className += " show";
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content_container">
        <%-- search --%>
        <div class="search">
            <div class="search-content">
                <div class="search-col flex flex-col">
                    <asp:Label runat="server" Text="Phòng"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Tìm theo phòng"></asp:TextBox>
                </div>
            </div>
            <div class="center">
                <asp:Button ID="Button1" runat="server" Text="Xoá điều kiện" CssClass="btn" />
                <asp:Button ID="Button2" runat="server" Text="Tìm kiếm" CssClass="btn search-btn" />
            </div>

        </div>

        <div class="add-container">
            <asp:Button CssClass="btn add-btn" runat="server" Text="Thêm mới" ID="addBtn" OnClick="AddBtnClick" />
        </div>

        <%-- table --%>
        <div class="table-container">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowDataBound="onRowDataBound">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="room_number" HeaderText="Số Phòng">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="fullname" HeaderText="Tên khách">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="mobile" HeaderText="SĐT khách">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="checkin_date" HeaderText="Ngày Checkin">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="checkout_date" HeaderText="Ngày Checkout">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="num_adults" HeaderText="Sô người lớn">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="num_children" HeaderText="Số trẻ nhỏ">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="status" HeaderText="Trạng thái" />
                    <asp:BoundField DataField="total_price" HeaderText="Giá" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="sua" CommandArgument='<%# Eval("ID") %>' CommandName="EditRow" CssClass="edit-btn" runat="server">Sửa</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="xoa" CommandArgument='<%# Eval("ID") %>' CommandName="DeleteRow" OnClientClick="return confirm('Bạn có chắc chắn muốn xoá loại phòng này?')" CssClass="delete-btn" runat="server">Xoá</asp:LinkButton>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- The Modal -->
    <div id="myModal" class="modal">

        <div class="modal-content">
            <asp:HiddenField ID="hiddenStatus" runat="server" />
            <!-- Modal content -->
            <div class="modal-header between">
                <asp:Label ID="modalTitle" runat="server" Text="Thêm mới Phòng" Font-Size="Large"></asp:Label>
                <span class="close">&times;</span>
            </div>
            <div class="modal-body flex flex-col">
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="flex flex-col">

                            <label>Chọn phòng</label>
                            <asp:DropDownList ID="ddlRoom" runat="server" AutoPostBack="true" CssClass="drop-select" OnSelectedIndexChanged="onRoomSelected">
                            </asp:DropDownList>


                            <label style="margin-top: 10px">Chọn người dùng</label>
                            <asp:DropDownList ID="ddlGuest" runat="server" CssClass="drop-select">
                            </asp:DropDownList>

                            <label style="margin-top: 10px">Thời gian checkin</label>
                            <asp:TextBox ID="txtCheckin" placeholder="Nhập thời gian checkin" TextMode="Date" runat="server" CssClass="date-select" />

                            <label style="margin-top: 10px">Thời gian checkout</label>
                            <asp:TextBox ID="txtCheckout" placeholder="Nhập thời gian checkout" CssClass="date-select" TextMode="Date" runat="server" />

                            <label style="margin-top: 10px">Số người lớn</label>
                            <asp:TextBox ID="txtNumAdults" placeholder="Nhập số lượng người lớn" TextMode="Number" runat="server" />
                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRoomType" ErrorMessage="Vui lòng nhập Loại Phòng"/>--%>

                            <label style="margin-top: 10px">Số trẻ nhỏ</label>
                            <asp:TextBox ID="txtNumChildren" placeholder="Nhập số trẻ nhỏ" TextMode="Number" runat="server" />

                            <label style="margin-top: 10px">Trạng thái</label>
                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="drop-select">
                                <asp:ListItem Value="0">Chờ nhận phòng</asp:ListItem>
                                <asp:ListItem Value="1">Đang hoạt động</asp:ListItem>
                                <asp:ListItem Value="2">Checkout</asp:ListItem>
                            </asp:DropDownList>

                            <label style="margin-top: 10px">Giá</label>
                            <asp:TextBox ID="txtTotalPrice" placeholder="Nhập giá phòng" runat="server" Enabled="false"/>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlRoom" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <div class="modal-footer">
                <button type="button" class="btn" style="margin-right: 6px" onclick="closeModal()">Đóng</button>
                <asp:Button CssClass="btn add-btn" runat="server" Text="Thêm" ID="submitButton" OnClick="onSubmit" />
            </div>
        </div>
    </div>
    <script>
        // Get the modal
        var modal = document.getElementById("myModal");

        // Get the <span> element that closes the modal
        var span = document.getElementsByClassName("close")[0];

        // When the user clicks on the button, open the modal
        function openModal() {
            modal.style.display = "block";
        }
        function closeModal() {
            modal.style.display = "none";
            PageMethods.ClearText();
        }
        // When the user clicks on <span> (x), close the modal
        span.onclick = function () {
            modal.style.display = "none";
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal.style.display = "none";
            }
        }
    </script>
    <div id="toastSuccess" class="snackbar" style="background-color: forestgreen; color: white">Thêm mới thành công</div>
    <div id="toastFailed" class="snackbar" style="background-color: red; color: white">Đã có lỗi xảy ra! Vui lòng thử lại!</div>
</asp:Content>
