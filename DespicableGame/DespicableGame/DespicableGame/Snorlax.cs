using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    /// <summary>
    /// Classe qui définit un Snorlax comme étant un personnage.
    /// </summary>
    public class Snorlax : Personnage
    {
        public SnorlaxStates.EtatSnorlax etatPresent;
        public List<Case> listeCasesRocket;

        /// <summary>
        /// Initializes a new instance of the <see cref="Snorlax"/> class.
        /// </summary>
        /// <param name="dessin">The dessin.</param>
        /// <param name="position">The position.</param>
        /// <param name="ActualCase">The actual case.</param>
        public Snorlax(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            listeCasesRocket = new List<Case>();
            etatPresent = new SnorlaxStates.EtatSommeil(this);

        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {
            etatPresent.Update();
        }

        /// <summary>
        /// Mouvements this instance.
        /// </summary>
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

        /// <summary>
        /// Mouvements the ia.
        /// </summary>
        /// <param name="_actualCase">The _actual case.</param>
        /// <returns></returns>
        private Case MouvementIA(Case _actualCase)
        {
            return etatPresent.Mouvement(_actualCase);
        }

        /// <summary>
        /// Ests the mort.
        /// </summary>
        /// <returns></returns>
        public override bool EstMort()
        {
            return false;
        }
        /// <summary>
        /// Touchers the autre personnage.
        /// </summary>
        public override void ToucherAutrePersonnage()
        {

        }

        /// <summary>
        /// Changers the etat.
        /// </summary>
        /// <param name="nouvelEtat">The nouvel etat.</param>
        public void ChangerEtat(SnorlaxStates.EtatSnorlax nouvelEtat)
        {
            etatPresent = nouvelEtat;
        }

        /// <summary>
        /// Sets the cases rocket.
        /// </summary>
        /// <param name="_casesRocket">The _cases rocket.</param>
        public void SetCasesRocket(List<Case> _casesRocket)
        {
            listeCasesRocket = _casesRocket;
        }
    }
}
