using Serialisation;
using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace ApplicationConsole
{
    /// <summary>
    /// Methode autour de la saisit clavier de l'utilisateur
    /// </summary>
    class SaisieUtilisateur
    {
        /// <summary>
        /// Permet de choisir un mot de mot pour un fichier
        /// </summary>
        public static void ChoixMotDePasse()
        {
            Console.WriteLine("Entrez le mot de passe qui servira au déchiffrement");
            Constantes.MotDePasse = Console.ReadLine();
        }

        /// <summary>
        /// Verifie si le mot de passe du fichier est le meme que celui que l'utilisateur ecrit
        /// </summary>
        /// <returns>True si le mot de passe a ete trouve en moins de 3 tentatives, faux sinon</returns>
        public static bool MotDePasseValide()
        {
            int compteur = 0;
            bool IsMotDePasseValide = true;
            string Password;
            do
            {
                Console.WriteLine("Entrer le mot de passe du fichier chiffrer");
                compteur++;
                Password = Console.ReadLine();
            } while ((!Password.Equals(Constantes.MotDePasse)) && (compteur < 3));

            // Si cela fait de plus de 3 fois, on supprimera la bdd
            if (compteur == 3)
            {
                Console.WriteLine();
                IsMotDePasseValide = false;
            }

            return IsMotDePasseValide;
        }

        /// <summary>
        /// Permet de definir la cle de chiffement du fichier
        /// </summary>
        /// <param name="Chifffrement">Chiffrement du fichier</param>
        public static void ChoixCleChiffrement(ref Rijndael Chifffrement)
        {
            Console.WriteLine("Entrez la clé de chiffrement");
            Console.WriteLine("Ne tapez rien pour une clé par défault");
            Console.WriteLine("Sinon, taper une chaine de plus de 8 caracteres");
            string CleUtilisateur;
            do
            {
                CleUtilisateur = Console.ReadLine();

            } while ((CleUtilisateur != "") && (CleUtilisateur.Length < 8));

            Rfc2898DeriveBytes rfcDb;

            // Si l'utilisateur ne rentre rien, on prendra le SID
            if (CleUtilisateur != "")
            {
                rfcDb = new Rfc2898DeriveBytes(CleUtilisateur, Encoding.UTF8.GetBytes(CleUtilisateur));

            }
            else
            {
                rfcDb = new Rfc2898DeriveBytes(WindowsIdentity.GetCurrent().User.ToString(), Encoding.UTF8.GetBytes(WindowsIdentity.GetCurrent().User.ToString()));
            }

            Chifffrement.Key = rfcDb.GetBytes(16);
            Chifffrement.IV = rfcDb.GetBytes(16);
        }

        /// <summary>
        /// Verifie si l'utisateur rentre un entier dans le terminal
        /// </summary>
        /// <returns>L'entier de l'utilisateur</returns>
        public static int ChoixUtilisateurValide()
        {
            string ChaineChoixUtilisateur;
            int EntierChoixUtilisateur = -1;
            try
            {
                ChaineChoixUtilisateur = Console.ReadLine();
                EntierChoixUtilisateur = int.Parse(ChaineChoixUtilisateur);
                Console.WriteLine();
            }
            catch (FormatException e)
            {
                throw e;
            }
            return EntierChoixUtilisateur;
        }
    }
}
