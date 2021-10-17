using System;
using System.Collections.Generic;
using System.Linq;
using Farm.Model;
using System.Threading;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Configuration;

namespace Farm
{
    public class CFarm
    {
        // Variabile contatore per impedire l'accesso a massimo 10 lavoratori
        private static int cont = 0;
        // Variabile che utilizzo per ricreare un ID di 5 elementi
        private static int dim = 5;
        private List<ObjFarm> _farmList;
        public CFarm ()
        {
            _farmList = new List<ObjFarm>();
        }
        #region Core functions
        public bool IsAlreadyExisting (string cod)
        {
            bool alreadyExist = _farmList.Any(x => x.OrderID == cod);
            return alreadyExist;
        }
        public bool RepairObj(string cod, int price, string comment)
        {
            Repair myRep = new Repair(price, comment);

            if ((_farmList != null) && (cont < 10))
            {
                ObjFarm filteredObjectFarm = _farmList.SingleOrDefault(x => x.OrderID == cod);
                if (filteredObjectFarm != null)
                {
                    filteredObjectFarm.listRep.Add(myRep);
                    filteredObjectFarm.TotCostRepair = filteredObjectFarm.listRep.Sum(x => x.Price);
                    cont++;
                    return true;
                }
                else
                    return false;
            }
            else
                throw new Exception("Cannot repair because list is null or cannot repair 11+ objects or this code doesn't exist."); 
        }
        // Funzione da completare per l'utilizzo del multithreading
        public void RunFarmer()
        {
            Thread myFarmer = new Thread(() => RepairObj("", 0, ""));
            myFarmer.Start(new Parameter());
        }
        #endregion
        #region Creation functions
        public void CreateTiller(string id, string brand, string nwheels)
        {
            try
            {
                Tiller tiller = new Tiller();
                var rand = new Random();
                var stringChars = new char[dim];
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                if (!IsAlreadyExisting(id))
                {
                    tiller.OrderID = id;
                    tiller.Brand = brand;
                    tiller.NumWheels = nwheels;
                    _farmList.Add(tiller);
                }
                else
                {
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[rand.Next(chars.Length)];
                    }
                    tiller.OrderID = new string(stringChars);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public void CreateTrimmer(string id, string brand, bool myState)
        {
            try
            {
                GrassTrimmer trimmer = new GrassTrimmer();
                var rand = new Random();
                var stringChars = new char[dim];
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                if (!IsAlreadyExisting(id))
                {
                    trimmer.OrderID = id;
                    trimmer.Brand = brand;
                    trimmer.SetIsElectronic(myState);
                    _farmList.Add(trimmer);
                }
                else
                {
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[rand.Next(chars.Length)];
                    }
                    trimmer.OrderID = new string(stringChars);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CreateLawn(string id, string brand, string nwheels)
        {
            try
            {
                LawnMowers lawn = new LawnMowers();
                var rand = new Random();
                var stringChars = new char[dim];
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                if (!IsAlreadyExisting(id))
                {
                    lawn.OrderID = id;
                    lawn.Brand = brand;
                    lawn.NumWheels = nwheels;
                    _farmList.Add(lawn);
                }
                else
                {
                    for (int i = 0; i < stringChars.Length; i++)
                    {
                        stringChars[i] = chars[rand.Next(chars.Length)];
                    }
                    lawn.OrderID = new string(stringChars);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion
        #region XML functions
        public void ExportFarmInXML()
        {
            if (_farmList.Count > 0)
            {
                XElement xlement = new XElement("Farmer");
                foreach (ObjFarm fobj in _farmList)
                {
                    xlement.Add(fobj.toXML());
                    XElement xlementRep = new XElement("Repair");
                    foreach (Repair robj in fobj.listRep)
                    {
                        xlementRep.Add(robj.toXML());
                    }
                }
                xlement.Save("farmout.xml");
                string str = File.ReadAllText("farmout.xml");
                Console.WriteLine(str);
            }
            else
                throw new Exception("List is null. Cannot write a null list");
        }

        public void FarmReaderXML()
        {
            string sourcePath = ConfigurationManager.AppSettings["InputFile"];
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(sourcePath);
            XmlNodeList nodeList = xdoc.GetElementsByTagName("Farmer");

            foreach (XmlNode node in nodeList)
            {
                dynamic obj = null;
                string typeObj = node.SelectSingleNode("Type").InnerText;
                switch (typeObj)
                {
                    case "Tiller":
                        obj = new Tiller();
                        obj.NumWheels = node.SelectSingleNode("NumberOfWheels").InnerText;
                        break;
                    case "GrassTrimmer":
                        obj = new GrassTrimmer();
                        obj._IsElectronic = node.SelectSingleNode("StatusType").InnerText;
                        break;
                    case "LawnMowers":
                        obj = new LawnMowers();
                        obj.NumWheels = node.SelectSingleNode("NumberOfWheels").InnerText;
                        break;
                }
                obj.OrderID = node.SelectSingleNode("OrderID").InnerText;
                obj.Brand = node.SelectSingleNode("Brand").InnerText;
                obj.TotCostRepair = node.SelectSingleNode("TotalCostRiparation").InnerText;
                XmlNodeList repList = xdoc.GetElementsByTagName("Repair");
                foreach (XmlNode nodeRep in repList)
                {
                    Repair rep = new Repair(0, "");
                    rep.Price = Convert.ToInt32(nodeRep.SelectSingleNode("Price").InnerText);
                    rep.Status = nodeRep.SelectSingleNode("Comment").InnerText;
                    foreach (ObjFarm fobj in _farmList)
                    {
                        fobj.listRep.Add(rep);
                    }
                }
                _farmList.Add(obj);
            }
        }
        #endregion
        #region XML Serialized
        public void SerializeFarm()
        {
            string sourcePath = ConfigurationManager.AppSettings["OutputFile"];
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObjFarm>));
            TextWriter writer = new StreamWriter(sourcePath);
            serializer.Serialize(writer, _farmList);
            writer.Close();
        }

        public void DeserializeFarm()
        {
            List<ObjFarm> farms = null;
            string sourcePath = ConfigurationManager.AppSettings["InputFile"];
            XmlSerializer serializer = new XmlSerializer(typeof(List<ObjFarm>));
            StreamReader reader = new StreamReader(sourcePath);
            farms = (List<ObjFarm>)serializer.Deserialize(reader);
            reader.Close();
        }
        #endregion
    }
}
