<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/AddRestaurant.aspx.cs" Inherits="PosRestaurant.AddRestaurant"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
            
			<ul class="shortcut-buttons-set">
				<li><a class="shortcut-button" href="Restaurants.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					View Location(s)
				</span></a></li>
				
			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->
            
            
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Add Location"></asp:Literal></h3>
					
					<ul class="content-box-tabs">
						<li><a href="#Form" class="default-tab current">Location Details</a></li> <!-- href must be unique and match the id of target div -->
						<li><a href="#Header">Receipt Header</a></li>
						<li><a href="#Footer">Receipt Footer</a></li>
						<li><a href="#Settings">Settings</a></li>
					</ul>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					<div id="Form">
					
							<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
								<p>
									<label>Location Name</label>
										<input class="text-input large-input" id="txtRestName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdRestName" runat="server" ErrorMessage=" *" ControlToValidate="txtRestName"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblRestName" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
                                
								<p>
									<label>Initial</label>
										<input class="text-input x_small-input" id="txtInitial" type="text" runat="server" /> [ ez. Grant Hayat (GH) ] 
										<asp:RequiredFieldValidator ID="ReqdInitial" runat="server" ErrorMessage=" *" ControlToValidate="txtInitial"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblInitial" runat="server" ForeColor="Red" ></asp:Label></small>
								</p>                                
								
								<p>
									<label>Address1</label>
										<input class="text-input large-input" id="txtAddress1" type="text"  runat="server" /> 
										<asp:RequiredFieldValidator ID="ReqdAddress1" runat="server" ErrorMessage=" *" ControlToValidate="txtAddress1"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblAddress1" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
                                    
								<p>
									<label>Address2</label>
										<input class="text-input large-input" id="txtAddress2" type="text"  runat="server" /> 
								</p>
                                    
								<p>
									<label>City</label>
										<input class="text-input small-input" id="txtCity" type="text"  runat="server" /> 
										<asp:RequiredFieldValidator ID="ReqdCity" runat="server" ErrorMessage=" *" ControlToValidate="txtCity"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblCity" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>State</label>
										<%--<input class="text-input small-input" id="txtState" type="text"  runat="server" /> --%>
										<asp:DropDownList ID="State" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
										<asp:RequiredFieldValidator ID="ReqdState" runat="server" ErrorMessage=" *" ControlToValidate="State"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblState" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>Zip</label>
										<input class="text-input small-input" id="txtZip" type="text"  runat="server" /> 
										<asp:RequiredFieldValidator ID="ReqdZip" runat="server" ErrorMessage=" *" ControlToValidate="txtZip"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblZip" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>Phone</label>
										<input class="text-input small-input" id="txtPhone" type="text"  runat="server" />
										<asp:RequiredFieldValidator ID="ReqdPhone" runat="server" ErrorMessage=" *" ControlToValidate="txtPhone"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblPhone" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>Fax</label>
										<input class="text-input small-input" id="txtFax" type="text"  runat="server" /> 
								</p>

								<p>
									<label>Email Address</label>
										<input class="text-input large-input" id="txtEmail" type="text"  runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdEmail" runat="server" ErrorMessage=" *" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ErrorMessage=" Please Enter Valid Email ID" ControlToValidate="txtEmail" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
										<br /><small><asp:Label ID="LblEmail" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								
								<p>
									<label>Website</label>
										<input class="text-input large-input" id="TxtWebsite" type="text" runat="server" />
								</p>

								<p>
									<label>Table Count</label>
										<input class="text-input x_small-input" id="txtTableCount" type="text" runat="server" />
										<asp:RequiredFieldValidator ID="ReqdTableCount" runat="server" ErrorMessage=" *" ControlToValidate="txtTableCount"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegExpValid" runat="server" ErrorMessage="Please specify a digit" ControlToValidate="txtTableCount" ValidationExpression="\d+"></asp:RegularExpressionValidator>
										<br /><small><asp:Label ID="LblTableCount" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

<%--								<p>
									<label>Header</label>
										<input class="text-input large-input" id="txtHeader" type="text"  runat="server" /> 
								</p>
