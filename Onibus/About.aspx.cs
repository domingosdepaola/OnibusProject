using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Onibus
{
    public partial class About : Page
    {
        private bool Emobile
        {
            get
            {
                if (Request.QueryString["mobile"] != null && Request.QueryString["mobile"].ToString() == "s")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["NaoBoasVindas"] = true;
        }
        protected override void OnPreInit(EventArgs e)
        {
            if (Emobile)
            {
                this.MasterPageFile = "~/Site.Mobile.Master";
            }
            base.OnPreInit(e);
        }
    }
}