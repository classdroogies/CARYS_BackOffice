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

            IEnumerable<ArticleFournisseur> liste = from article in _bdd.Articles
                                                    join stock in _bdd.StockArticles on article.Reference equals stock.Reference
                                                    select new ArticleFournisseur
                                                    {
                                                        Reference = article.Reference,
                                                        LibelleArticle = article.LibelleArticle,
                                                        PrixFournisseur = (double)article.PrixAchat,
                                                        SeuilStock = 0,
                                                        QuantiteStock = 0
                                                    };

            return liste.ToList();
        }
    }
}