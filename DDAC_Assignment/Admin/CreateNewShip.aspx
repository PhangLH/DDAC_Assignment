<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateNewShip.aspx.cs" Inherits="DDAC_Assignment.Admin.CreateNewShip" %>

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
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new ship</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxDesc" CssClass="col-md-2 control-label">Description</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="tboxDesc" TextMode="MultiLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tboxDesc" 
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The description field is required" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxName" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="tboxName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tboxDesc" 
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The name field is required" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxPrice" CssClass="col-md-2 control-label">Price(RM)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox id="tboxPrice" Text="0.00" runat="server" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tboxPrice" 
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The price field is required" />
                <asp:RegularExpressionValidator ID="regexPrice" runat="server" ValidationExpression="^(?:\d{1,14}|\d{1,11}\.\d\d)$"
                        ControlToValidate="tboxPrice" Display="Dynamic" CssClass="text-danger" ErrorMessage="Incorrect price format Example 123.00,123"></asp:RegularExpressionValidator>
            </div>
        </div>
        
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button runat="server" OnClick="CreateShip_Click" Text="Create" CssClass="btn btn-default" />
            </div>
        </div>
    </div>
</asp:Content>
