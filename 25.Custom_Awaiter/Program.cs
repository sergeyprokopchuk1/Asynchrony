using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace _25.Custom_Awaiter
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var t = new SomeClass();
            await t;

            await Process.Start("notepad.exe");
        }
    }

    public static class Ext
    {
        public static TaskAwaiter<int> GetAwaiter(this Process process)
        {
            var tcs = new TaskCompletionSource<int>();
            process.EnableRaisingEvents = true;
            process.Exited += (s, e) => tcs.TrySetResult(process.ExitCode);
            if (process.HasExited)
            {
                tcs.TrySetResult(process.ExitCode);
            }
            return tcs.Task.GetAwaiter();
        }
    }

    public class SomeClass
    {
        public CAwaiter GetAwaiter()
        {
            return new CAwaiter();
        }
    }

    public struct CAwaiter : INotifyCompletion
    {
        public bool IsCompleted { get; private set; }

        public void GetResult()
        {
            
        }

        public void OnCompleted(Action continuation)
        {
            continuation();
        }
    }

    
}
