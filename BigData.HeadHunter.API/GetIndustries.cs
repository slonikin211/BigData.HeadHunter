using BigData.HeadHunter.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace BigData.HeadHunter.API
{
    public class GetIndustries : Base
    {
        private readonly string _method = "https://api.hh.ru/industries";

        public override HttpResponseMessage DoRequest()
        {
            var request = PreparedRequest(HttpMethod.Get, _method);

            var response = client.Send(request);

            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else
            {
                throw new HttpRequestException($"Cannot proceed ther request to: {_method}");
            }
        }

        public override bool HandleResponse(HttpResponseMessage message)
        {
            var content = message.Content.ReadAsStringAsync().Result;
            var data = JsonSerializer.Deserialize<JsonArray>(content);

            if (data != null)
            {
                foreach (var industry in data)
                {
                    var mainId = float.Parse(industry["id"].ToString());
                    var mainName = industry["name"].ToString();

                    dbContext.Industries.Add(new Industry
                    {
                        Id = mainId,
                        Name = mainName,
                    });

                    foreach (var jobField in industry["industries"].AsArray())
                    {
                        var id = float.Parse(jobField["id"].ToString());
                        var name = jobField["name"].ToString();
                        var parentId = mainId;

                        dbContext.Industries.Add(new Industry
                        {
                            Id = id,
                            Name = name,
                            ParentId = mainId
                        });
                    }
                }
            }
            else
            {
                return false;
            }

            int affected = dbContext.SaveChanges();
            Console.WriteLine($"Added {affected} industries");
            return true;
        }
    }
}
