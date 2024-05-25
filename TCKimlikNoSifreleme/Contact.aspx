<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="TCKimlikNoSifreleme.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>İletişim:</h3>
        <address>
            Ankara<br />
            Türkiye<br />
            <abbr title="Phone">TEL:</abbr>
            0111 111 1111
        </address>

        <address>
            <strong>Projeler:</strong>   <a href="https://github.com/MusaSEKEROGLU">Github</a><br />
            <strong>Herşey:</strong> <a href="https://www.linkedin.com/in/musa-sekeroglu/">Linkedin</a>
        </address>
    </main>
</asp:Content>
