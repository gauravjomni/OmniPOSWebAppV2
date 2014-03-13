<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/AddIngredient.aspx.cs" Inherits="PosIngredients.AddIngredient"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />
            
            <ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="Ingredients.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					View Ingredient(s)
				</span></a></li>
				
			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->

			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Add Ingredient"></asp:Literal></h3>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					<div id="Form">
					
							<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
								
								<p>
									<label>Ingredient Name</label>
										<input class="text-input small-input" id="txtIngredientName" name="txtIngredientName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdIngredientName" runat="server" ForeColor="Red" ErrorMessage=" *" ControlToValidate="txtIngredientName"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblIngredient" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								
								<p>
									<label>Price</label><input class="text-input x_small-input" id="txtPrice" name="txtPrice" type="text" runat="server" onkeypress='return myFunc(event);' /> 
                                        <asp:RequiredFieldValidator ID="ReqdPrice" runat="server" ErrorMessage=" *" ForeColor="Red"  ControlToValidate="txtPrice"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblPrice" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								
								<p>
									<label>Unit</label> 
									    <asp:DropDownList ID="Unit" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ReqdUnit" runat="server" ErrorMessage=" *" ForeColor="Red" ControlToValidate="Unit"></asp:RequiredFieldValidator>
								</p>
								<p>
									<label>Opening Qty</label><input class="text-input x_small-input" id="txtOpQty" name="txtOpQty" type="text" runat="server"  onkeypress='return myFunc(event);'/> 
									<br /><small><asp:Label ID="LblOpQty" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>ReOrder Qty</label><input class="text-input x_small-input" id="txtReOrdQty" name="txtReOrdQty" type="text" runat="server"  onkeypress='return myFunc(event);'/> 
									<asp:RequiredFieldValidator ID="Reqdreordqty" runat="server" ErrorMessage=" *" ControlToValidate="txtReOrdQty" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br /><small><asp:Label ID="LblReOrdQty" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								<p>
									<label>ReOrder Level</label><input class="text-input x_small-input" id="txtReOrdLvl" name="txtReOrdLvl" type="text" runat="server"  onkeypress='return myFunc(event);'/> 
									<asp:RequiredFieldValidator ID="Reqdordlvl" runat="server" ErrorMessage=" *" ControlToValidate="txtReOrdLvl" ForeColor="Red"></asp:RequiredFieldValidator>
                                    <br /><small><asp:Label ID="LblReOrdLvl" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								<p>
									<label>BarCode</label><input class="text-input x_small-input" id="txtBarCode" name="txtBarCode" type="text" runat="server" /> 
										<br /><small><asp:Label ID="LblBarCode" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>Supplier</label><asp:DropDownList ID="Supplier" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="ReqdSupplier" runat="server" ForeColor="Red" ErrorMessage=" *" ControlToValidate="Supplier"></asp:RequiredFieldValidator>
								</p>	

								<p>
									<label>Daily Item</label><input id ="DailyItem" type="checkbox" runat="server" value="1" />
								</p>
                                							
								<p>
									<label>Active</label><input id ="Status" type="checkbox" runat="server" value="1" />
								</p>
								
								<p>
                                    <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" 
                                        onclick="BtnSave_Click"/>
                                    <a href="Ingredients.aspx" class="pageback">Back To Menu</a>
                                        <asp:HiddenField ID="IngID" runat="server" Value ="-1" />
                                        <asp:HiddenField ID="Mode" runat="server" Value ="add" />
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
						© Copyright 2009 Your Company | Powered by <a href="">Omnipos Admin</a> | <a href="#">Top</a>
				</small>
			</div>--%><!-- End #footer -->
			
		</div>
</asp:Content>