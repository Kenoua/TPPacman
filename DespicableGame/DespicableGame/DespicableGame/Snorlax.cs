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
        public SnorlaxStates.EtatSnorlax etatPresent;
        public List<Case> listeCasesRocket;

        public Snorlax(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            listeCasesRocket = new List<Case>();
            etatPresent = new SnorlaxStates.EtatSommeil(this);

        }

        public void Update()
        {
            etatPresent.Update();
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

        public void ChangerEtat(SnorlaxStates.EtatSnorlax nouvelEtat)
        {
            etatPresent = nouvelEtat;
        }

        public void setCasesRocket(List<Case> _casesRocket)
        {
            listeCasesRocket = _casesRocket;
        }
    }
}
