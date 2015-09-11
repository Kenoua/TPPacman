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

namespace Exercice5
{
    public class XMLScoreReader
    {
        List<Buttons> boutons;

        public XMLScoreReader()
        {
            boutons = new List<Buttons>();
        }

      
        public void Load(string _filePath, string typeInput)
        {
            XmlReader reader = XmlReader.Create(_filePath);

            if (typeInput == "Keyboard")
            {
                reader.MoveToContent();
                while (reader.ReadToFollowing("ScoreSave"))
                {
                    
                }
            }
            else
            {
                reader.MoveToContent();
                while (reader.ReadToFollowing("ScoreSave"))
                {
                   
                }
            }
        }

        private Buttons decodeXMLBouton(string boutonXML)
        {
            switch(boutonXML)
            {
                case "A":
                    break;
                case "B":
                    break;
                case "X":
                    break;
                case "Y":
                    break;
            }
            return Buttons.A;
        }
    }
}
