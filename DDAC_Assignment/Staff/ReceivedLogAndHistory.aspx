<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReceivedLogAndHistory.aspx.cs" Inherits="DDAC_Assignment.Staff.ReceivedLogAndHistory" %>

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

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div style="margin-top:20px;">

         <asp:GridView ID="gvReceived" CssClass="table table-striped table-bordered table-condensed" runat="server" 
            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="dsReceived">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="ID" />
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                <asp:BoundField DataField="CreationDateTime" HeaderText="CreationDateTime" SortExpression="CreationDateTime" />
                <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" />
                <asp:BoundField DataField="StartPort" HeaderText="StartPort" SortExpression="StartPort" />
                <asp:BoundField DataField="EndPort" HeaderText="EndPort" SortExpression="EndPort" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsReceived" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT ShippingRequest.sr_id AS ID, ShippingRequest.sr_status AS Status, ShippingRequest.sr_desc AS Description, ShippingRequest.sr_creationdatetime AS CreationDateTime, ShippingRequest.sr_price AS Price, Port.por_name AS StartPort, Port_1.por_name AS EndPort FROM ShippingRequest INNER JOIN Port ON ShippingRequest.sr_startportid = Port.por_id INNER JOIN Port AS Port_1 ON ShippingRequest.sr_endportid = Port_1.por_id WHERE (ShippingRequest.sr_status = 'Received')">
        </asp:SqlDataSource>

        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" id="lblEmptyTable" style="text-align: left;" CssClass="col-md-12 control-label" Font-Size="Large" Visible="false">There is no ReceivedShipping!</asp:Label>
            </div>
        </div>
     </div>

</asp:Content>
