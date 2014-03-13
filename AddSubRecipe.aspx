<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/AddSubRecipe.aspx.cs" Inherits="PosRecipe.AddSubRecipe"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            
        <link rel="stylesheet" href="jquery/autocomplete/jquery-ui.min.css" type="text/css" /> 
        <script type="text/javascript" src="jquery/autocomplete/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="jquery/autocomplete/jquery-ui.min.js"></script>

<script type="text/javascript">

	   $(document).ready(function () {
		
			function MyLoad(id1, id2, id3,id4) {
			$("#" + id1 + "").autocomplete({			
				source: [<%=strIngredients %>],
				minLength:1,
				select: function (event, ui) {
					$("#" + id1 + "").val(ui.item.value);
					$("#" + id2).val(ui.item.id);
					$("#" + id3).val(ui.item.unit);
					$("#" + id4).focus();
					return false;
				}
			});
		 }
			
            $(".delete").on('click', function () {
				count_row = $('#entry tr').length;
				$('.case:checkbox:checked').parents("tr").remove();
				$('.check_all').prop("checked", false);
				check();
			});

			var i = <%= rowStart %>; var cnt;
			$(".addmore").on('click', function () {
				count_row = $('#entry tr').length;
				var data = "<tr class='item_row'><td colspan='2'><span id='snum" + i + "' style='display:none;'>" + count_row + ".</span><input type='checkbox' class='case'/></td>";
				data += "<td><input type='text' id='IngredientName" + i + "' name='IngredientName' class='text-input xx_large-input prod' title='Ingredient will be shown while typing' /></td> <td><input type='text' id='IngredientQty" + i + "' name='IngredientQty' class='text-input medium-input' onkeypress='return myFunc(event);'/><input type='hidden' id='IngredientID" + i + "' name='IngredientID' class='text-input medium-input' /></td><td><input type='text' id='IngredientUnit" + i + "' name='IngredientUnit' class='text-input medium-input' readonly /></td></tr>";
				$('#entry').append(data);
				cnt = i;
				i++;
				if ($(".delete").length > 0) $(".delete").show();
				MyLoad('IngredientName' + cnt , 'IngredientID' + cnt , 'IngredientUnit' + cnt,'IngredientQty' + cnt);					
			});


			function check() {
			//			obj=$('#entry table tr').find('span');
			obj = $('#entry tr').find('span');
			$.each(obj, function (key, value) {
				id = value.id;
				$('#' + id).html(key + 1);
			});
		   }


           $("#entry tr .prod").each(function(){
                $(this).click(function(){
                   var objid = this.id;
                   var objcnt = (this.id).replace("IngredientName", ""); 
					MyLoad('IngredientName' + objcnt , 'IngredientID' + objcnt , 'IngredientUnit' + objcnt,'IngredientQty' + objcnt);					
                });
           });

		
			MyLoad('IngredientName', 'IngredientID', 'IngredientUnit','IngredientQty');
		
		});

	</script>            
            
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />
            
            		<ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="SubRecipes.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					View Sub Recipe(s)
				</span></a></li>

			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->
			
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Add Sub Recipe"></asp:Literal></h3>

                        <ul class="content-box-tabs">
    						<li><a href="#Form" class="default-tab current">Recipe Details</a></li> <!-- href must be unique and match the id of target div -->
						    <li><a href="#Ingredient">Ingredients</a></li>
    					</ul>					

					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
                    <Label ID="Msg" runat="server" Visible="false">
                        <div class="notification error png_bg">
                            <a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
                            <div id="ErrorMsg" runat="server"></div>                                
                        </div>
                    </Label>
                
					<div id="Form" style="display:block;" class="tab-content default-tab">
					
							<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
							    <div style="width:50%">	
								<p>
									<label>Sub Recipe Name</label>
										<input class="text-input large-input" id="txtSubRecipeName" name="txtSubRecipeName" type="text" runat="server" /> 
                                        <asp:RequiredFieldValidator ID="ReqdSubRecipeName" runat="server" ErrorMessage=" *" ControlToValidate="txtSubRecipeName"></asp:RequiredFieldValidator>
										<br /><small><asp:Label ID="LblSubRecipe" runat="server" ForeColor="Red"></asp:Label></small>
								</p>
								
                                <p>
                                    <label>Choose Unit</label> 
                                        <asp:DropDownList ID="Unit" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="ReqdUnit" runat="server" ErrorMessage=" *" ControlToValidate="Unit"></asp:RequiredFieldValidator>
                                </p>

								<p>
									<label>Active</label><input id ="Status" type="checkbox" runat="server" value="1" />
								</p>
								</div>								
							</fieldset>
							
							<div class="clear"></div><!-- End .clear -->
							
						
					</div> <!-- End #tab1 -->        

                    <div style="display: none;" class="tab-content" id="Ingredient">
                        <p>
                            <label>Choose Ingredients</label>
		 				 <table id="entry">
							
							<thead>
								<tr style="background-color:#000000; color:#ffffff">
								   <th colspan="2"><input class="check-all" type="checkbox"></th>
                                   <th>Ingredient Name</th>
                                   <th width="70">Quantity</th>
                                   <th width="70">Unit</th>                                   
								</tr>
							</thead>
                           <%
							if ((Session["ingredient_Name"] != null) && (Session["ingredient_Name"] != "") && IsPostBack)
							{
								arrIngredientName = (string[])Session["ingredient_Name"];
								arrIngredientID = (string[])Session["ingredient_ID"];
								arrIngredientQty = (string[])Session["ingredient_Qty"];
								arrIngredientUnit = (string[])Session["ingredient_Unit"];
							}
							else if (!IsPostBack && (Mode.Value == "edit" || Mode.Value == "clone"))
							{
								ArrayList varIngredientID = new ArrayList();
								ArrayList varIngredientName = new ArrayList();
								ArrayList varIngredientQty = new ArrayList();
								ArrayList varIngredientUnit = new ArrayList();

								for (int cnt = 0; cnt < dsIngredients.Tables[0].Rows.Count; cnt++)
								{
									varIngredientID.Add(dsIngredients.Tables[0].Rows[cnt][0].ToString());
									varIngredientName.Add(dsIngredients.Tables[0].Rows[cnt][1].ToString());
									varIngredientQty.Add(dsIngredients.Tables[0].Rows[cnt][2].ToString());
									varIngredientUnit.Add(dsIngredients.Tables[0].Rows[cnt][4].ToString());


									arrIngredientID = (string[])varIngredientID.ToArray(typeof(string));
									arrIngredientName = (string[])varIngredientName.ToArray(typeof(string));
									arrIngredientQty = (string[])varIngredientQty.ToArray(typeof(string));
									arrIngredientUnit = (string[])varIngredientUnit.ToArray(typeof(string));
								}
							}
                                
							if (arrIngredientName!=null)
							{
								for (int cnt = 0; cnt < arrIngredientName.Length; cnt++)
								{
					   %>                                                                
									<tr class="item_row">
										<td colspan="2"><span id='Span3' style="display:none;">1.</span><input type='checkbox' class="case"/></td>
										<td><input type='text' id='IngredientName<%=(cnt+1) %>' name='IngredientName' class="text-input xx_large-input prod" title="Ingredient will be shown while typing" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrIngredientName[cnt].ToString() %>" /></td>
										<td align="center"><input type='text' id='IngredientQty<%=(cnt+1) %>' name='IngredientQty' class="text-input medium-input qty" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrIngredientQty[cnt].ToString() %>" onkeypress='return myFunc(event);' />
											<input type='hidden' id='IngredientID<%=(cnt+1) %>' name='IngredientID' class="text-input medium-input" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrIngredientID[cnt].ToString() %>"/>
										</td>
										<td align="center"><input type='text' id='IngredientUnit<%=(cnt+1) %>' name='IngredientUnit' class="text-input medium-input cost" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrIngredientUnit[cnt].ToString() %>" readonly/></td>
								   </tr>								
					   <% 
								} 
							}
							else
							{
					%>
							 <tr class="item_row">
									<td colspan="2"><span id='Span1' style="display:none;">1.</span><input type='checkbox' class="case"/></td>
									<td><input type='text' id='IngredientName' name='IngredientName' class="text-input xx_large-input prod"  title="Ingredient will be shown while typing" /></td>
									<td align="center"><input type='text' id='IngredientQty' name='IngredientQty' class="text-input medium-input qty" onkeypress='return myFunc(event);'/>
										<input type='hidden' id='IngredientID' name='IngredientID' class="text-input medium-input" />
									</td>
									<td align="center">
									<input type='text' id='IngredientUnit' name='IngredientUnit' class="text-input medium-input unit" readonly/></td>
							  </tr>                                                   
					<%        
							}   
                        %>                                    

						</table>
                        <table cellspacing="0" id="optionrow">
                              <tr id="hiderow">
                                <td colspan="4">
                                  <button type="button" class='delete'>- Delete</button>
                                  <button type="button" class='addmore'>+ Add More</button>
                                </td>
                              </tr>
                        </table>
                        </p>

                    </div>
					
				</div> <!-- End .content-box-content -->
                        <p style="padding-left:30px">
                            <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" onclick="BtnSave_Click"/>
                            <a href="SubRecipes.aspx" class="pageback">Back To Menu</a>
                            <asp:HiddenField ID="SubRecipeID" runat="server" Value ="-1" />
                            <asp:HiddenField ID="Mode" runat="server" Value ="add" />
                        </p>
				
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
						© Copyright 2009 Your Company | Powered by <a href="">Omnipos Admin</a> | <a href="#">Top</a>
				</small>
			</div>--%><!-- End #footer -->
			
		</div>
</asp:Content>