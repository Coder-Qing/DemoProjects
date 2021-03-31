using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using 代码进阶控制台程序.Model;

namespace 代码进阶控制台程序
{
    class Program
    {
        delegate void pringString(string print);

        public delegate string returnMethodDelegate(string testString);

        public static returnMethodDelegate ReturnMethodDelegate;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            pringString pring = new pringString(Program.print);

            ReturnMethodDelegate = new returnMethodDelegate(returnMethod);

            Console.WriteLine(TestClass.TestDelegateMethod());

            #region C#1
            //测试排序
            //Product.TestSortCodeByComparaer();

            #endregion


            #region C#2

            //测试排序
            //Product2.TestSortCodeComparer();

            //List<Product2> products = Product2.GetSampleProducts();

            #endregion

            #region C#3
            //测试排序
            //Product3.TestSortCodeComparer();


            #endregion

            string i = "abcdefg";
            //i.ShowTest(i);

            Console.WriteLine(InvertString(i));

            #region LinQ

            //XDocument doc = XDocument.Load("data.xml");
            //var filtered = from p in doc.Descendants("Product")
            //               join s in doc.Descendants("Supplier")
            //                    on (int)p.Attribute("SupplierID")
            //                    equals (int)s.Attribute("SupplierID")
            //               where (decimal)p.Attribute("Price") > 10
            //               orderby (string)s.Attribute("Name"),
            //                       (string)p.Attribute("Name")
            //               select new
            //               {
            //                   SupplierName = (string)s.Attribute("Name"),
            //                   ProductName = (string)p.Attribute("Name")
            //               };
            //foreach (var v in filtered)
            //{
            //    Console.WriteLine("SupplierName:{0};ProductName:{1}",v.SupplierName,v.ProductName);
            //}

            #endregion
        }

        private static string returnMethod(string testString)
        {
            return "你是我的小可爱：" + testString;
        }

        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string InvertString(string input)
        {
            char[] chars = input.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// 计算string中包含单词得数量
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static Dictionary<string,int> CountWords(string text)
        {
            Dictionary<string, int> frequencies;
            frequencies = new Dictionary<string, int>();

            string[] worlds = Regex.Split(text,@"\W+");

            foreach (string world in worlds)
            {
                if (frequencies.ContainsKey(world))
                {
                    frequencies[world]++;
                }
                else
                {
                    frequencies[world] = 1;
                }
            }
            return frequencies;
        }

        static void print(string x)
        {

        }
    }


    public class TestClass
    {
        public static string TestDelegateMethod()
        {
           return Program.ReturnMethodDelegate?.Invoke("彩彩");
        }
        //好耶ヽ(✿ﾟ▽ﾟ)ノ
    }



    public static class Extend
    {
        public static void ShowTest(this string testString, string appendString)
        {
            Console.WriteLine(appendString);
        }

    }
}
