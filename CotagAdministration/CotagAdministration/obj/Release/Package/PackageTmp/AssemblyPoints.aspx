<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" Inherits="AssemblyPoints" Codebehind="AssemblyPoints.aspx.cs" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="AjaxToolKit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .style2
        {
            width: 186px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width:100%;">
                <tr>
                    <td class="style2">
                        Assembly Point Description:</td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtDescription" Display="Dynamic" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txtDescription" Display="Dynamic" 
                            ErrorMessage="Invalid Description" ValidationExpression="^[a-zA-Z\s*\d*\-*]+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Site:</td>
                    <td>
                        <asp:DropDownList ID="ddlSite" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="ddlSite" ErrorMessage="Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:HiddenField ID="txtId" runat="server" />
                    </td>
                    <td>
                        <asp:CheckBox ID="chkIsActive" runat="server" Text="Is Active" 
                            CausesValidation="True" Checked="True" />
                            
                        <AjaxToolKit:MutuallyExclusiveCheckBoxExtender ID="chkIsActive_MutuallyExclusiveCheckBoxExtender" 
                            runat="server" Enabled="True" Key="active" TargetControlID="chkIsActive">
                        </AjaxToolKit:MutuallyExclusiveCheckBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnNew" runat="server" onclick="btnNew_Click" 
                            onclientclick="(javascript:__doPostBack('','');" Text="Save" />
                        <asp:Button ID="btnEdit" runat="server" onclick="btnEdit_Click" 
                            onclientclick="(javascript:__doPostBack('','');" Text="Edit" Visible="False" />
                        <asp:Button ID="btnClear" runat="server" onclick="btnClear_Click" 
                            Text="Clear" />
                        <asp:Button ID="deleteButton" runat="server" CommandName="Delete" 
                            OnClick="deleteButton_Click" 
                            OnClientClick="return confirm('Are you sure you want to delete this item?');" 
                            Text="Delete" />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        </td>
                    <td>
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;</td>
                    <td style="padding-left:52%;">
                        
                        <asp:CheckBox ID="chkShow" runat="server" AutoPostBack="True" 
                            oncheckedchanged="chkShow_CheckedChanged" Text="Show Logically Deleted" />
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

<asp:Content ID="Content3" runat="server" 
    contentplaceholderid="GridViewContent">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:GridView ID="gvAssemblyPoint" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" onpageindexchanging="gvSite_PageIndexChanging" 
                onrowcommand="gvSite_RowCommand">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                        SortExpression="ID" />
                    <asp:BoundField DataField="Description" HeaderText="Description" 
                        ReadOnly="True" SortExpression="Description" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" 
                        SortExpression="IsActive" />
                    <asp:BoundField DataField="Createdby" HeaderText="Createdby" ReadOnly="True" 
                        SortExpression="Createdby" />
                    <asp:BoundField DataField="Createdon" HeaderText="Createdon" ReadOnly="True" 
                        SortExpression="Createdon" />
                    <asp:BoundField DataField="Updatedby" HeaderText="Updatedby" ReadOnly="True" 
                        SortExpression="Updatedby" />
                    <asp:BoundField DataField="Updatedon" HeaderText="Updatedon" ReadOnly="True" 
                        SortExpression="Updatedon" />
                    <asp:BoundField DataField="RecordVersion" HeaderText="RecordVersion" 
                        ReadOnly="True" SortExpression="RecordVersion" />
                    <asp:BoundField DataField="Site_Name" HeaderText="Site_Name" ReadOnly="True" 
                        SortExpression="Site_Name" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


