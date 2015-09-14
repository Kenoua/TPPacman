using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    public enum BadgeType {Boulder, Cascade, Lightning, Rainbow, Soul, March, Volcano, Earth};
    
    public class Badge : Objets
    {
        public BadgeType badgeType;
        public Badge(BadgeType badgeType, Texture2D sprite, Vector2 position, Case actualCase)
            : base(sprite, position, actualCase)
        {
            this.badgeType = badgeType;
        }
        public override void Rammasser()
        {
            Pointage.GetInstance().AjouterPoints(100);
            position = new Vector2(1000,100+(50*(int)(badgeType)));
            ActualCase = null;
            
        }
    }
}
