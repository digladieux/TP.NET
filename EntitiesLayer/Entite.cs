using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Entite
    {
        public static int Compteur { get; set; } = 0;
        public int Id { get; set; }

        public Entite()
        {
            Id = Compteur;
            Compteur++;
        }

    }
}
