using EntitiesLayer;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml.Serialization;

namespace Statique
{
    /// <summary>
    /// Classe pour la serialisation de l'arborescence de dossier en XML
    /// </summary>
    public class SerialisationXML : ISerialisation
    {
        /// <summary>
        /// Constructeur par default de la classe SerialisationXML
        /// </summary>
        public SerialisationXML() { }

        /// <summary>
        /// Methode pour la deserialisation des elements en XML. Si la deserialisation ne marche pas, la liste de dossier n'est pas vide
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="ListeDossier">Arborescence ou le resultat de la deserialisation se trouvera</param>
        public void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            try
            {
                CryptographieFichier.DechiffrementFichier(Chiffrement);

                Dossier ListeDossierTemporaire = null;
                XmlSerializer Serialiser = new XmlSerializer(typeof(Dossier));

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

        /// <summary>
        /// Methode pour la serialisation des elements en XML.
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="Arborescence">Arborescence que l'on veut chiffrer</param>
        public void Serialise(Rijndael Chiffrement, Dossier Arborescence)
        {

            XmlSerializer Serialiser = new XmlSerializer(Arborescence.GetType(), new Type[] { typeof(Dossier) });
            TextWriter Fichier = new StreamWriter(Constantes.CheminFichierNonChiffrer);

            Serialiser.Serialize(Fichier, Arborescence);
            Fichier.Close();
            CryptographieFichier.ChiffrementFichier(Chiffrement);
        }
    }
}
