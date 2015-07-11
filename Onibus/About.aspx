<%@ Page Title="Sobre - Localizador de Onibus RJ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Onibus.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Ache seu Ônibus</h2>
    <h3>Aqui você obtem as informações sobre a localização dos ônibus do Rio de Janeiro.</h3>
    <p>Os dados são disponibilizados pela prefeitura e são extraídos atravez de fonte pública do GPS contido em cada ônibus.</p>
    <script>
        (function (i, s, o, g, r, a, m) {
            i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
                (i[r].q = i[r].q || []).push(arguments)
            }, i[r].l = 1 * new Date(); a = s.createElement(o),
            m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
        })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

        ga('create', 'UA-49704451-1', 'localizadordeonibus.com.br');
        ga('send', 'pageview');

</script>
</asp:Content>
