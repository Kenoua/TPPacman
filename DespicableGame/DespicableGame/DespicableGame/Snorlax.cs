using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    public class Snorlax : Personnage
    {
        private SnorlaxStates.EtatSnorlax etatPresent;
        private int counterTest;
        public Snorlax(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            etatPresent = new SnorlaxStates.EtatSommeil(this);
            counterTest = 0;
        }

        public void Update()
        {
            counterTest++;
            if(counterTest >300)
            {
                etatPresent = new SnorlaxStates.EtatDeplacement(this);
            }
        }

        public override void Mouvement()
        {

            Update();
            if (Destination != null)
            {
                position.X += VitesseX;
                position.Y += VitesseY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    derniereCase = ActualCase;
                    ActualCase = Destination;
                    Destination = MouvementIA(ActualCase);
                }
            }
        }

        private Case MouvementIA(Case _actualCase)
        {
            return etatPresent.Mouvement(_actualCase);
        }

        public override bool EstMort()
        {
            return false;
        }
        public override void ToucherAutrePersonnage()
        {

        }
    }
}
