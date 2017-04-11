using CARYS_BackOffice.App_Code.Entity;
using CARYS_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice.App_Code.Manager
{
    /// <summary>
    /// Classe permettant la récupération des articles fournisseur
    /// </summary>
    public class CatalogueFournisseur
    {
        // On instancie la base
        private static BddEntities _bdd = new BddEntities();

        /// <summary>
        /// Contructeur privé pour empécher l'instanciation de la classe
        /// </summary>
        private CatalogueFournisseur()
        {
        }

        /// <summary>
        /// Méthode qui retourne la liste des articles proposés par un fournisseur
        /// </summary>
        /// <param name="idFournisseur">l'id du fournisseur pour qui on veut ses articles</param>
        /// <returns>la liste des articles</returns>
        public static List<ArticleFournisseur> GetArticlesFournisseur(int idFournisseur)
        {
            IEnumerable<ArticleFournisseur> liste;
            try
            {
                liste = from article in _bdd.Articles
                        join stock in _bdd.StockArticles on article.Reference equals stock.Reference 
                        where article.IdFournisseur == idFournisseur
                        select new ArticleFournisseur
                        {
                            Reference = article.Reference,
                            LibelleArticle = article.LibelleArticle,
                            PrixFournisseur = article.PrixAchat.Value ,
                            SeuilStock = stock.Seuil,
                            QuantiteStock = stock.Quantite
                        };
            }
            catch (Exception)
            {

                throw;
            }
            return liste.ToList();
        }

        /// <summary>
        /// Méthode qui retourne la liste des articles de tous les fournisseurs
        /// </summary>
        /// <returns>La liste de tous les articles</returns>
        public static List<ArticleFournisseur> GetArticles()
        {
            IEnumerable<ArticleFournisseur> liste;
            try
            {
                liste = from article in _bdd.Articles
                        join stock in _bdd.StockArticles on article.Reference equals stock.Reference
                        select new ArticleFournisseur
                        {
                            Reference = article.Reference,
                            LibelleArticle = article.LibelleArticle,
                            PrixFournisseur = article.PrixAchat.Value,
                            SeuilStock = stock.Seuil,
                            QuantiteStock = stock.Quantite
                        };
            }
            catch (Exception)
            {

                throw;
            }
            return liste.ToList();
        }
    }
}