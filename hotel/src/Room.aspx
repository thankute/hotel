<%@ Page Title="" Language="C#" MasterPageFile="~/CMSLayout.Master" AutoEventWireup="true" CodeBehind="Room.aspx.cs" Inherits="hotel.src.Room" %>

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
                    <asp:TextBox ID="txtSearchRoomNumber" runat="server" placeholder="Tìm theo số phòng"></asp:TextBox>
                </div>
            </div>
            <div class="center">
                <asp:Button ID="Button1" runat="server" Text="Xoá điều kiện" CssClass="btn" />
                <asp:Button ID="searchBtn" OnClick="searchSubmit" runat="server" Text="Tìm kiếm" CssClass="btn search-btn" />
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
                    <asp:BoundField DataField="room_name" HeaderText="Tên phòng">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="description" HeaderText="Mô tả">
                        <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField DataField="room_type" HeaderText="Loại Phòng" />
                    <asp:BoundField DataField="status" HeaderText="Trạng thái" />
                    <asp:BoundField DataField="price" HeaderText="Giá" />
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="sua" CommandArgument='<%# Eval("ID") %>' CommandName="EditRow" CssClass="edit-btn" runat="server">Sửa</asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="xoa" CommandArgument='<%# Eval("ID") %>' CommandName="DeleteRow" OnClientClick="return confirm('Bạn có chắc chắn muốn xoá loại phòng này?')" CssClass="delete-btn" runat="server">Xoá</asp:LinkButton>
                        </ItemTemplate>

                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
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
                        <label>Số phòng</label>
                        <asp:TextBox ID="txtRoomNumber" placeholder="Nhập số phòng" runat="server" ClientIDMode="Static"/>
                        <p id="errorRoomNumber" class="error"></p>


                        <label style="margin-top: 10px">Tên phòng</label>
                        <asp:TextBox ID="txtRoomName" placeholder="Nhập tên phòng" runat="server" />


                        <label style="margin-top: 10px">Mô tả</label>
                        <asp:TextBox ID="txtDescription" placeholder="Nhập mô tả" runat="server" />
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBasePrice" ErrorMessage="Vui lòng nhập Giá phòng"/>--%>

                        <label style="margin-top: 10px">Loại phòng</label>
                        <asp:DropDownList ID="ddlRoomType" runat="server" CssClass="drop-select" ClientIDMode="Static">
                        </asp:DropDownList>
                        <p id="errorRoomType" class="error"></p>

                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Can loai phong" ControlToValidate="ddlRoomType" Display="Dynamic" InitialValue="Chọn loại phòng" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                        <label style="margin-top: 10px">Trạng thái</label>
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="drop-select">
                            <asp:ListItem Value="0">Trống</asp:ListItem>
                            <asp:ListItem Value="1">Đang được sử dụng</asp:ListItem>
                            <asp:ListItem Value="2">Đang dọn</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn" style="margin-right: 6px" onclick="closeModal()">Đóng</button>
                        <asp:Button CssClass="btn add-btn" runat="server" Text="Thêm" ID="submitButton" OnClick="onSubmit" OnClientClick="return validate();"/>
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

        function validate() {
            const txtRoomNumber = document.getElementById("txtRoomNumber");
            const ddlRoomType = document.getElementById("ddlRoomType");
            const errorRoomNumber = document.getElementById("errorRoomNumber");
            const errorRoomType = document.getElementById("errorRoomType");
            let bool = true;

            if (!txtRoomNumber.value.trim()) {
                errorRoomNumber.innerHTML = "Vui lòng nhập số phòng!"
                bool = false;
            }

            if (ddlRoomType.value == "0") {
                errorRoomType.innerHTML = "Vui lòng chọn loại phòng!"
                bool = false;
            }
           
            return bool;
        }

    </script>
    <div id="toastSuccess" class="snackbar" style="background-color: forestgreen; color: white">Thêm mới thành công</div>
    <div id="toastFailed" class="snackbar" style="background-color: red; color: white">Đã có lỗi xảy ra! Vui lòng thử lại!</div>
</asp:Content>
