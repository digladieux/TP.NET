using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class Contact : Entite
    {

        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Courriel { get; set; }
        public string Societe { get; set; }
        public Lien Lien { get; set; }
        public DateTime DateDeCreation { get; set; }
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
