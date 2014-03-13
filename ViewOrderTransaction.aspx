<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/ViewOrderTransaction.aspx.cs" Inherits="PosReport.ViewOrderTransacttion"  %>
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
					
					<h3 style="cursor: s-resize;">Live Transactions [ <%= Session["R_Name"].ToString()%> ]</h3>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->

	            <script type="text/javascript">
	                $(function() {
	                $("#txtFromDate").datepicker();
	                $("#txtTillDate").datepicker();
	                });

	            </script>
				
				<div class="content-box-content">
					<div style="display: block;" class="tab-content default-tab" id="tab1"> 
					
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
                                 Visible="false"  />
                                </p>

                                </div>
								<div style="width:1080px;font-size:12px;">
                                 <p style="table-layout:auto" align="center">
                <asp:Label ID="company" runat="server" Visible="false"></asp:Label>
                </br>
                <asp:Label ID="location" runat="server" Visible="false"></asp:Label>
               </p>
									<table style="font-size:12px;">
							            <thead>
								            <tr style="background-color:#000000; color:#ffffff;">
								               <th style="font-size:12px;">ID</th>
								               <th style="font-size:12px;">Date</th>
								               <th style="font-size:12px;">Order #</th>
                                               <%--
								               <th style="font-size:12px;">Order Date</th>
                                               --%>

								               <th style="font-size:12px;">Gross</th>
								               <th style="font-size:12px;">GST</th>
								               <th style="font-size:12px;">Net</th>

                                               <%--
								               <th style="font-size:12px;">Tip</th>
                                               --%>

								               <th style="font-size:12px;">Surcharge</th>
								               <th style="font-size:12px;">Discount</th>
								               <th style="font-size:12px;">Mode</th>
								               <th style="font-size:12px;">Device</th>
								               <th style="font-size:12px;">User</th>
								            </tr>
    	        						</thead>
    	        					    <asp:Repeater ID="OrderTranHistoryRepeater" runat="server" OnItemDataBound="OrderTranHistoryRepeater_ItemDataBound">
							            <HeaderTemplate>
							            <tbody>
							            </HeaderTemplate>
						            	<ItemTemplate>
								            <tr class="alt-row">
									            <td><%# DataBinder.Eval(Container.DataItem,"Order_TranID") %></td>
									            <td style="width:290px;"><%# String.Format("{0:dd/MM/yy hh:mm:ss tt}", DataBinder.Eval(Container.DataItem,"TransactionDate")) %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderNo") %></td>

                                                <%--
									            <td><%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "OrderedOn")) %></td>
									            --%>

                                                <td style="text-align:right;padding-right:25px;"><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "GrossAmount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalTax"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalAmount"))%></td>

                                                <%--
									            <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TipAmount"))%></td>
                                                --%>

                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Surcharge"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Discount"))%></td>
                                                <td style="width:150px;"><%# DataBinder.Eval(Container.DataItem, "PaymentModeStatus")%></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "DeviceName")%></td>
                                                <td style="width:150px;"><%# DataBinder.Eval(Container.DataItem, "Name")%></td>
								            </tr>
    							        </ItemTemplate>
						            	<AlternatingItemTemplate>
								            <tr>
									            <td><%# DataBinder.Eval(Container.DataItem,"Order_TranID") %></td>
									            <td style="width:290px;"><%# String.Format("{0:dd/MM/yy hh:mm:ss tt}", DataBinder.Eval(Container.DataItem,"TransactionDate")) %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderNo") %></td>

                                                <%--
									            <td><%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "OrderedOn")) %></td>
									            --%>

                                                <td style="text-align:right;padding-right:25px;"><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "GrossAmount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalTax"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalAmount"))%></td>

                                                <%--
									            <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TipAmount"))%></td>
                                                --%>

                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Surcharge"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Discount"))%></td>
                                                <td style="width:150px;"><%# DataBinder.Eval(Container.DataItem, "PaymentModeStatus")%></td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "DeviceName")%></td>
                                                <td style="width:150px;"><%# DataBinder.Eval(Container.DataItem, "Name")%></td>
								            </tr>
							            </AlternatingItemTemplate>		
					            		<FooterTemplate>
    	    						        <asp:Label ID="Footer" runat="server"  ForeColor="DarkRed" ></asp:Label>
            							</tbody>
			            				</FooterTemplate>	
                                        </asp:Repeater>                                        
						            </table>
						            <div style="margin:10px 5px 5px 5px; color:Green;" runat="server" id="MsgTip" visible="false">
						                <b>[CSH]</b> = <span style="color:Red">Cash,</span> <b>[CC]</b> = <span style="color:Red">Credit Card,</span> <b>[Vcr]</b> = <span style="color:Red">Voucher</span>
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