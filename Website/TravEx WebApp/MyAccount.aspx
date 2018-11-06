<%-- Author: Lindsay --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="TravEx_WebApp.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">

        <div class="row m-5">
            <div class="card bg-light border-success col-lg-7 p-0">
                <div class="card-header p-3">
                    <h2 class="row">Personal Information</h2>
                </div>
                <div class="card-body p-3">
                    <div class="form-row">
                        <div class="col-md-6 form-group">
                            First Name
                            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server" Width="100%" ValidationGroup="vgUpdate"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrFirstName" runat="server" ErrorMessage="First Name is required." ControlToValidate="txtFirstName" ValidationGroup="vgUpdate" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 form-group ">
                            Last Name
                            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server" Width="100%" ValidationGroup="vgUpdate"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrLN" runat="server" ErrorMessage="Last Name is required." ControlToValidate="txtLastName" ValidationGroup="vgUpdate" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col form-group">
                            Street Address
                            <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrAddress" runat="server" ErrorMessage="Street Address is required." ControlToValidate="txtAddress" ValidationGroup="vgUpdate" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-8 col-md-5 form-group">
                            City
                            <asp:TextBox ID="txtCity" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrCity" runat="server" ErrorMessage="City is required." ControlToValidate="txtCity" ValidationGroup="vgUpdate" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-4 col-md-2 form-group"> 
                            Prov
                            <asp:DropDownList ID="ddlProv" CssClass="form-control" runat="server" Width="100%">
                                <asp:ListItem Value="AB">AB</asp:ListItem>
                                <asp:ListItem Value="BC">BC</asp:ListItem>
                                <asp:ListItem Value="MB">MB</asp:ListItem>
                                <asp:ListItem Value="NB">NB</asp:ListItem>
                                <asp:ListItem Value="NL">NL</asp:ListItem>
                                <asp:ListItem Value="NT">NT</asp:ListItem>
                                <asp:ListItem Value="NS">NS</asp:ListItem>
                                <asp:ListItem Value="NU">NU</asp:ListItem>
                                <asp:ListItem Value="ON">ON</asp:ListItem>
                                <asp:ListItem Value="PE">PE</asp:ListItem>
                                <asp:ListItem Value="QC">QC</asp:ListItem>
                                <asp:ListItem Value="SK">SK</asp:ListItem>
                                <asp:ListItem Value="YT">YT</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-5 form-group">
                            Postal Code
                            
                            <asp:TextBox ID="txtPostal" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrPostal" runat="server" ErrorMessage="Postal Code is required." ControlToValidate="txtPostal" ValidationGroup="vgUpdate" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="vRegExPostal" runat="server" Display="Dynamic" ErrorMessage="Invalid Postal Code." SetFocusOnError="True" ValidationExpression="[a-zA-Z][0-9][a-zA-Z] ?[0-9][a-zA-Z][0-9]" ValidationGroup="vgUpdate" ControlToValidate="txtPostal" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-md-6 form-group">
                            Home Phone
                            <asp:TextBox ID="txtHomePhone" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrHomePage" runat="server" ErrorMessage="Home Phone is required." ControlToValidate="txtHomePhone" ValidationGroup="vgUpdate" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="vRegExHomePage" runat="server" Display="Dynamic" ErrorMessage="Invalid Phone Number." SetFocusOnError="True" ValidationExpression="(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}" ValidationGroup="vgUpdate" ControlToValidate="txtHomePhone" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-md-6 form-group ">
                            Business Phone
                            <asp:TextBox ID="txtBusPhone" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vRegExBusPhone" runat="server" Display="Dynamic" ErrorMessage="Invalid Phone Number." SetFocusOnError="True" ValidationExpression="(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}" ValidationGroup="vgUpdate" ControlToValidate="txtBusPhone" ForeColor="Red"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col form-group">
                            Email
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Width="100%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="vRegExEmail" runat="server" ErrorMessage="Invalid Email" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" ControlToValidate="txtEmail" ForeColor="Red" ValidationGroup="vgUpdate"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <br/>
                    <div class="form-row">
                        <div class="col-md-6 form-group">
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-warning mb-2" Text="Update" Width="100%" OnClick="btnUpdate_Click" ValidationGroup="vgUpdate" />
                        </div>
                        <div class="col-md-6 form-group">
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning mb-2" Text="Cancel" Width="100%" OnClick="btnCancel_Click"/>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col">
                        <asp:Label ID="lblUpdateError" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg">
                
            </div>

            <div class="card bg-light border-danger col-lg-4 p-0">
                <div class="card-header p-3">
                    <h2 class="row">Reset Password</h2>
                </div>
                <div class="card-body p-3">
                    <div class="form-row">
                        <div class="col form-group">
                            Current Password
                            <asp:TextBox ID="txtOldPassword" CssClass="form-control" runat="server" TextMode="Password" ValidationGroup="vgReset"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrOldPwd" runat="server" ErrorMessage="Current password is required." ControlToValidate="txtOldPassword" ValidationGroup="vgReset" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>     
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col form-group">
                            New Password
                            <asp:TextBox ID="txtNewPassword" CssClass="form-control" runat="server" ValidationGroup="vgReset" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrNewPwd" runat="server" ErrorMessage="Please enter a New password." ControlToValidate="txtNewPassword" ValidationGroup="vgReset" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>  
                            
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col form-group">
                            Confirm Password
                            <asp:TextBox ID="txtConfirmPwd" CssClass="form-control" runat="server" ValidationGroup="vgReset" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="vrConfrimPwd" runat="server" ErrorMessage="Please enter your new password again." ControlToValidate="txtConfirmPwd" ValidationGroup="vgReset" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>  
                        </div>
                    </div>
                    <br/>
                    <div class="form-row">
                        <div class="col-md-6 form-group">
                            <asp:Button ID="btnReset" runat="server" CssClass="btn btn-warning mb-2" Text="Reset" Width="100%" OnClick="btnSubmit_Click" ValidationGroup="vgReset" />
                        </div>
                        <div class="col-md-6 form-group">
                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning mb-2" Text="Clear" Width="100%" OnClick="btnClear_Click1"/>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col">
                        <asp:Label ID="lblResetError" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
