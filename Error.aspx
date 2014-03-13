<%@ Page Language="C#"  MasterPageFile="~/innerpage.master"%>
<%@ Register Src="~/usercontrols/header_Ctrl1.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />
            
            <div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">Error Box</h3>
					
					<%--<ul class="content-box-tabs">
						<li><a href="#tab1" class="default-tab current">Table</a></li> <!-- href must be unique and match the id of target div -->
						<li><a href="#tab2">Forms</a></li>
					</ul>--%>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
<%--                	<div style="display: block;" class="tab-content default-tab" id="tab1">
                	<p style="text-align:center"><h3>Welcome To The Administrative Panel.</h3></p>
                	</div>
					
--%>                	<div style="display: block;" class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
						
						<div class="notification attention png_bg">
							<%--<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>--%>
							<div><h3>You don't have the privilege to access the page.</h3></div>
						</div>
						
						
					</div> <!-- End #tab1 -->
					
				</div> <!-- End .content-box-content -->
				
			</div>
            
			<%--<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-content">
					
				   <h3> You don't have the privilege to access the page.</h3></div> <!-- End .content-box-content -->
				
			</div>--%> <!-- End .content-box -->
			


			
		</div>
</asp:Content>