using EntitiesLayer;
using Serialisation;
using System;
using System.Security.Cryptography;

namespace ApplicationConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Running = true;
            Dossier ListeDossier = null ;
            int EntierChoixUtilisateur;
            Rijndael Chiffrement = Rijndael.Create();
            do
            {
                AffichageMenu();
                EntierChoixUtilisateur = ChoixUtilisateurValide();
                Running = ChoixMethode(ref ListeDossier, Chiffrement, EntierChoixUtilisateur);

            }while (Running) ;
        }

        private static void AffichageMenu()
        {
            Console.WriteLine("Taper 1 pour sortir de l'application");
            Console.WriteLine("Taper 2 pour afficher l'arborescence");
            Console.WriteLine("Taper 3 pour ajouter un nouveau dossier");
            Console.WriteLine("Taper 4 pour ajouter un nouveau contact");
            Console.WriteLine("Taper 5 pour charger les donnees");
            Console.WriteLine("Taper 6 pour enregistrer les donnees\n");
        }

        private static bool ChoixMethode(ref Dossier ListeDossier, Rijndael Chiffrement, int ChoixUtilisateur)
        {
            bool Running = true;
            switch(ChoixUtilisateur)
            {
                case 1:
                    Running = false;
                    break;
                case 2:
                    CaseAffichageArborescence(ListeDossier);
                    break;
                case 3:
                    CaseCreationDossier(ref ListeDossier);
                    break;
                case 4:
                    CaseCreationContact(ListeDossier);
                    break;
                case 5:
                    CaseDeserialisation(Chiffrement, ref ListeDossier);
                    break;
                case 6:
                    CaseSerialisation(Chiffrement, ListeDossier);
                    break;
                default:
                    Console.WriteLine("Instruction Inconnue\n");
                    break;
            }
            return Running;
        }

        private static void CaseAffichageArborescence(Dossier ListeDossier)
        {
            if (ListeDossier == null)
            {
                Console.WriteLine("Arborescence Vide\n");
            }
            else
            {
                Console.WriteLine(ListeDossier.ToString(true));
            }
        }

        private static void CaseDeserialisation(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            if (Constantes.ChoixSerialisation == null)
            {
                Console.WriteLine("Le fichier de Serialisation n'existe pas\n");
            }
            else
            {
                Constantes.ChoixSerialisation.Deserialise(Chiffrement, ref ListeDossier);
            }
        }

        private static void CaseCreationContact(Dossier ListeDossier)
        {
            if (ListeDossier == null)
            {
                Console.WriteLine("Vous devez creer un dossier racine pour ajouter des contacts\n");
            }
            else
            {

                Console.WriteLine("Quel est le nom de votre contact ?");
                string NomContact = Console.ReadLine();

                Console.WriteLine("\nQuel est le prenom de votre contact ?");
                string PrenomContact = Console.ReadLine();

                Console.WriteLine("\nQuel est la societe de votre contact ?");
                string SocieteContact = Console.ReadLine();


                Console.WriteLine("\nQuel est le courrier de votre contact ? (adresse email valide)\n");
                bool IsCourrielValid = false;
                string CourrielContact = null ;
                while (!IsCourrielValid)
                {
                    CourrielContact = Console.ReadLine();
                    IsCourrielValid = Contact.IsValidCourriel(CourrielContact);
                }

                Console.WriteLine("\nQuel est votre relation avec ce contact ?");
                Console.WriteLine("Taper 1 si le contact est un Ami");
                Console.WriteLine("Taper 2 si le contact est un Collegue");
                Console.WriteLine("Taper 3 si le contact est une Relation");
                Console.WriteLine("Taper 4 si le contact est un Reseau\n");

                int RelationContact;
                do
                {
                    RelationContact = ChoixUtilisateurValide();
                } while ((RelationContact < 0) || (RelationContact > 4));

                Contact NouveauContact = new Contact(NomContact, PrenomContact, CourrielContact, SocieteContact, MethodeStatique.IntToLien(RelationContact));
                Console.WriteLine("\nOù voulez vous inserer ce nouveau contact ?\n");
                Dossier DossierParent = RechercheDossier(ListeDossier);
                DossierParent.AjouterEntite(NouveauContact);
            }

        }

        static void CaseCreationDossier(ref Dossier ListeDossier)
        {
            Console.WriteLine("Comment voulez vous appeller votre dossier ?\n");
            string NomDossier = Console.ReadLine();
            Dossier NouveauDossier = new Dossier(NomDossier);

            Console.WriteLine();

            if (ListeDossier == null)
            {
                NouveauDossier.Profondeur = 0;
                ListeDossier = NouveauDossier;
            }
            else
            {
                Console.WriteLine("Où voulez vous inserer ce nouveau dossier ?");
                Dossier DossierParent = RechercheDossier(ListeDossier);
                NouveauDossier.Profondeur = DossierParent.Profondeur + 1;
                DossierParent.AjouterEntite(NouveauDossier);
            }
        }
        private static void CaseSerialisation(Rijndael Chiffrement, Dossier ListeDossier)
        {
            if (ListeDossier == null)
            {
                Console.WriteLine("Il n'y a rien a Serialiser\n");
            }
            else
            {
                Console.WriteLine("Comment voulez vous Serialiser votre arborescence ? ");
                Console.WriteLine("Taper 1 pour une serialisation binaire");
                Console.WriteLine("Taper 2 pour une serialisation XML\n");

                int ChoixUtilisateur;
                do
                {
                    ChoixUtilisateur = ChoixUtilisateurValide();
                }while((ChoixUtilisateur != 1) && (ChoixUtilisateur != 2) );

                if (ChoixUtilisateur == 1)
                {
                    Constantes.ChoixSerialisation = new SerialisationBinaire() ;
                }
                else
                {
                    Constantes.ChoixSerialisation = new SerialisationXML();
                }

                Constantes.ChoixSerialisation.Serialise(Chiffrement, ListeDossier);
            }
        }

        private static int ChoixUtilisateurValide()
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
                    Console.WriteLine();

                }
                catch (Exception)
                {
                    Console.WriteLine("Chaine Invalide\n");
                }
            }
            return EntierChoixUtilisateur;

        }

        private static Dossier RechercheDossier(Dossier ListeDossier)
        {
            Dossier DossierParent = null;
            while (DossierParent == null)
            {
                Console.WriteLine(ListeDossier.ToString(false));
                int choix = ChoixUtilisateurValide();
                DossierParent = ListeDossier.RechercherDossier(choix);
            }
            return DossierParent;
        }
    }

   
}
