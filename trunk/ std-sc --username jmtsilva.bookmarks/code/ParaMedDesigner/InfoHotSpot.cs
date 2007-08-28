using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.ComponentModel;

namespace ParaMedDesigner
{
    [XmlRootAttribute(ElementName = "controlHotSpot", IsNullable = false)]
    public class InfoHotSpot: Info
    {
        public InfoHotSpot(Control cOwner)
        {
            owner = cOwner;
        }

        // NAME
        private String name;
        [XmlAttribute("TreataInt")]
        [CategoryAttribute("Name")]
        public String Name
        {
            get { return name; }
            set {
                name = value;
                ((MyControlHotSpot)(owner)).changeTabName(name);
            }
        }
	


    }
}
