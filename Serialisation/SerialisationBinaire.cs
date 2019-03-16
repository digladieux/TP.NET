using EntitiesLayer;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Serialisation
{
    public class SerialisationBinaire : ISerialisation
    {
        private readonly string CheminFichierChiffrer ;
        private readonly string CheminFichierNonChiffrer;

        public SerialisationBinaire()
        {
            CheminFichierChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName +"ArborescenceChiffree.dat";
            CheminFichierNonChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName + "ArborescenceNonChiffree.dat";
        }

        public Dossier Deserialise(Rijndael Chiffrement)
        {



            MethodesStatique.DechiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer);

            Dossier ListeDossier = null;
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream FichierNonChiffrer= new FileStream(CheminFichierNonChiffrer, FileMode.Open);
            try
            {
                ListeDossier = (Dossier) formatter.Deserialize(FichierNonChiffrer);
            }catch (SerializationException e)
            {
                Console.WriteLine("Echec de la deserialisation : " + e.Message);
            }
            finally
            {
                FichierNonChiffrer.Close();
            }

            File.Delete(CheminFichierNonChiffrer);

            return ListeDossier;
        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
            FileStream FichierNonChiffrer= new FileStream(CheminFichierNonChiffrer, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            try
            {
                binaryFormatter.Serialize(FichierNonChiffrer, Arborescence);
            }
            catch(SerializationException e)
            {
                Console.WriteLine("Echec de la serialisation : " + e.Message);
            }
            FichierNonChiffrer.Close();
            MethodesStatique.ChiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer);



        }
    }
}
