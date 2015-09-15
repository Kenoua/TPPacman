using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    /// <summary>
    /// Interface qui définit tous les états d'un ennemi.
    /// </summary>
    public interface EtatEnnemi
    {
        void Update();

        Case Mouvement(Case AI_Case);
    }
}
