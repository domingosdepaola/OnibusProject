using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    public class OnibusBO
    {
        public DateTime DataHora { get; set; }
        public string Ordem { get; set; }
        public string Linha { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int Velocidade { get; set; }

        public String Endereco { get; set; }
    }
    public class DATA
    {
        [JsonProperty("DataHora")]
        public DateTime DataHora { get; set; }

        [JsonProperty("Ordem")]
        public string Ordem { get; set; }

        [JsonProperty("Linha")]
        public string Linha { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("Velocidade")]
        public int Velocidade { get; set; }
    }
    public class DATAS
    {
        [JsonProperty("data")]
        List<DATA> data { get; set; }
    }
}
