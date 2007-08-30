using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using RectTrackerSharp;


namespace ParaMedDesigner
{

    public class MyControl
    {
        private InfoControl infoControl;
        private TabPage owner;
        private PropertyGrid propertyGrid;
        private Control bindControl;

        private RectTrackerSharp.RectTracker rectTracker;



        public MyControl(int x, int y, int width, int height, TabPage tab, PropertyGrid propGrid, Control control)
        {
            bindControl = control;

            infoControl = new InfoControl(bindControl);
            infoControl.X = x;
            infoControl.Y = y;
            infoControl.Width = width;
            infoControl.Height = height;

            owner = tab;
            propertyGrid = propGrid;

            bindControl.Click += new EventHandler(updateProperties);
            bindControl.MouseMove += new MouseEventHandler(doMouseMove);
            bindControl.SizeChanged += new EventHandler(bindControl_SizeChanged);
            bindControl.LocationChanged += new EventHandler(bindControl_LocationChanged);
            owner.Controls.Add(bindControl);

            // context menu
            ContextMenu cm = new ContextMenu();
            MenuItem removeItem = new MenuItem("&Remove", new EventHandler(removeControl));
            cm.MenuItems.Add(removeItem);
            bindControl.ContextMenu = cm;

            // tracker related

            rectTracker = new RectTracker(bindControl);
            owner.Controls.Add(rectTracker);
            rectTracker.Visible = false;
        }

        void bindControl_LocationChanged(object sender, EventArgs e)
        {
            infoControl.setX (this.bindControl.Location.X);
            infoControl.setY (this.bindControl.Location.Y);
            if (((IMyControl)sender).mControlNotNull())
                updateProperties(sender, e);    
        }

        void bindControl_SizeChanged(object sender, EventArgs e)
        {
            infoControl.setWidth(this.bindControl.Size.Width);
            infoControl.setHeight(this.bindControl.Size.Height);
            if(((IMyControl)sender).mControlNotNull())
                updateProperties(sender, e);
        }

        public void doMouseMove(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.SizeAll;
            rectTracker.BringToFront();
            rectTracker.Draw();
            rectTracker.Visible = true;


            /*
            // simulate dragdrop operation
            if (e.Button == MouseButtons.Left)
            {
                //DragDropEffects d = this.DoDragDrop(infoControl, DragDropEffects.Move);
                //String r = d.ToString();
                bindControl.Location = new Point(bindControl.Location.X + 5, bindControl.Location.Y + 5);
            }
            */
            //tracker.showTracker();
        }

        public void removeControl(object sender, EventArgs e)
        {
            owner.Controls.Remove(bindControl);
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

        public void setInfoControl(InfoControl infoControl)
        {
            this.infoControl = infoControl;
        }
        
    }
}
