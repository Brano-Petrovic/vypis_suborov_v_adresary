<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WebApplication._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Výpis změn v adresáři</h1>

    <form id="form1" runat="server">
        <div>
            <asp:Label ID="path_label" runat="server" Text="Zadaj cestu k adresáři (Napr.: D:/test/):"></asp:Label>
        &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="input_file_path" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="input_file_path" ErrorMessage="Nezadali ste cestu!"></asp:RequiredFieldValidator>
        </div>
        <div >
            <br />
            <asp:Button ID="Button_zobrazit" runat="server" Text="Zobrazit subory" OnClick="zobrazit" />
        </div>
            <p>
            <asp:Label ID="vypis_aktualny" runat="server" Text=""></asp:Label>
        </p>
    </form>
</body>
</html>
