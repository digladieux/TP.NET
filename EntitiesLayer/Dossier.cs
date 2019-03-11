using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Dossier : Entite
    {
        
        public int Profondeur { get; set; }
        public string Nom { get; set; }
        public DateTime DateDeCreation { get; set; }
        public DateTime DateDeModification { get; set; }
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
            string  Chaine = "[D : " + Id + "] " + Nom + " (creation " + DateDeModification + ")\n";
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
            return Chaine ;
        }

        public Dossier RechercherDossier(int Identifiant)
        {
            Dossier DossierRecherche = null;
            if ( (Id == Identifiant) && (this is Dossier))
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
