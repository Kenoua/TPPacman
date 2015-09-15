using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    /// <summary>
    /// Classe servant de modèle pour tous les autres
    /// personnages du jeu.
    /// </summary>
    public abstract class Personnage
    {
        private const int VITESSE = 4;

        protected Texture2D dessin;
        protected Vector2 position;
        protected bool mort;
        protected int pointsVie;
        protected DateTime dernierContact;
        protected TimeSpan delaiProchainContact;
        protected int directionArriere;
        public List<Case> caseSnorlax;
        public Color spriteColorEffect;

        /// <summary>
        /// TToutes les propriétés utilisés pour la classe Personnage
        /// </summary>
        public Case derniereCase;
        public int PointsVie { get { return pointsVie; } }
        public int DirectionArriere { get; set; }
        public Case ActualCase { get; set; }
        public Case Destination { get; set; }
        public int VitesseX { get; set; }
        public int VitesseY { get; set; }
        public int Vitesse { get { return VITESSE; }}

        /// <summary>
        /// Initializes a new instance of the <see cref="Personnage"/> class.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        /// <param name="position">The position.</param>
        /// <param name="actualCase">The actual case.</param>
        public Personnage(Texture2D sprite, Vector2 position, Case actualCase)
        {
            mort = false;
            VitesseX = 0;
            VitesseY = 0;
            caseSnorlax = new List<Case>();

            dessin = sprite;
            this.position = position;
            ActualCase = actualCase;
            spriteColorEffect = Color.White;
        }

        /// <summary>
        /// Mouvements this instance.
        /// </summary>
        public abstract void Mouvement();

        /// <summary>
        /// Touchers the autre personnage.
        /// </summary>
        public abstract void ToucherAutrePersonnage();

        /// <summary>
        /// Ests the mort.
        /// </summary>
        /// <returns></returns>
        public abstract bool EstMort();

        /// <summary>
        /// Draws the specified spritebatch.
        /// </summary>
        /// <param name="spritebatch">The spritebatch.</param>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(dessin, position, spriteColorEffect);
        }
    }
}
