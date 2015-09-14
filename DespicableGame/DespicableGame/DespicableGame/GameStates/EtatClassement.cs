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
    class EtatClassement : EtatJeu
    {
        protected ContentManager content;
        protected InputHandler input;
        private bool exit = false;
        private List<Score> scores;


        /// <summary>
        /// Loads the content.
        /// @see GetScores
        /// @see ArrangeTopList
        /// </summary>
        /// <param name="_content">The _content.</param>
        public void LoadContent(ContentManager _content)
        {
            content = _content;
            scores = new List<Score>();
            input = DespicableGame.input;
            XMLScoreReader reader = new XMLScoreReader();
            reader.Load("Scores.xml");
            scores = reader.GetScores();
            arrangeTopList();
        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Arranges the list so the scores are written from best to worse
        /// using bubble sorting.
        /// </summary>
        public void arrangeTopList()
        {

            Score temp;
            for (int i = 0; i < scores.Count; i++)
            {
                for (int j = 0; j < scores.Count - 1; j++)
                {
                    if (scores[j].score < scores[j + 1].score)
                    {
                        temp = scores[j + 1];
                        scores[j + 1] = scores[j];
                        scores[j] = temp;
                    }
                }
            }
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
        }

        /// <summary>
        /// Draws the specified _sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The _sprite batch.</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), "Joueur", new Vector2(300, 0), Color.White);
            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), "Pointage", new Vector2(700, 0), Color.White);

            for (int i = 0; i < scores.Count; i++)
            {
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), scores[i].name, new Vector2(300, 100 + 100 * i), Color.White);
                _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\MainFont"), scores[i].score.ToString(), new Vector2(700, 100 + 100 * i), Color.White);
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
    }
}

