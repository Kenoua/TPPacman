using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame
{
    public class Pointage
    {
        private static Pointage instance = null;
        int totalPointage;
        int serie;

        private Pointage()
        {
            totalPointage = 0;
            serie = 0;
        }

        public static Pointage GetInstance()
        {
            if(instance == null)
            {
                instance = new Pointage();
            }
            return instance;
        }

        public void AjouterPoints(int _ajout)
        {
            totalPointage += _ajout + serie;
            IncrementerSerie();
        }

        public void RetirerPoints(int _retrait)
        {
            totalPointage -= _retrait;
        }

        public int GetTotalPointage()
        {
            return totalPointage;
        }

        public void RetourZero()
        {
            totalPointage = 0;
        }

        private void IncrementerSerie()
        {
            serie += 10;
        }

        public void ReinitialiserSerie()
        {
            serie = 0;
        }
    }
}
