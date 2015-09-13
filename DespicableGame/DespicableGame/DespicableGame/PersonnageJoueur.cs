using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    public class PersonnageJoueur : Personnage
    {
        public List<Badge> badgesAmasse;
        public List<Pokeball> pokeballAmasse;
        public Case snorlaxUsed;
        public PersonnageJoueur(Texture2D dessin, Vector2 position, Case ActualCase)
            : base(dessin, position, ActualCase)
        {
            dernierContact = DateTime.Now;
            derniereCase = null;
            snorlaxUsed = null;
            delaiProchainContact = new TimeSpan(0, 0, 0, 1, 500);
            pointsVie = 3;
            Destination = null;
            badgesAmasse = new List<Badge>();
            pokeballAmasse = new List<Pokeball>();
        }

        //Algo assez ordinaire.  Pour que ça fonctionne, la vitesse doit être un diviseur entier de 64, pourrait être à revoir.
        public override void Mouvement()
        {
            if (Destination != null)
            {
                position.X += VitesseX;
                position.Y += VitesseY;

                if (position.X == Destination.GetPosition().X && position.Y == Destination.GetPosition().Y)
                {
                    derniereCase = ActualCase;
                    ActualCase = Destination;
                    Destination = null;
                }
            }
        }

        public void VerifierMouvement(Case caseDestionation, int vitesseX, int vitesseY)
        {
            //Si la direction choisie n'est pas nulle
            if (caseDestionation != null && !checkSnorlax(caseDestionation))
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

        public override bool EstMort()
        {
            return mort;
        }

        public override void ToucherAutrePersonnage()
        {
            if (DateTime.Now - dernierContact >= delaiProchainContact)
            {
                dernierContact = DateTime.Now;
                pointsVie--;
                if (pointsVie == 0)
                {
                    mort = true;
                }
            }
        }

        private Case TestTeleporter(Case laCase)
        {
            if (laCase is Teleporteur)
            {
                return ((Teleporteur)laCase).Teleport();
            }
            return null;
        }

        private bool checkSnorlax(Case _case)
        {
            if(snorlaxUsed != null)
            {
                if((_case.GetPosition() == snorlaxUsed.GetPosition()))
                {
                    return true;
                }
            }
            return false;

        }
    }
}
