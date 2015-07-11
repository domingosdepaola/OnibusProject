using RN;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;

namespace MvcApplication1.Util
{
    public class ProcessoUtil
    {
        static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private static ProcessoUtil instance;
        Thread threadProcessoParalelo;
        private ProcessoUtil()
        {

        } 

        public static ProcessoUtil Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProcessoUtil();
                }
                return instance;
            }
        }
        private void Processa()
        {
            DateTime dataUltimaExecucao = DateTime.Now.AddHours(-1);
            while (true)
            {
                try
                {
                    int tempoAtualizacao = 60;
                    try
                    {
                        tempoAtualizacao = Convert.ToInt32(ConfigurationManager.AppSettings["tempoAtualizacao"]);
                    }
                    catch { }
                    if ((dataUltimaExecucao.AddSeconds(tempoAtualizacao) < DateTime.Now))
                    {
                        OnibusRN onibusRN = new OnibusRN();
                        onibusRN.AtualizaListaTotalOnibus();
                        dataUltimaExecucao = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    log.ErrorException(ex.Message, ex);
                }
            }
        }
        public void ProcessamentoParalelo()
        {
            if (threadProcessoParalelo == null)
            {
                threadProcessoParalelo = new Thread(Processa);
            }
            if (threadProcessoParalelo.ThreadState != ThreadState.Running)
            {
                threadProcessoParalelo.Start();
            }
        }
    }
}