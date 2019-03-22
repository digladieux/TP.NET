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
        public SerialisationBinaire(){}

        public void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            MethodesStatiques.DechiffrementFichier(Chiffrement);
                    Dossier ListeDossierTemporaire = null ;
                    BinaryFormatter formatter = new BinaryFormatter();

                    try
                    {
                        FileStream FichierNonChiffrer= new FileStream(Constantes.CheminFichierNonChiffrer, FileMode.Open);
                        ListeDossierTemporaire = (Dossier) formatter.Deserialize(FichierNonChiffrer);
                        FichierNonChiffrer.Close();
                        if (ListeDossierTemporaire != null)
                        {
                            ListeDossier = ListeDossierTemporaire;
                        }
                        File.Delete(Constantes.CheminFichierNonChiffrer);
                    }
                    catch (FileNotFoundException e)
                    {
                        throw e;
                    }
        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
            FileStream FichierNonChiffrer= new FileStream(Constantes.CheminFichierNonChiffrer, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(FichierNonChiffrer, Arborescence);
            FichierNonChiffrer.Close();
            MethodesStatiques.ChiffrementFichier(Chiffrement);
        }
    }
}
