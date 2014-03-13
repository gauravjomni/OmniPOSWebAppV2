<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/OrderAdjust.aspx.cs" Inherits="CSharpDemoEditGrid.OrderAdjust"  %>
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
					<h3 style="cursor: s-resize;">Order Adjustment [<%= Session["R_Name"].ToString()%> ]</h3>
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
					        
					    <div class="notification attention png_bg" id="Message" runat="server" visible="false">
							<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
							<div>Order has been adjusted successfully.</div>
						</div>
					        
							<fieldset class="column-left"> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
                            <div id="forminput" runat="server">
                                <p>
   			                         <label>From Date :</label>
                                     <input type="text" id="txtFromDate" name="txtFromDate" readonly value="<%=fromdater %>" />
                                      <br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>

                                     <label>Till Date :</label>
                                         <input type="text" id="txtTillDate" name="txtTillDate" readonly value="<%= tilldater %>" />
                                          <br /><small><asp:Label ID="Label2" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>
           	                    </p>
                                <p>
                                <asp:Button ID="BtnSave" runat="server" Text="View Report" CssClass="button" 
                                        onclick="BtnSave_Click" />&nbsp;
<%--                                <asp:Button ID="BtnPrint" runat="server" Text="Print Report" CssClass="button" 
                                 Visible="false" />
