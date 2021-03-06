﻿using System;
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
    /// État du jeu qui permet de sauvegarder son score 
    /// après avoir fini une partie
    /// </summary>
    class EtatSauvegarderScore : EtatJeu
    {
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private string score;
        private string name;
        private bool partiePerdu = false;
        private bool partieGagner = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveScoreMenu"/> class.
        /// </summary>
        /// <param name="_score">The _score.</param>
        public EtatSauvegarderScore(string _score)
        {
            score = _score;
            name = "";
        }

        /// <summary>
        /// Loads the content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        public void LoadContent(ContentManager _content)
        {
            content = _content;
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
            if (input.IsInputPressed(Keys.Escape) || input.IsInputPressed(Buttons.Back))
            {
                DespicableGame.etatDeJeu = new EtatMenu();
                DespicableGame.etatDeJeu.LoadContent(content);
            }

            if (input.IsInputPressed(Keys.Enter))
            {
                XMLScoreWriter writer = new XMLScoreWriter();
                writer.WriteXML(name, score);
                DespicableGame.etatDeJeu = new EtatMenu();
                DespicableGame.etatDeJeu.LoadContent(content);
            }

            foreach (Keys key in input.GetPressedKeys())
            {
                if (key == Keys.Back)
                {
                    if (name.Length > 0)
                    {
                        name = name.Substring(0, name.Length - 1);
                    }
                }
                else if (name.Length <= 10)
                {
                    if (key.ToString().Length == 1)
                    {
                        name += key.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// Parties the perdu.
        /// </summary>
        public void PartiePerdu()
        {
            partiePerdu = true;
        }

        /// <summary>
        /// Parties the gagner.
        /// </summary>
        public void PartieGagner()
        {
            partieGagner = true;
        }

        /// <summary>
        /// Draws the specified _sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The _sprite batch.</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (partiePerdu)
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), "Game Over", new Vector2(550, 290), Color.Red);

            if (partieGagner)
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), "Congratulations! You are now a Pokemon Master!", new Vector2(100, 290), Color.Gold);

            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), name, new Vector2(300, 400), Color.White);
            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), score, new Vector2(700, 400), Color.White);
        }

        /// <summary>
        /// Determines whether this instance has exited.
        /// </summary>
        /// <returns></returns>
        public bool HasExited()
        {
            return exit;
        }
    }
}
