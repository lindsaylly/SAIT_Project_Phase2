<%@ Page Language="C#" Title="Booking Summary" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderSummary.aspx.cs" Inherits="TravEx_WebApp.OrderSummary" %>
<%---------------------------------------------------
    This page written by Carol He
    -------------------------------------------------%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">          
    <link href="styles/OrderSummary.css" rel="stylesheet" />

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" runat="server">
       
        <h1 class="jumbotron text-center text-secondary">Booking Summary</h1>
        <h4 class="text-secondary">Here are the list of your bookings.</h4>
        <br />
        <asp:GridView CssClass="table table-striped" ID="GridView6" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource8">
            <Columns>
                <asp:BoundField DataField="BookingId" HeaderText="BookingId" SortExpression="BookingId" />
                <asp:BoundField DataField="BookingDate" HeaderText="BookingDate" SortExpression="BookingDate" DataFormatString="{0:D}" />
                <asp:BoundField DataField="BookingNo" HeaderText="BookingNo" SortExpression="BookingNo" />
                <asp:BoundField DataField="CustomerId" HeaderText="CustomerId" SortExpression="CustomerId" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetBookingsByCustomerId" TypeName="TravEx_WebApp.App_Code.BookingDB">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="143" Name="CustomerId" SessionField="CustomerId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
            <br />
            <br />
        <asp:DropDownList CssClass="form-control col-sm-3 bg-mute" ID="ddlBookingId" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource1" DataTextField="BookingId" DataValueField="BookingId">
        </asp:DropDownList>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetBookingsByCustomerId" TypeName="TravEx_WebApp.App_Code.BookingDB">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="143" Name="CustomerId" SessionField="CustomerId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:GridView CssClass="table table-striped" ID="GridView7" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2">
            <Columns>
                <asp:BoundField DataField="BookingDetailId" HeaderText="BookingDetailId" SortExpression="BookingDetailId" />
                <asp:BoundField DataField="TripStart" HeaderText="TripStart" SortExpression="TripStart" DataFormatString="{0:D}" />
                <asp:BoundField DataField="TripEnd" HeaderText="TripEnd" SortExpression="TripEnd" DataFormatString="{0:D}" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="Destination" HeaderText="Destination" SortExpression="Destination" />
                <asp:BoundField DataField="ProdName" HeaderText="ProdName" SortExpression="ProdName" />
                <asp:BoundField DataField="SupName" HeaderText="SupName" SortExpression="SupName" />
                <asp:BoundField DataField="BasePrice" HeaderText="BasePrice" SortExpression="BasePrice" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="FeeAmt" HeaderText="FeeAmt" SortExpression="FeeAmt" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="TaxAmt" HeaderText="TaxAmt" SortExpression="TaxAmt" DataFormatString="{0:C2}" />
                <asp:BoundField DataField="TotalPrice" HeaderText="TotalPrice" SortExpression="TotalPrice" DataFormatString="{0:C2}" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetOrderSummaryByBookingId" TypeName="TravEx_WebApp.App_Code.OrderSummaryDB">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlBookingId" Name="BookingId" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <br />

</asp:Content>






    
       

 
     
   

