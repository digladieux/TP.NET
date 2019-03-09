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
    public class SerialisationBinaire
    {
        public static Dossier Deserialize()
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
                throw;
            }
            finally
            {
                Fichier.Close();
            }

            return ListeDossier;
        }

        public static void Serialize(Dossier Arborescence)
        {
            FileStream Fichier = new FileStream("Arborescence.dat", FileMode.OpenOrCreate);
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
