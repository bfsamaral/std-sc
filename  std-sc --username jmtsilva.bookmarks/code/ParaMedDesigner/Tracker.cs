using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ParaMedDesigner
{
    public class Tracker : PictureBox
    {
        private Control control2track;

        public Tracker(Control controlToTrack)
	    {
            BackColor = Color.Green;

            control2track = controlToTrack;
            Size = new Size(10, 10);
            control2track.Controls.Add(this);

            this.Hide();

            this.MouseMove += new MouseEventHandler(Tracker_MouseMove);
	    }

        void Tracker_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button = MouseButtons.Left) 
            {
                                
            }
        }

        public void showTracker() 
        {
            Location = new Point(control2track.Location.X, control2track.Location.Y);
            BringToFront();
            Show();
        }

        public void hideTracker() 
        {
            Hide();
        }


    }
}
