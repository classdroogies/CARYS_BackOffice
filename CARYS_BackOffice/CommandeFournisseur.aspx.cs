using CARYS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CARYS_BackOffice
{
    public partial class CommandeFournisseur : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Méthode appelée après l'éxecution du controle entitydatasourse Article afin d'ajouter un filtrage des données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EdsArticle_QueryCreated(object sender, QueryCreatedEventArgs e)
        {
            // Récupération de l'id du fournisseur sélectionné
            int id = int.Parse(DropDownListFournisseur.SelectedValue);
            // Récupération de la requête initiale
            IQueryable<Article> data = e.Query.Cast<Article>();
            // Modification de la requète en ne récupérant que les articles du fournisseur sélectionné
            e.Query = from article in data
                      where article.IdFournisseur == id
                      select article;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListFournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownListArticle.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnValiderFournisseur_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TextIdCommande.Text))
            {
                // Récupération de l'id du fournisseur sélectionné
                int id = int.Parse(TextIdCommande.Text);

                BddEntities bdd = new BddEntities();

                var liste = from article in bdd.Articles
                            join ligneCommande in bdd.LigneCommandeFournisseurs on article.Reference equals ligneCommande.Reference
                            where ligneCommande.NumeroCommandeFournisseur == id
                            select new { ligneCommande.NumeroCommandeFournisseur, article.LibelleArticle, article.PrixFournisseur, ligneCommande.QuantiteCommandeFournisseur };

                GridViewCommande.DataSource = liste.ToList();
                GridViewCommande.DataBind();
            }
        }
    }
}