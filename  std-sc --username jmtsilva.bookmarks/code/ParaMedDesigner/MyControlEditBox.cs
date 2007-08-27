using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ParaMedDesigner
{
    public class MyControlEditBox: TextBox, IMyControl
    {
        private InfoControl infoControl;
        private TabPage owner;
        private PropertyGrid propertyGrid;

        public MyControlEditBox(int x, int y, int width, int height, TabPage tab, PropertyGrid propGrid)
        {
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
