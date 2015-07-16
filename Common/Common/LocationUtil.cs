using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json;

namespace Common
{
    public class LocationUtil
    {
        public class Location
        {
            public double latitude;
            public double longitude;
        }
        public static Location GetLatLong(string enderecoTratado)
        {
            return GetLocation(enderecoTratado);
        }
        public static Location GetLatLong(string rua, string numero, string bairro, string cidade, string estado)
        {
            string adress = "";
            adress = RetornaEnderecoTratado(rua, numero, bairro, cidade, estado);

            return GetLocation(adress);
        }
        private static Location GetLocation(string adress)
        {
            Location location = new Location();
            try
            {
                //adress = adress.Replace(" ", "+");
                XmlDocument doc = new XmlDocument();
                WebClient client = new WebClient();

                string key = "";
                try
                {
                    key = ConfigurationManager.AppSettings["GoogleKey"].ToString();
                }
                catch { }

                string adressFormated = HttpUtility.UrlEncode(adress);
                string url = "https://maps.googleapis.com/maps/api/geocode/json?address=" + adressFormated + "&key=" + key;
                string retorno = client.DownloadString(url);
                GoogleGeoCodeResponse googleGeoCodeResponse = JsonConvert.DeserializeObject<GoogleGeoCodeResponse>(retorno);

                if (googleGeoCodeResponse.results.Count() > 0)
                {
                    if (googleGeoCodeResponse.results[0].geometry.location != null && googleGeoCodeResponse.results[0].geometry.location != null)
                    {
                        if (googleGeoCodeResponse.results[0].geometry.location.lat != "" && googleGeoCodeResponse.results[0].geometry.location.lat != null && googleGeoCodeResponse.results[0].geometry.location.lat != "0" && googleGeoCodeResponse.results[0].geometry.location.lng != null && googleGeoCodeResponse.results[0].geometry.location.lng != "" && googleGeoCodeResponse.results[0].geometry.location.lng != "0")
                        {
                            location.latitude = Convert.ToDouble(googleGeoCodeResponse.results[0].geometry.location.lat.Replace(".", ","), new CultureInfo("pt-BR"));
                            location.longitude = Convert.ToDouble(googleGeoCodeResponse.results[0].geometry.location.lng.Replace(".", ","), new CultureInfo("pt-BR"));
                        }
                    }
                }
            }
            catch
            {
                return null;
            }
            return location;
        }

        public static string RetornaEnderecoTratado(string rua, string numero, string bairro, string cidade, string estado)
        {
            string adress;
            adress = TrataNomeRua(rua);
            adress += (numero != "" ? (", " + numero) : "") + " " + bairro + ", " + cidade + " - " + estado;
            return adress;
        }

        private static string TrataNomeRua(string rua)
        {
            string adress = rua.Replace("- lado impar", "").Replace("- lado par", "").Replace("- lado ímpar", "");
            string[] ateVet = adress.Split('-');
            if (ateVet.Length > 1)
            {
                string ate = ateVet[1];
                adress = adress.Replace(ate, "");
                adress = adress.Replace("-", "");
            }
            return adress;
        }
        public static string ReverseGeoLoc(string latitude, string longitude)
        {

            string Address_ShortName = "";
            string Address_country = "";
            string Address_administrative_area_level_1 = "";
            string Address_administrative_area_level_2 = "";
            string Address_administrative_area_level_3 = "";
            string Address_colloquial_area = "";
            string Address_locality = "";
            string Address_sublocality = "";
            string Address_neighborhood = "";
            latitude = latitude.Replace(",", ".");
            longitude = longitude.Replace(",", ".");
            XmlDocument doc = new XmlDocument();

            try
            {
                string key = "";
                try
                {
                    key = ConfigurationManager.AppSettings["GoogleKey"].ToString();
                }
                catch { }

                WebClient client = new WebClient();

                string retorno = client.DownloadString("https://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false&key=" + key);
                //doc.Load("http://maps.googleapis.com/maps/api/geocode/xml?latlng=" + latitude + "," + longitude + "&sensor=false");
                doc.LoadXml(retorno);
                XmlNode element = doc.SelectSingleNode("//GeocodeResponse/status");
                if (element.InnerText == "ZERO_RESULTS")
                {
                    return "";
                }
                else
                {

                    element = doc.SelectSingleNode("//GeocodeResponse/result/formatted_address");

                    string longname = "";
                    string shortname = "";
                    string typename = "";
                    bool fHit = false;


                    XmlNodeList xnList = doc.SelectNodes("//GeocodeResponse/result/address_component");
                    foreach (XmlNode xn in xnList)
                    {
                        try
                        {
                            longname = xn["long_name"].InnerText;
                            shortname = xn["short_name"].InnerText;
                            typename = xn["type"].InnerText;


                            fHit = true;
                            switch (typename)
                            {
                                //Add whatever you are looking for below
                                case "country":
                                    {
                                        Address_country = longname;
                                        Address_ShortName = shortname;
                                        break;
                                    }

                                case "locality":
                                    {
                                        Address_locality = longname;
                                        //Address_locality = shortname; //Om Longname visar sig innehålla konstigheter kan man använda shortname istället
                                        break;
                                    }

                                case "sublocality":
                                    {
                                        Address_sublocality = longname;
                                        break;
                                    }

                                case "neighborhood":
                                    {
                                        Address_neighborhood = longname;
                                        break;
                                    }

                                case "colloquial_area":
                                    {
                                        Address_colloquial_area = longname;
                                        break;
                                    }

                                case "administrative_area_level_1":
                                    {
                                        Address_administrative_area_level_1 = longname;
                                        break;
                                    }

                                case "administrative_area_level_2":
                                    {
                                        Address_administrative_area_level_2 = longname;
                                        break;
                                    }

                                case "administrative_area_level_3":
                                    {
                                        Address_administrative_area_level_3 = longname;
                                        break;
                                    }

                                default:
                                    fHit = false;
                                    break;
                            }


                            if (fHit)
                            {
                                Console.Write(typename);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("\tL: " + longname + "\tS:" + shortname + "\r\n");
                                Console.ForegroundColor = ConsoleColor.Gray;
                            }
                        }

                        catch (Exception e)
                        {
                            //Node missing either, longname, shortname or typename
                            fHit = false;
                            Console.Write(" Invalid data: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("\tX: " + xn.InnerXml + "\r\n");
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }


                    }

                    //Console.ReadKey();
                    return (element.InnerText);
                }

            }
            catch (Exception ex)
            {
                return ("(Address lookup failed: ) " + ex.Message);
            }
        }

    }
}