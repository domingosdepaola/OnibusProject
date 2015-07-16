using OnibusAPI.Util;
using RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnibusGet
{
    public partial class GetOnibus : System.Web.UI.Page
    {
        OnibusRN onibusRN = new OnibusRN();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            if (Request.QueryString["x"] != null && Request.QueryString["y"] != null && Request.QueryString["z"] != null)
            {
                try
                {
                    String x = Request.QueryString["x"].ToString();
                    String y = Request.QueryString["y"].ToString();
                    String z = Request.QueryString["z"].ToString();
                    string numeroLinha = CriptUtil.Cript.Decript(x, "NestorDePaola");
                    string latitudeString = CriptUtil.Cript.Decript(y, "NestorDePaola");
                    string longitudeString = CriptUtil.Cript.Decript(z, "NestorDePaola");
                    if (numeroLinha != null)
                    {

                        if (numeroLinha != null && numeroLinha != "")
                        {
                            ProcessoUtil.Instance.ProcessamentoParalelo();
                            string resultado = onibusRN.GetOnibusJson(numeroLinha);
                            Response.Write(resultado);
                        }
                        else
                        {
                            Response.Write(onibusRN.GetOnibusJson(numeroLinha));
                        }
                    }
                    else
                    {
                        Response.Write("");
                    }
                }
                catch
                {
                    Response.Write("");
                }
                finally 
                {
                    Response.End();
                }
            }
        }
    }
}