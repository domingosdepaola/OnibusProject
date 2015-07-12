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
        public string GetOnibus(string numeroLinha)
        {
            ProcessoUtil.Instance.ProcessamentoParalelo();
            return onibusRN.GetOnibusJson(numeroLinha);
        }
        
        
        
    }
}
