using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Onibus.BO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace Onibus.Data
{
    public class OnibusDAO
    {
        public List<OnibusBO> getOnibus()
        {
            List<OnibusBO> lstOnibus = new List<OnibusBO>();
            string path = AppDomain.CurrentDomain.BaseDirectory.ToString() + @"csv";
            string nomeArquivo = "arquivo.csv";
            string pathCompleto = path + "\\" + nomeArquivo;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            DataSet ds = getDadosFromCSV(pathCompleto);

            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    NumberFormatInfo provider = new NumberFormatInfo();
                    provider.NumberDecimalSeparator = ",";
                    provider.NumberGroupSeparator = ".";
                    OnibusBO onibus = new OnibusBO();
                    onibus.DataHora = Convert.ToDateTime(dr["dataHora"]);
                    onibus.Latitude = Convert.ToDouble(dr["Latitude"].ToString().Replace(".", ","), provider);
                    onibus.Longitude = Convert.ToDouble(dr["Longitude"].ToString().Replace(".", ","), provider);
                    onibus.Ordem = dr["ordem"].ToString();
                    onibus.Velocidade = Convert.ToInt32(dr["Velocidade"].ToString() == "" ? 0 : dr["Velocidade"]);
                    onibus.Linha = dr["linha"].ToString();
                    lstOnibus.Add(onibus);
                }
            }

            return lstOnibus;

        }
        public List<OnibusBO> getOnibusJSON()
        {
            string jsonURL = "http://dadosabertos.rio.rj.gov.br/apiTransporte/apresentacao/rest/index.cfm/obterTodasPosicoes";
            List<OnibusBO> lstOnibus = new List<OnibusBO>();
            

            //DataSet ds = getDadosFromCSV(pathCompleto);
            lstOnibus = getDadosFromJSON(jsonURL);

            return lstOnibus;

        }
        private string getStringFromURL(string url) 
        {
            return GetStringJSON(url);
        }
        private List<OnibusBO> getDadosFromJSON(string jsonURL)
        {
            string json = GetStringJSON(jsonURL);
            JObject list = JsonConvert.DeserializeObject<JObject>(json);
            List<OnibusBO> lst = preencheObjetoJSON(list);
            return lst;
        }
        private List<OnibusBO> preencheObjetoJSON(JObject jObject)
        {
            List<OnibusBO> lstOnibus = new List<OnibusBO>();
            if (jObject != null)
            {

                List<ColunaIndice> lstColunaIndice = new List<ColunaIndice>();
                if (jObject["COLUMNS"] != null)
                {
                    for (int i = 0; i < jObject["COLUMNS"].Count(); i++)
                    {
                        ColunaIndice colunaIndice = new ColunaIndice();
                        colunaIndice.coluna = jObject["COLUMNS"][i].ToString();
                        colunaIndice.indice = i;
                        lstColunaIndice.Add(colunaIndice);
                    }
                }
                if (jObject["DATA"] != null)
                {
                    for (int i = 0; i < jObject["DATA"].Count(); i++)
                    {
                        NumberFormatInfo provider = new NumberFormatInfo();
                        provider.NumberDecimalSeparator = ",";
                        provider.NumberGroupSeparator = ".";
                        OnibusBO onibus = new OnibusBO();
                        onibus.DataHora = Convert.ToDateTime(jObject["DATA"][i][lstColunaIndice.Find(x => x.coluna == "DATAHORA").indice]);
                        onibus.Latitude = Convert.ToDouble(jObject["DATA"][i][lstColunaIndice.Find(x => x.coluna == "LATITUDE").indice].ToString().Replace(".", ","), provider);
                        onibus.Longitude = Convert.ToDouble(jObject["DATA"][i][lstColunaIndice.Find(x => x.coluna == "LONGITUDE").indice].ToString().Replace(".", ","), provider);
                        onibus.Ordem = jObject["DATA"][i][lstColunaIndice.Find(x => x.coluna == "ORDEM").indice].ToString();
                        // onibus.Velocidade = Convert.ToInt32(jObject["DATA"][i]["Velocidade"].ToString() == "" ? 0 : jObject["DATA"][i]["Velocidade"]);
                        onibus.Linha = jObject["DATA"][i][lstColunaIndice.Find(x => x.coluna == "LINHA").indice].ToString();
                        lstOnibus.Add(onibus);
                    }
                }
            }
            return lstOnibus;
        }
        private struct ColunaIndice
        {
            public string coluna;
            public int indice;
        }
        private DataSet getDadosFromCSV(string pathCompleto)
        {
            using (WebClient Client = new WebClient())
            {
                //Client.OpenWrite("http://data.rio.rj.gov.br/dataset/gps-de-onibus");
                Client.DownloadFile("http://dadosabertos.rio.rj.gov.br/apiTransporte/apresentacao/csv/onibus.cfm", pathCompleto);
            }
            DataSet ds = GetDataTabletFromCSVFile(pathCompleto);
            return ds;
        }
        private string GetStringJSON(string url)
        {
            try
            {
                string retorno = "";
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);

                // Get the associated response for the above request.
                myHttpWebRequest.Method = "GET";
                myHttpWebRequest.Accept = "application/x-ms-application, image/jpeg, application/xaml+xml,         image/gif, image/pjpeg, application/x-ms-xbap, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                myHttpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                using (var sr = new StreamReader(myHttpWebResponse.GetResponseStream()))
                {
                    retorno = sr.ReadToEnd();
                }

                return retorno; 
            }
            catch (WebException ex)
            {
                System.Console.WriteLine(ex.Message);
                return "";
            }
        }
        private DataSet GetDataTabletFromCSVFile(string csv_file_path)
        {
            string FileName = csv_file_path;
            OleDbConnection conn = new OleDbConnection
                   ("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " +
                     Path.GetDirectoryName(FileName) +
                     "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");

            conn.Open();

            OleDbDataAdapter adapter = new OleDbDataAdapter
                   ("SELECT * FROM " + Path.GetFileName(FileName), conn);

            DataSet ds = new DataSet("Temp");
            adapter.Fill(ds);

            conn.Close();

            return ds;
        }
    }
}