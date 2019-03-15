using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [Serializable]
    [XmlInclude(typeof(Contact))]
    public class Contact : Entite
    {
        [XmlElement("Nom")]
        public string Nom { get; set; }
        [XmlElement("Prenom")]
        public string Prenom { get; set; }
        [XmlElement("Courriel")]
        public string Courriel { get; set; }
        [XmlElement("Societe")]
        public string Societe { get; set; }
        [XmlElement("Lien")]
        public Lien Lien { get; set; }
        [XmlElement("DateDeCreation")]
        public DateTime DateDeCreation { get; set; }
        [XmlElement("DateDeModification")]
        public DateTime DateDeModification { get; set; }

        public Contact() { }
        public Contact(string nom, string prenom, string courriel, string societe, Lien lien) : base()
        {
            Nom = nom;
            Prenom = prenom;
            Courriel = courriel;
            Societe = societe;
            Lien = lien;
            DateDeCreation = DateTime.Now;
            DateDeModification = DateDeCreation;
        }

        public static bool IsValidCourriel(string AdresseEmail)
        {
            bool validCourriel = true;
            try
            {
                new MailAddress(AdresseEmail);
            }
            catch (FormatException)
            {
                validCourriel = false;
            }
            return validCourriel;
        }

        public override string ToString()
        {
            return "[C] " + Prenom + ", " + Nom + "(" + Societe + "), Email:" + Courriel + ", Link:" + Lien + "\n";
        }
    }
}
