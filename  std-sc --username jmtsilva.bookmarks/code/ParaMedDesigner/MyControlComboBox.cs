using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace ParaMedDesigner
{
    public class MyControlComboBox: ComboBox, IMyControl
    {
        private MyControl mControl;
        public bool mControlNotNull() { return mControl != null; }

        public MyControlComboBox(int x, int y, int width, int height, TabPage tab, PropertyGrid propGrid)
        {
            mControl = new MyControl(x, y, width, height, tab, propGrid, this);
            mControl.getInfoControl().Type = "ComboBox";
            
            
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
            Items.Clear();

            InfoControl ic = mControl.getInfoControl();
            StringCollection aux = ic.ElemsArrayList;

            String[] aux2 = new String[aux.Count];
            for (int i = 0; i < aux.Count; ++i)
            {
                if (i == 0)
                    Text = aux[i];
                aux2[i] = aux[i];
            }
            Items.AddRange(aux2);
        }
    }
}
