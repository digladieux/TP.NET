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
            CheminFichierChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "ArborescenceChiffree.dat";
            CheminFichierNonChiffrer = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "ArborescenceNonChiffree.dat";
        }

        public Dossier Deserialise(Rijndael Chiffrement)
        {
            XmlSerializer Serialiser = new XmlSerializer(typeof(Dossier));
            TextReader Fichier = new StreamReader(CheminFichierNonChiffrer);
            Dossier ListeDossier = (Dossier)Serialiser.Deserialize(Fichier);
            Fichier.Close();
            return ListeDossier;

        }

        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
          
            XmlSerializer Serialiser = new XmlSerializer(Arborescence.GetType(), new Type[] { typeof(Dossier) });
            TextWriter Fichier = new StreamWriter(CheminFichierNonChiffrer);
        
            Serialiser.Serialize(Fichier, Arborescence);
            Fichier.Close();
        }
    }
}
