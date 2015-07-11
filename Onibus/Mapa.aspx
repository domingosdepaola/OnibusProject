<%@ Page Title="Localizador de Ônibus - RJ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mapa.aspx.cs" Inherits="Onibus.Mapa" %>

<%@ Register Assembly="GMaps" Namespace="Subgurim.Controles" TagPrefix="cc1" %>
<%@ Register Src="~/UserControls/Mensagem.ascx" TagName="mensagem" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/Scripts.js" type="text/javascript"></script>
    <asp:HiddenField ID="hdJSON" ClientIDMode="Static" runat="server" />
    <div class="jumbotron">
        <h1>Localizar Ônibus - RJ</h1>
        <p class="lead">Aqui você confere aonde está seu ônibus no Rio de Janeiro</p>
        <div id="floatDesk">
        <p>
            <label class="importante">Linha:</label>
           <%-- <span id="mobileFace" style="display: none;top:70px;padding-left: 90%; position: absolute">
               <span id="anuncieMobile"  style="display: none;"><span onclick="location.href='Contact.aspx'" style="border:1px solid blue;padding-top:5px;padding-left:5px;padding-right:5px;padding-bottom:5px;background-color:white;font-size:16px" class="btn btn-primary btn-large"  onmouseover="javascript:SelecionaAnuncio(this)" onmouseout="DesSelecionaAnuncio(this)"><a href="Contact.aspx" >Anuncie aqui !</a></span></span>
                <br />
                <br />
                <iframe src="http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.facebook.com/localizadordeonibusRj&amp;width=120&amp;height=21&amp;colorscheme=light&amp;layout=button_count&amp;action=like&amp;show_faces=false&amp;send=false&amp;appId=482092695211310" scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 60px; height: 21px;" allowtransparency="true"></iframe>
            </span>--%>

            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtBusca" onfocus="SomeMsg()" />
            <asp:Button runat="server" ID="btnBuscar" ClientIDMode="Static" OnClientClick="Processando()" CssClass="btn btn-primary btn-large" Text="Buscar >>" OnClick="btnBuscar_Click" /></p></div><div id="segundalinhaMobile"><asp:LinkButton runat="server" ID="lnkTrocar" ClientIDMode="Static" OnClick="lnkTrocar_Click" Text="Trocar mapa" /><span id="desktopFace">&nbsp;&nbsp;&nbsp;&nbsp;<iframe src="http://www.facebook.com/plugins/like.php?href=http%3A%2F%2Fwww.facebook.com/localizadordeonibusRj&amp;width=120&amp;height=21&amp;colorscheme=light&amp;layout=button_count&amp;action=like&amp;show_faces=false&amp;send=false&amp;appId=482092695211310" scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 120px; height: 21px;" allowtransparency="true"></iframe>
            </span><span onclick="location.href='Contact.aspx'" id="anuncieDesktop" style="border:1px solid blue;padding-top:5px;padding-left:5px;padding-right:5px;padding-bottom:5px;background-color:white;font-size:16px" class="btn btn-primary btn-large"  onmouseover="javascript:SelecionaAnuncio(this)" onmouseout="DesSelecionaAnuncio(this)"><a href="Contact.aspx" >Anuncie aqui !</a></span></div>
            <img id="imgProcessando" style="display: none" src="Images/processando.gif" />
        
        <div id="DvMsgBoasVindas">
            <uc:mensagem runat="server" ID="msgBoasVindas" />
        </div>
        <div id="msgMensagem">
            <uc:mensagem runat="server" ID="msg" />
        </div>
    </div>
    <div runat="server" id="dvMapa">
        <cc1:GMap ID="GMap1" runat="server" GZoom="12" mapType="Normal" />
    </div>
    <script>
        if (isMobile()) {
            document.getElementById("mobileFace").style.display = 'inline';
            document.getElementById("desktopFace").style.display = 'none';
            document.getElementById("DvMsgBoasVindas").style.fontWeight = 'bold';
            document.getElementById("msgMensagem").style.fontWeight = 'bold';
            document.getElementById("segundalinhaMobile").style.paddingTop = "10px";
            // document.getElementById("anuncieMobile").style.display = 'inline';
           // document.getElementById("anuncieDesktop").style.display = 'none';

        } else {
            document.getElementById("floatDesk").style.cssFloat = "left";
            document.getElementById("floatDesk").style.paddingRight = "25px";
            //document.getElementById("btnBuscar").style.cssFloat = 'left';
            //document.getElementById("btnBuscar").style.paddingRight = "20px";
            
           // document.getElementById("desktopFace").style.display = 'inline';
        }
        setTimeout(function () { SomeBoasVindas() }, 5000);
        setTimeout(function () { SomeMsg() }, 10000);
    </script>
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
