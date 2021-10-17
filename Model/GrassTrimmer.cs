using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Farm.Model
{
    [Serializable()]
    [XmlRoot("GrassTrimmer")]
    public class GrassTrimmer : ObjFarm
    {
        [XmlAttribute(DataType = "bool", AttributeName = "StatusType")]
        protected bool _isElectronic = false;
        public GrassTrimmer() { }

        public bool IsElectronic()
        {
            return _isElectronic;
        }
        public void SetIsElectronic(bool newStatus)
        {
            _isElectronic = newStatus;
        }
        public override string ToString()
        {
            return " GRASSTRIMMER == " + base.ToString();
        }

        public override XElement toXML()
        {
            XElement elementBase = base.toXML();
            elementBase.Add(new XElement("Type", "GrassTrimmer"));
            elementBase.Add(new XElement("StatusType", IsElectronic()));
            return elementBase;
        }
    }
}
