using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;
using EntitiesLayer;

namespace Serialisation
{
    public class SerialisationXML : ISerialisation
    {

        private readonly string CheminFichierChiffrer;
        private readonly string CheminFichierNonChiffrer;

        public SerialisationXML()
        {
           
            CheminFichierChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName + "ArborescenceChiffree.dat";
            CheminFichierNonChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Environment.UserName + "ArborescenceNonChiffree.dat";
        }

        public void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            
            if (MethodesStatique.DechiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer))
            {
                Dossier ListeDossierTemporaire = null;
                XmlSerializer Serialiser = new XmlSerializer(typeof(Dossier));

                try
                {
                    TextReader Fichier = new StreamReader(CheminFichierNonChiffrer);
                    ListeDossierTemporaire = (Dossier)Serialiser.Deserialize(Fichier);
                    Fichier.Close();
                    File.Delete(CheminFichierNonChiffrer);
                    if (ListeDossierTemporaire != null)
                    {
                        ListeDossier = ListeDossierTemporaire;
                    }
                }
                catch(FileNotFoundException)
                {
                    Console.WriteLine("Vous gardez votre Liste de Dossier\n");
                }
            }
        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
          
            XmlSerializer Serialiser = new XmlSerializer(Arborescence.GetType(), new Type[] { typeof(Dossier) });
            TextWriter Fichier = new StreamWriter(CheminFichierNonChiffrer);
        
            Serialiser.Serialize(Fichier, Arborescence);
            Fichier.Close();
            MethodesStatique.ChiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer);
        }
    }
}
