using BO;
using OnibusAPI.Util;
using RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace OnibusAPI.Controllers
{
    public class OnibusController : ApiController
    {

        // GET: /Onibus/
        List<OnibusBO> lstOnibus = new List<OnibusBO>();
        OnibusRN onibusRN = new OnibusRN();
        public string GetOnibus(string x, string y, string z)
        {
            string numeroLinha = CriptUtil.Cript.Decript(x, "NestorDePaola");
            string latitudeString = CriptUtil.Cript.Decript(y, "NestorDePaola");
            string longitudeString = CriptUtil.Cript.Decript(z, "NestorDePaola");
            if (numeroLinha != null && numeroLinha != "")
            {
                ProcessoUtil.Instance.ProcessamentoParalelo();
                return onibusRN.GetOnibusJson(numeroLinha);
            }
            else 
            {
                return null;
            }
        }
        public List<OnibusBO> GetOnibusWP(string x,string y,string z) 
        {
            string numeroLinha = x;
            string latitudeString = y;
            string longitudeString = z;
            if (numeroLinha != null && numeroLinha != "")
            {
                ProcessoUtil.Instance.ProcessamentoParalelo();
                return onibusRN.GetBus(numeroLinha);
            }
            else
            {
                return null;
            }
        }


    }
}
