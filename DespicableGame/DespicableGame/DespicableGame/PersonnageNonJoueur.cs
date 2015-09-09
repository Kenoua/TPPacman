using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    class PersonnageNonJoueur : Personnage
    {
        public PersonnageNonJoueur(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            Destination = MouvementIA(ActualCase);
        }

        public override void Mouvement()
        {
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

        //AI totalement random et qui ne peut pas entrer dans les téléporteurs.  À revoir absolument.
        private Case MouvementIA(Case AI_Case)
        {
            Random r = new Random();

            while (true)
            {
                int choixRandom = r.Next(4);

                if (choixRandom == 0)
                {
                    //Plus efficace qu'un &&, dès que la première condition courante est remplie, on arrête le test
                    if (!(AI_Case.CaseHaut == null || AI_Case.CaseHaut is Teleporteur))
                    {
                        VitesseX = 0;
                        VitesseY = -DespicableGame.VITESSE;
                        return AI_Case.CaseHaut;
                    }
                }

                if (choixRandom == 1)
                {
                    if (!(AI_Case.CaseBas == null || AI_Case.CaseBas is Teleporteur))
                    {
                        VitesseX = 0;
                        VitesseY = DespicableGame.VITESSE;
                        return AI_Case.CaseBas;
                    }
                }

                if (choixRandom == 2)
                {
                    if (!(AI_Case.CaseGauche == null || AI_Case.CaseGauche is Teleporteur))
                    {
                        VitesseX = -DespicableGame.VITESSE;
                        VitesseY = 0;
                        return AI_Case.CaseGauche;
                    }
                }

                if (choixRandom == 3)
                {
                    if (!(AI_Case.CaseDroite == null || AI_Case.CaseDroite is Teleporteur))
                    {
                        VitesseX = DespicableGame.VITESSE;
                        VitesseY = 0;
                        return AI_Case.CaseDroite;
                    }
                }
            }
        }
    }
}
