using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using  System.Threading.Tasks;

namespace Console_20210406
{
    class Program
    {

        delegate void Int32Printer(int x);
        delegate void Int64Printer(long x);


        delegate void GeneralPrinter(object obj);
        delegate void Print(string message);


        static void Main(string[] args)
        {
            Solution solution = new Solution();
            //Console.WriteLine(solution.LongestPalindrome("xaabacxcabaaxcabaax"));



            var book = new { Name = "", Poke = "" };
            string name = book.Name;
            string poke = book.Poke;



            //ConcurrentQueue 线程安全版本的Queue
            //ConcurrentStack线程安全版本的Stack
            //ConcurrentBag线程安全的对象集合
            //ConcurrentDictionary线程安全的Dictionary
            //BlockingCollection

            Expression

            //CountDownMethod(60)(currentTime => { Console.WriteLine(currentTime); }, () => { Console.WriteLine("我结束了"); });
            Console.ReadLine();


        }

        static List<Action> CreateCountingActions()
        {

            //List<Action> actions = CreateCountingActions();
            //actions[0]();
            //actions[0]();
            //actions[1]();
            //actions[1]();

            List<Action> actions = new List<Action>();
            int outerCounter = 0;
            for (int i = 0; i < 2; i++)
            {
                int innerCounter = 0;
                Action action = () => 
                {
                    Console.WriteLine("Other: {0}; Inner: {1}",outerCounter,innerCounter);
                    outerCounter++;
                    innerCounter++;
                };
                actions.Add(action);
            }
            return actions;
        }


        public static List<Action> CreateAction()
        {
            //List<Action> actions = CreateAction();
            //foreach (var item in actions)
            //{
            //    item();
            //}

            List<Action> actions = new List<Action>();
            for (int i = 0; i < 5; i++)
            {
                string text = string.Format("message {0}", i);
                actions.Add(() => { Console.WriteLine(text); });
            }
            return actions;
        }

        public static (T first,T second) TMethod<T>(T T1,T T2)  where T : struct
        {
            T t3 = default(T);
            t3 = T1;
            T1 = T2;
            T2 = t3;
            return (T1,T2);
        }


        public static string MethodTest()
        {
            try
            {
                Console.WriteLine("123");
                return "456";
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Console.WriteLine("789");
            }
        }

        
        


        public class FaClass
        {
            public virtual void ShowText() { }
        }

        public class ChildClss:FaClass
        {
            public override void ShowText()
            {
                base.ShowText();
            }
        }
            
        public static Func<Action<int>,Action,Task> CountDownMethod(int second)
           => (ShowCurrentTime, CloseCountDownTimer) =>
        {
            return Task.Run(async () => 
            {
                while (second >= 0)
                {
                    ShowCurrentTime(second);
                    second--;
                    await Task.Delay(new TimeSpan(0,0,1));
                }
                CloseCountDownTimer();
            });
        };


        partial class PartialMehodDemo
        {
            partial void CustomizeToString(ref string text);
        }


        partial class PartialMehodDemo
        {
            partial  void CustomizeToString(ref string text)
            {
                text += " - customized!";
            }
        }

        static IEnumerable<int> CreateSimpleIterator()
        {
            yield return 10;
            for (int i = 0; i < 3; i++)
            {
                yield return i;
            }
            yield return 20;
        }

        static (int min, int max) FindMinMax(int[] input)
        {

            if (input is null || input.Length == 0)
            {
                throw new ArgumentException("Cannot find minimum and maximum of a null or empty array.");
            }

            var min = int.MaxValue;
            var max = int.MinValue;
            foreach (var i in input)
            {
                if (i < min)
                {
                    min = i;
                }
                if (i > max)
                {
                    max = i;
                }
            }
            return (min, max);
        }



        static void PrintTypeof<T1, T2>()
        {
            Console.WriteLine(typeof(T1));
            Console.WriteLine(typeof(Dictionary<T1,T2>));
            Console.WriteLine(typeof(Dictionary<,>));
        }

        public class Person
        {
            public Person(string fName, string lName)
            {
                this.firstName = fName;
                this.lastName = lName;
            }
            public Person()
            {

            }

            public string firstName;
            public string lastName;
        }

        public class Class1<T> :IEnumerable<T>
        {
            private T[] _people;
            public Class1(T[] pArray)
            {
                _people = new T[pArray.Length];

                for (int i = 0; i < pArray.Length; i++)
                {
                    _people[i] = pArray[i];
                }
            }

            public IEnumerator GetEnumerator()
            {
                return new Class2<T>(_people);
            }

            IEnumerator<T> IEnumerable<T>.GetEnumerator()
            {
                return new Class2<T>(_people); 
            }
        }

        public class Class2<T>:IEnumerator<T>
        {

            public Class2(T[] people)
            {
                _people = people;
            }
            public string Name { get; set; }
            public int Age { get; set; }

            int position = -1;

            public T[] _people;

            object IEnumerator.Current => _people[position];

            public T Current => _people[position];

            public bool MoveNext()
            {
                position++;
                return position < _people.Length;

            }

            public void Reset()
            {
                position = -1;
            }

            public void Dispose()
            {
                _people = null;
                position = -1;
            }
        }

        class GenericCounter<T>
        {
            private static int value;//封闭的
            private static string _string;

            static GenericCounter()
            {
                Console.WriteLine("Initializing counter for {0}",typeof(T));
            }

            public static void Increment()
            {
                value++;
                _string += "+1";
            }

            public  static void Display()
            {
                Console.WriteLine("Counter for {0}: {1}  string:{2}",typeof(T),value,_string);
            }
        }
    }


    


    public class CapturedVairablesDemo
    {


        //var demo = new CapturedVairablesDemo();
        //Action<string> action = demo.CreateAction("method argument");
        //action("LambdaContext argument");


        public string instanceField = "instance field";
        public Action<string> CreateAction(string methodParamerter)
        {
            string methodLocal = "method local";
            string uncpatured = "uncaptured local";
            Action<string> action = lambdaParameter => 
            {
                string lambdaLocal = "lambda local";
                Console.WriteLine("instance field:{0}",instanceField);
                Console.WriteLine("Mthod parameter:{0}",methodParamerter);
                Console.WriteLine("Method local:{0}",methodLocal);
                Console.WriteLine("Lambda parameter:{0}",lambdaParameter);
                Console.WriteLine("Lambda local:{0}", lambdaLocal);
            }; 
            methodLocal = "modified method local";
            return action;
        }
    }

    public class Solution
    {
        public string Convert(string s, int numRows)
        {
            if (s.Length == 1 || numRows == 1)
            {
                return s;
            }

            int len = s.Length;

            //转为数组
            char[] charArray = s.ToCharArray();

            char[,] ZChars = new char[numRows,numRows];
            
            return s;
        }


        public static Func<Func<T1, T2, bool>, Func<T1, T2, bool>, List<T1>> Com_Except<T1, T2>(List<T1> t1, List<T2> t2)
            => (small, big) => 
            {
                int i1 = 0, i2 = 0;
                List<T1> t1Result = new List<T1>();

                t1.ForEach((t)=> 
                {
                    t1Result.Add(t);
                });


                while (i1 < t1.Count && i2 < t2.Count)
                {
                    if (small(t1[i1],t2[i2]))
                    {
                        i1++;
                    }
                    else if (big(t1[i1],t2[i2]))
                    {
                        i2++;
                    }
                    else
                    {
                        t1Result.Remove(t1[i1]);
                        i1++;i2++;
                    }
                }
                return t1Result;
            };

    }

}
