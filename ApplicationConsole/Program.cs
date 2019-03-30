using EntitiesLayer;
using Serialisation;
using System;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// Espace de nom de l'application console
/// </summary>
namespace ApplicationConsole
{
    /// <summary>
    /// Classe principale du programme
    /// </summary>
    class Program
    {
        /// <summary>
        /// Fonction Main du programme
        /// </summary>
        /// <param name="args">Aucun argument en ligne de commande pour cette solution </param>
        static void Main(string[] args)
        {
            bool Running = true;
            Dossier ListeDossier = null;
            int EntierChoixUtilisateur;
            Rijndael Chiffrement = Rijndael.Create();
            do
            {
                AffichageMenu();
                try
                {
                    EntierChoixUtilisateur = SaisieUtilisateur.ChoixUtilisateurValide();
                    Running = ChoixMethode(ref ListeDossier, Chiffrement, EntierChoixUtilisateur);
                }
                catch (FormatException)
                {
                    Console.Write("Instruction inconnue\n");
                }

            } while (Running);
        }

        /// <summary>
        /// Affichage des choix de l'utilisateur dans l'ecran de menu
        /// </summary>
        private static void AffichageMenu()
        {
            Console.WriteLine("Taper 1 pour sortir de l'application");
            Console.WriteLine("Taper 2 pour afficher l'arborescence");
            Console.WriteLine("Taper 3 pour ajouter un nouveau dossier");
            Console.WriteLine("Taper 4 pour ajouter un nouveau contact");
            Console.WriteLine("Taper 5 pour charger les donnees");
            Console.WriteLine("Taper 6 pour enregistrer les donnees\n");
        }

