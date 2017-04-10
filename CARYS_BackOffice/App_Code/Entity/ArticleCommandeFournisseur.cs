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
        // Le numéro de la commmande fournisseur
        public int NumeroCommandeFournisseur { get; set; }
        // L'id de l'article à commander
        public int Reference { get; set; }
        // Le nom de l'article
        public string LibelleArticle { get; set; }
        // Le prix fournisseur de l'article
        public double PrixFournisseur { get; set; }
        // Le quantité d'article commandé
        public int QuantiteCommmandeFournisseur { get; set; }
    }
}
