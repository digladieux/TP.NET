using EntitiesLayer;
using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Statique
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

        public void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            if (MethodesStatiques.DechiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer))
            {
                Dossier ListeDossierTemporaire = null ;
                BinaryFormatter formatter = new BinaryFormatter();

                try
                {
                    FileStream FichierNonChiffrer= new FileStream(CheminFichierNonChiffrer, FileMode.Open);
                    ListeDossierTemporaire = (Dossier) formatter.Deserialize(FichierNonChiffrer);
                    FichierNonChiffrer.Close();
                    if (ListeDossierTemporaire != null)
                    {
                        ListeDossier = ListeDossierTemporaire;
                    }
                    File.Delete(CheminFichierNonChiffrer);
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Vous gardez votre Liste de Dossier\n");
                }

            }
        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
            FileStream FichierNonChiffrer= new FileStream(CheminFichierNonChiffrer, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(FichierNonChiffrer, Arborescence);
            FichierNonChiffrer.Close();
            MethodesStatiques.ChiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer);
        }
    }
}
