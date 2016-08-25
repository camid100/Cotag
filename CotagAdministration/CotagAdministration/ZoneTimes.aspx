<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    Inherits="ZoneTimes" CodeBehind="ZoneTimes.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style2
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td class="style2">
                        Zone:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlZones" runat="server" 
                            onselectedindexchanged="ddlZones_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Site:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSites" runat="server" OnSelectedIndexChanged="ddlSites_SelectedIndexChanged"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Access Point:
                    </td>
                    <td>
                        <table>
                            <thead>
                                <tr>
                                    <th>
                                        Readers
                                    </th>
                                    <th>
                                        Times
                                    </th>
                                    
                                </tr>
                            </thead>
                            <tr>
                                <td>
                                    <asp:ListBox ID="lbAccessPoints" runat="server" Height="187px" Width="202px"></asp:ListBox>
                                </td>
                                <td>
                                    <asp:ListBox ID="lbTimes" runat="server" Height="187px" Width="208px"></asp:ListBox>
                                </td>
                               
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                <td></td>
                 <td align="center">
                                    <asp:Button ID="btnInsert" runat="server" Text="Insert" 
                                        onclick="btnInsert_Click" Width="60px" />
                                    <asp:Button ID="btnRemove" runat="server" Text="Remove" 
                                        onclick="btnRemove_Click" />
                                </td>
                </tr>
                <tr>
                <td>Current</td>
                <td>
                  <asp:ListBox ID="lbCurrent" runat="server" Height="187px" Width="416px"></asp:ListBox>
                </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:HiddenField ID="txtId" runat="server" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" Checked="True" />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td class="style2">
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="Save" 
                            OnClientClick="(javascript:__doPostBack('','');" onclick="btnNew_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                </tr>
            </table>
            <br />
                <h2>Export</h2>
   <asp:CheckBoxList ID="cclZoneTimes"     CellPadding="2"
           CellSpacing="2"
           RepeatColumns="3"
           RepeatDirection="Vertical"
           TextAlign="right"
 runat="server"  AutoPostBack="true">
                        </asp:CheckBoxList>
                        <asp:Button runat="server" id="btnExport" Text="Export" 
                onclick="btnExport_Click" />
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


<%--<asp:Content ID="Content3" ContentPlaceHolderID="GridViewContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvZones" runat="server" AllowPaging="True" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" SortExpression="ID" />
                    <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True"
                        SortExpression="Description" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>--%>
