using BO;
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
            return ListaOnibus.Instance.Lista.Where(x => x.Linha == numeroLinha).ToList();

        }
        public string GetOnibusJson(string numeroLinha)
        {
            List<OnibusBO> lstOnibus = GetOnibus(numeroLinha);
            return JsonConvert.SerializeObject(lstOnibus);
        }
    }
}
