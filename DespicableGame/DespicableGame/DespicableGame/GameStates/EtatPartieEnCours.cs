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
    /// <summary>
    /// État du jeu qui définit une partie dans la fenêtre de jeu.
    /// </summary>
    public class EtatPartieEnCours : EtatJeu
    {
        private InputHandler input;

        public const int SCREENWIDTH = 1280;
        public const int SCREENHEIGHT = 796;

        public static PersonnageJoueur Gru;

        private List<Badge> listeBadges;
        private List<Badge> listeBadgesEnlever;

        private List<Pokeball> listePokeballs;
        private List<Pokeball> listePokeballsEnlever;

        private List<MasterBall> listeMasterballs;
        private List<MasterBall> listeMasterballsEnlever;
        private Vector2 emplacementFinNiveau;

        private List<PersonnageNonJoueur> Polices;

        private Texture2D murHorizontal;
        private Texture2D murVertical;

        private Texture2D warpEntree;
        private Texture2D background;
        private Vector2 warpEntreePos;

        private Texture2D[] warpSorties = new Texture2D[4];
        private Vector2[] warpSortiesPos = new Vector2[4];

        private Labyrinthe labyrinthe;

        private bool exit = false;
        private ContentManager content;

        /// <summary>
        /// Loads the content.
        /// @see ChargerPersonnage
        /// @see ChargerEnnemis
        /// @see ChargerMasterballs
        /// @see ChargerPokeballs
        /// @see ChargerBadges
        /// </summary>
        /// <param name="_content">The _content.</param>
        public void LoadContent(ContentManager _content)
        {
            labyrinthe = new Labyrinthe();
            content = _content;

            if (LevelLoader.AugementerLevel(1) == 9)
            {
                DespicableGame.etatDeJeu = new EtatSauvegarderScore(Pointage.GetInstance().GetTotalPointage().ToString());
                ((EtatSauvegarderScore)DespicableGame.etatDeJeu).PartieGagner();
                DespicableGame.etatDeJeu.LoadContent(content);
                LevelLoader.Recommencer();
            }

            LevelLoader.SetContent(content, labyrinthe);
            emplacementFinNiveau = new Vector2(-1, -1);

            input = DespicableGame.input;

            murHorizontal = content.Load<Texture2D>("Sprites\\Hwall");
            murVertical = content.Load<Texture2D>("Sprites\\Vwall");
            background = content.Load<Texture2D>("Sprites\\background");

            // TODO: use this.Content to load your game content here
            Gru = LevelLoader.ChargerPersonnage();


            Polices = LevelLoader.ChargerEnnemis();


            //L'entrée du téléporteur
            warpEntree = content.Load<Texture2D>("Sprites\\Pigeot");
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
            listeBadgesEnlever = new List<Badge>();

            listePokeballs = LevelLoader.ChargerPokeballs();
            listePokeballsEnlever = new List<Pokeball>();

            listeMasterballs = LevelLoader.ChargerMasterballs();
            listeMasterballsEnlever = new List<MasterBall>();
        }

        /// <summary>
        /// Updates this instance.
        /// @see updateObjets
        /// @see Mouvement
        /// </summary>
        public void Update()
        {
            if (!estNiveauTerminer())
            {
                if (!Gru.EstMort())
                {
                    Gru.Mouvement();
                    foreach (PersonnageNonJoueur police in Polices)
                    {
                        police.Mouvement();
                        if (Gru.ActualCase == police.ActualCase)
                        {
                            Gru.ToucherAutrePersonnage();
                        }
                    }
                }
                else
                {
                    DespicableGame.etatDeJeu = new EtatSauvegarderScore(Pointage.GetInstance().GetTotalPointage().ToString());
                    ((EtatSauvegarderScore)DespicableGame.etatDeJeu).PartiePerdu();
                    DespicableGame.etatDeJeu.LoadContent(content);
                    LevelLoader.Recommencer();
                }
                UpdateObjets();
            }
            else
            {
                Pointage.GetInstance().AjouterPoints(250);
                DespicableGame.etatDeJeu = new EtatPartieEnCours();
                DespicableGame.etatDeJeu.LoadContent(content);
            }
        }

        /// <summary>
        /// Updates the objets.
        /// @see Ramasser
        /// </summary>
        public void UpdateObjets()
        {
            foreach (Pokeball P in listePokeballs)
            {
                if(P.ActualCase == Gru.ActualCase)
                {
                    P.Rammasser();
                    Gru.pokeballAmasse.Add(P);
                    listePokeballsEnlever.Add(P);
                }
            }

            foreach (MasterBall M in listeMasterballs)
            {
                if (M.ActualCase == Gru.ActualCase)
                {
                    M.Rammasser();
                    
                    Gru.masterBallAmasse.Add(M);
                    listeMasterballsEnlever.Add(M);
                }
            }

            foreach (Badge B in listeBadges)
            {
                if (B.ActualCase == Gru.ActualCase)
                {
                    foreach(Pokeball pokeball in listePokeballsEnlever)
                    {
                        if(pokeball.pokeType == B.badgeType)
                        {
                            B.Rammasser();
                            Gru.badgesAmasse.Add(B);
                            listeBadgesEnlever.Add(B);
                        }
                    }
                }
            }
            foreach (Badge B in listeBadgesEnlever)
            {
                listeBadges.Remove(B);
            }
            foreach (Pokeball P in listePokeballsEnlever)
            {
                listePokeballs.Remove(P);
            }
            foreach (MasterBall M in listeMasterballsEnlever)
            {
                listeMasterballs.Remove(M);
            }
            if (listeBadges.Count == 0 && emplacementFinNiveau == new Vector2(-1,-1))
            {
                emplacementFinNiveau = LevelLoader.ChargerFinNiveau();
            }
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
        }

        /// <summary>
        /// Ests the niveau terminer.
        /// </summary>
        /// <returns></returns>
        private bool estNiveauTerminer()
        {
            if (emplacementFinNiveau.X != -1)
            {
                if (emplacementFinNiveau.X == Gru.ActualCase.OrdreX && emplacementFinNiveau.Y == Gru.ActualCase.OrdreY)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Handles the keyboard input.
        /// </summary>
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
                    if (Gru.masterBallAmasse.Count() > 0 && !Gru.estPokemonLegendaire)
                    {
                        Gru.UtiliseLegendaire(content);
                        Gru.masterBallAmasse.RemoveAt(0);
                             
                    }
                    else if (Gru.derniereCase != null && Gru.snorlaxUsed == null && !Gru.estPokemonLegendaire)
                    {

                        Gru.snorlaxUsed = Gru.derniereCase;
                        foreach(PersonnageNonJoueur PNJ in Polices)
                        {
                            PNJ.caseSnorlax = Gru.snorlaxUsed;
                        }
                        
                    }
                }
                else if (input.IsInputDown(InputHandler.touchesClavier[0]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseHaut, 0, -Gru.Vitesse);
                }

                else if (input.IsInputDown(InputHandler.touchesClavier[1]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseBas, 0, Gru.Vitesse);
                }

                else if (input.IsInputDown(InputHandler.touchesClavier[2]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseGauche, -Gru.Vitesse, 0);
                }

                else if (input.IsInputDown(InputHandler.touchesClavier[3]))
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseDroite, Gru.Vitesse, 0);
                }
            }
        }

        /// <summary>
        /// Handles the game pad input.
        /// </summary>
        private void HandleGamePadInput()
        {
            if (input.IsInputPressed(Buttons.Back))
            {
                PausePartie();
            }
            if (Gru.Destination == null)
            {
                if (input.IsInputDown(InputHandler.boutonGamePad))
                {
                    if (Gru.masterBallAmasse.Count() > 0 && !Gru.estPokemonLegendaire)
                    {
                        Gru.UtiliseLegendaire(content);
                        Gru.masterBallAmasse.RemoveAt(0);

                    }
                    else if (Gru.derniereCase != null && Gru.snorlaxUsed == null && !Gru.estPokemonLegendaire)
                    {

                        Gru.snorlaxUsed = Gru.derniereCase;
                        foreach (PersonnageNonJoueur PNJ in Polices)
                        {
                            PNJ.caseSnorlax = Gru.snorlaxUsed;
                        }

                    }
                }
                else if (input.GetGamePadJoystick().Left.Y == 1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseHaut, 0, -Gru.Vitesse);
                }

                else if (input.GetGamePadJoystick().Left.Y == -1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseBas, 0, Gru.Vitesse);
                }

                else if (input.GetGamePadJoystick().Left.X == -1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseGauche, -Gru.Vitesse, 0);
                }

                else if (input.GetGamePadJoystick().Left.X == 1)
                {
                    Gru.VerifierMouvement(Gru.ActualCase.CaseDroite, Gru.Vitesse, 0);
                }
            }
        }

        /// <summary>
        /// Pauses the partie.
        /// </summary>
        private void PausePartie()
        {
            DespicableGame.etatDeJeu = new EtatPause();
            DespicableGame.etatDeJeu.LoadContent(content);
            ((EtatPause)DespicableGame.etatDeJeu).setPartieInachever(this);
        }

        /// <summary>
        /// Draws the specified _sprite batch.
        /// </summary>
        /// <param name="_spriteBatch">The _sprite batch.</param>
        public void Draw(SpriteBatch _spriteBatch)
        {
            //Draw Background
            _spriteBatch.Draw(background, new Vector2(0,0), Color.White);

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
            _spriteBatch.Draw(warpEntree,new Vector2(warpEntreePos.X - 24,warpEntreePos.Y) , Color.White);

            //Draw des sorties du téléporteur
            for (int i = 0; i < 4; i++)
            {
                _spriteBatch.Draw(warpSorties[i], warpSortiesPos[i], Color.White);
            }
            //Draw des badges
            foreach (Objets O in listeBadges)
            {
                O.Draw(_spriteBatch);
            }
            foreach (Badge B in Gru.badgesAmasse)
            {
                B.Draw(_spriteBatch);
            }
            //Draw des pokeballs/MasterBalls
            foreach (Pokeball P in listePokeballs)
            {
                P.Draw(_spriteBatch);
            }
            foreach (Pokeball P in Gru.pokeballAmasse)
            {
                P.Draw(_spriteBatch);
            }
            foreach (MasterBall M in listeMasterballs)
            {
                M.Draw(_spriteBatch);
            }
            int counterMBalls = 0;
            foreach (MasterBall M in Gru.masterBallAmasse)
            {
                M.Draw(_spriteBatch,counterMBalls);
                counterMBalls++;
            }
            //drawsnorlax
            if(Gru.snorlaxUsed != null)
            {
                _spriteBatch.Draw(content.Load<Texture2D>("Sprites\\snorlax"), labyrinthe.GetCase(Gru.snorlaxUsed.OrdreX, Gru.snorlaxUsed.OrdreY).GetPosition(), Color.White);
            }

            if(emplacementFinNiveau.X != -1)
                _spriteBatch.Draw(content.Load<Texture2D>("Sprites\\ladder"),labyrinthe.GetCase((int)emplacementFinNiveau.X, (int)emplacementFinNiveau.Y).GetPosition(), Color.White);

            //Dessiner les points de vie restant
            for (int i = 0; i < Gru.PointsVie; i++ )
            {
                _spriteBatch.Draw(content.Load<Texture2D>("Sprites\\RedPlayer"), new Vector2(0 + 100 * i, 0), Color.White);
            }

            //Dessiner le score
            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\SecondFont"), "Score: " + Pointage.GetInstance().GetTotalPointage(), new Vector2(450, 0), Color.Black);
            _spriteBatch.DrawString(content.Load<SpriteFont>("Font\\SecondFont"), "Badges", new Vector2(1000, 50), Color.Black);


                //Draw de la Police
                foreach (PersonnageNonJoueur police in Polices)
                {
                    police.Draw(_spriteBatch);
                }

            //Draw de Gru
            if (!Gru.EstMort())
                Gru.Draw(_spriteBatch);
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
