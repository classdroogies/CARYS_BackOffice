using CARYS_BackOffice.App_Code.Entity;
using CARYS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice.App_Code.Manager
{
    public class CommandeFournisseurManager
    {
        // On instancie la base
        private static BddEntities _bdd = new BddEntities();

        /// <summary>
        /// Constructeur privé pour le manager 
        /// </summary>
        private CommandeFournisseurManager()
        {
        }

        /// <summary>
        /// Méthode permettant la récupération de les commandes correspondantes à un fournisseur
        /// </summary>
        /// <param name="idCommandeFournisseur">l'id du fournisseur pour qui l'on veut récupérer ses commandes</param>
        /// <returns>la liste des commandes passées au fournisseur</returns>
        public static List<ArticleCommandeFournisseur> GetArticlesCommandeAtCommandeFournisseur(int idCommandeFournisseur)
        {
            IEnumerable<ArticleCommandeFournisseur> liste = from article in _bdd.Articles
                                                             join ligneCommande in _bdd.LigneCommandeFournisseurs on article.Reference
                                                             equals ligneCommande.Reference
                                                             join stock in _bdd.StockArticles on article.Reference
                                                             equals stock.Reference
                                                             where ligneCommande.NumeroCommandeFournisseur == idCommandeFournisseur
                                                             select new ArticleCommandeFournisseur
                                                             {
                                                                 NumeroCommandeFournisseur = ligneCommande.NumeroCommandeFournisseur,
                                                                 LibelleArticle = article.LibelleArticle,
                                                                 PrixFournisseur = (double)article.PrixAchat,
                                                                 QuantiteCommandeFournisseur = ligneCommande.QuantiteCommandeFournisseur
                                                             };
            return liste.ToList();
        }

        /// <summary>
        /// Permet de créer une nouvelle commande fournisseur
        /// </summary>
        /// <returns>la liste des articles de la nouvelle commande</returns>
        public static int CreateCommandeFournisseur(HttpContext context, int idCommandeFournisseur)
        {
            int idNewCommande = 0;
            try
            {
                //// Création de la commande
                //Models.CommandeFournisseur commande = new Models.CommandeFournisseur();
                //commande.DateCommandeFournisseur = DateTime.Now;
                //commande.IdFournisseur = idCommandeFournisseur;
                //// Ajout de la commande à l'entité de la base
                //_bdd.CommandeFournisseurs.Add(commande);
                //// Mise à jour de la base
                //_bdd.SaveChanges();
                //// Mise à jour de l'id de la nouvelle commande
                //idNewCommande = _bdd.CommandeFournisseurs.Select(c => c.NumeroCommandeFournisseur).OrderByDescending(p => p).FirstOrDefault();

            }
            catch (Exception)
            {
                context.Response.Write("Erreur d'ajout !");
                //throw;
            }
            // Retourne l'id de la nouvelle commande
            return idNewCommande;
        }

        /// <summary>
        /// Permet l'ajout d'un article à la commande fournisseur en cours
        /// </summary>
        /// <param name="context">Le context de la page</param>
        /// <param name="idCommande">L'id de la commande</param>
        /// <param name="quantite">Le nombre d'article à commander</param>
        /// <param name="reference">La référence de l'article à commander</param>
        public static void AddArticleCommandeFournisseur(HttpContext context, int idCommande, int quantite, int reference)
        {
            try
            {
                // Vérification de la présence de l'article dans la commande
                bool articleExist = (from a in _bdd.LigneCommandeFournisseurs
                                     where (a.Reference == reference && a.NumeroCommandeFournisseur == idCommande)
                                     select a.NumeroCommandeFournisseur).Any();
                if (articleExist)
                {
                    context.Response.Write("Article déja dans la commande!");
                }
                else
                {
                    // Ajout d'un article à la commande
                    LigneCommandeFournisseur ligneCommande = new LigneCommandeFournisseur();
                    ligneCommande.NumeroCommandeFournisseur = idCommande;
                    ligneCommande.QuantiteCommandeFournisseur = quantite;
                    ligneCommande.Reference = reference;
                    // Ajout de la ligne de commande à l'entité de la base
                    _bdd.LigneCommandeFournisseurs.Add(ligneCommande);
                    // Mise à jour de la base
                    _bdd.SaveChanges();
                }
            }
            catch (Exception)
            {
                context.Response.Write("Erreur d'ajout !");
                //throw;
            }
        }
    }
}
