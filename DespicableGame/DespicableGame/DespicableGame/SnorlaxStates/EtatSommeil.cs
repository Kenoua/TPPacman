using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.SnorlaxStates
{
    class EtatSommeil : EtatSnorlax
    {
        private readonly Snorlax personnage;

        public EtatSommeil(Snorlax _personnage)
        {
            personnage = _personnage;
        }

        public void Update()
        {
            
        }

        public Case Mouvement(Case AI_Case)
        {
            return AI_Case;

        }

    }
}
