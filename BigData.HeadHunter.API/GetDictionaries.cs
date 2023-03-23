using BigData.HeadHunter.EFCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Threading.Tasks;

namespace BigData.HeadHunter.API
{
    public class GetDictionaries : Base
    {
        private readonly string _method = "https://api.hh.ru/dictionaries";

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
            var data = JsonSerializer.Deserialize<Dictionary<string, Object>>(content);

            int valuePrimaryId = 0;
            if (data != null)
            {
                foreach (var item in data)
                {
                    var dictionaryKey = item.Key;

                    dbContext.DictionaryKeys.Add(new DictionaryKey
                    {
                        Id = dictionaryKey,
                    });

                    var valuesData = JsonSerializer.Deserialize<JsonArray>(item.Value.ToString());
                    foreach (var dictionaryValue in valuesData)
                    {
                        if (dictionaryKey == "currency")
                        {
                            var currencyCode = dictionaryValue["code"].ToString();
                            var currencyName = dictionaryValue["name"].ToString();

                            dbContext.DictionaryValues.Add(new DictionaryValue
                            {
                                Id = valuePrimaryId++,
                                ValueId = currencyCode,
                                Name = currencyName,
                                KeyId = dictionaryKey
                            });

                            continue;
                        }

                        if (dictionaryKey == "driver_license_types")
                        {
                            var driverLicenseId = dictionaryValue["id"].ToString();

                            dbContext.DictionaryValues.Add(new DictionaryValue
                            {
                                Id = valuePrimaryId++,
                                ValueId = driverLicenseId,
                                Name = driverLicenseId,
                                KeyId = dictionaryKey
                            });

                            continue;
                        }

                        var id = dictionaryValue["id"].ToString();
                        var name = dictionaryValue["name"].ToString();

                        dbContext.DictionaryValues.Add(new DictionaryValue
                        {
                            Id = valuePrimaryId++,
                            ValueId = id,
                            Name = name,
                            KeyId = dictionaryKey
                        });
                    }
                }
            }
            else
            {
                return false;
            }

            int affected = dbContext.SaveChanges();
            Console.WriteLine($"Added {affected} dictionaries");
            return true;
        }
    }
}
