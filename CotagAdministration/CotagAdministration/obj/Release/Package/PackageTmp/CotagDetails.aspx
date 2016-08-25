<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    Inherits="CotagDetails" CodeBehind="CotagDetails.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style2
        {
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
         <table style="width: 100%;">
                <tr>
                    <td class="style2">
                        Type</td>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server" AutoPostBack="True" 
                            onselectedindexchanged="ddlType_SelectedIndexChanged">
                            <asp:ListItem>Please Choose a Type...</asp:ListItem>
                            <asp:ListItem>C-Tag</asp:ListItem>
                            <asp:ListItem>T-Tag</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
         </table>
        <div id="div1" runat="server">
            <table style="width: 100%;">
                <tr>
                    <td class="style2">
                        Cotag No:
                    </td>
                    <td>
                        <asp:TextBox ID="txtCotagNo" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                            ControlToValidate="txtCotagNo" Display="Dynamic" ErrorMessage="Invalid Input!" 
                            ValidationExpression="^[\d+]+$"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCotagNo" Display="Dynamic" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        ID No:</td>
                    <td>
                        <asp:TextBox ID="txtIDNo" runat="server" ontextchanged="txtIDNo_TextChanged"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="btnCheck" runat="server" CausesValidation="False" 
                            OnClick="btnCheck_Click" Text="Check" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtIDNo" Display="Dynamic" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                            ControlToValidate="txtIDNo" ErrorMessage="Invalid Input" 
                            ValidationExpression="^\d*[a-zA-Z]+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Name:</td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Surname:</td>
                    <td>
                        <asp:TextBox ID="txtSurname" runat="server" Enabled="False"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Start Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtStartDate"
                            Display="Dynamic" ErrorMessage="Required!"></asp:RequiredFieldValidator>
                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate" />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        End Date:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEndDate" />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Assembly Point:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAssemblyPoint" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlAssemblyPoint"
                            Display="Dynamic" ErrorMessage="Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Telephone:
                    </td>
                    <td>
                        <asp:TextBox ID="txtTelephone" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtTelephone"
                            ErrorMessage="Invalid Input!" ValidationExpression="^[\d*]+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Mobile:
                    </td>
                    <td>
                        <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtMobile"
                            ErrorMessage="Invalid Input!" ValidationExpression="^[\d*]+$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        Cotag Description
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCotagDescription" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlCotagDescription"
                            Display="Dynamic" ErrorMessage="Required!" InitialValue="-1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
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
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnNew" runat="server" Text="Save" OnClick="btnNew_Click" OnClientClick="(javascript:__doPostBack('','');" />
                        <asp:Button ID="btnEdit" runat="server" OnClick="btnEdit_Click" Text="Edit" Visible="False"
                            OnClientClick="(javascript:__doPostBack('','');" />
                        <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click" Text="Clear" CausesValidation="False" />
                        <asp:Button ID="deleteButton" runat="server" CommandName="Delete" 
                            OnClick="deleteButton_Click" 
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
          </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnCheck" EventName="Click" />
        </Triggers>
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
            <asp:GridView ID="gvCotagDetail" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                OnPageIndexChanging="gvCotagDetail_PageIndexChanging" OnRowCommand="gvCotagDetail_RowCommand">
                <Columns>
                    <asp:BoundField DataField="CotagNo" HeaderText="CotagNo" ReadOnly="True" SortExpression="CotagNo" />
                    <asp:BoundField DataField="IDNo" HeaderText="IDNo" ReadOnly="True" SortExpression="IDNo" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
                    <asp:BoundField DataField="Surname" HeaderText="Surname" ReadOnly="True" SortExpression="Surname" />
                    <asp:BoundField DataField="StartDate" HeaderText="StartDate" ReadOnly="True" SortExpression="StartDate" />
                    <asp:BoundField DataField="EndDate" HeaderText="EndDate" ReadOnly="True" SortExpression="EndDate" />
                    <asp:BoundField DataField="AssemblyPoint_Name" HeaderText="Assembly Point" ReadOnly="True"
                        SortExpression="AssemblyPoint_Name" />
                    <asp:BoundField DataField="telephone" HeaderText="Telephone" ReadOnly="True" SortExpression="telephone" />
                    <asp:BoundField DataField="mobile" HeaderText="Mobile" ReadOnly="True" SortExpression="mobile" />
                    <asp:BoundField DataField="cotagDesc_Name" HeaderText="Cotag Description" ReadOnly="True"
                        SortExpression="cotagDesc_Name" />
                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" ReadOnly="True" SortExpression="IsActive" />
                    <asp:BoundField DataField="Createdby" HeaderText="Createdby" ReadOnly="True" SortExpression="Createdby" Visible="false"/>
                    <asp:BoundField DataField="Createdon" HeaderText="Createdon" ReadOnly="True" SortExpression="Createdon" Visible="false"/>
                    <asp:BoundField DataField="Updatedby" HeaderText="Updatedby" ReadOnly="True" SortExpression="Updatedby" Visible="false"/>
                    <asp:BoundField DataField="Updatedon" HeaderText="Updatedon" ReadOnly="True" SortExpression="Updatedon" Visible="false"/>
                    <asp:BoundField DataField="Type" HeaderText="Type" ReadOnly="True" SortExpression="Type" />
                    <asp:BoundField DataField="RecordVersion" HeaderText="RecordVersion" ReadOnly="True" Visible="false"
                        SortExpression="RecordVersion" />
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
