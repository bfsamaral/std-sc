using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing;


namespace ParaMedDesigner
{
    public class MyControlLabel: Label, IMyControl
    {
        private InfoControl infoControl;
        private TabPage owner;
        private PropertyGrid propertyGrid;

        public MyControlLabel(int x, int y, int width, int height, TabPage tab, PropertyGrid propGrid)
        {
            this.BackColor = Color.Red;
            infoControl = new InfoControl(this);
            infoControl.X = x;
            infoControl.Y = y;
            infoControl.Width = width;
            infoControl.Height = height;

            owner = tab;
            propertyGrid = propGrid;

            this.Click += new EventHandler(updateProperties);

            owner.Controls.Add(this);

            // context menu
            ContextMenu cm = new ContextMenu();
            MenuItem removeItem = new MenuItem("&Remove", new EventHandler(removeControl));
            cm.MenuItems.Add(removeItem);
            this.ContextMenu = cm;
        }

        void removeControl(object sender, EventArgs e)
        {
            owner.Controls.Remove(this);
            propertyGrid.SelectedObject = null;
        }

        private void updateProperties(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = ((IMyControl)sender).getInfoControl();
        }

        /*
        public String SerializeObject()
        {
            String XmlizedString = null;
            MemoryStream memoryStream = new MemoryStream();
            XmlSerializer xs = new XmlSerializer(typeof(InfoControl));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

            xs.Serialize(xmlTextWriter, infoControl);
            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            return XmlizedString;
        }

        public Object DeserializeObject(String pXmlizedString)
        {
            XmlSerializer xs = new XmlSerializer(typeof(InfoControl));
            MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
            XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            return xs.Deserialize(memoryStream);
        }

        // helper method
        private String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        // helper method
        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }
        */

        public InfoControl getInfoControl()
        {
            return infoControl;
        }

        public void setInfoControl(InfoControl infControl)
        {
            infoControl = infControl;
        }

    }
}
