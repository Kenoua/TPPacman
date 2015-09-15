using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;


namespace DespicableGame.GameStates
{
    /// <summary>
    /// État du jeu qui définit un menu dans la fenêtre de jeu.
    /// </summary>
    class EtatMenu : EtatJeu
    {
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private readonly int NB_OPTION = 3;
        private int optionSelectionner = 0;
        private string[] textesMenu;
       

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        public void LoadContent(ContentManager _content)
        {
            content = _content;
            textesMenu = new string[NB_OPTION];
            textesMenu[0] = "Play";
            textesMenu[1] = "Classement";
            textesMenu[2] = "Exit";
            input = DespicableGame.input;

      
            
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Handles the input.
        /// </summary>
        public void HandleInput()
        {
            if (input.IsGamePadOneConnected())
            {
                HandleGamePadInput();
            }
            else
            {
                HandleKeyboardInput();
            }

            if (optionSelectionner < 0)
            {
                optionSelectionner = NB_OPTION - 1;
            }
            if (optionSelectionner >= NB_OPTION)
            {
                optionSelectionner = 0;
            }


        }
        /// <summary>
        /// Handles the keyboard input.
        /// </summary>
        private void HandleKeyboardInput()
        {
            if (input.IsInputPressed(Keys.Escape))
                exit = true;

            if (input.IsInputPressed(Keys.W))
            {
                optionSelectionner--;

            }
            if (input.IsInputPressed(Keys.S))
            {
                optionSelectionner++;
            }

            if (input.IsInputPressed(Keys.Space))
            {
                ChoisirOption();
            }
        }
        /// <summary>
        /// Handles the game pad input.
        /// </summary>
        private void HandleGamePadInput()
        {
            if (input.IsInputPressed(Buttons.Back))
                exit = true;

            if (input.IsThumbStickDown(InputHandler.GamePadThumbSticksSide.LEFT, -0.5f))
            {
                optionSelectionner++;
            }
            if (input.IsThumbStickUp(InputHandler.GamePadThumbSticksSide.LEFT, 0.5f))
            {
                optionSelectionner--;
            }
            if (input.IsInputPressed(Buttons.A))
            {
                ChoisirOption();
            }
        }

        /// <summary>
        /// Choisirs the option.
        /// </summary>
        private void ChoisirOption()
        {
            if (optionSelectionner == 0)
            {
                DespicableGame.etatDeJeu = new EtatPartieEnCours();
                DespicableGame.etatDeJeu.LoadContent(content);
            }

            if (optionSelectionner == 1)
            {
                DespicableGame.etatDeJeu = new EtatClassement();
                DespicableGame.etatDeJeu.LoadContent(content);
            }

            if (optionSelectionner == 2)
            {
                exit = true;
            }
        }
        /// <summary>
        /// Determines whether this instance has exited.
        /// </summary>
        /// <returns></returns>
        public bool HasExited()
        {
            return exit;
        }

        /// <summary>
        /// Draws the specified _sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The _sprite batch.</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(content.Load<Texture2D>("Sprites\\titleScreen"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            Color couleurTexte;

            for (int i = 0; i < NB_OPTION; i++)
            {
                couleurTexte = Color.Gray;
                if (optionSelectionner == i)
                    couleurTexte = Color.Blue;
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), textesMenu[i], new Vector2(800, 400 + 100 * i), couleurTexte);
            }
        }
    }
}
