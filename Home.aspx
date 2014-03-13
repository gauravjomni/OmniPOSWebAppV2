<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/Home.aspx.cs" Inherits="Logged_panel.Home" EnableViewStateMac="false"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />
            			
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">Content box</h3>
					
					<%--<ul class="content-box-tabs">
						<li><a href="#tab1" class="default-tab current">Table</a></li> <!-- href must be unique and match the id of target div -->
						<li><a href="#tab2">Forms</a></li>
					</ul>--%>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
                	<div style="display: block;" class="tab-content default-tab" id="tab1">
                	<p style="text-align:center"><h3>Welcome To The Administrative Panel.</h3></p>
                	</div>
				
				    <asp:Literal ID="Notification" runat="server" Visible="false"></asp:Literal>	
<%--					<div style="display: block;" class="tab-content default-tab" id="tab1"> <!-- This is the target div. id must match the href of this div's tab -->
						
						<div class="notification attention png_bg">
							<a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
							<div>
								This is a Content Box. You can put whatever you want in it. By 
the way, you can close this notification with the top-right cross.							</div>
						</div>
						
		                <table>
							
							<thead>
								<tr>
								   <th><input class="check-all" type="checkbox"></th>
								   <th>Column 1</th>
								   <th>Column 2</th>
								   <th>Column 3</th>
								   <th>Column 4</th>
								   <th>Column 5</th>
								</tr>
								
							</thead>
						 
							<tfoot>
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
							</tfoot>
						 
							<tbody>
								<tr class="alt-row">
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
								
								<tr>
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
								
								<tr class="alt-row">
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
								
								<tr>
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
								
								<tr class="alt-row">
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
								
								<tr>
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
								
								<tr class="alt-row">
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
								
								<tr>
									<td><input type="checkbox"></td>
									<td>Lorem ipsum dolor</td>
									<td><a href="#" title="title">Sit amet</a></td>
									<td>Consectetur adipiscing</td>
									<td>Donec tortor diam</td>
									<td>
										<!-- Icons -->
										 <a href="#" title="Edit"><img src="images/pencil.png" alt="Edit"></a>
										 <a href="#" title="Delete"><img src="images/cross.png" alt="Delete"></a> 
										 <a href="#" title="Edit Meta"><img src="images/hammer_screwdriver.png" alt="Edit Meta"></a>
									</td>
								</tr>
							</tbody>
							
						</table>
						
					</div> <!-- End #tab1 -->
					
					<div style="display: none;" class="tab-content" id="tab2">
					
						<form action="" method="post">
							
							<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
								
								<p>
									<label>Small form input</label>
										<input class="text-input small-input" id="small-input" name="small-input" type="text"> <span class="input-notification success png_bg">Successful message</span> <!-- Classes for input-notification: success, error, information, attention -->
										<br><small>A small description of the field</small>
								</p>
								
								<p>
									<label>Medium form input</label>
									<input class="text-input medium-input datepicker" id="medium-input" name="medium-input" type="text"> <span class="input-notification error png_bg">Error message</span>
								</p>
								
								<p>
									<label>Large form input</label>
									<input class="text-input large-input" id="large-input" name="large-input" type="text">
								</p>
								
								<p>
									<label>Checkboxes</label>
									<input name="checkbox1" type="checkbox"> This is a checkbox <input name="checkbox2" type="checkbox"> And this is another checkbox
								</p>
								
								<p>
									<label>Radio buttons</label>
									<input name="radio1" type="radio"> This is a radio button<br>
									<input name="radio2" type="radio"> This is another radio button
								</p>
								
								<p>
									<label>This is a drop down list</label>              
									<select name="dropdown" class="small-input">
										<option selected="selected" value="option1">Option 1</option>
										<option value="option2">Option 2</option>
										<option value="option3">Option 3</option>
										<option value="option4">Option 4</option>
									</select> 
								</p>
								
								<p>
									<label>Textarea with WYSIWYG</label>
									<div class="wysiwyg" style="width: 653px;"><ul class="panel"><li><a class="bold"><!-- --></a></li><li><a class="italic"><!-- --></a></li><li class="separator"></li><li><a class="createLink"><!-- --></a></li><li><a class="insertImage"><!-- --></a></li><li class="separator"></li><li><a class="h1"><!-- --></a></li><li><a class="h2"><!-- --></a></li><li><a class="h3"><!-- --></a></li><li class="separator"></li><li><a class="increaseFontSize"><!-- --></a></li><li><a class="decreaseFontSize"><!-- --></a></li><li class="separator"></li><li><a class="removeFormat"><!-- --></a></li></ul><div style="clear: both;"><!-- --></div><iframe id="textareaIFrame" style="min-height: 250px; width: 645px;"></iframe></div><textarea style="display: none;" class="text-input textarea wysiwyg" id="textarea" name="textfield" cols="79" rows="15"></textarea>
								</p>
								
								<p>
									<input class="button" value="Submit" type="submit">
								</p>
								
							</fieldset>
							
							<div class="clear"></div><!-- End .clear -->
							
						</form>
						
					</div> <!-- End #tab2 -->        --%>
					
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