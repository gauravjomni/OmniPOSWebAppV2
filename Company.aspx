<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/Company.aspx.cs" Inherits="PosCompany.CompanySettings"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
            
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Company Profile"></asp:Literal></h3>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					<div id="Form">
					
							<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
								
								<p>
									<label>Company Name</label>
										<input class="text-input small-input" id="txtCompanyName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdCompName" runat="server" ErrorMessage=" *" ControlToValidate="txtCompanyName"></asp:RequiredFieldValidator>
								</p>
                                
								<p>
									<label>Address</label>
										<input class="text-input small-input" id="txtAddress" type="text" runat="server"/> 
                                        <asp:RequiredFieldValidator ID="ReqdAddress" runat="server" ErrorMessage=" *" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
								</p>                                
								
								<p>
									<label>Email</label>
										<input class="text-input small-input" id="txtEmail" type="text" runat="server"/> 
                                        <asp:RequiredFieldValidator ID="ReqdEmail" runat="server" ErrorMessage=" *" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ErrorMessage=" Please Enter Valid Email ID" ControlToValidate="txtEmail" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

								</p>                                

								<p>
									<label>Phone</label>
										<input class="text-input small-input" id="txtPhone" type="text" runat="server"/> 
                                        <asp:RequiredFieldValidator ID="ReqdPhone" runat="server" ErrorMessage=" *" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
								</p>

								<p>
									<label>Fax</label>
										<input class="text-input small-input" id="txtFax" type="text" runat="server"/> 
								</p>

								<p>
									<label>ABN Number</label>
										<input class="text-input small-input" id="txtABNNo" type="text" runat="server"/> 
								</p>
								
								<p>
									<label>Tax Rate</label>
										<input class="text-input x_small-input" id="txtRate" type="text" runat="server"/> 
                                        <asp:RequiredFieldValidator ID="ReqdTax" runat="server" ErrorMessage=" *" ControlToValidate="txtRate"></asp:RequiredFieldValidator>
								</p>

								<p>
									<label>Currency (In Symbol)</label>
										<input class="text-input x_small-input" id="txtCurrency" type="text" runat="server"/> 
                                        <asp:RequiredFieldValidator ID="RequdCurrency" runat="server" ErrorMessage=" *" ControlToValidate="txtCurrency"></asp:RequiredFieldValidator>
								</p>

								<p>
									<label>Date Format</label>
                                    <asp:DropDownList ID="DDDtFmt" runat="server">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="103">dd/MM/yyyy</asp:ListItem>
                                        <asp:ListItem Value="101">MM/dd/yyyy</asp:ListItem>
                                    </asp:DropDownList>
										<asp:RequiredFieldValidator ID="ReqdDateFormat" runat="server" ErrorMessage=" *" ControlToValidate="DDDtFmt"></asp:RequiredFieldValidator>
								</p>                                

								<p>
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" onclick="BtnSave_Click"   />
                                    <asp:HiddenField ID="RestInitial" runat="server" />
                                    <asp:HiddenField ID="Mode" runat="server" />
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