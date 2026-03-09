<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmEstudiante.aspx.cs" Inherits="webSIA.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 90%;
            border-style: solid;
            border-width: 2px;
        }
        .auto-style2 {
            height: 23px;
        }
        .auto-style3 {
        }
        .auto-style4 {
            height: 23px;
        }
        .auto-style5 {
            width: 245px;
        }
        .auto-style6 {
            height: 23px;
            width: 245px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>
        <table class="auto-style1">
            <tr>
                <td colspan="2" style="text-align: center; background-color: #FF0000; font-size: 16px; font-weight: bold;">Maestro de Estudiante</td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style6" style="text-align: right">Carné: </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtCodi" runat="server" ReadOnly="True" Width="212px"></asp:TextBox>
                    <asp:ImageButton ID="ibtnBusc" runat="server" Height="21px" ImageUrl="~/Imagenes/lupa.png" Width="23px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style6" style="text-align: right">Programa: </td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlProg" runat="server" Width="181px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">Nro Doc: </td>
                <td>
                    <asp:TextBox ID="txtNDoc" runat="server" Width="212px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">Tipo Doc: </td>
                <td>
                    <asp:DropDownList ID="ddITDoc" runat="server" Height="16px" Width="182px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">Nombre: </td>
                <td>
                    <asp:TextBox ID="txtNomb" runat="server" Width="209px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">Apellido: </td>
                <td>
                    <asp:TextBox ID="txtApel" runat="server" Width="205px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">&nbsp;</td>
                <td>
                    <asp:CheckBox ID="chkActi" runat="server" Text="Activo" TextAlign="Left" />
                </td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style5" style="text-align: right">Observaciones: </td>
                <td>
                    <asp:TextBox ID="txtObse" runat="server" Rows="3" TextMode="MultiLine" Width="486px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td aria-orientation="horizontal" class="auto-style4" colspan="2" rowspan="1" style="text-align: center">
                    <asp:Menu ID="Menu1" runat="server" BackColor="#CCCCCC" BorderWidth="2px" CssClass="horizontal-separator" DynamicHorizontalOffset="2" Font-Bold="True" Font-Size="Small" ForeColor="Red" OnMenuItemClick="Menu1_MenuItemClick" Orientation="Horizontal" RenderingMode="Table" StaticSubMenuIndent="16px" Width="100%">
                        <Items>
                            <asp:MenuItem Text="Buscar" Value="opcBusc"></asp:MenuItem>
                            <asp:MenuItem Text="Agregar" Value="opcAgre"></asp:MenuItem>
                            <asp:MenuItem Text="Modificar" Value="opcModi"></asp:MenuItem>
                            <asp:MenuItem Text="Grabar" Value="opcGrab"></asp:MenuItem>
                            <asp:MenuItem Text="Cancelar" Value="opcCanc"></asp:MenuItem>
                        </Items>
                        <StaticMenuItemStyle HorizontalPadding="20px" />
                    </asp:Menu>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3" colspan="2" style="text-align: center">
                    <asp:Label ID="IBIMsj" runat="server" Font-Bold="True" ForeColor="Red" Text="IbIMsj"></asp:Label>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
