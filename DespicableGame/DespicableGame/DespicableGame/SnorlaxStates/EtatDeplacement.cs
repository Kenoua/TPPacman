using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DespicableGame.SnorlaxStates
{
    /// <summary>
    /// Définit l'état dans lequel Snorlax se déplace
    /// </summary>
    class EtatDeplacement : EtatSnorlax
    {
        private readonly Snorlax personnage;
        private int nombreDeplacements;


        /// <summary>
        /// Initializes a new instance of the <see cref="EtatDeplacement"/> class.
        /// </summary>
        /// <param name="_personnage">The _personnage.</param>
        public EtatDeplacement(Snorlax _personnage)
        {
            personnage = _personnage;
            personnage.Destination = personnage.ActualCase;
            nombreDeplacements = GenerateurChiffreAleatoire.NouveauChiffre(1, 5);


        }

        /// <summary>
        /// Updates this instance.
        /// </summary>
        public void Update()
        {

        }

        /// <summary>
        /// Mouvements the specified a i_ case.
        /// </summary>
        /// <param name="AI_Case">a i_ case.</param>
        /// <returns></returns>
        public Case Mouvement(Case AI_Case)
        {

            if (PersonnageEnvue())
            {
                personnage.ChangerEtat(new EtatSommeil(personnage));
                personnage.Destination = personnage.ActualCase;
                return personnage.Destination;
            }
            else
            {
                List<int> optionsInvalides = new List<int>();

                while (true)
                {
                    int choixRandom = GenerateurChiffreAleatoire.NouveauChiffre(4);

                    if (choixRandom == 0)
                    {
                        if (!(AI_Case.CaseHaut == null || AI_Case.CaseHaut is Teleporteur))
                        {
                            personnage.DirectionArriere = 2;
                            personnage.VitesseX = 0;
                            personnage.VitesseY = -2/*-DespicableGame.VITESSE*/;
                            return AI_Case.CaseHaut;
                        }
                        else
                        {
                            if (!optionsInvalides.Contains(choixRandom))
                                optionsInvalides.Add(choixRandom);
                        }
                    }

                    if (choixRandom == 2)
                    {
                        if (!(AI_Case.CaseBas == null || AI_Case.CaseBas is Teleporteur))
                        {
                            personnage.DirectionArriere = 0;
                            personnage.VitesseX = 0;
                            personnage.VitesseY = 2;
                            return AI_Case.CaseBas;
                        }
                        else
                        {
                            if (!optionsInvalides.Contains(choixRandom))
                                optionsInvalides.Add(choixRandom);
                        }
                    }

                    if (choixRandom == 1)
                    {
                        if (!(AI_Case.CaseGauche == null || AI_Case.CaseGauche is Teleporteur))
                        {
                            personnage.DirectionArriere = 3;
                            personnage.VitesseX = -2;
                            personnage.VitesseY = 0;
                            return AI_Case.CaseGauche;
                        }
                        else
                        {
                            if (!optionsInvalides.Contains(choixRandom))
                                optionsInvalides.Add(choixRandom);
                        }
                    }

                    if (choixRandom == 3)
                    {
                        if (!(AI_Case.CaseDroite == null || AI_Case.CaseDroite is Teleporteur))
                        {
                            personnage.DirectionArriere = 1;
                            personnage.VitesseX = 2;
                            personnage.VitesseY = 0;
                            return AI_Case.CaseDroite;
                        }
                        else
                        {
                            if (!optionsInvalides.Contains(choixRandom))
                                optionsInvalides.Add(choixRandom);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Personnages en vue.
        /// </summary>
        /// <returns></returns>
        public bool PersonnageEnvue()
        {
            bool personnageEnvue = false;
            personnageEnvue = regardHaut(personnage.ActualCase.CaseHaut);

            if (!personnageEnvue)
            {
                personnageEnvue = regardGauche(personnage.ActualCase.CaseGauche);
            }
            if (!personnageEnvue)
            {
                personnageEnvue = regardDroit(personnage.ActualCase.CaseDroite);
            }
            if (!personnageEnvue)
            {
                personnageEnvue = regardBas(personnage.ActualCase.CaseBas);
            }

            return personnageEnvue;
        }

        /// <summary>
        /// Regarder vers le haut.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
        private bool regardHaut(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase || personnage.listeCasesRocket.Contains(_caseVerifier))
                {
                    return true;
                }
                else if (_caseVerifier.CaseHaut != null)
                {
                    return regardHaut(_caseVerifier.CaseHaut);
                }
            }
            return false;
        }

        /// <summary>
        /// Regarder vers la droite.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
        private bool regardDroit(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase || personnage.listeCasesRocket.Contains(_caseVerifier))
                {
                    return true;
                }
                else if (_caseVerifier.CaseDroite != null)
                {
                    return regardDroit(_caseVerifier.CaseDroite);
                }
            }
            return false;
        }

        /// <summary>
        /// Regarder vers le bas.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
        private bool regardBas(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase || personnage.listeCasesRocket.Contains(_caseVerifier))
                {
                    return true;
                }
                else if (_caseVerifier.CaseBas != null)
                {
                    return regardBas(_caseVerifier.CaseBas);
                }
            }
            return false;
        }

        /// <summary>
        /// Regarder vers la gauche.
        /// </summary>
        /// <param name="_caseVerifier">The _case verifier.</param>
        /// <returns></returns>
        private bool regardGauche(Case _caseVerifier)
        {
            if (_caseVerifier != null)
            {
                if (_caseVerifier == GameStates.EtatPartieEnCours.Gru.ActualCase || personnage.listeCasesRocket.Contains(_caseVerifier))
                {
                    return true;
                }
                else if (_caseVerifier.CaseGauche != null)
                {
                    return regardGauche(_caseVerifier.CaseGauche);
                }
            }
            return false;
        }
    }
}
