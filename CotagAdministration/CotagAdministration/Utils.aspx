<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeBehind="Utils.aspx.cs" Inherits="CotagAdministration.Utils" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <table style="width: 100%;">
 <tr><td><asp:Button ID="btnGenerate" runat="server" Text="Generate" Height="63px" 
        onclick="btnGenerate_Click" Width="159px" />
    <asp:Label ID="lblMsg" runat="server" Text="" Visible="true" ForeColor="Red"></asp:Label></td></tr>
 </table>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="GridViewContent" runat="server">
 <asp:GridView ID="gvContacts" runat="server" AllowPaging="True" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="request_date" HeaderText="Requested On" ReadOnly="True" SortExpression="request_date" />
                    <asp:BoundField DataField="status" HeaderText="Status" ReadOnly="True"
                        SortExpression="Status" />
                            <asp:BoundField DataField="status_detail" HeaderText="Detail" ReadOnly="True"
                        SortExpression="Detail" />
                            <asp:BoundField DataField="processed_date" HeaderText="Processed On" ReadOnly="True"
                        SortExpression="Processed On" />
                </Columns>
            </asp:GridView>
</asp:Content>
