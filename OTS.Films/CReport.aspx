<%@ Page Title="Сводный отчет" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CReport.aspx.cs" Inherits="OTS.Films.CReport" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <!-- Сводный отчет -->
      
        <table class="my-table">
            <thead>
                <tr>
                    <th>Название</th>
                    <th>Количество</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="MyRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Eval("Название") %></td>
                            <td><%# Eval("Количество") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

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