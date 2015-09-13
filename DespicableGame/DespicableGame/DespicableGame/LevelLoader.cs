using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace DespicableGame
{
    public class LevelLoader
    {
        private static ContentManager content;
        private static Labyrinthe labyrinthe;
        private static int level = 0;
        private static Dictionary<int, int> emplacementsUtiliser;

        public static void SetContent(ContentManager _content, Labyrinthe _labyrinthe)
        {
            content = _content;
            labyrinthe = _labyrinthe;
        }

        public static int AugementerLevel(int _level)
        {
            level += _level;
            return level;
        }

        public static List<Badge> ChargerBadges()
        {
            List<Badge> badges = new List<Badge>();
            for(int i = 0; i < level; i++)
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

        public static List<PersonnageNonJoueur> ChargerEnnemis()
        {
            List<PersonnageNonJoueur> ennemis = new List<PersonnageNonJoueur>();

            for (int i = 0; i < 1 + level / 3; i++)
            {
                ennemis.Add(new PersonnageNonJoueur(content.Load<Texture2D>("Sprites\\RocketGrunt"),
                    new Vector2(labyrinthe.GetCase(i, i).GetPosition().X, labyrinthe.GetCase(i, i).GetPosition().Y),
                    labyrinthe.GetCase(i, i)));
            }
            return ennemis;
        }

        public static void Recommencer()
        {
            Pointage.GetInstance().RetourZero();
            level = 0;
        }

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
