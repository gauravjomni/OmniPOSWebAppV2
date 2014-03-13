<%@ Control Language="C#" Debug="true" autoeventwireup="true" CodeFile="~/usercontrols/menu_ctrl.ascx.cs" Inherits="MenuControl.Menus" %>
<%@ Import Namespace="System.Data" %>
<div id="sidebar"><div id="sidebar-wrapper"> <!-- Sidebar with logo and menu -->
			
			<h1 id="sidebar-title"><a href="#">Omnipos Admin</a></h1>
		  
			<!-- Logo (221px wide) -->
			<div style="margin:0% auto; text-align:center"><a href="#"><img id="logo" src="images/logo.png" alt="Omnipos" align="	"></a></div>
		  
			<!-- Sidebar Profile links -->
			<div id="profile-links">
				Hello, <asp:Label ID="LoggedUserName" runat="server"></asp:Label>
                 | <a href="signout.aspx" title="Sign Out">Sign Out</a>
            </div>
			
			<ul id="main-nav">  <!-- Accordion Menu -->
<!--				<li>
					<a href="http://www.google.com/" class="nav-top-item no-submenu">Dashboard</a>
				</li>
-->				

<!--			<li>
					<a style="padding-right: 15px;" href="#" class="nav-top-item">Location</a>
                </li>-->
				
               <asp:Repeater id="parentRepeater" runat="server" OnItemCreated="parentRepeater_ItemCreated" >
                   <ItemTemplate>
					<li>
						    <input type="hidden" id="HMenu" value="<%# DataBinder.Eval(Container.DataItem, "MenuID")%>" />
						<a style="padding-right: 15px;" id="Menu" href="<%# DataBinder.Eval(Container.DataItem, "NavigateUrl")%>" class='<%# (DataBinder.Eval(Container.DataItem,"MenuID").ToString()== HitMenuID)? "nav-top-item current" : "nav-top-item" %>' ><%# DataBinder.Eval(Container.DataItem, "MenuName") %></a>
							<asp:Repeater id="childRepeater" runat="server" datasource='<%# ((DataRowView)Container.DataItem).Row.GetChildRows("ParentChild") %>'    >
      						<HeaderTemplate>
                            <ul style="display: none;">
                            </HeaderTemplate>	
                            <ItemTemplate>
									<li>
    									<a  id="SubMenu" runat="server"  href ='<%# DataBinder.Eval(Container.DataItem, "[\"NavigateUrl\"]")%>' class='<%# (DataBinder.Eval(Container.DataItem,"[\"NavigateUrl\"]").ToString()== ("~/"+sPageName))? "current" : "" %>'  ><%# DataBinder.Eval(Container.DataItem, "[\"MenuName\"]")%></a>

<%--                                        <asp:HyperLink  ID="SubMenu1" runat="server" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "[\"NavigateUrl\"]") %>' CssClass='<%# (DataBinder.Eval(Container.DataItem,"[\"NavigateUrl\"]").ToString()== sPageName)? "current" : "" %>' ><%# DataBinder.Eval(Container.DataItem, "[\"MenuName\"]")%></asp:HyperLink>
--%>    									<asp:HiddenField ID="IsLocationBased" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "[\"IsLocationBased\"]") %>' />
                                            <asp:HiddenField ID="IsMenuShownWithinLocationLevel" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "[\"IsMenuShownWithinLocationLevel\"]") %>' />
                                    </li>
                            </ItemTemplate>
                            <FooterTemplate></ul></FooterTemplate>
                            </asp:Repeater>
					</li>
	                </ItemTemplate>
	            </asp:Repeater>	

			  <%--<li>
					<a style="padding-right: 15px;" id="Menus" href="#" class="<%= (sPageName=="zreport_company.aspx" || sPageName=="Company.aspx") ? "nav-top-item current" : "nav-top-item"  %>">Company Level</a>
					<ul style="display:none">
						<li>
							<a id="TimeZone" href ="TimeZone_Settings.aspx" class='<%= (sPageName=="TimeZone_Settings.aspx") ? "current" : ""  %>'  >Setting Server TimeZone</a>
                        </li>
					
						<li>
							<a id="Company" href ="Company.aspx" class='<%= (sPageName=="Company.aspx") ? "current" : ""  %>'  >Profile</a>
                        </li>
						<li>
							<a id="Zreport" href ="zreport_company.aspx" class='<%= (sPageName=="zreport_company.aspx") ? "current" : ""  %>'  >Z-Report</a>
                        </li>
						<li>
							<a id="Location" href ="Select_Restaurants.aspx" class='<%= (sPageName=="Select_Restaurants.aspx") ? "current" : ""  %>'  >Back To Location List</a>
                        </li>
                     </ul>
               </li>--%>
               
<%--				<li>
					<a style="padding-right: 15px;" href="#" class="nav-top-item">Logout</a>
               </li>      
--%>               
			</ul> <!-- End #main-nav -->
			
			<div id="messages" style="display: none"> <!-- Messages are shown when a link with these attributes are clicked: href="#messages" rel="modal"  -->
				
				<h3>3 Messages</h3>
			 
				<p>
					<strong>17th May 2009</strong> by Admin<br>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna. Cras in mi at felis aliquet congue.
					<small><a href="#" class="remove-link" title="Remove message">Remove</a></small>				</p>
			 
				<p>
					<strong>2nd May 2009</strong> by Jane Doe<br>
					Ut a est eget ligula molestie gravida. Curabitur massa. Donec 
eleifend, libero at sagittis mollis, tellus est malesuada tellus, at 
luctus turpis elit sit amet quam. Vivamus pretium ornare est.
					<small><a href="#" class="remove-link" title="Remove message">Remove</a></small>				</p>
			 
				<p>
					<strong>25th April 2009</strong> by Admin<br>
					Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus magna. Cras in mi at felis aliquet congue.
					<small><a href="#" class="remove-link" title="Remove message">Remove</a></small>				</p>
				
					<h4>New Message</h4>
					
					<fieldset>
						<textarea class="textarea" name="textfield" cols="79" rows="5"></textarea>
					</fieldset>
					
					<fieldset>
					
						<select name="dropdown" class="small-input">
							<option selected="selected" value="option1">Send to...</option>
							<option value="option2">Everyone</option>
							<option value="option3">Admin</option>
							<option value="option4">Jane Doe</option>
						</select>
						
						<input class="button" value="Send" type="submit">
					</fieldset>
			</div> <!-- End #messages -->
			
		</div></div>