<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/AddGoodsReceiveNote.aspx.cs" Inherits="PosInvoice.AddGoodsReceiveNote" EnableSessionState="True" %>
<%@ Register Src="~/usercontrols/header_Ctrl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"/>

		<link rel="stylesheet" href="jquery/autocomplete/jquery-ui.min.css" type="text/css" /> 
        <script type="text/javascript" src="jquery/autocomplete/jquery-1.9.1.min.js"></script>
        <script type="text/javascript" src="jquery/autocomplete/jquery-ui.min.js"></script>
        
	    <script type="text/javascript">
	        $(document).ready(function () {

	            function MyLoad(id1, id2, id3, id4, id5, id6) {
	                $("#" + id1 + "").autocomplete({
	                    //source: [{id:1,value:"Thomas",cp:134},{id:65,value:"Richard",cp:1743},{id:235,value:"Sarat Ghosh",cp:7342},{id:982,value:"Nina",cp:21843},{id:724,value:"Pinta",cp:35}, {id:78,value:"Santa Maria",cp:787}],
	                    source: [<%=strProduct_Ingredients %>],
	                    minLength:1,
	                    select: function (event, ui) {
	                        $("#" + id1 + "").val(ui.item.value);
	                        $("#" + id2).val(ui.item.id);
	                        $("#" + id3).val(ui.item.type);
	                        $("#" + id4).val(ui.item.unitcost);
	                        $("#" + id5).val(ui.item.unit);
	                        $("#" + id6).focus();
	                        return false;
	                    }
	                });
	            }

	            $(".delete").on('click', function () {
	                count_row = $('#entry tr').length;
	                $('.case:checkbox:checked').parents("tr").remove();
	                $('.check_all').prop("checked", false);
	                check();
	                update_total();

	            });

	            var i = 2; var cnt;
	            $(".addmore").on('click', function () {
	                count_row = $('#entry tr').length;
	                var data = "<tr class='item_row'><td colspan='2'><span id='snum" + i + "' style='display:none;'>" + count_row + ".</span><input type='checkbox' class='case'/></td>";
	                data += "<td><input type='text' id='purc_name" + i + "' name='purc_name' class='text-input x_large-input' title='Product/Ingredient will be shown while typing' /></td> <td><input type='text' id='purc_qty" + i + "' name='purc_qty' class='text-input medium-input qty' onkeypress='return myFunc(event);' /><input type='hidden' id='purc_id" + i + "' name='purc_id' class='text-input medium-input'/><input type='hidden' id='prod_type" + i + "' name='prod_type'  class='text-input medium-input'/></td><td><input type='text' id='purc_unitcost" + i + "' name='purc_unitcost' class='text-input medium-input cost' onkeypress='return myFunc(event);'/></td> <td><input type='text' id='purc_unit" + i + "' name='purc_unit' class='text-input medium-input' readonly /></td> <td><input type='text' id='purc_amount" + i + "' name='purc_amount' class='text-input large-input price' readonly /></td></tr>";
	                //alert(data)

	                $('#entry').append(data);

	                cnt = i;
	                i++;
	                if ($(".delete").length > 0) $(".delete").show();
	                MyLoad('purc_name' + cnt, 'purc_id' + cnt, 'prod_type' + cnt, 'purc_unitcost' + cnt, 'purc_unit' + cnt, 'purc_qty' + cnt);
	                bind();
	            });


	            function check() {
	                //			obj=$('#entry table tr').find('span');
	                obj = $('#entry tr').find('span');
	                $.each(obj, function (key, value) {
	                    id = value.id;
	                    $('#' + id).html(key + 1);
	                });
	            }

	            //MyLoad('purc_name', 'purc_id', 'prod_type', 'purc_unitcost', 'purc_unit', 'purc_qty');
	            bind();

	            function roundNumber(number, decimals) {
	                var newString; // The new rounded number
	                decimals = Number(decimals);

	                if (decimals < 1) {
	                    newString = (Math.round(number)).toString();
	                } else {
	                    var numString = number.toString();
	                    if (numString.lastIndexOf(".") == -1) {// If there is no decimal point
	                        numString += "."; // give it one at the end
	                    }

	                    var cutoff = numString.lastIndexOf(".") + decimals; // The point at which to truncate the number
	                    var d1 = Number(numString.substring(cutoff, cutoff + 1)); // The value of the last decimal place that we'll end up with
	                    var d2 = Number(numString.substring(cutoff + 1, cutoff + 2)); // The next decimal, after the last one we want

	                    if (d2 >= 5) {// Do we need to round up at all? If not, the string will just be truncated
	                        if (d1 == 9 && cutoff > 0) {// If the last digit is 9, find a new cutoff point
	                            while (cutoff > 0 && (d1 == 9 || isNaN(d1))) {
	                                if (d1 != ".") {
	                                    cutoff -= 1;
	                                    d1 = Number(numString.substring(cutoff, cutoff + 1));
	                                } else {
	                                    cutoff -= 1;
	                                }
	                            }
	                        }
	                        d1 += 1;
	                    }
	                    if (d1 == 10) {
	                        numString = numString.substring(0, numString.lastIndexOf("."));
	                        var roundedNum = Number(numString) + 1;
	                        newString = roundedNum.toString() + '.';
	                    } else {
	                        newString = numString.substring(0, cutoff) + d1.toString();
	                    }
	                }
	                if (newString.lastIndexOf(".") == -1) {// Do this again, to the new string
	                    newString += ".";
	                }
	                var decs = (newString.substring(newString.lastIndexOf(".") + 1)).length;
	                for (var i = 0; i < decimals - decs; i++) newString += "0";
	                //var newNumber = Number(newString);// make it a number if you like
	                return newString; // Output the result to the form field (change for your purposes)
	            }

	            function update_price() {
                //price = $(this).html().replace("$","");

	                var totalamt = $('.totalamt').val().replace(",","");
                    var discamt = $('.disc').val().replace(",","");
	                var paidamt = (totalamt-discamt);
                    paidamt = roundNumber(paidamt, 2);
	                isNaN(paidamt) ? $('.paid').val("0.00") : $('.paid').val(paidamt);
	                //update_due();
	            }

                function update_due()
                {
                    var totalamt = $('.totalamt').val().replace(",","");
                    var discamt = $('.disc').val().replace(",","");
                    var finallypaidamt = $("#txtPaidAmt").val().replace(",","");
                    var due = (totalamt-discamt-finallypaidamt);
                    due = roundNumber(due, 2);
                    isNaN(due) ? $('.due').val("0.00") : $('.due').val(due);
                }

	            function bind() {
	                $(".disc").blur(update_price);
	                $(".paid").blur(update_due);
	            }
	        });

		</script>        

		<script type="text/javascript">
            $(function() {
                $("#GRNDt").datepicker();
            });
        </script>

	    <!-- Our jQuery Script to make everything work -->
    
            <div id="main-content"> <!-- Main Content Section with everything -->
			
			<!-- Page Head -->
            <uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			
            
			<ul class="shortcut-buttons-set">
				
				<li><a class="shortcut-button" href="GoodsReceivedNotes.aspx"><span>
					<img src="images/paper_content_pencil_48.png" alt="icon"><br>
					View Goods Received Note(s)
				</span></a></li>
				
			</ul><!-- End .shortcut-buttons-set -->
			
			<div class="clear"></div> <!-- End .clear -->            
            
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Create Goods Receive Note"></asp:Literal>
					</h3>
					
					<ul class="content-box-tabs">
						<li><a href="#Form" class="default-tab current">GRN Info</a></li> <!-- href must be unique and match the id of target div -->
