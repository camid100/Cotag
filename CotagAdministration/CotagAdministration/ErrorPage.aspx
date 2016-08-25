<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="CotagAdministration.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="Label1" runat="server" Font-Size="XX-Large" Text="An error occurred"></asp:Label>
<br />
<br />
<asp:Label ID="Label2" runat="server" Font-Size="Large" Text=""></asp:Label>
<br />
<asp:Label ID="Label3" runat="server" Text=""></asp:Label>
<br />
<br />
<asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Default.aspx">Home</asp:HyperLink>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="GridViewContent" runat="server">
</asp:Content>
