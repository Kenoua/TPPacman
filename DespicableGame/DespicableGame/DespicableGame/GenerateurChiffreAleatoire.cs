using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame
{
    public static class GenerateurChiffreAleatoire
    {
        private static Random random = new Random();

        public static int NouveauChiffre(int _max)
        {
            return random.Next(_max);
        }
    }
}
