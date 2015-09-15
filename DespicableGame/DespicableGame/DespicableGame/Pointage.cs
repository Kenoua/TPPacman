using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame
{
    /// <summary>
    /// Classe singleton qui permet d'attribuer des points
    /// au joueur pour ses différents accomplissement dans
    /// le jeu.
    /// </summary>
    public class Pointage
    {
        private static Pointage instance = null;
        int totalPointage;
        int serie;

        /// <summary>
        /// Prevents a default instance of the <see cref="Pointage"/> class from being created.
        /// </summary>
        private Pointage()
        {
            totalPointage = 0;
            serie = 0;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static Pointage GetInstance()
        {
            if(instance == null)
            {
                instance = new Pointage();
            }
            return instance;
        }

        /// <summary>
        /// Ajouters the points.
        /// </summary>
        /// <param name="_ajout">The _ajout.</param>
        public void AjouterPoints(int _ajout)
        {
            totalPointage += _ajout + serie;
            IncrementerSerie();
        }

        /// <summary>
        /// Retirers the points.
        /// </summary>
        /// <param name="_retrait">The _retrait.</param>
        public void RetirerPoints(int _retrait)
        {
            ReinitialiserSerie();
            totalPointage -= _retrait;
        }

        /// <summary>
        /// Gets the total pointage.
        /// </summary>
        /// <returns></returns>
        public int GetTotalPointage()
        {
            return totalPointage;
        }

        /// <summary>
        /// Retours à zero.
        /// </summary>
        public void RetourZero()
        {
            totalPointage = 0;
        }

        /// <summary>
        /// Incrementers the serie.
        /// </summary>
        private void IncrementerSerie()
        {
            serie += 10;
        }

        /// <summary>
        /// Reinitialisers the serie.
        /// </summary>
        private void ReinitialiserSerie()
        {
            serie = 0;
        }
    }
}
