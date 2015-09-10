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

        public PersonnageNonJoueur(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            pointsVie = 1;
            etatPresent = new EnemyStates.EtatAleatoire(this);
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

        public override bool EstMort()
        {
            return mort;
        }

        public override void Toucher()
        {
            
        }

        //AI totalement random et qui ne peut pas entrer dans les téléporteurs.  À revoir absolument.
        private Case MouvementIA(Case AI_Case)
        {
            return etatPresent.Mouvement(AI_Case);
        }
    }
}
