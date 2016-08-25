<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="CotagZones.aspx.cs" Inherits="CotagAdministration.CotagZones" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <style type="text/css">
        .style2
        {
            height: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<table>
<tr>
<td>
 CotagNo: 
</td>
<td>
    <asp:DropDownList ID="ddlCotagNos" AutoPostBack="true" EnableViewState="true" runat="server" 
        onselectedindexchanged="ddlCotagNos_SelectedIndexChanged">
    </asp:DropDownList>
</td>
</tr>
</table>
<table>
<tr>
<td>
    <asp:ListBox ID="lbZone" runat="server" AutoPostBack="true" EnableViewState="true"
 Height="358px" Width="284px" onselectedindexchanged="lbZone_SelectedIndexChanged"></asp:ListBox>
</td>
<td>

</td>
<td>
<img alt="swap" src="Assets/swap.png" height="40px" width="40px" />
</td>
<td>
<asp:ListBox ID="lbCurrentZone" runat="server" AutoPostBack="true" 
        EnableViewState="true" Height="358px" Width="284px" 
        onselectedindexchanged="lbCurrentZone_SelectedIndexChanged"></asp:ListBox>
</td>
</tr>
</table>
            <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
 </ContentTemplate>
  <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lbZone" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lbCurrentZone" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCotagNos" EventName="SelectedIndexChanged" />
        </Triggers>
     </asp:UpdatePanel>
<br />
   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
<table>
<tr>
<td>
  <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" AllowSorting="True" 
        onpageindexchanged="GridView1_PageIndexChanged" 
        onpageindexchanging="GridView1_PageIndexChanging" >
               <%-- OnPageIndexChanging="gvCotagDetail_PageIndexChanging" 
                OnRowCommand="gvCotagDetail_RowCommand" 
                onselectedindexchanged="gvCotagDetail_SelectedIndexChanged"--%> 
               
               <%-- onsorting="gvCotagDetail_Sorting"--%>
                
                <Columns>
                    <asp:BoundField DataField="CotagNo" HeaderText="CotagNo" ReadOnly="True" SortExpression="CotagNo" />
                    <asp:BoundField DataField="IDNo" HeaderText="IDNo" ReadOnly="True" SortExpression="IDNo" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" SortExpression="Name" />
                    <asp:BoundField DataField="Surname" HeaderText="Surname" ReadOnly="True" SortExpression="Surname" />
                     <asp:BoundField DataField="cotagDesc_Name" HeaderText="Cotag Description" ReadOnly="True" SortExpression="cotagDesc_Name" />
                     <asp:BoundField DataField="Zone" HeaderText="Zone" ReadOnly="True" SortExpression="Zone" />

                          </Columns>
            </asp:GridView>
</td>
</tr>
</table>
<table>
<tr>
<td>Search:</td>
<td></td>
<td></td>
</tr>
<tr>
<td>ZoneID:</td>
<td><asp:DropDownList ID="ddSearchZoneID" runat="server">
    </asp:DropDownList></td>
<td></td>
</tr>
<tr>
<td>CotagNo</td>
<td>   <asp:DropDownList ID="ddSearchCotagNo" runat="server">
    </asp:DropDownList></td>
<td></td>
</tr>
<tr>
<td><asp:Button ID="btnSearch" runat="server" Text="Search" 
        onclick="btnSearch_Click" /></td>
<td><asp:Button ID="btnExport" runat="server" Text="Export" 
        onclick="btnExport_Click" /></td>
<td></td>
</tr>
</table>
        </ContentTemplate>
    </asp:UpdatePanel>


 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="GridViewContent" runat="server">
</asp:Content>
