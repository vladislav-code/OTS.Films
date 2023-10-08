<%@ Title="Отчет по жанрам" Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenreReport.aspx.cs" Inherits="OTS.Films.GenreReport" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <!-- Отчет по жанру -->

        <h3>Выберите жанр</h3>
        <asp:Label ID="lblDirectorName" runat="server" Text="Имя режиссера:" CssClass="form-label" />
        <asp:TextBox ID="txtDirectorName" runat="server" CssClass="form-control" />
        <asp:DropDownList ID="ddlGenre" runat="server" DataTextField="name" DataValueField="id"></asp:DropDownList>

        <table class="my-table">
            <thead>
                <tr>
                    <th>Название фильма</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="MyRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Title") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

        <asp:Button ID="btnSaveChoice" runat="server" Text="Отправить" OnClick="btnSave_Click" />
        <br />
    </div>

    <style type="text/css">
    .my-table {
        border-collapse: collapse;
        width: 100%;
    }

    .my-table th, .my-table td {
        border: 1px solid #ddd; /* Границы ячеек */
        padding: 8px; /* Отступ внутри ячеек */
        text-align: left; /* Выравнивание текста */
    }

    .my-table th {
        background-color: #f2f2f2; /* Цвет фона заголовков */
    }
    </style>
</asp:Content>
