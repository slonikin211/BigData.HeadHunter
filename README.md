# BigData.HeadHunter

## Содержание

- [О программе](#about)
- [Установка](#getting_started)
- [Использование](#usage)

## О программе <a name = "about"></a>

Программа для выполнения лабороторной работы №2 по курсу "Наука о данных и аналитика больших данных"

## Начало работы <a name = "getting_started"></a>

Для скачивания проекта, в пустую директории выполните команду:
```bash
git clone https://github.com/slonikin211/BigData.HeadHunter.git
```
Если у вас не установлен Git, то установите его с [официального сайта](https://git-scm.com/downloads), либо скачайте архив и распакуйте его в удобную директорию.

### Предварительные условия

Инструменты:
- Git
- Sqlite
- SqliteStudio
- VisualStudio (или VS Code или другая IDE)
- .NET 7, C# 11

### Установка

После скачивания архива, можно выполнить следующие команды в корне проекта:
```
dotnet build BigData.HeadHunter.EFCore
dotnet build BigData.HeadHunter.API
dotnet build BigData.HeadHunter.Console
```
После этого, в каждом проекте установятся пакеты и зависимости.
Также можно просто открыть BigData.HeadHunter.sln файл в VisualStudio и через графический интерфейс собрать проекты (через хоткей Ctrl+B).

Далее рассмотрим варианты использования приложения

## Использование <a name = "usage"></a>

### Заполнение данных

Предварительно нужно проверить наличие файла hh.db и правильность строки подключения к нему в BigData.HeadHunter.EFCore/HhContext.cs:
```cs
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder.UseSqlite("Filename=D:\\Projects\\BigData.HeadHunter\\hh.db");
```

#### Areas
Заполнение Areas
```cs
    GetAreas handler = new();

    var response = handler.DoRequest();
    var resultStatus = handler.HandleResponse(response);
    Console.WriteLine($"GetAreas result status: {resultStatus}");
```

#### Industries
Заполнение Industries
```cs
    GetIndustries handler = new();

    var response = handler.DoRequest();
    var resultStatus = handler.HandleResponse(response);
    Console.WriteLine($"GetIndustries result status: {resultStatus}");
```

#### Dictionaries
Заполнение Dictionaries
```cs
    GetDictionaries handler = new();

    var response = handler.DoRequest();
    var resultStatus = handler.HandleResponse(response);
    Console.WriteLine($"GetDictionaries result status: {resultStatus}");
```

#### Employer
Добавление работодателя с указанным ID в Employer
```cs
    GetEmployers handler = new();

    var response = handler.DoRequestById(988247);
    var resultStatus = handler.HandleResponse(response);
    Console.WriteLine($"GetEmployers result status: {resultStatus}");
```

#### Vacancies
Спрасить все доступные вакансии по заранее подготовленным кластерам локаций и категорий:
```cs
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
```
