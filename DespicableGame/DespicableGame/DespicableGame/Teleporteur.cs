using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    class Teleporteur : Case
    {
        public Teleporteur(Case caseHaut, Case caseBas, Case caseGauche, Case caseDroite)
        {
            this.CaseHaut = caseHaut;
            this.CaseBas = caseBas;
            this.CaseGauche = caseGauche;
            this.CaseDroite = caseDroite;
        }

        public Case Teleport()
        {
            int choixRandom = GenerateurChiffreAleatoire.NouveauChiffre(4);

            switch (choixRandom)
            {
                case 0:
                    return CaseHaut;
                case 1:
                    return CaseBas;
                case 2:
                    return CaseGauche;
                case 3:
                    return CaseDroite;
                default:
                    return null;
            }
        }
    }
}