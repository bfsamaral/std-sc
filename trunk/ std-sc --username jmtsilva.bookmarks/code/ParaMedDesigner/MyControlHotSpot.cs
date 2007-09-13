using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ParaMedDesigner
{
    class MyControlHotSpot: PictureBox
    {
        private InfoHotSpot info;
        private Panel owner;
        private PropertyGrid propertyGrid;
        private TabControl tabControl;
        private TabPage myTabPage;

        public MyControlHotSpot(int x, int y, int width, int height, Panel owner, PropertyGrid propGrid, TabControl tControl)
        {
            info = new InfoHotSpot(this);
            info.X = x;
            info.Y = y;
            info.Width = width;
            info.Height = height;
            propertyGrid = propGrid;
            tabControl = tControl;
            this.owner = owner;
            owner.Controls.Add(this);

            Load(@"..\..\images\hotSpot_green.gif");

            // context menu
            ContextMenu cm = new ContextMenu();
            MenuItem removeItem = new MenuItem("&Remove", new EventHandler(removeHotSpot));
            cm.MenuItems.Add(removeItem);
            this.ContextMenu = cm;

            // add a new tabpage
            myTabPage = new TabPage("<no name>");
            myTabPage.BackColor = Color.White;
            myTabPage.AllowDrop = true;
            myTabPage.DragDrop += new DragEventHandler(myTabPage_DragDrop);
            myTabPage.DragEnter += new DragEventHandler(myTabPage_DragEnter);
            myTabPage.GotFocus+=new EventHandler(myTabPage_GotFocus);
            this.Click += new EventHandler(MyControlHotSpot_Click);

            tabControl.TabPages.Add(myTabPage);
        }

        public MyControlHotSpot(Panel owner, PropertyGrid propGrid, TabControl tControl, InfoHotSpot infohs)
            : this(infohs.X, infohs.Y, infohs.Width, infohs.Height, owner, propGrid, tControl)
        {
            // store new info
            info = infohs;

            // reconstruct controls for the hotspot
            for(int j=0; j<info.infoControls.Count; ++j)
            {
                InfoControl ic = ((InfoControl)(info.infoControls[j]));
                generateControl(ic.Y, ic.X, ic.Type);
            }
        }

        void myTabPage_GotFocus(object sender, EventArgs e)
        {
            propertyGrid.SelectedObject = null;
        }

        void MyControlHotSpot_Click(object sender, EventArgs e)
        {
            tabControl.SelectedTab = myTabPage;
            propertyGrid.SelectedObject = info;
        }

        void myTabPage_DragEnter(object sender, DragEventArgs e)
        {
            Silver.UI.ToolBoxItem item = (Silver.UI.ToolBoxItem)e.Data.GetData("Silver.UI.ToolBoxItem");
            if (item == null) return;
            if (item.Caption.Equals("HotSpot")) return;
            
            e.Effect = DragDropEffects.Move;
        }

        void myTabPage_DragDrop(object sender, DragEventArgs e)
        {
            Silver.UI.ToolBoxItem item = (Silver.UI.ToolBoxItem)e.Data.GetData("Silver.UI.ToolBoxItem");
            if (item.Caption.Equals("HotSpot")) return;

            String controlName = item.Caption;
            // JMTS parse the sender to the object and build infocontrol with img
            //Console.WriteLine("coord: " + e.X + " " + e.Y);

            Point clientCoords = PointToClient(new Point(e.X, e.Y));
            generateControl(5, 5, controlName);
        }

        private void generateControl(int x, int y, String controlName)
        {
            //Console.WriteLine("client coord: " + clientCoords.X + " " + clientCoords.Y);
            if (controlName.Equals("Label"))
                new MyControlLabel(x, y, 80, 20, myTabPage, propertyGrid);
            else
                if (controlName.Equals("TextBox"))
                    new MyControlTextBox(x, y, 80, 20, myTabPage, propertyGrid);
                else
                    if (controlName.Equals("ComboBox"))
                        new MyControlComboBox(x, y, 80, 20, myTabPage, propertyGrid);
                    else
                        if (controlName.Equals("CheckBox"))
                            new MyControlCheckBox(x, y, 80, 20, myTabPage, propertyGrid);
                        else
                            if (controlName.Equals("PictureBox"))
                                new MyControlPictureBox(x, y, 80, 20, myTabPage, propertyGrid);
        }

        void removeHotSpot(object sender, EventArgs e)
        {
            tabControl.TabPages.Remove(myTabPage);
            owner.Controls.Remove(this);
            propertyGrid.SelectedObject = null;
        }
        

        void MyControlHotSpot_MouseClick(object sender, MouseEventArgs e)
        {

        }

        public InfoHotSpot getInfoHotSpot()
        {
            return info;
        }


        public Info getInfo()
        {
            return (Info)info;
        }

        public void setInfo(Info infControl)
        {
            info = (InfoHotSpot)infControl;
        }

        public void changeTabName(String newName) 
        {
            myTabPage.Text = newName;
        }

        public TabPage getTabPage() 
        {
            return myTabPage;
        }
    }
}
