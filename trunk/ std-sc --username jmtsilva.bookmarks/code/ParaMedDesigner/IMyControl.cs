using System;
using System.Collections.Generic;
using System.Text;

namespace ParaMedDesigner
{
    public interface IMyControl
    {
        InfoControl getInfoControl();
        void setInfoControl(InfoControl infoControl);

        // added because of a combobox event problem!! deriva de list e n de control!! -- stupid event
        bool mControlNotNull();

        void updateItems();
    }
}
