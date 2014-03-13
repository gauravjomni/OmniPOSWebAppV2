<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/ClearOrderTransact.aspx.cs" Inherits="PosTools.ClearOrderTransact"  %>
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
					
					<h3 style="cursor: s-resize;">Clear Order Transactions [ <%= Session["R_Name"].ToString()%> 
                        ]</h3>
					
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
                                <p>
   			                         <label>From Date :</label>
   			                            <%--<asp:TextBox ID="txtFromDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png"  />
                                        <asp:RequiredFieldValidator ID="ReqdFromDate" runat="server" ErrorMessage=" *" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" PopupButtonID="Image1" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>--%>
                                     <input type="text" id="txtFromDate" name="txtFromDate" readonly value="<%=fromdater %>" />
                                      <br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red">[Place the 
                                     Mouse Pointer In The TextBox And Select Date]</asp:Label></small>

                                     <label>Till Date :</label>
<%--   			                            <asp:TextBox ID="txtTillDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image2" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png" />
                                        <asp:RequiredFieldValidator ID="ReqdTillDate" runat="server" ErrorMessage=" *" ControlToValidate="txtTillDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" PopupButtonID="Image2" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>
--%>                                 
                                     <input type="text" id="txtTillDate" name="txtTillDate" readonly value="<%= tilldater %>" />
                                      <br /><small><asp:Label ID="Label2" runat="server" ForeColor="Red">[Place the 
                                     Mouse Pointer In The TextBox And Select Date]</asp:Label></small>
    
        	                    </p>
                                <p>
                                <asp:Button ID="BtnSave" runat="server" Text="View Report" CssClass="button" 
                                        onclick="BtnSave_Click" />&nbsp;
                                        
                                 <asp:Button ID="BtnDelete" runat="server" Text="Delete Selected" CssClass="button" 
                                  onclick="BtnDelete_Click" OnClientClick = "return Confirm()" Visible="false" />
                                </p>
                                </div>
								<div style="width:690px;font-size:12px;">
									<table style="font-size:12px;">
							            <thead >
								            <tr style="background-color:#000000; color:#ffffff;">
								               <th><input class="check-all" type="checkbox"></th>
								               <th style="font-size:12px;">Tr. ID</th>
								               <th style="font-size:12px;">Tr. Date</th>
								               <th style="font-size:12px;">Order #</th>
								               <th style="font-size:12px;">Order Date</th>
								               <th style="font-size:12px;">Net Sale</th>
								               <th style="font-size:12px;">GST</th>
								               <th style="font-size:12px;">Gross Sale</th>
								            </tr>
    	        						</thead>
    	        					    <asp:Repeater ID="OrderTranHistoryRepeater" runat="server" OnItemDataBound="OrderTranHistoryRepeater_ItemDataBound">
							            <HeaderTemplate>
							            <tbody>
							            </HeaderTemplate>
						            	<ItemTemplate>
								            <tr class="alt-row">
									            <td><input type="checkbox" id="Order" runat="server" /></td>
									            <td><div id="OrderID" runat="server"><%# DataBinder.Eval(Container.DataItem,"Order_TranID") %></div>
									            <td><%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem,"TransactionDate")) %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderNo") %></td>
									            <td><%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "OrderedOn")) %></td>
									            <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "GrossAmount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalTax"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalAmount"))%></td>
								            </tr>
    							        </ItemTemplate>
						            	<AlternatingItemTemplate>
								            <tr>
									            <td><input type="checkbox" runat="server" id="Order" /></td>
									            <td><div id="OrderID" runat="server"><%# DataBinder.Eval(Container.DataItem,"Order_TranID") %></div>
                                                <td><%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem,"TransactionDate")) %></td>									            
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderNo") %></td>
									            <td><%# String.Format("{0:MM/dd/yyyy}", DataBinder.Eval(Container.DataItem, "OrderedOn")) %></td>
									            <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "GrossAmount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalTax"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalAmount"))%></td>
								            </tr>
							            </AlternatingItemTemplate>		
					            		<FooterTemplate>
							                <tr>
									            <td align="center" colspan="8">
									                <asp:Label ID="lblEmptyData" Text="No Data To Display" runat="server" Visible="false" ForeColor="DarkRed"  >
                                                    </asp:Label>
                                                </td>
								            </tr>	
    							            </tbody>
							            </FooterTemplate>		
                                        </asp:Repeater>                                        
						            </table>
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