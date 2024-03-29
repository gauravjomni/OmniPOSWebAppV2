﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OmniLogin.aspx.cs" Inherits="Admin_OmniLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>OmniPos</title>
    <link rel="stylesheet" href="../css/screen.css" type="text/css" media="screen" title="default" />
    <!--  jquery core -->
    <script src="~/js/jquery/jquery-1.4.1.min.js" type="text/javascript"></script>
    <!-- Custom jquery scripts -->
    <script src="../js/jquery/custom_jquery.js" type="text/javascript"></script>
    <script src="../js/jquery/jquery.pngFix.pack.js" type="text/javascript"></script>
    <script type="text/javascript">
$(document).ready(function(){
$(document).pngFix( );
});
    </script>
</head>
<body id="login-bg">
    <!-- Start: login-holder -->
    <div id="login-holder">
        <!-- start logo -->
        <div id="logo-login">
            &nbsp;</div>
        <!-- end logo -->
        <div class="clear">
        </div>
        <!--  start loginbox ................................................................................. -->
        <div id="loginbox">
            &nbsp;<!--  start login-inner --><div id="login-inner">
                <form id="login" runat="server">
                <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                        <th colspan="3">
                              &nbsp;Admin Login
                        </th>
                    </tr>
                    <tr>
                        <th colspan="3">
                            <asp:Label CssClass="redtxt_validator" id="lblMsg" runat="server" ForeColor="Maroon" />
                        </th>
                    </tr>
                    <tr>
                        <th>
                            &nbsp;Login Name
                        
                        <td>
                            <input type="text" name="txtUserName" id="txtUserName" runat="server" class="login-inp" />
                        </td>
                        <td align="right">
                            <asp:RequiredFieldValidator ControlToValidate="txtUserName" Display="Static" ErrorMessage=" *"
                                runat="server" ID="vUserName" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            Password
                        </th>
                        <td>
                            <input type="password" name="txtUserPass" id="txtUserPass" runat="server" class="login-inp" />
                        </td>
                        <td align="right">
                            <asp:RequiredFieldValidator ControlToValidate="txtUserPass" Display="Static" ErrorMessage=" *"
                                runat="server" ID="vUserPass" />
                        </td>
                    </tr>
                  
                    <!--		<tr>
			<th></th>
			<td valign="top"><input type="checkbox" class="checkbox-size" id="login-check" /><label for="login-check">Remember me</label></td>
		</tr>
-->
                    <tr>
                        <th>
                        </th>
                        <td>
                            <asp:Button ID="CMDLogin" runat="server" Text="Button" onclick="CMDLogin_Click" CssClass="submit-login" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                </form>
            </div>
            <!--  end login-inner -->
            <div class="clear">
            </div>
            <!--<a href="" class="forgot-pwd">Forgot Password?</a>-->
        </div>
        <!--  end loginbox -->
        <!--  start forgotbox ................................................................................... -->
        <!--<div id="forgotbox">
		<div id="forgotbox-text">Please send us your email and we'll reset your password.</div>-->
        <!--  start forgot-inner -->
        <!--<div id="forgot-inner">
		<table border="0" cellpadding="0" cellspacing="0">
		<tr>
			<th>Email address:</th>
			<td><input type="text" value=""   class="login-inp" /></td>
		</tr>
		<tr>
			<th> </th>
			<td><input type="button" class="submit-login"  /></td>
		</tr>
		</table>
		</div>-->
        <!--  end forgot-inner -->
        <!--<div class="clear"></div>
		<a href="" class="back-login">Back to login</a>
	</div>-->
        <!--  end forgotbox -->
    </div>
    <!-- End: login-holder -->
</body>
</html>
