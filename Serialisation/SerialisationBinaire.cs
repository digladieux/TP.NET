using EntitiesLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public class SerialisationBinaire : ISerialisation
    {
        public Dossier Deserialise()
        {
            Dossier ListeDossier = null;
            FileStream Fichier = new FileStream("Arborescence.dat", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();

            try
            {
                ListeDossier = (Dossier) formatter.Deserialize(Fichier);
            }catch (SerializationException e)
            {
                Console.WriteLine("Echec de la deserialisation : " + e.Message);
            }
            finally
            {
                Fichier.Close();
            }

            return ListeDossier;
        }

        public void Serialise(Dossier Arborescence)
        {
            FileStream Fichier = new FileStream("Arborescence.dat", FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            try
            {
                binaryFormatter.Serialize(Fichier, Arborescence);
            }
            catch(SerializationException e)
            {
                Console.WriteLine("Echec de la serialisation : " + e.Message);
            }
            finally
            {
                Fichier.Close();
            }
        }
    }
}
