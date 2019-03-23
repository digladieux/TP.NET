using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace EntitiesLayer
{
    /// <summary>
    /// Classe represantant un contact. Elle herite de la classe Entite pour obtenir un Id unique entre toutes les entites.
    /// </summary>
    [XmlInclude(typeof(Contact))]
    [Serializable]
    public class Contact : Entite
    {
        /// <summary>
        /// Nom 
        /// </summary>
        [XmlElement("Nom")]
        public string Nom { get; set; }

        /// <summary>
        /// Prenom
        /// </summary>
        [XmlElement("Prenom")]
        public string Prenom { get; set; }

        /// <summary>
        /// Adresse e-mail
        /// </summary>
        [XmlElement("Courriel")]
        public string Courriel { get; set; }

        /// <summary>
        /// Societe
        /// </summary>
        [XmlElement("Societe")]
        public string Societe { get; set; }

        /// <summary>
        /// Lien entre le contact et l'utilisateur
        /// </summary>
        [XmlElement("Lien")]
        public Lien Lien { get; set; }

        /// <summary>
        /// Date de creation du contact
        /// </summary>
        [XmlElement("DateDeCreation")]
        public DateTime DateDeCreation { get; set; }

        /// <summary>
        /// Date de derniere modification du contact
        /// </summary>
        [XmlElement("DateDeModification")]
        public DateTime DateDeModification { get; set; }

        /// <summary>
        /// Constructeur par default de la classe Contact
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Constructeur de la classe Contact
        /// </summary>
        /// <param name="nom">Le nom du contact</param>
        /// <param name="prenom">Le prenom du contact</param>
        /// <param name="courriel">L'e-mail du contact</param>
        /// <param name="societe">La societe du contact</param>
        /// <param name="lien">Le lien entre le contact et l'utilisateur</param>
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

        /// <summary>
        /// Verifie si une adresse e-mail est valide suivant une expression reguliere
        /// </summary>
        /// <param name="AdresseEmail">Adresse email dont on doit determiner sa validite</param>
        /// <returns>Vrai si l'adresse email est correcte, faux sinon</returns>
        public static bool IsValidCourriel(string AdresseEmail)
        {
            Regex myRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.IgnoreCase);
            return myRegex.IsMatch(AdresseEmail);
        }

        /// <summary>
        /// Redefinission de la methode ToString pour un contact
        /// </summary>
        /// <returns>La representation d'un contact en chaine de caractere</returns>
        public override string ToString()
        {
            return "[C] " + Prenom + ", " + Nom + "(" + Societe + "), Email:" + Courriel + ", Link:" + Lien + "\n";
        }
    }
}
