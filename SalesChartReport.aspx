<%@ Page Language="C#" MasterPageFile="~/innerpage.master" AutoEventWireup="true" CodeFile="SalesChartReport.aspx.cs" Inherits="Default3" %>
<%@ Register Src="~/usercontrols/SalesChartControl.ascx" TagName="header_Ctrl" TagPrefix="uc_head" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />

            <div id="main-content"> <!-- Main Content Section with everything -->
			<!-- Page Head -->
			<uc_head:header_Ctrl ID="Header_Ctrl" runat="server" />			

			
			<div class="content-box"><!-- Start Content Box -->
				
				<div class="content-box-header">
					
					<h3 style="cursor: s-resize;">Sales History [ <%= Session["R_Name" ].ToString()%> ]</h3>
					
					
                   
					<div class="clear"></div>
				</div> <!-- End .content-box-header -->
              
	            <script type="text/javascript">
	                $(function () {
	                    $("#txtFromDate").datepicker();
	                    $("#txtTillDate").datepicker();
	                });

	            </script>
	            				
				<div class="content-box-content">
					<div id="Form">
					
							<fieldset class="column-left"> <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
                              <div id="forminput">
                                <table>
                                <tr>
   			                       <td>  <label>From Date</label></td>
                                    <td>  <label>To Date</label></td>
                                      </tr>
   			                            <%--<asp:TextBox ID="txtFromDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image1" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png"  />
                                        <asp:RequiredFieldValidator ID="ReqdFromDate" runat="server" ErrorMessage=" *" ControlToValidate="txtFromDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate" PopupButtonID="Image1" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>--%>
                                     <tr>
                                     <td>
                                     <input type="text" id="txtFromDate" name="txtFromDate" readonly value="<%=fromdater %>" />
                                      <br /><small><asp:Label ID="Label1" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>

                                    
<%--   			                            <asp:TextBox ID="txtTillDate" runat="server" CssClass="text-input x_small-input" ></asp:TextBox>
                                        <asp:Image ID="Image2" runat="server" Height="19px" ImageUrl="~/images/calendar.png.png" />
                                        <asp:RequiredFieldValidator ID="ReqdTillDate" runat="server" ErrorMessage=" *" ControlToValidate="txtTillDate"></asp:RequiredFieldValidator>
                                          <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtTillDate" PopupButtonID="Image2" CssClass="cal_Theme1" >
                                     </ajaxToolkit:CalendarExtender>
