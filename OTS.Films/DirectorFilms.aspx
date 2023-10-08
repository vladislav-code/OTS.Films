<%@ Page Title="Фильмы режиссеров" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DirectorFilms.aspx.cs" Inherits="OTS.Films.DirectorFilms" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <!-- Фильмы режиссера -->
        <table class="my-table">
            <thead>
                <tr>
                    <th>Имя</th>
                    <th>Жанр</th>
                    <th>Количество</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="MyRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <%--Выводим имя только в тех ячейках, где rowspan != 0 - признак того, что данная ячейка была объединена--%>
                            <%# Convert.ToInt32(Eval("rowspan")) > 0 ? "<td rowspan='" + Eval("rowspan") + "'>" + Eval("Имя") + "</td>" : "" %> 
                            <td><%# Eval("Жанр") %></td>
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
