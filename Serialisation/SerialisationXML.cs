using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using EntitiesLayer;

namespace Statique
{
    public class SerialisationXML : ISerialisation
    {
        
        public SerialisationXML(){}

        public void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            MethodesStatiques.DechiffrementFichier(Chiffrement);

            Dossier ListeDossierTemporaire = null;
                    XmlSerializer Serialiser = new XmlSerializer(typeof(Dossier));

                    try
                    {
                        TextReader Fichier = new StreamReader(Constantes.CheminFichierNonChiffrer);
                        ListeDossierTemporaire = (Dossier)Serialiser.Deserialize(Fichier);
                        Fichier.Close();
                        File.Delete(Constantes.CheminFichierNonChiffrer);
                        if (ListeDossierTemporaire != null)
                        {
                            ListeDossier = ListeDossierTemporaire;
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        throw e;
                    }
        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
          
            XmlSerializer Serialiser = new XmlSerializer(Arborescence.GetType(), new Type[] { typeof(Dossier) });
            TextWriter Fichier = new StreamWriter(Constantes.CheminFichierNonChiffrer);
        
            Serialiser.Serialize(Fichier, Arborescence);
            Fichier.Close();
            MethodesStatiques.ChiffrementFichier(Chiffrement);
        }
    }
}
