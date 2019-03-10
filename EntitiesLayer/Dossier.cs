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

        public override string ToString()
        {
            string Chaine = "[D] " + Nom + " (creation " + DateDeModification + ")\n";
            foreach(Entite entite in ListeEntite)
            {
                Chaine += entite + "\n";

            }
            return Chaine ;
        }
    }
}
