using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame
{
    /// <summary>
    /// Classe statique qui permet d'avoir un chiffre aléatoire.
    /// </summary>
    public static class GenerateurChiffreAleatoire
    {
        private static Random random = new Random();

        /// <summary>
        /// Retourne un nouveau chiffre.
        /// </summary>
        /// <param name="_max">The _max.</param>
        /// <returns></returns>
        public static int NouveauChiffre(int _max)
        {
            return random.Next(_max);
        }
    }
}
