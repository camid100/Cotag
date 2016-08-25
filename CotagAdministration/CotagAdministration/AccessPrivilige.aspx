<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true"
    Inherits="AccessPrivilige" CodeBehind="AccessPrivilige.aspx.cs" %>

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
                      Zone:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlZone" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtNewZone" runat="server"></asp:TextBox>
                       </td>

                </tr>
                  <tr>
                    <td class="style2">
                        Site:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="true"
                            onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CheckBox ID="cbSelectAll" Text="Select All" runat="server" AutoPostBack="true" 
                            oncheckedchanged="cbSelectAll_CheckedChanged" />
                    </td>
                      
                </tr>
                  <tr>
                    <td class="style2">
                       Access Points:
                    </td>
                    <td>
                        <asp:CheckBoxList ID="cclAccessPoints" runat="server"  AutoPostBack="true"
                            onselectedindexchanged="cclAccessPoints_SelectedIndexChanged">
                        </asp:CheckBoxList>
                      
                            </td>
                              <td>
                                  <asp:DropDownList ID="ddlTime" runat="server">
                                  </asp:DropDownList>
                      
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
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnNew" runat="server"
                            onclientclick="(javascript:__doPostBack('','');" Text="Save" 
                            onclick="btnNew_Click" />
                        <asp:Button ID="btnEdit" runat="server"
                            onclientclick="(javascript:__doPostBack('','');" Text="Edit" 
                            Visible="False" onclick="btnEdit_Click" />
                        <asp:Button ID="btnClear" runat="server"
                            Text="Clear" onclick="btnClear_Click" />
                        <asp:Button ID="deleteButton" runat="server" CommandName="Delete" 
                            
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
            </table>
           <%-- <table style="width: 100%;">
                <tr>
                    <td class="style2">
                        Cotag Number:
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
                        Site:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSite" runat="server" AutoPostBack="true"
                            onselectedindexchanged="ddlSite_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:CheckBox ID="cbSelectAll" Text="Select All" runat="server" AutoPostBack="true" 
                            oncheckedchanged="cbSelectAll_CheckedChanged" />
                    </td>
                      
                </tr>
                  <tr>
                    <td class="style2">
                       Access Points:
                    </td>
                    <td>
                        <asp:CheckBoxList ID="cclAccessPoints" runat="server"  AutoPostBack="true"
                            onselectedindexchanged="cclAccessPoints_SelectedIndexChanged" 
                            RepeatColumns="3">
                        </asp:CheckBoxList>
                      
                            </td>
                </tr>
                 <tr>
                    <td class="style2">
                       From:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                        <asp:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="TextBox2"
                                 Mask="99:99"
                                 MaskType="Time"
                                 CultureName="en-us"
                                 MessageValidatorTip="true"
                                 runat="server">
         </asp:MaskedEditExtender>
                            </td>
                </tr>
                 <tr>
                    <td class="style2">
                       To:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                      <asp:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="TextBox1"
                                 Mask="99:99"
                                 MaskType="Time"
                                 CultureName="en-us"
                                 MessageValidatorTip="true"
                                 runat="server">
         </asp:MaskedEditExtender>
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
                        &nbsp;</td>
                    <td>
                        <asp:Button ID="btnNew" runat="server"
                            onclientclick="(javascript:__doPostBack('','');" Text="Save" 
                            onclick="btnNew_Click" />
                        <asp:Button ID="btnEdit" runat="server"
                            onclientclick="(javascript:__doPostBack('','');" Text="Edit" 
                            Visible="False" onclick="btnEdit_Click" />
                        <asp:Button ID="btnClear" runat="server"
                            Text="Clear" onclick="btnClear_Click" />
                        <asp:Button ID="deleteButton" runat="server" CommandName="Delete" 
                            
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
            </table>--%>
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
              <asp:GridView ID="gvCotagDetail" runat="server" AllowPaging="True" 
                  AutoGenerateColumns="False" 
                  onpageindexchanging="gvCotagDetail_PageIndexChanging" 
                  onrowcommand="gvCotagDetail_RowCommand">
                <Columns>
                    <asp:BoundField DataField="CotagNo" HeaderText="CotagNo" ReadOnly="True" SortExpression="CotagNo" />
                    <asp:BoundField DataField="idCard" HeaderText="IDNo" ReadOnly="True" SortExpression="IDNo" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
                    <asp:BoundField DataField="accessFrom" HeaderText="From" ReadOnly="True" SortExpression="Surname" />
                    <asp:BoundField DataField="accessTo"  HeaderText="To" ReadOnly="True" SortExpression="StartDate" />
                 <asp:BoundField DataField="telephone" HeaderText="Telephone" ReadOnly="True" SortExpression="telephone" />
                    <asp:BoundField DataField="mobile" HeaderText="Mobile" ReadOnly="True" SortExpression="mobile" />
                    <asp:CommandField ShowSelectButton="True" />
                 
                    <asp:ButtonField Text="Export" ControlStyle-BorderStyle="NotSet" CommandName="Export" />
                 
                </Columns>
            </asp:GridView>
                     <table>
<tr>
<td>
   <td>
  <asp:Button ID="btnExport" runat="server" Text="Export All" 
        CausesValidation="False" OnClientClick="(javascript:__doPostBack('','');" OnClick="btnExport_Click" 
        /></td>
</td>
</tr>
</table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
