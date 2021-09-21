using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace AvengaPizaTest
{
    class Program
    {
        static string FileName = "Test task #1 - Pizzas.json";
        static string FileToRead = AppDomain.CurrentDomain.BaseDirectory + FileName;

        static List<Pizza> AllPizzas = new List<Pizza>();
        static Dictionary<Pizza, int> PopularToppings = new Dictionary<Pizza, int>();

        static void Main(string[] args)
        {
            SetInputDatas();

            ReadFile();

            Calculate();

            PrintResult();
        }

        static void SetInputDatas()
        {
            FileName = "Test task #1 - Pizzas.json";
            FileToRead = AppDomain.CurrentDomain.BaseDirectory + FileName;
        }

        static void ReadFile()
        {
            using (StreamReader r = new StreamReader(FileToRead))
            {
                string json = r.ReadToEnd();
                AllPizzas = JsonConvert.DeserializeObject<List<Pizza>>(json);
            }
        }

        static void Calculate()
        {
            foreach (Pizza pizza in AllPizzas)
            {
                Pizza basePizzaToRate;
                if(IsExists(pizza, out basePizzaToRate))
                {
                    PopularToppings[basePizzaToRate] = PopularToppings[basePizzaToRate] + 1;
                }
                else
                {
                    PopularToppings.Add(pizza, 1);
                }
            }
        }

        static bool IsExists(Pizza pizza, out Pizza basePizzaToRate)
        {
            basePizzaToRate = null;
            foreach (Pizza PizzaFromRate in PopularToppings.Keys)
            {
                int foundedItems = 0;
                foreach(string topping in pizza.Toppings)
                {
                    if (PizzaFromRate.Toppings.Contains(topping)) { foundedItems++; }
                }

                if(foundedItems == pizza.Toppings.Count) {
                    basePizzaToRate = PizzaFromRate;
                    return true; 
                }
            }

            return false;
        }

        static void PrintResult()
        {
            Console.WriteLine("Receipts popularity");
            Console.WriteLine("Receipts amount " + PopularToppings.Count.ToString());

            var SortedPopularToppings = PopularToppings.OrderByDescending(key => key.Value).ToDictionary(p=>p.Key);

            foreach (Pizza pizza in SortedPopularToppings.Keys)
            {
                Console.WriteLine(string.Format("Popularity: {0} Receipt: {1}", PopularToppings[pizza], string.Join(", ", pizza.Toppings)));
            }

            Console.WriteLine("Press any key to exit");

            Console.ReadKey();
        }
    }
}
