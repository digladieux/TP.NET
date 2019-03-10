using EntitiesLayer;
using Serialisation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Running = true;
            Dossier ListeDossier = null ;
            ListeDossier = new Dossier("Dossier 1");
            int EntierChoixUtilisateur;

            do
            {
                AffichageMenu();
                EntierChoixUtilisateur = ChoixUtilisateurValide();
                Running = ChoixMethode(ListeDossier, EntierChoixUtilisateur);

            }while (Running) ;
        }

        static void AffichageMenu()
        {
            Console.WriteLine("\nTaper 1 pour sortir de l'application");
            Console.WriteLine("Taper 2 pour afficher l'arborescence");
            Console.WriteLine("Taper 3 pour ajouter un nouveau dossier");
            Console.WriteLine("Taper 4 pour ajouter un nouveau contact");
            Console.WriteLine("Taper 5 pour charger les donnees");
            Console.WriteLine("Taper 6 pour enregistrer les donnees");
        }

        static bool ChoixMethode(Dossier ListeDossier, int ChoixUtilisateur)
        {
            bool Running = true;
            switch(ChoixUtilisateur)
            {
                case 1:
                    Running = false;
                    break;
                case 2:
                    Console.WriteLine(ListeDossier);
                    break;
                case 3:
                    CaseCreationDossier(ListeDossier);
                    break;
                case 4:
                    CaseCreationContact(ListeDossier);
                    break;
                case 5:
                    ListeDossier = CaseDeserialisation();
                    break;
                case 6:
                    CaseSerialisation(ListeDossier);
                    break;
                default:
                    Console.WriteLine("Instruction Inconnue");
                    break;
            }
            return Running;
        }

        static void CaseCreationContact(Dossier ListeDossier)
        {
            Console.WriteLine("Quel est le nom de votre contact ?");
            string NomContact = Console.ReadLine();

            Console.WriteLine("Quel est le prenom de votre contact ?");
            string PrenomContact = Console.ReadLine();

            Console.WriteLine("Quel est la societe de votre contact ?");
            string SocieteContact = Console.ReadLine();


            Console.WriteLine("Quel est le courrier de votre contact ? (adresse email valide)");
            bool IsCourrielValid = false;
            string CourrielContact = null ;
            while (!IsCourrielValid)
            {
                CourrielContact = Console.ReadLine();
                IsCourrielValid = Contact.IsValidCourriel(CourrielContact);
            }

            Console.WriteLine("Quel est votre relation avec ce contact ?");
            Console.WriteLine("Taper 1 si le contact est un Ami");
            Console.WriteLine("Taper 2 si le contact est un Collegue");
            Console.WriteLine("Taper 3 si le contact est une Relation");
            Console.WriteLine("Taper 4 si le contact est un Reseau");

            int RelationContact;
            do
            {
                RelationContact = ChoixUtilisateurValide();
            } while ((RelationContact < 0) || (RelationContact > 4));

            Contact NouveauContact = new Contact(NomContact, PrenomContact, CourrielContact, SocieteContact, MethodeStatique.IntToLien(RelationContact));

            //TODO Ou l'ajouter

        }

        static void CaseCreationDossier(Dossier ListeDossier)
        {
            Console.WriteLine("Comment voulez vous appeller votre dossier ?");
            string NomDossier = Console.ReadLine();
            Dossier NouveauDossier = new Dossier(NomDossier);
         
            //TODO Ou l'ajouter
        }
        static void CaseSerialisation(Dossier ListeDossier)
        {
            Console.WriteLine("Comment voulez vous Serialiser votre arborescence ? ");
            Console.WriteLine("Taper 1 pour une serialisation binaire");
            Console.WriteLine("Taper 2 pour une serialisation XML");

            int ChoixUtilisateur;
            do
            {
                ChoixUtilisateur = ChoixUtilisateurValide();
            }while((ChoixUtilisateur != 1) && (ChoixUtilisateur != 2) );

            ISerialisation Serialisation;
            if (ChoixUtilisateur == 1)
            {
                Serialisation = new SerialisationBinaire();
            }
            else
            {
                Serialisation = new SerialisationXML();
            }

            Serialisation.Serialise(ListeDossier);
        }

        static Dossier CaseDeserialisation()
        {
            Console.WriteLine("Comment voulez vous Deserialiser votre arborescence ? ");
            Console.WriteLine("Taper 1 pour une deserialisation binaire");
            Console.WriteLine("Taper 2 pour une deserialisation XML");

            int ChoixUtilisateur;
            do
            {
                ChoixUtilisateur = ChoixUtilisateurValide();
            } while ((ChoixUtilisateur != 1) && (ChoixUtilisateur != 2));

            ISerialisation Deserialisation;
            if (ChoixUtilisateur == 1)
            {
                Deserialisation = new SerialisationBinaire();
            }
            else
            {
                Deserialisation = new SerialisationXML();
            }

            return Deserialisation.Deserialise();
        }
        public static int ChoixUtilisateurValide()
        {
            string ChaineChoixUtilisateur;
            int EntierChoixUtilisateur = -1;
            bool IsChoixValide = false;
            while (!IsChoixValide)
            {
                try
                {
                    ChaineChoixUtilisateur = Console.ReadLine();
                    EntierChoixUtilisateur = int.Parse(ChaineChoixUtilisateur);
                    IsChoixValide = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Chaine Invalide");
                }
            }
            return EntierChoixUtilisateur;

        }
    }

   
}
