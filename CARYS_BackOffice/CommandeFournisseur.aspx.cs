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

            if (!IsPostBack)
            {
                // Chargement de la liste de tous les articles fournisseurs
                ListViewArticles.DataSource = CatalogueFournisseur.GetArticles();
                ListViewArticles.DataBind();
            }
        }

        /// <summary>
        /// Désactive les composants afin de ne pas choisir un autre fournisseur durant la commande
        /// </summary>
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
            // Récupération de l'id du fournisseur
            int id = int.Parse(DropDownListFournisseur.SelectedValue);
            // Si un fournisseur est sélectionné on met à jour la grille
            if (id > 0)
            {
                ListViewArticles.DataSource = CatalogueFournisseur.GetArticlesFournisseur(id);
            }
            else
            {
                ListViewArticles.DataSource = CatalogueFournisseur.GetArticles();
            }
            // Mise à jour de la grille contenant les articles
            ListViewArticles.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNouvelleCommandeFournisseur_Click(object sender, EventArgs e)
        {
            // Création de la commande dans la session si elle n'existe pas
            if (Session["CommandeFournisseur"] == null)
            {
                Session.Add("CommandeFournisseur", new List<ArticleCommandeFournisseur>());
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
            // Récupération de l'item sélectionné
            ListViewItem item = ((LinkButton)sender).NamingContainer as ListViewItem;
            //Récupération des données
            Label lblLibelle = (Label)item.FindControl("LblLibelle");
            Label lblPrix = (Label)item.FindControl("LblPrix");
            TextBox txtQuantite = (TextBox)item.FindControl("TxtQuantite");

            int reference = 0;
            int quantite = 0;

            // Vérification de la référence de l'article
            if (!int.TryParse(((LinkButton)sender).CommandArgument, out reference))
            {
                Response.Write("Quantité saisie incorrect !");
            }
            // Vérification de la quantité
            else if (!int.TryParse(txtQuantite.Text, out quantite))
            {
                Response.Write("Quantité saisie incorrect !");
            }
            else if (quantite <= 0)
            {
                Response.Write("La quantité doit être supérieur 0 !");
            }
            else
            {
                ((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"]).Add(new ArticleCommandeFournisseur(reference, lblLibelle.Text, double.Parse(lblPrix.Text), quantite));
                GridViewCommande.DataSource = Session["CommandeFournisseur"];
                GridViewCommande.DataBind();
            }
        }
    }
}