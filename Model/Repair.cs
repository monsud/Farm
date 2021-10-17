using System;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Farm.Model
{
    [Serializable()]
    [XmlRoot("Repair")]
    public class Repair
    {
        protected int _price;
        protected string _status;

        public Repair(int price, string comment)
        {
            Price = price;
            Status = comment;
        }
        public Repair() { }
        
        [XmlAttribute(DataType = "int", AttributeName = "Price")]
        public int Price
        {
            get => _price;
            set => _price = value;
        }
        [XmlAttribute(DataType = "string", AttributeName = "Comment")]
        public string Status
        {
            get => _status;
            set => _status = value;
        }

        public override string ToString()
        {
            return " Cost repair" + Price + " Comment: " + Status;
        }
        public virtual XElement toXML()
        {
            XElement element =
                 new XElement("Repair",
                 new XElement("Price", Price),
                 new XElement("Comment", Status));
            return element;
        }
    }
}
