using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DespicableGame.GameStates
{
    public interface EtatJeu 
    {
        void LoadContent(ContentManager _content);

        void Update();

        void HandleInput();

        void Draw(SpriteBatch _spriteBatch);

        bool HasExited();
    }
}
