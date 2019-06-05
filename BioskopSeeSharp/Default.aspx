<%@ Page Title="Naslovna" Language="C#" MasterPageFile="~/Template.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <h2>Dobro došli na stranicu SEE SHARP bioskopa.</h2>

    <h3>Na ovoj stranici možete videti kratki prikaz filmova koje trenutno nudimo. Kako biste otvorili puni prikaz filma, kliknite na sliku ili naslov filma.</h3>

    <asp:ScriptManager ID="ScriptManager1" runat="server">

    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <section class="BioskopBlock">
                <div class="BioskopSlike">
            <asp:Image ID="ImageCurrent" runat="server" />
                 </div>
            </section>
            <asp:Timer ID="TimerUvecaj" runat="server" Interval="2500" OnTick="TimerUvecaj_Tick"></asp:Timer>
        </ContentTemplate>    
    </asp:UpdatePanel>
    
</asp:Content>