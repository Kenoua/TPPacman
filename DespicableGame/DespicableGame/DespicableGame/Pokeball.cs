using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public class Pokeball : Objets
    {
        public BadgeType pokeType;

        public Pokeball(BadgeType _pokeType, Texture2D sprite, Vector2 position, Case actualCase)
            : base(sprite, position, actualCase)
        {
            pokeType = _pokeType;
        }

        public override void Rammasser()
        {
            position = new Vector2(1100, 100 + (50 * (int)(pokeType)));
            ActualCase = null;
        }
    }
}
