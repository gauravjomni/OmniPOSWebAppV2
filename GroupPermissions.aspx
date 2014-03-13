<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/GroupPermissions.aspx.cs" Inherits="PosGroupPerm.GroupPermissions"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%@ Import Namespace="System.Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />						
			<!--<ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="AddGroup.aspx"><span>
					<img src="images/pencil_48.png" alt="icon"><br>
					Add Group
				</span></a></li>
				
				<li><a class="shortcut-button" href="UserGroups.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					User Groups
				</span></a></li>
				
			</ul>--><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->

			
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">Groupwise Permission</h3>
					
					<ul class="content-box-tabs">
						<li><a href="#tab1" class="default-tab current">User Groups</a></li> <!-- href must be unique and match the id of target div -->
					</ul>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
					
					<div style="display: block;" class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
						<asp:Panel ID="Panel" runat="server" Visible="false">
                        <div class="notification attention png_bg">
							<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
							<div>User Group Permission Set Successfully.</div>
						</div>
						</asp:Panel>
						<p>
						    <label>Choose UserGroup</label>
                            <asp:DropDownList ID="DDUserGroup" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ReqdUserGroup" runat="server" ErrorMessage=" *" ControlToValidate="DDUserGroup"></asp:RequiredFieldValidator>
                            <asp:Button ID="CmdView" runat="server" Text="View Permission" 
                                CssClass="button" onclick="CmdView_Click" />
                            
                        </p>
		 				 <table>
							
							<thead>
								<tr style="background-color:#000000; color:#ffffff">
								   <th><input type="checkbox" id="chkall" runat="server"  />Permissions</th>
<%--								   <th>Active</th>
								   <th></th>
--%>								</tr>
								
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
						    
                          <asp:Repeater ID="ParentGroupPermRepeater" runat="server" OnItemDataBound="ParentGroupPermRepeater_OnItemDataBound"  >
							<HeaderTemplate>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
								<tr >
									<td><%--<input type="checkbox" runat="server" id="ChkParent" value="" />--%>
									<asp:HiddenField runat="server" ID="ChkParent" />
									<b style="padding: 10px 10px 10px 0px"><%# DataBinder.Eval(Container.DataItem,"MenuName") %></b></td>
									        <asp:Repeater id="ChildGroupPermRepeater" runat="server" datasource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("ParentChildGroup") %>' >
									                <HeaderTemplate>
        									            <table>
        									        </HeaderTemplate>
		        							        <ItemTemplate>
		        							               <tr>
		        							                    <td style="padding-left:25px"><input type="checkbox" id="ChkChild" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "[\"MenuID\"]")%>' />
			        						                    <%# DataBinder.Eval(Container.DataItem, "[\"MenuName\"]")%>
			        						                    </td>
			        						               </tr> 
				        					        </ItemTemplate>
				        					        
		        							        <AlternatingItemTemplate>
		        							               <tr>
		        							                    <td style="padding-left:25px"><input type="checkbox" id="ChkChild" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "[\"MenuID\"]")%>' />
			        						                    <%# DataBinder.Eval(Container.DataItem, "[\"MenuName\"]")%>
			        						                    </td>
			        						               </tr> 
				        					        </AlternatingItemTemplate>
        									        <FooterTemplate>
        									            </table>
                                                    </FooterTemplate>        									            
									        </asp:Repeater>
									
									<%--<td></td>--%>
								</tr>
							</ItemTemplate>
                            <AlternatingItemTemplate>
								<tr>
									<td>
									<%--<input type="checkbox" runat="server" id="ChkParent" value="" />--%>
									<asp:HiddenField runat="server" ID="ChkParent" />
									<b style="padding: 10px 10px 10px 0px"><%# DataBinder.Eval(Container.DataItem, "MenuName")%></b></td>
								    	<asp:Repeater id="ChildGroupPermRepeater" runat="server" datasource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("ParentChildGroup") %>' >
									                <HeaderTemplate>
        									            <table>
        									        </HeaderTemplate>
		        							        <ItemTemplate>
		        							                <tr>
			        						                    <td style="padding-left:25px;"><input type="checkbox" id="ChkChild" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "[\"MenuID\"]")%>' />
			        						                    <%# DataBinder.Eval(Container.DataItem, "[\"MenuName\"]")%></td>
			        						                </tr>
				        					        </ItemTemplate>
				        					        <AlternatingItemTemplate>
		        							               <tr>
		        							                    <td style="padding-left:25px"><input type="checkbox" id="ChkChild" runat="server" value='<%# DataBinder.Eval(Container.DataItem, "[\"MenuID\"]")%>' />
			        						                    <%# DataBinder.Eval(Container.DataItem, "[\"MenuName\"]")%>
			        						                    </td>
			        						               </tr> 
				        					        </AlternatingItemTemplate>
        									        <FooterTemplate>
        									            </table>
                                                    </FooterTemplate>        									            
									        </asp:Repeater>
								</tr>
							</AlternatingItemTemplate>
							<FooterTemplate>
							</tbody>
							</FooterTemplate>	
                          </asp:Repeater>
                            <br />
							<p>
                                <asp:Button ID="BtnSave" runat="server" Text="Save/Update Permission" CssClass="button" 
                                    onclick="BtnSave_Click"/>
                                <asp:HiddenField ID="Mode" runat="server" Value="add" />
    						</p>						
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