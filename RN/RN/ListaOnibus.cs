using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RN
{
    public class ListaOnibus
    {
        private List<OnibusBO> listaOnibus;
        private static ListaOnibus instance;
        public bool Atualiza { get; set; }
        private static object objLockLista = new object();
        private ListaOnibus()
        {
           
        }

        public static ListaOnibus Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ListaOnibus();
                }
                return instance;
            }
        }
        public List<OnibusBO> Lista
        {
            get
            {
                lock (objLockLista)
                {
                    if (listaOnibus == null)
                    {
                        this.listaOnibus = new List<OnibusBO>();
                    }
                    return listaOnibus;
                }

            }
            set 
            {
                lock (objLockLista) 
                {
                    listaOnibus = value;
                }
            }
        }
    }
}
