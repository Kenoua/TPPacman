using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    public class EtatPoursuite : EtatEnnemi
    {
        private readonly Personnage personnage;

        public EtatPoursuite(Personnage _personnage)
        {
            personnage = _personnage;
        }

        public void Update()
        {

        }

        public Case Mouvement(Case AI_Case)
        {
            //while (true)
            //{
            //    if (choixRandom != personnage.DirectionArriere)
            //    {
            //        if (choixRandom == 0)
            //        {
            //            if (!(AI_Case.CaseHaut == null || AI_Case.CaseHaut is Teleporteur))
            //            {
            //                personnage.DirectionArriere = 1;
            //                personnage.VitesseX = 0;
            //                personnage.VitesseY = -4/*-DespicableGame.VITESSE*/;
            //                return AI_Case.CaseHaut;
            //            }
            //            else
            //            {
            //                if (!optionsInvalides.Contains(choixRandom))
            //                    optionsInvalides.Add(choixRandom);
            //            }
            //        }

            //        if (choixRandom == 1)
            //        {
            //            if (!(AI_Case.CaseBas == null || AI_Case.CaseBas is Teleporteur))
            //            {
            //                personnage.DirectionArriere = 0;
            //                personnage.VitesseX = 0;
            //                personnage.VitesseY = 4;
            //                return AI_Case.CaseBas;
            //            }
            //            else
            //            {
            //                if (!optionsInvalides.Contains(choixRandom))
            //                    optionsInvalides.Add(choixRandom);
            //            }
            //        }

            //        if (choixRandom == 2)
            //        {
            //            if (!(AI_Case.CaseGauche == null || AI_Case.CaseGauche is Teleporteur))
            //            {
            //                personnage.DirectionArriere = 3;
            //                personnage.VitesseX = -4;
            //                personnage.VitesseY = 0;
            //                return AI_Case.CaseGauche;
            //            }
            //            else
            //            {
            //                if (!optionsInvalides.Contains(choixRandom))
            //                    optionsInvalides.Add(choixRandom);
            //            }
            //        }

            //        if (choixRandom == 3)
            //        {
            //            if (!(AI_Case.CaseDroite == null || AI_Case.CaseDroite is Teleporteur))
            //            {
            //                personnage.DirectionArriere = 2;
            //                personnage.VitesseX = 4;
            //                personnage.VitesseY = 0;
            //                return AI_Case.CaseDroite;
            //            }
            //            else
            //            {
            //                if (!optionsInvalides.Contains(choixRandom))
            //                    optionsInvalides.Add(choixRandom);
            //            }
            //        }

            //        if (optionsInvalides.Count() == 3)
            //        {

            //            culDeSac = true;
            //        }

            //    }
            return null;
        }
    }
}

