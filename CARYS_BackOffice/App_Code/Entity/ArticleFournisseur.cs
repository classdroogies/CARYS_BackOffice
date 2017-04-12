using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice.App_Code.Entity
{
    /// <summary>
    /// Entité représentant un article d'un fournisseur
    /// </summary>
    public class ArticleFournisseur
    {
        // L'id de l'article à commander
        public int Reference { get; set; }
        // Le nom de l'article
        public string LibelleArticle { get; set; }
        // Le prix fournisseur de l'article
        public double PrixFournisseur { get; set; }
        // Quantité en stock
        public int QuantiteStock { get; set; }
        // Seuil stock
        public int SeuilStock { get; set; }
        // Genre 
        public string LibelleGenre { get; set; }
        // Categorie
        public string LibelleCategorie { get; set; }
    }
}