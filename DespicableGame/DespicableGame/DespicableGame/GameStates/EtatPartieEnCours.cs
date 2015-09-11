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

        PersonnageJoueur Gru;
        List<Objets> listeObjets;
        PersonnageNonJoueur Police;

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

            input = DespicableGame.input;

            murHorizontal = content.Load<Texture2D>("Sprites\\Hwall");
            murVertical = content.Load<Texture2D>("Sprites\\Vwall");

            badgesTextures[0] = content.Load<Texture2D>("Sprites\\Badge1");
            badgesTextures[1] = content.Load<Texture2D>("Sprites\\badge2");
            badgesTextures[2] = content.Load<Texture2D>("Sprites\\badge3");
            badgesTextures[3] = content.Load<Texture2D>("Sprites\\badge4");
            badgesTextures[4] = content.Load<Texture2D>("Sprites\\badge5");
            badgesTextures[5] = content.Load<Texture2D>("Sprites\\badge6");
            badgesTextures[6] = content.Load<Texture2D>("Sprites\\badge7");
            badgesTextures[7] = content.Load<Texture2D>("Sprites\\badge8");

            // TODO: use this.Content to load your game content here
            Gru = new PersonnageJoueur
                (
                content.Load<Texture2D>("Sprites\\RedPlayer"),
                new Vector2(labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().X, labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().Y),
                labyrinthe.GetCase(DEPART_X, DEPART_Y)
                );


            Police = new PersonnageNonJoueur
                (
                content.Load<Texture2D>("Sprites\\RocketGrunt"),
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

            //Les objets, Badges/Pokéballs/MasterBalls
            listeObjets = new List<Objets>();
        }

        public void Update()
        {

            if (!Gru.EstMort())
            {
                Gru.Mouvement();
                Police.Mouvement();

                if (Gru.ActualCase == Police.ActualCase)
                {
                    Gru.ToucherAutrePersonnage();
                }
            }
            else
            {
                DespicableGame.etatDeJeu = new EtatMenu();
                ((EtatMenu)DespicableGame.etatDeJeu).PartiePerdu();
                DespicableGame.etatDeJeu.LoadContent(content);
            }

            Gru.Mouvement();
            Police.Mouvement();
            updateObjets();
        }
        
        public void updateObjets()
        {
            if(listeObjets.Count() ==0)
            {
                Badge newBadge = new Badge(BadgeType.Boulder, badgesTextures[0], new Vector2(64*10, 64*10), labyrinthe.GetCase(9,9));
                Badge newBadge1 = new Badge(BadgeType.Cascade, badgesTextures[1], new Vector2(64 * 13, 64 * 4), labyrinthe.GetCase(12, 3));
                Badge newBadge2 = new Badge(BadgeType.Lightning, badgesTextures[2], new Vector2(64 * 3, 64 * 5), labyrinthe.GetCase(2, 4));
                Badge newBadge3 = new Badge(BadgeType.March, badgesTextures[3], new Vector2(64 * 7, 64 * 7), labyrinthe.GetCase(6, 6));
                Badge newBadge4 = new Badge(BadgeType.Rainbow, badgesTextures[4], new Vector2(64 * 5, 64 * 8), labyrinthe.GetCase(4, 7));
                Badge newBadge5 = new Badge(BadgeType.Soul, badgesTextures[5], new Vector2(64 * 11, 64 * 6), labyrinthe.GetCase(10, 5));
                Badge newBadge6 = new Badge(BadgeType.Volcano, badgesTextures[6], new Vector2(64 * 9, 64 * 3), labyrinthe.GetCase(8, 2));
                Badge newBadge7 = new Badge(BadgeType.Earth, badgesTextures[7], new Vector2(64 * 4, 64 * 4), labyrinthe.GetCase(3, 3));

                listeObjets.Add(newBadge);
                listeObjets.Add(newBadge1);
                listeObjets.Add(newBadge2);
                listeObjets.Add(newBadge3);
                listeObjets.Add(newBadge4);
                listeObjets.Add(newBadge5);
                listeObjets.Add(newBadge6);
                listeObjets.Add(newBadge7);
            }
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
            foreach (Objets O in listeObjets)
            {
                O.Draw(_spriteBatch);
            }
            //Draw de la Police
            Police.Draw(_spriteBatch);

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
