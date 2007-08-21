using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ParaMedDesigner
{
    public class Info
    {
        protected Control owner;

        //------------------------------------------------------
        // NAME
        //------------------------------------------------------
        private String name;
        [XmlAttribute("Name")]
        public String Name
        {
            get { return name; }
            set { 
                name = value;
                //if(owner is MyControlLabel)
                ((Control)owner).Text = name;
            }
        }

        //------------------------------------------------------
        // ID
        //------------------------------------------------------
        private int id;
        [XmlAttribute("id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //------------------------------------------------------
        // POSITION: X, Y
        //------------------------------------------------------
        private int x;
        [XmlAttribute("x")]
        [CategoryAttribute("Coordinates")]
        public int X
        {
            get { return x; }
            set { 
                x = value;
                int ycoord = owner.Location.Y;
                owner.Location = new Point(x, ycoord);
            }
        }
        private int y;
        [XmlAttribute("y")]
        [CategoryAttribute("Coordinates")]
        public int Y
        {
            get { return y; }
            set 
            { 
                y = value;
                int xcoord = owner.Location.X;
                owner.Location = new Point(y, xcoord);
            }
        }

        //------------------------------------------------------
        // DIMENSIONS: WIDTH, HEIGHT
        //------------------------------------------------------
        private int width;
        [XmlAttribute("width")]
        [CategoryAttribute("Position")]
        public int Width
        {
            get { return width; }
            set 
            {
                width = value;
                owner.Width = width;
            }
        }

        private int heigth;
        [XmlAttribute("height")]
        [CategoryAttribute("Position")]
        public int Height
        {
            get { return heigth; }
            set {
                heigth = value;
                owner.Height = heigth;
            }
        }
	
	

    }
}
