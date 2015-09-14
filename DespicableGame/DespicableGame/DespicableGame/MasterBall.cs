using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public class MasterBall : Objets
    {
        public MasterBall(Texture2D sprite, Vector2 position, Case actualCase)
            : base(sprite, position, actualCase)
        {
            
        }
        public override void Rammasser()
        {
            position = new Vector2(1200, 100);
            ActualCase = null;
        }

        public void Draw(SpriteBatch spritebatch,int _position)
        {

            spritebatch.Draw(dessin, new Vector2(position.X,position.Y+(_position* 50)),Color.White);
        }
        
    }
}
