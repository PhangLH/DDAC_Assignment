<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckContainer.aspx.cs" Inherits="DDAC_Assignment.Admin.CheckContainer" %>

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

         <asp:GridView ID="gvCheckContainer" CssClass="table table-striped table-bordered table-condensed" runat="server" 
            AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="con_id" DataSourceID="dsCheckContainer" OnRowCommand="gvCheckContainer_RowCommand">
            <Columns>
                <asp:BoundField DataField="con_id" HeaderText="ID" InsertVisible="False" ReadOnly="True" SortExpression="con_id" />
                <asp:BoundField DataField="con_name" HeaderText="Name" SortExpression="con_name" />
                <asp:BoundField DataField="con_price" HeaderText="Price" SortExpression="con_price" />
                <asp:BoundField DataField="con_size" HeaderText="Max Weight(KG)" SortExpression="con_size" />
                <asp:ButtonField CommandName="DeleteRow" Text="Delete" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="dsCheckContainer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [Container]">
        </asp:SqlDataSource>

        <div class="form-horizontal">
            <div class="form-group">
                <asp:Label runat="server" id="lblEmptyTable" style="text-align: left;" CssClass="col-md-12 control-label" Font-Size="Large" Visible="false">There is no Container!</asp:Label>
            </div>
        </div>

     </div>

</asp:Content>
