using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;

namespace ParaMedDesigner
{
    [XmlRootAttribute(ElementName = "spot", IsNullable = false)]
    public class InfoHotSpot: Info
    {
        public InfoHotSpot()
        {
        }

        public InfoHotSpot(Control cOwner)
        {
            owner = cOwner;
        }

        //-------------------------------------
        // serialize purpose
        //-------------------------------------
        [XmlArray("InfoControl"), XmlArrayItem("controls", typeof(InfoControl))]
        public ArrayList infoControls = new ArrayList();

        public void appendInfoControl(InfoControl infoControl)
        {
            infoControls.Add(infoControl);

        }
        public void removeAllInfoControls() 
        {
            infoControls.RemoveRange(0, infoControls.Count);
        }

        // its mandatory a predicate
        private static bool GetAllElements(InfoControl i)
        {
            return i != null ? true : false;    
        }
        //-------------------------------------

        // NAME
        private String name;
        [XmlAttribute("Name")]
        [CategoryAttribute("Name")]
        public String Name
        {
            get { return name; }
            set {
                name = value;
                if (owner != null)
                {
                    ((MyControlHotSpot)(owner)).changeTabName(name);
                }
            }
        }
    }
}
