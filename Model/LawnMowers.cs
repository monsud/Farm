using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Farm.Model
{
    [Serializable()]
    [XmlRoot("LawnMowers")]
    public class LawnMowers : ObjFarm
        {
        protected string _nwheels;
        public LawnMowers() {}
        [XmlAttribute(DataType = "string", AttributeName = "NumberOfWheels")]
        public string NumWheels
            {
                get => _nwheels;
                set => _nwheels = value;
            }
            public override string ToString()
            {
            return " LAWNMOWERS == " + base.ToString() + " NumWheels: " + NumWheels;
            }

        public override XElement toXML()
        {
            XElement elementBase = base.toXML();
            elementBase.Add(new XElement("Type", "LawnMowers"));
            elementBase.Add(new XElement("NumberOfWheels", NumWheels));
            return elementBase;
        }
    }    
}
