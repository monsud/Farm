using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Farm.Model
{
    [Serializable()]
    [XmlRoot("Farmer")]
    [XmlInclude(typeof(Tiller))]
    [XmlInclude(typeof(GrassTrimmer))]
    [XmlInclude(typeof(LawnMowers))]

    public class ObjFarm
        {
        protected string _orderID; 
        protected string _brand;
        protected int _totcostrepair;
        public List<Repair> listRep;
 
        public ObjFarm() {
            _totcostrepair = 0;
            listRep = new List<Repair>();
        }
        [XmlAttribute(DataType = "string", AttributeName = "OrderID")]
        public string OrderID
        {
            get => _orderID;
            set => _orderID = value;
        }
        [XmlAttribute(DataType = "string", AttributeName = "Brand")]
        public string Brand
        {
            get => _brand;
            set => _brand = value;
        }
        [XmlAttribute(DataType = "int", AttributeName = "TotalCostRiparation")]
        public int TotCostRepair
        {
            get => _totcostrepair;
            set => _totcostrepair = value;
        }
        public override string ToString()
        {
            return " OrderID: " + OrderID + " Brand: " + Brand +  " TotRepair: " + TotCostRepair;
        }

        public virtual XElement toXML()
        {
            XElement element =
                 new XElement("Farmer",
                 new XElement("OrderID", OrderID),
                 new XElement("Brand", Brand),
                 new XElement("TotalCostRiparation", TotCostRepair));
            return element;
        }
    }
}
