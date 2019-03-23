using EntitiesLayer;
using System.Security.Cryptography;

namespace Statique
{
    /// <summary>
    /// Interface pour l'împlementation du design pattern Factory pour le stockage des instances dans des fichiers
    /// </summary>
    public interface ISerialisation
    {
        /// <summary>
        /// Methode pour la deserialisation des elements. Si la deserialisation ne marche pas, la liste de dossier n'est pas vide
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="ListeDossier">Arborescence ou le resultat de la deserialisation se trouvera</param>
        void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier);

        /// <summary>
        /// Methode pour la serialisation des elements.
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="Arborescence">Arborescence que l'on veut chiffrer</param>
        void Serialise(Rijndael Chiffrement, Dossier Arborescence);
    }


}
