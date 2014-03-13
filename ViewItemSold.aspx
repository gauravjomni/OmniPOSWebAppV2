<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/ViewItemSold.aspx.cs" Inherits="PosReport.ViewItemSold"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

            <div id="main-content"> <!-- Main Content Section with everything -->
			<uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
			<!-- Page Head -->

			
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
                    <h3 style="cursor: s-resize;">Individual Items Sold Report [ <%= Session["R_Name"].ToString()%> ]</h3>
					<!--<h3 style="cursor: s-resize;">Individual <asp:Literal ID="LblHead" runat="server" Text="Item Sold Report [ Cumulative ]"></asp:Literal></h3>-->
					
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
   			                            <%--<asp:TextBox ID="txtFromDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png"  />
                                        <asp:RequiredFieldValidator ID="ReqdFromDate" runat="server" ErrorMessage=" *" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" PopupButtonID="Image1" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>--%>
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
                                     </table>
                                <p>
                                <asp:Button ID="BtnSave" runat="server" Text="View Report" CssClass="button" 
                                        onclick="BtnSave_Click" />&nbsp;
                                <asp:Button ID="BtnPrint" runat="server" Text="Print Report" CssClass="button" 
                                 Visible="false" />
                                </p>
                                </div>



								<div  style="width:680px;" >
									<table>
							            <thead>
								            <tr style="background-color:#000000; color:#ffffff">
								               <td></td> <td></td><td></td><td></td><td></td><td align="center">Individual Items Sold Report</td>
								            </tr>
    	        						</thead>
    	        						
                          
						            </table>
									 <p style="table-layout:auto" align="center">
                <asp:Label ID="company" runat="server" Visible="false"></asp:Label>
                </br>
                <asp:Label ID="location" runat="server" Visible="false"></asp:Label>
               </p>
									
                                    <div id = "Product" runat="server" visible="false">
                                        <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="left" style="font-weight:bold"><label id="LblRepo" runat="server" align="center"></label></td>
                                       </tr>
                                      <tr>
                                            <td align="left" style="font-weight:bold">Product Sale Breakdown</td>
                                      </tr>
                                      <tr>
                                        <td align="left">
                                        <%--<div id = "Employees" runat="server" visible="false">--%>
                                            <table width="80%" border="0" cellspacing="0" cellpadding="0" style="border:1px solid #DDDDDD;">
                                          <tr style="background-color:Gray;" >
<%--                                            <td width="20%" align="center" style="color:White; font-weight:bolder;">Dt</td>--%>
                                            <td width="50%" align="left" style="color:White; font-weight:bolder;">Product Name</td>
                                            <td width="5%" align="center" style="color:White; font-weight:bolder;">Qty</td>
                                            <td width="5%" align="center" style="color:White; font-weight:bolder;">Amount</td>
                                          </tr>
                                          
                                          <asp:Repeater ID="ProductSaleInfoRepeater" runat="server" OnItemDataBound="ProductSaleInfoRepeater_ItemDataBound" >
                                          <ItemTemplate>
                                          <tr>
<%--                                            <td width="20%" align="center" style="color:Black;"><%# DataBinder.Eval(Container.DataItem,"TransactionDate")%></td>
--%>                                            <td width="50%" align="left" style="color:Black;"><%# DataBinder.Eval(Container.DataItem,"ProductName")%></td>
                                            <td width="5%" align="center" style="color:Black;">(<%# DataBinder.Eval(Container.DataItem,"Qty") %>)</td>
                                            <td width="5%" align="center" style="color:Black; font-style:italic;"><%# StrCurrency + DataBinder.Eval(Container.DataItem,"Amount")%></td>
                                          </tr>
                                          </ItemTemplate>
					            		  <FooterTemplate>
                                            <asp:Label ID="Footer" runat="server"  ForeColor="DarkRed" ></asp:Label>
            							  </tbody>
			            				  </FooterTemplate>	
                                          
                                          </asp:Repeater>
                                        </table>
                                        <%--</div> --%>
                                        </td>
                                    </tr>
                                    
                                    </table>
                                    </div>
                                    <div id = "ProductOption" runat="server" visible="false">
                                        <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="left" style="font-weight:bold"></td>
                                       </tr>
                                    
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
						� Copyright 2009 Your Company | Powered by <a href="http://themeforest.net/item/simpla-admin-flexible-user-friendly-admin-skin/46073">Omnipos Admin</a> | <a href="#">Top</a>
				</small>
			</div>--%><!-- End #footer -->
			
		</div>
</asp:Content>