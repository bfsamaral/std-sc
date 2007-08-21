using System;
using System.Collections.Generic;
using System.Text;

namespace ParaMedDesigner
{
    public interface IMyControl
    {
        InfoControl getInfoControl();
        void setInfoControl(InfoControl infoControl);
    }
}
