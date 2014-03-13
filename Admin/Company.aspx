<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Company.aspx.cs" Inherits="Admin_Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="main-content">
        <!-- Main Content Section with everything -->
        <!-- Page Head -->
        <h2>
            <asp:Label ID="LabelCompany" runat="server" Text="" ForeColor="#039EB9"></asp:Label></h2>
        <div style="float: left; position: absolute; top: 10px; left: 75%">
            <h3>
                Welcome {
                <asp:Label ID="Header_LoggedUserName" runat="server" ForeColor="#6D7E47"></asp:Label>
                &nbsp;}
            </h3>
        </div>
      
        
        <div class="content-box">
            <!-- Start Content Box -->
            <div class="content-box-header">
                <h3 style="cursor: s-resize;">
                    Current Company(s)</h3>
                <ul class="content-box-tabs">
                    <li><a href="#tab1" class="default-tab current">Company(s)</a></li>
                    <!-- href must be unique and match the id of target div -->
                    <%--<li><a href="#tab2">Create User Group</a></li>--%>
                </ul>
                
            </div>
               
            <!-- End .content-box-header -->
            <div class="content-box-content">
             <div class="clear">
                            
                                <asp:Label ID="lblError" runat="server" ForeColor="Red" Visible="False"></asp:Label>
        </div>
                <div style="display: block;" class="tab-content default-tab" id="tab1">
                    <table>
                        <thead>
                            <tr style="background-color: #000000; color: #ffffff">
                                <th>
                                    Company Name
                                </th>
                                <th>
                                    Company Code
                                </th>
                                <%--<th>
                                </th>--%>
                                <%--<th>Password</th>--%>
                                <th>
                                    Company DB Name
                                </th>
                                <th>
                                    Company DB User
                                </th>
                                
                                <th>
                                    Activate Company
                                </th>
                                <th>
                                    Action
                                </th>
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
                        <asp:Repeater ID="UserRepeater" runat="server" 
                            onitemcommand="UserRepeater_ItemCommand">
                            <HeaderTemplate>
                                <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr class="alt-row">
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"Name") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"Code") %>
                                    </td>
                                    <%--<td>
                                        <%# DataBinder.Eval(Container.DataItem, "Active").ToString() == "1" ? "<img src=\"../images/true.gif\">" : "<img src=\"../images/false.gif\">"%>
                                    </td>--%>
                                    <%--<td><%# DataBinder.Eval(Container.DataItem, "UserPassword")%></td>--%>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"DBName") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"DBUserName") %>
                                    </td>
                                     
                                    <td>
                                      <%--  <asp:Button ID="btnActivate" runat="server" Enabled='<%# DataBinder.Eval(Container.DataItem,"IsDataBaseCreated") %>'
                                            Text="Activate"  CommandName="Activate" CssClass="button"  />--%>

                                              <asp:Button ID="btnActivate" runat="server" 
                                            Text="Activate"  CommandName="Activate" Visible='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem,"IsDataBaseCreated")) == false %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                            <asp:Button ID="btnDeActivate" runat="server" 
                                            Text="DeActivate"  CommandName="DeActivate" Visible='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem,"IsDataBaseCreated")) == true %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                    </td>
                                    <td>
                                        <!-- Icons -->
                                        <a href='<%# "AddCompany.aspx?comid=" + iTool.encryptString(DataBinder.Eval(Container.DataItem,"Id").ToString())  %>'
                                            title="Edit">
                                            <img src="../images/pencil.png" alt="Edit"></a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"Name") %>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem,"Code") %>
                                    </td>
                                   <%-- <td> <%# DataBinder.Eval(Container.DataItem, "Active").ToString() == "1" ? "<img src=\"../images/true.gif\">" : "<img src=\"../images/false.gif\">"%> </td>--%>
                                   
                                    <%--<td><%# DataBinder.Eval(Container.DataItem, "UserPassword")%></td>--%>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "DBName")%>
                                    </td>
                                    <td>
                                        <%# DataBinder.Eval(Container.DataItem, "DBUserName")%>
                                    </td>
                                     <%--<td>
                                              <asp:Button ID="btnCreate" runat="server" 
                                            Text="Create/Upgrade Company"  CommandName="Create" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                    </td>--%>
                                    <td>
                                             <asp:Button ID="btnActivate" runat="server" 
                                            Text="Activate"  CommandName="Activate" Visible='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem,"IsDataBaseCreated")) == false %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                            <asp:Button ID="btnDeActivate" runat="server" 
                                            Text="DeActivate"  CommandName="DeActivate" Visible='<%# Convert.ToBoolean( DataBinder.Eval(Container.DataItem,"IsDataBaseCreated")) == true %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"Id") %>' />
                                </td>
                                    <td>
                                        <!-- Icons -->
                                        <a href='<%# "AddCompany.aspx?comid=" + iTool.encryptString(DataBinder.Eval(Container.DataItem,"Id").ToString())  %>'
                                            title="Edit">
                                            <img src="../images/pencil.png" alt="Edit"></a>
                                    </td>
                                </tr>
                            </AlternatingItemTemplate>
                            <FooterTemplate>
                                </tbody>
                            </FooterTemplate>
                        </asp:Repeater>
                    </table>
                </div>
                <!-- End #tab1 -->
            </div>
            <!-- End .content-box-content -->
        </div>
    </div>
</asp:Content>
