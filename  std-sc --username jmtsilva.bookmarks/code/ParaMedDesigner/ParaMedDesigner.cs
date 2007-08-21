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
            e.Effect = DragDropEffects.Move;
        }

        private void tabPage1_DragDrop(object sender, DragEventArgs e)
        {
            // JMTS parse the sender to the object and build infocontrol with img
            MyControlLabel infoControl = new MyControlLabel(e.X - 300, e.Y - 300, 80, 20);
            //infoControl.Load(@"E:\______________MYDOCS\PFC\google_prj\code\ParaMedDesigner\images\label.bmp");
            infoControl.Click += new EventHandler(infoControl_Click);
            //infoControl.MouseMove += new MouseEventHandler(infoControl_MouseDown);

            tabPage1.Controls.Add(infoControl);

        }

        void infoControl_MouseDown(object sender, MouseEventArgs e)
        {
            ((MyControlLabel)sender).Location = new Point(e.X, e.Y);
        }

        void infoControl_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = ((IMyControl)sender).getInfoControl();
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            MyControlEditBox hsControl = new MyControlEditBox(e.X - 550, e.Y - 270, 80, 20);
            //hsControl.Load(@"E:\______________MYDOCS\PFC\google_prj\code\ParaMedDesigner\images\hotSpot_green.gif");
            hsControl.Click += new EventHandler(infoControl_Click);
            panel1.Controls.Add(hsControl);
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
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