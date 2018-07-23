<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CreateNewPort.aspx.cs" Inherits="DDAC_Assignment.Admin.CreateNewPort" %>

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
        <h4>Create a new port</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxName" CssClass="col-md-2 control-label">Name</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="tboxName" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tboxName" 
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The name field is required" />
            </div>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxLat" CssClass="col-md-2 control-label">Latitude</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="tboxLat" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tboxLat" 
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The latitude field is required" />
                <asp:RegularExpressionValidator ID="regexNumberDot" runat="server" ControlToValidate="tboxLat" CssClass="text-danger"
                    ErrorMessage="Invalid Value e.g. 123.123 or -123.123" Display="Dynamic" ValidationExpression="^[-+]?[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxLong" CssClass="col-md-2 control-label">Longitude</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="tboxLong" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tboxLong" 
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The longitude field is required" />
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="tboxLong" CssClass="text-danger"
                    ErrorMessage="Invalid Value e.g. 123.123" Display="Dynamic" ValidationExpression="^[-+]?[0-9]*\.?[0-9]+$"></asp:RegularExpressionValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlStaff" CssClass="col-md-2 control-label">Staff</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlStaff" AppendDataBoundItems="True" runat="server" CssClass="form-control" DataSourceID="dsStaff" DataTextField="use_name" DataValueField="use_id">
                    <asp:ListItem Value="" Selected="True" Text="Select Staff..."></asp:ListItem>
                </asp:DropDownList>   
                <asp:SqlDataSource ID="dsStaff" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT use_id, use_name FROM Users WHERE rol_id = 2;"></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStaff"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Value Required!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>
        
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button id="btnNext" runat="server" OnClick="CreatePort_Click" Text="Create" CssClass="btn btn-default" />
            </div>
        </div>
    </div>

</asp:Content>
