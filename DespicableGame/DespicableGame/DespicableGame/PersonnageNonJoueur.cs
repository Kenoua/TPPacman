using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    /// <summary>
    /// Classe qui définit un personnage n'étant pas joueur
    /// et qui sera donc complètement géré par l'application
    /// </summary>
    public class PersonnageNonJoueur : Personnage
    {
        private EnemyStates.EtatEnnemi etatPresent;
        private Case positionJoueur;
        private Case dernierePositionJoueur;

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
            caseSnorlax = new List<Case>();
            Destination = MouvementIA(ActualCase);
        }

        /// <summary>
        /// Changers the etat.
        /// </summary>
        /// <param name="nouvelEtat">The nouvel etat.</param>
        public void ChangerEtat(EnemyStates.EtatEnnemi nouvelEtat)
        {
            etatPresent = nouvelEtat;
        }

        /// <summary>
        /// Gets or sets the position joueur.
        /// </summary>
        /// <value>
        /// The position joueur.
        /// </value>
        public Case PositionJoueur
        {
            get { return positionJoueur; }
            set { positionJoueur = value; }
        }

        /// <summary>
        /// Gets or sets the derniere position joueur.
        /// </summary>
        /// <value>
        /// The derniere position joueur.
        /// </value>
        public Case DernierePositionJoueur
        {
            get { return dernierePositionJoueur; }
            set { dernierePositionJoueur = value; }
        }

        /// <summary>
        /// Touchers the autre personnage.
        /// </summary>
        public override void ToucherAutrePersonnage()
        {
            
        }

        /// <summary>
        /// Mouvements this instance.
        /// </summary>
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
                    while(caseSnorlax.Contains(Destination));
                }
            }

        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            etatPresent.Update();
        }

        /// <summary>
        /// Ests the mort.
        /// </summary>
        /// <returns></returns>
        public override bool EstMort()
        {
            return mort;
        }

        /// <summary>
        /// Mouvements the ia.
        /// </summary>
        /// <param name="AI_Case">a i_ case.</param>
        /// <returns></returns>
        private Case MouvementIA(Case AI_Case)
        {
            return etatPresent.Mouvement(AI_Case);
        }

        /// <summary>
        /// Joueurs the en vue.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Attribuer the joueur.
        /// </summary>
        /// <param name="joueur">The joueur.</param>
        private void attribuerJoueur(Case joueur)
        {
            positionJoueur = joueur;
            dernierePositionJoueur = joueur;
        }

        /// <summary>
        /// Regardes vers le haut.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Regardes vers la droite.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Regardes vers le  bas.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Regardes vers la gauche.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
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
        public void setCasesSnorlax(List<Case> _casesSnorlax)
        {
            caseSnorlax = _casesSnorlax;
        }
    }
}
