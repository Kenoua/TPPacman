using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DespicableGame.GameStates
{
    /// <summary>
    /// Interface qui définit les différents états d'une fenêtre de jeu.
    /// </summary>
    public interface EtatJeu 
    {
        void LoadContent(ContentManager _content);

        void Update();

        void HandleInput();

        void Draw(SpriteBatch _spriteBatch);

        bool HasExited();
    }
}
