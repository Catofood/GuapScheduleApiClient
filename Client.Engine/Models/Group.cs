using Newtonsoft.Json;

namespace Application.DTO;

public class Group()
{
    [JsonProperty("Name")] public string Name;
    [JsonProperty("ItemId")] public int ItemId;
}

// Пример:
//
// [
//     {
//         "Name": "8114Кв",
//         "ItemId": 1
//     },
//     {
//         "Name": "В1441",
//         "ItemId": 2
//     },
// ]