--%>                                 </td>
                                      <td>
                                     <input type="text" id="txtTillDate" name="txtTillDate" readonly value="<%= tilldater %>" />
                                      <br /><small><asp:Label ID="Label2" runat="server" ForeColor="Red">[Place the Mouse Pointer In The TextBox And Select Date]</asp:Label></small>
                                     </td>
                                     </tr>
        	                    </table>
                                <p>
                                 <asp:Button ID="BtnSave" runat="server" Text="View Report" CssClass="button" 
                                        onclick="viewReportAction" align="center" />&nbsp;
                                <asp:Button ID="BtnPrint" runat="server" Text="Print Report" CssClass="button" 
                                 Visible="false" onclick="BtnPrint_Click" />
                                </p>
                                </div>
                                
								<div  style="float:left; width:850px;">
									<table>
							            
								            <tr style="background-color:#000000; color:#ffffff">
								              <td></td> <td></td><td></td><td></td><td></td><td align="center" style="font-weight:bold">Sales History(Chart)</td>
								            </tr>
    	        						
                                         </table>
    	        						 <p style="table-layout:auto" align="center">
                <asp:Label ID="company" runat="server" Visible="false"></asp:Label>
                </br>
                <asp:Label ID="location" runat="server" Visible="false"></asp:Label>
               </p>
                          
						           
									
									<table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="center" style="text-align: center;font-weight:bold">Current Date :&nbsp; <%=String.Format("{0:dd/MM/yyyy HH:mm:ss}", CurrDateTime)%></td>
                                  </tr>
                                 <tr>
                                        <td align="left"><label id="LblRepo" runat="server" align="center"></label></td>
                                  </tr>
                                  
                                    </table>
                                   
					            </div>
                                <div  id = "saleschart_report" runat="server" visible="false">
    <asp:Table runat="server" ID="table1">
         <asp:TableRow>
            <asp:TableCell>
                <asp:chart id="Chart1" runat="server" Height="300px" Width="400px">
					<titles>
						<asp:Title ShadowOffset="3" Name="Title1" />
					</titles>
					<legends>
						<asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="True" Name="Default3" LegendStyle="Row" />
					</legends>
					<series>
						<asp:Series Name="Default3" />
					</series>
					<chartareas>
						<asp:ChartArea Name="ChartArea1" BorderWidth="0" />
					</chartareas>
				</asp:chart> 
                </asp:TableCell>
            <asp:TableCell>
            <asp:chart id="Chart2" runat="server" Height="300px" Width="400px">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Monthly Report" /></Titles>
            <Series>

                <asp:Series Name="Default3" />

            </Series>

            <ChartAreas>

             <asp:ChartArea Name="ChartArea2" BorderWidth="0" />

            </ChartAreas>
                </asp:chart> 
            </asp:TableCell>
         </asp:TableRow>
     </asp:Table>
    </div> 
    <div id = "Employee" runat="server" visible="false">
                                        <table width="100%" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="left" style="font-weight:bold"></td>
                                       </tr>
                                      <tr>
                                            <td align="left" style="font-weight:bold ">
                                            <asp:Label id="Label11" runat="server" Text="Individual Employee's Breakdown" Visible="false"></asp:Label>
                                            </td>
                                          
                                      </tr>
                                      
                                      <tr>
                                        <td align="left">
                                        <%--<div id = "Employees" runat="server" visible="false">--%>
                                             <asp:Table ID="table2" runat="server">
                                           
                                              
        
    </asp:Table></td>
                                    </tr>
                                          
                                          
                                    </table>
                                     <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="left" style="font-weight:bold"></td>
                                       </tr>
                                      <tr>
                                            <td align="left" style="font-weight:bold ">
                                            <asp:Label id="Label3" runat="server" Text="Total Employee Sales"></asp:Label>
                                            </td>
                                          
                                      </tr>
                                       <tr>
                                        <td align="left">

                                    <asp:Table runat="server" ID="table3">
         <asp:TableRow>
            <asp:TableCell>
                <asp:chart id="Chart3" runat="server" Height="300px" Width="400px">
					<titles>
						<asp:Title ShadowOffset="3" Name="Title1" />
					</titles>
					<legends>
						<asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="True" Name="Default3" LegendStyle="Row" />
					</legends>
					<series>
						<asp:Series Name="Default3" />
					</series>
					<chartareas>
						<asp:ChartArea Name="ChartArea1" BorderWidth="0" />
					</chartareas>
				</asp:chart> 
                </asp:TableCell>
            <asp:TableCell>
            <asp:chart id="Chart4" runat="server" Height="300px" Width="400px">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Monthly Report" /></Titles>
            <Series>

                <asp:Series Name="Default3" />

            </Series>

            <ChartAreas>

             <asp:ChartArea Name="ChartArea2" BorderWidth="0" />

            </ChartAreas>
                </asp:chart> 
            </asp:TableCell>
         </asp:TableRow>
     </asp:Table>
     </td>
     </tr>
     </table>
                                    </div>

                                    <div id = "SubCategory" runat="server" visible="false">
                                        <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="left" style="font-weight:bold"></td>
                                       </tr>
                                      <tr>
                                            <td align="left" style="font-weight:bold ">
                                            <asp:Label id="Label4" runat="server" Text="Sub-Category Sales Breakdown" Visible="false" Font-Bold="true"></asp:Label>
                                            </td>
                                          
                                      </tr>
                                      
                                      <tr>
                                        <td align="left">
                                        <%--<div id = "Employees" runat="server" visible="false">--%>
                                             <asp:Table ID="table4" runat="server">
                                              <asp:TableRow>
            <asp:TableCell>
                <asp:chart id="Chart5" runat="server" Height="300px" Width="400px">
					<titles>
						<asp:Title ShadowOffset="3" Name="Title1" />
					</titles>
					<legends>
						<asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="True" Name="Default3" LegendStyle="Row" />
					</legends>
					<series>
						<asp:Series Name="Default3" />
					</series>
					<chartareas>
						<asp:ChartArea Name="ChartArea1" BorderWidth="0" />
					</chartareas>
				</asp:chart> 
                </asp:TableCell>
            <asp:TableCell>
            <asp:chart id="Chart6" runat="server" Height="300px" Width="400px">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Monthly Report" /></Titles>
            <Series>

                <asp:Series Name="Default3" />

            </Series>

            <ChartAreas>

             <asp:ChartArea Name="ChartArea2" BorderWidth="0" />

            </ChartAreas>
                </asp:chart> 
            </asp:TableCell>
         </asp:TableRow>

                                           
                                              
        
    </asp:Table></td>
                                    </tr>
                                          
                                          
                                    </table>
                                     <table width="100%" border="0" align="right" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td align="left" style="font-weight:bold"></td>
                                       </tr>
                                      <tr>
                                            <td align="left" style="font-weight:bold ">
                                            <asp:Label id="Label5" runat="server" Text="Sub-Category Quantity Breakdown"></asp:Label>
                                            </td>
                                          
                                      </tr>
                                       <tr>
                                        <td align="left">

                                    <asp:Table runat="server" ID="table5">
         <asp:TableRow>
            <asp:TableCell>
                <asp:chart id="Chart7" runat="server" Height="300px" Width="400px">
					<titles>
						<asp:Title ShadowOffset="3" Name="Title1" />
					</titles>
					<legends>
						<asp:Legend Alignment="Center" Docking="Bottom" IsTextAutoFit="True" Name="Default3" LegendStyle="Row" />
					</legends>
					<series>
						<asp:Series Name="Default3" />
					</series>
					<chartareas>
						<asp:ChartArea Name="ChartArea1" BorderWidth="0" />
					</chartareas>
				</asp:chart> 
                </asp:TableCell>
            <asp:TableCell>
            <asp:chart id="Chart8" runat="server" Height="300px" Width="400px">
                <Titles>
                    <asp:Title ShadowOffset="3" Name="Monthly Report" /></Titles>
            <Series>

                <asp:Series Name="Default3" />

            </Series>

            <ChartAreas>

             <asp:ChartArea Name="ChartArea2" BorderWidth="0" />

            </ChartAreas>
                </asp:chart> 
            </asp:TableCell>
         </asp:TableRow>
     </asp:Table>
     </td>
     </tr>
     </table>
                                    
                                    </div>


					        </fieldset>
							
							<div class="clear"></div><!-- End .clear -->
							
						
			</div> <!-- End #tab2 -->   
                         
					
				</div> <!-- End .content-box-content -->
               
				
			</div> <!-- End .content-box -->
			
			

		</div>
  </asp:Content>

