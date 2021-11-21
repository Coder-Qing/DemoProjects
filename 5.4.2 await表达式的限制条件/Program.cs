using System;
using System.Security.AccessControl;
using System.Threading;
using System.Threading.Tasks;

namespace 锁的使用
{
    internal class Program
    {
        internal static void Main()
        {
            DelaySemaphore();
            Console.ReadKey();
        }

        public static async void DelaySemaphore()
        {
            //Semaphore (int initialCount, int maximumCount);
            //initialCount代表还分配几个线程,比如是1,那就是还能允许一个线程继续跑锁起来的代码
            //maximumCount代表最大允许数,比如是1,那就是进去1个线程,就会锁起来
            System.Threading.SemaphoreSlim slimlock = new SemaphoreSlim(1, 1);
            await slimlock.WaitAsync();
            try
            {
                Console.WriteLine("111");
                await Task.Delay(3000);
                Console.WriteLine("semaphore complete");
            }
            finally
            {
                slimlock.Release();
            }
        }

        public void MonitorAndLockDemo()
        {
            //1.Monitor
            object lockObject = new object();
            bool acquiredLock = false;
            try
            {
                Monitor.TryEnter(lockObject, ref acquiredLock);
                if (acquiredLock)
                {

                    // Code that accesses resources that are protected by the lock.
                }
                else
                {

                    // Code to deal with the fact that the lock was not acquired.
                }
            }
            finally
            {
                if (acquiredLock)
                {
                    Monitor.Exit(lockObject);
                }
            }

            //2.Monitor lock
            //Monitor
            try
            {
                Monitor.Enter(lockObject);
                // Code that accesses resources that are protected by the lock.
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
            //lock:

            lock (lockObject)
            {
                //Code that accesses resources that are protected by the lock.
            }
        }
    }
}
