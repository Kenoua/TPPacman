using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    public interface EtatEnnemi
    {
        void Update();

        Case Mouvement(Case AI_Case);
    }
}
