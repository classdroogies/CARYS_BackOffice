using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice
{
    public class ArticlesCommandeFournisseur
    {
        public int NumeroCommandeFournisseur { get; set; }
        public string LibelleArticle { get; set; }
        public double PrixFournisseur { get; set; }
        public int QuantitéCommandeFournisseur { get; set; }
    }
}

