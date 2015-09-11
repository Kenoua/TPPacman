using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    public class EtatApeurer : EtatEnnemi
    {
        private readonly Personnage personnage;

        public EtatApeurer(Personnage _personnage)
        {
            personnage = _personnage;
        }

        public void Update()
        {

        }

        public Case Mouvement(Case AI_Case)
        {
            return null;
        }
    }
}
