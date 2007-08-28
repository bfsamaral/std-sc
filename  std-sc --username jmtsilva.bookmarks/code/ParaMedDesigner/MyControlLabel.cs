using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ParaMedDesigner
{
    // must implement the interface and delegate in the ""inherited"" control
    public class MyControlLabel: Label, IMyControl
    {
        // multiple inheritance performed with composition
        private MyControl mControl;

        public MyControlLabel(int x, int y, int width, int height, TabPage tab, PropertyGrid propGrid)
        {
            mControl = new MyControl(x, y, width, height, tab, propGrid, this);

            // specific properties
            this.BackColor = Color.Green;

            
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


        public string SerializeObject()
        {
            return mControl.SerializeObject();
        }

        public object DeserializeObject(string pXmlizedString)
        {
            return mControl.DeserializeObject(pXmlizedString);
        }

    }
}
