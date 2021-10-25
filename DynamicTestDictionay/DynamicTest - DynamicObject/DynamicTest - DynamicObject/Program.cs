using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicTest___DynamicObject
{
    class Program
    {
        static void Main(string[] args)
        {
            dynamic example = new SimpleDynamicExample();
            example.CallSomeMethod("x",10);
            Console.WriteLine(example.SomeProperty);

            Console.ReadKey();
        }

        class SimpleDynamicExample:DynamicObject
        {
            //调用方法  args是参数
            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                Console.WriteLine("Invoked: {0}{1}",binder.Name,string.Join(", ",args));
                result = null;
                return true;
            }

            //获取属性
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                result = "Fetched: " + binder.Name;
                return true;
            }
        }
    }
}
