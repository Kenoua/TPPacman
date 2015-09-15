using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace DespicableGame.GameStates
{
    /// <summary>
    /// État du jeu qui permet de mettre une partie en pause.
    /// </summary>
    class EtatPause : EtatJeu
    {

        private EtatPartieEnCours dernierePartieEnCours;
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
            textesMenu[0] = "Resume";
            textesMenu[1] = "Retour au menu";
            textesMenu[2] = "Quitter";
            input = DespicableGame.input;
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Sets the partie inachever.
        /// </summary>
        /// <param name="_etatPartieEnCours">The _etat partie en cours.</param>
        public void setPartieInachever(EtatPartieEnCours _etatPartieEnCours)
        {
            dernierePartieEnCours = _etatPartieEnCours;
        }

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
                choisirOption();
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
                choisirOption();
            }
        }

        /// <summary>
        /// Choisirs the option.
        /// </summary>
        private void choisirOption()
        {
            if (optionSelectionner == 0)
            {
                DespicableGame.etatDeJeu = dernierePartieEnCours;
            }

            if (optionSelectionner == 1)
            {
                LevelLoader.Recommencer();
                DespicableGame.etatDeJeu = new EtatMenu();
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
            
            dernierePartieEnCours.Draw(_spriteBatch);
            Color couleurTexte;
         
            for (int i = 0; i < NB_OPTION; i++)
            {
                couleurTexte = Color.Gray;
                if (optionSelectionner == i)
                    couleurTexte = Color.Blue;
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), textesMenu[i], new Vector2(500, 300 + 100 * i), couleurTexte);
            }
        }
    }
}
