using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.SnorlaxStates
{
    /// <summary>
    /// Classe qui définit les différents états de Snorlax.
    /// </summary>
    public interface EtatSnorlax 
    {
        /// <summary>
        /// Updates this instance.
        /// </summary>
        void Update();

        /// <summary>
        /// Mouvements the specified a i_ case.
        /// </summary>
        /// <param name="AI_Case">a i_ case.</param>
        /// <returns></returns>
        Case Mouvement(Case AI_Case);
    }
}
