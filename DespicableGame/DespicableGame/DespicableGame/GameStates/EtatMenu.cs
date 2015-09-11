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
    class EtatMenu : EtatJeu
    {
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private readonly int NB_OPTION = 2;
        private int optionSelectionner = 0;
        private string[] textesMenu;
        private bool partiePerdu = false;
        private bool partieGagner = false;

        public void LoadContent(ContentManager _content)
        {
            content = _content;
            textesMenu = new string[NB_OPTION];
            textesMenu[0] = "Play";
            textesMenu[1] = "Exit";
            input = DespicableGame.input;
        }

        public void Update()
        {

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

        private void choisirOption()
        {
            if (optionSelectionner == 0)
            {
                DespicableGame.etatDeJeu = new EtatPartieEnCours();
                DespicableGame.etatDeJeu.LoadContent(content);
            }

            if (optionSelectionner == 1)
            {
                exit = true;
            }
        }
        public bool HasExited()
        {
            return exit;
        }

        public void PartiePerdu()
        {
            partiePerdu = true;
        }

        public void PartieGagner()
        {
            partieGagner = true;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(content.Load<Texture2D>("Sprites\\background"), Vector2.Zero, null, Color.White, 0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0f);
            Color couleurTexte;

            if(partiePerdu)
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), "Game Over", new Vector2(550, 100), Color.Tomato);

            if (partieGagner)
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), "Congratulations! You are now a Pokemon Master!", new Vector2(100, 100), Color.Gold);

            for (int i = 0; i < NB_OPTION; i++)
            {
                couleurTexte = Color.White;
                if (optionSelectionner == i)
                    couleurTexte = Color.Blue;
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), textesMenu[i], new Vector2(600, 200 + 100 * i), couleurTexte);
            }
        }
    }
}
