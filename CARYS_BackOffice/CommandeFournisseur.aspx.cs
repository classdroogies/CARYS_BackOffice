﻿using CARYS_BackOffice.App_Code.Entity;
using CARYS_BackOffice.App_Code.Manager;
using CARYS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Data;
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
                PaginationVisibilite();
                CommandeExist();
            }

            if (Session["Fournisseur"] != null)
            {
                DropDownListFournisseur.SelectedValue = Session["Fournisseur"].ToString();
            }
            VisibilitePanier();
        }

        /// <summary>
        /// Gère l'affichage de la pagination en fonction du nombre d'article
        /// </summary>
        private void PaginationVisibilite()
        {
            // Gestion de l'affichage de la pagination
            if (((List<ArticleFournisseur>)ListViewArticles.DataSource).Count > 20)
            {
                DataPager.Visible = true;
            }
            else
            {
                DataPager.Visible = false;
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
            GroupBtnCommande.Visible = true;
            // Activation du panier
            panier.Visible = true;
        }

        /// <summary>
        /// Méthode qui permet d'activer les commandes de création d'une nouvelle commande
        /// </summary>
        private void EnableFormCommande()
        {
            GroupBtnCommande.Visible = false;
            panier.Visible = false;
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
            PaginationVisibilite();
        }

        /// <summary>
        /// Methode appelée quand l'utilisateur clic sur le bouton nouvelle commmande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnNouvelleCommandeFournisseur_Click(object sender, EventArgs e)
        {
            // Désactivation du message d'erreur
            LblError.Text = "";
            LblError.Visible = false;
            // Désactivation du message de success
            LblSuccess.Text = "";
            LblSuccess.Visible = false;
            // Création de la commande dans la session si elle n'existe pas 
            if (Session["CommandeFournisseur"] == null && DropDownListFournisseur.SelectedIndex != 0)
            {
                Session.Add("Fournisseur", DropDownListFournisseur.SelectedValue);
                Session.Add("CommandeFournisseur", new List<ArticleCommandeFournisseur>());
                GridViewCommande.DataSource = Session["CommandeFournisseur"];
                GridViewCommande.DataBind();
                CommandeExist();
            }
            else
            {
                LblError.Text = "Veuillez choisir un fournisseur.";
                LblError.Visible = true;
            }
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur bouton annuler commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnAnnulerCommande_Click(object sender, EventArgs e)
        {
            // Désactivation du message d'erreur
            LblError.Text = "";
            LblError.Visible = false;
            ClearCommande();
        }

        /// <summary>
        /// Méthode qui permet de vider le panier de commande
        /// </summary>
        private void ClearCommande()
        {
            if (Session["CommandeFournisseur"] != null)
            {
                Session.Remove("Fournisseur");
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
            // Désactivation du message d'erreur
            LblError.Text = "";
            LblError.Visible = false;

            // Récupération de l'item sélectionné
            ListViewItem item = ((LinkButton)sender).NamingContainer as ListViewItem;
            //Récupération des controles
            Label lblLibelle = (Label)item.FindControl("LblLibelle");
            Label lblPrix = (Label)item.FindControl("LblPrix");
            TextBox txtQuantite = (TextBox)item.FindControl("TxtQuantite");

            int reference = 0;
            int quantite = 0;

            // Vérification de la référence de l'article
            if (!int.TryParse(((LinkButton)sender).CommandArgument, out reference))
            {
                LblError.Text = "Référence article incorrect !";
                LblError.Visible = true;
            }
            // Vérification de la quantité
            else if (!int.TryParse(txtQuantite.Text, out quantite) && quantite <= 0)
            {
                LblError.Text = "Quantité saisie incorrect, la valeur doit être un nombre entier supérieur à 0 !";
                LblError.Visible = true;
            }
            else if (Session["CommandeFournisseur"] != null)
            {
                // Récupération du panier
                List<ArticleCommandeFournisseur> panier = ((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"]);

                // Si le panier contient déjà l'article
                if (panier.Contains(new ArticleCommandeFournisseur(reference, lblLibelle.Text, double.Parse(lblPrix.Text), quantite)))
                {
                    // On  récupère la ligne de commande
                    ArticleCommandeFournisseur article = panier.Find(x => x.Reference == reference);
                    // On ajoute la quantité 
                    article.QuantiteCommandeFournisseur += quantite;
                }
                else
                {
                    // Sinon on créé une nouvelle ligne d'article
                    panier.Add(new ArticleCommandeFournisseur(reference, lblLibelle.Text, double.Parse(lblPrix.Text), quantite));
                }
                // On met à jour la grille
                GridViewCommande.DataSource = Session["CommandeFournisseur"];
                GridViewCommande.DataBind();
                // On modifie le total
                LblPanierTotal.Text = String.Format("{0:0.00}", TotalPanier(panier));
            }
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur utilise la pagination
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ListViewArticles_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            //set current page startindex, max rows and rebind to false
            DataPager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            // Chargement de la liste de tous les articles fournisseurs
            ListViewArticles.DataSource = CatalogueFournisseur.GetArticles();
            ListViewArticles.DataBind();
            CommandeExist();
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur la bouton de validation d'un commande
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnValiderCommande_Click(object sender, EventArgs e)
        {
            // Désactivation du message d'erreur
            LblError.Text = "";
            LblError.Visible = false;

            List<ArticleCommandeFournisseur> panier = ((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"]);

            // Si le panier n'est pas vide
            if (Session["CommandeFournisseur"] != null && panier.Count > 0)
            {
                // Si la sauvegarde du panier c'est bien passée
                if (CommandesFournisseur.SaveCommandeFournisseur(int.Parse(DropDownListFournisseur.SelectedValue), panier))
                {
                    LblSuccess.Text = "La commande à bien à été créée !";
                    LblSuccess.Visible = true;
                    ClearCommande();
                }
            }
            else
            {
                LblError.Text = "Le panier est vide !";
                LblError.Visible = true;
            }
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur un lien supprimer du panier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewCommande_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            if (Session["CommandeFournisseur"] != null && e.RowIndex >= 0)
            {
                ((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"]).RemoveAt(e.RowIndex);
                GridViewCommande.DataSource = Session["CommandeFournisseur"];
                GridViewCommande.DataBind();
                Session["VisibilitePanier"] = true;
                // On modifie le total
                LblPanierTotal.Text = String.Format("{0:0.00}", TotalPanier(((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"])));
            }
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur le lien Modifier du panier pour passer en mode édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewCommande_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // On met à jour l'index de modification de la ligne de la grille
            GridViewCommande.EditIndex = e.NewEditIndex;
            // On met à jour la grille
            GridViewCommande.DataSource = Session["CommandeFournisseur"];
            GridViewCommande.DataBind();
            // On modifie le total
            LblPanierTotal.Text = String.Format("{0:0.00}", TotalPanier(((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"])));
            Session["VisibilitePanier"] = true;
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur le lien valider du panier en mode édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewCommande_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Récupération du controle contenant la quantité
            TextBox txtQuantite = (TextBox)GridViewCommande.Rows[e.RowIndex].FindControl("TxtQuantiteCommande");
            // Vérification de la valeur saisie
            int quantite = 0;
            if (!int.TryParse(txtQuantite.Text, out quantite) && quantite <= 0)
            {
                LblError.Text = "Quantité saisie incorrect, la valeur doit être un nombre entier supérieur à 0 !";
                LblError.Visible = true;
            }
            else
            {
                // Récupération du panier
                List<ArticleCommandeFournisseur> panier = ((List<ArticleCommandeFournisseur>)Session["CommandeFournisseur"]);
                // On  récupère la ligne de commande
                ArticleCommandeFournisseur article = panier.Find(x => x.Reference == (int)e.Keys["Reference"]);
                // On modifie la quantité 
                article.QuantiteCommandeFournisseur = quantite;
                // On modifie le total
                LblPanierTotal.Text = String.Format("{0:0.00}", TotalPanier(panier));
            }
            GridViewCommande.EditIndex = -1;
            GridViewCommande.DataSource = Session["CommandeFournisseur"];
            GridViewCommande.DataBind();
            Session["VisibilitePanier"] = true;
        }

        /// <summary>
        /// Méthode appelée quand l'utilisateur clic sur le lien annuler du panier en mode édition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridViewCommande_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

            // On met à jour l'index de modification à -1 pour aucune ligne en mode édition dans la grille
            GridViewCommande.EditIndex = -1;
            // On met à jour la grille
            GridViewCommande.DataSource = Session["CommandeFournisseur"];
            GridViewCommande.DataBind();
            Session["VisibilitePanier"] = true;
        }

        /// <summary>
        /// Calcul le cout total de la commande
        /// </summary>
        /// <param name="panier">le panier de la commande a calculer</param>
        /// <returns>le total de la commande</returns>
        private double TotalPanier(List<ArticleCommandeFournisseur> panier)
        {
            double total = 0;

            foreach (ArticleCommandeFournisseur item in panier)
            {
                total += item.PrixFournisseur * item.QuantiteCommandeFournisseur;
            }
            return total;
        }

        /// <summary>
        /// Gestion de l'affichage du panier au rechargement de la page
        /// </summary>
        private void VisibilitePanier()
        {
            if ((bool)Session["VisibilitePanier"])
            {
                // Si le panier était visible avant le rafraichissement
                // on ajout la class css open pour afficher le panier
                panier.Attributes["class"] = "btn-group open";
                Session["VisibilitePanier"] = false;
            }
            else
            {
                panier.Attributes["class"] = "btn-group";
            }
        }
    }
}