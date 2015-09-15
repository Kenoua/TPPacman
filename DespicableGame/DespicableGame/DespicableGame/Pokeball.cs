using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    /// <summary>
    /// Classe qui définit une pokeball comme étant
    /// un objet du jeu pouvant être ramassé.
    /// </summary>
    public class Pokeball : Objets
    {
        public BadgeType pokeType;
        public Color pokeColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pokeball"/> class.
        /// </summary>
        /// <param name="_pokeType">Type of the _poke.</param>
        /// <param name="sprite">The sprite.</param>
        /// <param name="position">The position.</param>
        /// <param name="actualCase">The actual case.</param>
        public Pokeball(BadgeType _pokeType, Texture2D sprite, Vector2 position, Case actualCase)
            : base(sprite, position, actualCase)
        {
            pokeType = _pokeType;
            switch (_pokeType)
            {
                case BadgeType.Boulder:
                    pokeColor = Color.Gray;
                    break;
                case BadgeType.Cascade:
                    pokeColor = Color.Blue;
                    break;
                case BadgeType.Lightning:
                    pokeColor = Color.Yellow;
                    break;
                case BadgeType.March:
                    pokeColor = Color.Salmon;
                    break;
                case BadgeType.Rainbow:
                    pokeColor = Color.Lime;
                    break;
                case BadgeType.Soul:
                    pokeColor = Color.Magenta;
                    break;
                case BadgeType.Volcano:
                    pokeColor = Color.Orange;
                    break;
                case BadgeType.Earth:
                    pokeColor = Color.SandyBrown;
                    break;
            }
        }

        /// <summary>
        /// Rammassers this instance.
        /// </summary>
        public override void Rammasser()
        {
            Pointage.GetInstance().AjouterPoints(50);
            position = new Vector2(1100, 100 + (50 * (int)(pokeType)));
            ActualCase = null;
        }

        /// <summary>
        /// Draws the specified spritebatch.
        /// </summary>
        /// <param name="spritebatch">The spritebatch.</param>
        new public void Draw(SpriteBatch spritebatch)
        {
            
            spritebatch.Draw(dessin, position,pokeColor);
        }
    }
}
