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
        /// Méthode appelée quand l'utilisateur selectionne le fournisseur auquel on passe une commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DropDownListFournisseur_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Mise à jour de la liste des articles associés au fournisseur sélectionné
            DropDownListArticle.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNouvelleCommandeFournisseur_Click(object sender, EventArgs e)
        {
            // Récupération de l'id du fournisseur sélectionné
            int id = int.Parse(DropDownListFournisseur.SelectedValue);
            // Récupération du numero de la nouvelle commande
            int idCommande = CommandeFournisseurManager.CreateCommandeFournisseur(Context, id);
            HiddenNumeroCommande.Value = idCommande.ToString();
            // Mise à jour des données
            GridViewCommande.DataSource = CommandeFournisseurManager.GetArticlesCommandeAtCommandeFournisseur(idCommande);
            GridViewCommande.DataBind();
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur le bouton ajouter article
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAddArticle_Click(object sender, EventArgs e)
        {
            int idCommande = 0;

            if (int.TryParse(HiddenNumeroCommande.Value, out idCommande))
            {
                int quantite = 0;
                if (int.TryParse(TextQuantite.Text, out quantite))
                {
                    CommandeFournisseurManager.AddArticleCommandeFournisseur(Context, idCommande, quantite, int.Parse(DropDownListArticle.SelectedValue));
                    GridViewCommande.DataSource = CommandeFournisseurManager.GetArticlesCommandeAtCommandeFournisseur(idCommande);
                    GridViewCommande.DataBind();
                }
            }

        }
    }
}