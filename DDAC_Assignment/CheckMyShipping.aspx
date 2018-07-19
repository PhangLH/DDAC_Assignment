<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckMyShipping.aspx.cs" Inherits="DDAC_Assignment.CheckMyShipping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:20px;">
        
        <asp:GridView ID="gvCheckShipping" CssClass="table table-striped table-bordered table-condensed" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="dsCheckShipping">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="CreationDateTime" HeaderText="CreationDateTime" SortExpression="CreationDateTime" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="StartPort" HeaderText="StartPort" SortExpression="StartPort" />
                <asp:BoundField DataField="EndPort" HeaderText="EndPort" SortExpression="EndPort" />
                <asp:BoundField DataField="shi_name" HeaderText="shi_name" SortExpression="shi_name" />
                <asp:BoundField DataField="con_name" HeaderText="con_name" SortExpression="con_name" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsCheckShipping" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ShippingRequest.sr_id AS ID, ShippingRequest.sr_status AS Status, ShippingRequest.sr_desc AS Description, ShippingRequest.sr_creationdatetime AS CreationDateTime, ShippingRequest.sr_price AS Price, Port.por_name AS StartPort, Port_1.por_name AS EndPort, Ship.shi_name, Container.con_name FROM ShippingRequest INNER JOIN Port ON ShippingRequest.sr_startportid = Port.por_id INNER JOIN Port AS Port_1 ON ShippingRequest.sr_endportid = Port_1.por_id INNER JOIN Ship ON ShippingRequest.shi_id = Ship.shi_id INNER JOIN Container ON ShippingRequest.con_id = Container.con_id WHERE (ShippingRequest.use_id = @userid)">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="userid" SessionField="UserId" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
</asp:Content>
