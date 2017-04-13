using CARYS_BackOffice.App_Code.Entity;
using CARYS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice.App_Code.Manager
{
    public class CommandesFournisseur
    {
        // On instancie la base
        private static BddEntities _bdd = new BddEntities();

        /// <summary>
        /// Constructeur privé pour le manager 
        /// </summary>
        private CommandesFournisseur()
        {
        }

        /// <summary>
        /// Méthode permettant la récupération de les commandes correspondantes à un fournisseur
        /// </summary>
        /// <param name="idCommandeFournisseur">l'id du fournisseur pour qui l'on veut récupérer ses commandes</param>
        /// <returns>la liste des commandes passées au fournisseur</returns>
        public static List<ArticleCommandeFournisseur> GetCommandesFournisseur(int idCommandeFournisseur)
        {
            IEnumerable<ArticleCommandeFournisseur> liste = from article in _bdd.Articles
                                                            join ligneCommande in _bdd.LigneCommandeFournisseurs on article.Reference
                                                            equals ligneCommande.Reference
                                                            join stock in _bdd.StockArticles on article.Reference
                                                            equals stock.Reference
                                                            where ligneCommande.NumeroCommandeFournisseur == idCommandeFournisseur
                                                            select new ArticleCommandeFournisseur
                                                            {
                                                                LibelleArticle = article.LibelleArticle,
                                                                PrixFournisseur = (double)article.PrixAchat,
                                                                QuantiteCommandeFournisseur = ligneCommande.QuantiteCommandeFournisseur
                                                            };
            return liste.ToList();
        }

        /// <summary>
        /// Permet de créer une nouvelle commande fournisseur
        /// </summary>
        /// <returns>l'id de la nouvelle commande ou 0 si il y eu une erreur d'ajout</returns>
        private static int CreateCommandeFournisseur(int idCommandeFournisseur)
        {
            int idNewCommande = 0;
            try
            {
                // Création de la commande
                Models.CommandeFournisseur commande = new Models.CommandeFournisseur();
                // La date actuelle pour la création de la commande
                commande.DateCommandeFournisseur = DateTime.Now;
                // L'id du fournisseur pour qui on passe la commande
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
                throw;
            }
            // Retourne l'id de la nouvelle commande ou 0 si il y a une erreur
            return idNewCommande;
        }

        /// <summary>
        /// Permet la sauvegarde de la nouvelle commande dans la base
        /// </summary>
        /// <param name="idFournisseur">L'id du fournisseur pour qui on passe la commande</param>
        /// <param name="commande">La liste des articles de la nouvelle commande</param>
        public static bool SaveCommandeFournisseur(int idFournisseur, List<ArticleCommandeFournisseur> commande)
        {
            bool commandeSave = true;
            try
            {
                // Si la nouvelle commande n'est pas vide
                if (commande.Count > 0)
                {
                    // Création de la nouvelle commande
                    int idNewCommande = CreateCommandeFournisseur(idFournisseur);
                    // Si la création de la nouvelle commande c'est bien passée
                    if (idNewCommande > 0)
                    {
                        // On parcours la nouvelle commande
                        foreach (ArticleCommandeFournisseur article in commande)
                        {
                            // Ajout d'un article à la commande
                            LigneCommandeFournisseur ligneCommande = new LigneCommandeFournisseur();
                            ligneCommande.NumeroCommandeFournisseur = idNewCommande;
                            ligneCommande.QuantiteCommandeFournisseur = article.QuantiteCommandeFournisseur;
                            ligneCommande.PrixUnitaireFournisseur = article.PrixFournisseur;
                            ligneCommande.Reference = article.Reference;
                            // Ajout de la ligne de commande à l'entité de la base
                            _bdd.LigneCommandeFournisseurs.Add(ligneCommande);
                        }
                        // Mise à jour de la base
                        _bdd.SaveChanges();
                    }
                    else
                    {
                        commandeSave = false;
                    }
                }
                else
                {
                    commandeSave = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return commandeSave;
        }
    }
}
