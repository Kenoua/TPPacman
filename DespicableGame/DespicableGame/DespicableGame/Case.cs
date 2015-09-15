<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    public enum limites { haut = 1, bas = 2, gauche = 4, droite = 8 };

    /// <summary>
    /// Classe qui définit une case du labyrinthe.
    /// </summary>
    public class Case
    {
        private int contour;
        private Vector2 position;
        private int ordreX;
        private int ordreY;

        //chaque case a des références sur toutes les cases qui l'entoure
        private Case caseHaut = null;
        private Case caseBas = null;
        private Case caseGauche = null;
        private Case caseDroite = null;

        /// <summary>
        /// Gets the ordre x.
        /// </summary>
        /// <value>
        /// The ordre x.
        /// </value>
        public int OrdreX 
        {
            get { return ordreX; }
        }
        /// <summary>
        /// Gets the ordre y.
        /// </summary>
        /// <value>
        /// The ordre y.
        /// </value>
        public int OrdreY
        {
            get { return ordreY; }
        }
        /// <summary>
        /// Gets or sets the case haut.
        /// </summary>
        /// <value>
        /// The case haut.
        /// </value>
        public Case CaseHaut
        {
            get { return caseHaut; }
            set { caseHaut = value; }
        }

        /// <summary>
        /// Gets or sets the case bas.
        /// </summary>
        /// <value>
        /// The case bas.
        /// </value>
        public Case CaseBas
        {
            get { return caseBas; }
            set { caseBas = value; }
        }

        /// <summary>
        /// Gets or sets the case gauche.
        /// </summary>
        /// <value>
        /// The case gauche.
        /// </value>
        public Case CaseGauche
        {
            get { return caseGauche; }
            set { caseGauche = value; }
        }

        /// <summary>
        /// Gets or sets the case droite.
        /// </summary>
        /// <value>
        /// The case droite.
        /// </value>
        public Case CaseDroite
        {
            get { return caseDroite; }
            set { caseDroite = value; }
        }

        //Décalage en pixel du labyrinthe par rapport à la position 0,0
        //Pourrait être porté par la classe labyrinthe
        public const int DECALAGE_X = 64;
        public const int DECALAGE_Y = 64;

        public const int TAILLE_CASE = 64;

        public const int OFFSET_CASE = 56;

        public const int TAILLE_LIGNE = 8;

        //Pour le téléporteur, qui est un type de case spéciale
        protected Case()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Case"/> class.
        /// </summary>
        /// <param name="contour">The contour.</param>
        /// <param name="ordreX">The ordre x.</param>
        /// <param name="ordreY">The ordre y.</param>
        public Case(int contour, int ordreX, int ordreY)
        {
            //Contour: ce qu'on vérifie c'est les présences bit à bit: premier bit = mur haut, second = mur bas, troisième = gauche, quatrière droite
            this.contour = contour;
            this.ordreX = ordreX;
            this.ordreY = ordreY;
            this.position.X = ordreX * TAILLE_CASE + DECALAGE_X;
            this.position.Y = ordreY * TAILLE_CASE + DECALAGE_Y;
        }

        /// <summary>
        /// Gets the contour.
        /// </summary>
        /// <value>
        /// The contour.
        /// </value>
        public int Contour
        {
            get { return contour; }
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <returns></returns>
        public Vector2 GetPosition()
        {
            return position;
        }

        /// <summary>
        /// Determines whether [is mur haut].
        /// </summary>
        /// <returns></returns>
        public bool IsMurHaut()
        {
            //Comparaison binaire sur le bit 1
            if ((contour & (int)limites.haut) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is mur bas].
        /// </summary>
        /// <returns></returns>
        public bool IsMurBas()
        {
            //Comparaison binaire sur le bit 2
            if ((contour & (int)limites.bas) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is mur gauche].
        /// </summary>
        /// <returns></returns>
        public bool IsMurGauche()
        {
            //Comparaison binaire sur le bit 3
            if ((contour & (int)limites.gauche) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines whether [is mur droit].
        /// </summary>
        /// <returns></returns>
        public bool IsMurDroit()
        {
            //Comparaison binaire sur le bit 4
            if ((contour & (int)limites.droite) != 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Dessiners the murs.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="horizontale">The horizontale.</param>
        /// <param name="verticale">The verticale.</param>
        public void DessinerMurs(SpriteBatch spriteBatch, Texture2D horizontale, Texture2D verticale)
        {
            //S'il y a un mur, on le dessine
            if (IsMurHaut())
            {
                spriteBatch.Draw(horizontale, position, Color.White);
            }

            if (IsMurBas())
            {
                position.Y += OFFSET_CASE;
                spriteBatch.Draw(horizontale, position, Color.White);
                position.Y -= OFFSET_CASE;
            }

            if (IsMurGauche())
            {
                spriteBatch.Draw(verticale, position, Color.White);
            }

            if (IsMurDroit())
            {
                position.X += OFFSET_CASE;
                spriteBatch.Draw(verticale, position, Color.White);
                position.X -= OFFSET_CASE;
            }
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace DespicableGame
{
    public enum limites { haut = 1, bas = 2, gauche = 4, droite = 8 };

    public class Case
    {
        private int contour;
        private Vector2 position;
        private int ordreX;
        private int ordreY;

        //chaque case a des références sur toutes les cases qui l'entoure
        private Case caseHaut = null;
        private Case caseBas = null;
        private Case caseGauche = null;
        private Case caseDroite = null;

        public int OrdreX 
        {
            get { return ordreX; }
        }
        public int OrdreY
        {
            get { return ordreY; }
        }
        public Case CaseHaut
        {
            get { return caseHaut; }
            set { caseHaut = value; }
        }

        public Case CaseBas
        {
            get { return caseBas; }
            set { caseBas = value; }
        }

        public Case CaseGauche
        {
            get { return caseGauche; }
            set { caseGauche = value; }
        }

        public Case CaseDroite
        {
            get { return caseDroite; }
            set { caseDroite = value; }
        }

        //Décalage en pixel du labyrinthe par rapport à la position 0,0
        //Pourrait être porté par la classe labyrinthe
        public const int DECALAGE_X = 64;
        public const int DECALAGE_Y = 64;

        public const int TAILLE_CASE = 64;

        public const int OFFSET_CASE = 56;

        public const int TAILLE_LIGNE = 8;

        //Pour le téléporteur, qui est un type de case spéciale
        protected Case()
        {

        }

        public Case(int contour, int ordreX, int ordreY)
        {
            //Contour: ce qu'on vérifie c'est les présences bit à bit: premier bit = mur haut, second = mur bas, troisième = gauche, quatrière droite
            this.contour = contour;
            this.ordreX = ordreX;
            this.ordreY = ordreY;
            this.position.X = ordreX * TAILLE_CASE + DECALAGE_X;
            this.position.Y = ordreY * TAILLE_CASE + DECALAGE_Y;
        }

        public int Contour
        {
            get { return contour; }
        }

        public Vector2 GetPosition()
        {
            return position;
        }

        public bool IsMurHaut()
        {
            //Comparaison binaire sur le bit 1
            if ((contour & (int)limites.haut) != 0)
            {
                return true;
            }
            return false;
        }

        public bool IsMurBas()
        {
            //Comparaison binaire sur le bit 2
            if ((contour & (int)limites.bas) != 0)
            {
                return true;
            }
            return false;
        }

        public bool IsMurGauche()
        {
            //Comparaison binaire sur le bit 3
            if ((contour & (int)limites.gauche) != 0)
            {
                return true;
            }
            return false;
        }

        public bool IsMurDroit()
        {
            //Comparaison binaire sur le bit 4
            if ((contour & (int)limites.droite) != 0)
            {
                return true;
            }
            return false;
        }

        public void DessinerMurs(SpriteBatch spriteBatch, Texture2D horizontale, Texture2D verticale)
        {
            //S'il y a un mur, on le dessine
            if (IsMurHaut())
            {
                spriteBatch.Draw(horizontale, position, Color.White);
            }

            if (IsMurBas())
            {
                position.Y += OFFSET_CASE;
                spriteBatch.Draw(horizontale, position, Color.White);
                position.Y -= OFFSET_CASE;
            }

            if (IsMurGauche())
            {
                spriteBatch.Draw(verticale, position, Color.White);
            }

            if (IsMurDroit())
            {
                position.X += OFFSET_CASE;
                spriteBatch.Draw(verticale, position, Color.White);
                position.X -= OFFSET_CASE;
            }
        }
    }
}
>>>>>>> origin/master
