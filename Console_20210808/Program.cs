using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_20210808
{
    class Program
    {
        static void Main(string[] args)
        {
            //dynamic function =(Func<dynamic,dynamic>) (x => x * 2);
            //Console.WriteLine(function(0.75));

            Predicate<int> predicate = (x => x < 15);

            List<int> list = new List<int>();
            IList list2 = list;
            Console.WriteLine(list2.IsFixedSize);


            dynamic souce = new List<dynamic>
            {
                5,
                2.75,
                TimeSpan.FromSeconds(45)
            };
            
            new List<int>().Find(predicate);

            

            Console.ReadKey();

        }
    }
}
