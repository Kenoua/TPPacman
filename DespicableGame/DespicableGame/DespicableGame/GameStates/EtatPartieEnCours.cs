﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DespicableGame.GameStates
{
    public class EtatPartieEnCours : EtatJeu
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHandler input;
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

        private bool exit = false;
        private ContentManager content;

        public void LoadContent(ContentManager _content)
        {
            labyrinthe = new Labyrinthe();
            content = _content;
            input = DespicableGame.input;

            murHorizontal = content.Load<Texture2D>("Sprites\\Hwall");
            murVertical = content.Load<Texture2D>("Sprites\\Vwall");

            // TODO: use this.Content to load your game content here
            Gru = new PersonnageJoueur
                (
                content.Load<Texture2D>("Sprites\\Gru"),
                new Vector2(labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().X, labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().Y),
                labyrinthe.GetCase(DEPART_X, DEPART_Y)
                );


            Police = new PersonnageNonJoueur
                (
                content.Load<Texture2D>("Sprites\\Police"),
                new Vector2(labyrinthe.GetCase(7, 9).GetPosition().X, labyrinthe.GetCase(7, 9).GetPosition().Y),
                labyrinthe.GetCase(7, 9)
                );

            //L'entrée du téléporteur
            warpEntree = content.Load<Texture2D>("Sprites\\Warp1");
            warpEntreePos = new Vector2(labyrinthe.GetCase(7, 4).GetPosition().X - Case.TAILLE_LIGNE, labyrinthe.GetCase(7, 4).GetPosition().Y + Case.TAILLE_LIGNE);

            //Les sorties du téléporteur
            for (int i = 0; i < warpSorties.Length; i++)
            {
                warpSorties[i] = content.Load<Texture2D>("Sprites\\Warp2");
            }

            warpSortiesPos[0] = new Vector2(labyrinthe.GetCase(0, 0).GetPosition().X, labyrinthe.GetCase(0, 0).GetPosition().Y);
            warpSortiesPos[1] = new Vector2(labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, 0).GetPosition().X, labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, 0).GetPosition().Y);
            warpSortiesPos[2] = new Vector2(labyrinthe.GetCase(0, Labyrinthe.HAUTEUR - 1).GetPosition().X, labyrinthe.GetCase(0, Labyrinthe.HAUTEUR - 1).GetPosition().Y);
            warpSortiesPos[3] = new Vector2(labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, Labyrinthe.HAUTEUR - 1).GetPosition().X, labyrinthe.GetCase(Labyrinthe.LARGEUR - 1, Labyrinthe.HAUTEUR - 1).GetPosition().Y);

        }

        public void Update()
        {
            Gru.Mouvement();
            Police.Mouvement();
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
        }

        private void HandleKeyboardInput()
        {
            if (input.IsInputPressed(Keys.Escape))
                exit = true;

            if (Gru.Destination == null)
            {
                if (input.IsInputDown(Keys.Up))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseHaut, 0, -VITESSE);
                }

                else if (input.IsInputDown(Keys.Down))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseBas, 0, VITESSE);
                }

                else if (input.IsInputDown(Keys.Left))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseGauche, -VITESSE, 0);
                }

                else if (input.IsInputDown(Keys.Right))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseDroite, VITESSE, 0);
                }
            }  
        }

        private void HandleGamePadInput()
        {
            if (input.IsInputPressed(Buttons.Back))
                exit = true;

            if (Gru.Destination == null)
            {
                if (input.GetGamePadJoystick().Left.Y == -1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseHaut, 0, -VITESSE);
                }

                else if (input.GetGamePadJoystick().Left.Y == 1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseBas, 0, VITESSE);
                }

                else if (input.GetGamePadJoystick().Left.X == -1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseGauche, -VITESSE, 0);
                }

                else if (input.GetGamePadJoystick().Left.X == 1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseDroite, VITESSE, 0);
                }
            }  
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            //Draw de chacune des cases
            for (int i = 0; i < Labyrinthe.LARGEUR; i++)
            {
                for (int j = 0; j < Labyrinthe.HAUTEUR; j++)
                {
                    labyrinthe.GetCase(i, j).DessinerMurs(_spriteBatch, murHorizontal, murVertical);
                }
            }

            //Draw du cadre extérieur
            labyrinthe.DessinerHorizontal(_spriteBatch, murHorizontal);
            labyrinthe.DessinerVertical(_spriteBatch, murVertical);

            //Draw de l'entrée du téléporteur
            _spriteBatch.Draw(warpEntree, warpEntreePos, Color.White);

            //Draw des sorties du téléporteur
            for (int i = 0; i < 4; i++)
            {
                _spriteBatch.Draw(warpSorties[i], warpSortiesPos[i], Color.White);
            }

            //Draw de la Police
            Police.Draw(_spriteBatch);

            //Draw de Gru
            Gru.Draw(_spriteBatch);
        }

        public bool HasExited()
        {
            return exit;
        }
    }
}