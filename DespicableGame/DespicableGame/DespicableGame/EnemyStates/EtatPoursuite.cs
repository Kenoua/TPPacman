using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    /// <summary>
    /// État qui oblige un ennemi à foncer vers le joueur
    /// lorsque celui-ci est dans la vision de l'ennemi.
    /// </summary>
    public class EtatPoursuite : EtatEnnemi
    {
        private readonly PersonnageNonJoueur personnage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtatPoursuite"/> class.
        /// </summary>
        /// <param name="_personnage">The _personnage.</param>
        public EtatPoursuite(PersonnageNonJoueur _personnage)
        {
            personnage = _personnage;
        }

        /// <summary>
        /// Updates this instance.
        /// @see ChangerEtat
        /// @see JoueurEnVue
        /// </summary>
        public void Update()
        {
            if (GameStates.EtatPartieEnCours.Gru.estPokemonLegendaire)
                personnage.ChangerEtat(new EtatApeurer(personnage));

            personnage.PositionJoueur = personnage.JoueurEnVue();
            if (personnage.PositionJoueur == null)
            {
                personnage.ChangerEtat(new EtatRoder(personnage));
            }
        }

        /// <summary>
        /// Mouvements the specified a i_ case.
        /// </summary>
        /// <param name="AI_Case">a i_ case.</param>
        /// <returns></returns>
        public Case Mouvement(Case AI_Case)
        {
            Case caseDirection = personnage.Destination;
            personnage.VitesseX = 0;
            personnage.VitesseY = 0;

            if (personnage.ActualCase.OrdreX != GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreX)
            {
                if (personnage.ActualCase.OrdreX < GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreX)
                {
                    //Joueur est à la droite de l'ennemi
                    personnage.VitesseX = 4;
                    personnage.VitesseY = 0;
                    caseDirection = AI_Case.CaseDroite;
                }
                else
                {
                    //Joueur est à la gauche de l'ennemi
                    personnage.VitesseX = -4;
                    personnage.VitesseY = 0;
                    caseDirection = AI_Case.CaseGauche;
                }
            }
            else if (personnage.ActualCase.OrdreY != GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreY)
            {
                if (personnage.ActualCase.OrdreY < GameStates.EtatPartieEnCours.Gru.ActualCase.OrdreY)
                {
                    //Joueur est dessous l'ennemi
                    personnage.VitesseX = 0;
                    personnage.VitesseY = 4;
                    caseDirection = AI_Case.CaseBas;
                }
                else
                {
                    //Joueur est en haut de l'ennemi
                    personnage.VitesseX = 0;
                    personnage.VitesseY = -4;
                    caseDirection = AI_Case.CaseHaut;
                }

            }

            return caseDirection;
        }
    }
}

