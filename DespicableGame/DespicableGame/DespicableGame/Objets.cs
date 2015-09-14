using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    public abstract class Objets
    {
        protected Texture2D dessin;
        public Vector2 position;
        public Case ActualCase { get; set; }

        public Objets(Texture2D sprite, Vector2 position, Case actualCase)
        {
            dessin = sprite;
            this.position = position;
            ActualCase = actualCase;
        }

        public abstract void Rammasser();

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(dessin, position, Color.White);
        }
    }
}
