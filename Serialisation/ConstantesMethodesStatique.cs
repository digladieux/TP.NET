using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Serialisation
{

    public class Constantes
    {
        public static ISerialisation ChoixSerialisation = null ;
        public static string MotDePasse = null;
    }

    public class MethodesStatique
    {
    
        public static void ChiffrementFichier(Rijndael Chiffrement, string CheminFichierChiffrer, string CheminFichierNonChiffrer)
        {
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
                    //FileStream FichierChiffrer = new FileStream(CheminFichierChiffrer, FileMode.Open);
                    FileStream FichierChiffrer = new FileStream("toto.txt", FileMode.Open);
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
    }
}
