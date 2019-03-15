using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
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

        public Dossier Deserialise(Rijndael Chiffrement)
        {
            Dossier ListeDossier = null;
            if (MethodeStatique.DechiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer))
            {
                XmlSerializer Serialiser = new XmlSerializer(typeof(Dossier));
                TextReader Fichier = new StreamReader(CheminFichierNonChiffrer);
                ListeDossier = (Dossier)Serialiser.Deserialize(Fichier);
                Fichier.Close();
                File.Delete(CheminFichierNonChiffrer);

            }
            return ListeDossier;


        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
          
            XmlSerializer Serialiser = new XmlSerializer(Arborescence.GetType(), new Type[] { typeof(Dossier) });
            TextWriter Fichier = new StreamWriter(CheminFichierNonChiffrer);
        
            Serialiser.Serialize(Fichier, Arborescence);
            Fichier.Close();
            MethodeStatique.ChiffrementFichier(Chiffrement, CheminFichierChiffrer, CheminFichierNonChiffrer);
        }
    }
}