        /// <summary>
        /// Fonction pour le lancement des differentes activites suivant le choix de l'utilisateur
        /// </summary>
        /// <param name="ListeDossier">Reference sur l'arborescence de dossier</param>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="ChoixUtilisateur">Choix de l'utilisateur</param>
        /// <returns>Si le programme continue de tourner ou s'il doit s'arreter</returns>
        private static bool ChoixMethode(ref Dossier ListeDossier, Rijndael Chiffrement, int ChoixUtilisateur)
        {
            bool Running = true;
            switch (ChoixUtilisateur)
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

        /// <summary>
        /// Affichage de l'arborescence de fichier a l'utilisateur. Si la liste est vide, on indique un message
        /// </summary>
        /// <param name="ListeDossier">La liste que l'on veut afficher</param>
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

        /// <summary>
        /// Creation d'un dossier dans l'arboresence
        /// </summary>
        /// <param name="ListeDossier">L'arborescence</param>
        private static void CaseCreationDossier(ref Dossier ListeDossier)
        {
            // On instancie un nouveau dossier
            Console.WriteLine("Comment voulez vous appeller votre dossier ?\n");
            string NomDossier = Console.ReadLine();
            Dossier NouveauDossier = new Dossier(NomDossier);
            Console.WriteLine();

            // Si l'arborescence est vide alors on le cree a la racine
            if (ListeDossier == null)
            {
                NouveauDossier.Profondeur = 0;
                ListeDossier = NouveauDossier;
            }
            // Sinon on propose a l'utilisateur de l'inserer ou il le souhaite
            else
            {
                Console.WriteLine("Où voulez vous inserer ce nouveau dossier ?");
                Dossier DossierParent = RechercheDossier(ListeDossier);

                NouveauDossier.Profondeur = DossierParent.Profondeur + 1;
                DossierParent.AjouterEntite(NouveauDossier);
            }
        }

        /// <summary>
        /// Affichage des differentes relations possible entre un utilisateur et son contact
        /// </summary>
        private static void AffichageRelation()
        {
            Console.WriteLine("\nQuel est votre relation avec ce contact ?");
            Console.WriteLine("Taper 1 si le contact est un Ami");
            Console.WriteLine("Taper 2 si le contact est un Collegue");
            Console.WriteLine("Taper 3 si le contact est une Relation");
            Console.WriteLine("Taper 4 si le contact est un Reseau\n");

        }

        /// <summary>
        /// Formulaire pour la creation d'un nouveau contact
        /// </summary>
        /// <returns>Le contact</returns>
        private static Contact FormulaireCreationContact()
        {
            Console.WriteLine("Quel est le nom de votre contact ?");
            string NomContact = Console.ReadLine();

            Console.WriteLine("\nQuel est le prenom de votre contact ?");
            string PrenomContact = Console.ReadLine();

            Console.WriteLine("\nQuel est la societe de votre contact ?");
            string SocieteContact = Console.ReadLine();


            Console.WriteLine("\nQuel est le courrier de votre contact ? (adresse email valide)\n");
            bool IsCourrielValid = false;
            string CourrielContact = null;
            while (!IsCourrielValid)
            {
                CourrielContact = Console.ReadLine();
                IsCourrielValid = Contact.IsValidCourriel(CourrielContact);
            }

            AffichageRelation();
            int RelationContact = -1;
            bool IsRelationValide;
            do
            {
                try
                {
                    RelationContact = SaisieUtilisateur.ChoixUtilisateurValide();
                    IsRelationValide = true;
                }
                catch (FormatException e)
                {
                    IsRelationValide = false;
                    Console.WriteLine(e.Message);
                }
            } while (!IsRelationValide);
            return new Contact(NomContact, PrenomContact, CourrielContact, SocieteContact, LienMethodeStatique.IntToLien(RelationContact));
        }

        /// <summary>
        /// Permet la creation et l'ajout d'un contact dans l'arborescence
        /// </summary>
        /// <param name="ListeDossier">Arborescence de fichier</param>
        private static void CaseCreationContact(Dossier ListeDossier)
        {
            // Si il n'existe pas de dossier racine on ne peut rien ajouter dedans
            if (ListeDossier == null)
            {
                Console.WriteLine("Vous devez creer un dossier racine pour ajouter des contacts\n");
            }
            else
            {

                Contact NouveauContact = FormulaireCreationContact();

                Console.WriteLine("\nOù voulez vous inserer ce nouveau contact ?\n");
                Dossier DossierParent = RechercheDossier(ListeDossier);
                DossierParent.AjouterEntite(NouveauContact);
            }

        }

        /// <summary>
        /// Methode pour lancer la deserialisation d'un fichier chiffre
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="ListeDossier">L'arborescence qui va recuperer le resultat de la deserialisation</param>
        private static void CaseDeserialisation(Rijndael Chiffrement, ref Dossier ListeDossier)
        {
            // Si nous n'avons pas serialiser avant ou que notre base de donnee a ete supprime, on ne peut pas deserialiser
            if (Constantes.ChoixSerialisation == null)
            {
                Console.WriteLine("Le fichier de Serialisation n'existe pas (vous n'avez peut etre pas serialiser de donnee)\n");
            }
            else
            {
                // Si l'utilisateur saisit un mot de passe invalide, alors on supprime la base de donnee
                if (!SaisieUtilisateur.MotDePasseValide())
                {
                    Constantes.ChoixSerialisation = null;
                    File.Delete(Constantes.CheminFichierChiffrer);
                    Console.WriteLine("Mot de passe Invalide 3 tentatives .. Suppression de la Base de donnée\n");
                }

                // Sinon on essaie de deserialiser
                else
                {
                    try
                    {
                        Constantes.ChoixSerialisation.Deserialise(Chiffrement, ref ListeDossier);
                        Constantes.ChoixSerialisation = null;
                    }
                    catch (FileNotFoundException)
                    {
                        Console.WriteLine("Le fichier n'existe pas\n");
                    }
                }
            }
        }

        /// <summary>
        /// Methode pour le lancement de la serialisation de notre arborescence
        /// </summary>
        /// <param name="Chiffrement">Chiffrement du fichier</param>
        /// <param name="ListeDossier">L'arborescence que l'on veut chiffrer</param>
        private static void CaseSerialisation(Rijndael Chiffrement, Dossier ListeDossier)
        {
            // Si l'arborescence est null, on ne peut pas serialiser
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
                    try
                    {
                        ChoixUtilisateur = SaisieUtilisateur.ChoixUtilisateurValide();
                    }
                    catch (FormatException)
                    {
                        ChoixUtilisateur = -1;
                        Console.WriteLine("Combinaison Invalide");
                    }
                } while ((ChoixUtilisateur != 1) && (ChoixUtilisateur != 2));

                // Utilisation du pattern Factory
                if (ChoixUtilisateur == 1)
                {
                    Constantes.ChoixSerialisation = new SerialisationBinaire();
                }
                else
                {
                    Constantes.ChoixSerialisation = new SerialisationXML();
                }

                SaisieUtilisateur.ChoixCleChiffrement(ref Chiffrement);
                Constantes.ChoixSerialisation.Serialise(Chiffrement, ListeDossier);

                // On creer un mot de passe pour le fichier
                SaisieUtilisateur.ChoixMotDePasse();
            }
        }

        /// <summary>
        /// On effectue la recherche d'un dossier dans une arborescence
        /// </summary>
        /// <param name="ListeDossier">Arborescence ou l'on recherche un dossier</param>
        /// <returns>Le dossier recherche</returns>
        private static Dossier RechercheDossier(Dossier ListeDossier)
        {
            Dossier DossierParent = null;
            int ChoixUtilisateur;
            while (DossierParent == null)
            {
                Console.WriteLine(ListeDossier.ToString(false));
                try
                {
                    ChoixUtilisateur = SaisieUtilisateur.ChoixUtilisateurValide();
                    DossierParent = ListeDossier.RechercherDossier(ChoixUtilisateur);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Dossier Inexistant");
                }
            }
            return DossierParent;
        }

    }
}
