using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace 代码进阶控制台程序
{
    public class Product
    {
        string name;
        public string Name { get { return name; } }

        //注意：必须要带“m”,否则将和标准的浮点类型一样。而我们要求的却是
        //用来计算货币类的浮点数，但是可以给其赋整数值。
        // decimal 对小数的运算更加的准确。
        decimal price;
        public decimal Price { get { return price; } }

        public Product(string name, decimal price)
        {
            this.name = name;
            this.price = price;
        }

        public static ArrayList GetSampleProducts()
        {
            ArrayList list = new ArrayList();
            list.Add(new Product("West Side Story", 9.99m));
            list.Add(new Product("Assassins", 14.99m));
            list.Add(new Product("Froga", 13.99m));
            list.Add(new Product("Sweeney Todd", 10.99m));
            return list;
        }
        public override string ToString()
        {
            return string.Format("{0}:{1}", name, price);
        }

        /// <summary>
        /// 测试排序代码
        /// </summary>
        public static void TestSortCodeByComparaer()
        {
            #region C#1
            ArrayList products = GetSampleProducts();
            products.Sort(new ProductNameComparer());
            foreach (Product product in products)
            {
                Console.WriteLine(product);
            }
            #endregion
        }
    }

    public class ProductNameComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            Product first = (Product)x;
            Product second = (Product)y;
            return first.Name.CompareTo(second.Name);

        }
    }
}
