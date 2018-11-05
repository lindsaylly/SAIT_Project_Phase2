<%@ Page Title="Sign Up" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="TravEx_WebApp.SignUp" %>
<%---------------------------------------------------
    This page written by Joel Barr
    -------------------------------------------------%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
    <div class="row mx-3 my-4">
            <div class="card bg-light border-success col-lg-4 p-0 order-lg-last">
                <div class="card-header p-3">
                    <h1>Login</h1>
                </div>
                <div class="card-body p-3">
                    <div class="form-row">
                        <div class="form-group col">
                            Username
                            <asp:TextBox ID="txtLoginEmail" CssClass="form-control" runat="server" ValidationGroup="loginGroup"></asp:TextBox>
                            <asp:CustomValidator ID="loginEmailvalidator" runat="server" ErrorMessage="Invalid username" CssClass="text-danger" ControlToValidate="txtLoginEmail" OnServerValidate="loginEmailvalidator_ServerValidate" ValidateEmptyText="True" ValidationGroup="loginGroup" Display="Dynamic" SetFocusOnError="True"></asp:CustomValidator>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col">
                            Password
                            <asp:TextBox ID="txtLoginPassword" CssClass="form-control" runat="server" ValidationGroup="loginGroup" TextMode="Password"></asp:TextBox>
                            <asp:CustomValidator ID="loginPasswordValidator" runat="server" ErrorMessage="Incorrect password" CssClass="text-danger" ControlToValidate="txtLoginPassword" ValidateEmptyText="True" ValidationGroup="loginGroup" Display="Dynamic" OnServerValidate="loginPasswordValidator_ServerValidate" SetFocusOnError="True"></asp:CustomValidator>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="form-group col">
                            <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="btn btn-success col-6" ValidationGroup="loginGroup" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                </div>
            </div> 

            <div class="col-lg"></div>

            <div class="card border-info bg-light col-lg-7 p-0 order-lg-first">
                <div class="card-header p-3">
                    <h1>Register</h1>
                </div>
                <div class="card-body p-3">
                    <div class="form-row">
                        <div class="col-md-6 form-group">
                            First Name*
                            <asp:TextBox ID="txtFirstName" CssClass="form-control" runat="server" Width="100%" ValidationGroup="registrationGroup"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="firstNameRequiredValidator" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First name is Required" ValidationGroup="registrationGroup" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 form-group ">
                            Last Name*
                            <asp:TextBox ID="txtLastName" CssClass="form-control" runat="server" Width="100%" ValidationGroup="registrationGroup"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="lastNameRequiredValidator" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last name is required" ValidationGroup="registrationGroup" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                    </div>

                    <div class="form-row">
                        <div class="col form-group">
                            Street Address*
                            <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server" Width="100%" ValidationGroup="registrationGroup"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="addressRequiredValidator" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address is required" ValidationGroup="registrationGroup" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-row">

                        <div class="col-8 col-md-5 form-group">
                            City*
                            <asp:TextBox ID="txtCity" CssClass="form-control" runat="server" Width="100%" ValidationGroup="registrationGroup"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="cityrequiredValidator" runat="server" ControlToValidate="txtCity" ErrorMessage="City is Required" ValidationGroup="registrationGroup" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>

                        <div class="col-4 col-md-2 form-group">
                            Province
                            <asp:DropDownList ID="drpProvince" runat="server" CssClass="form-control">
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
                            Postal Code*
                            <asp:TextBox ID="txtPostal" CssClass="form-control" runat="server" Width="100%" ValidationGroup="registrationGroup"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="postalRequiredValidator" runat="server" ControlToValidate="txtPostal" Display="Dynamic" ErrorMessage="Postal code is required" SetFocusOnError="True" ValidationGroup="registrationGroup" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="postalValidator" runat="server" ErrorMessage="Incorrect Postal code format" ControlToValidate="txtPostal" ValidationExpression="[a-zA-Z][0-9][a-zA-Z] ?[0-9][a-zA-Z][0-9]" ValidationGroup="registrationGroup" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-row">

                        <div class="col-md-6 form-group">
                            Home Phone*
                            <asp:TextBox ID="txtHomePhone" CssClass="form-control" runat="server" Width="100%" TextMode="Phone"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="phoneRequiredValidator" runat="server" ErrorMessage="Home phone is required" ControlToValidate="txtHomePhone" ValidationGroup="registrationGroup" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="homePhoneregExValidator" runat="server" ControlToValidate="txtHomePhone" ErrorMessage="Invalid home phone" ValidationExpression="(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}" ValidationGroup="registrationGroup" SetFocusOnError="True" CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>

                        <div class="col-md-6 form-group ">
                            Business Phone<asp:TextBox ID="txtWorkPhone" CssClass="form-control" runat="server" Width="100%" TextMode="Phone" ValidationGroup="registrationGroup"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="busPhoneRegExValidator" runat="server" ControlToValidate="txtWorkPhone" ErrorMessage="Invalid business phone" ValidationExpression="(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}" ValidationGroup="registrationGroup" SetFocusOnError="True" CssClass="text-danger" Display="Dynamic"></asp:RegularExpressionValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-md-6 form-group">
                            Email
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server" Width="100%" TextMode="Email" ValidationGroup="registrationGroup" CausesValidation="True"></asp:TextBox>
                        </div>
                        <div class="col-md-6 form-group">
                            Choose a Username*
                            <asp:TextBox ID="txtRegUserName" CssClass="form-control" runat="server" Width="100%" TextMode="SingleLine" ValidationGroup="registrationGroup" CausesValidation="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="userNameRequiredValidator" runat="server" ErrorMessage="Username required" ControlToValidate="txtRegUsername" ValidationGroup="registrationGroup" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col-md-6 form-group">
                            Password*
                            <asp:TextBox ID="txtPassword1" CssClass="form-control" runat="server" Width="100%" TextMode="Password" ValidationGroup="registrationGroup" CausesValidation="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="passwordRequiredValidator" runat="server" ControlToValidate="txtPassword1" Display="Dynamic" ErrorMessage="Password is required" SetFocusOnError="True" ValidationGroup="registrationGroup" CssClass="text-danger"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-md-6 form-group ">
                            Confirm Password*
                            <asp:TextBox ID="txtPassword2" CssClass="form-control" runat="server" Width="100%" TextMode="Password" ValidationGroup="registrationGroup" CausesValidation="True"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="confirmationRequiredValidator" runat="server" ControlToValidate="txtPassword2" ErrorMessage="Please confirm your password" SetFocusOnError="True" ValidationGroup="registrationGroup" CssClass="text-danger"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="passwordMatchValidator" runat="server" ErrorMessage="Passwords do not match" ControlToValidate="txtPassword2" ValidationGroup="registrationGroup" ControlToCompare="txtPassword1" Display="Dynamic" SetFocusOnError="True" CssClass="text-danger"></asp:CompareValidator>
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="col form-group">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="btn btn-primary" OnClick="btnRegister_Click" ValidationGroup="registrationGroup" />
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Errors found. Please check the following fields:" ShowSummary="False" ValidationGroup="registrationGroup" ShowModelStateErrors="False" />
                        </div>
                    </div>
                </div>
            </div>
        </div> 
</asp:Content>
