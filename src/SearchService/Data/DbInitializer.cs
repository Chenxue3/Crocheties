using System;
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Data;

public class DbInitializer
{
    public static async Task InitDb(WebApplication app)
    {
        await DB.InitAsync("SearchDb", MongoClientSettings
            .FromConnectionString(app.Configuration.GetConnectionString("MongoDbConnection")));

        await DB.Index<Item>()
            .Key(x => x.Make, KeyType.Text)
            .Key(x => x.Model, KeyType.Text)
            .Key(x => x.Color, KeyType.Text)
            .CreateAsync();

        var count = await DB.CountAsync<Item>();//check if db is empty

        if (count == 0){
            Console.WriteLine("No data to seed");
            var itemData = await File.ReadAllTextAsync("Data/Auctions.json");

            //这里的代码是为了将json数据转换为Item对象
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            // Deserialize the JSON to a list of Item objects， 是一个List<Item>对象
            var items = JsonSerializer.Deserialize<List<Item>>(itemData, options);  

            await DB.SaveAsync(items);
        }

    }
}
