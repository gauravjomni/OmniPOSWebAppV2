<%@ Page Language="C#" CodeFile="signout.aspx.cs" Inherits="Signout.Default" Title="User Signout" EnableViewStateMac="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
<title>OmniPos</title>
<link rel="stylesheet" href="css/screen.css" type="text/css" media="screen" title="default" />
<script src="js/jquery/jquery.pngFix.pack.js" type="text/javascript"></script>
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
		<a href="index.html"><img src="images/logo.png" width="156" height="40" alt="" /></a>
	</div>
	<!-- end logo -->
	
	<div class="clear"></div>
	
	<!--  start loginbox ................................................................................. -->
	<div id="loginbox">
	
	<!--  start login-inner -->
	<div id="logout-inner">
	    <form id="logout" runat="server">
			<p class="message-logout"><br />
            You are logged out from the System.          </p>
			<p align="center">&nbsp;</p>
			<p align="center"  class="white-login">Click Here To <a href="Default.aspx"  class="yellow-login">[ Login Again ]</a></p>
      </form>
	</div>
 	<!--  end login-inner -->
	<div class="clear"></div>
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