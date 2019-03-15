using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public interface ISerialisation
    {
        Dossier Deserialise(Rijndael Chiffrement);
        void Serialise(Rijndael Chiffrement, Dossier Arborescence);
    }
}
