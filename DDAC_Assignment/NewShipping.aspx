<%@ Page Title="Create New Shipping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewShipping.aspx.cs" Inherits="DDAC_Assignment.NewShipping" %>

<asp:Content ID="content_NewShipping" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlStartPort" CssClass="col-md-2 control-label">Start Port</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlStartPort" AppendDataBoundItems="true" runat="server" CssClass="form-control" DataSourceID="dsAllPort" DataTextField="por_name" DataValueField="por_id">
                    <asp:ListItem Value="" Selected="True" Text="Select Start Port..."></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="dsAllPort" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [por_name], [por_id] FROM [Port]"></asp:SqlDataSource>
            </div>
        </div>
       
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlEndPort" CssClass="col-md-2 control-label">End Port</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlEndPort" AppendDataBoundItems="true" runat="server" CssClass="form-control" DataSourceID="dsAllPort" DataTextField="por_name" DataValueField="por_id">
                    <asp:ListItem Value="" Selected="True" Text="Select End Port..."></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator runat="server" ControlToCompare="ddlStartPort" ControlToValidate="ddlEndPort"
                    CssClass="text-danger" Display="Dynamic" Operator="NotEqual" ErrorMessage="The start port and end port must not match." />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlShip" CssClass="col-md-2 control-label">Ship</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlShip" AppendDataBoundItems="True" runat="server" CssClass="form-control" DataSourceID="dsShip" DataTextField="shi_name" DataValueField="shi_id">
                    <asp:ListItem Value="" Selected="True" Text="Select Ship..."></asp:ListItem>
                </asp:DropDownList>   
                <asp:SqlDataSource ID="dsShip" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [shi_id], [shi_name] FROM [Ship]"></asp:SqlDataSource>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlContainer" CssClass="col-md-2 control-label">Container</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlContainer" AppendDataBoundItems="True" runat="server" CssClass="form-control" DataSourceID="dsContainer" DataTextField="con_name" DataValueField="con_id">
                    <asp:ListItem Value="" Selected="True" Text="Select Container..."></asp:ListItem>
                </asp:DropDownList>   
                <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [con_id], [con_name] FROM [Container]"></asp:SqlDataSource>
            </div>
        </div>

        
        
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button id="btnNext" runat="server" OnClick="Next_Click" Text="Next" CssClass="btn btn-default" />
            </div>
        </div>
    </div>

</asp:Content>
