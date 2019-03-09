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

        public void AddEntite(Entite entite)
        {
            ListeEntite.Add(entite);
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
