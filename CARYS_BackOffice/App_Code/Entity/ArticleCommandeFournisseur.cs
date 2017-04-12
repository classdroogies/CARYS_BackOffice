using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice.App_Code.Entity
{
    /// <summary>
    /// Entitée représentant une ligne de commande passée à un fournisseur
    /// </summary>
    public class ArticleCommandeFournisseur : IEquatable<ArticleCommandeFournisseur>
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

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            ArticleCommandeFournisseur objAsArticleCommandeFournisseur = obj as ArticleCommandeFournisseur;
            if (objAsArticleCommandeFournisseur == null) return false;
            else return Equals(objAsArticleCommandeFournisseur);
        }

        public override int GetHashCode()
        {
            return Reference;
        }

        public bool Equals(ArticleCommandeFournisseur other)
        {
            if (other == null) return false;
            return (this.Reference.Equals(other.Reference));
        }
    }
}