<!--						<li><a href="#Ingredient">Ingredients</a></li>-->
					</ul>
					
					<div class="clear"></div>
					
				</div> <!-- End .content-box-header -->
               	<asp:Literal ID="LblPurchaseSSHeader" runat="server"></asp:Literal>
				<div class="content-box-content">

					<div id="Form" style="display:block;" class="tab-content default-tab">
                    
	                    <Label ID="Msg" runat="server" Visible="false">
						    <div class="notification error png_bg">
							    <a href="#" class="close"><img src="images/cross_grey_small.png" title="Close this notification" alt="close"></a>
							    <div id="ErrorMsg" runat="server"></div>                                
						    </div>
					    </Label>
                    
<!--	                    	<div class="accordion" id="section1"><b><asp:Literal ID="LBLPrdct" runat="server" Text="Product Info"></asp:Literal></b><span></span></div>-->
	                        <div class="container">
                                <div>
									<!--<fieldset>--> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->					<div id="info" style="width:100%;">
											<div id="orderL">
                                                <table id="metaL">
                                                    <tr>
                                                      <td>PO No ::</td>
                                                        <td>
                                                            <asp:DropDownList ID="PONo" AutoPostBack="true" runat="server" CssClass="login-inp_lb"  ></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="ReqdPONo" runat="server" ErrorMessage=" *" ControlToValidate="PONo" ForeColor="Red"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                      <td align="left" valign="top">PO Date ::</td>
                                                      <td align="left"><input type="text" name="PODate" id="PODate" class="text-input medium-input" value="<%=strPurchOrdDt %>" readonly="readonly" /></td>
                                                    </tr>
                                                    <tr>
                                                      <td align="left" valign="top">Supplier Name ::</td>
                                                      <td align="left"><input type="text" name="txtSupplier" id="txtSupplier" class="text-input medium-input" runat="server" readonly="readonly" /></td>
                                                    </tr>

                                                    <tr>
                                                      <td align="left" valign="top">Delivery Address</td>
                                                        <td align="left">
                                                            <input type="text" name="DeliveryAddress1" id="DeliveryAddress1" runat="Server" class="text-input x_large-input"  readonly />
															<p style="padding:15px 0px 0px 0px;"></p>
                                                            <input type="text" name="DeliveryAddress2" id="DeliveryAddress2" runat="Server" class="text-input x_large-input" readonly />
                                                        </td>
                                                    </tr>
                                              </table>
									  </div>
    	                                    <div id="orderR">
                                                <table id="metaL">
                                                    <tr>
                                                        <td class="meta-head">GRN #</td>
                                                        <td>
                                                            <input type="text" name="GRNCode" id="GRNCode" class="text-input large-input"  runat="server" />
                                                            <asp:RequiredFieldValidator ID="ReqdGRNNo" runat="server" ErrorMessage=" *" ForeColor="Red" ControlToValidate="GRNCode"></asp:RequiredFieldValidator>
                                                            <br /><small><asp:Label ID="LblGRNNo" runat="server" ForeColor="Red"></asp:Label></small>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                    
                                                        <td class="meta-head">Date</td>
                                                        <td>
                                                            <input type="text" name="GRNDt" id="GRNDt" class="text-input medium-input" value="<%=strGRNDt %>" readonly /><span id="EmptyError" runat="server" visible="false"> *</span>
