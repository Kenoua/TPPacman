using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame.EnemyStates
{
    public class EtatApeurer : EtatEnnemi
    {

        private readonly PersonnageNonJoueur personnage;

        public EtatApeurer(PersonnageNonJoueur _personnage)
        {
            personnage = _personnage;
        }

        public void Update()
        {
            if(!GameStates.EtatPartieEnCours.Gru.estPokemonLegendaire)
            {
                personnage.ChangerEtat(new EtatAleatoire(personnage));
            }
        }

        public Case Mouvement(Case AI_Case)
        {
            Case caseDirection = personnage.Destination;
            personnage.VitesseX = 0;
            personnage.VitesseY = 0;

            List<int> directionsPriorises = new List<int>();

            if (personnage.ActualCase.OrdreY < GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreY)
            {
                directionsPriorises.Add(0);
            }
            else
            {
                directionsPriorises.Add(1);
            }
            if (personnage.ActualCase.OrdreX < GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreX)
            {
                directionsPriorises.Add(3);
            }
            else
            {
                directionsPriorises.Add(2);
            }
            for (int i = 0; i < 4; i++)
            {
                if (!directionsPriorises.Contains(i))
                    directionsPriorises.Add(i);
            }

            foreach (int direction in directionsPriorises)
            {
                if (direction != personnage.DirectionArriere || (direction == directionsPriorises[3] || direction == directionsPriorises[2]))
                {
                    switch (direction)
                    {
                        case 0:
                            if (!(AI_Case.CaseHaut == null || AI_Case.CaseHaut is Teleporteur))
                            {
                                personnage.DirectionArriere = 1;
                                personnage.VitesseX = 0;
                                personnage.VitesseY = -4/*-DespicableGame.VITESSE*/;
                                return AI_Case.CaseHaut;
                            }
                            break;
                        case 1:
                            if (!(AI_Case.CaseBas == null || AI_Case.CaseBas is Teleporteur))
                            {
                                personnage.DirectionArriere = 0;
                                personnage.VitesseX = 0;
                                personnage.VitesseY = 4;
                                return AI_Case.CaseBas;
                            }
                            break;
                        case 2:
                            if (!(AI_Case.CaseDroite == null || AI_Case.CaseDroite is Teleporteur))
                            {
                                personnage.DirectionArriere = 3;
                                personnage.VitesseX = 4;
                                personnage.VitesseY = 0;
                                return AI_Case.CaseDroite;
                            }
                            break;
                        case 3:
                            if (!(AI_Case.CaseGauche == null || AI_Case.CaseGauche is Teleporteur))
                            {
                                personnage.DirectionArriere = 2;
                                personnage.VitesseX = -4;
                                personnage.VitesseY = 0;
                                return AI_Case.CaseGauche;
                            }
                            break;
                        default:
                            return personnage.Destination;
                    }
                }
            }
            return null;
        }
    }
}
