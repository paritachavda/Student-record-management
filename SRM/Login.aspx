<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" MasterPageFile="~/Site1.Master" Inherits="SRM.Login" %>





    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" Visible="false">
   
<div>
    <fieldset style="width:873px">
    <legend style="font-weight: 700">Login </legend>
    <table style="height: 364px; margin-left: 450px; width: 393px;">
    <tr>
    <td><strong>User Name: *&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </strong> </td><td>
        <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox><br />
        <asp:RequiredFieldValidator
            ID="rfvUserName" runat="server" ErrorMessage="Please enter username"
            Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
            ControlToValidate="txtUserName" style="font-weight: 700"></asp:RequiredFieldValidator></td>
    </tr>
     <tr>
    <td style="font-weight: 700">Password: *</td><td>
        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password"></asp:TextBox><br />
        <asp:RequiredFieldValidator
            ID="rfvPwd" runat="server" ErrorMessage="Please enter password"
            Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
             ControlToValidate="txtPwd" style="font-weight: 700"></asp:RequiredFieldValidator></td>
    </tr>
     <tr>
     <td>&nbsp;</td>
    <td>
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" style="font-weight: 700" BackColor="#0099FF" BorderColor="Black" Height="43px" Width="99px"
            /></td>
    </tr>
     <tr>
     <td>&nbsp;</td>
     <td>
         <asp:Label ID="lblStatus" runat="server" Text="" Font-Bold="true" ForeColor="#ff0066"></asp:Label>
         </td>
    </tr>
    </table>
    </fieldset>   
    </div>
  </asp:Content>     
    
    