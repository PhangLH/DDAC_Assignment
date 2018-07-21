<%@ Page Title="Create New Shipping" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewShipping.aspx.cs" Inherits="DDAC_Assignment.NewShipping" %>

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
                <asp:DropDownList ID="ddlStartPort" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlStartPort_SelectedIndexChanged" runat="server" CssClass="form-control" DataSourceID="dsAllPort" DataTextField="por_name" DataValueField="por_location">
                    <asp:ListItem Value="" Selected="True" Text="Select Start Port..."></asp:ListItem>
                </asp:DropDownList>
                <asp:SqlDataSource ID="dsAllPort" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT por_name, por_latitude + ',' + por_longitude + ',' + CONVERT(varchar(10), por_id)  AS por_location FROM Port"></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlStartPort"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Value Required!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>
       
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlEndPort" CssClass="col-md-2 control-label">End Port</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlEndPort" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlEndPort_SelectedIndexChanged" runat="server" CssClass="form-control" DataSourceID="dsAllPort" DataTextField="por_name" DataValueField="por_location">
                    <asp:ListItem Value="" Selected="True" Text="Select End Port..."></asp:ListItem>
                </asp:DropDownList>
                <asp:CompareValidator runat="server" ControlToCompare="ddlStartPort" ControlToValidate="ddlEndPort"
                    CssClass="text-danger" Display="Dynamic" Operator="NotEqual" ErrorMessage="The start port and end port must not match." />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlEndPort"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Value Required!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlShip" CssClass="col-md-2 control-label">Ship</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlShip" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlShip_SelectedIndexChanged" runat="server" CssClass="form-control" DataSourceID="dsShip" DataTextField="shi_name" DataValueField="shi_info">
                    <asp:ListItem Value="" Selected="True" Text="Select Ship..."></asp:ListItem>
                </asp:DropDownList>   
                <asp:SqlDataSource ID="dsShip" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT CONVERT(varchar(10), shi_price) + ',' + CONVERT(varchar(10), shi_id) as shi_info, [shi_name] FROM [Ship]"></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlShip"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Value Required!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlContainer" CssClass="col-md-2 control-label">Container</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList ID="ddlContainer" AutoPostBack="True" OnSelectedIndexChanged="ddlContainer_SelectedIndexChanged" AppendDataBoundItems="True" runat="server" CssClass="form-control" DataSourceID="dsContainer" DataTextField="con_name" DataValueField="con_info">
                    <asp:ListItem Value="" Selected="True" Text="Select Container..."></asp:ListItem>
                </asp:DropDownList>   
                <asp:SqlDataSource ID="dsContainer" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT CONVERT(varchar(10), con_price) + ',' + CONVERT(varchar(10), con_id) as con_info, [con_name] FROM [Container]"></asp:SqlDataSource>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlContainer"
                CssClass="text-danger" Display="Dynamic" ErrorMessage="Value Required!" InitialValue=""></asp:RequiredFieldValidator>
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxDesc" CssClass="col-md-2 control-label">Description</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="tboxDesc" TextMode="MultiLine" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="tboxDesc" 
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The description field is required" />
            </div>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="tboxPrice" CssClass="col-md-2 control-label">Price(RM)</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="tboxPrice" Text="0.00" CssClass="form-control" Enabled="False" />
            </div>
        </div>
        
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button id="btnNext" runat="server" OnClick="Create_Click" Text="Create" CssClass="btn btn-default" />
            </div>
        </div>
    </div>

</asp:Content>
