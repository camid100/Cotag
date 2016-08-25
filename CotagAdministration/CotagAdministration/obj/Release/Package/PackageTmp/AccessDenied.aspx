<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="CotagAdministration.AccessDenied" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="header">
                    <div class="title">
                        <div class="logo" />
                        <div class="titleHeader">
                        <br />
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                        <div class="title2">
                        <br />
                        <br />
                        <br />
                    </div>
                        </div>
                        </div>
                        <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        <div class="body1">
                        
                        <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial Black" 
                                Text="THIS WEBSITE IS RESTRICTED" Font-Size="XX-Large"></asp:Label>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Contact a System Administrator for privileges" 
                                Font-Size="X-Large"></asp:Label>
                        </div>
    </form>
</body>
</html>
