using BO;
using Common;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RN
{
    public class OnibusRN
    {
        private List<OnibusBO> GetOnibusAllJSON()
        {
            OnibusDAO dao = new OnibusDAO();
            return dao.getOnibusJSON();
        }
        public void AtualizaListaTotalOnibus()
        {
            ListaOnibus.Instance.Lista = GetOnibusAllJSON();
        }
        public List<OnibusBO> GetOnibus(string numeroLinha)
        {
            if (ListaOnibus.Instance.Lista.Count == 0)
            {
                AtualizaListaTotalOnibus();
            }
            List<OnibusBO> lstRetorno = ListaOnibus.Instance.Lista.Where(x => x.Linha == numeroLinha).ToList();
            return lstRetorno;
        }
        public string GetOnibusJson(string x)
        {
            List<OnibusBO> lstOnibus = GetOnibus(x);
            return JsonConvert.SerializeObject(lstOnibus);
        }
        public List<OnibusBO> GetBus(string x) 
        {
            List<OnibusBO> lstOnibus = GetOnibus(x);
            return lstOnibus;
        }
    }
}
