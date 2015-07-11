using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Onibus.UserControls
{
    public partial class Mensagem : System.Web.UI.UserControl
    {
        private const string CSS_MESSAGE = "message";
        private const string CSS_WARN_MESSAGE = "warn-message";
        private const string CSS_ERROR_MESSAGE = "error-message";

        private string message = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            this.lblMensagem.Visible = this.message != null && this.message != string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ShowMessage(string message)
        {
            this.Show(message, CSS_MESSAGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ShowWarnMessage(string message)
        {
            this.Show(message, CSS_WARN_MESSAGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void ShowErrorMessage(string message)
        {
            this.Show(message, CSS_ERROR_MESSAGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="cssClass"></param>
        private void Show(string message, string cssClass)
        {
            this.message = message;
            this.lblMensagem.CssClass = cssClass;
            this.lblMensagem.Visible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        protected string Message
        {
            get
            {
                string msg = this.message;
                this.message = string.Empty;
                return msg;
            }
        }
    }
}