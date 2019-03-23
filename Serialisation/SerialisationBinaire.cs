using EntitiesLayer;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

namespace Statique
{
    /// <summary>
    /// Classe pour la serialisation de l'arborescence de dossier en binaire
    /// </summary>
    public class SerialisationBinaire : ISerialisation
    {
        /// Constructeur par default de la classe SerialisationBinaire

        public SerialisationBinaire() { }

        /// <summary>
        /// Methode pour la deserialisation des elements binaire. Si la deserialisation ne marche pas, la liste de dossier n'est pas vide
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="ListeDossier">Arborescence ou le resultat de la deserialisation se trouvera</param>
        public void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            try
            {
                //MethodesStatiques.DechiffrementFichier(Chiffrement);
                Dossier ListeDossierTemporaire = null;
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream FichierNonChiffrer = new FileStream(Constantes.CheminFichierNonChiffrer, FileMode.Open);
                ListeDossierTemporaire = (Dossier)formatter.Deserialize(FichierNonChiffrer);
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

        /// <summary>
        /// Methode pour la serialisation des elements en binaire.
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="Arborescence">Arborescence que l'on veut chiffrer</param>
        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {
            FileStream FichierNonChiffrer = new FileStream(Constantes.CheminFichierNonChiffrer, FileMode.Create);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(FichierNonChiffrer, Arborescence);
            FichierNonChiffrer.Close();
            //MethodesStatiques.ChiffrementFichier(Chiffrement);
        }
    }
}
