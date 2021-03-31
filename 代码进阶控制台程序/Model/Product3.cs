using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 代码进阶控制台程序.Model
{
    public class Product3
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }

        public Product3(string name, decimal price)
        {
            this.Name = name;
            this.Price = price;
        }

        Product3() { }

        public static List<Product3> GetSampleProducts()
        {
            return new List<Product3>()
            {
                new Product3("West Side Story", 9.99m),
                new Product3("Assassins", 14.99m),
                new Product3("Froga", 13.99m),
                new Product3("Sweeney Todd", 10.99m)
            };
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Name, Price);
        }

        /// <summary>
        /// 测试排序代码
        /// </summary>
        public static void TestSortCodeComparer()
        {

            List<Product3> products = GetSampleProducts();
            #region 根据名字排序方法1
            //products.Sort((x, y) => x.Name.CompareTo(y.Name));
            //foreach (Product3 product in products)
            //{
            //    Console.WriteLine(product);
            //}
            #endregion

            #region 根据名字排序方法2
            //foreach (Product3 product in products.OrderBy(p => p.Name))
            //{
            //    Console.WriteLine(product);
            //}

            #region 复习排序
            List<int> stringList = new List<int>() { 55, 99, 150, 35, 810, 55 };
            stringList.Sort((x, y) => { return x != y ? x > y ? 1 : -1 : 0; });
            foreach (var item in stringList)
            {
                Console.WriteLine(item);
            }
            #endregion

            #endregion

            #region 根据价格排序
            foreach (Product3 product in products.Where(p => p.Price > 10m))
            {
                Console.WriteLine(product);
            }
            #endregion

        }
    }
}
