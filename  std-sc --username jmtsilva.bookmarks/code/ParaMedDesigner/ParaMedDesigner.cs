using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

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

            item = new Silver.UI.ToolBoxItem("EditBox", 0, true);
            tab1.AddItem(item);

            item = new Silver.UI.ToolBoxItem("TextBox", 0, true);
            tab1.AddItem(item);

            Silver.UI.ToolBoxTab tab2 = new Silver.UI.ToolBoxTab("Others", 1);
            toolBox1.AddTab(tab2);

            item = new Silver.UI.ToolBoxItem("HotSpot", 0, true);
            tab2.AddItem(item);
        }

        private void tabPage1_DragEnter(object sender, DragEventArgs e)
        {
            Silver.UI.ToolBoxItem item = (Silver.UI.ToolBoxItem)e.Data.GetData("Silver.UI.ToolBoxItem");
            if (item == null) return;
            if (item.Caption.Equals("HotSpot")) return;

            e.Effect = DragDropEffects.Move;
        }

        private void tabPage1_DragDrop(object sender, DragEventArgs e)
        {
            Silver.UI.ToolBoxItem item = (Silver.UI.ToolBoxItem)e.Data.GetData("Silver.UI.ToolBoxItem");
            if (item.Caption.Equals("HotSpot")) return;
            
            // JMTS parse the sender to the object and build infocontrol with img
            //Console.WriteLine("coord: " + e.X + " " + e.Y);
            Point clientCoords = PointToClient(new Point(e.X, e.Y));
            //Console.WriteLine("client coord: " + clientCoords.X + " " + clientCoords.Y);
            if (item.Caption.Equals("Label")) 
                new MyControlLabel(clientCoords.X - 130, clientCoords.Y - 50, 80, 20, tabPage1, propertyGrid1);
            else
                if (item.Caption.Equals("EditBox")) 
                    new MyControlEditBox(clientCoords.X - 130, clientCoords.Y - 50, 80, 20, tabPage1, propertyGrid1);
            //Console.WriteLine("res: " + (clientCoords.X - 130) + " " + (clientCoords.Y-50));
        }
        
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            //String []str = e.Data.GetFormats();
            Silver.UI.ToolBoxItem item = (Silver.UI.ToolBoxItem)e.Data.GetData("Silver.UI.ToolBoxItem");
            if (!item.Caption.Equals("HotSpot")) return;
            MyControlHotSpot hsControl = new MyControlHotSpot(e.X - 550, e.Y - 270, 80, 20, panel1, propertyGrid1);
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
            
            String fileName = saveFileDialog1.FileName;
            for (int i = 0; i < tabPage1.Controls.Count; ++i )
            {
                MyControlLabel c = ((MyControlLabel)tabPage1.Controls[i]);
                //String res = c.SerializeObject();
            }
        }

        private void openAndRegenerateXML(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
        }

    }
}