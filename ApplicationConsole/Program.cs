using System;
using System.Collections.Generic;
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
            int ChoixUtilisateur;
            do
            {
                AffichageMenu();
                ChoixUtilisateur = Console.Read();
                Running = ChoixMethode(ChoixUtilisateur);
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

        static bool ChoixMethode(int ChoixUtilisateur)
        {
            bool Running = true;
            switch(ChoixUtilisateur)
            {
                case 1:
                    Running = false;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    Console.WriteLine("Instruction Inconnue");
                    break;
            }
            return Running;
        }
    }
}
