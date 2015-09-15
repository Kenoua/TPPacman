using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.SnorlaxStates
{
    public interface EtatSnorlax 
    {
        void Update();

        Case Mouvement(Case AI_Case);
    }
}
