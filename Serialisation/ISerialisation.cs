using EntitiesLayer;
using System.Security.Cryptography;

namespace Serialisation
{
    public interface ISerialisation
    {
        Dossier Deserialise(Rijndael Chiffrement);
        void Serialise(Rijndael Chiffrement, Dossier Arborescence);
    }


}
