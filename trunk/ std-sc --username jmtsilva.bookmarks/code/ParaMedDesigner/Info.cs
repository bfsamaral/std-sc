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
        protected String name;
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
        protected int id;
        [XmlAttribute("id")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        //------------------------------------------------------
        // POSITION: X, Y
        //------------------------------------------------------
        protected int x;
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
        public void setX(int newValue)
        {
            x = newValue;
        }

        protected int y;
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

        public void setY(int newValue)
        {
            y = newValue;
        }

        //------------------------------------------------------
        // DIMENSIONS: WIDTH, HEIGHT
        //------------------------------------------------------
        protected int width;
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
        public void setWidth(int newValue) 
        {
            width = newValue;
        }

        protected int heigth;
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

        public void setHeight(int newValue)
        {
            heigth = newValue;
        }

    }
}
