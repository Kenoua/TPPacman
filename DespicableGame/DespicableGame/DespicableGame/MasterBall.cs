using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    class MasterBall : Objets
    {
        public MasterBall(Texture2D sprite, Vector2 position, Case actualCase)
            : base(sprite, position, actualCase)
        {
            
        }
        public override void Rammasser()
        {
            throw new NotImplementedException();
        }
    }
}
