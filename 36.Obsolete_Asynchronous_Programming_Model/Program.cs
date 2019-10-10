using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace _36.Obsolete_Asynchronous_Programming_Model
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. APM - BeginRead, EndRead

            //2. APM - Asynchronous delegate

            Func<string> foo = () => { Thread.Sleep(1000); return "foo"; };

            //foo.BeginInvoke(asyncResult =>
            //Console.WriteLine(foo.EndInvoke(asyncResult)), null
            //);

            //the same as
            Func<string> foo2 = () => { Thread.Sleep(1000); return "foo2"; };
            //Task.Run(foo2).ContinueWith(c => Console.WriteLine(c.Result));

            //3. EAP - Event-based Asynchronous Pattern

            //4. EAP - BackgroundWorker
            var worker = new BackgroundWorker { WorkerSupportsCancellation = true };

            worker.DoWork += (sender, args) => 
            { 
                //in work thread (в рабочем потоке)
                if(args.Cancel)
                {
                    return;
                }

                Thread.Sleep(1000);
                args.Result = 123;
            };

            worker.RunWorkerCompleted += (sender, args) => 
            { 
                //in client thread (в пользовательском потоке)
                if(args.Cancelled)
                {
                    Console.WriteLine("Canceled");
                }
                else if(args.Error != null)
                {
                    Console.WriteLine($"Error - {args.Error.Message}");
                }
                else
                {
                    Console.WriteLine($"Result - {args.Result}");
                }
            };

            worker.RunWorkerAsync();

            Console.ReadKey();
        }
    }
}
