using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicTest___json.Net
{
    class Program
    {
        static void Main(string[] args)
        {
            string json = @"
            {

                'name':'Jon Skeet',
                'address':{
                    'town':'Reading',
                    'country':'UK'
                }
            }".Replace('\'','"');

            //静态类型视图
            JObject obj1 = JObject.Parse(json);

            Console.WriteLine(obj1["address"]["town"]);

            //动态类型视图
            dynamic obj2 = obj1;
            Console.WriteLine(obj2.address.town);

            Console.ReadKey();
        }
    }
}
