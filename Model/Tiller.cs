using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Farm.Model
{
    [Serializable()]
    [XmlRoot("Tiller")]
    public class Tiller : ObjFarm
    {
        protected string _nwheels;
        public Tiller() { }

        [XmlAttribute(DataType = "string", AttributeName = "NumberOfWheels")]
        public string NumWheels
        {
            get => _nwheels;
            set => _nwheels = value;
        }
        public override string ToString()
        {
            return " TILLER == " + base.ToString() + " NumWheels: " + NumWheels;
        }
        public override XElement toXML()
        {
            XElement elementBase = base.toXML();
            elementBase.Add(new XElement("Type", "Tiller"));
            elementBase.Add(new XElement("NumberOfWheels", NumWheels));
            return elementBase;
        }
    }
}
