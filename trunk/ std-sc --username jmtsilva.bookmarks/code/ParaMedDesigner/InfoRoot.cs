using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace ParaMedDesigner
{
    [XmlRootAttribute("InfoRoot", Namespace="", IsNullable=false)]
    public class InfoRoot
    {
        // mandatory for serialization
        public InfoRoot()
        {

        }
        //-------------------------------------
        // serialize purpose
        //-------------------------------------
        [XmlArray("InfoHotSpot"), XmlArrayItem("InfoX", typeof(InfoHotSpot))]
        public ArrayList infoHotSpots = new ArrayList();
        public void appendInfoHotSpot(InfoHotSpot infoHotSpot)
        {
            infoHotSpots.Add(infoHotSpot);
        }
        public void removeAllInfoHotSpots()
        {
            infoHotSpots.RemoveRange(0, infoHotSpots.Count);
        }
        //-------------------------------------

    }
}
