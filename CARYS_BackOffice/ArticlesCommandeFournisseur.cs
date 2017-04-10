using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CARYS_BackOffice
{
    /// <summary>
    /// Entitée représentant une ligne de commande pour un fournisseur
    /// </summary>
    public class ArticlesCommandeFournisseur
    {
        // Le numéro de la commmande fournisseur
        private int _numeroCommandeFournisseur;
        // Le nom de l'article
        private string _libelleArticle;
        // Le prix fournisseur de l'article
        private double _prixFournisseur;
        // Le quantité d'article commandé
        private int _quantiteCommmandeFournisseur;

        public ArticlesCommandeFournisseur()
        {
        }

        /// <summary>
        /// Constructeur d'un ligne d'article pour la commande fournisseur
        /// </summary>
        /// <param name="numeroCommandeFournisseur">Le numéro de la commande fournisseur</param>
        /// <param name="libelleArticle">Le nom de l'article</param>
        /// <param name="prixFournisseur">Le prix fournisseur de l'article</param>
        /// <param name="quantiteCommandeFournisseur">La quantité d'article commande</param>
        public ArticlesCommandeFournisseur(int numeroCommandeFournisseur, string libelleArticle, double prixFournisseur, int quantiteCommandeFournisseur)
        {
            _numeroCommandeFournisseur = numeroCommandeFournisseur;
            _libelleArticle = libelleArticle;
            _prixFournisseur = prixFournisseur;
            _quantiteCommmandeFournisseur = quantiteCommandeFournisseur;
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
    }
}

