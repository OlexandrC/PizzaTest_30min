using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace AvengaPizaTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string FileName = "Test task #1 - Pizzas.json";
            string FileToRead = AppDomain.CurrentDomain.BaseDirectory + FileName;

            //string FullText = File.ReadAllText(FileToRead);
            //List<string> pizza = new List<string>();

            Dictionary<string, int> ToppingCalculation = new Dictionary<string, int>();
            List<string> ToppingNames = new List<string>();

            using (StreamReader r = new StreamReader(FileToRead))
            {
                string json = r.ReadToEnd();
                List<object> items = JsonConvert.DeserializeObject<List<object>>(json);

                foreach(object item in items)
                {
                    string[] words = item.ToString().Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                    string toppingName = "";
                    foreach (string word in words)
                    {
                        toppingName = word.Replace("[", "");
                        toppingName = toppingName.Replace("]", "");
                        toppingName = toppingName.Replace("(", "");
                        toppingName = toppingName.Replace(")", "");
                        toppingName = toppingName.Replace(":", "");
                        toppingName = toppingName.Replace(" ", "");
                        toppingName = toppingName.Replace("\r\n", "");
                        toppingName = toppingName.Replace("{", "");
                        toppingName = toppingName.Replace("}", "");
                        toppingName = toppingName.Replace("\\", "");
                        toppingName = toppingName.Replace("\"", "");
                        
                        if (toppingName.Contains("toppings")) { continue; }
                        if (toppingName.Length == 0) { continue; }

                        if (ToppingCalculation.ContainsKey(toppingName)){
                            ToppingCalculation[toppingName] = ToppingCalculation[toppingName] + 1;
                        }
                        else
                        {
                            ToppingNames.Add(toppingName);
                            ToppingCalculation.Add(toppingName, 1);
                        }
                    }
                }
            }

            Console.WriteLine("Toppings popularity:");
            foreach (string toppingName in ToppingNames)
            {
                
                Console.WriteLine(toppingName + " = " + ToppingCalculation[toppingName]);
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
