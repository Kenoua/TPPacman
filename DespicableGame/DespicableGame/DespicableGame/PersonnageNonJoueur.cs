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
        private Case dernierePositionJoueur;
        public Case caseSnorlax;

        public PersonnageNonJoueur(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            positionJoueur = null;
            dernierePositionJoueur = null;
            directionArriere = -1;
            pointsVie = 1;
            dernierContact = DateTime.Now;
            delaiProchainContact = new TimeSpan(0, 0, 0, 2, 500);
            etatPresent = new EnemyStates.EtatAleatoire(this);
            caseSnorlax = new Case(0,-1,-1);
            Destination = MouvementIA(ActualCase);
        }

        public void ChangerEtat(EnemyStates.EtatEnnemi nouvelEtat)
        {
            etatPresent = nouvelEtat;
        }

        public Case PositionJoueur
        {
            get { return positionJoueur; }
            set { positionJoueur = value; }
        }

        public Case DernierePositionJoueur
        {
            get { return dernierePositionJoueur; }
            set { dernierePositionJoueur = value; }
        }

        public override void ToucherAutrePersonnage()
        {
            
        }

        public override void Mouvement()
        {
            
            if (Destination != null)
            {
                position.X += VitesseX;
                position.Y += VitesseY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    derniereCase = ActualCase;
                    ActualCase = Destination;
                    Update();
                    int counter = 0;
                    do
                    {
                        counter++;
                        Destination = MouvementIA(ActualCase);
                        if(counter >30)
                        {
                            VitesseX = -VitesseX;
                            VitesseY = -VitesseY;
                            Destination = derniereCase;
                            directionArriere += 2;
                            if(directionArriere >3)
                            {
                                directionArriere -= 4;
                            }
                        }                      
                        
                    }
                    while(CheckSnorlax(Destination));
           
                    
                }
            }

        }

        private bool CheckSnorlax(Case _case)
        {
            if (caseSnorlax != null && _case!= null)
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

        private Case MouvementIA(Case AI_Case)
        {
            return etatPresent.Mouvement(AI_Case);
        }

        public Case JoueurEnVue()
        {
            Case joueur = null;

            if (ActualCase == GameStates.EtatPartieEnCours.Gru.ActualCase)
                joueur = ActualCase;

            joueur = RegardHaut(this.ActualCase.CaseHaut);

            if (joueur != null)
            {
                attribuerJoueur(joueur);
                return joueur;
            }
            else
            {
                joueur = RegardDroit(this.ActualCase.CaseDroite);
                if (joueur != null)
                {
                    attribuerJoueur(joueur);
                    return joueur;
                }
                else
                {
                    joueur = RegardBas(this.ActualCase.CaseBas);
                    if (joueur != null)
                    {
                        attribuerJoueur(joueur);
                        return joueur;
                    }
                    else
                    {
                        joueur = RegardGauche(this.ActualCase.CaseGauche);
                        if (joueur != null)
                        {
                            attribuerJoueur(joueur);
                            return joueur;
                        }
                    }
                }
            }
            return joueur;
        }

        private void attribuerJoueur(Case joueur)
        {
            positionJoueur = joueur;
            dernierePositionJoueur = joueur;
        }

        private Case RegardHaut(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseHaut != null)
                {
                    return RegardHaut(_caseVerifier.CaseHaut);
                }
            }
            return null;
        }

        private Case RegardDroit(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseDroite != null)
                {
                    return RegardDroit(_caseVerifier.CaseDroite);
                }
            }
            return null;
        }

        private Case RegardBas(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseBas != null)
                {
                    return RegardBas(_caseVerifier.CaseBas);
                }
            }
            return null;
        }

        private Case RegardGauche(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase)
                {
                    return _caseVerifier;
                }
                else if (_caseVerifier.CaseGauche != null)
                {
                    return RegardGauche(_caseVerifier.CaseGauche);
                }
            }
            return null;
        }
    }
}
