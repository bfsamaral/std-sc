using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;

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

        // TYPE
        private String type;
        [XmlAttribute("Type")]
        [CategoryAttribute("Type"), ReadOnlyAttribute(true), DescriptionAttribute("The Type of the Control")]
        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        // ELEMNS
        private StringCollection elems = new StringCollection();
        [XmlArray("Elems"), XmlArrayItem("ElemX", typeof(String))]
        [CategoryAttribute("Elems"), DescriptionAttribute("Elems for list controls")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public StringCollection ElemsArrayList
        {
            get { 
                return elems;
            }
            set { 
                elems = value; 
                /*
                if( (owner != null) && type.Equals("ComboBox") )
                {
                    String[] aux = new String[elems.Count];
                    for(int i =0; i<elems.Count; ++i)
                    {
                        aux[i] = elems[i];
                    }
                    ((ComboBox)owner).Items.AddRange(aux);
                }
                 */
            }
        }

    }
}
