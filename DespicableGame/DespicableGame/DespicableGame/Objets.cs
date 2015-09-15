using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    /// <summary>
    /// Classe abstraite servant de modèle pour les autres
    /// objets du jeu.
    /// </summary>
    public abstract class Objets
    {
        protected Texture2D dessin;
        public Vector2 position;
        public Case ActualCase { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Objets"/> class.
        /// </summary>
        /// <param name="sprite">The sprite.</param>
        /// <param name="position">The position.</param>
        /// <param name="actualCase">The actual case.</param>
        public Objets(Texture2D sprite, Vector2 position, Case actualCase)
        {
            dessin = sprite;
            this.position = position;
            ActualCase = actualCase;
        }

        /// <summary>
        /// Rammassers this instance.
        /// </summary>
        public abstract void Rammasser();

        /// <summary>
        /// Draws the specified spritebatch.
        /// </summary>
        /// <param name="spritebatch">The spritebatch.</param>
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(dessin, position, Color.White);
        }
    }
}
