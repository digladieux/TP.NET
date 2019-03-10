using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EntitiesLayer;

namespace Serialisation
{
    public class SerialisationXML : ISerialisation
    {
        public Dossier Deserialise()
        {
            XmlSerializer Serialiser = new XmlSerializer(typeof(Dossier));
            TextReader Fichier = new StreamReader("Arborescence.dat");
            Dossier ListeDossier = (Dossier)Serialiser.Deserialize(Fichier);
            Fichier.Close();
            return ListeDossier;

        }

        public void Serialise(Dossier Arborescence)
        {
            XmlSerializer Serialiser = new XmlSerializer(typeof(Dossier));
            TextWriter Fichier = new StreamWriter("Arborescence.dat");
            Serialiser.Serialize(Fichier, Arborescence);
            Fichier.Close();
        }
    }
}
