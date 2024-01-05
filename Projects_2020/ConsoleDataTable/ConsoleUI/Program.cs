using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Order> orders = new List<Order>
            {
                new Order("Saint's Place", "PC Tower", 725, 8),
                new Order("Acme Software", "Adobel", 25, 3),
                new Order("Lupa's PC Repair", "Keyboard", 78, 3),
                new Order("Jean Realty", "Macbook", 2100, 2),
                new Order("Dev Flights", "Slim Drones", 5800, 2),
                new Order("Code All Day", "Youtube Videos", 525, 1),
                new Order("Morning Coder", "Square Reader", 45, 1),
            };

            var distinctOrders = orders.GroupBy(o => o.CustomerName).Select(o => o.First());

            DataTableFormatter.PrintRow("Customer Name","Item","Price","Quantity", "Total");
            DataTableFormatter.LineSeparator();

            foreach (Order name in distinctOrders)
            {

                for (int i = 0; i < orders.Count; i++)
                {
                    if (name.CustomerName == orders[i].CustomerName)
                    {
                        DataTableFormatter.PrintRow(name.CustomerName, name.Item, name.Price.ToString(), name.Quantity.ToString(), (name.Price * name.Quantity).ToString());
                    }
                }
            }
            Console.ReadLine();
        }
    }

}
