using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitiesLayer
{
    public class MethodesStatiques
    {
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
                    Console.WriteLine();

                }
                catch (Exception)
                {
                    Console.WriteLine("Chaine Invalide\n");
                }
            }
            return EntierChoixUtilisateur;
        }
    }
}
