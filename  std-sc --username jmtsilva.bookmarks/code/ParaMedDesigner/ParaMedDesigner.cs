using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing;

namespace ParaMedDesigner
{
    public partial class ParaMedDesigner : Form
    {
        public ParaMedDesigner()
        {
            InitializeComponent();
        }

        private void ParaMedDesigner_Load(object sender, EventArgs e)
        {
            Silver.UI.ToolBoxTab tab1 = new Silver.UI.ToolBoxTab("Components", 0);
            toolBox1.AddTab(tab1);

            Silver.UI.ToolBoxItem item = new Silver.UI.ToolBoxItem("Label", 0, true);
            tab1.AddItem(item);

            item = new Silver.UI.ToolBoxItem("TextBox", 0, true);
            tab1.AddItem(item);

            item = new Silver.UI.ToolBoxItem("ComboBox", 0, true);
            tab1.AddItem(item);

            item = new Silver.UI.ToolBoxItem("CheckBox", 0, true);
            tab1.AddItem(item);

            item = new Silver.UI.ToolBoxItem("Picture", 0, true);
            tab1.AddItem(item);


            Silver.UI.ToolBoxTab tab2 = new Silver.UI.ToolBoxTab("Others", 1);
            toolBox1.AddTab(tab2);

            item = new Silver.UI.ToolBoxItem("HotSpot", 0, true);
            tab2.AddItem(item);
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            //String []str = e.Data.GetFormats();
            Silver.UI.ToolBoxItem item = (Silver.UI.ToolBoxItem)e.Data.GetData("Silver.UI.ToolBoxItem");
            if (!item.Caption.Equals("HotSpot")) return;
            MyControlHotSpot hsControl = new MyControlHotSpot(e.X - 550, e.Y - 270, 80, 20, panel1, propertyGrid1, tabControl1);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            Silver.UI.ToolBoxItem item = (Silver.UI.ToolBoxItem)e.Data.GetData("Silver.UI.ToolBoxItem");
            if (item == null) return;
            if (!item.Caption.Equals("HotSpot")) return;

            e.Effect = DragDropEffects.Move;
        }

        private void generateAndSaveXML(object sender, EventArgs e)
        {
            //if(saveFileDialog1.ShowDialog() != DialogResult.OK) return;
            //String fileName = saveFileDialog1.FileName;

            // clean
            rootInfo.removeAllInfoHotSpots();

            // construct object to be serialized with valued data
            for(int hsIdx = 0; hsIdx < panel1.Controls.Count; ++hsIdx)
            {
                MyControlHotSpot hs = (MyControlHotSpot)panel1.Controls[hsIdx];
                InfoHotSpot hsInfo = hs.getInfoHotSpot();
                TabPage mappedTabPage = hs.getTabPage();
                for(int ctrlIdx=0; ctrlIdx<mappedTabPage.Controls.Count; ++ctrlIdx)
                {
                    Control currControl = mappedTabPage.Controls[ctrlIdx];
                    String type = currControl .GetType().ToString();
                    if (!currControl.GetType().ToString().Equals("RectTrackerSharp.RectTracker"))
                    {
                        hsInfo.appendInfoControl( ((IMyControl)currControl).getInfoControl() );
                    }
                }
                rootInfo.appendInfoHotSpot(hsInfo);
            }
            serializedObject = SerializeObject();
        }

        

        private void openAndRegenerateXML(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() != DialogResult.OK) return;

            InfoRoot objectsInfo = (InfoRoot)DeserializeObject(serializedObject);

            //reconstruct the objects
            for(int i = 0; i<objectsInfo.infoHotSpots.Count; ++i)
            {
                // reconstruct hotspot
                InfoHotSpot infohs = (InfoHotSpot)objectsInfo.infoHotSpots[i];  
                new MyControlHotSpot(panel1, propertyGrid1, tabControl1, infohs);               
            }
        }

        //-----------------------------------------------------------------
        private InfoRoot rootInfo = new InfoRoot();
        private String serializedObject;

        private String SerializeObject()
        {
            String XmlizedString = null;
            try
            {
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(InfoRoot));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, rootInfo);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
            } catch (Exception ex)
            {
                String sss = ex.Message;
            }
            return XmlizedString;
        }

        private Object DeserializeObject(String pXmlizedString)
        {
            XmlSerializer xs = new XmlSerializer(typeof(InfoRoot));
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

        private int i = 0;
        private void HELP(object sender, PaintEventArgs e)
        {
            if (tabControl1.SelectedTab == null) return;
            // update all items for controls
            for (int i = 0; i < tabControl1.SelectedTab.Controls.Count; ++i)
            {
                Control currControl = tabControl1.SelectedTab.Controls[i];
                if (!currControl.GetType().ToString().Equals("RectTrackerSharp.RectTracker"))
                    ((IMyControl)currControl).updateItems();
            }
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            propertyGrid1.SelectedObject = null;
        }

    }
}