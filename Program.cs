using System;

namespace Farm
{
    class Program
    {
        static void Main(string[] args)
        {
            CFarm myShop = new CFarm();
            
            Console.WriteLine("Welcome in the repairing shop!");
            int userChoice = 0;
            try
            {
                do
                {
                    Console.WriteLine("=====================================");
                    Console.WriteLine("Select from following option.");
                    Console.WriteLine("1. Put an object in the stock.");
                    Console.WriteLine("2. Repair your object.");
                    Console.WriteLine("3. Show all the list in the shop.");
                    Console.WriteLine("4. Export in a XML file.");
                    Console.WriteLine("5. Import in a XML file.");
                    Console.WriteLine("6. Exit.");
                    Console.WriteLine("=====================================");
                    userChoice = int.Parse(Console.ReadLine());
                    switch (userChoice)
                    {
                        case 1:
                            int typeChoice = 0;
                            do
                            {
                                Console.WriteLine("=====================================");
                                Console.WriteLine("Select from following option.");
                                Console.WriteLine("1. Tiller.");
                                Console.WriteLine("2. GrassTrimmer.");
                                Console.WriteLine("3. LawnMowers.");
                                Console.WriteLine("4. Exit.");
                                Console.WriteLine("=====================================");
                                typeChoice = int.Parse(Console.ReadLine());
                                switch (typeChoice)
                                {
                                    case 1:
                                        Console.WriteLine("=== Creating new tiller ===");
                                        Console.WriteLine();
                                        Console.Write("Order ID - ");
                                        string tcod = Console.ReadLine();
                                        Console.Write("Brand - ");
                                        string tbrand = Console.ReadLine();
                                        Console.Write("Number of wheels - ");
                                        string twheels = Console.ReadLine();
                                        myShop.CreateTiller(tcod,tbrand,twheels);
                                        break;
                                    case 2:
                                        Console.WriteLine("=== Creating new grass trimmer ===");
                                        Console.WriteLine();
                                        Console.Write("Order ID - ");
                                        string gcod = Console.ReadLine();
                                        Console.Write("Brand - ");
                                        string gbrand = Console.ReadLine();
                                        Console.Write("Is Electronic? Press 0 for Yes, Press 1 for No - ");
                                        string eleObj = Console.ReadLine();
                                        if (eleObj.Equals("0"))
                                        {
                                            myShop.CreateTrimmer(gcod, gbrand, true);
                                        }
                                        else if (eleObj.Equals("1"))
                                        {
                                            myShop.CreateTrimmer(gcod, gbrand, false);
                                        }
                                        else
                                            Console.WriteLine("Cannot insert this char. ");
                                        break;
                                    case 3:
                                        Console.WriteLine("=== Creating new lawn mowers ===");
                                        Console.WriteLine();
                                        Console.Write("Order ID - ");
                                        string mcod = Console.ReadLine();
                                        Console.Write("Brand - ");
                                        string mbrand = Console.ReadLine();
                                        Console.Write("Number of wheels - ");
                                        string mwheels = Console.ReadLine();
                                        myShop.CreateTiller(mcod, mbrand, mwheels);
                                        break;
                                    case 4:
                                        break;
                                    default:
                                        Console.WriteLine("Sorry. You have entered wrong choice. Please try again");
                                        break;
                                }
                            } while (typeChoice != 4);
                            break;
                        case 2:
                            Console.WriteLine("Insert code of the repair order: ");
                            string repord = Console.ReadLine();
                            Console.WriteLine("Insert repair price: ");
                            int repprice = int.Parse(Console.ReadLine());
                            Console.WriteLine("Insert motivation for repair. ");
                            string comm = Console.ReadLine();
                            if (myShop.RepairObj(repord, repprice, comm))
                                Console.WriteLine("Object under repair. ");
                            else
                                Console.WriteLine("Cannot repair. Code doesn't exist. ");
                            break;
                        case 3:
                            Console.WriteLine("=== Your shop stock ===");
                            Console.WriteLine();
                            myShop.ToString();
                            break;
                        case 4:
                            int ExpXMLChoice = 0;
                            do
                            {
                                Console.WriteLine("=====================================");
                                Console.WriteLine("Select from following option.");
                                Console.WriteLine("1. Export classic XML.");
                                Console.WriteLine("2. Export with serialization.");
                                Console.WriteLine("3. Exit.");
                                Console.WriteLine("=====================================");
                                ExpXMLChoice = int.Parse(Console.ReadLine());
                                switch (ExpXMLChoice)
                                {
                                    case 1:
                                        myShop.ExportFarmInXML();
                                        break;
                                    case 2:
                                        myShop.SerializeFarm();
                                        break;
                                    case 3:
                                        break;
                                    default:
                                        Console.WriteLine("Sorry. You have entered wrong choice. Please try again");
                                        break;
                                }
                            } while (ExpXMLChoice != 3);
                            break;
                        case 5:
                            int ImpXMLChoice = 0;
                            do
                            {
                                Console.WriteLine("=====================================");
                                Console.WriteLine("Select from following option.");
                                Console.WriteLine("1. Import classic XML.");
                                Console.WriteLine("2. Import with deserialization.");
                                Console.WriteLine("3. Exit.");
                                Console.WriteLine("=====================================");
                                ImpXMLChoice = int.Parse(Console.ReadLine());
                                switch (ImpXMLChoice)
                                {
                                    case 1:
                                        myShop.FarmReaderXML();
                                        break;
                                    case 2:
                                        myShop.DeserializeFarm();
                                        break;
                                    case 3:
                                        break;
                                    default:
                                        Console.WriteLine("Sorry. You have entered wrong choice. Please try again");
                                        break;
                                }
                            } while (ImpXMLChoice != 3);
                            break;
                        case 6:
                            break;
                        default:
                            Console.WriteLine("Sorry. You have entered wrong choice. Please try again");
                            break;
                    }
                } while (userChoice != 6);

            } catch (Exception e)
            {
                Console.WriteLine("Something went wrong. " + e.Message);
            }
        }
    }
}
