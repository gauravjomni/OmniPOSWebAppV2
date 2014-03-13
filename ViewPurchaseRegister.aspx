<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/ViewPurchaseRegister.aspx.cs" Inherits="PosReport.ViewPurchaseRegister"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
			<div class="content-box"><!-- Start Content Box -->
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">View Purchase Register [ <%= Session["R_Name"].ToString()%> ]</h3>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->

	            <script type="text/javascript">
	                $(function() {
	                $("#txtFromDate").datepicker();
	                $("#txtTillDate").datepicker();
	                });

	            </script>
				
				<div class="content-box-content">
                    <div id="Form">
							<fieldset class="column-left"> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
                            <div id="forminput">
                                <table>
                                <tr>
   			                       <td>  <label>From Date</label></td>
                                    <td>  <label>To Date</label></td>
                                </tr>
                                <tr>
                                     <td>
                                     <input type="text" id="txtFromDate" name="txtFromDate" readonly value="<%=fromdater %>" />
                                      <br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>

                                     
<%--   			                            <asp:TextBox ID="txtTillDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image2" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png" />
                                        <asp:RequiredFieldValidator ID="ReqdTillDate" runat="server" ErrorMessage=" *" ControlToValidate="txtTillDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" PopupButtonID="Image2" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>

--%>             
                                    </td>
                                      <td>                    
                                     <input type="text" id="txtTillDate" name="txtTillDate" readonly value="<%= tilldater %>" />
                                      <br /><small><asp:Label ID="Label2" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>
    
        	                    </td>
                                     </tr>
                                    <tr>
                                        <td><b>Supplier :: 
                                            <asp:DropDownList ID="Supplier" runat="server"></asp:DropDownList></b>
                                        </td>
                                        <td><b>Item Type:: <asp:DropDownList ID="ProductType" runat="server">
                                            <asp:ListItem Value=""></asp:ListItem>
                                            <asp:ListItem Value="Ing">Ingredient</asp:ListItem>
                                            <asp:ListItem Value="Prod">Product</asp:ListItem>
                                            </asp:DropDownList></b></td>
                                    </tr>
                                     </table>
                                <p>
                                <asp:Button ID="BtnSave" runat="server" Text="View Report" CssClass="button" 
                                        onclick="BtnSave_Click" />&nbsp;
                                        
                                <asp:Button ID="BtnPrint" runat="server" Text="Print Report" CssClass="button" 
                                 Visible="false"  />
                                </p>
                            </div>
                            
							<div style="width:680px;font-size:12px;">
                                 <p>
					                <asp:Label ID="company" runat="server" Visible="false"></asp:Label>
					                </br>
					                <asp:Label ID="location" runat="server" Visible="false"></asp:Label>
					              </p>
    	                            <div style="width:680px; margin:2px;">
										<table style="font-size:12px;" id="list">
								            <thead>
									            <tr class="head">
                                                   <th style="font-size:12px;">Invoice#</th>
                                                   <th style="font-size:12px;">Inv Dt</th>								               
                                                   <th style="font-size:12px;">PO#</th>
                                                   <th style="font-size:12px;">Item Name</th>
                                                   <th style="font-size:12px;">Type</th>
                                                   <th style="font-size:12px;">Qty</th>
                                                   <th style="font-size:12px;">Unit Price</th>
                                                   <th style="font-size:12px;">Amount</th>
									            </tr>
	    	        						</thead>
    	        					    <asp:Repeater ID="PurchaseHistoryRepeater" runat="server" OnItemDataBound="PurchaseHistoryRepeater_ItemDataBound">
							            <HeaderTemplate>
							            <tbody>
							            </HeaderTemplate>
						            	<ItemTemplate>
								            <tr>
									            <td><%# DataBinder.Eval(Container.DataItem,"Tran_Code") %></td>
									            <td style="width:200px;"><%# String.Format("{0:dd/MM/yy hh:mm:ss tt}", DataBinder.Eval(Container.DataItem,"Tran_Date")) %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "PONo") %></td>
                                                <td style="width:300px;"><%# DataBinder.Eval(Container.DataItem, "ProductName") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "ProductType") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem,  String.Format("{0:n2}", "Qty")) %></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:n2}", "Unit_Price"))%></td>
                                                <td style="text-align:right;padding-right:25px;"><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:n2}", "TotalAmt"))%></td>
							            </tr>
    							        </ItemTemplate>
						            	<AlternatingItemTemplate>
								            <tr class="alt-row">
									            <td><%# DataBinder.Eval(Container.DataItem,"Tran_Code") %></td>
									            <td style="width:200px;"><%# String.Format("{0:dd/MM/yy hh:mm:ss tt}", DataBinder.Eval(Container.DataItem,"Tran_Date")) %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "PONo") %></td>
                                                <td style="width:300px;"><%# DataBinder.Eval(Container.DataItem, "ProductName") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "ProductType") %></td>
                                                <td><%# DataBinder.Eval(Container.DataItem,  String.Format("{0:n2}", "Qty")) %></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:n2}", "Unit_Price"))%></td>
                                                <td style="text-align:right;padding-right:25px;"><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:n2}", "TotalAmt"))%></td>
								            </tr>
							            </AlternatingItemTemplate>
                                        <FooterTemplate>
    	    						        <asp:Label ID="Footer" runat="server"  ForeColor="DarkRed" ></asp:Label>
            							</tbody>
			            				</FooterTemplate>	
                                        </asp:Repeater>                                        
						            </table>
                                  </div>  
								</div>	
                                    								
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