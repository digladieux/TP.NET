using System;

namespace EntitiesLayer
{
    [Flags]
    public enum Lien
    {
        Ami,
        Collegue,
        Relation,
        Reseau
    }

    public class LienMethodeStatique
    {

        public static Lien IntToLien(int ChoixLien)
        {
            Lien Lien;

            switch (ChoixLien)
            {
                case 1:
                    Lien = Lien.Ami;
                    break;
                case 2:
                    Lien = Lien.Collegue;
                    break;
                case 3:
                    Lien = Lien.Relation;
                    break;
                case 4:
                    Lien = Lien.Reseau;
                    break;
                default:
                    throw new InvalidCastException("Choix Invalide");
            }

            return Lien;
        }
    }

}
