using EntitiesLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serialisation
{
    public interface ISerialisation
    {
        Dossier Deserialise();
        void Serialise(Dossier Arborescence);
    }
}
