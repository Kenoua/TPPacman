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

namespace DespicableGame.GameStates
{
    public class EtatPartieEnCours : EtatJeu
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        InputHandler input;

        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;

        public static PersonnageJoueur Gru;

        List<Badge> listeBadges;

        List<PersonnageNonJoueur> Polices;

        Texture2D murHorizontal;
        Texture2D murVertical;

        Texture2D warpEntree;
        Vector2 warpEntreePos;

        Texture2D[] warpSorties = new Texture2D[4];
        Vector2[] warpSortiesPos = new Vector2[4];

        Texture2D[] badgesTextures = new Texture2D[8];

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

            if (LevelLoader.AugementerLevel(1) == 8) 
            {
                DespicableGame.etatDeJeu = new EtatMenu();
                ((EtatMenu)DespicableGame.etatDeJeu).PartieGagner();
                DespicableGame.etatDeJeu.LoadContent(content);
            }

            LevelLoader.SetContent(content, labyrinthe);

            input = DespicableGame.input;

            murHorizontal = content.Load<Texture2D>("Sprites\\Hwall");
            murVertical = content.Load<Texture2D>("Sprites\\Vwall");

            // TODO: use this.Content to load your game content here
            Gru = new PersonnageJoueur
                (
                content.Load<Texture2D>("Sprites\\RedPlayer"),
                new Vector2(labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().X, labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().Y),
                labyrinthe.GetCase(DEPART_X, DEPART_Y)
                );


            Polices = LevelLoader.ChargerEnnemis();
                

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

            //Les objets, Badges/Pokéballs/MasterBalls
            listeBadges = LevelLoader.ChargerBadges();
        }

        public void Update()
        {

            if (!Gru.EstMort())
            {
                Gru.Mouvement();
                foreach(PersonnageNonJoueur police in Polices)
                {
                    police.Update();
                    police.Mouvement();
                    if (Gru.ActualCase == police.ActualCase)
                    {
                        Gru.ToucherAutrePersonnage();
                    }
                }
            }
            else
            {
                LevelLoader.Recommencer();
                DespicableGame.etatDeJeu = new EtatMenu();
                ((EtatMenu)DespicableGame.etatDeJeu).PartiePerdu();
                DespicableGame.etatDeJeu.LoadContent(content);
            }
            updateObjets();
        }
        
        public void updateObjets()
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
        }

        private void HandleKeyboardInput()
        {
            if (input.IsInputPressed(Keys.P))
            {
                PausePartie();
            }
            else if (Gru.Destination == null)
            {
                if (input.IsInputDown(InputHandler.touchesClavier[4]))
                {
                    //Attaquer
                }
                else if (input.IsInputDown(InputHandler.touchesClavier[0]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseHaut, 0, -VITESSE);
                }

                else if (input.IsInputDown(InputHandler.touchesClavier[1]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseBas, 0, VITESSE);
                }

                else if (input.IsInputDown(InputHandler.touchesClavier[2]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseGauche, -VITESSE, 0);
                }

                else if (input.IsInputDown(InputHandler.touchesClavier[3]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseDroite, VITESSE, 0);
                }
            }
        }

        private void HandleGamePadInput()
        {
            if (input.IsInputPressed(Buttons.Back))
            {
                PausePartie();
            }
            if (Gru.Destination == null)
            {
                if(input.IsInputDown(InputHandler.boutonGamePad))
                {
                    //Attaquer
                }
                else if (input.GetGamePadJoystick().Left.Y == 1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseHaut, 0, -VITESSE);
                }

                else if (input.GetGamePadJoystick().Left.Y == -1)
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

        private void PausePartie()
        {
            DespicableGame.etatDeJeu = new EtatPause();
            DespicableGame.etatDeJeu.LoadContent(content);
            ((EtatPause)DespicableGame.etatDeJeu).setPartieInachever(this);
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
            //Draw des objets
            foreach (Objets O in listeBadges)
            {
                O.Draw(_spriteBatch);
            }
            //Draw de la Police
            foreach(PersonnageNonJoueur police in Polices)
            {
                police.Draw(_spriteBatch);
            }

            //Draw de Gru
            if (!Gru.EstMort())
                Gru.Draw(_spriteBatch);
        }

        public bool HasExited()
        {
            return exit;
        }
    }
}
