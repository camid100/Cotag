<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    Inherits="_Default" CodeBehind="Default.aspx.cs" %>

<asp:Content ID="HeadContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="MainContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <div class="menu" runat="server" id="menu">
                <a class="assembly" href="AssemblyPoints.aspx" runat="server" id="assembly" onclick="">
                    Assembly Points</a> <a class="cotagDesc" href="CotagDescription.aspx" runat="server"
                        id="cotagDesc" onclick="">Cotag Description</a> <a class="cotagDetails" href="CotagDetails.aspx"
                            runat="server" id="cotagDetails" onclick="">Cotag Details</a> <a class="locations"
                                href="Locations.aspx" runat="server" id="locations" onclick="">Locations</a>
                <a class="sites" href="Sites.aspx" runat="server" id="sites" onclick="">Sites</a>
                <a class="status" href="Status.aspx" runat="server" id="status" onclick="">Status</a></div>
        </ContentTemplate>
    </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
        DisplayAfter="200">
        <ProgressTemplate>
            <asp:Panel ID="Panel1" CssClass="overlay" runat="server">
                <asp:Panel ID="Panel2" CssClass="loader" runat="server">
                    <table>
                        <tr>
                            <td>
                                
                                <img id="Img1" src="~/Assets/loadingModal.gif" alt="Processing" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <b>Loading...</b>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>
