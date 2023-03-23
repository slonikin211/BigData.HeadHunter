using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Threading.Tasks;

namespace BigData.HeadHunter.API
{
    public abstract class Base
    {
        public EFCore.HhContext dbContext;

        public HttpClient client = null!;

        public Base() 
        {
            client = new HttpClient();
            dbContext = new EFCore.HhContext();
        }

        public HttpRequestMessage PreparedRequest(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, url);

            request.Headers.Add("User-Agent", "MyTestApp/1.0 (georgepark.dev@gmail.com)");

            return request;
        }

        public abstract HttpResponseMessage DoRequest();

        public abstract bool HandleResponse(HttpResponseMessage message);
    }
}
