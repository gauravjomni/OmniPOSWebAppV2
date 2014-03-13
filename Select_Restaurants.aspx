<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/Select_Restaurants.aspx.cs" Inherits="PosLocation.Select_Restaurants" EnableEventValidation="false" %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />
            			
			<div class="clear"></div> <!-- End .clear -->
			
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">Location(s)</h3>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					
					<div class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
						<div class="div_content1">
						  <table id="mytable1" cellspacing="0">	
                          <thead>
		                    <tr>
			                    <th>
                          <asp:Repeater ID="RestaurantRepeater" runat="server" OnItemDataBound="RestaurantRepeater_ItemDataBound"  >   
                          	<headertemplate>
                                <ul class="shortcut-buttons-set">
                            </headertemplate>
                          	<ItemTemplate>
	                                <li><a class="shortcut-button" id="link" runat="server" ><span>
										<%# DataBinder.Eval(Container.DataItem,"RestName") %><br />
										<asp:HiddenField ID="RestInitial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Initials").ToString().Trim() %>'  />
                                        <asp:HiddenField ID="RestID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Rest_ID").ToString().Trim() %>'  />
                                        <asp:HiddenField ID="Rest_Name" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"RestName").ToString().Trim() %>'  />
                                        <asp:HiddenField ID="Header_Address" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Header_Address1").ToString().Trim() %>'  />
                                        <asp:HiddenField ID="Header_ABN" runat="server" Value='<%# DataBinder.Eval(Container.DataItem,"Header_ABN").ToString().Trim() %>'  />                                        
                                        </span></a>
                                        <div style="text-align:center"><asp:Button ID="Select" runat="server" Text="Select" OnClick="Select_Click" CssClass="button" /></div>
                                        <br />
                                     </li>
                                        
                            </ItemTemplate>
                            <footertemplate>
								</ul>
                            </footertemplate>
						</asp:Repeater>
                        		</th>
                        	</tr>
                            </thead>
                        </table>
<%--                            <asp:Button ID="Restaurant" runat="server" Text="Select Restaurant" 
                                CssClass="button" onclick="Restaurant_Click" />
--%>                        </div>
					</div> <!-- End #tab1 -->
					
					
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