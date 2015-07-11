<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Mensagem.ascx.cs" Inherits="Onibus.UserControls.Mensagem" %>
<style>
    /*Mensagens*/
.message-box{
	 /*position:absolute;*/
	z-index:100;
	width:99%;
}

.error-message, .warn-message, .message{
	display:block;
	padding:4px 8px 4px 25px;
	font-size:.9em;
	margin:auto;
	-moz-border-radius:5px; /* Firefox */ 
	-webkit-border-radius:5px; /* chrome & mozila */
}

.error-message{
	text-align:center;
	background:#FFCCCC url(../imagens/atencao.gif) center left no-repeat;
	width:360px;
	border:2px solid #F00; 
	color:#F00;
}

.warn-message {
	background: #B1C8DE url(../imagens/atencao.gif) center left no-repeat;
	border: 2px solid #069 ; 
	color: #333;
	width:360px;
}

.message {
	text-align:center;
	background: #99CC99 url(../imagens/mensagem_ok.gif) center left no-repeat;
	border: 2px solid #090 ; 
	color: #333;
	width:360px;
}
</style>
<div class="message-box" onclick="SomeMsg()" style="display:block" title="Clique para sumir a mensagem" id="msgRetorno">
    <asp:Label ID="lblMensagem" runat="server" CssClass="message"><%= Message %></asp:Label>
</div>
