using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public enum Lien
    {
        Ami,
        Collegue,
        Relation,
        Reseau
    }

    public class MethodeStatique
    {
        public static Lien IntToLien(int ChoixLien)
        {
            Lien Lien;

            switch (ChoixLien)
            {
                case 0:
                    Lien = Lien.Ami;
                    break;
                case 1:
                    Lien = Lien.Collegue;
                    break;
                case 2:
                    Lien = Lien.Relation;
                    break;
                case 3:
                    Lien = Lien.Reseau;
                    break;
                default:
                    throw new InvalidCastException("Choix Invalide");
            }

            return Lien;
        }
    }
    
}
