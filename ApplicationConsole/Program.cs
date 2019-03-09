using EntitiesLayer;
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
            int ChoixUtilisateur;

            do
            {
                AffichageMenu();
                ChoixUtilisateur = Console.Read();
                Running = ChoixMethode(ListeDossier, ChoixUtilisateur);
            }while (Running) ;
        }

        static void AffichageMenu()
        {
            Console.WriteLine("Taper 1 pour sortir de l'application");
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
                    break;
                case 4:
                    break;
                case 5:
                    ListeDossier = Serialisation.SerialisationBinaire.Deserialize();
                    break;
                case 6:
                    Serialisation.SerialisationBinaire.Serialize(ListeDossier);
                    break;
                default:
                    Console.WriteLine("Instruction Inconnue");
                    break;
            }
            return Running;
        }

        static
    }
}
