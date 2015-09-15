using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.SnorlaxStates
{
    /// <summary>
    /// Définit l'état dans lequel Snorlax est immobile
    /// </summary>
    class EtatSommeil : EtatSnorlax
    {
        private readonly Snorlax personnage;
        private int tempsSommeil;
        public EtatSommeil(Snorlax _personnage)
        {
            personnage = _personnage;
            tempsSommeil = GenerateurChiffreAleatoire.NouveauChiffre(180,300);
            personnage.VitesseY = 0;
            personnage.VitesseX = 0;
        }

        public void Update()
        {
            tempsSommeil--;
            if (tempsSommeil <= 0)
            {
                personnage.ChangerEtat(new SnorlaxStates.EtatDeplacement(personnage));
               
            }
        }
        public Case Mouvement(Case AI_Case)
        {
            return AI_Case;
        }

    }
}
