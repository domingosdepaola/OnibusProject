using OnibusAPI.Util.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnibusAPI.Util
{
    public static class Token
    {
        public static bool PermiteAcesso(string token, string chave)
        {
            CryptLib critografia = new CryptLib();
            string openedToken = critografia.Decript(token, chave);
            if (openedToken == "EcoSistemasInovacaoEmGestaoDeSaude")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}