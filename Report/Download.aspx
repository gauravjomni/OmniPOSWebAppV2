﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Download.aspx.cs" Inherits="Admin_Download" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="gvDownload" runat="server" AutoGenerateColumns="False" AllowPaging="False">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Description"/>
                <asp:BoundField DataField="Value" HeaderText="Amount in ($)" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
