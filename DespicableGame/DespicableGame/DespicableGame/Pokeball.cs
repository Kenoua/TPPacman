using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public enum PokeType { Roche, Eau, Electrique, Herbe, Poison, Psy, Feu, Terre };

    class Pokeball : Objets
    {
        public PokeType pokeType;
        public Pokeball(PokeType pokeType, Texture2D sprite, Vector2 position, Case actualCase)
            : base(sprite, position, actualCase)
        {
            this.pokeType = pokeType;
        }

        public override void Rammasser()
        {
            throw new NotImplementedException();
        }
    }
}