--%>

								<p>
									<label>View Options</label>
									    <input type="checkbox" id="KitchenView" runat="server" />Kitchen View&nbsp;&nbsp;&nbsp;&nbsp; <input type="checkbox" id="ExpediteView" runat="server" />Expedite View
								</p>
                                       
								<p>
									<label>Tax</label>
									    <input type="checkbox" id="ChkTax" runat="server" />
								</p>

								<p>
									<label>Active</label><input id ="Status" type="checkbox" runat="server" value="1" />
								</p>
                                       
								
							</fieldset>
							
							<div class="clear"></div><!-- End .clear -->
							
						
					</div> <!-- End #tab2 -->        
					<div style="display: none;" class="tab-content" id="Header">
                        <p>
                            <label>Name</label>
							<input class="text-input medium-input" id="txtHeaderName" type="text"  runat="server" />
                        </p>

                        <p>
                            <label>Address1</label>
							<input class="text-input medium-input" id="txtHeaderAddress1" type="text"  runat="server" />
                        </p>
                        
                        <p>
                            <label>City</label>
							<input class="text-input small-input" id="txtHeadercity" type="text"  runat="server" />
                        </p>

                        <p>
                            <label>State</label>
							<input class="text-input small-input" id="txtHeaderState" type="text"  runat="server" />
                        </p>

                        <p>
                            <label>Zip</label>
							<input class="text-input small-input" id="txtHeaderZip" type="text"  runat="server" />
                        </p>

                        <p>
                            <label>Phone</label>
							<input class="text-input small-input" id="txtHeaderPhone" type="text"  runat="server" />
                        </p>

                        <p>
                            <label>ABN</label>
							<input class="text-input medium-input" id="txtHeaderABN" type="text"  runat="server" />
                        </p>
                        
                        <p>
							<label>Tax Invoice</label><input id ="ChkTaxInvoice" type="checkbox" runat="server" value="1" />
                        </p>

                        <p>
                            <label>Website</label>
							<input class="text-input medium-input" id="txtHeaderWebsite" type="text"  runat="server" />
                        </p>

                        <p>
                            <label>Email</label>
							<input class="text-input medium-input" id="txtHeaderEmail" type="text"  runat="server" />
                            <%--<asp:RequiredFieldValidator ID="Reqd" runat="server" ErrorMessage=" *" ControlToValidate="txtHeaderEmail"></asp:RequiredFieldValidator>--%>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                            ErrorMessage=" Please Enter Valid Email ID" ControlToValidate="txtHeaderEmail" 
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </p>

                    </div>
                    
                    <div style="display: none;" class="tab-content" id="Footer">
						<p>
							<label>Footer1</label>
								<input class="text-input medium-input" id="txtFooter1" type="text"  runat="server" /> 
						</p>

						<p>
							<label>Footer2</label>
								<input class="text-input medium-input" id="txtFooter2" type="text"  runat="server" /> 
						</p>

                    </div>
                    
                    <div style="display: none;" class="tab-content" id="Settings">
						<p>
							<label>Customer View : </label>
								<input id ="Customer_View" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Add Lines between Order Items</label>
								<input id ="Add_Lines" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Sort course management by</label>
								<asp:DropDownList ID="Sort_Course_By" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>
						
						<p>
							<label>Print transferred orders on Kitchen Print</label>
								<input id ="Print_Transfer" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Print Deleted Orders on Kitchen Print</label>
								<input id ="Print_Deleted_Orders" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Print Voided Items on Kitchen Print</label>
								<input id ="Print_Voided_Items" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Kitchen View time-out interval (In Mins)</label>
								<asp:DropDownList ID="KitchenView_Timeout" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

						<p>
							<label>Allow Void for sent Order Items</label>
								<input id ="Allow_Void_Items" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Allow Delete for Sent Order</label>
								<input id ="Allow_Delete_Order" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Quick Service Title</label>
								<asp:DropDownList ID="Quick_Service" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

						<p>
							<label>Table layout type</label>
								<asp:DropDownList ID="Table_Layout_Type" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

						<p>
							<label>Auto Prompt Tip</label>
								<input id ="Auto_Prompt_Tip" type="checkbox" runat="server" value="1" />
						</p>

						<p>
							<label>Sort Items by</label>
								<asp:DropDownList ID="Sort_Items_By" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

						<p>
							<label>Sort Sub-Categories by</label>
								<asp:DropDownList ID="Sort_SubCat_By" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

						<p>
							<label>Sort Products By</label>
								<asp:DropDownList ID="Sort_Products" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

						<p>
							<label>Number of Devices for Multi device mode</label>
								<asp:DropDownList ID="No_Of_Devices" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

						<p>
							<label>Use Table Layout</label>
								<input id ="Table_Layout" type="checkbox" runat="server" value="1" />
						</p>

                        <p>
							<label>Email End-Of-Day (Z) Report</label>
								<input id ="Email_Z_Report" type="checkbox" runat="server" value="1" />
						</p>

                        <p>
							<label>No Sale Notification (Notification will be sent to Company Email Address)</label>
								<input id ="No_Sale_Notify" type="checkbox" runat="server" value="1" />
						</p>

                        <p>
							<label>No Sale Limit</label>
								<input class="text-input medium-input" id="NoSaleLimitText" type="text"  runat="server" /> 
						</p>

                        <p>
							<label>Enable Clock-In/Clock-Out</label>
								<input id ="EnableClockIn" type="checkbox" runat="server" value="1" />
						</p>

                        <p>
							<label>Scanner Mode</label>
								<asp:DropDownList ID="ScannerModeList" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>

                        <p>
							<label>Cash Drawer Balancing</label>
								<input id ="drawerBalancinCheckBoxg" type="checkbox" runat="server" value="1" />
						</p>

                        <p>
							<label>Hold And Fire</label>
								<asp:DropDownList ID="holdAndFireList" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
						</p>



                    </div>
				</div> <!-- End .content-box-content -->
				
						<p style="padding-left:20px">
                            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" onclick="BtnSave_Click" />
                            <a href="Restaurants.aspx" class="pageback">Back To Menu</a>
                                <asp:HiddenField ID="RestID" runat="server" Value ="-1" />
                                <asp:HiddenField ID="Mode" runat="server" Value ="add" />
						</p>

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