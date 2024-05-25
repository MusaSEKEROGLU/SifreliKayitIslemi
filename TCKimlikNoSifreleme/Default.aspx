<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TCKimlikNoSifreleme._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <h1 id="aspnetTitle">TC Kimlik Numarası Şifreleme ve Giriş</h1>
            <p class="lead">TC Kimlik numarasını ve şifrenizi şifreleyip veritabanına kaydetmek veya giriş yapmak için aşağıdaki formları kullanabilirsiniz.</p>
        </section>

        <section class="row">
            <div class="col-md-6">
                <h2>Kayıt Formu</h2>
                <div class="form-group">
                    <label for="txtTCNo">TC Kimlik Numarası:</label>
                    <asp:TextBox ID="txtTCNo" runat="server" CssClass="form-control" placeholder="TC Kimlik Numarası giriniz"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtSifre">Şifre:</label>
                    <asp:TextBox ID="txtSifre" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifre giriniz"></asp:TextBox>
                </div>
                <br />
                <div class="form-group">
                    <asp:Button ID="btnKaydet" runat="server" Text="Şifrele ve Kaydet" OnClick="btnKaydet_Click" CssClass="btn btn-primary" />
                </div>
            </div>

            <div class="col-md-6">
                <h2>Giriş Formu</h2>
                <div class="form-group">
                    <label for="txtLoginTCNo">TC Kimlik Numarası:</label>
                    <asp:TextBox ID="txtLoginTCNo" runat="server" CssClass="form-control" placeholder="TC Kimlik Numarası giriniz"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label for="txtLoginSifre">Şifre:</label>
                    <asp:TextBox ID="txtLoginSifre" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifre giriniz"></asp:TextBox>
                </div>
                <br />
                <div class="form-group">
                    <asp:Button ID="btnGiris" runat="server" Text="Giriş Yap" OnClick="btnGiris_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </section>
    </main>


</asp:Content>
