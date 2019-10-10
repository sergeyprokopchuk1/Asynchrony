using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace _31.Synchronous_Ending
{
    class Program
    {
        //1.
        static Dictionary<string, string> _cache = new Dictionary<string, string>();
        //3.
        static Dictionary<string, Task<string>> _fcache = new Dictionary<string, Task<string>>();

        static async Task Main(string[] args)
        {
            //1. synchronous ending in the first condition
            var d = await GetWebPageAsync("http://dashboard.acuitytrading.com");


            //2. synchronous ending using Task.FromResult construction
            var dd = await FromResult();

            //3. cache of future
            var res = GetFutureWebPageAsync("http://dashboard.acuitytrading.com");
            Console.ReadKey();
        }

        private static async Task<string> GetWebPageAsync(string uri)
        {
            if(_cache.TryGetValue(uri, out string html))
            {
                return html;
            }
            else
            {
                return _cache[uri] = await new WebClient().DownloadStringTaskAsync(uri);
            }
        }

        private static Task<string> GetFutureWebPageAsync(string uri)
        {
            lock (_fcache)
            {
                if (_fcache.TryGetValue(uri, out Task<string> download))
                {
                    return download;
                }
                else
                {
                    return _fcache[uri] = new WebClient().DownloadStringTaskAsync(uri);
                }
            }
        }

        private static async Task<string> FromResult()
        { 
            return await Task.FromResult("result");
        }
    }
}
