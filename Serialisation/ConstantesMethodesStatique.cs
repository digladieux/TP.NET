using EntitiesLayer;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace Statique
{

    public class Constantes
    {
        public static ISerialisation ChoixSerialisation = null ;
        public static string MotDePasse = null;
    }

    public class MethodesStatiques
    {
    
        public static void ChiffrementFichier(Rijndael Chiffrement, string CheminFichierChiffrer, string CheminFichierNonChiffrer)
        {
            ChoixCleChiffrement(Chiffrement);
            TextReader FichierNonChiffrer = new StreamReader(CheminFichierNonChiffrer);
            FileStream FichierChiffre = new FileStream(CheminFichierChiffrer, FileMode.Create);
            ICryptoTransform aesEncryptor = Chiffrement.CreateEncryptor();
            CryptoStream cs = new CryptoStream(FichierChiffre, aesEncryptor, CryptoStreamMode.Write);

            byte[] buffer = Encoding.ASCII.GetBytes(FichierNonChiffrer.ReadToEnd());
            cs.Write(buffer, 0, buffer.Length);
            cs.Close();

            FichierNonChiffrer.Close();
            File.Delete(CheminFichierNonChiffrer);
            FichierChiffre.Close();
            ChoixMotDePasse();
        }

        public static bool DechiffrementFichier(Rijndael Chiffrement, string CheminFichierChiffrer, string CheminFichierNonChiffrer)
        {
            bool IsMotDePasseValide = MotDePasseValide();
            if (IsMotDePasseValide)
            {
                try
                {
                    FileStream FichierChiffrer = new FileStream(CheminFichierChiffrer, FileMode.Open);
                    //FileStream FichierChiffrer = new FileStream("toto.txt", FileMode.Open);
                    FileStream FichierNonChiffrer = new FileStream(CheminFichierNonChiffrer, FileMode.Create);
                    ICryptoTransform aesDecryptor = Chiffrement.CreateDecryptor();

                    CryptoStream cs = new CryptoStream(FichierNonChiffrer, aesDecryptor, CryptoStreamMode.Write);

                    int data;

                    while ((data = FichierChiffrer.ReadByte()) != -1)
                        cs.WriteByte((byte)data);

                    cs.Close();
                    FichierNonChiffrer.Close();
                    FichierChiffrer.Close();
                }
                catch(FileNotFoundException)
                {
                    Console.WriteLine("\nLe fichier n'existe pas");
                }
            }
            else
            {
                Constantes.ChoixSerialisation = null;
                Console.WriteLine("Mot de passe Invalide 3 tentatives .. Suppression de la Base de donnée\n");
                File.Delete(CheminFichierChiffrer);

            }
            return IsMotDePasseValide;
        }

        private static void ChoixMotDePasse()
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

        private static void ChoixCleChiffrement(Rijndael Chifffrement)
        {
            Console.WriteLine("Entrez la clé de chiffrement");
            Console.WriteLine("Ne tapez rien pour une clé par défault");
            Console.WriteLine("Sinon, taper une chaine de plus de 8 caracteres");
            string CleUtilisateur;
            do
            {
                CleUtilisateur = Console.ReadLine();

            } while((CleUtilisateur != "") && (CleUtilisateur.Length < 8)) ;

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

        public static Dossier RechercheDossier(Dossier ListeDossier)
        {
            Dossier DossierParent = null;
            while (DossierParent == null)
            {
                Console.WriteLine(ListeDossier.ToString(false));
                int choix = ChoixUtilisateurValide();
                DossierParent = ListeDossier.RechercherDossier(choix);
            }
            return DossierParent;
        }


        public static int ChoixUtilisateurValide()
        {
            string ChaineChoixUtilisateur;
            int EntierChoixUtilisateur = -1;
            bool IsChoixValide = false;
            while (!IsChoixValide)
            {
                try
                {
                    ChaineChoixUtilisateur = Console.ReadLine();
                    EntierChoixUtilisateur = int.Parse(ChaineChoixUtilisateur);
                    IsChoixValide = true;
                    Console.WriteLine();

                }
                catch (Exception)
                {
                    Console.WriteLine("Chaine Invalide\n");
                }
            }
            return EntierChoixUtilisateur;
        }
    }
}
