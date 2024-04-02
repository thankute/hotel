<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="hotel.Dashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
/* The Modal (background) */
.panel {
  visibility: hidden;
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  left: 0;
  top: 400px;
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0,0,0); /* Fallback color */
  background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

.modal {
  display: none; /* Hidden by default */
  position: fixed; /* Stay in place */
  z-index: 1; /* Sit on top */
  left: 0;
  top: 0;
  width: 100%; /* Full width */
  height: 100%; /* Full height */
  overflow: auto; /* Enable scroll if needed */
  background-color: rgb(0,0,0); /* Fallback color */
  background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}

/* Modal Content/Box */
.modal-content {
  background-color: #fefefe;
  margin: 15% auto; /* 15% from the top and centered */
  padding: 20px;
  border: 1px solid #888;
  width: 80%; /* Could be more or less, depending on screen size */
}

/* The Close Button */
.close {
  color: #aaa;
  float: right;
  font-size: 28px;
  font-weight: bold;
}

.close:hover,
.close:focus {
  color: black;
  text-decoration: none;
  cursor: pointer;
}
        /* Modal Header */
.modal-header {
  padding: 2px 16px;
  background-color: #5cb85c;
  color: white;
}

/* Modal Body */
.modal-body {padding: 2px 16px;}

/* Modal Footer */
.modal-footer {
  padding: 2px 16px;
  background-color: #5cb85c;
  color: white;
}

/* Modal Content */
.modal-content {
  position: relative;
  background-color: #fefefe;
  margin: auto;
  padding: 0;
  border: 1px solid #888;
  width: 80%;
  box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
  animation-name: animatetop;
  animation-duration: 0.4s
}

/* Add Animation */
@keyframes animatetop {
  from {top: -300px; opacity: 0}
  to {top: 0; opacity: 1}
}
#snackbar {
  visibility: hidden; /* Hidden by default. Visible on click */
  min-width: 250px; /* Set a default minimum width */
  margin-left: -125px; /* Divide value of min-width by 2 */
  background-color: #333; /* Black background color */
  color: #fff; /* White text color */
  text-align: center; /* Centered text */
  border-radius: 2px; /* Rounded borders */
  padding: 16px; /* Padding */
  position: fixed; /* Sit on top of the screen */
  z-index: 1; /* Add a z-index if needed */
  right: 5%; /* Center the snackbar */
  top: 30px; /* 30px from the bottom */
}

/* Show the snackbar when clicking on a button (class added with JavaScript) */
#snackbar.show {
  visibility: visible; /* Show the snackbar */
  /* Add animation: Take 0.5 seconds to fade in and out the snackbar.
  However, delay the fade out process for 2.5 seconds */
  -webkit-animation: fadein 0.5s, fadeout 0.5s 2.5s;
  animation: fadein 0.5s, fadeout 0.5s 2.5s;
}

/* Animations to fade the snackbar in and out */
@-webkit-keyframes fadein {
  from {top: 0; opacity: 0;}
  to {top: 30px; opacity: 1;}
}

@keyframes fadein {
  from {top: 0; opacity: 0;}
  to {top: 30px; opacity: 1;}
}

@-webkit-keyframes fadeout {
  from {top: 30px; opacity: 1;}
  to {top: 0; opacity: 0;}
}

@keyframes fadeout {
  from {top: 30px; opacity: 1;}
  to {top: 0; opacity: 0;}
}
    </style>
    <script type="text/javascript">

        function myFunction() {
            // Get the snackbar DIV
            var x = document.getElementById("snackbar");

            // Add the "show" class to DIV
            x.className = "show";

            // After 3 seconds, remove the show class from DIV
            setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            This is Dashboard and is protected
        </div>
       <!-- Trigger/Open The Modal -->

        <asp:Button runat="server" Text="Logout" OnClick="onLogoutClick"/>

    <button id="myBtn" type="button">Open Modal</button>
    <asp:Button runat="server" ID="opBtn" OnClick="openPanel" Text="Open Panel"/>


<!-- The Modal -->
<div id="myModal" class="modal">

  <!-- Modal content -->
  <div class="modal-header">
    <span class="close">&times;</span>
    <h2>Modal Header</h2>
  </div>
  <div class="modal-body">
    <p>Some text in the Modal Body</p>
    <p>Some other text...</p>
  </div>
  <div class="modal-footer">
    <h3>Modal Footer</h3>
  </div>

</div>

   
        <asp:Panel ID="Panel1" runat="server" CssClass="panel" BorderStyle="Solid" Height="271px" style="margin-left: 451px; margin-right: 438px">
            This is hide show

            <asp:Button runat="server" ID="Button2" OnClick="closePanel" Text="Close Panel"/>
        </asp:Panel>

        <div id="snackbar">Some text some message..</div>




     <script>
         // Get the modal
         var modal = document.getElementsByClassName("panel")[0];


         // Get the button that opens the modal
         var btn = document.getElementById("myBtn");

         // Get the <span> element that closes the modal
         var span = document.getElementsByClassName("close")[0];

         console.log("aaa", modal);
         // When the user clicks on the button, open the modal
         btn.onclick = function () {
             modal.style.visibility = "visible";
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

         function myFunction() {
             // Get the snackbar DIV
             var x = document.getElementById("snackbar");

             // Add the "show" class to DIV
             x.className = "show";

             // After 3 seconds, remove the show class from DIV
             setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
         }


     </script>
        <asp:Repeater ID="Repeater1" runat="server">
        </asp:Repeater>
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="393px">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="name" HeaderText="Tên phòng" SortExpression="name" />
                <asp:ButtonField Text="Button" />
            </Columns>
        </asp:GridView>
        <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
            <asp:ListItem>Đang hoạt động</asp:ListItem>
            <asp:ListItem></asp:ListItem>
            <asp:ListItem></asp:ListItem>
        </asp:DropDownList>
    </form>




     </body>
</html>
