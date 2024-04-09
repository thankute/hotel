<%@ Page Title="" Language="C#" MasterPageFile="~/CMSLayout.Master" AutoEventWireup="true" CodeBehind="RoomType.aspx.cs" Inherits="hotel.src.RoomType" %>

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
                    <asp:Label runat="server" Text="Loại phòng"></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" placeholder="Tìm theo loại phòng"></asp:TextBox>
                </div>
            </div>
            <div class="center">
                <asp:Button ID="Button1" runat="server" Text="Xoá điều kiện" CssClass="btn"/>
                <asp:Button ID="Button2" runat="server" Text="Tìm kiếm" CssClass="btn search-btn"/>
            </div>

        </div>

        <div class="add-container">
            <asp:Button CssClass="btn add-btn" runat="server" Text="Thêm mới" ID="addBtn" OnClick="AddBtnClick" />
        </div>

        <%-- table --%>
        <div class="table-container">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="name" HeaderText="Hạng Phòng">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:BoundField DataField="base_price" HeaderText="Giá gốc">
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    </asp:BoundField>
                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="sua" CommandArgument='<%# Eval("ID") %>' CommandName="EditRow" CssClass="edit-btn" runat="server">Sửa</asp:LinkButton> 
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="">
                         <ItemTemplate>
                             <asp:LinkButton ID="xoa" CommandArgument='<%# Eval("ID") %>' CommandName="DeleteRow" OnClientClick="return confirm('Bạn có chắc chắn muốn xoá loại phòng này?')" CssClass="delete-btn" runat="server">Xoá</asp:LinkButton> 
                         </ItemTemplate>
                     </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <!-- The Modal -->
    <div id="myModal" class="modal">

        <div class="modal-content">
            <asp:HiddenField ID="hiddenStatus" runat="server"/>
            <!-- Modal content -->
            <div class="modal-header between">
                <asp:Label ID="modalTitle" runat="server" Text="Thêm mới Loại phòng" Font-Size="Large"></asp:Label>
                <span class="close">&times;</span>
            </div>
            <div class="modal-body flex flex-col">
                <label>Loại phòng</label>
                <asp:TextBox ID="txtRoomType" placeholder="Nhập loại phòng" runat="server" ClientIDMode="Static"/>
                <p id="errorRoomType" class="error"></p>

                <label style="margin-top: 10px">Giá phòng</label>
                <asp:TextBox ID="txtBasePrice" placeholder="Nhập giá phòng" runat="server" TextMode="Number" ClientIDMode="Static" />
                <p id="errorPrice" class="error"></p>


            </div>
            <div class="modal-footer">
                <button type="button" class="btn" style="margin-right: 6px" onclick="closeModal()">Đóng</button>
                 <asp:Button CssClass="btn add-btn" runat="server" Text="Thêm" ID="submitButton" OnClick="onSubmit" OnClientClick="return validate();" />
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
            const txtRoomType = document.getElementById("txtRoomType");
            const txtBasePrice = document.getElementById("txtBasePrice");

            const errorRoomType = document.getElementById("errorRoomType");
            const errorPrice= document.getElementById("errorPrice");

            let bool = true;

            if (!txtRoomType.value.trim()) {
                errorRoomType.innerHTML = "Vui lòng nhập loại phòng!";
                bool = false;
            }
            if (!txtBasePrice.value.trim()) {
                errorPrice.innerHTML = "Vui lòng nhập giá phòng!";
                bool = false;
            }
            return bool;
        }


    </script>
     <div id="toastSuccess" class="snackbar" style="background-color:forestgreen; color: white">Thêm mới thành công</div>
     <div id="toastFailed" class="snackbar" style="background-color: red; color: white">Đã có lỗi xảy ra! Vui lòng thử lại!</div>
</asp:Content>
