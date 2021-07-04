using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
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


            People people = new People()
            {
                Id = 11,
                Name = "Nigle",
                Age = 31
            };

            PeopleCopy peopleCopy = Trans<People, PeopleCopy>(people);


            var PeopleCopyObject = ExpressionMapper.Trans<People, PeopleCopy>(people);

            var PeopleCopyObject2 = ExpressionGenericMapper<People, PeopleCopy>.Trans(people);


            //CountDownMethod(60)(currentTime => { Console.WriteLine(currentTime); }, () => { Console.WriteLine("我结束了"); });
            Console.ReadLine();

        }
        #region 表达式树


        internal class OperationsVisitor:ExpressionVisitor
        {

        }

        /// <summary>
        /// 除了上述5中方法，还可以使用框架自带的AutoMapper，首先我们要nuget添加引用AutoMapper即可直接使用，具体代码为：
        /// 有空看一下
        /// </summary>
        public class AutoMapperTest
        {
            //public static TOut Trans<TIn, TOut>(TIn tIn)
            //{
            //    return AutoMapper.Mapper.Instance.Map<TOut>(tIn);
            //}
        }

        /// <summary>
        /// 生成表达式树 缓存
        /// </summary>
        public class ExpressionMapper
        {
            private static Dictionary<string, object> _Dic = new Dictionary<string, object>();

            /// <summary>
            /// 字典缓存表达式树
            /// </summary>
            public static TOut Trans<TIn, TOut>(TIn tIn)
            {
                string key = string.Format("funckey_{0}_{1}", typeof(TIn).FullName, typeof(TOut).FullName);
                if (!_Dic.ContainsKey(key))
                {
                    //定义参数p（TIn类型）
                    ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
                    List<MemberBinding> memberBindingList = new List<MemberBinding>();
                    foreach (var item in typeof(TOut).GetProperties())
                    {
                        //可以在这里判断是否为空 空则跳过不赋值
                        PropertyInfo propertyInfo = typeof(TIn).GetProperty(item.Name);
                        if (propertyInfo == null)
                        {
                            break;
                        }
                        MemberExpression property = Expression.Property(parameterExpression, propertyInfo);
                        //如果全部属性都是相同没有多余跟缺少的就用下面这条语句
                        //MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                        //绑定Out和In之间的关系：Age = p.Age
                        MemberBinding memberBinding = Expression.Bind(item, property);
                        memberBindingList.Add(memberBinding);
                    }
                    foreach (var item in typeof(TOut).GetFields())
                    {
                        //可以在这里判断是否为空 空则跳过不赋值
                        //FieldInfo propertyInfo = typeof(TIn).GetProperty(item.Name);
                        //if (propertyInfo == null)
                        //{
                        //    break;
                        //}
                        //MemberExpression property = Expression.Property(parameterExpression, propertyInfo);
                        MemberExpression property = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                        MemberBinding memberBinding = Expression.Bind(item, property);
                        memberBindingList.Add(memberBinding);
                    }
                    MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                    //p => new PeopleCopy() {Age = p.Age, Name = p.Name, Id = p.Id}
                    Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, parameterExpression);
                    Func<TIn, TOut> func = lambda.Compile();//拼装是一次性的
                    _Dic[key] = func;
                }
                return ((Func<TIn, TOut>)_Dic[key]).Invoke(tIn);
            }
        }

        /// <summary>
        /// 生成表达式树  泛型缓存 最快
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        public class ExpressionGenericMapper<TIn, TOut>//Mapper`2
        {
            private static Func<TIn, TOut> func = null;
            static ExpressionGenericMapper()
            {
                //设置p为TIn类型
                ParameterExpression parameterExpression = Expression.Parameter(typeof(TIn), "p");
                //储存绑定关系列表
                List<MemberBinding> memberBindingList = new List<MemberBinding>();
                //获取属性
                foreach (var item in typeof(TOut).GetProperties())
                {
                    //找到Name相同的属性
                    MemberExpression property = Expression.Property(parameterExpression, typeof(TIn).GetProperty(item.Name));
                    //设置绑定关系
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                //获取字段
                foreach (var item in typeof(TOut).GetFields())
                {
                    //找到Name相同的字段
                    MemberExpression property = Expression.Field(parameterExpression, typeof(TIn).GetField(item.Name));
                    //设置绑定关系
                    MemberBinding memberBinding = Expression.Bind(item, property);
                    memberBindingList.Add(memberBinding);
                }
                //初始化TOut类型的实例 将绑定列表传入进去
                MemberInitExpression memberInitExpression = Expression.MemberInit(Expression.New(typeof(TOut)), memberBindingList.ToArray());
                //设置表达式树
                Expression<Func<TIn, TOut>> lambda = Expression.Lambda<Func<TIn, TOut>>(memberInitExpression, new ParameterExpression[]
                {
                parameterExpression
                });
                //表达式树转化委托
                func = lambda.Compile();//拼装是一次性的
            }
            public static TOut Trans(TIn t)
            {
                return func(t);
            }
        }



        /// <summary>
        /// 复制两个对象里相同的属性值赋予
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="tIn"></param>
        /// <returns></returns>
        public static TOut Trans<TIn, TOut>(TIn tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();
            //复制公共属性 :有get set
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                foreach (var itemIn in tIn.GetType().GetProperties())
                {
                    if (itemOut.Name.Equals(itemIn.Name))
                    {
                        itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                        break;
                    }
                }
            }
            //复制公共字段 ： 没有 get set
            foreach (var itemOut in tOut.GetType().GetFields())
            {
                foreach (var itemIn in tIn.GetType().GetFields())
                {
                    if (itemOut.Name.Equals(itemIn.Name))
                    {
                        itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                        break;
                    }
                }
            }
            return tOut;
        }


        /// <summary>
        /// 表达式树实例
        /// </summary>
        public static void MethodExPression()
        {
            //实例一 (x,y) => x + y
            ParameterExpression xParameter = Expression.Parameter(typeof(int), "x");
            ParameterExpression yParameter = Expression.Parameter(typeof(int), "y");
            Expression body = Expression.Add(xParameter, yParameter);//两数相加
            ParameterExpression[] parameterExpressions = new[] { xParameter, yParameter };

            Expression<Func<int, int, int>> adder = Expression.Lambda<Func<int, int, int>>(body, parameterExpressions);
            Console.WriteLine(adder);

            //实例二 （m,n） => (m * n) + x
            ParameterExpression parameterLeft = Expression.Parameter(typeof(int), "m");//定义参数
            ParameterExpression parameterRight = Expression.Parameter(typeof(int), "n");//定义参数
            BinaryExpression binaryMultiply = Expression.Multiply(parameterLeft, parameterRight);//组建第一步的乘法             
            ConstantExpression constant = Expression.Constant(2, typeof(int)); //定义常数参数
            ParameterExpression constantInt = Expression.Parameter(typeof(int), "x"); //定义常数参数
            //ConstantExpression constantInt2 = Expression.Constant(2, typeof(int)); //定义常数参数
            BinaryExpression binaryAdd = Expression.Add(binaryMultiply, constantInt);//组建第二步的加法
            var expression = Expression.Lambda<Func<int, int, int>>(binaryAdd, parameterLeft, parameterRight);//构建表达式   
            Console.WriteLine(expression);
            var func = expression.Compile();  //编译为lambda表达式
            int iResult3 = func(3, 2);
            int iResult4 = expression.Compile().Invoke(3, 2);
            int iResult5 = expression.Compile()(3, 2);


            //实例三 返回x的id是否等于5
            Expression<Func<People, bool>> lambda = x => x.Id.ToString().Equals("5");

            //声明对象x变量
            ParameterExpression parameterExpression = Expression.Parameter(typeof(People), "x");

            //找到 People的id  下面三个好像都相同
            MemberExpression member = Expression.Field(parameterExpression, typeof(People).GetField("Id"));
            MemberExpression member2 = Expression.Field(parameterExpression, "Id");
            MemberExpression member3 = Expression.PropertyOrField(parameterExpression, "Id");

            //调用ToString方法
            MethodCallExpression method = Expression.Call(member3, typeof(int).GetMethod("ToString", new Type[] { }), new Expression[0]);
            //调用Equals方法
            MethodCallExpression methodEquals = Expression.Call(method, typeof(string).GetMethod("Equals", new Type[] { typeof(string) }), new Expression[]
            {
                Expression.Constant("5",typeof(string))
            });
            //拼接Lambda
            var expression3 = Expression.Lambda<Func<People, bool>>(methodEquals, parameterExpression);
            bool bResult = expression3.Compile().Invoke(new People()
            {
                Id = 5,
                Name = "Nigle",
                Age = 31
            });
            Console.WriteLine(expression3);
            Console.WriteLine(bResult);


            //实例四  比较Age大于5
            People p = new People()
            {
                Id = 11,
                Name = "Nigle",
                Age = 31
            };
            //拼装表达式树，交给下端用
            ParameterExpression parameterExpressionPeople = Expression.Parameter(typeof(People), "x");//声明一个参数
            Expression propertyExpression = Expression.Property(parameterExpressionPeople, typeof(People).GetProperty("Age"));//声明访问参数属性的对象
                                                                                                                        //Expression property = Expression.Field(parameterExpression, typeof(People).GetField("Id"));
            ConstantExpression constantExpression = Expression.Constant(5, typeof(int));//声明一个常量
            BinaryExpression binary = Expression.GreaterThan(propertyExpression, constantExpression);//添加比较方法
            var lambdaGreater = Expression.Lambda<Func<People, bool>>(binary, new ParameterExpression[] { parameterExpressionPeople });//构建表达式主体
            Console.WriteLine(lambdaGreater); // x => x.Age > 5
            bool bGreaterResult = lambda.Compile().Invoke(p); //比较值
        }

        public class People
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public int Id;
        }

        public class PeopleCopy
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public int Id;
        }

        public class PeopleCopyOtherName
        {
            public int Age { get; set; }
            public string Name { get; set; }
            public int Id;
            public string CopyName { get; set; }
        }

        #endregion




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
