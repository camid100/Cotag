<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="CotagAdministration.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="Woopsie Daisy!"></asp:Label>
<br />
<br />
<asp:Label ID="Label2" runat="server" Font-Size="Large" Text="Looks like something went completely wrong!"></asp:Label>
<br />
<asp:Label ID="Label3" runat="server" Text="But don't worry this can happen to the best of us and it just happened to you!"></asp:Label>
<br />
<br />
<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx">Try going back to home</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="GridViewContent" runat="server">
</asp:Content>
