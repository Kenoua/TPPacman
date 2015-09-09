using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    abstract class Personnage
    {
        protected Texture2D dessin;
        protected Vector2 position;
        public Case ActualCase { get; set; }
        public Case Destination { get; set; }
        public int VitesseX { get; set; }
        public int VitesseY { get; set; }

        public Personnage(Texture2D sprite, Vector2 position, Case actualCase)
        {
            VitesseX = 0;
            VitesseY = 0;

            dessin = sprite;
            this.position = position;
            ActualCase = actualCase;
        }

        public abstract void Mouvement();

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(dessin, position, Color.White);
        }
    }
}
