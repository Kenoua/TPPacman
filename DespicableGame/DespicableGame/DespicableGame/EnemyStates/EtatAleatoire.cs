using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    public class EtatAleatoire : EtatEnnemi
    {
        private readonly Personnage personnage;

        public EtatAleatoire(Personnage _personnage)
        {
            personnage = _personnage;
        }

        public void Update()
        {

        }

        public Case Mouvement(Case AI_Case)
        {
            Random r = new Random();

            while (true)
            {
                int choixRandom = r.Next(4);

                if (choixRandom == 0)
                {
                    if (!(AI_Case.CaseHaut == null || AI_Case.CaseHaut is Teleporteur))
                    {
                        personnage.VitesseX = 0;
                        personnage.VitesseY = -4/*-DespicableGame.VITESSE*/;
                        return AI_Case.CaseHaut;
                    }
                }

                if (choixRandom == 1)
                {
                    if (!(AI_Case.CaseBas == null || AI_Case.CaseBas is Teleporteur))
                    {
                        personnage.VitesseX = 0;
                        personnage.VitesseY = 4;
                        return AI_Case.CaseBas;
                    }
                }

                if (choixRandom == 2)
                {
                    if (!(AI_Case.CaseGauche == null || AI_Case.CaseGauche is Teleporteur))
                    {
                        personnage.VitesseX = -4;
                        personnage.VitesseY = 0;
                        return AI_Case.CaseGauche;
                    }
                }

                if (choixRandom == 3)
                {
                    if (!(AI_Case.CaseDroite == null || AI_Case.CaseDroite is Teleporteur))
                    {
                        personnage.VitesseX = 4;
                        personnage.VitesseY = 0;
                        return AI_Case.CaseDroite;
                    }
                }
            }
        }
    }
}
