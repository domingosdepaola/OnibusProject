using OnibusAPI.Util.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;

namespace OnibusAPI.Util
{
   public static class CriptUtil
    {
        public static string Chave
        {
            get
            {
                return "ptrs2015Master";
            }
        }
        private static CryptLib cript;
        public static CryptLib Cript
        {
            get
            {
                if (cript == null)
                {
                    cript = new CryptLib();
                }
                return cript;
            }
            set
            {
                cript = value;
            }
        }
        private static object objData = new object();
        private static List<DateTime> ultimasDatas;
        private static List<DateTime> DatasBloqueadas
        {
            get
            {
                lock (objData)
                {
                    if (ultimasDatas == null)
                    {
                        ultimasDatas = new List<DateTime>();
                    }
                    return ultimasDatas;
                }
            }
        }
        private static void LimpaDatasBloqueadas() 
        {
            try
            {
                List<DateTime> lstDatasToRemove = DatasBloqueadas.Where(x => x.AddMinutes(5) < DateTime.Now).ToList();
                foreach (DateTime item in lstDatasToRemove)
                {
                    DatasBloqueadas.Remove(item);
                }
            }
            catch { }
        }
        private static string GetChave(string criptKey)
        {
            string chave = Cript.Decript(criptKey, Chave);
            return chave;
        }
        private static DateTime? GetDateChave(string chaveStringComposta)
        {
            try
            {
                char[] chaveStringArray = chaveStringComposta.ToCharArray();
                string chaveString = chaveStringArray[2].ToString() +
                    chaveStringArray[7].ToString() +
                    chaveStringArray[13].ToString() +
                    chaveStringArray[19].ToString() +
                    chaveStringArray[4].ToString() +
                    chaveStringArray[16].ToString() +
                    chaveStringArray[0].ToString() +
                    chaveStringArray[32].ToString() +
                    chaveStringArray[9].ToString() +
                    chaveStringArray[22].ToString() +
                    chaveStringArray[5].ToString() +
                    chaveStringArray[34].ToString() +
                    chaveStringArray[10].ToString() +
                    chaveStringArray[23].ToString() +
                    chaveStringArray[40].ToString() +
                    chaveStringArray[36].ToString() +
                    chaveStringArray[38].ToString();
                string dataString = chaveString;
                dataString = dataString.Insert(4, "-").Insert(7, "-").Insert(10, " ").Insert(13, ":").Insert(16, ":").Insert(19,".");
                DateTime data = Convert.ToDateTime(dataString, new CultureInfo("en-US"));
                return data;
            }
            catch
            {
                return null;
            }
        }
        public static string GetObjJsonAutorizado(string x, string y, string z)
        {
            if (Autoriza(x, y))
            {
                string jsonDecript = CriptUtil.Cript.Decript(z, CriptUtil.Chave);
                if (jsonDecript != null)
                {
                    return jsonDecript;
                }
            }
            return null;
        }
        public static bool Autoriza(string x, string y)
        {
            string dataChaveString = CriptUtil.PermiteChave(y);
            if (dataChaveString != null)
            {
                if (Token.PermiteAcesso(x, CriptUtil.Chave + dataChaveString))
                {
                    return true;
                }
            }
            return false;
        }
        public static string PermiteChave(string chaveCript)
        {
            try
            {
                //"02270550610521506185543304213385141";
                string chaveString = GetChave(chaveCript);
                DateTime? dataChave = GetDateChave(chaveString);
                if (dataChave != null)
                {
                    if (DatasBloqueadas.Contains(dataChave.Value))
                    {
                        return null;
                    }
                    else
                    {
                        if (dataChave.Value.AddMinutes(5) > DateTime.Now)
                        {
                            DatasBloqueadas.Add(dataChave.Value);
                            return dataChave.Value.ToString("yyyyMMddHHmmssfff");
                        }
                    }
                }
                return null;
            }
            finally 
            {
                Thread threadLimpeza = new Thread(LimpaDatasBloqueadas);
                threadLimpeza.Start();
            }
        }
    }
}
