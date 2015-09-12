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

        private Pointage()
        {
            totalPointage = 0;
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
            totalPointage += _ajout;
        }

        public int GetTotalPointage()
        {
            return totalPointage;
        }

        public void RetourZero()
        {
            totalPointage = 0;
        }
    }
}
