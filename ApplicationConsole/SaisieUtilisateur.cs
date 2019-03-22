using Statique;
using System;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace ApplicationConsole
{
    class SaisieUtilisateur
    {
        public static void ChoixMotDePasse()
        {
            Console.WriteLine("Entrez le mot de passe qui servira au déchiffrement");
            Constantes.MotDePasse = Console.ReadLine();
        }

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
            if (compteur == 3)
            {
                Console.WriteLine();
                IsMotDePasseValide = false;
            }

            return IsMotDePasseValide;
        }

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
