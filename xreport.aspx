<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/xreport.aspx.cs" Inherits="PosReport.xreport"  %>
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
					
					<!--<h3 style="cursor: s-resize;">  <asp:Literal ID="LblHead" runat="server" Text="Current Summary (X)"></asp:Literal></h3> -->
                    <h3 style="cursor: s-resize;">Total Sale-Current [<%= Session["R_Name"].ToString()%> ]</h3>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					<div id="Form">
					
							<fieldset class="column-left"> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
<%--                                <p>
   			                         <label>From Date :</label>
   			                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png"  />
                                        <asp:RequiredFieldValidator ID="ReqdFromDate" runat="server" ErrorMessage=" *" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" PopupButtonID="Image1" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>

                                     
                                     <label>Till Date :</label>
   			                            <asp:TextBox ID="txtTillDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image2" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png" />
                                        <asp:RequiredFieldValidator ID="ReqdTillDate" runat="server" ErrorMessage=" *" ControlToValidate="txtTillDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" PopupButtonID="Image2" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>
                                     
        	                    </p>
--%>                            
								<div id="forminput">
									<p>
	                                <asp:Button ID="BtnSave" runat="server" Text="View Report" CssClass="button" 
                                        onclick="BtnSave_Click" />&nbsp;
                                <asp:Button ID="BtnPrint" runat="server" Text="Print Report" CssClass="button" 
                                 Visible="false" />
    	                            </p>
                                </div>
								<div  style="float:left; width:650px;">
									<table>
							            <thead>
								            <tr style="background-color:#000000; color:#ffffff">
								               <td></td> <td></td><td></td><td></td><td align="center">Total Sale-Current</td>
								            </tr>
    	        						</thead>
    	        						
                          
						            </table>
                                    <p style="table-layout:auto" align="center">
                <asp:Label ID="company" runat="server" Visible="false"></asp:Label>
                </br>
                <asp:Label ID="location" runat="server" Visible="false"></asp:Label>
               </p>
									
									<table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="left" style="font-weight:bold;text-align: center">Current Date :&nbsp; <%=CurrDateTime%></td>
                                  </tr>
                                  <tr>
                                        <td align="left"><label id="LblRepo" runat="server" align="center"></label></td>
                                  </tr>
                                      <tr>
                                        <td align="left">
                                        <div id = "x_report" runat="server" visible="false">
                                            <table width="80%" border="0" cellspacing="0" cellpadding="0" style="border:1px solid #DDDDDD;">
                                          <tr style="background-color:Gray;" >
                                            <td align="left" style="color:White; font-weight:bolder">Description</td>
                                            <td width="24%" align="center" style="color:White; font-weight:bolder">Amount</td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">Cash</td>
                                            <td align="center" style="color:Black; font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}",CashSale) %></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">Card</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", CardSale)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">Voucher</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", VoucherSale)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">Total Discount</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", Discount)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">Total Surcharge</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}",SurCharge) %></td>
                                          </tr>
                                     
                                          <tr>
                                            <td align="left" style="color:Black;">FLOAT(+)</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", TotalFloatAmt)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">REFUND(-)</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", TotalRefundAmt)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">PAYOUT(-)</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", TotalPayoutAmt)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">TIPS(+)</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}",TipAmount )%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">GST Included (10 %)</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", TaxAmt)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">No Sale Count</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= String.Format("{0000}", noSaleCount)%> </td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">TOTAL Net Sale</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", TotalNetAmt)%></td>
                                          </tr>
                                          <tr>
                                            <td align="left" style="color:Black;">TOTAL Gross Sale</td>
                                            <td align="center" style="color:Black;font-style:italic;"><%= StrCurrency + String.Format("{0:0.00}", TotalGrossAmt)%> </td>
                                          </tr>
                                          <asp:Label ID="Drawer" runat="server"></asp:Label>
                                          <%--<tr>
                                            <td align="center" style="color:Red;">Total In Drawer</td>
                                            <td align="center" style="color:Red;font-style:italic;">$<%=String.Format("{0:0.00}",TotalInDrawerAmt)%> </td>
                                          </tr>--%>
                                        </table>
                                        </div> 
                                        </td>
                                  </tr>
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