using System;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    /// <summary>
    /// Classe representant toutes les entites de notre arborescence
    /// </summary>
    [XmlInclude(typeof(Entite))]
    [Serializable]
    public class Entite
    {
        /// <summary>
        /// Nombre d'entite genere au total
        /// </summary>
        public static int Compteur { get; set; } = 0;

        /// <summary>
        /// Identifiant unique 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Constructeur par default de la classe Entite
        /// </summary>
        public Entite()
        {
            Id = Compteur;
            Compteur++;
        }

    }
}
