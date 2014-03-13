<%@ control language="C#" debug="true" autoeventwireup="true" CodeFile="header_Ctrl.ascx.cs" inherits="UserControls.header_ctrl" %>
<h2><asp:Label ID="LabelCompany" runat="server" Text="" ForeColor="#039EB9" ></asp:Label></h2>

<div style="float:left; position:absolute; top:10px; left:75%"><h3>Welcome { <asp:Label ID="Header_LoggedUserName" runat="server" 
        ForeColor="#6D7E47"></asp:Label>  &nbsp;} </h3></div>
<p></p>
<h3><p><asp:Label ID="Lbl_Selected_Restaurant" runat="server" CssClass="success1"></asp:Label></p></h3><h4><asp:Label ID="LblRestInfo" runat="server" CssClass="success2"></asp:Label></h4>
<br />
<%--<p id="page-intro">What would you like to do?</p>--%>