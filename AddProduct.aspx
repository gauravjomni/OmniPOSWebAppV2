<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/AddProduct.aspx.cs" Inherits="PosProducts.AddProduct"  %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" /> --%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>		
        
<!--		<script type="text/javascript" src="jquery/highlight.pack.js"></script>
		<script type="text/javascript" src="jquery/jquery.accordion.js"></script>
-->
		 
        <link rel="stylesheet" href="jquery/autocomplete/jquery-ui.min.css" type="text/css" /> 
        <script type="text/javascript" src="jquery/autocomplete/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="jquery/autocomplete/jquery-ui.min.js"></script>
            
                
		<script type="text/javascript">
		    $(document).ready(function () {
		        $("#<%=IsSoleProduct.ClientID %>").click(function () {
		            checkSoleProduct();
		        })

		    });

		    function checkSoleProduct() {
		        if ($("#<%=IsSoleProduct.ClientID %>").is(':checked')) {
		            $("#entry").hide();
		            $("#optionrow").hide();
		        }
		        else {
		            $("#entry").show();
		            $("#optionrow").show();
		        }
		    }

		
/*		    $(document).ready(function () { 

		        //syntax highlighter
		        hljs.tabReplace = '    ';
		        hljs.initHighlightingOnLoad();

		        $.fn.slideFadeToggle = function (speed, easing, callback) {
		            return this.animate({ opacity: 'toggle', height: 'toggle' }, speed, easing, callback);
		        };

		        //accordion
		        $('.accordion').accordion({
		            defaultOpen: 'section1',
		            cookieName: 'accordion_nav',
		            speed: 'slow',
		            animateOpen: function (elem, opts) { //replace the standard slideUp with custom function
		                elem.next().stop(true, true).slideFadeToggle(opts.speed);
		            },
		            animateClose: function (elem, opts) { //replace the standard slideDown with custom function
		                elem.next().stop(true, true).slideFadeToggle(opts.speed);
		            }
		        });

		    });
*/
	</script>
    
    <script type="text/javascript">

	   $(document).ready(function () {
		
			function MyLoad(id1, id2, id3,id4) {
			$("#" + id1 + "").autocomplete({			
				source: [<%=strSubRcp_Ingredients %>],
				minLength:1,
				select: function (event, ui) {
					$("#" + id1 + "").val(ui.item.value);
					$("#" + id2).val(ui.item.id);
					$("#" + id3).val(ui.item.type);
					$("#" + id4).val(ui.item.unit);
					$("#" + id1).focus();
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
				data += "<td><input type='text' id='Ing_SubRcp_name" + i + "' name='Ing_SubRcp_name' class='text-input x_large-input' title='Product/Ingredient will be shown while typing' /></td> <td><input type='text' id='Ing_SubRcp_Type" + i + "' name='Ing_SubRcp_Type' class='text-input medium-input'  readonly /><input type='hidden' id='Ing_SubRcp_ID" + i + "' name='Ing_SubRcp_ID' class='text-input medium-input' /></td><td><input type='text' id='Ing_SubRcp_Unit" + i + "' name='Ing_SubRcp_Unit' class='text-input medium-input' readonly /></td></tr>";
				$('#entry').append(data);
	
				cnt = i;
				i++;
				if ($(".delete").length > 0) $(".delete").show();
				MyLoad('Ing_SubRcp_name' + cnt, 'Ing_SubRcp_ID' + cnt, 'Ing_SubRcp_Type' + cnt, 'Ing_SubRcp_Unit' + cnt);
			});


			function check() {
			//	obj=$('#entry table tr').find('span');
			obj = $('#entry tr').find('span');
			$.each(obj, function (key, value) {
				id = value.id;
				$('#' + id).html(key + 1);
			});
		   }


           $("#entry tr .prod").each(function(){
                $(this).click(function(){
                   var objid = this.id;
                   var objcnt = (this.id).replace("Ing_SubRcp_name", ""); 
                    MyLoad('Ing_SubRcp_name' + objcnt, 'Ing_SubRcp_ID' + objcnt , 'Ing_SubRcp_Type' + objcnt , 'Ing_SubRcp_Unit' + objcnt);
                });
           });

		
			MyLoad('Ing_SubRcp_name', 'Ing_SubRcp_ID', 'Ing_SubRcp_Type', 'Ing_SubRcp_Unit');
						
		});

	</script>

	    <!-- Our jQuery Script to make everything work -->
    
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
            
			<ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="Products.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					View Product(s)
				</span></a></li>
				
			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->            
            
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Add Product"></asp:Literal></h3>
					
					<ul class="content-box-tabs">
						<li><a href="#Form" class="default-tab current">Product Details</a></li> <!-- href must be unique and match the id of target div -->
						<li><a href="#Ingredient">Ingredients</a></li>
                        <li><a href="#Image">Image</a></li>
						<li><a href="#Option">Modifiers</a></li>
						<li><a href="#Cooking">Cooking Options</a></li>
						<li><a href="#PrnKitchen">Printers/Kitchens</a></li>
					</ul>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
               	<asp:Literal ID="LblProductNameHeader" runat="server"></asp:Literal>
				<div class="content-box-content">

					<Label ID="Msg" runat="server" Visible="false">
                        <div class="notification error png_bg">
                            <a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
                            <div id="ErrorMsg" runat="server"></div>                                
                        </div>
                    </Label>
                    
					<div id="Form" style="display:block;" class="tab-content default-tab">
                    
	                    	<div class="accordion" id="section1"><b><asp:Literal ID="LBLPrdct" runat="server" Text="Product Info"></asp:Literal></b><span></span></div>
	                        <div class="container">
    							<div class="content">
									<fieldset> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
		                            	<div style="float:left; height:690px; width:55%;background-color:#D4D4D4; padding:15px;" id="tab1part1">  
                                        <p>
                                            <label>Category</label>
                                                <asp:DropDownList ID="Category" runat="server" AutoPostBack="true" 
                                                CssClass="login-inp_lb" onselectedindexchanged="Category_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="ReqdCategory" runat="server" ErrorMessage=" *" ControlToValidate="Category"></asp:RequiredFieldValidator>
                                                <br /><small><asp:Label ID="LblCategory" runat="server" ForeColor="Red"></asp:Label></small>
                                        </p>

                                        <p>
                                            <label>Sub Category</label> 
                                                <asp:DropDownList ID="SubCategory" runat="server" AutoPostBack="false" 
                                                    CssClass="login-inp_lb"></asp:DropDownList>
                                               <asp:RequiredFieldValidator ID="ReqdSubCategory" runat="server" ErrorMessage=" *" ControlToValidate="SubCategory"></asp:RequiredFieldValidator>
                                                <br /><small><asp:Label ID="LblSubCategory" runat="server" ForeColor="Red"></asp:Label></small>
                                        </p>
                                        
                                        <p>
                                            <label>Product Name</label>
                                                <input class="text-input large-input" id="txtProductName" type="text" runat="server" /> 
                                                <asp:RequiredFieldValidator ID="ReqdProductName" runat="server" ErrorMessage=" *" ControlToValidate="txtProductName"></asp:RequiredFieldValidator>
                                                <br /><small><asp:Label ID="LblProductName" runat="server" ForeColor="Red"></asp:Label></small>
                                        </p>
        
                                        
                                        <p>
                                            <label>Name2</label>
                                                <input class="text-input large-input" id="txtProductName2" type="text" runat="server" /> 
                                                <br />
                                        </p>
        
                                        <p>
                                            <label>Description</label>
                                                <textarea class="text-input large-input" id="txtProductDesc" runat="server" /> 
                                                <br />
                                        </p>
        
                                        <p>
                                            <label>Color</label>
                                                <input class="text-input small-input color" id="txtColor" type="text" runat="server" /> 
                                                <br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red" Text="Place the mouse on the text box. Pick the color and click outside the pallette."></asp:Label></small>
                                         
                                        </p>
                                                                                
                                        <p>
                                            <label>BarCode</label> 
                                             <input class="text-input small-input" id="txtBarCode" type="text" runat="server" />
                                        </p>

                                        <p>
                                            <label>Choose Unit</label> 
                                                <asp:DropDownList ID="Unit" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
                                                 <asp:RequiredFieldValidator ID="ReqdUnit" runat="server" ErrorMessage=" *" ControlToValidate="Unit"></asp:RequiredFieldValidator>
                                        </p>

                                        <p>
                                            <label>Choose Supplier</label> 
                                                <asp:DropDownList ID="Supplier" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
                                        </p>

                                        <p>
                                            <label>Choose Course</label> 
                                                <asp:DropDownList ID="Course" runat="server" AutoPostBack="false" CssClass="login-inp_lb"></asp:DropDownList>
                                        </p>
                                        </div>
                                        
                                        <div style="float:right;height:680px; width:30%;background-color:#D4D4D4; padding:20px;" id="tab1part2">
        								
                                        <div style="float:left; width:40%">
                                          <label>Price1</label>
                                            <input class="text-input medium-input" id="txtPrice1" type="text" runat="server" onkeypress='return myFunc(event);'/> 
                                            <asp:RequiredFieldValidator ID="ReqdPrice1" runat="server" ErrorMessage=" *" ControlToValidate="txtPrice1"></asp:RequiredFieldValidator>
                                            <br /><small><asp:Label ID="LblPrice1" runat="server" ForeColor="Red"></asp:Label></small>
                                        </div>
                                        <div style="float:right; width:40%">
                                          <label>Price2</label>
                                            <input class="text-input medium-input" id="txtPrice2" type="text" runat="server" onkeypress='return myFunc(event);'/> 
                                            <asp:RequiredFieldValidator ID="ReqdPrice2" runat="server" ErrorMessage=" *" ControlToValidate="txtPrice2"></asp:RequiredFieldValidator>
                                            <br /><small><asp:Label ID="LblPrice2" runat="server" ForeColor="Red"></asp:Label></small>
										</div>
                                        <div style="clear:both"></div>
                                        
                                        <p>
                                        <div style="float:left; width:40%">
                                          <label>Price3</label>
                                            <input class="text-input medium-input" id="txtPrice3" type="text" runat="server" onkeypress='return myFunc(event);' /> 
                                        </div>
                                        <div style="float:right; width:40%">
                                          <label>Price4</label>
                                            <input class="text-input medium-input" id="txtPrice4" type="text" runat="server" onkeypress='return myFunc(event);' /> 
										</div>
                                        <div style="clear:both"></div>
                                        </p>
                                      
                                        <p>
	                                    <div style="float:left; width:40%">
                                          <label>Price5</label>
                                            <input class="text-input medium-input" id="txtPrice5" type="text" runat="server" onkeypress='return myFunc(event);' /> 
										</div>
                                        </p>

                                        <div style="clear:both"></div>
                                        <hr style="margin:15px 0px 10px 0px; border:1px solid #EEE;" />
                						
                                        <p>
	                                        <div style="float:left; width:40%">
	                                          <label>Re.Ord Lvl</label>
                                                <input class="text-input small-input" id="txtreordlbl" type="text"  runat="server"  onkeypress='return myFunc(event);' />
		                                            <asp:RequiredFieldValidator ID="Reqdreordlbl" runat="server" ErrorMessage=" *" ControlToValidate="txtreordlbl"></asp:RequiredFieldValidator>
                                            <br /><small><asp:Label ID="Lblreordlbl" runat="server" ForeColor="Red"></asp:Label></small>
                                                
                                            </div> 
											<div style="float:right; width:40%">
	                                          <label>Re.Ord Qty</label>
                                                <input class="text-input small-input" id="txtreordqty" type="text"  runat="server"  onkeypress='return myFunc(event);' />
		                                            <asp:RequiredFieldValidator ID="Reqdreordqty" runat="server" ErrorMessage=" *" ControlToValidate="txtreordqty"></asp:RequiredFieldValidator>
                                            <br /><small><asp:Label ID="Lblreordqty" runat="server" ForeColor="Red"></asp:Label></small>
                                            </div>
                                            <div style="clear:both"></div>
                                       </p> 
                                       <p>
											<div style="float:left; width:50%">
                                                <label>Op. Qty</label>
                                                <input class="text-input medium-input" id="txtOpQty" type="text"  runat="server" /> 
											</div>                            
                                                            
											<div style="float:left; width:40%">
                                                <label>Stock In Hand</label>
                                                <input class="text-input medium-input" id="txtInHand" type="text" readonly  runat="server" /> 
											</div>                            

                                            <div style="clear:both"></div>
	                                        <hr style="margin:15px 0px 10px 0px; border:1px solid #EEE;" />                                        </p>
                                        
                                        <p>                                        
                                            <div style="float:left; width:40%">
	                                            <label>Sort Order</label>
                                                <input class="text-input small-input" id="txtSort" name="txtSort" type="text" runat="server" value="0"  onkeypress='return myFunc(event);' /> 
                                                <asp:RequiredFieldValidator ID="ReqdSortOrder" runat="server" ErrorMessage=" *" ControlToValidate="txtSort"></asp:RequiredFieldValidator>
                                            </div>
                                            <div style="float:right; width:40%">
	                                            <label>Points</label>
                                                <input class="text-input small-input" id="txtPoints" type="text" runat="server"  onkeypress='return myFunc(event);' /> 
                                            </div>
											<div style="clear:both"></div>
                                            <hr style="margin:15px 0px 10px 0px; border:1px solid #EEE;" />
                                        </p>
                                      
                                        <p>
                	                        <div style="float:left; width:55%;">
                    	                        <label><input id ="ChkOpenPrice" type="checkbox" runat="server" value="1" /> Open Price</label>                                            
            	                            </div>    
	                                        <div style="float:right; width:30%">
		                                        <label><input id ="ChkGST" type="checkbox" runat="server" value="1" /> GST</label>
        	                                </div>    
                                            <div style="clear:both"></div>
                                        </p>
        									
                                        <p>
                                            <div style="float:left; width:62%">
	                                            <label><input id ="ChkChangePrice" type="checkbox" runat="server" value="1" /> Change Price</label>
                                            </div>
                                            <div style="float:right; width:30%">
	                                            <label><input id ="ChkFlag" type="checkbox" runat="server" value="1" /> Flag</label>
                                            </div>
                                            <div style="clear:both"></div>
                                        </p>
                                        
                                        <p>
                                            <div style="float:left; width:62%">
                                            <label><input id ="DailyItem" type="checkbox" runat="server" value="1" /> Daily Item</label>
                                            </div>
                                            <div style="float:right; width:30%">
                                            <label><input id ="Status" type="checkbox" runat="server" value="1" /> Active</label>
                                            </div>
                                            <div style="clear:both"></div>
                                        </p>

                                        <p>
                                            <div style="float:left; width:62%">
                                            <label><input id ="IsSoleProduct" type="checkbox" runat="server" value="1" /> Sole Product</label>
                                            </div>
                                        </p>
                                                                
                                </div>
		                            </fieldset>
                            	</div>
                            </div>
							
<!--							<div class="clear"></div><!-- End .clear -->
							
<!--                            <div style="float:left;height:auto; background-color:#CCCCCC"></div>
-->                            
                            <div class="clear"></div><!-- End .clear -->
                            
					</div> <!-- End #tab1 -->  
                    
					<div style="display: none;" class="tab-content" id="Ingredient">
                        <div class="accordion" id="section2"><b>Product Ingredient</b><span></span></div>
                        <div class="container">
    							<div class="content">
									<p>
                            <label>Choose Ingredient</label>
                            
		 				 <table id="entry">
							
							<thead>
								<tr style="background-color:#000000; color:#ffffff">
								   <th colspan="2"><input class="check-all" type="checkbox"></th>
                                   <th>Ingredient / SubRecipe</th>
                                   <th>Type</th>
                                   <th>Unit</th>                                   
								</tr>
							</thead>
                           <%
                                if ((Session["ing_SubRcp_name"] != null) && (Session["ing_SubRcp_name"] != "") && IsPostBack)
                                {
                                    arrIngSubRcpName = (string[])Session["ing_SubRcp_name"];
                                    arrIngSubRcpID = (string[])Session["ing_SubRcp_ID"];
                                    arrIngSubRcpType = (string[])Session["ing_SubRcp_Type"];
                                    arrIngSubRcpUnit = (string[])Session["ing_SubRcp_Unit"];
                                }
                                else if (!IsPostBack && (Mode.Value == "edit" || Mode.Value == "clone"))
                                {
                                    ArrayList varIngSbRcpID = new ArrayList();
                                    ArrayList varIngSbRcpName = new ArrayList();
                                    ArrayList varIngSbRcpUnit = new ArrayList();
                                    ArrayList varIngSbRcpType = new ArrayList();

                                    for (int cnt = 0; cnt < dsProdMixing.Tables[0].Rows.Count; cnt++)
                                    {
                                        varIngSbRcpID.Add(dsProdMixing.Tables[0].Rows[cnt][2].ToString());
                                        varIngSbRcpType.Add(dsProdMixing.Tables[0].Rows[cnt][3].ToString());
                                        varIngSbRcpName.Add(dsProdMixing.Tables[0].Rows[cnt][4].ToString());
                                        varIngSbRcpUnit.Add(dsProdMixing.Tables[0].Rows[cnt][5].ToString());

                                        arrIngSubRcpID = (string[])varIngSbRcpID.ToArray(typeof(string));
                                        arrIngSubRcpName = (string[])varIngSbRcpName.ToArray(typeof(string));
                                        arrIngSubRcpUnit = (string[])varIngSbRcpUnit.ToArray(typeof(string));
                                        arrIngSubRcpType = (string[])varIngSbRcpType.ToArray(typeof(string));
                                    }
                                }
                                
                                if (arrIngSubRcpName!=null)
								{
                                	for (int cnt = 0; cnt < arrIngSubRcpName.Length; cnt++)
                                    {
                               %>                                                                
						 	        <tr class="item_row">
                                        <td colspan="2"><span id='Span2<%=cnt %>' style="display:none;">1.</span><input type='checkbox' class="case"/></td>
                                        <td><input type='text' id='Ing_SubRcp_name<%=(cnt+1) %>' name='Ing_SubRcp_name' class="text-input x_large-input prod" title="Product/Ingredient will be shown while typing" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrIngSubRcpName[cnt].ToString() %>"  /></td>
                                        <td align="center"><input type='text' id='Ing_SubRcp_Type<%=(cnt+1) %>' name='Ing_SubRcp_Type' class="text-input medium-input qty" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" value="<%= arrIngSubRcpType[cnt].ToString() %>" readonly />
    	                                    <input type='hidden' id='Ing_SubRcp_ID<%=(cnt+1) %>' name='Ing_SubRcp_ID' class="text-input medium-input" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrIngSubRcpID[cnt].ToString() %>"/>
	                                    </td>
                                        <td align="center"><input type='text' id='Ing_SubRcp_Unit<%=(cnt+1) %>' name='Ing_SubRcp_Unit' class="text-input medium-input cost" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrIngSubRcpUnit[cnt].ToString() %>" readonly/></td>
                                    </tr>
                                    <% 
                                        } 
								}
								else
								{
								%>
                                    <tr class="item_row">
                                        <td colspan="2"><span id='Span1' style="display:none;">1.</span><input type='checkbox' class="case"/></td>
                                        <td><input type='text' id='Ing_SubRcp_name' name='Ing_SubRcp_name' class="text-input x_large-input prod"  title="Product/Ingredient will be shown while typing" /></td>
                                        <td align="center"><input type='text' id='Ing_SubRcp_Type' name='Ing_SubRcp_Type' class="text-input medium-input qty" readonly />
                                            <input type='hidden' id='Ing_SubRcp_ID' name='Ing_SubRcp_ID' class="text-input medium-input" />
                                        </td>
                                        <td align="center"><input type='text' id='Ing_SubRcp_Unit' name='Ing_SubRcp_Unit' class="text-input medium-input cost" readonly/></td>
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
                            </div>    
                    </div>
                    
                    <div style="display: none;" class="tab-content" id="Image">
                        <p>
                            <label>Image</label>
                            <input class="text-input small-input" id="txtImage" type="file" runat="server" /> 
                            <br />
                        </p>
                        <p>
                            <asp:Image ID="Prdt_Img" runat="server" Height="200"  />
                        </p>

                    </div>
      
					<div style="display: none;" class="tab-content" id="Option">
                        <p>
                            <label>Choose Modifiers</label>
		 				 <table>
							
							<thead>
								<tr style="background-color:#04b0c9; color:#ffffff">
								   <th><input class="check-all" type="checkbox"></th>
                                   <th>Modifier Name</th>
                                   <th>Price</th>
                                   <th>Modifier Level</th>                                   
								</tr>
							</thead>
						 
                          <asp:Repeater ID="ModfRepeater" runat="server" OnItemDataBound="Repeater_ItemDataBound">
							<HeaderTemplate>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td><input type="checkbox" runat="server" id="ChkModifier"  /></td>
									<td><%# DataBinder.Eval(Container.DataItem, "ModifierName")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Price1")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "ModifierLevelName")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr>
									<td><input type="checkbox" runat="server" id="ChkModifier"></td>
									<td><%# DataBinder.Eval(Container.DataItem, "ModifierName")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "Price1")%></td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "ModifierLevelName")%></td>
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
                            
                            
                        </p>

                    </div>
      
                    <div style="display: none;" class="tab-content" id="Cooking">
                        <p>
                            <label>Choose Cooking Options</label>
		 				 <table>
							
							<thead>
								<tr style="background-color:#04b0c9; color:#ffffff">
								   <th><input class="check-all" type="checkbox"></th>
                                   <th>Cooking Option</th>
								</tr>
							</thead>
						 
                          <asp:Repeater ID="CookingRepeater" runat="server" OnItemDataBound="CookingRepeater_ItemDataBound">
							<HeaderTemplate>
							<tbody>
							</HeaderTemplate>
							<ItemTemplate>
								<tr>
									<td><input type="checkbox" runat="server" id="ChkCookingOpt"  /></td>
									<td><%# DataBinder.Eval(Container.DataItem, "OptionName")%></td>
								</tr>
							</ItemTemplate>
							<AlternatingItemTemplate>
								<tr   class="alt-row">
									<td><input type="checkbox" runat="server" id="ChkCookingOpt"  /></td>
									<td><%# DataBinder.Eval(Container.DataItem, "OptionName")%></td>
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
                            
                            
                        </p>

                    </div>
                    
                    <div style="display: none;" class="tab-content" id="PrnKitchen">
                        <p>
                            <label>Choose Printer</label>
                            <asp:ListBox ID="Printer" runat="server"  AutoPostBack="false" CssClass="login-inp_lb" SelectionMode="Multiple"></asp:ListBox>
<%--							<asp:DropDownList ID="Printer" runat="server" AutoPostBack="false" CssClass="login-inp_lb" ></asp:DropDownList>--%>
                            <%--<asp:RequiredFieldValidator ID="ReqdPrinter" runat="server" ErrorMessage=" *" ControlToValidate="Printer"></asp:RequiredFieldValidator>--%>
                        </p>
                        
                        <p>
                            <label>Choose Kitchen</label>
							<asp:DropDownList ID="Kitchen" runat="server" AutoPostBack="false" CssClass="login-inp_lb" ></asp:DropDownList>
                            <%--<asp:RequiredFieldValidator ID="ReqdKitchen" runat="server" ErrorMessage=" *" ControlToValidate="Kitchen"></asp:RequiredFieldValidator>--%>
                        </p>

                    </div>
                    

				</div> <!-- End .content-box-content -->
					<p style="padding-left:30px">
                        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" onclick="BtnSave_Click"   />
                        <a href="Products.aspx" class="pageback">Back To Menu</a>
                        <asp:HiddenField ID="ProductID" runat="server" Value ="-1" />
                        <asp:HiddenField ID="Product_Img" runat="server" Value ="" />
                        <asp:HiddenField ID="Mode" runat="server" Value ="add" />
                        <asp:HiddenField ID="RestInitial" runat="server"  />
                        <asp:HiddenField ID="AvlIng" runat="server"  />
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
						© Copyright 2009 Your Company | Powered by <a href="http://themeforest.net/item/simpla-admin-flexible-user-friendly-admin-skin/46073">Omnipos Admin</a> | <a href="#">Top</a>
				</small>
			</div>--%><!-- End #footer -->
			
		</div>

</div>
</div>

</div>
</div>

</div>
</div>

</div>
</div>

</div>
</div>

</div>
</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>

</div>
</div>

</div>

</div>
</div>

</div>
</div>

</div>

</div>

</div>
</div>

</div>
</div>

</div>

</div>

</div>

</div>

</div>
</div>

</div>
</div>

</div>
</div>

</div>
</div>

</div>
</div>

</div>

</div>

</div>

</div>

</asp:Content>