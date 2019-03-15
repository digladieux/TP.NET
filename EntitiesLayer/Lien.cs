using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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

    public class MethodeStatique
    {
        public static string MotDePasse = null;

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
                FileStream FichierNonChiffrer = new FileStream(CheminFichierNonChiffrer, FileMode.OpenOrCreate);
                FileStream FichierChiffrer = new FileStream(CheminFichierChiffrer, FileMode.OpenOrCreate);

                ICryptoTransform aesDecryptor = Chiffrement.CreateDecryptor();

                CryptoStream cs = new CryptoStream(FichierNonChiffrer, aesDecryptor, CryptoStreamMode.Write);

                int data;

                while ((data = FichierChiffrer.ReadByte()) != -1)
                    cs.WriteByte((byte)data);

                cs.Close();
                FichierNonChiffrer.Close();
                FichierChiffrer.Close();
            }
            else
            {
                Console.WriteLine("Mot de passe Invalide 3 tentatives .. Suppression Base de donnée");
                File.Delete(CheminFichierChiffrer);

            }
            return IsMotDePasseValide;
        }  

        private static void ChoixMotDePasse()
        {
           Console.WriteLine("Entrer un mdp");
           MotDePasse = Console.ReadLine();
        }

        public static bool MotDePasseValide()
        {
            int compteur = 0;
            bool IsMotDePasseValide = true;
            string Password;
            do
            {
                Console.WriteLine("Entrer un mdp");
                compteur++;
                Password = Console.ReadLine();
            } while ((!Password.Equals(MotDePasse)) && (compteur < 3 ) );
            if (compteur == 3)
            {
                IsMotDePasseValide = false;
            }

            return IsMotDePasseValide;
        }
    }

}
