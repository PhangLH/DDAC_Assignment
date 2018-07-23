<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckMyShipping.aspx.cs" Inherits="DDAC_Assignment.CheckMyShipping" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        var appInsights=window.appInsights||function(a){
        function b(a){c[a]=function(){var b=arguments;c.queue.push(function(){c[a].apply(c,b)})}}var c={config:a},d=document,e=window;setTimeout(function(){var b=d.createElement("script");b.src=a.url||"https://az416426.vo.msecnd.net/scripts/a/ai.0.js",d.getElementsByTagName("script")[0].parentNode.appendChild(b)});try{c.cookie=d.cookie}catch(a){}c.queue=[];for(var f=["Event","Exception","Metric","PageView","Trace","Dependency"];f.length;)b("track"+f.pop());if(b("setAuthenticatedUserContext"),b("clearAuthenticatedUserContext"),b("startTrackEvent"),b("stopTrackEvent"),b("startTrackPage"),b("stopTrackPage"),b("flush"),!a.disableExceptionTracking){f="onerror",b("_"+f);var g=e[f];e[f]=function(a,b,d,e,h){var i=g&&g(a,b,d,e,h);return!0!==i&&c["_"+f](a,b,d,e,h),i}}return c
        }({
            instrumentationKey:"1d9677f0-20d1-43f6-bb8b-b03395ad721d"
        });
    
        window.appInsights=appInsights,appInsights.queue&&0===appInsights.queue.length&&appInsights.trackPageView();
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-top:20px;">
        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="tboxSearch" CssClass="col-md-2 control-label">Search By Port Name</asp:Label>
                <div class="col-md-10">
                    <asp:TextBox runat="server" ID="tboxSearch" CssClass="form-control" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="Search" CssClass="btn btn-default" />
                </div>
            </div>
        </div>
        

        <asp:GridView ID="gvCheckShipping" CssClass="table table-striped table-bordered table-condensed" runat="server" 
            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="dsCheckShipping" OnRowCommand="gvCheckShipping_RowCommand">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="CreationDateTime" HeaderText="CreationDateTime" SortExpression="CreationDateTime" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="StartPort" HeaderText="StartPort" SortExpression="StartPort" />
                <asp:BoundField DataField="EndPort" HeaderText="EndPort" SortExpression="EndPort" />
                <asp:BoundField DataField="ShipName" HeaderText="ShipName" SortExpression="ShipName" />
                <asp:BoundField DataField="ContainerName" HeaderText="ContainerName" SortExpression="ContainerName" />
                <asp:ButtonField CommandName="DeleteRow" Text="Delete" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsCheckShipping" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" FilterExpression="StartPort LIKE '%{0}%' or EndPort LIKE '%{0}%'" SelectCommand="SELECT ShippingRequest.sr_id AS ID, ShippingRequest.sr_status AS Status, ShippingRequest.sr_desc AS Description, ShippingRequest.sr_creationdatetime AS CreationDateTime, ShippingRequest.sr_price AS Price, Port.por_name AS StartPort, Port_1.por_name AS EndPort, Ship.shi_name AS ShipName, Container.con_name AS ContainerName FROM ShippingRequest INNER JOIN Port ON ShippingRequest.sr_startportid = Port.por_id INNER JOIN Port AS Port_1 ON ShippingRequest.sr_endportid = Port_1.por_id INNER JOIN Ship ON ShippingRequest.shi_id = Ship.shi_id INNER JOIN Container ON ShippingRequest.con_id = Container.con_id WHERE (ShippingRequest.use_id = @userid)">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="userid" SessionField="UserId" />
            </SelectParameters>
            <filterparameters>
                <asp:controlparameter controlid="tboxSearch" propertyname="Text" />
            </filterparameters>
        </asp:SqlDataSource>

        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" id="lblEmptyTable" style="text-align: left;" CssClass="col-md-12 control-label" Font-Size="Large" Visible="false">There is no ShippingRequest!</asp:Label>
            </div>
        </div>

    </div>

</asp:Content>
