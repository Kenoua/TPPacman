using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DespicableGame
{
    /// <summary>
    /// Classe statique qui se charge d'initialiser chaque niveau.
    /// </summary>
    public class LevelLoader
    {
        private static ContentManager content;
        private static Labyrinthe labyrinthe;
        private static int level = 0;

        const int DEPART_X = 6;
        const int DEPART_Y = 7;

        /// <summary>
        /// Sets the content.
        /// </summary>
        /// <param name="_content">The _content.</param>
        /// <param name="_labyrinthe">The _labyrinthe.</param>
        public static void SetContent(ContentManager _content, Labyrinthe _labyrinthe)
        {
            content = _content;
            labyrinthe = _labyrinthe;
        }

        /// <summary>
        /// Augementers the level.
        /// </summary>
        /// <param name="_level">The _level.</param>
        /// <returns></returns>
        public static int AugementerLevel(int _level)
        {
            level += _level;
            return level;
        }

        /// <summary>
        /// Chargers the personnage.
        /// </summary>
        /// <returns></returns>
        public static PersonnageJoueur ChargerPersonnage()
        {
            return new PersonnageJoueur
                (
                content.Load<Texture2D>("Sprites\\RedPlayer"),
                new Vector2(labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().X, labyrinthe.GetCase(DEPART_X, DEPART_Y).GetPosition().Y),
                labyrinthe.GetCase(DEPART_X, DEPART_Y)
                );
        }

        /// <summary>
        /// Chargers the badges.
        /// @see verifierCaseNonValide
        /// </summary>
        /// <returns></returns>
        public static List<Badge> ChargerBadges()
        {
            List<Badge> badges = new List<Badge>();
            for (int i = 0; i < level; i++)
            {
                int x = -1;
                int y = -1;
                do
                {
                    x = GenerateurChiffreAleatoire.NouveauChiffre(14);
                    y = GenerateurChiffreAleatoire.NouveauChiffre(10);
                } while (verifierCaseNonValide(x, y));

                badges.Add(new Badge((BadgeType)i, content.Load<Texture2D>("Sprites\\badge" + (i + 1).ToString()),
                    new Vector2(labyrinthe.GetCase(x, y).GetPosition().X, labyrinthe.GetCase(x, y).GetPosition().Y),
                    labyrinthe.GetCase(x, y)));
            }
            return badges;
        }

        /// <summary>
        /// Chargers the pokeballs.
        /// @see verifierCaseNonValide
        /// </summary>
        /// <returns></returns>
        public static List<Pokeball> ChargerPokeballs()
        {
            List<Pokeball> pokeballs = new List<Pokeball>();
            for (int i = 0; i < level; i++)
            {
                int x = -1;
                int y = -1;
                do
                {
                    x = GenerateurChiffreAleatoire.NouveauChiffre(14);
                    y = GenerateurChiffreAleatoire.NouveauChiffre(10);
                } while (verifierCaseNonValide(x, y));

                pokeballs.Add(new Pokeball((BadgeType)i, content.Load<Texture2D>("Sprites\\Pokeball"),
                    new Vector2(labyrinthe.GetCase(x, y).GetPosition().X, labyrinthe.GetCase(x, y).GetPosition().Y),
                    labyrinthe.GetCase(x, y)));
            }
            return pokeballs;
        }

        /// <summary>
        /// Chargers the masterballs.
        /// @see verifierCaseNonValide
        /// </summary>
        /// <returns></returns>
        public static List<MasterBall> ChargerMasterballs()
        {
            List<MasterBall> masterBalls = new List<MasterBall>();

            for (int i = 0; i < level / 2 + 1; i++)
            {
                int x = -1;
                int y = -1;
                do
                {
                    x = GenerateurChiffreAleatoire.NouveauChiffre(14);
                    y = GenerateurChiffreAleatoire.NouveauChiffre(10);
                } while (verifierCaseNonValide(x, y));

                masterBalls.Add(new MasterBall(content.Load<Texture2D>("Sprites\\MasterBall"),
                    new Vector2(labyrinthe.GetCase(x, y).GetPosition().X, labyrinthe.GetCase(x, y).GetPosition().Y),
                    labyrinthe.GetCase(x, y)));
            }
            return masterBalls;
        }


        /// <summary>
        /// Chargers the fin niveau.
        /// @see verifierCaseNonValide
        /// </summary>
        /// <returns></returns>
        public static Vector2 ChargerFinNiveau()
        {
            Vector2 position;
            do
            {
                position.X = GenerateurChiffreAleatoire.NouveauChiffre(14);
                position.Y = GenerateurChiffreAleatoire.NouveauChiffre(10);
            } while (verifierCaseNonValide((int)position.X, (int)position.Y));
            return position;
        }

        /// <summary>
        /// Chargers the ennemis.
        /// @see verifierCaseNonValide
        /// </summary>
        /// <returns></returns>
        public static List<PersonnageNonJoueur> ChargerEnnemis()
        {
            List<PersonnageNonJoueur> ennemis = new List<PersonnageNonJoueur>();

            for (int i = 0; i < 3 + level / 3; i++)
            {
                int x = -1;
                int y = -1;
                do
                {
                    x = GenerateurChiffreAleatoire.NouveauChiffre(14);
                    y = GenerateurChiffreAleatoire.NouveauChiffre(10);
                } while (!verifierCaseNonValide(x, y));

                ennemis.Add(new PersonnageNonJoueur(content.Load<Texture2D>("Sprites\\RocketGrunt"),
                    new Vector2(labyrinthe.GetCase(x, y).GetPosition().X, labyrinthe.GetCase(x, y).GetPosition().Y),
                    labyrinthe.GetCase(x, y)));
            }
            return ennemis;
        }


        /// <summary>
        /// Recommencers this instance.
        /// </summary>

        public static List<Snorlax> ChargerSnorlax()
        {
            List<Snorlax> snorlaxs = new List<Snorlax>();

            for (int i = 0; i < 1 + level / 3; i++)
            {
                int x = -1;
                int y = -1;
                do
                {
                    x = GenerateurChiffreAleatoire.NouveauChiffre(14);
                    y = GenerateurChiffreAleatoire.NouveauChiffre(10);
                } while (verifierCaseNonValide(x, y));

                snorlaxs.Add(new Snorlax(content.Load<Texture2D>("Sprites\\Snorlax"),
                    new Vector2(labyrinthe.GetCase(x, y).GetPosition().X, labyrinthe.GetCase(x, y).GetPosition().Y),
                    labyrinthe.GetCase(x, y)));
            }
            return snorlaxs;
        }

        public static void Recommencer()
        {
            Pointage.GetInstance().RetourZero();
            level = 0;
        }

        /// <summary>
        /// Vérifie si les coordonnnées de la case choisie
        /// tombe à l'intérieur des zones de départ des officiers.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        private static bool verifierCaseNonValide(int x, int y)
        {
            bool caseNonValide = false;

            if (x == 6 || x == 7)
            {
                if (y == 0 || y == 9)
                {
                    caseNonValide = true;
                }
            }
            else if (y == 4 || y == 5)
            {
                if (x == 0 || x == 13)
                {
                    caseNonValide = true;
                }
            }
            return caseNonValide;
        }
    }
}
