using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DespicableGame
{
    /// <summary>
    /// Classe qui définit une case comme étant un téléporteur
    /// afin de téléporter le joueur à travers le labyrinthe.
    /// </summary>
    class Teleporteur : Case
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Teleporteur"/> class.
        /// </summary>
        /// <param name="caseHaut">The case haut.</param>
        /// <param name="caseBas">The case bas.</param>
        /// <param name="caseGauche">The case gauche.</param>
        /// <param name="caseDroite">The case droite.</param>
        public Teleporteur(Case caseHaut, Case caseBas, Case caseGauche, Case caseDroite)
        {
            this.CaseHaut = caseHaut;
            this.CaseBas = caseBas;
            this.CaseGauche = caseGauche;
            this.CaseDroite = caseDroite;
        }

        /// <summary>
        /// Teleports this instance.
        /// </summary>
        /// <returns></returns>
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