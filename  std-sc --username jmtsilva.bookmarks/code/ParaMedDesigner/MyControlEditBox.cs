using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ParaMedDesigner
{
    public class MyControlEditBox: TextBox, IMyControl
    {
        private InfoControl infoControl;

        public MyControlEditBox(int x, int y, int width, int height)
        {
            infoControl = new InfoControl(this);
            infoControl.X = x;
            infoControl.Y = y;
            infoControl.Width = width;
            infoControl.Height = height;
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
