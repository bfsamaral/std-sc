using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ParaMedDesigner
{
    public class MyControlCheckBox: CheckBox, IMyControl
    {
        private MyControl mControl;
        public bool mControlNotNull() { return mControl != null; }

        public MyControlCheckBox(int x, int y, int width, int height, TabPage tab, PropertyGrid propGrid)
        {
            mControl = new MyControl(x, y, width, height, tab, propGrid, this);
            mControl.getInfoControl().Type = "CheckBox";

            Text = "some text here?";

        }

        public void doMouseMove(object sender, MouseEventArgs e)
        {
            mControl.doMouseMove(sender, e);
        }

        public void removeControl(object sender, EventArgs e)
        {
            mControl.removeControl(sender, e);
        }

        private void updateProperties(object sender, EventArgs e)
        {
            updateProperties(sender, e);
        }

        public InfoControl getInfoControl()
        {
            return mControl.getInfoControl();
        }

        public void setInfoControl(InfoControl infControl)
        {
            mControl.setInfoControl(infControl);
        }

        public void updateItems()
        {
        }

    }
}
