using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    /// <summary>
    /// État d'un ennemi qui le fait roder vers
    /// la derniere position connue du joueur.
    /// </summary>
    public class EtatRoder : EtatEnnemi
    {

        private readonly PersonnageNonJoueur personnage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtatRoder"/> class.
        /// </summary>
        /// <param name="_personnage">The _personnage.</param>
        public EtatRoder(PersonnageNonJoueur _personnage)
        {
            personnage = _personnage;
        }

        /// <summary>
        /// Updates this instance.
        /// @see ChangerEtat
        /// </summary>
        public void Update()
        {
            if(personnage.ActualCase == personnage.DernierePositionJoueur)
            {
                personnage.DernierePositionJoueur = null;
                personnage.ChangerEtat(new EtatAleatoire(personnage));
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

            if (personnage.ActualCase.OrdreX != personnage.DernierePositionJoueur.OrdreX)
            {
                if (personnage.ActualCase.OrdreX < personnage.DernierePositionJoueur.OrdreX)
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
            else if (personnage.ActualCase.OrdreY != personnage.DernierePositionJoueur.OrdreY)
            {
                if (personnage.ActualCase.OrdreY < personnage.DernierePositionJoueur.OrdreY)
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
