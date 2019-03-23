using System;

namespace EntitiesLayer
{
    /// <summary>
    /// Enumeration de tous les liens possibles entre un utilisateur et son contact
    /// </summary>
    [Flags]
    public enum Lien
    {
        /// <summary>
        /// Les 2 personnes sont amis
        /// </summary>
        Ami,
        /// <summary>
        /// Les 2 personnes sont collegues
        /// </summary>
        Collegue,
        /// <summary>
        /// Les 2 personnes sont des connaissances
        /// </summary>
        Relation,
        /// <summary>
        /// Les 2 personnes font parti d'un meme reseau
        /// </summary>
        Reseau
    }

    /// <summary>
    /// Classe pour Parser un entier en Lien
    /// </summary>
    public class LienMethodeStatique
    {
        /// <summary>
        /// Convertir un entier en Lien. Renvoie une erreur de type InvalidCastException si le lien n'existe pas
        /// </summary>
        /// <param name="ChoixLien">L'entier que l'on veut convertir</param>
        /// <returns>Le nouveau lien</returns>
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
                    throw new InvalidCastException("Relation Invalide");
            }

            return Lien;
        }
    }

}
