<%@ control language="C#" debug="true" autoeventwireup="true" CodeFile="header_Ctrl1.ascx.cs" inherits="UserControls.header_ctrl" %>
<h1><asp:Label  ID="LabelCompany" runat="server" ForeColor="#FF0066" 
        style="text-align: center" ></asp:Label></h1>

<h2>Welcome [ <asp:Label ID="Header_LoggedUserName" runat="server"></asp:Label> ]</h2>
<p></p>
<h3><p><asp:Label ID="Lbl_Selected_Restaurant" runat="server" CssClass="success1"></asp:Label></p></h3>
<br />
<%--<p id="page-intro">What would you like to do?</p>--%>