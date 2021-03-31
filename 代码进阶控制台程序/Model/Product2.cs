using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace 代码进阶控制台程序.Model
{
    public class Product2
    {
        string name;
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        decimal price;
        public decimal Price
        {
            get { return price; }
            private set { price = value; }
        }

        public Product2(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }
        public static List<Product2> GetSampleProducts()
        {
            List<Product2> list = new List<Product2>();
            list.Add(new Product2("West Side Story", 9.99m));
            list.Add(new Product2("Assassins", 14.99m));
            list.Add(new Product2("Froga", 13.99m));
            list.Add(new Product2("Sweeney Todd", 10.99m));
            return list;
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", name, price);
        }

        /// <summary>
        /// 测试排序代码
        /// </summary>
        public static void TestSortCodeComparer()
        {

            List<Product2> products = GetSampleProducts();
            #region 排序

            //products.Sort(new ProductNameComparer2());
            //foreach (Product2 product in products)
            //{
            //    Console.WriteLine(product);
            //}
            #endregion

            #region 根据价格排序方法1
            Predicate<Product2> test = delegate (Product2 p) { return p.Price > 10m; };
            List<Product2> matches = products.FindAll(test);

            Action<Product2> print = Console.WriteLine;
            //matches.ForEach(print);
            //等同于matches.ForEach(p => Console.WriteLine(p));
            #endregion

            #region 根据价格排序方法2
            //products.FindAll(delegate (Product2 p) { return p.Price > 10m; }).ForEach(Console.WriteLine);

            #endregion

            #region Linq查询

            var filtered = from product in products
                           where product.Price > 10m
                           select product;
            foreach (Product2 producrt in filtered)
            {
                Console.WriteLine(producrt);
            }
            #endregion
        }
    }

    class ProductNameComparer2 : IComparer<Product2>
    {
        public int Compare([AllowNull] Product2 x, [AllowNull] Product2 y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
