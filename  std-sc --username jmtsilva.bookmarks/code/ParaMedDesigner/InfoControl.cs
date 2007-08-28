using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Forms;

namespace ParaMedDesigner
{

    [XmlRootAttribute(ElementName = "control", IsNullable = false)]
    public class InfoControl: Info
    {
        public InfoControl()
        {

        }

        public InfoControl(Control cOwner)
        {
            //Info(this);
            owner = cOwner;
        }

        // TRETA
        private int tretaInt;
        [XmlAttribute("TreataInt")]
        [CategoryAttribute("Treta")]
        public int TretaInt
        {
            get { return tretaInt; }
            set { tretaInt = value; }
        }

    }
}
