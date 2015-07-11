<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Onibus.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Contato.</h2>
    <h3>Quer anunciar aqui ? Entre em contato conosco.</h3>
    <address>
         <a target="_blank" href="http://www.domingoscarreiradepaola.com">http://www.domingoscarreiradepaola.com</a>
    </address>

    <address>
        <strong>Contato:</strong>   <a href="mailto:domingosdepaola@gmail.com">domingosdepaola@gmail.com</a><br />
        
    </address>
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
