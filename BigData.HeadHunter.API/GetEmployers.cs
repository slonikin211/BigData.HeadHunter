using BigData.HeadHunter.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace BigData.HeadHunter.API
{
    public class GetEmployers : Base
    {
        private readonly string _method = "https://api.hh.ru/employers";

        public override HttpResponseMessage DoRequest()
        {
            throw new NotImplementedException("You can't use this method in this time. Try to use DoRequestById(int id) instead.");
        }

        public HttpResponseMessage DoRequestById(int id)
        {
            var request = PreparedRequest(HttpMethod.Get, _method + $"/{id}");

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
            var data = JsonSerializer.Deserialize<Dictionary<string, Object>>(content);

            if (data != null)
            {
                var id = int.Parse(data["id"].ToString());
                var trusted = bool.Parse(data["trusted"].ToString()) ? 1 : 0;
                var name = data["name"].ToString();

                var area = data["area"];
                int? areaId = null;
                if (area != null)
                {
                    var areaObject = JsonObject.Parse(area.ToString());
                    areaId = int.Parse(areaObject["id"].ToString());
                }

                var industriesIds = new List<string>();
                var industries = JsonDocument.Parse(data["industries"].ToString());
                foreach (var industry in industries.RootElement.EnumerateArray())
                {
                    var industryId = industry.GetProperty("id").ToString();
                    industriesIds.Add(industryId);
                }

                dbContext.Employers.Add(new Employer
                {
                    Id = id,
                    Trusted = trusted,
                    Name = name,
                    AreaId = areaId,
                });

                foreach (var indId in industriesIds)
                {
                    dbContext.EmployerIndustries.Add(new EmployerIndustry
                    {
                        EmployerId = id,
                        IndustryId = indId,
                    });
                }
            }
            else
            {
                return false;
            }

            int affected = dbContext.SaveChanges();
            Console.WriteLine($"Added {affected} employers");
            return true;
        }
    }
}
