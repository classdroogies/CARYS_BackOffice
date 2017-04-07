
using CARYS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice
{ 
    public class CommandeFournisseurManager
    {
        // Récupération de l'entité de la base
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
        public static List<ArticlesCommandeFournisseur> GetArticlesCommandeAtCommandeFournisseur(int idCommandeFournisseur)
        {
            IEnumerable<ArticlesCommandeFournisseur> liste = from article in _bdd.Articles
                                                             join ligneCommande in _bdd.LigneCommandeFournisseurs on article.Reference
                                                             equals ligneCommande.Reference
                                                             where ligneCommande.NumeroCommandeFournisseur == idCommandeFournisseur
                                                             select new ArticlesCommandeFournisseur
                                                             {
                                                                 NumeroCommandeFournisseur = ligneCommande.NumeroCommandeFournisseur,
                                                                 LibelleArticle = article.LibelleArticle,
                                                                 PrixFournisseur = (double)article.PrixFournisseur,
                                                                 QuantitéCommandeFournisseur = ligneCommande.QuantiteCommandeFournisseur
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
                // Création de la commande
                Models.CommandeFournisseur commande = new Models.CommandeFournisseur();
                commande.DateCommandeFournisseur = DateTime.Now;
                commande.IdFournisseur = idCommandeFournisseur;
                // Ajout de la commande à l'entité de la base
                _bdd.CommandeFournisseurs.Add(commande);
                // Mise à jour de la base
                _bdd.SaveChanges();
                // Mise à jour de l'id de la nouvelle commande
                idNewCommande = _bdd.CommandeFournisseurs.Select(c => c.NumeroCommandeFournisseur).OrderByDescending(p => p).FirstOrDefault();
            }
            catch (Exception)
            {
                context.Response.Write("Erreur d'ajout !");
                //throw;
            }
            // Retourne l'id de la nouvelle commande
            return idNewCommande;
        }

        public static void AddArticleCommandeFournisseur(HttpContext context, int idCommande, int quantite, int reference)
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
}
