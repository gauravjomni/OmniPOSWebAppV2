<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="AddCompany.aspx.cs" Inherits="Admin_AddCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="main-content">
        <!-- Main Content Section with everything -->
        <!-- Page Head -->
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
                    <asp:Literal ID="LblHead" runat="server" Text="Add/Update Company"></asp:Literal></h3>
               
            </div>
            
            <!-- End .content-box-header -->
            <div class="content-box-content">
             <div class="clear">
                           
                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div id="Form">
                    <fieldset>
                        <!-- Set class to "column-left" or "column-right" on fieldsets to divide the form into columns -->
                        <p>
                        <table>
                        <tr><td style="width: 168px; ">
                         <label>
                                Name</label>
                                </td>
                                <td>  <input class="text-input small-input" id="txName" type="text" runat="server" />
                            <asp:RequiredFieldValidator ID="ReqdFirstName" runat="server" ErrorMessage=" *" 
                                ControlToValidate="txName" ForeColor="Red"></asp:RequiredFieldValidator>
                                </td>
                                </tr>
                        
                        <tr>
                        <td style="width: 168px; ">   <label>
                                Code</label>
                                </td>
                                <td> <input class="text-input small-input" id="txtCode" type="text" runat="server" 
                                maxlength="6" size="6"/>
                                    <asp:RequiredFieldValidator ID="ReqdLastName" runat="server" ErrorMessage=" *" ControlToValidate="txtCode"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegCode" runat="server" ErrorMessage="Please use only Alphanumeric characters without spaces!"
                                        ValidationExpression="^[a-zA-Z0-9]+$" ControlToValidate="txtCode" 
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                    <small>
                                    <br/>
                                <asp:Label ID="LblCodeName" runat="server" ForeColor="Red"></asp:Label></small> 
                                 </td>
                                 </tr>
                          
                           
                        
                        <tr><td style="width: 168px"> 
                        <label>
                                Database Name</label>
                        </td>
                        <td> 
                        <input class="text-input small-input" id="txtDBName" type="text" runat="server" />
                            <asp:RequiredFieldValidator ID="ReqdName" runat="server" ErrorMessage=" *" 
                                ControlToValidate="txtDBName" ForeColor="Red"></asp:RequiredFieldValidator>
                            <br />
                            <small>
                                <asp:Label ID="LblDBName" runat="server" ForeColor="Red"></asp:Label></small>
                         </td>
                         </tr>
                            
                        <tr><td style="width: 168px">    <label>
                                Database User Name</label></td>
                                <td>  
                                 <input class="text-input small-input" id="txtUserName" type="text" runat="server" />
                                </td>
                                </tr>
                         
                           
                            
                        <tr>
                        <td style="width: 168px">
                          <label>
                                Database Password</label>
                         </td>
                         <td> 
                                                     <input class="text-input small-input" id="txtPassword" name="txtPassword" type="text"
                                runat="server" />
                            <asp:RequiredFieldValidator ID="ReqdPassword" runat="server" ErrorMessage=" *" 
                                ControlToValidate="txtPassword" ForeColor="Red"></asp:RequiredFieldValidator>
                                <br />
                                 <small>
                                <asp:Label ID="LblPassword" runat="server" ForeColor="Red"></asp:Label></small>
                          </td>
                          </tr>
                            
                           
                        <tr><td style="width: 168px"><label>
                                Database Password (Again)</label> </td>
                                <td>  
                                <input class="text-input small-input" id="txtPasswordConf" name="txtPasswordConf"
                                type="text" runat="server" />
                            <asp:RequiredFieldValidator ID="ReqdtxtPasswordConf" runat="server" ErrorMessage=" *"
                                ControlToValidate="txtPasswordConf" ForeColor="Red"></asp:RequiredFieldValidator>
                            <br />
                            <small>
                                <asp:Label ID="LblPasswordConf" runat="server" ForeColor="Red"></asp:Label></small>
                       
                                </td>
                                </tr>
                                <tr><td style="width: 168px"><label>
                                Company's Email</label> </td>
                                <td>  
                                <input class="text-input small-input" id="txtEmailAdd" name="txtEmailAdd"
                                type="text" runat="server" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ForeColor = "Red"
                                        ErrorMessage=" Please Enter Valid Email ID" ControlToValidate="txtEmailAdd" 
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            <br />
                            
                                
                       
                                </td>
                                </tr>
                            
                            
                            <tr><td style="width: 168px"> </td><td>  </td></tr>
                            </table>
                            <asp:Button ID="BtnAdd" runat="server" Text="Add New Company Info" 
                                CssClass="button" OnClick="BtnAdd_Click" />
                            <asp:Button ID="BtnUpdate" runat="server" Text="Update Company Info" 
                                CssClass="button" OnClick="BtnUpdate_Click" Visible="False" />
<label> </label>
                             <asp:Button ID="BtnCreate" runat="server" Text="Create/Upgrade Company" 
                                CssClass="button" OnClick="BtnCreate_Click" Width="187px" 
                                Visible="False" />
                           
                            <asp:HiddenField ID="UserID" runat="server" Value="-1" />
                            <asp:HiddenField ID="Mode" runat="server" Value="add" />
                            <asp:HiddenField ID="RestInitial" runat="server" />
                        </p>
                    </fieldset>
                    <div class="clear">
                    </div>
                    <!-- End .clear -->
                </div>
                <!-- End #tab2 -->
            </div>
            <!-- End .content-box-content -->
        </div>
        <!-- End .content-box -->
    </div>
</asp:Content>
