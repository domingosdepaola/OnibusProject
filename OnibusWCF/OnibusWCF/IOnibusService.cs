using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OnibusWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IOnibusService" in both code and config file together.
    [ServiceContract]
    public interface IOnibusService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        [WebGet(UriTemplate = "Onibus/{x}")] 
        String GetOnibus(string x);
    }
}
