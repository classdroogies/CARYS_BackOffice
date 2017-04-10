using CARYS_BackOffice.App_Code.Entity;
using CARYS_BackOffice.App_Code.Manager;
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
            // Désactivation du bouton création de commande 
            // si elle existe déjà dans la session
            if (Session["CommandeFournisseur"] != null)
            {
                // Désactivation des commandes de création d'une nouvelle commande
                DisableFormCommande();
            }
        }

        private void DisableFormCommande()
        {
            // Désactivation du bouton de création d'une nouvelle commande
            BtnNouvelleCommande.Enabled = false;
            // Désactivation de la dropdown de sélection du fournisseur
            DropDownListFournisseur.Enabled = false;
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
            // Mise à jour de la grille contenant les articles
            GridViewArticle.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNouvelleCommandeFournisseur_Click(object sender, EventArgs e)
        {
            //// Récupération de l'id du fournisseur sélectionné
            //int id = int.Parse(DropDownListFournisseur.SelectedValue);
            //// Récupération du numero de la nouvelle commande
            //int idCommande = CommandeFournisseurManager.CreateCommandeFournisseur(Context, id);
            //HiddenNumeroCommande.Value = idCommande.ToString();
            //// Mise à jour des données
            //GridViewCommande.DataSource = CommandeFournisseurManager.GetArticlesCommandeAtCommandeFournisseur(idCommande);
            //GridViewCommande.DataBind();

            // Création de la commande dans la session si elle n'existe pas
            if (Session["CommandeFournisseur"] == null)
            {
                Session.Add("CommandeFournisseur", new List<ArticlesCommandeFournisseur>());
                GridViewCommande.DataSource = Session["CommandeFournisseur"];
                GridViewCommande.DataBind();
            }

            // Désactivation des commandes de création d'une nouvelle commande
            DisableFormCommande();
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur le bouton ajouter article
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAddArticle_Click(object sender, EventArgs e)
        {
            //int idCommande = 0;

            //if (int.TryParse(HiddenNumeroCommande.Value, out idCommande))
            //{
            //    int quantite = 0;
            //    if (int.TryParse(TextQuantite.Text, out quantite))
            //    {
            //        CommandeFournisseurManager.AddArticleCommandeFournisseur(Context, idCommande, quantite, int.Parse(DropDownListArticle.SelectedValue));
            //        GridViewCommande.DataSource = CommandeFournisseurManager.GetArticlesCommandeAtCommandeFournisseur(idCommande);
            //        GridViewCommande.DataBind();
            //    }
            //}

            // Vérification de la référence de l'article
            int reference = 0;
            int quantite = 0;
            if (!int.TryParse(TextQuantite.Text, out reference))
            {
                Context.Response.Write("Quantité saisie incorrect !");
            }
            // Vérification de la quantité
            else if (!int.TryParse(TextQuantite.Text, out quantite))
            {
                Context.Response.Write("Quantité saisie incorrect !");
            }
            else if (quantite <= 0)
            {
                Context.Response.Write("La quantité doit être supérieur 0 !");
            }
            else
            {
                //((List<ArticlesCommandeFournisseur>)Session["CommandeFournisseur"]).Add( ));
            }
        }

        protected void GridViewArticle_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}