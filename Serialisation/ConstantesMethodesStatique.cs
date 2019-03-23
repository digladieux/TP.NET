using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Statique
{
    /// <summary>
    /// Classe contenant toutes les constantes
    /// </summary>
    public class Constantes
    {
        /// <summary>
        /// Attribut pour connaitre la methode serialisation adopter par l'utilisateur
        /// </summary>
        public static ISerialisation ChoixSerialisation = null;

        /// <summary>
        /// Mot de passe du fichier chiffrer
        /// </summary>
        public static string MotDePasse = null;

        /// <summary>
        /// Chemin pour trouver le fichier de donner serialiser chiffrer
        /// </summary>
        public static string CheminFichierChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName + "ArborescenceChiffree.dat";


        /// <summary>
        /// Chemin pour trouver le fichier de donner serialiser non chiffrer
        /// </summary>
        public static string CheminFichierNonChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName + "ArborescenceNonChiffree.dat";
    }

    /// <summary>
    /// Classe pour l'implementation du chiffrage et dechiffrage de fichier
    /// </summary>
    public class CryptographieFichier
    {
        /// <summary>
        /// Permet le chiffrement d'un fichier
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        public static void ChiffrementFichier(Rijndael Chiffrement)
        {
            TextReader FichierNonChiffrer = null;
            CryptoStream cs = null;

            try
            {
                FichierNonChiffrer = new StreamReader(Constantes.CheminFichierNonChiffrer);
                FileStream FichierChiffre = new FileStream(Constantes.CheminFichierChiffrer, FileMode.Create);

                ICryptoTransform aesEncryptor = Chiffrement.CreateEncryptor();
                cs = new CryptoStream(FichierChiffre, aesEncryptor, CryptoStreamMode.Write);

                // On recuperer le contenu du fichier que l'on transforme en tableau de byte
                byte[] buffer = Encoding.ASCII.GetBytes(FichierNonChiffrer.ReadToEnd());
                cs.Write(buffer, 0, buffer.Length);

            }
            finally
            {
                // Cs ferme aussi le fichier chiffrer 
                if (cs != null)
                {
                    cs.Close();
                }

                FichierNonChiffrer.Close();
                File.Delete(Constantes.CheminFichierNonChiffrer);

            }

        }

        /// <summary>
        /// Methode pour le dechiffrement d'un fichier
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        public static void DechiffrementFichier(Rijndael Chiffrement)
        {
            CryptoStream cs = null;
            FileStream FichierChiffrer = null;
            try
            {
                FileStream FichierNonChiffrer = new FileStream(Constantes.CheminFichierNonChiffrer, FileMode.Create);
                
                // A decommenter pour observer l'erreur que cela generer
                //FichierChiffrer = new FileStream("toto.txt", FileMode.Open);
                FichierChiffrer = new FileStream(Constantes.CheminFichierChiffrer, FileMode.Open);

                ICryptoTransform aesDecryptor = Chiffrement.CreateDecryptor();

                cs = new CryptoStream(FichierNonChiffrer, aesDecryptor, CryptoStreamMode.Write);

                int data;

                while ((data = FichierChiffrer.ReadByte()) != -1)
                {
                    cs.WriteByte((byte)data);
                }
            }
            catch(FileNotFoundException e)
            {
                throw e;
            }
            finally
            {
                /* Cs ferme le fichier non chiffrer */
                if (cs != null)
                {
                    cs.Close();
                }
                if (FichierChiffrer != null)
                {
                    FichierChiffrer.Close();
                }
            }
        }

    }
}
