using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    class Contact : Entite
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        public string Societe { get; set; }
        public Lien Lien { get; set; }
        public DateTime DateDeCreation { get; set; }
        public DateTime DateDeModification { get; set; }

        public override string ToString()
        {
            return "[C] " + Prenom + ", " + Nom + "(" + Societe + "), Email:" + Courriel + ", Link:" + Lien;
        }
    }
}
