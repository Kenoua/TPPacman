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
    public class XMLCommandReader
    {
        private Buttons boutonGamePad;
        private List<Keys> touchesClaviers;

        public XMLCommandReader()
        {
            touchesClaviers = new List<Keys>();
        }


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
