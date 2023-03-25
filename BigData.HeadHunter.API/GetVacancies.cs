using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BigData.HeadHunter.API
{
    public class GetVacancies : Base
    {
        private readonly string _method = "https://api.hh.ru/vacancies";

        public override HttpResponseMessage DoRequest()
        {
            throw new NotImplementedException("You can't use this method in this time. Try to use DoRequestById(int id) instead.");
        }

        public HttpResponseMessage DoRequest(long areaId, string industryId, int page = 0, int perPage = 100)
        {
            var endpoint = _method +
                $"/?page={page}" +
                $"&per_page={perPage}" +
                $"&area={areaId}" + 
                $"&industry={industryId}";

            var request = PreparedRequest(HttpMethod.Get, endpoint);
            
            Console.WriteLine($"Sending request: {endpoint}");
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
                try
                {
                    var items = JsonDocument.Parse(data["items"].ToString()).RootElement.EnumerateArray();
                    foreach ( var item in items )
                    {
                        var id = int.Parse(item.GetProperty("id").ToString());

                        // Check if exists
                        var isExists = dbContext.Vacancies
                            .Select(x => x.Id == id)
                            .FirstOrDefault();

                        if (!isExists)
                        {
                            dynamic area = JsonObject.Parse(item.GetProperty("area").ToString());
                            dynamic salary = JsonObject.Parse(item.GetProperty("salary").ToString());
                            dynamic employer = JsonObject.Parse(item.GetProperty("employer").ToString());

                            // Employer Info
                            if (employer != null)
                            {
                                GetEmployers handler = new();
                                int employerId = int.Parse((string)employer["id"]);

                                var isEmployerExists = dbContext.Employers
                                    .Select(x => x.Id == employerId)
                                    .FirstOrDefault();

                                if (!isEmployerExists)
                                {
                                    var response = handler.DoRequestById(employerId);
                                    var result = handler.HandleResponse(response);
                                    Console.WriteLine($"Employer added status: {result}");
                                }
                            }

                            var vacancy = new EFCore.Vacancy
                            {
                                Id = id,
                                Name = item.GetProperty("name").ToString(),
                                SalaryFrom = salary == null ? null : (int?)salary["from"],
                                SalaryTo = salary == null ? null : (int?)salary["to"],
                                SalaryCurrency = salary == null ? null : (string?)salary["currency"],
                                SalaryGross = salary == null ? (int?)null : (bool)salary["gross"] ? 1 : 0,
                                AreaId = area == null ? (int?)null : int.Parse((string)area["id"]),
                                Url = item.GetProperty("url").ToString(),
                                PublishedDate = item.GetProperty("published_at").ToString(),
                                CreatedDate = item.GetProperty("created_at").ToString(),
                                EmployerId = employer == null ? null : int.Parse((string)employer["id"]),
                            };

                            // Vacancy info (Add)
                            var added = dbContext.Vacancies.Add(vacancy);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Trace.TraceError(ex.Message);
                    return false;
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
