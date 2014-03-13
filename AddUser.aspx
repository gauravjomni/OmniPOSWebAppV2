<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/AddUser.aspx.cs" Inherits="PosUser.AddUser"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
            
			<ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="Users.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					View User(s)
				</span></a></li>
				
			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->
	
	<script type="text/javascript">
	    $(function() {
	        $("#txtJoinDate").datepicker();
	    });
	</script>
	
	
			            
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Add User"></asp:Literal></h3>
					
					<div class="clear">
                        <asp:ScriptManager ID="ScriptManager1" runat="server">
                        </asp:ScriptManager>
                    </div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					<div id="Form">

							<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
								<p>
									<label>First Name</label>
										<input class="text-input small-input" id="txtFirstName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdFirstName" runat="server" ErrorMessage=" *" ControlToValidate="txtFirstName"></asp:RequiredFieldValidator>
<%--										<br /><small><asp:Label ID="LblName" runat="server" ForeColor="Red"></asp:Label></small>
--%>								</p>
                                
								<p>
									<label>Last Name</label>
										<input class="text-input small-input" id="txtLastName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdLastName" runat="server" ErrorMessage=" *" ControlToValidate="txtLastName"></asp:RequiredFieldValidator>
<%--										<br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></small>
--%>								</p>

								<p>
									<label>User Name</label>
										<input class="text-input small-input" id="txtName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdName" runat="server" ErrorMessage=" *" ControlToValidate="txtName"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblName" runat="server" ForeColor="Red"></asp:Label></small>
								</p>                                

								<p>
									<label>User Login Name</label>
										<input class="text-input small-input" id="txtUserName" type="text" runat="server" readonly /> 
										<br /><small><asp:Label ID="LblUserName" runat="server" ForeColor="Red"></asp:Label></small>
								</p>                                
								
								<p>
									<label>User Pin</label>
										<input class="text-input small-input" id="txtUserPin" type="text" runat="server" /> 
										<asp:RequiredFieldValidator ID="ReqdUserPin" runat="server" ErrorMessage=" *" ControlToValidate="txtUserPin"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblUserPin" runat="server" ForeColor="Red"></asp:Label></small>
								</p>                                

								<p>
									<label>Password</label>
										<input class="text-input small-input" id="txtPassword" name="txtPassword" type="password"  runat="server" /> 
										<asp:RequiredFieldValidator ID="ReqdPassword" runat="server" ErrorMessage=" *" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblPassword" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
                                    
								<p>
									<label>Password (Again)</label>
										<input class="text-input small-input" id="txtPasswordConf" name="txtPasswordConf" type="password"  runat="server" /> 
										<asp:RequiredFieldValidator ID="ReqdtxtPasswordConf" runat="server" ErrorMessage=" *" ControlToValidate="txtPasswordConf"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblPasswordConf" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
                                   
								<p>
									<label>Phone</label>
										<input class="text-input small-input" id="txtPhone" type="text" runat="server" /> 
										<asp:RequiredFieldValidator ID="RequdPhone" runat="server" ErrorMessage=" *" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
								</p>                                
                                    
								<p>
									<label>Email Address</label>
										<input class="text-input medium-input" id="txtEmail" name="txtEmail" type="text"  runat="server" /> 
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ErrorMessage=" Please Enter Valid Email ID" ControlToValidate="txtEmail" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
										<br /><small><asp:Label ID="LblEmail" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>Joining Date</label>
                                      <div>
<%--                                      <asp:TextBox ID="txtJoinDate" runat="server" ></asp:TextBox>
                                      <asp:Image ID="Image1" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png" />
                                      <asp:RequiredFieldValidator ID="ReqdJoinDate" runat="server" ErrorMessage=" *" ControlToValidate="txtJoinDate">
                                      </asp:RequiredFieldValidator>
                                      <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtJoinDate" PopupButtonID="Image1" CssClass="cal_Theme1" >
                                          </ajaxToolkit:CalendarExtender>
