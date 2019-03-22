using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [XmlInclude(typeof(Dossier))]
    [XmlInclude(typeof(Contact))]
    [Serializable]
    public class Dossier : Entite
    {
        [XmlElement("Profondeur")]
        public int Profondeur { get; set; }
        [XmlElement("Nom")]
        public string Nom { get; set; }
        [XmlElement("DateDeCreation")]
        public DateTime DateDeCreation { get; set; }
        [XmlElement("DateDeModification")]
        public DateTime DateDeModification { get; set; }
        [XmlArrayItem("ListeEntite", typeof(Entite))]
        public List<Entite> ListeEntite { get; set; }

        public Dossier() { }
        public Dossier(string nom, int profondeur = -1) : base()
        {
            Profondeur = profondeur;
            DateDeCreation = DateTime.Now;
            DateDeModification = DateDeCreation;
            Nom = nom;
            ListeEntite = new List<Entite>();
        }

        public void AjouterEntite(Entite entite)
        {
            ListeEntite.Add(entite);
            DateDeModification = DateTime.Now;
        }

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