<br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>                                                            
                                                        </td>
                                                    </tr>                                    
                                                </table>
                                        	</div>
                                        </div>
                                        <div style="clear:both"></div>
										<div id="details">

                               	 		 <table border="1" cellspacing="0" id="entry">
                                    <tr>
                                        <th colspan="2"><%--<input class='check_all' type='checkbox' onclick="javascript:select_all();" />--%></th>
<!--                                    <th width="24">SL</th>-->
                                        <th width="283" align="center">Item Name</th>
                                      <th width="100" align="center">Qty</th>
                                        <th width="170" align="center">Unit Cost( <%=Session["Currency"]%> )</th>
                                        <th width="124" align="center">Unit</th>
                                        <th width="144" align="center">Amount( <%=Session["Currency"]%> )</th>
                                    </tr>
                                    <%
                                        if (Mode.Value == "edit" || Mode.Value == "clone" || Mode.Value == "Show")
                                        {
                                            
                                            if ((Session["prod_name"] != null) && (Session["prod_name"] != "") && IsPostBack)
                                            {
                                                arrProdName = (string[])Session["prod_name"];
                                                arrProdID = (string[])Session["prod_id"];
                                                arrProdUnit = (string[])Session["prod_unit"];
                                                arrProdType = (string[])Session["prod_type"];
                                                arrProdCost = (string[])Session["purc_unitcost"];
                                                arrProdQty = (string[])Session["purc_qty"];
                                                arrProdTotalAmt = (string[])Session["prod_totalamt"];
                                            }
                                            else
                                            {
                                                ArrayList varPOID = new ArrayList();
                                                ArrayList varProdID = new ArrayList();
                                                ArrayList varProdName = new ArrayList();
                                                ArrayList varProdUnit = new ArrayList();
                                                ArrayList varProdType = new ArrayList();
                                                ArrayList varProdQty = new ArrayList();
                                                ArrayList varProdCost = new ArrayList();
                                                ArrayList varProdTotal = new ArrayList();
                                                
                                                for (int cnt = 0; cnt < ds.Tables[0].Rows.Count; cnt++)
                                                {
                                                    varPOID.Add(ds.Tables[0].Rows[cnt][0].ToString());
                                                    varProdID.Add(ds.Tables[0].Rows[cnt][1].ToString());
                                                    varProdName.Add(ds.Tables[0].Rows[cnt][2].ToString());
                                                    varProdUnit.Add(ds.Tables[0].Rows[cnt][3].ToString());
                                                    varProdType.Add(ds.Tables[0].Rows[cnt][4].ToString());
                                                    varProdQty.Add(ds.Tables[0].Rows[cnt][5].ToString());
                                                    varProdCost.Add(ds.Tables[0].Rows[cnt][6].ToString());

                                                    decimal varTotalAmt = (Convert.ToDecimal(ds.Tables[0].Rows[cnt][5]) * Convert.ToDecimal(ds.Tables[0].Rows[cnt][6]));
                                                    varProdTotal.Add(varTotalAmt.ToString());

                                                    arrProdID = (string[])varProdID.ToArray(typeof(string));
                                                    arrProdName = (string[])varProdName.ToArray(typeof(string));
                                                    arrProdUnit = (string[])varProdUnit.ToArray(typeof(string));
                                                    arrProdType = (string[])varProdType.ToArray(typeof(string));
                                                    arrProdQty = (string[])varProdQty.ToArray(typeof(string));
                                                    arrProdCost = (string[])varProdCost.ToArray(typeof(string));
                                                    arrProdTotalAmt = (string[])varProdTotal.ToArray(typeof(string));
                                                }
                                            }
                                            
                                                for (int cnt = 0; cnt < arrProdName.Length; cnt++)
                                                {
                                    %>
                                    <tr class="item_row">
                                        <td colspan="2"><span id='snum' style="display:none;">1.</span><%--<input type='checkbox' class="case"/>--%></td>
                                        <!--<td><span id='snum' style="display:none;">1.</span></td>-->
                                        <td><input type='text' id='purc_name' name='purc_name' class="text-input x_large-input prod" value="<%= arrProdName[cnt].ToString() %>" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" <%= (cnt==errorinpos) ? "readonly" : "" %> title="Product/Ingredient will be shown while typing" readonly /></td>
                                        <td align="center"><input type='text' id='purc_qty' name='purc_qty' class="text-input medium-input qty" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= String.Format("{0:0.00}",(arrProdQty[cnt].ToString()!="") ? (Convert.ToDecimal(arrProdQty[cnt])) : 0) %>" onkeypress="return myFunc(event);" readonly />
                                            <input type='hidden' id='purc_id' name='purc_id' class="text-input medium-input" value="<%= arrProdID[cnt]%>"/>
                                            <input type='hidden' id='prod_type' name='prod_type' class="text-input medium-input" value="<%= arrProdType[cnt]%>"/>
                                        </td>
                                        <td align="center"><input type='text' id='purc_unitcost' name='purc_unitcost' class="text-input medium-input cost" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" <%= (cnt==errorinpos) ? "readonly" : "" %>ss  value="<%=String.Format("{0:0.00}",(arrProdCost[cnt].ToString()!="") ? (Convert.ToDecimal(arrProdCost[cnt])):0) %>" onkeypress="return myFunc(event);" readonly /></td>
                                        <td align="center"><input type='text' id='purc_unit' name='purc_unit' class="text-input medium-input" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>"  value="<%=arrProdUnit[cnt] %>" readonly /></td>
                                        <td><input type='text' id='purc_amount' name='purc_amount' class="text-input large-input price" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" value="<%=String.Format("{0:0.00}",(arrProdTotalAmt[cnt].ToString()!="") ? (Convert.ToDecimal(arrProdTotalAmt[cnt])) :0) %>"  readonly/></td>
                                    </tr>
                                    <% 
                                           } // for
                                        }
                                        else if ((Session["prod_name"] != null) && (Session["prod_name"] != "") && IsPostBack && errorinpos >-1 )
                                        {
                                            arrProdName = (string[])Session["prod_name"];
                                            arrProdID = (string[])Session["prod_id"];
                                            arrProdUnit = (string[])Session["prod_unit"];                                            
                                            arrProdType = (string[])Session["prod_type"];
                                            arrProdCost = (string[])Session["purc_unitcost"];
                                            arrProdQty = (string[])Session["purc_qty"];
                                            arrProdTotalAmt = (string[])Session["prod_totalamt"];

                                            for (int cnt = 0; cnt < arrProdName.Length; cnt++)
                                            {
                                             %>
                                    <tr class="item_row">
                                        <td colspan="2"><span id='Span1' style="display:none;">1.</span><%--<input type='checkbox' class="case"/>--%></td>
                                        <!--<td><span id='snum' style="display:none;">1.</span></td>-->
                                        <td><input type='text' id='purc_name'  name='purc_name' class="text-input x_large-input" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""  %>" <%= (cnt==errorinpos) ? "readonly" : "" %> value="<%= arrProdName[cnt].ToString() %>"  readonly /></td>
                                        <td align="center"><input type='text' id='purc_qty' name='purc_qty' class="text-input medium-input qty" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" title="Product/Ingredient will be shown while typing"  value="<%= String.Format("{0:0.00}",(arrProdQty[cnt].ToString()!="") ? (Convert.ToDecimal(arrProdQty[cnt])) : 0) %>" onkeypress="return myFunc(event);" readonly />
                                            <input type='hidden' id='purc_id' name='purc_id' class="text-input medium-input" value="<%= arrProdID[cnt].ToString() %>" />
                                            <input type='hidden'  id='prod_type' name='prod_type' class="text-input medium-input" value="<%= arrProdType[cnt].ToString() %>"/>
                                       </td>
                                      <td align="center"><input type='text' id='purc_unitcost' name='purc_unitcost' class="text-input medium-input cost" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" title="Product/Ingredient will be shown while typing"   value="<%=String.Format("{0:0.00}",(arrProdCost[cnt].ToString()!="") ? (Convert.ToDecimal(arrProdCost[cnt])):0) %>" onkeypress="return myFunc(event);"  readonly /></td>
                                      <td align="center"><input type='text' id='purc_unit'  name='purc_unit' class="text-input medium-input" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" value="<%=arrProdUnit[cnt].ToString() %>" readonly /></td>
                                      <td><input type='text' id='purc_amount'  name='purc_amount' class="text-input large-input price" value="<%=String.Format("{0:0.00}",(arrProdTotalAmt[cnt].ToString()!="") ? (Convert.ToDecimal(arrProdTotalAmt[cnt])) :0) %>" style="<%= (cnt==errorinpos) ? "background-color:#df8f8f" : ""%>" readonly/></td>
                                    </tr>
                                    <%      }
                                        }
                                        else
                                        {
                                    %>
                                    <tr class="item_row">
                                        <td colspan="2"><span id='Span2' style="display:none;">1.</span><%--<input type='checkbox' class="case"/>--%></td>
                                        <!--<td><span id='snum' style="display:none;">1.</span></td>-->
                                        <td><input type='text' id='purc_name' name='purc_name' class="text-input x_large-input prod"  title="Product/Ingredient will be shown while typing" /></td>
                                        <td align="center"><input type='text' id='purc_qty' name='purc_qty' class="text-input medium-input qty" onkeypress="return myFunc(event);" />
                                            <input type='hidden' id='purc_id' name='purc_id' class="text-input medium-input" />
                                            <input type='hidden' id='prod_type' name='prod_type' class="text-input medium-input" />
                                        </td>
                                        <td align="center"><input type='text' id='purc_unitcost' name='purc_unitcost' class="text-input medium-input cost" onkeypress="return myFunc(event);"/></td>
                                        <td align="center"><input type='text' id='purc_unit' name='purc_unit' class="text-input medium-input"  readonly /></td>
                                        <td><input type='text' id='purc_amount' name='purc_amount' class="text-input large-input price"  readonly/></td>
                                    </tr>
                                    <%        
                                        }   
                                    %>                                    
                                   </table>
                                   <div id="ErrorInPurchase" style="width:100%; text-align:center; font-size:medium;"><asp:Label ID="LblDetail" runat="server" ForeColor="Red"></asp:Label></div>  
                                   <%--<table cellspacing="0" id="optionrow" width="100%">
	                                  <tr id="hiderow">
									    <td colspan="5">
                                          <button type="button" class='delete'>- Delete</button>
										  <button type="button" class='addmore'>+ Add More</button>
                                        </td>
                                        <td colspan="2" class="info">[Remove The Row If Cell Is Marked By Red Color. Create A New Row.]</td>
							 	    </tr>
								   </table>--%>
                                   		 <div id="comments" style="float:left; width:370px;margin-top:5px;">
	                                         <table>
    	                                     	<tr>
        	                                    	<td>Notes :</td>
            	                                </tr>
                	                         	<tr>
                    	                        	<td><textarea rows="3" id="txtNote" name="txtNote" runat="server"></textarea></td>
                        	                    </tr>
                            	             </table>                                         
                                         </div> 
                                   		 	<div id="summary">
                                             <table cellspacing="0">
                                            <tr>
                                              <td colspan="6" class="total-line">Subtotal</td>
                                              <td width="83" align="right" class="total-value">
                                              <input type='text' id='subtotal' name='subtotal' class="text-input large-input tax" value="<%=strSubTotalAmt %>" readonly /><!--<div id="subtotal"></div>--></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class="total-line">Tax Amt</td>
                                                <td align="right" class="total-value"><div id="total">
                                                  <input type='text' id='txtTaxAmt' name='txtTaxAmt' class="text-input medium-input tax" value="<%=strTaxAmt %>" readonly />
                                                </div></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class="total-line">Total Amount</td>
                                                <td align="right" class="total-value">
                                                <input type='text' id='totalamt' name='totalamt' class="text-input large-input totalamt" value="<%= strTotalAmt%>" readonly /><!--<div id="totalamt"></div>--></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class="total-line">Discount</td>
                                                <td align="right" class="total-value">
                                                <input type='text' id='txtDisc' name='txtDisc' class="text-input medium-input disc" value="<%= strDiscountAmt%>" onkeypress='return myFunc(event);'  /><!--<div id="totalamt"></div>--></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class="total-line">Paid Amount</td>
                                                <td align="right" class="total-value">
                                                <input type='text' id='txtPaidAmt' name='txtPaidAmt' class="text-input large-input paid" value="<%= strPaidAmt%>"  onkeypress='return myFunc(event);' /><!--<div id="totalamt"></div>--></td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" class="total-line">Due Amount</td>
                                                <td align="right" class="total-value">
                                                <input type='text' id='txtDueAmt' name='txtDueAmt' class="text-input large-input due" value="<%= strDueAmt%>"  readonly /><!--<div id="totalamt"></div>--></td>
                                            </tr>
                                            </table>
                                            </div>
                                    	</div>
                                  <!--</table>-->
                                  	<p>
		                               	 
                                    </p>      
						  		</div>
<!---		                            </fieldset>-->
										
                            	</div>
                                
                            </div>
							<p></p>
							
                            <div class="clear"></div><!-- End .clear -->
                            
					</div> <!-- End #tab1 -->  
      
				</div> <!-- End .content-box-content -->
					<p style="padding-left:30px">
                        <asp:Button ID="BtnSave" runat="server" Text="Save" CssClass="button" onclick="BtnSave_Click"   />
                        <a href="Products.aspx" class="pageback">Back To Menu</a>
                        <asp:HiddenField ID="GRNNo" runat="server" Value ="-1" />
                        <asp:HiddenField ID="SupplierID" runat="server" Value ="" />
                        <asp:HiddenField ID="Mode" runat="server" Value ="add" />
                        <asp:HiddenField ID="RestInitial" runat="server"  />
		    		</p>
				
			</div> <!-- End .content-box -->
			
			
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