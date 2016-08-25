<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="CotagAdministration.Contacts.Contacts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contacts</title>
    <script src="../Scripts/funcs.js" type="text/javascript"></script>
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:LinkButton ID="btnA" runat="server" CommandArgument="A" CommandName="Search"
            OnCommand="btn_Command">A</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnB" runat="server" CommandArgument="B" CommandName="Search"
            OnCommand="btn_Command">B</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnC" runat="server" CommandArgument="C" CommandName="Search"
            OnCommand="btn_Command">C</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnD" runat="server" CommandArgument="D" CommandName="Search"
            OnCommand="btn_Command">D</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnE" runat="server" CommandArgument="E" CommandName="Search"
            OnCommand="btn_Command">E</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnF" runat="server" CommandArgument="F" CommandName="Search"
            OnCommand="btn_Command">F</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnG" runat="server" CommandArgument="G" CommandName="Search"
            OnCommand="btn_Command">G</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnH" runat="server" CommandArgument="H" CommandName="Search"
            OnCommand="btn_Command">H</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnI" runat="server" CommandArgument="I" CommandName="Search"
            OnCommand="btn_Command">I</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnJ" runat="server" CommandArgument="J" CommandName="Search"
            OnCommand="btn_Command">J</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnK" runat="server" CommandArgument="K" CommandName="Search"
            OnCommand="btn_Command">K</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnL" runat="server" CommandArgument="L" CommandName="Search"
            OnCommand="btn_Command">L</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnM" runat="server" CommandArgument="M" CommandName="Search"
            OnCommand="btn_Command">M</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnN" runat="server" CommandArgument="N" CommandName="Search"
            OnCommand="btn_Command">N</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnO" runat="server" CommandArgument="O" CommandName="Search"
            OnCommand="btn_Command">O</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnP" runat="server" CommandArgument="P" CommandName="Search"
            OnCommand="btn_Command">P</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnQ" runat="server" CommandArgument="Q" CommandName="Search"
            OnCommand="btn_Command">Q</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnR" runat="server" CommandArgument="R" CommandName="Search"
            OnCommand="btn_Command">R</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnS" runat="server" CommandArgument="S" CommandName="Search"
            OnCommand="btn_Command">S</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnT" runat="server" CommandArgument="T" CommandName="Search"
            OnCommand="btn_Command">T</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnU" runat="server" CommandArgument="U" CommandName="Search"
            OnCommand="btn_Command">U</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnV" runat="server" CommandArgument="V" CommandName="Search"
            OnCommand="btn_Command">V</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnW" runat="server" CommandArgument="W" CommandName="Search"
            OnCommand="btn_Command">W</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnX" runat="server" CommandArgument="X" CommandName="Search"
            OnCommand="btn_Command">X</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnY" runat="server" CommandArgument="Y" CommandName="Search"
            OnCommand="btn_Command">Y</asp:LinkButton>
        &nbsp;<asp:LinkButton ID="btnZ" runat="server" CommandArgument="Z" CommandName="Search"
            OnCommand="btn_Command">Z</asp:LinkButton>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
        <br />
        <br />
    </div>
    <div>
        <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" />
                <asp:BoundField DataField="Surname" HeaderText="Surname" ReadOnly="True" />
                <asp:BoundField DataField="Department" HeaderText="Department" ReadOnly="True" />
                <asp:BoundField DataField="Telephone" HeaderText="Telephone" ReadOnly="True" />
                <asp:BoundField DataField="Mobile" HeaderText="Mobile" ReadOnly="True" />
                <asp:CommandField ButtonType="Button" ShowCancelButton="False" ShowSelectButton="True"
                    CausesValidation="False" SelectText="View Detail" />
            </Columns>
        </asp:GridView>
    </div>
    <div id="waitingDiv" class="overlay2" onclick="stopWait('waitingDiv');stopWait('waiting');">
    </div>
    <div id="waiting">
        <img align="right" src="../Assets/close-icon.png" onclick="stopWait('waitingDiv');stopWait('waiting');"
            width="15px" />
        <asp:Table ID="Table1" runat="server">
            <asp:TableRow>
                <asp:TableCell> Name: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblName" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Surname: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblSurname" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Email: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblEmail" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Department: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblDepartment" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Section: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblSection" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Site: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblSite" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Location: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblLocation" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Telephone: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Mobile: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblMobile" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell> Assembly Point: </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="lblAssemblyPoint" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
    </form>
</body>
</html>
