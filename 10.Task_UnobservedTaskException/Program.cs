using System;
using System.Threading;
using System.Threading.Tasks;

namespace _10.Task_UnobservedTaskException
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskScheduler.UnobservedTaskException += TaskException;

            Task.Run(() => throw null);

            Task.Run(() => throw null);

            Task.Run(() => throw new FormatException());

            Thread.Sleep(1000);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Thread.Sleep(1000);
            GC.Collect();
            GC.WaitForPendingFinalizers();

            Console.ReadLine();
        }

        static void TaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            e.SetObserved();

            ((AggregateException)e.Exception).Handle(ex => 
            {
                Console.WriteLine($"Message {ex.Message}");

                return true;
            });
        }
    }
}
