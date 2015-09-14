using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    public class EtatRoder : EtatEnnemi
    {

        private readonly PersonnageNonJoueur personnage;

        public EtatRoder(PersonnageNonJoueur _personnage)
        {
            personnage = _personnage;
        }

        public void Update()
        {
            if(personnage.ActualCase == personnage.DernierePositionJoueur)
            {
                personnage.DernierePositionJoueur = null;
                personnage.ChangerEtat(new EtatAleatoire(personnage));
            }
        }

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
