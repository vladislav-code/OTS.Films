<%@ Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFilmPage.aspx.cs" Inherits="OTS.Films.AddFilmPage" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <!-- Форма для добавления режиссера -->

        <h3>Добавить нового режиссера</h3>
        <asp:Label ID="lblDirectorTitle" runat="server" Text="Имя режиссера:" CssClass="form-label" />
        <asp:TextBox ID="txtFilmTitle" runat="server" CssClass="form-control" />

        <asp:Label ID="Label1" runat="server" Text="id фильма:" CssClass="form-label" />
        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" />

        <asp:Label ID="Label2" runat="server" Text="" CssClass="form-label" />
        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" />

        <asp:Button ID="btnSaveDirector" runat="server" Text="Отправить" OnClick="btnSave_Click" />
        <br />
    </div>
</asp:Content>
