//------------------------------------------------------------------------------
// <auto-generated>
//    Ce code a été généré à partir d'un modèle.
//
//    Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//    Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CARYS_BackOffice.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Paiement
    {
        public Paiement()
        {
            this.PanierCommandes = new HashSet<PanierCommande>();
        }
    
        public int IdPaiement { get; set; }
        public double MontantPaiement { get; set; }
        public string ModePaiement { get; set; }
        public int NumeroCommande { get; set; }
    
        public virtual PanierCommande PanierCommande { get; set; }
        public virtual ICollection<PanierCommande> PanierCommandes { get; set; }
    }
}
