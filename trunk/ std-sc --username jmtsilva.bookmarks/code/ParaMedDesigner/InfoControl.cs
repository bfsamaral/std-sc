using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Forms;

namespace ParaMedDesigner
{

    [XmlRootAttribute(ElementName = "control", IsNullable = false)]
    public class InfoControl: Info
    {
        private int tretaInt;

        [XmlAttribute("TreataInt")]
        [CategoryAttribute("Treta")]
        public int TretaInt
        {
            get { return tretaInt; }
            set { tretaInt = value; }
        }

        public InfoControl(Control cOwner)
        {
            //Info(this);
            owner = cOwner;
        }

    }
}
