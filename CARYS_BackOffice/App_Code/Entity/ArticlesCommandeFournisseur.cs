using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice.App_Code.Entity
{
    /// <summary>
    /// Entitée représentant une ligne de commande pour un fournisseur
    /// </summary>
    public class ArticlesCommandeFournisseur
    {
        // Le numéro de la commmande fournisseur
        private int _numeroCommandeFournisseur;
        // L'id de l'article à commander
        private int _reference;
        // Le nom de l'article
        private string _libelleArticle;
        // Le prix fournisseur de l'article
        private double _prixFournisseur;
        // Le quantité d'article commandé
        private int _quantiteCommmandeFournisseur;
        // Quantité en stock
        private int _quantiteStock;
        // Seuil stock
        private int _seuilStock;


        public ArticlesCommandeFournisseur()
        {
        }

        /// <summary>
        /// Constructeur d'un ligne d'article pour la commande fournisseur
        /// </summary>
        /// <param name="reference">L'id de l'article</param>
        /// <param name="libelleArticle">Le nom de l'article</param>
        /// <param name="prixFournisseur">Le prix fournisseur de l'article</param>
        /// <param name="quantiteCommandeFournisseur">La quantité d'article commande</param>
        /// <param name="quantiteStock">La quantité d'article dans le stock</param>
        /// <param name="seuilStock">Le seuil limite de l'article dans le stock</param>
        public ArticlesCommandeFournisseur( int reference, 
                                            string libelleArticle, 
                                            double prixFournisseur, 
                                            int quantiteCommandeFournisseur,
                                            int quantiteStock,
                                            int seuilStock)
        {
            _reference = reference;
            _libelleArticle = libelleArticle;
            _prixFournisseur = prixFournisseur;
            _quantiteCommmandeFournisseur = quantiteCommandeFournisseur;
            _quantiteStock = quantiteStock;
            _seuilStock = seuilStock;
        }

        public int NumeroCommandeFournisseur
        {
            get
            {
                return _numeroCommandeFournisseur;
            }
            set
            {
                _numeroCommandeFournisseur = value;
            }
        }

        public int Reference
        {
            get
            {
                return _reference;
            }
            set
            {
                _reference = value;
            }
        }


        public string LibelleArticle
        {
            get
            {
                return _libelleArticle;
            }
            set
            {
                _libelleArticle = value;
            }
        }

        public double PrixFournisseur
        {
            get
            {
                return _prixFournisseur;
            }
            set
            {
                _prixFournisseur = value;
            }
        }

        public int QuantiteCommmandeFournisseur
        {
            get
            {
                return _quantiteCommmandeFournisseur;
            }
            set
            {
                _quantiteCommmandeFournisseur = value;
            }
        }

        public int QuantiteStock
        {
            get
            {
                return _quantiteStock;
            }

            set
            {
                _quantiteStock = value;
            }
        }

        public int SeuilStock
        {
            get
            {
                return _seuilStock;
            }
            set
            {
                _seuilStock = value;
            }
        }
    }
}

