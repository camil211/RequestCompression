<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRequestCompression.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>   
    <script src="Scripts/jszip.min.js"></script>    
    <script src="Scripts/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="Scripts/customScripts.js"></script>
</head>
<body>
    <form id="form1" runat="server">        
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:HiddenField ID="HiddenFieldCompressedData" runat="server" />        
        <div>
            <h2>Request Compression</h2>
            <asp:CheckBox ID="CheckBox1" runat="server" Text="Enable compression" />           
        </div>               
        <hr />             
        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" CausesValidation="true" OnClientClick="clientValidate()" onclick="ButtonSubmit_Click"/>
    </form>
</body>
</html>
