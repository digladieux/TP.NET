using System;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [XmlInclude(typeof(Entite))]
    [Serializable]
    public class Entite
    {
        public static int Compteur { get; set; } = 0;
        public int Id { get; set; }

        public Entite()
        {
            Id = Compteur;
            Compteur++;
        }

    }
}
