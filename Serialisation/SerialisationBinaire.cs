using EntitiesLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public class SerialisationBinaire : ISerialisation
    {
        private readonly string CheminFichierChiffrer ;
        private readonly string CheminFichierNonChiffrer;

        public SerialisationBinaire()
        {
            CheminFichierChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "ArborescenceChiffree.dat";
            CheminFichierNonChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "ArborescenceNonChiffree.dat";
        }

        public Dossier Deserialise(Rijndael Chiffrement)
        {

          /*  string Password;
            do
            {
                Console.WriteLine("Entrer un mdp");
                Password = Console.ReadLine();
            } while (!Password.Equals(MotDePasse));

    */

            Dossier ListeDossier = null;

            BinaryFormatter formatter = new BinaryFormatter();

            /*FileStream FichierNonChiffrer = new FileStream(CheminFichierNonChiffrer, FileMode.OpenOrCreate);
            FileStream FichierChiffrer = new FileStream(CheminFichierChiffrer, FileMode.OpenOrCreate);

            ICryptoTransform aesDecryptor = Chiffrement.CreateDecryptor();

            CryptoStream cs = new CryptoStream(FichierNonChiffrer, aesDecryptor, CryptoStreamMode.Write);

            int data;

            while ((data = FichierChiffrer.ReadByte()) != -1)
                cs.WriteByte((byte)data);

            cs.Close();
            FichierNonChiffrer.Close();
            FichierChiffrer.Close();
            */
            FileStream FichierNonChiffrer = new FileStream(CheminFichierNonChiffrer, FileMode.Open);
            try
            {
                ListeDossier = (Dossier) formatter.Deserialize(FichierNonChiffrer);
            }catch (SerializationException e)
            {
                Console.WriteLine("Echec de la deserialisation : " + e.Message);
            }
            finally
            {
            }
            FichierNonChiffrer.Close();
            //File.Delete(CheminFichierNonChiffrer);

            return ListeDossier;
        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
            FileStream FichierNonChiffrer = new FileStream(CheminFichierNonChiffrer, FileMode.Create);

         /*   Console.WriteLine("Entrer un mdp");
            MotDePasse = Console.ReadLine();*/
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            try
            {
                binaryFormatter.Serialize(FichierNonChiffrer, Arborescence);
            }
            catch(SerializationException e)
            {
                Console.WriteLine("Echec de la serialisation : " + e.Message);
            }
            
         /*   FileStream FichierChiffre = new FileStream(CheminFichierChiffrer, FileMode.Create);
            ICryptoTransform aesEncryptor = Chiffrement.CreateEncryptor();
            CryptoStream cs = new CryptoStream(FichierChiffre, aesEncryptor, CryptoStreamMode.Write);

            int data;

            while ((data = FichierNonChiffrer.ReadByte()) != -1)
            {
                cs.WriteByte((byte)data);
            }
            cs.Close();
            */
            FichierNonChiffrer.Close();
           /* File.Delete(CheminFichierNonChiffrer);
            FichierChiffre.Close();*/

       
        }
    }
}
