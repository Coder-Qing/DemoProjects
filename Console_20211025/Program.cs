using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace _5._2._3_异步方法模型
{
    class Program
    {
        static readonly HttpClient httpClient = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //SynchronizationContext 
            //5.4.1可等待模式👇
            ValidPrintYieldPrintAsync();
            //5.2.3 异步方法模型👇
            //PrintPageLength();
        }

        static async Task<int> GetPageLengthAsync(string path)
        {
            Task<string> fetchTextTask = httpClient.GetStringAsync(path);
            int length = (await fetchTextTask).Length;
            return length;
        }

        static void PrintPageLength()
        {
            //阻塞 ui线程
            Task<int> lengthTask = GetPageLengthAsync("http://csharpindepth.com");
            Console.WriteLine(lengthTask.Result);
        }

        static async void ValidPrintYieldPrintAsync()
        {
            await ValidPrintYieldPrint();
        }

        public static async Task ValidPrintYieldPrint()
        {
            Console.WriteLine("Before yielding!");
            await Task.Yield();
            Console.WriteLine("After yielding!");
        }
    }



}
