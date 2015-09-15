using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
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

        public Case derniereCase;
        public int PointsVie { get { return pointsVie; } }
        public int DirectionArriere { get; set; }
        public Case ActualCase { get; set; }
        public Case Destination { get; set; }
        public int VitesseX { get; set; }
        public int VitesseY { get; set; }
        public int Vitesse { get { return VITESSE; }}

        public Personnage(Texture2D sprite, Vector2 position, Case actualCase)
        {
            mort = false;
            VitesseX = 0;
            VitesseY = 0;
            caseSnorlax = new List<Case>();

            dessin = sprite;
            this.position = position;
            ActualCase = actualCase;
        }

        public abstract void Mouvement();

        public abstract void ToucherAutrePersonnage();

        public abstract bool EstMort();

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(dessin, position, Color.White);
        }
    }
}
