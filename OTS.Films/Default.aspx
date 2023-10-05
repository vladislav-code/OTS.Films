
<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OTS.Films._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Фильмы</h1>

    <table class="my-table">
            <thead>
                <tr>
                    <th>Название</th>
                    <th>Режиссер</th>
                    <th>Жанр</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="MyRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <%--Выводим имя только в тех ячейках, где rowspan != 0 - признак того, что данная ячейка была объединена--%>
                            <%# Convert.ToInt32(Eval("rowspan")) > 0 ? "<td rowspan='" + Eval("rowspan") + "'>" + Eval("Название") + "</td>" : "" %> 
                            <td><%# Eval("Режиссер") %></td>
                            <td><%# Eval("Жанр") %></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>


    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="https://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
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
