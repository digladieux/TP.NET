using System;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    [XmlInclude(typeof(Contact))]
    [Serializable]
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
            Regex myRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.IgnoreCase);
            return myRegex.IsMatch(AdresseEmail);
        }

        public override string ToString()
        {
            return "[C] " + Prenom + ", " + Nom + "(" + Societe + "), Email:" + Courriel + ", Link:" + Lien + "\n";
        }
    }
}
