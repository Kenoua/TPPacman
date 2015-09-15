using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;



namespace DespicableGame
{
    /// <summary>
    /// Classe qui définit le personnage qui sera joué par le joueur.
    /// </summary>
    public class PersonnageJoueur : Personnage
    {
        public List<Badge> badgesAmasse;
        public List<Pokeball> pokeballAmasse;
        public List<MasterBall> masterBallAmasse;
        public Case snorlaxUsed;
        public bool estPokemonLegendaire;
        public int counterLegendaire;
        public Texture2D spriteJoueurReserve;
        public int modificateurVitese;
        private bool estToucher;


        /// <summary>
        /// Initializes a new instance of the <see cref="PersonnageJoueur"/> class.
        /// </summary>
        /// <param name="dessin">The dessin.</param>
        /// <param name="position">The position.</param>
        /// <param name="ActualCase">The actual case.</param>
        public PersonnageJoueur(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            dernierContact = DateTime.Now;
            derniereCase = null;
            snorlaxUsed = null;
            delaiProchainContact = new TimeSpan(0, 0, 0, 1, 500);
            pointsVie = 3;
            estPokemonLegendaire = false;
            estToucher = false;
            counterLegendaire = 0;
            Destination = null;
            badgesAmasse = new List<Badge>();
            pokeballAmasse = new List<Pokeball>();
            masterBallAmasse = new List<MasterBall>();
            spriteJoueurReserve = dessin;
            modificateurVitese = 1;
        }

        /// <summary>
        /// Mouvements this instance.
        /// </summary>
        public override void Mouvement()
        {
            if (DateTime.Now - dernierContact >= delaiProchainContact && !estPokemonLegendaire)
            {
                estToucher = false;
                modificateurVitese = 1;
            }

            if (Destination != null)
            {
                position.X += VitesseX * modificateurVitese;
                position.Y += VitesseY * modificateurVitese;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    derniereCase = ActualCase;
                    ActualCase = Destination;
                    Destination = null;
                    if (estToucher)
                        modificateurVitese = 2;
                }
            }

            if (estPokemonLegendaire)
            {
                counterLegendaire--;
                if (counterLegendaire <= 0)
                {
                    estPokemonLegendaire = false;
                    dessin = spriteJoueurReserve;
                    modificateurVitese = 1;
                }
            }
        }

        /// <summary>
        /// Verifiers the mouvement.
        /// </summary>
        /// <param name="caseDestionation">The case destionation.</param>
        /// <param name="vitesseX">The vitesse x.</param>
        /// <param name="vitesseY">The vitesse y.</param>
        public void VerifierMouvement(Case caseDestionation, int vitesseX, int vitesseY)
        {
            //Si la direction choisie n'est pas nulle

            if (caseDestionation != null && !caseSnorlax.Contains(caseDestionation))

            {
                //On vérifie si la case est un téléporteur
                Case testTeleportation = TestTeleporter(caseDestionation);

                //Si non, on bouge
                if (testTeleportation == null)
                {
                    Destination = caseDestionation;
                    VitesseX = vitesseX;
                    VitesseY = vitesseY;
                }
                //Si oui, on se téléporte.
                else
                {
                    ActualCase = testTeleportation;
                    position = new Vector2(ActualCase.GetPosition().X, ActualCase.GetPosition().Y);
                }
            }
        }

        /// <summary>
        /// Ests the mort.
        /// </summary>
        /// <returns></returns>
        public override bool EstMort()
        {
            return mort;
        }

        /// <summary>
        /// Touchers the autre personnage.
        /// </summary>
        public override void ToucherAutrePersonnage()
        {
            if (!estPokemonLegendaire)
            {
                if (DateTime.Now - dernierContact >= delaiProchainContact)
                {
                    dernierContact = DateTime.Now;
                    pointsVie--;
                    estToucher = true;
                    Pointage.GetInstance().RetirerPoints(200);
                    if (pointsVie == 0)
                    {
                        mort = true;
                    }
                }
            }
        }

        /// <summary>
        /// Tests the teleporter.
        /// </summary>
        /// <param name="laCase">The la case.</param>
        /// <returns></returns>
        private Case TestTeleporter(Case laCase)
        {
            if (laCase is Teleporteur)
            {
                return ((Teleporteur)laCase).Teleport();
            }
            return null;
        }

        /// <summary>
        /// Checks the snorlax.
        /// </summary>
        /// <param name="_case">The _case.</param>
        /// <returns></returns>
        private bool CheckSnorlax(Case _case)
        {
            if (snorlaxUsed != null)
            {
                if ((_case.GetPosition() == snorlaxUsed.GetPosition()))
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// Utilises the legendaire.
        /// </summary>
        /// <param name="_content">The _content.</param>
        public void UtiliseLegendaire(ContentManager _content)
        {
            counterLegendaire = 300;
            modificateurVitese = 2;
            estPokemonLegendaire = true;

            int choixLegendaire = new Random().Next(3);
            if (choixLegendaire == 0)
            {
                dessin = _content.Load<Texture2D>("Sprites\\Zapdos");
            }
            else if (choixLegendaire == 1)
            {
                dessin = _content.Load<Texture2D>("Sprites\\Moltres");
            }
            else
            {
                dessin = _content.Load<Texture2D>("Sprites\\Articuno");
            }
        }
        public void setCasesSnorlax(List<Case> _casesSnorlax)
        {
            caseSnorlax = _casesSnorlax;
        }
    }
}
