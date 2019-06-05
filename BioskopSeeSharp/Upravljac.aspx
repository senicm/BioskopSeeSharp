<%@ Page Title="Upravljac" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Upravljac.aspx.cs" Inherits="Upravljac" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="RegularPanel" runat="server" Visible="False" CssClass="upravljacPanel">
        <h3>Upravljanje vlastitim rezervacijama:</h3>
        <asp:ListBox ID="PopisKorisnikovihRezervacija" runat="server" CssClass="listBoxSelect" Rows="4"></asp:ListBox>
        <div class="listBoxButton">
            <asp:Button ID="OtkaziRezervacijuButton" runat="server" Text="Otkaži rezervaciju" OnClick="OtkaziRezervacijuButton_Click" />
        </div>
    </asp:Panel>
    <asp:Panel ID="AdminPanel" runat="server" Visible="False" CssClass="upravljacPanel">
        <h3>Upravljanje svim rezervacijama:</h3>
        <asp:ListBox ID="PopisSvihRezervacija" runat="server" CssClass="listBoxSelect" Rows="4"></asp:ListBox>
        <div class="listBoxButton">
            <asp:Button ID="PotvrdiRezervacijuButton" runat="server" Text="Potvrdi rezervaciju" OnClick="PotvrdiRezervacijuButton_Click" />
            <asp:Button ID="UrediRezervacijuButton" runat="server" Text="Uredi rezervaciju" OnClick="UrediRezervacijuButton_Click" />
        </div>
    </asp:Panel>
</asp:Content>