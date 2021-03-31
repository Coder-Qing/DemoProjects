using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Timers;

namespace 泛型控制台_20210323
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<int> integers = new List<int>();
            integers.Add(1);
            integers.Add(2);
            integers.Add(3);
            integers.Add(4);

            Converter<int, double> converter = TakeSquareRoot;

            List<double> doubles;
            doubles = integers.ConvertAll<double>(converter);
            foreach (double a in doubles)
            {
                Console.WriteLine(a);
            }

            //泛型方法2
            List<string> list = MakeList<string>("Line 1","Line 2");

            Console.WriteLine("------------");
            Console.WriteLine(ComplareToDefault("x"));//1 默认是null 所有引用类型的值都大于null
            Console.WriteLine(ComplareToDefault(10));//1 默认是0 10 > 0 所以是1
            Console.WriteLine(ComplareToDefault(0));//0  默认是0 0 = 0 所以是0
            Console.WriteLine(ComplareToDefault(-10));//-1 默认是0 -1 < 0 所以是-1
            Console.WriteLine(ComplareToDefault(DateTime.MinValue));//0 datatime的最小值就是minValue

            Pair<int, string> pair = new Pair<int, string>(10,"value");
            //与上相同
            Pair<int, string> pair1 = Pair.Of(10,"value");
            Console.WriteLine(pair.Equals(pair1));
        }

        static double TakeSquareRoot(int x)
        {
            //获得平方根
            return Math.Sqrt(x);
        }

        /// <summary>
        /// 创建相应类型的list，并将两个参数传入进去
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        static List<T> MakeList<T>(T first,T second)
        {
            List<T> list = new List<T>();
            list.Add(first);
            list.Add(second);

            return list;
        }

        /// <summary>
        /// 根据传入的值 与默认值比较
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Value"></param>
        /// <returns></returns>
         static int ComplareToDefault<T>(T Value) where T:IComparable<T>
        {
            return Value.CompareTo(default(T));
        }



        /// <summary>
        /// 泛型字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        public class Dictionary2<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        {
            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            {
                throw new NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

        }


        public sealed class Pair<T1, T2> : IEquatable<Pair<T1, T2>>
        {
            private static readonly IEqualityComparer<T1> FirstComparer = EqualityComparer<T1>.Default;
            private static readonly IEqualityComparer<T2> SecondComparer = EqualityComparer<T2>.Default;

            private readonly T1 first;
            private readonly T2 second;

            public Pair(T1 first,T2 second)
            {
                this.first = first;
                this.second = second;
            }

            public T1 First { get { return first; } }
            public T2 Second { get { return second; } }

            public bool Equals([AllowNull] Pair<T1, T2> other)
            {
                return other != null && FirstComparer.Equals(this.First, other.First) &&
                    SecondComparer.Equals(this.Second, other.Second);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as Pair<T1,T2>);
            }
            public override int GetHashCode()
            {
                return FirstComparer.GetHashCode(first) * 37 + SecondComparer.GetHashCode(second);
            }
        }

        public static class Pair
        {
            public static Pair<T1,T2> Of<T1, T2>(T1 first,T2 second)
            {
                return new Pair<T1, T2>(first,second);
            }
        }
    }
}
