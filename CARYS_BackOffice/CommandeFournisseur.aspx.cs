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
            if (!IsPostBack)
            {
                // Chargement de la liste de tous les articles fournisseurs
                ListViewArticles.DataSource = CatalogueFournisseur.GetArticles();
                ListViewArticles.DataBind();
                CommandeExist();
            }
        }

        protected void ListViewArticles_Load(object sender, EventArgs e)
        {
            CommandeExist();
        }

        private void CommandeExist()
        {
            // Désactivation du bouton création de commande 
            // si elle existe déjà dans la session
            if (Session["CommandeFournisseur"] != null)
            {
                // Désactivation des commandes de création d'une nouvelle commande
                DisableFormCommande();
                // Activation des boutons de commande sur les articles
                EnableFormArticleCommande();
            }
            else
            {
                EnableFormCommande();
                DisableFormArticleCommande();
            }
        }

        /// <summary>
        /// Méthode qui permet de désactiver tous les boutons et les champs de texte de commande de la liste des articles
        /// </summary>
        private void DisableFormArticleCommande()
        {
            // Parcours de tous les items de la listview contenant les articles
            foreach (ListViewDataItem item in ListViewArticles.Items)
            {
                // Pour chaque controls d'un item on rend invisible la saisie 
                // de la quantité et le bouton de commande
                foreach (Control control in item.Controls)
                {
                    if (control.GetType() == typeof(LinkButton))
                    {
                        control.Visible = false;
                    }
                    else if (control.GetType() == typeof(TextBox))
                    {
                        control.Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Méthode qui permet de rendre visible tous les boutons et les champs de texte de commande de la liste des articles
        /// </summary>
        private void EnableFormArticleCommande()
        {
            // Parcours de tous les items de la listview contenant les articles
            foreach (ListViewDataItem item in ListViewArticles.Items)
            {
                // Pour chaque controls d'un item on rend visible la saisie 
                // de la quantité et le bouton de commande
                foreach (Control control in item.Controls)
                {
                    if (control.GetType() == typeof(LinkButton))
                    {
                        control.Visible = true;
                    }
                    else if (control.GetType() == typeof(TextBox))
                    {
                        control.Visible = true;
                    }
                }
            }
        }

        /// <summary>
        /// Désactive les composants afin de ne pas choisir un autre fournisseur durant la commande
        /// </summary>
        private void DisableFormCommande()
        {
            // Désactivation du bouton de création d'une nouvelle commande
            BtnNouvelleCommande.Visible = false;
            // Désactivation de la dropdown de sélection du fournisseur
            DropDownListFournisseur.Enabled = false;
            //Activation du bouton annuler
            BtnAnnulerCommande.Visible = true;
        }

        /// <summary>
        /// Méthode qui permet d'activer les commandes de création d'une nouvelle commande
        /// </summary>
        private void EnableFormCommande()
        {
            BtnAnnulerCommande.Visible = false;
            BtnNouvelleCommande.Visible = true;
            DropDownListFournisseur.Enabled = true;
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
            CommandeExist();
        }

        /// <summary>
        /// Methode appelée quand l'utilisateur clic sur le bouton nouvelle commmande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNouvelleCommandeFournisseur_Click(object sender, EventArgs e)
        {
            // Création de la commande dans la session si elle n'existe pas 
            if (Session["CommandeFournisseur"] == null && DropDownListFournisseur.SelectedIndex != 0)
            {

                Session.Add("CommandeFournisseur", new List<ArticleCommandeFournisseur>());
                GridViewCommande.DataSource = Session["CommandeFournisseur"];
                GridViewCommande.DataBind();
                CommandeExist();
            }
            else
            {
                Response.Write("Veuillez choisir un fournisseur.");
            }
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur bouton annuler commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnnulerCommande_Click(object sender, EventArgs e)
        {
            if (Session["CommandeFournisseur"] != null)
            {
                Session.Remove("CommandeFournisseur");
                GridViewCommande.DataSource = null;
                GridViewCommande.DataBind();
                CommandeExist();
            }
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
            else if (Session["CommandeFournisseur"] != null)
            {
                ((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"]).Add(new ArticleCommandeFournisseur(reference, lblLibelle.Text, double.Parse(lblPrix.Text), quantite));
                GridViewCommande.DataSource = Session["CommandeFournisseur"];
                GridViewCommande.DataBind();
            }
        }

        protected void ListViewArticles_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //set current page startindex, max rows and rebind to false
            DataPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            // Chargement de la liste de tous les articles fournisseurs
            ListViewArticles.DataSource = CatalogueFournisseur.GetArticles();
            ListViewArticles.DataBind();
            CommandeExist();
        }
    }
}