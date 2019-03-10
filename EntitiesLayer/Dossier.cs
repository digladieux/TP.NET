using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Dossier : Entite
    {
        

        public string Nom { get; set; }
        public DateTime DateDeCreation { get; set; }
        public DateTime DateDeModification { get; set; }
        public List<Entite> ListeEntite { get; set; }

        public Dossier() { }
        public Dossier(string nom) : base()
        {
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
            foreach(Entite entite in ListeEntite)
            {
                if ( (entite is Dossier) || (ContactVisible) ) 
                {
                    Chaine += entite + "\n";
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
            foreach(Dossier Dossier in ListeEntite)
            {
                RechercherDossier(Identifiant);
            }
            return DossierRecherche;
        }
    }
}
