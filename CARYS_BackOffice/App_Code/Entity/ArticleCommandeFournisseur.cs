using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice.App_Code.Entity
{
    /// <summary>
    /// Entitée représentant une ligne de commande passée à un fournisseur
    /// </summary>
    public class ArticleCommandeFournisseur
    {
        // L'id de l'article à commander
        public int Reference { get; set; }
        // Le nom de l'article
        public string LibelleArticle { get; set; }
        // Le prix fournisseur de l'article
        public double PrixFournisseur { get; set; }
        // Le quantité d'article commandé
        public int QuantiteCommandeFournisseur { get; set; }


        public ArticleCommandeFournisseur()
        {
        }

        public ArticleCommandeFournisseur(int reference, string libelleArticle, double prixFournisseur, int quantite)
        {
            this.Reference = reference;
            this.LibelleArticle = libelleArticle;
            this.PrixFournisseur = prixFournisseur;
            this.QuantiteCommandeFournisseur = quantite;
        }
    }
}
