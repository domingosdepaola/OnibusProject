using OnibusAPI.Util;
using RN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace OnibusWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "OnibusService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select OnibusService.svc or OnibusService.svc.cs at the Solution Explorer and start debugging.
    public class OnibusService : IOnibusService
    {
        public void DoWork()
        {
        }

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


        public string GetOnibus(string x)
        {
            return "OK";
        }
    }
}
