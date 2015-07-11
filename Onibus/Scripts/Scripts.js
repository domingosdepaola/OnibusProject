function SomeMsg() {
    document.getElementById("msgMensagem").style.display = "none";
    document.getElementById("msgRetorno").style.display = "none";
}
function ExibeMsg() {
    document.getElementById("msgMensagem").style.display = "block";
    document.getElementById("msgRetorno").style.display = "block";

}
function Processando() {
    document.getElementById("imgProcessando").style.display = 'block';
}
function SomeBoasVindas() {
    document.getElementById("DvMsgBoasVindas").style.display = 'none';
}
//function httpGet(theUrl) {
//    var xmlHttp = null;

//    xmlHttp = new XMLHttpRequest();
//    xmlHttp.open("GET", theUrl, false);
//    xmlHttp.send(null);
//    return xmlHttp.responseText;
//}
//function AtualizaJSON() {
//    Processando();
//    document.getElementById("hdJSON").value = httpGet("http://dadosabertos.rio.rj.gov.br/apiTransporte/apresentacao/rest/index.cfm/obterTodasPosicoes");
//}
