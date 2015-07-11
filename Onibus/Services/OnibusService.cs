using Onibus.BO;
using Onibus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Onibus.Services
{
    public class OnibusService
    {
        public List<OnibusBO> GetOnibusAll() 
        {
            OnibusDAO dao = new OnibusDAO();
            return dao.getOnibus();
        }
        public List<OnibusBO> GetOnibusAllJSON() 
        {
            OnibusDAO dao = new OnibusDAO();
            return dao.getOnibusJSON();
        }
    }
}