--%>                                        <input type="text" id="txtJoinDate" name="txtJoinDate" value="<%= strjoindate %>"  readonly />
                                            <br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>
                                    </div>
                                     <br /><small><asp:Label ID="LblDate" runat="server" ForeColor="Red" ></asp:Label></small>
								</p>  
                                
                                 <p>
									<label>Hourly Rate ($)</label>
										<input class="text-input small-input" id="txtHourlyRate" type="text" runat="server" /> 
								</p>                              
                                       
								<p>
									<label>Active</label><input id ="Status" type="checkbox" runat="server" value="1" />
								</p>
                                       
                                 <p>
									<label>Group:</label>
									<asp:DropDownList ID="UserGroup" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
										<br /><small><asp:Label ID="LblGrp" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
                                
								<p>
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" onclick="BtnSave_Click"   />
                                    <a href="Users.aspx" class="pageback">Back To Menu</a>
                                        <asp:HiddenField ID="UserID" runat="server" Value ="-1" />
                                        <asp:HiddenField ID="Mode" runat="server" Value ="add" />
                                        <asp:HiddenField ID="RestInitial" runat="server"  />
								</p>
								
							</fieldset>
							
							<div class="clear"></div><!-- End .clear -->
							
						
					</div> <!-- End #tab2 -->        
					
				</div> <!-- End .content-box-content -->
				
			</div> <!-- End .content-box -->
			
			<%--<div class="content-box column-left">
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">Content box left</h3>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					
					<div style="display: block;" class="tab-content default-tab">
					
						<h4>Maecenas dignissim</h4>
						<p>
						Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in 
porta lectus. Maecenas dignissim enim quis ipsum mattis aliquet. 
Maecenas id velit et elit gravida bibendum. Duis nec rutrum lorem. Donec
 egestas metus a risus euismod ultricies. Maecenas lacinia orci at neque
 commodo commodo.
						</p>
						
					</div> <!-- End #tab3 -->        
					
				</div> <!-- End .content-box-content -->
				
			</div>--%> <!-- End .content-box -->
			
            <%--			<div class="content-box column-right closed-box">
				
				<div class="content-box-header"> <!-- Add the class "closed" to the Content box header to have it closed by default -->
					
					<h3 style="cursor: s-resize;">Content box right</h3>
					
				</div> <!-- End .content-box-header -->
				
				<div style="display: none;" class="content-box-content">
					
					<div style="display: block;" class="tab-content default-tab">
					
						<h4>This box is closed by default</h4>
						<p>
						Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed in 
porta lectus. Maecenas dignissim enim quis ipsum mattis aliquet. 
Maecenas id velit et elit gravida bibendum. Duis nec rutrum lorem. Donec
 egestas metus a risus euismod ultricies. Maecenas lacinia orci at neque
 commodo commodo.
						</p>
						
					</div> <!-- End #tab3 -->        
					
				</div> <!-- End .content-box-content -->
				
			</div> <!-- End .content-box -->
			<div class="clear"></div>--%>
			
			
			<!-- Start Notifications -->
			
			<%--<div class="notification attention png_bg">
				<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
				<div>
					Attention notification. Lorem ipsum dolor sit amet, consectetur 
adipiscing elit. Proin vulputate, sapien quis fermentum luctus, libero.				</div>
			</div>--%>
			
            <%--<div class="notification information png_bg">
				<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
				<div>
					Information notification. Lorem ipsum dolor sit amet, consectetur 
adipiscing elit. Proin vulputate, sapien quis fermentum luctus, libero.				</div>
			</div>--%>
			
            <%--<div class="notification success png_bg">
				<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
				<div>
					Success notification. Lorem ipsum dolor sit amet, consectetur 
adipiscing elit. Proin vulputate, sapien quis fermentum luctus, libero.				</div>
			</div>--%>
			
                    
			
      <!-- End Notifications -->
			
			<%--<div id="footer">
				<small> <!-- Remove this notice or replace it with whatever you want -->
						© Copyright 2009 Your Company | Powered by <a href="http://themeforest.net/item/simpla-admin-flexible-user-friendly-admin-skin/46073">Omnipos Admin</a> | <a href="#">Top</a>
				</small>
			</div>--%><!-- End #footer -->
			
		</div>

</asp:Content>