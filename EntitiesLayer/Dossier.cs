using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    /// <summary>
    /// Classe reprensentant un dossier. Elle herite de la classe Entite pour obtenir un Id unique entre toutes les entites.
    /// </summary>
    [XmlInclude(typeof(Dossier))]
    [XmlInclude(typeof(Contact))]
    [Serializable]
    public class Dossier : Entite
    {
        /// <summary>
        /// Indique la profondeur par rapport au dossier racine
        /// </summary>
        [XmlElement("Profondeur")]
        public int Profondeur { get; set; }

        /// <summary>
        /// Nom
        /// </summary>
        [XmlElement("Nom")]
        public string Nom { get; set; }

        /// <summary>
        /// Date de creation
        /// </summary>
        [XmlElement("DateDeCreation")]
        public DateTime DateDeCreation { get; set; }

        /// <summary>
        /// Date de derniere modification
        /// </summary>
        [XmlElement("DateDeModification")]
        public DateTime DateDeModification { get; set; }

        /// <summary>
        /// Liste des differentes entites
        /// </summary>
        [XmlArrayItem("ListeEntite", typeof(Entite))]
        public List<Entite> ListeEntite { get; set; }

        /// <summary>
        /// Constructeur par default de la classe Dossier
        /// </summary>
        public Dossier() { }

        /// <summary>
        /// Constructeur de la classe Dossier
        /// </summary>
        /// <param name="nom">Nom du nouveau dossier</param>
        /// <param name="profondeur">Profondeur par rapport au dossier racine</param>
        public Dossier(string nom, int profondeur = -1) : base()
        {
            Profondeur = profondeur;
            DateDeCreation = DateTime.Now;
            DateDeModification = DateDeCreation;
            Nom = nom;
            ListeEntite = new List<Entite>();
        }

        /// <summary>
        /// Permet l'ajout d'une entite dans le dossier
        /// </summary>
        /// <param name="entite">L'entite que l'on veut ajouter</param>
        public void AjouterEntite(Entite entite)
        {
            ListeEntite.Add(entite);
            DateDeModification = DateTime.Now;
        }

        /// <summary>
        /// Redefinission de la methode ToString pour un dossier
        /// </summary>
        /// <param name="ContactVisible">Indique si les contacts doivent etre afficher</param>
        /// <returns>La representation d'un dossier en chaine de caractere</returns>
        public string ToString(bool ContactVisible)
        {
            string Chaine = "[D : " + Id + "] " + Nom + " (creation " + DateDeModification + ")\n";
            foreach (Entite entite in ListeEntite)
            {
                int i = 0;
                while (i < Profondeur)
                {
                    Chaine += "  ";
                    i++;
                }
                if (entite is Dossier)
                {
                    Chaine += " | " + ((Dossier)entite).ToString(ContactVisible);
                }
                else if (ContactVisible)
                {
                    Chaine += " | " + entite.ToString();
                }
            }
            return Chaine;
        }

        /// <summary>
        /// Effectue une recherche recursive pour retrouver un dossier suivant son identifiant
        /// </summary>
        /// <param name="Identifiant">Identifiant du dossier rechercher</param>
        /// <returns>Un dossier null s'il n'existe pas, sinon le dossie</returns>
        public Dossier RechercherDossier(int Identifiant)
        {
            Dossier DossierRecherche = null;
            if ((Id == Identifiant) && (this is Dossier))
            {
                DossierRecherche = this;
            }
            else
            {
                foreach (Entite Dossier in ListeEntite)
                {
                    if (Dossier is Dossier)
                    {
                        DossierRecherche = ((Dossier)Dossier).RechercherDossier(Identifiant);
                        if (DossierRecherche != null)
                        {
                            break;
                        }
                    }

                }
            }
            return DossierRecherche;
        }
    }
}
