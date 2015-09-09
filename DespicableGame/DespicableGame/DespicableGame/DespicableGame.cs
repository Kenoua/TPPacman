using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DespicableGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class DespicableGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;
        
        PersonnageJoueur Gru;

        PersonnageNonJoueur Police;

        Texture2D murHorizontal;
        Texture2D murVertical;

        Texture2D warpEntree;
        Vector2 warpEntreePos;

        Texture2D[] warpSorties = new Texture2D[4];
        Vector2[] warpSortiesPos = new Vector2[4];

        //VITESSE doit être un diviseur entier de 64
        public const int VITESSE = 4;

        //Position de départ de Gru
        public const int DEPART_X = 6;
        public const int DEPART_Y = 7;

        private Labyrinthe labyrinthe;


        public DespicableGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            labyrinthe = new Labyrinthe();
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            InitGraphicsMode(SCREENWIDTH, SCREENHEIGHT, false);
            base.Initialize();
        }

        private bool InitGraphicsMode(int width, int height, bool fullScreen)
        {
            // If we aren't using a full screen mode, the height and width of the window can
            // be set to anything equal to or smaller than the actual screen size.
            if (fullScreen == false)
            {
                if ((width <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)
                    && (height <= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height))
                {
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                }
            }
            else
            {
                // If we are using full screen mode, we should check to make sure that the display
                // adapter can handle the video mode we are trying to set.  To do this, we will
                // iterate thorugh the display modes supported by the adapter and check them against
                // the mode we want to set.
                foreach (DisplayMode dm in GraphicsAdapter.DefaultAdapter.SupportedDisplayModes)
                {
                    // Check the width and height of each mode against the passed values
                    //if ((dm.Width == width) && (dm.Height == height))
                    //{
                    // The mode is supported, so set the buffer formats, apply changes and return
                    graphics.PreferredBackBufferWidth = width;
                    graphics.PreferredBackBufferHeight = height;
                    graphics.IsFullScreen = fullScreen;
                    graphics.ApplyChanges();
                    return true;
                    //}
                }
            }
            return false;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            murHorizontal = Content.Load<Texture2D>("Sprites\\Hwall");
            murVertical = Content.Load<Texture2D>("Sprites\\Vwall");

            // TODO: use this.Content to load your game content here
            Gru = new PersonnageJoueur
                (
                Content.Load<Texture2D>("Sprites\\Gru"),
                new Vector2(labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().X, labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().Y),
                labyrinthe.GetCase(DEPART_X, DEPART_Y)
                );


            Police = new PersonnageNonJoueur
                (
                Content.Load<Texture2D>("Sprites\\Police"),
                new Vector2(labyrinthe.GetCase(7, 9).GetPosition().X, labyrinthe.GetCase(7, 9).GetPosition().Y),
                labyrinthe.GetCase(7, 9)
                );

            //L'entrée du téléporteur
            warpEntree = Content.Load<Texture2D>("Sprites\\Warp1");
            warpEntreePos = new Vector2(labyrinthe.GetCase(7, 4).GetPosition().X - Case.TAILLE_LIGNE, labyrinthe.GetCase(7, 4).GetPosition().Y + Case.TAILLE_LIGNE);

            //Les sorties du téléporteur
            for (int i = 0; i < warpSorties.Length; i++)
            {
                warpSorties[i] = Content.Load<Texture2D>("Sprites\\Warp2");
            }

            warpSortiesPos[0] = new Vector2(labyrinthe.GetCase(0, 0).GetPosition().X, labyrinthe.GetCase(0, 0).GetPosition().Y);
            warpSortiesPos[1] = new Vector2(labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, 0).GetPosition().X, labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, 0).GetPosition().Y);
            warpSortiesPos[2] = new Vector2(labyrinthe.GetCase(0, Labyrinthe.HAUTEUR - 1).GetPosition().X, labyrinthe.GetCase(0, Labyrinthe.HAUTEUR - 1).GetPosition().Y);
            warpSortiesPos[3] = new Vector2(labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, Labyrinthe.HAUTEUR - 1).GetPosition().X, labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, Labyrinthe.HAUTEUR - 1).GetPosition().Y);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            GamePadState padOneState = GamePad.GetState(PlayerIndex.One);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) || padOneState.Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (Gru.Destination == null)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || padOneState.DPad.Up == ButtonState.Pressed)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseHaut, 0, -VITESSE);
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Down) || padOneState.DPad.Down == ButtonState.Pressed)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseBas, 0, VITESSE);
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Left) || padOneState.DPad.Left == ButtonState.Pressed)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseGauche, -VITESSE, 0);
                }

                else if (Keyboard.GetState().IsKeyDown(Keys.Right) || padOneState.DPad.Right == ButtonState.Pressed)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseDroite, VITESSE, 0);
                }
            }  

            // TODO: Add your update logic here
            Gru.Mouvement();
            Police.Mouvement();
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            //Draw de chacune des cases
            for (int i = 0; i < Labyrinthe.LARGEUR; i++)
            {
                for (int j = 0; j < Labyrinthe.HAUTEUR; j++)
                {
                    labyrinthe.GetCase(i, j).DessinerMurs(spriteBatch, murHorizontal, murVertical);
                }
            }

            //Draw du cadre extérieur
            labyrinthe.DessinerHorizontal(spriteBatch, murHorizontal);
            labyrinthe.DessinerVertical(spriteBatch, murVertical);

            //Draw de l'entrée du téléporteur
            spriteBatch.Draw(warpEntree, warpEntreePos, Color.White);

            //Draw des sorties du téléporteur
            for (int i = 0; i < 4; i++)
            {
                spriteBatch.Draw(warpSorties[i], warpSortiesPos[i], Color.White);
            }

            //Draw de la Police
            Police.Draw(spriteBatch);

            //Draw de Gru
            Gru.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
