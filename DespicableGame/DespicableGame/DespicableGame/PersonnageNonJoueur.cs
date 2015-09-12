using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public class PersonnageNonJoueur : Personnage
    {
        private EnemyStates.EtatEnnemi etatPresent;
        private Case positionJoueur;
        public Case caseSnorlax;

        public PersonnageNonJoueur(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            positionJoueur = null;
            directionArriere = -1;
            pointsVie = 1;
            dernierContact = DateTime.Now;
            delaiProchainContact = new TimeSpan(0, 0, 0, 2, 500);
            etatPresent = new EnemyStates.EtatAleatoire(this);
            Destination = MouvementIA(ActualCase);
            caseSnorlax = null;
        }

        public void ChangerEtat(EnemyStates.EtatEnnemi nouvelEtat)
        {
            etatPresent = nouvelEtat;
        }

        public Case GetPositionJoueur()
        {
            return positionJoueur;
        }

        public override void Mouvement()
        {
            if(checkSnorlax(Destination))
            {
                Destination = MouvementIA(ActualCase);
            }
            if (Destination != null)
            {
                position.X += VitesseX;
                position.Y += VitesseY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    ActualCase = Destination;
                    Destination = MouvementIA(ActualCase);
                }
            }

        }

        private bool checkSnorlax(Case _case)
        {
            if (caseSnorlax != null)
            {
                if ((_case.GetPosition() == caseSnorlax.GetPosition()))
                {
                    return true;
                }
            }
            return false;

        }

        public void Update()
        {
            etatPresent.Update();
        }

        public override bool EstMort()
        {
            return mort;
        }

        public override void ToucherAutrePersonnage()
        {
            
        }

        private Case MouvementIA(Case AI_Case)
        {
            return etatPresent.Mouvement(AI_Case);
        }

        public Case JoueurEnVue()
        {
            Case joueur = null;

            joueur = regardHaut(this.ActualCase.CaseHaut);

            if (joueur != null)
            {
                positionJoueur = joueur;
                return joueur;
            }
            else
            {
                joueur = regardDroit(this.ActualCase.CaseDroite);
                if (joueur != null)
                {
                    positionJoueur = joueur;
                    return joueur;
                }
                else
                {
                    joueur = regardBas(this.ActualCase.CaseBas);
                    if (joueur != null)
                    {
                        positionJoueur = joueur;
                        return joueur;
                    }
                    else
                    {
                        joueur = regardGauche(this.ActualCase.CaseHaut);
                        if (joueur != null)
                        {
                            positionJoueur = joueur;
                            return joueur;
                        }
                    }
                }
            }
            return joueur;
        }

        private Case regardHaut(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseHaut != null)
                {
                    regardHaut(_caseVerifier.CaseHaut);
                }
            }
            return null;
        }

        private Case regardDroit(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseDroite != null)
                {
                    regardDroit(_caseVerifier.CaseDroite);
                }
            }
            return null;
        }

        private Case regardBas(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseBas != null)
                {
                    regardBas(_caseVerifier.CaseBas);
                }
            }
            return null;
        }

        private Case regardGauche(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseGauche != null)
                {
                    regardGauche(_caseVerifier.CaseGauche);
                }
            }
            return null;
        }
    }
}
