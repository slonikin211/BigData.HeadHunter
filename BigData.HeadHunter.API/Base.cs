using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Threading.Tasks;

namespace BigData.HeadHunter.API
{
    internal class Base
    {
        public EFCore.HeadHunter DbContext;

        public HttpClient HttpClient = null!;

        public Base() 
        {
            HttpClient = new HttpClient();
            DbContext = new EFCore.HeadHunter();
        }

        public HttpRequestMessage PreparedRequest(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, url);

            request.Headers.Add("User-Agent", "MyTestApp/1.0 (georgepark.dev@gmail.com)");

            return request;
        }
    }
}
