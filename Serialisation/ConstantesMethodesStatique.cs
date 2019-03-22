using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Statique
{

    public class Constantes
    {
        public static ISerialisation ChoixSerialisation = null;
        public static string MotDePasse = null;
        public static string CheminFichierChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName + "ArborescenceChiffree.dat";
        public static string CheminFichierNonChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName + "ArborescenceNonChiffree.dat";
    }

    public class MethodesStatiques
    {

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

                byte[] buffer = Encoding.ASCII.GetBytes(FichierNonChiffrer.ReadToEnd());
                cs.Write(buffer, 0, buffer.Length);

            }
            finally
            {
                /* Cs ferme aussi le fichier chiffrer */
                if (cs != null)
                {
                    cs.Close();
                }

                FichierNonChiffrer.Close();
                File.Delete(Constantes.CheminFichierNonChiffrer);

            }

        }

        public static void DechiffrementFichier(Rijndael Chiffrement)
        {
            CryptoStream cs = null;
            FileStream FichierChiffrer = null;
            try
            {
                FichierChiffrer = new FileStream(Constantes.CheminFichierChiffrer, FileMode.Open);
                //FileStream FichierChiffrer = new FileStream("toto.txt", FileMode.Open);
                FileStream FichierNonChiffrer = new FileStream(Constantes.CheminFichierNonChiffrer, FileMode.Create);
                ICryptoTransform aesDecryptor = Chiffrement.CreateDecryptor();

                cs = new CryptoStream(FichierNonChiffrer, aesDecryptor, CryptoStreamMode.Write);

                int data;

                while ((data = FichierChiffrer.ReadByte()) != -1)
                {
                    cs.WriteByte((byte)data);
                }
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
