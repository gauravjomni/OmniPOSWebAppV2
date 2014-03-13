<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/ProductClone.aspx.cs" Inherits="PosCloning.ProductClone" %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
            
			<ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="Products.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					View Product(s)
				</span></a></li>
				
			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->            
            
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Add Product"></asp:Literal></h3>
					
					<ul class="content-box-tabs">
						<li><a href="#Form" class="default-tab current">Product Details</a></li> <!-- href must be unique and match the id of target div -->
						<li><a href="#Image">Image</a></li>
						<li><a href="#Option">Modifiers</a></li>
						<li><a href="#Cooking">Cooking Options</a></li>
						<li><a href="#PrnKitchen">Printers/Kitchens</a></li>
					</ul>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					<div id="Form" style="display:block" class="tab-content default-tab">
							<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
                                
								<p>
									<label>Category</label>
										<asp:DropDownList ID="Category" runat="server" AutoPostBack="true" 
                                        CssClass="login-inp_lb" onselectedindexchanged="Category_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="ReqdCategory" runat="server" ErrorMessage=" *" ControlToValidate="Category"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblCategory" runat="server" ForeColor="Red"></asp:Label></small>
								</p>

								<p>
									<label>Sub Category</label> 
									    <asp:DropDownList ID="SubCategory" runat="server" AutoPostBack="false" 
                                            CssClass="login-inp_lb"></asp:DropDownList>
                                       <asp:RequiredFieldValidator ID="ReqdSubCategory" runat="server" ErrorMessage=" *" ControlToValidate="SubCategory"></asp:RequiredFieldValidator>
    								    <br /><small><asp:Label ID="LblSubCategory" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								
								<p>
									<label>Product Name</label>
										<input class="text-input small-input" id="txtProductName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdProductName" runat="server" ErrorMessage=" *" ControlToValidate="txtProductName"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblProductName" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
                                
								<p>
									<label>Name2</label>
										<input class="text-input small-input" id="txtProductName2" type="text" runat="server" /> 
										<br />
								</p>

								<p>
									<label>Description</label>
										<textarea class="text-input small-input" id="txtProductDesc" runat="server" /> 
										<br />
								</p>

								<p>
									<label>Color</label>
										<%--<input class="text-input small-input" id="txtColor" type="text" runat="server" /> --%>
                                        <div class="demoarea">
                                        <asp:TextBox runat="server" ID="txtColor" AutoCompleteType="None" MaxLength="6" style="float:left"  />
                                        <asp:ImageButton runat="Server" ID="Image1" style="float:left;margin:0 3px" ImageUrl="~/images/cp_button.png" AlternateText="Click to show color picker" CausesValidation="false" />
                                        <asp:Panel ID="Sample1" style="width:18px;height:18px;border:1px solid #000;margin:0 3px;float:left" runat="server" />
                                        <ajaxToolkit:ColorPickerExtender ID="buttonCPE" runat="server" TargetControlID="txtColor" PopupButtonID="Image1" SampleControlID="Sample1" OnClientColorSelectionChanged="colorChanged"  /> 
                                        </div>
										<br />
								</p>
								

								<p>
									<label>Price1</label>
										<input class="text-input x_small-input" id="txtPrice1" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdPrice1" runat="server" ErrorMessage=" *" ControlToValidate="txtPrice1"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblPrice1" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								
								<p>
									<label>Price2</label>
										<input class="text-input x_small-input" id="txtPrice2" type="text" runat="server" /> 
								</p>

								<p>
									<label>Open Price</label><input id ="ChkOpenPrice" type="checkbox" runat="server" value="1" />
								</p>

								<p>
									<label>GST</label><input id ="ChkGST" type="checkbox" runat="server" value="1" />
								</p>


								<p>
									<label>Stock In Hand</label>
										<input class="text-input x_small-input" id="txtInHand" type="text" readonly  runat="server" /> 
								</p>                                
								
								<p>
									<label>Change Price</label><input id ="ChkChangePrice" type="checkbox" runat="server" value="1" />
								</p>
								
								<p>
									<label>Course</label> 
									    <asp:DropDownList ID="Course" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
								</p>
								
								
								<p>
									<label>Sort Order</label>
										<input class="text-input xx_small-input" id="txtSort" name="txtSort" type="text" runat="server" value="0" /> 
                                        <asp:RequiredFieldValidator ID="ReqdSortOrder" runat="server" ErrorMessage=" *" ControlToValidate="txtSort"></asp:RequiredFieldValidator>
                                </p>
								
								
								<p>
									<label>Active</label><input id ="Status" type="checkbox" runat="server" value="1" />
								</p>
                                       
								
							</fieldset>
							
							<div class="clear"></div><!-- End .clear -->
							
						
					</div> <!-- End #tab2 -->        
					<div style="display: none;" class="tab-content" id="Image">
                        <p>
                            <label>Image</label>
                            <input class="text-input small-input" id="txtImage" type="file" runat="server" /> 
                            <br />
                        </p>
                        <p>
                            <asp:Image ID="Prdt_Img" runat="server" Height="200"  />
                        </p>

                    </div>
      
					<div style="display: none;" class="tab-content" id="Option">
                        <p>
                            <label>Choose Modifiers</label>
		 				 <table>
							
							<thead>
								<tr style="background-color:#000000; color:#ffffff">
								   <th><input class="check-all" type="checkbox"></th>
                                   <th>Modifier Name</th>
                                   <th>Price</th>
                                   <th>Modifier Level</th>                                   
								</tr>
							</thead>
						 
                          <asp:Repeater ID="ModfRepeater" runat="server" OnItemDataBound="Repeater_ItemDataBound">
							<HeaderTemplate>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class="alt-row">
									<td><input type="checkbox" runat="server" id="ChkModifier"  /></td>
									<td><%# DataBinder.Eval(Container.DataItem, "ModifierName")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Price1")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "ModifierLevelName")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td><input type="checkbox" runat="server" id="ChkModifier"></td>
									<td><%# DataBinder.Eval(Container.DataItem, "ModifierName")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Price1")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "ModifierLevelName")%></td>
								</tr>
							</AlternatingItemTemplate>		
							<FooterTemplate>
							</tbody>
							</FooterTemplate>	
                          </asp:Repeater>

						</table>
                            
                            
                        </p>

                    </div>
      
                    <div style="display: none;" class="tab-content" id="Cooking">
                        <p>
                            <label>Choose Cooking Options</label>
		 				 <table>
							
							<thead>
								<tr style="background-color:#000000; color:#ffffff">
								   <th><input class="check-all" type="checkbox"></th>
                                   <th>Cooking Option</th>
								</tr>
							</thead>
						 
                          <asp:Repeater ID="CookingRepeater" runat="server" OnItemDataBound="CookingRepeater_ItemDataBound">
							<HeaderTemplate>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class="alt-row">
									<td><input type="checkbox" runat="server" id="ChkCookingOpt"  /></td>
									<td><%# DataBinder.Eval(Container.DataItem, "OptionName")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td><input type="checkbox" runat="server" id="ChkCookingOpt"  /></td>
									<td><%# DataBinder.Eval(Container.DataItem, "OptionName")%></td>
								</tr>
							</AlternatingItemTemplate>		
							<FooterTemplate>
							</tbody>
							</FooterTemplate>	
                          </asp:Repeater>

						</table>
                            
                            
                        </p>

                    </div>
                    
                    <div style="display: none;" class="tab-content" id="PrnKitchen">
                        <p>
                            <label>Choose Printer</label>
                            <asp:ListBox ID="Printer" runat="server"  AutoPostBack="false" CssClass="login-inp_lb" SelectionMode="Multiple"></asp:ListBox>
<%--							<asp:DropDownList ID="Printer" runat="server" AutoPostBack="false" CssClass="login-inp_lb" ></asp:DropDownList>--%>
                            <asp:RequiredFieldValidator ID="ReqdPrinter" runat="server" ErrorMessage=" *" ControlToValidate="Printer"></asp:RequiredFieldValidator>
                        </p>
                        
                        <p>
                            <label>Choose Kitchen</label>
							<asp:DropDownList ID="Kitchen" runat="server" AutoPostBack="false" CssClass="login-inp_lb" ></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ReqdKitchen" runat="server" ErrorMessage=" *" ControlToValidate="Kitchen"></asp:RequiredFieldValidator>
                        </p>

                    </div>
                    

				</div> <!-- End .content-box-content -->
					<p style="padding-left:30px">
                        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" onclick="BtnSave_Click"   />
                        <a href="Products.aspx" class="pageback">Back To Menu</a>
                        <asp:HiddenField ID="ProductID" runat="server" Value ="-1" />
                        <asp:HiddenField ID="Product_Img" runat="server" Value ="" />
                        <asp:HiddenField ID="Mode" runat="server" Value ="add" />
                        <asp:HiddenField ID="RestInitial" runat="server"  />
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