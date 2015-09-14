using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.EnemyStates
{
    public class EtatPoursuite : EtatEnnemi
    {
        private readonly PersonnageNonJoueur personnage;

        public EtatPoursuite(PersonnageNonJoueur _personnage)
        {
            personnage = _personnage;
        }

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