--%>                                </p>
                                </div>
							    <div id="OrderSummary" style="float:right; width:250px;position:absolute; right:5%; top:270px; border:1px solid #CCCCCC; background-color:#EEE;" runat="server" visible="false">
							        <div style="width:210px; margin:5px; padding:5px 1px 5px 10px;"><b>Total Ordered Amount</b> : 
                                        <asp:Label ID="OrderedAmt" runat="server" Text="" ForeColor="Green" Font-Bold="true"></asp:Label></div>
							        <div style="padding:10px;"><input type="checkbox" id="Adjustment"  runat="server" /> <b>Apply Adjustment</b></div>
							        <div style="width:210px; margin:5px; padding:5px 1px 5px 10px;"><b>Amount Will be</b> : <asp:Label ID="FinalAmt" runat="server" ForeColor="Green" Font-Bold="true"></asp:Label></div>
							        <div style="width:210px; text-align:center; margin:5px; padding:5px;">
							            <asp:Button ID="BtnAdjustApply" runat="server" Text="Apply Adjustment" CssClass="button" OnClick="BtnAdjustApply_Click" />
						            </div>
							            <asp:HiddenField ID="HiddenOrderedAmt" runat="server" />
							            <asp:HiddenField ID="HiddenAdjust" runat="server"  />
                                        <asp:HiddenField ID="HiddenOrderTransIDs" runat="server" />							        
							    </div>
							<div style="width:650px;font-size:12px;">
									<%--<table style="font-size:12px;">
							            <thead>
								            <tr style="background-color:#000000; color:#ffffff">
								               <th>TranID</th>
								               <th>Dt.</th>
								               <th>Ord.No</th>
								               <th>Dt</th>
								               <th>Net</th>
								               <th>Tip</th>
								               <th>SrChg</th>
								               <th>Disc</th>
								               <th>Tax</th>
								               <th>Gross</th>
								            </tr>
    	        						</thead>
    	        					    <asp:Repeater ID="OrderTranHistoryRepeater" runat="server" OnItemDataBound="OrderTranHistoryRepeater_ItemDataBound">
							            <HeaderTemplate>
							            <tbody>
							            </HeaderTemplate>
						            							<ItemTemplate>
							<asp:label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Order_TranID")  %>' ID="Label5" />
						</ItemTemplate>

						            	<ItemTemplate>
								            <tr class="alt-row">
									            <td>
									                <asp:label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Order_TranID")  %>' ID="Order_TranID" />
									            </td>
									            <td><%# DataBinder.Eval(Container.DataItem,"TransactionDate") %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderNo") %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderedOn") %></td>
									            <td style="text-align:right;padding-right:25px;"><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "GrossAmount"))%></td>
									            <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TipAmount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Surcharge"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Discount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalTax"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalAmount"))%></td>
								            </tr>
    							        </ItemTemplate>
    							        
						            	<AlternatingItemTemplate>
								            <tr>
									            <td><%# DataBinder.Eval(Container.DataItem,"Order_TranID") %></td>
									            <td><%# DataBinder.Eval(Container.DataItem,"TransactionDate") %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderNo") %></td>
									            <td><%# DataBinder.Eval(Container.DataItem, "OrderedOn") %></td>
									            <td style="text-align:right;padding-right:25px;"><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "GrossAmount"))%></td>
									            <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TipAmount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Surcharge"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "Discount"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalTax"))%></td>
                                                <td><%# StrCurrency + DataBinder.Eval(Container.DataItem, String.Format("{0:00}", "TotalAmount"))%></td>
								            </tr>
							            </AlternatingItemTemplate>		
					            		<FooterTemplate>
    	    						        <asp:Label ID="Footer" runat="server"  ForeColor="DarkRed" ></asp:Label>
            							</tbody>
			            				</FooterTemplate>	
                                        </asp:Repeater>
                          
						            </table>--%>
						           <div style="width:650px;font-size:12px;">  
						             <table>
						                <asp:datagrid id="DataGrid1" runat="server" PageSize="15" AutoGenerateColumns="False"
				                        CellPadding="2" CellSpacing="2" GridLines="Both" ShowFooter="True" PagerStyle-HorizontalAlign="Right"  OnItemDataBound="DataGrid1_ItemDataBound">
				                        <SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="lightBlue"></SelectedItemStyle>
				                        <AlternatingItemStyle BackColor="#F3F3F3"></AlternatingItemStyle>
				                        <ItemStyle ></ItemStyle>
                        				<HeaderStyle ForeColor="White" BackColor="black"></HeaderStyle>
			                        	<Columns>
					                        <asp:TemplateColumn HeaderText="TranID">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "Order_TranID")  %>' ID="TranID" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Dt">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TransactionDate") %>' ID="TranDate" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Ord. No">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderNo")  %>' ID="OrderNo" />
						                        </ItemTemplate>
                        				    </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Ord. Dt">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderedOn")  %>' ID="OrderedOn" />
    					                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Net">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# StrCurrency + DataBinder.Eval(Container.DataItem, "GrossAmount")  %>' ID="GrossAmount" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Tip">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# StrCurrency + DataBinder.Eval(Container.DataItem, "TipAmount")  %>' ID="TipAmount" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="SrChg">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# StrCurrency + DataBinder.Eval(Container.DataItem, "Surcharge")  %>' ID="Surcharge" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Disc">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# StrCurrency + DataBinder.Eval(Container.DataItem, "Discount")  %>' ID="Discount" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Tax">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"> </HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# StrCurrency + DataBinder.Eval(Container.DataItem, "TotalTax")  %>' ID="TotalTax" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:TemplateColumn HeaderText="Gross">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# StrCurrency + DataBinder.Eval(Container.DataItem, "TotalAmount")  %>' ID="TotalAmount" />
						                        </ItemTemplate>
					                        </asp:TemplateColumn>
					                        
					                        <%--<asp:TemplateColumn HeaderText="Reduce Sale(%)">
						                        <HeaderStyle HorizontalAlign="Center" Font-Bold="true"></HeaderStyle>
						                        <ItemTemplate>
							                        <asp:label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReduceSaleInPercent")  %>' ID="ReduceSaleInPercent" />
						                        </ItemTemplate>
						                        <EditItemTemplate>
						                            <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReduceSaleInPercent") %>' ID="TxtReduceSaleInPercent" Width="38px"  />
        						                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" Display="Dynamic" ErrorMessage="Only Decimals With Precision Value(2)" ControlToValidate="TxtReduceSaleInPercent" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
						                        </EditItemTemplate>
					                        </asp:TemplateColumn>
					                        <asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;img  border=0 src='images/Update.gif' title='Update Transaction' &gt;" CancelText="&lt;img border=0 src='images/Cancel.gif' title='Cancel Transaction' &gt;"
						                        EditText="&lt;img border=0 src='images/Edit.gif' title='Edit Transaction' &gt;"  >
					                        </asp:EditCommandColumn>--%>
					                        <asp:TemplateColumn>
    					                        <FooterTemplate>
                                                    <asp:Label ID="Footer" runat="server"  ForeColor="DarkRed" ></asp:Label>        													                        
		    			                        </FooterTemplate>
					                        </asp:TemplateColumn>
				                        </Columns>
				                        <PagerStyle HorizontalAlign="Right"></PagerStyle>				                        
			                        </asp:datagrid>
    						        
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