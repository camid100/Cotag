<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    Inherits="Sites" CodeBehind="Sites.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 118px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td class="style2">
                        Site Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription"
                            Display="Dynamic" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDescription"
                            Display="Dynamic" ErrorMessage="RegularExpressionValidator" ValidationExpression="^[a-zA-Z\s*\d*\-*]+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:HiddenField ID="txtId" runat="server" Visible="False" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" Checked="True" />
                        <asp:MutuallyExclusiveCheckBoxExtender ID="chkIsActive_MutuallyExclusiveCheckBoxExtender"
                            runat="server" Enabled="True" Key="active" TargetControlID="chkIsActive">
                        </asp:MutuallyExclusiveCheckBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="Save" OnClick="btnNew_Click" OnClientClick="(javascript:__doPostBack('','');" />
                        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" Visible="False"
                            OnClientClick="(javascript:__doPostBack('','');" />
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" />
                        <asp:Button ID="deleteButton" runat="server" CommandName="Delete" OnClick="deleteButton_Click"
                            OnClientClick="return confirm('Are you sure you want to delete this item?');"
                            Text="Delete" />
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
                        <asp:CheckBox ID="chkShow" runat="server" AutoPostBack="True" OnCheckedChanged="chkShow_CheckedChanged"
                            Text="Show Logically Deleted" />
                    </td>
                </tr>
            </table>
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
<asp:Content ID="Content3" ContentPlaceHolderID="GridViewContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvSite" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                OnPageIndexChanging="gvSite_PageIndexChanging" OnRowCommand="gvSite_RowCommand">
                <Columns>
                    <asp:BoundField DataField="SiteNo" HeaderText="SiteNo" ReadOnly="True" SortExpression="SiteNo" />
                    <asp:BoundField DataField="SiteDescription" HeaderText="SiteDescription" ReadOnly="True"
                        SortExpression="SiteDescription" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" SortExpression="IsActive" />
                    <asp:BoundField DataField="Createdby" HeaderText="Createdby" ReadOnly="True" SortExpression="Createdby" />
                    <asp:BoundField DataField="Createdon" HeaderText="Createdon" ReadOnly="True" SortExpression="Createdon" />
                    <asp:BoundField DataField="Updatedby" HeaderText="Updatedby" ReadOnly="True" SortExpression="Updatedby" />
                    <asp:BoundField DataField="Updatedon" HeaderText="Updatedon" ReadOnly="True" SortExpression="Updatedon" />
                    <asp:BoundField DataField="RecordVersion" HeaderText="RecordVersion" ReadOnly="True"
                        SortExpression="RecordVersion" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
