<%@ Page Language="C#"  MasterPageFile="~/innerpage.master" AutoEventWireup="true"  CodeFile="~/CloneLocation.aspx.cs" Inherits="PosLocation.CloneLocation" %>
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
					
					<h3 style="cursor: s-resize;"><asp:Literal ID="LblHead" runat="server" Text="Clone Location"></asp:Literal></h3>
					
				</div> <!-- End .content-box-header -->
				
				<div class="content-box-content">
                    <div id="Form" style="display:block" class="tab-content default-tab">
                        <p>
                            <label>From Location&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            To Location</label><asp:ListBox ID="FromLoation" runat="server"  AutoPostBack="false" SelectionMode="Single" Width="300px" Height="100px"></asp:ListBox>
                             <asp:RequiredFieldValidator ID="ReqdFromLocation" runat="server" ErrorMessage=" *" ControlToValidate="FromLoation"></asp:RequiredFieldValidator>
                             <asp:ListBox ID="ToLocation" runat="server"  AutoPostBack="false" SelectionMode="Single" Width="300px" Height="100px"></asp:ListBox>
                             <asp:RequiredFieldValidator ID="ReqdToLocation" runat="server" ErrorMessage=" *" ControlToValidate="ToLocation"></asp:RequiredFieldValidator>
                             <br /><small><asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label></small>
                        </p>
                        <div style="width:90%;" >
                            <div style="float:left; width:48%; height:220px; background-color:#E5E5E5; border:1px solid #EEEEEE;">
                            <div style="text-align:center; background-color :#CCCCCC; padding:10px; font-weight:bold;" >Management General</div>
                                <p style="padding-left:20px;">
                                    <input id ="ChkCookingOption" type="radio" runat="server" />Cooking Option(s)
                                </p>
                                <p style="padding-left:20px;">
                                    <input id ="ChkKitchen" type="radio" runat="server" />Kitchen(s)
                                </p>
                                <p style="padding-left:20px;">
                                    <input id ="ChkPrinter" type="radio" runat="server" />Printer(s)
                                </p>
                                <p style="padding-left:20px;">
                                    <input id ="ChkCourse" type="radio" runat="server" />Course(s)
                                </p>
                            </div>
                            <div style="float:right; width:46%; height:220px;background-color:#E5E5E5; border:1px solid #EEEEEE;">
                            <div style="text-align:center; background-color :#CCCCCC; padding:10px; font-weight:bold;" >Management Others</div>
                                <p style="padding-left:20px;">
                                    <input id ="ChkModifierLevel" type="radio" runat="server" />Modifier Level(s)
                                </p>
                                <p style="padding-left:20px;">
                                    <input id ="ChkModifier" type="radio" runat="server" />Modifier(s)
                                </p>
                                <p style="padding-left:20px;">
                                    <input id ="ChkCategory" type="radio" runat="server" />Category(s)
                                </p>
                                <p style="padding-left:20px;">
                                    <input id ="ChkSubCategory" type="radio" runat="server" />SubCategory(s)
                                </p>
                                <p style="padding-left:20px;">
                                    <input id ="ChkProduct" type="radio" runat="server" />Product(s)
                                </p>
                        </div>
                        </div>
                        <div style="clear:both"></div>
                    </div>
                    

				</div> <!-- End .content-box-content -->
				    <p style="padding-left:30px">
				    <asp:Label ID="Results" runat="server" ForeColor="Green"></asp:Label>
				    </p>
					<p style="padding-left:30px">
                        <asp:Button ID="BtnSave" runat="server" Text="Clone" CssClass="button" 
                            onclick="BtnSave_Click" />
                        <a href="Products.aspx" class="pageback">Back To Menu</a>
                        <asp:HiddenField ID="ProductID" runat="server" Value ="-1" />
                        <asp:HiddenField ID="Product_Img" runat="server" Value ="" />
                        <asp:HiddenField ID="Mode" runat="server" Value ="add" />
                        <asp:HiddenField ID="RestInitial" runat="server"  />
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

</asp:Content>