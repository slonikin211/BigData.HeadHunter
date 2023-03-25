using BigData.HeadHunter.API;
using BigData.HeadHunter.EFCore;
using System.Text;


//Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("Program Started");

// Areas
{
    //GetAreas handler = new();

    //var response = handler.DoRequest();
    //var resultStatus = handler.HandleResponse(response);
    //Console.WriteLine($"GetAreas result status: {resultStatus}");
}

// Industries
{
    //GetIndustries handler = new();

    //var response = handler.DoRequest();
    //var resultStatus = handler.HandleResponse(response);
    //Console.WriteLine($"GetIndustries result status: {resultStatus}");
}

// Dictionaries
{
    //GetDictionaries handler = new();

    //var response = handler.DoRequest();
    //var resultStatus = handler.HandleResponse(response);
    //Console.WriteLine($"GetDictionaries result status: {resultStatus}");
}

// Employer
{
    //GetEmployers handler = new();

    //var response = handler.DoRequestById(988247);
    //var resultStatus = handler.HandleResponse(response);
    //Console.WriteLine($"GetEmployers result status: {resultStatus}");
}

// Vacancies
{
    const int MAX_PAGE = 20;
    const int PER_PAGE = 100;

    HhContext dbContext = new();
    var areas = dbContext.Areas
        .Select(a => a.Id)
        .ToList();
    var industries = dbContext.Industries
        .Select(i => i.Id)
        .ToList();

    var MAX = (long)MAX_PAGE * (long)PER_PAGE * (long)industries.Count * (long)areas.Count;

    GetVacancies handler = new();
    long current = 0;
    foreach ( var area in areas )
    {
        foreach (var industry in industries )
        {
            for (int page = 0; page < MAX_PAGE; ++page)
            {
                var response = handler.DoRequest(
                    areaId: area,
                    industryId: industry,
                    page: page);
                var resultStatus = handler.HandleResponse(response);
                Console.WriteLine($"Area: {area}, Industry: {industry}, Page: {page}. Current progress: {current}/{MAX}");
                current += PER_PAGE;
            }
        }
    }
    Console.WriteLine($"Industries amount: {industries.Count}");
    Console.WriteLine($"Area amount: {areas.Count}");
    Console.WriteLine($"Pages available per one type of query: {MAX_PAGE}");
    Console.WriteLine($"Amount of vacancies: {(long)MAX_PAGE * (long)PER_PAGE * (long)industries.Count * (long)areas.Count}");
}

Console.WriteLine("Program Finished");
