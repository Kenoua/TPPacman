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

        }

        public Case Mouvement(Case AI_Case)
        {
            Case caseDirection = personnage.Destination;
            Vector2 objectif = new Vector2(Labyrinthe.LARGEUR - GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreX,
                                            Labyrinthe.HAUTEUR - GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreY);
            personnage.VitesseX = 0;
            personnage.VitesseY = 0;

            if (personnage.ActualCase.OrdreX == objectif.X && personnage.ActualCase.OrdreY == objectif.Y)
                return personnage.ActualCase;

            List<int> directionsPriorises = new List<int>();

            if (personnage.ActualCase.OrdreY < objectif.Y)
            {
                directionsPriorises.Add(0);
                directionsPriorises.Add(1);
            }
            else
            {
                directionsPriorises.Add(1);
                directionsPriorises.Add(0);
            }
            if (personnage.ActualCase.OrdreX < objectif.X)
            {
                directionsPriorises.Add(2);
                directionsPriorises.Add(3);
            }
            else
            {
                directionsPriorises.Add(3);
                directionsPriorises.Add(2);
            }

            foreach (int direction in directionsPriorises)
            {
                if (direction != personnage.DirectionArriere)
                {
                    switch (direction)
                    {
                        case 0:
                            if (!(AI_Case.CaseHaut == null || AI_Case.CaseHaut is Teleporteur))
                            {
                                personnage.DirectionArriere = 2;
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
                                personnage.DirectionArriere = 1;
                                personnage.VitesseX = 4;
                                personnage.VitesseY = 0;
                                return AI_Case.CaseDroite;
                            }
                            break;
                        case 3:
                            if (!(AI_Case.CaseGauche == null || AI_Case.CaseGauche is Teleporteur))
                            {
                                personnage.DirectionArriere = 3;
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
            //bool culDeSac = false;
            //List<int> optionsInvalides = new List<int>();

            //while (true)
            //{
            //    if (!culDeSac)
            //    {
            //        if (personnage.ActualCase.OrdreY < objectif.Y)
            //        {
            //            if (!(AI_Case.CaseHaut == null || AI_Case.CaseHaut is Teleporteur))
            //            {
            //                personnage.DirectionArriere = 2;
            //                personnage.VitesseX = 0;
            //                personnage.VitesseY = -4/*-DespicableGame.VITESSE*/;
            //                return AI_Case.CaseHaut;
            //            }
            //            else
            //            {
            //                if (!optionsInvalides.Contains(0))
            //                    optionsInvalides.Add(0);
            //            }
            //        }
            //        else if (personnage.ActualCase.OrdreY > objectif.Y)
            //        {
            //if (!(AI_Case.CaseBas == null || AI_Case.CaseBas is Teleporteur))
            //{
            //    personnage.DirectionArriere = 0;
            //    personnage.VitesseX = 0;
            //    personnage.VitesseY = 4;
            //    return AI_Case.CaseBas;
            //}
            //            else
            //            {
            //                if (!optionsInvalides.Contains(0))
            //                    optionsInvalides.Add(0);
            //            }
            //        }
            //        else if (personnage.ActualCase.OrdreX < objectif.X)
            //        {
            //if (!(AI_Case.CaseGauche == null || AI_Case.CaseGauche is Teleporteur))
            //{
            //    personnage.DirectionArriere = 3;
            //    personnage.VitesseX = -4;
            //    personnage.VitesseY = 0;
            //    return AI_Case.CaseGauche;
            //}
            //            else
            //            {
            //                if (!optionsInvalides.Contains(2))
            //                    optionsInvalides.Add(2);
            //            }
            //        }
            //        else
            //        {
            //if (!(AI_Case.CaseDroite == null || AI_Case.CaseDroite is Teleporteur))
            //{
            //    personnage.DirectionArriere = 1;
            //    personnage.VitesseX = 4;
            //    personnage.VitesseY = 0;
            //    return AI_Case.CaseDroite;
            //}
            //            else
            //            {
            //                if (!optionsInvalides.Contains(3))
            //                    optionsInvalides.Add(3);
            //            }
            //        }

            //        if (optionsInvalides.Count() == 3)
            //        {

            //            culDeSac = true;
            //        }
            //    }

        }
    }
}
