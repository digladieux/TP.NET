using EntitiesLayer;
using System.Security.Cryptography;

namespace Serialisation
{
    public interface ISerialisation
    {
        void Deserialise(Rijndael Chiffrement, ref Dossier ListeDossier);
        void Serialise(Rijndael Chiffrement, Dossier Arborescence);
    }


}
