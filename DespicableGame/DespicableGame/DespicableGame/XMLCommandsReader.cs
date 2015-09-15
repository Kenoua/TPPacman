using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DespicableGame
{
    /// <summary>
    /// Classe qui permet d'aller lire un fichier XML
    /// pour y rechercher les boutons qui seront utilisés
    /// pour jouer au jeu.
    /// </summary>
    public class XMLCommandReader
    {
        private Buttons boutonGamePad;
        private List<Keys> touchesClaviers;

        /// <summary>
        /// Initializes a new instance of the <see cref="XMLCommandReader"/> class.
        /// </summary>
        public XMLCommandReader()
        {
            touchesClaviers = new List<Keys>();
        }


        /// <summary>
        /// Loads the keys. (Keyboard)
        /// </summary>
        /// <returns></returns>
        public List<Keys> LoadKeys()
        {
            XmlReader reader = XmlReader.Create("keyboardCommands.xml");

            reader.MoveToContent();
            while (reader.ReadToFollowing("Command"))
            {
                Keys key;
                reader.ReadToFollowing("Button");
                
                string keyString = reader.ReadElementContentAsString();
                Enum.TryParse(keyString, out key);
                touchesClaviers.Add(key);
            }

            return touchesClaviers;
        }

        /// <summary>
        /// Loads the button (Gamepad).
        /// </summary>
        /// <returns></returns>
        public Buttons LoadButton()
        {
            XmlReader reader = XmlReader.Create("gamepadCommands.xml");

            reader.MoveToContent();
            while (reader.ReadToFollowing("Command"))
            {
                reader.ReadToFollowing("Button");
                string keyString = reader.ReadElementContentAsString();
                Enum.TryParse(keyString, out boutonGamePad);
            }
            return boutonGamePad;
        }
    }
}
