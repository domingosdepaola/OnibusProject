using Microsoft.AspNet.FriendlyUrls.Resolvers;
using Onibus.BO;
using Onibus.Services;
using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Onibus
{
    public partial class Mapa : System.Web.UI.Page
    {
        private bool Emobile
        {
            get
            {
                if (Request.QueryString["mobile"] != null && Request.QueryString["mobile"].ToString() == "s")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private List<OnibusBO> ListaSessao 
        {
            get 
            {
                if (Session["listaOnibus"] == null)
                {
                    return null;
                }
                else 
                {
                    return (List<OnibusBO>)Session["listaOnibus"];
                }
            }
            set 
            {
                Session["listaOnibus"] = value;
            }
        }
        private DateTime DataUltimaBusca 
        {
            get 
            {
                if (Session["dataBusca"] == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return (DateTime)Session["dataBusca"];
                }
            }
            set 
            {
                Session["dataBusca"] = value;
            }
        }
        private string tipoMapa
        {
            get
            {
                return ViewState["tipoMapa"] == null ? "H" : ViewState["tipoMapa"].ToString();
            }
            set
            {
                ViewState["tipoMapa"] = value;
            }
        }
        protected override void OnPreInit(EventArgs e)
        {
            if (Emobile)
            {
                this.MasterPageFile = "~/Site.Mobile.Master";
                tipoMapa = "N";
            }
            else 
            {
                tipoMapa = "H";
            }
            base.OnPreInit(e);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["NaoBoasVindas"] == null)
                {
                    msgBoasVindas.ShowMessage("Seja bem vindo, digite o numero do ônibus e clique em buscar");
                }
                LimpaMapa();
            }
            SetaTamanhoMapa();
        }

        private void SetaTamanhoMapa()
        {

            if (Emobile)
            {
                dvMapa.Attributes.Add("class", "mapaMobile");
                GMap1.Width = 530;
            }
            else
            {
                dvMapa.Attributes.Add("class", "mapa");
                GMap1.Width = 1024;

            }
            GMap1.Height = 600;
        }
        private void ConfiguraMapa()
        {
            GMap1.GZoom = 12;
            if (tipoMapa != "")
            {
                if (tipoMapa == "N")
                {
                    GMap1.mapType = GMapType.GTypes.Normal;
                }
                else
                {
                    GMap1.mapType = GMapType.GTypes.Hybrid;
                }
            }
            else
            {
                if (Emobile)
                {
                    GMap1.mapType = GMapType.GTypes.Normal;
                }
                else
                {
                    GMap1.mapType = GMapType.GTypes.Hybrid;
                }
            }
            //GMap1.Attributes.Add("OnMarkerClick", "GMap1_MarkerClick");
            // GMap1.enableServerEvents = true;
            //GMap1.EnableViewState = true;
            //GMap1.serverEventsType = GMap.ServerEventsTypeEnum.AspNetPostBack;
            SetaTamanhoMapa();
            GMap1.addControl(new Subgurim.Controles.GControl(GControl.preBuilt.GOverviewMapControl));
            GMap1.addControl(new GControl(GControl.preBuilt.LargeMapControl));
        }
        private void LimpaMapa()
        {
            GMap1.reset();
            GMap1.GZoom = 12;
            GLatLng center = new GLatLng(-22.949944, -43.34832);
            GMap1.setCenter(center);
            if (tipoMapa != "")
            {
                if (tipoMapa == "N")
                {
                    GMap1.mapType = GMapType.GTypes.Normal;
                }
                else
                {
                    GMap1.mapType = GMapType.GTypes.Hybrid;
                }
            }
            else
            {
                if (Emobile)
                {
                    GMap1.mapType = GMapType.GTypes.Normal;
                }
                else
                {
                    GMap1.mapType = GMapType.GTypes.Hybrid;
                }
            }
        }
        private void GeraMapa(List<OnibusBO> lstOnibus)
        {
            //GMap1.reset();

            ConfiguraMapa();

            int contAdicionados = 0;
            int contOnibus = 0;
            foreach (OnibusBO item in lstOnibus)
            {
                contOnibus++;

                if (item.Latitude != 0 && item.Longitude != 0)
                {

                    if (Emobile)
                    {
                        CriaNovoMarcador(item.Latitude, item.Longitude, "", "<h3>" + item.Linha + " - " + item.Ordem + "<br/>" + item.DataHora.ToString("dd/MM/yyyy HH:mm:ss") + "</h3>");
                    }
                    else
                    {
                        CriaNovoMarcador(item.Latitude, item.Longitude, "", "<b>" + item.Linha + " - " + item.Ordem + "<br/>" + item.DataHora.ToString("dd/MM/yyyy HH:mm:ss") + "</b>");
                    }
                    contAdicionados++;
                    //if (posicionado)

                    //    GMap1.setCenter(Centro);
                }

            }
            if (contAdicionados == 0 && contOnibus > 0)
            {
                ExibeMensagem("Nenhuma dado da Linha encontrada");
            }
            else if (contOnibus == 0)
            {
                ExibeMensagem("Nenhuma Linha encontrada");

            }
        }

        private void ExibeMensagem(string mensagem)
        {
            msg.ShowWarnMessage(mensagem);
        }
        private void ExibeMensagemErro(string mensagem)
        {
            msg.ShowErrorMessage(mensagem);
        }
        private void CriaNovoMarcador(double latitude, double longitude, string caminhoImagem, string texto)
        {
            GMap1.addControl(new GControl(GControl.preBuilt.GOverviewMapControl));
            GMap1.addControl(new GControl(GControl.preBuilt.LargeMapControl));
            GMarker marker = new GMarker(new GLatLng(latitude, longitude));
            GMarkerOptions markerOptions = new GMarkerOptions(new GIcon(caminhoImagem));
            marker.options = markerOptions;
            GInfoWindow window;
            window = new GInfoWindow(marker, texto, false);
            GMap1.addInfoWindow(window);

            GMap1.setCenter(marker.point);

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Session["NaoBoasVindas"] = true;
            LimpaMapa();
            OnibusService servico = new OnibusService();
            if (txtBusca.Text != "")
            {
                try
                {
                    int testeLinha = Convert.ToInt32(txtBusca.Text);
                }
                catch
                {
                    ExibeMensagemErro("Linha digitada invalida");
                    return;
                }
            }
            else
            {
                ExibeMensagemErro("Digite o numero do onibus a pesquisar");
                return;
            }
            string linha = txtBusca.Text;
            List<OnibusBO> lstOnibus = new List<OnibusBO>();
            if (ListaSessao == null)
            {

                lstOnibus = servico.GetOnibusAllJSON();
                ListaSessao = lstOnibus;
                DataUltimaBusca = DateTime.Now;
            }
            else 
            {
                DateTime dataTeste = DataUltimaBusca.AddSeconds(40);
                if (dataTeste <= DateTime.Now) 
                {
                    ListaSessao = servico.GetOnibusAllJSON();
                    DataUltimaBusca = DateTime.Now;
                }
            }
            List<OnibusBO> objOnibus = ListaSessao.Where(x => x.Linha == linha.Trim()).ToList();

            GeraMapa(objOnibus);
        }

        protected void lnkTrocar_Click(object sender, EventArgs e)
        {
            if (GMap1.mapType == GMapType.GTypes.Normal)
            {
                GMap1.mapType = GMapType.GTypes.Hybrid;
                tipoMapa = "H";
            }
            else
            {
                GMap1.mapType = GMapType.GTypes.Normal;
                tipoMapa = "N";
            }
        }

    }

}