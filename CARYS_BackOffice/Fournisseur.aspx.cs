using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CARYS_BackOffice
{
    public partial class Fournisseur : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void DvAddFournisseur_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
        {
            GridViewFournisseurs.DataBind();
        }
    }
}