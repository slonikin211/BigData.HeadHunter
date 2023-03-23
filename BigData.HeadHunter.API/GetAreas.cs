using BigData.HeadHunter.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BigData.HeadHunter.API
{
    public sealed class GetAreas : Base
    {
        private readonly string _method = "https://api.hh.ru/areas";
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
                foreach (var area in data)
                {
                    int mainId = int.Parse(area["id"].ToString());
                    string mainName = area["name"].ToString();

                    dbContext.Areas.Add(new Area
                    {
                        Id = mainId,
                        Name = mainName,
                    });

                    foreach (var subArea in area["areas"].AsArray())
                    {
                        int id = int.Parse(subArea["id"].ToString());
                        string name = subArea["name"].ToString();
                        int parentId = int.Parse(subArea["parent_id"].ToString());

                        dbContext.Areas.Add(new Area
                        {
                            Id = id,
                            Name = name,
                            ParentId = parentId,
                        });

                        foreach (var subsubArea in subArea["areas"].AsArray())
                        {
                            int subId = int.Parse(subsubArea["id"].ToString());
                            string subName = subsubArea["name"].ToString();
                            int subParentId = int.Parse(subsubArea["parent_id"].ToString());

                            dbContext.Areas.Add(new Area
                            {
                                Id = subId,
                                Name = subName,
                                ParentId = subParentId,
                            });
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            int affected = dbContext.SaveChanges();
            Console.WriteLine($"Added {affected} areas");
            return true;
        }
    }
}
