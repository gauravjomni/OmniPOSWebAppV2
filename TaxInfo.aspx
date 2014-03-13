<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/TaxInfo.aspx.cs" Inherits="PosTaxes.TaxInfo"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
			<uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />
			
			<ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="AddTaxInfo.aspx"><span>
					<img src="images/pencil_48.png" alt="icon"><br>
					Add Tax Rate
				</span></a></li>
				
<%--				<li><a class="shortcut-button" href="Kitchens.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					Kitchen(s)
				</span></a></li>
--%>				
			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->

			
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">Current Tax Rate(s)</h3>
					
					<ul class="content-box-tabs">
						<li><a href="#tab1" class="default-tab current">Tax Info</a></li> <!-- href must be unique and match the id of target div -->
						<%--<li><a href="#tab2">Create User Group</a></li>--%>
					</ul>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					
					<div style="display: block;" class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
						
						<!--<div class="notification attention png_bg">
							<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
							<div>
								This is a Content Box. You can put whatever you want in it. By 
the way, you can close this notification with the top-right cross.							</div>
						</div>-->
						
		 				 <table>
							
							<thead>
								<tr style="background-color:#000000; color:#ffffff">
								   <th><input class="check-all" type="checkbox"></th>
								   <th>Literal</th>
								   <th>Rate(in %)</th>
								   <th>Status</th>
								   <th>Action</th>
								</tr>
								
							</thead>
						 
							<%--<tfoot>
								<tr>
									<td colspan="6">
										<div class="bulk-actions align-left">
											<select name="dropdown">
												<option selected="selected" value="option1">Choose an action...</option>
												<option value="option2">Edit</option>
												<option value="option3">Delete</option>
											</select>
											<a class="button" href="#">Apply to selected</a>
										</div>
										
										<div class="pagination">
											<a href="#" title="First Page">« First</a><a href="#" title="Previous Page">« Previous</a>
											<a href="#" class="number" title="1">1</a>
											<a href="#" class="number" title="2">2</a>
											<a href="#" class="number current" title="3">3</a>
											<a href="#" class="number" title="4">4</a>
											<a href="#" title="Next Page">Next »</a><a href="#" title="Last Page">Last »</a>
										</div> <!-- End .pagination -->
										<div class="clear"></div>
									</td>
								</tr>
							</tfoot>--%>
						    
                          <asp:Repeater ID="TaxInfoRepeater" runat="server" OnItemDataBound="TaxInfoRepeater_ItemDataBound">
							<HeaderTemplate>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
								<tr class="alt-row">
									<td><input type="checkbox"></td>
									<td><%# DataBinder.Eval(Container.DataItem, "TaxInfoLiteral")%></td>
									<td><%# DataBinder.Eval(Container.DataItem, "TaxRate")%></td>
									<td><%# DataBinder.Eval(Container.DataItem, "Status").ToString() == "1" ? "<img src=\"images/true.gif\">" : "<img src=\"images/false.gif\">" %></td>
									<td>
										<!-- Icons -->
										 <a href='<%# "AddTaxInfo.aspx?Txid=" + iTool.encryptString(DataBinder.Eval(Container.DataItem,"TaxInfoID").ToString()) + "&mode=edit" %>' title="Edit"><img src="images/pencil.png" alt="Edit"></a>
 										 <a href="javascript:;" title="Delete" onclick="removerec('<%# iTool.encryptString(DataBinder.Eval(Container.DataItem,"TaxInfoID").ToString())%>','TaxInfo.aspx')"><img src="images/cross.png" alt="Delete"></a>

									</td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td><input type="checkbox"></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "TaxInfoLiteral")%></td>
									<td><%# DataBinder.Eval(Container.DataItem, "TaxRate")%></td>
									<td><%# DataBinder.Eval(Container.DataItem, "Status").ToString() == "1" ? "<img src=\"images/true.gif\">" : "<img src=\"images/false.gif\">" %></td>
									<td>
										<!-- Icons -->
										 <a href='<%# "AddTaxInfo.aspx?Txid=" + iTool.encryptString(DataBinder.Eval(Container.DataItem,"TaxInfoID").ToString()) + "&mode=edit" %>' title="Edit"><img src="images/pencil.png" alt="Edit"></a>
 										 <a href="javascript:;" title="Delete" onclick="removerec('<%# iTool.encryptString(DataBinder.Eval(Container.DataItem,"TaxInfoID").ToString())%>','TaxInfo.aspx')"><img src="images/cross.png" alt="Delete"></a>
									</td>
								</tr>
							</AlternatingItemTemplate>		
							<FooterTemplate>
							    <tr>
									<td align="center" colspan="6">
									    <asp:Label ID="lblEmptyData" Text="No Data To Display" runat="server" Visible="false" ForeColor="DarkRed"  >
                                        </asp:Label>
                                    </td>
								</tr>	
							</tbody>
							</FooterTemplate>	
                          </asp:Repeater>

						</table>
						
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
			
			<%--<div id="footer">
				<small> <!-- Remove this notice or replace it with whatever you want -->
						© Copyright 2009 Your Company | Powered by <a href="http://themeforest.net/item/simpla-admin-flexible-user-friendly-admin-skin/46073">Omnipos Admin</a> | <a href="#">Top</a>
				</small>
			</div>--%><!-- End #footer -->
			
		</div>
</asp:Content>