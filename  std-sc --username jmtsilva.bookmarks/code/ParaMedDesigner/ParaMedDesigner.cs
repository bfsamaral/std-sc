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

            for (int i = 0; i < tabControl1.Controls.Count; ++i )
            {
                for(int j=0; j<((TabPage)tabControl1.Controls[i]).Controls.Count ; ++j)
                {
                    Control mControl = (Control)((TabPage)tabControl1.Controls[i]).Controls[j];
                    String type = mControl.GetType().ToString();
                    if (!mControl.GetType().ToString().Equals("RectTrackerSharp.RectTracker"))
                    {
                        String res = ((IMyControl)mControl).SerializeObject();
                    }
                }
            }
        }

        private void openAndRegenerateXML(object sender, EventArgs e)
        {
            //if (openFileDialog1.ShowDialog() != DialogResult.OK) return;
        }

    }
}