<%@ Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddDirectorPage.aspx.cs" Inherits="OTS.Films.AddDirectorPage" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <!-- Форма для добавления режиссера -->

        <h3>Добавить нового режиссера</h3>
        <asp:Label ID="lblDirectorName" runat="server" Text="Имя режиссера:" CssClass="form-label" />
        <asp:TextBox ID="txtDirectorName" runat="server" CssClass="form-control" />

        <asp:Button ID="btnSaveDirector" runat="server" Text="Отправить" OnClick="btnSave_Click" />
        <br />
    </div>
</asp:Content>
