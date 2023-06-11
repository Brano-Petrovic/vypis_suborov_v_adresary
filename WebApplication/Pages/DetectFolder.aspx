<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetectFolder.aspx.cs" Inherits="WebApplication.Pages.DetectFolder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
<h1>Výpis změn v adresáři</h1>

<form id="form1" runat="server">
    <div>
        <asp:Label ID="pathLabel" runat="server" Text="Zadajte relativní cestu k adresáři (Napr.: ~/testing_data):"></asp:Label>
        <asp:TextBox ID="inputFilePath" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="inputFilePath" ErrorMessage="Nezadali ste cestu!"></asp:RequiredFieldValidator>
    </div>
    <div >
        <asp:Button ID="ViewButton" runat="server" Text="Skontrolovat adresář" OnClick="DetectChanges" />
    </div>
    <p>
        <asp:Label ID="ResultsLabel" runat="server" Text=""></asp:Label>
    </p>
</form>
</body>
</html>
