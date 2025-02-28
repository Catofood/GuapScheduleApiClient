using Newtonsoft.Json;

namespace Test.Static.DTO;

public class Building
{
    [JsonProperty("Name")] private string Name;
    [JsonProperty("Title")] private string Title;
    [JsonProperty("ItemId")] private int ItemId;
}

// /get-sem-builds?ids=<id1_>,<id2_>,<id3_>... - получить список корпусов
// Аргумент ids - список ИД через запятую
// Формат:

//     [
//     ...
//     {'Name':adress, 'Title':adress, 'ItemId': id}
//     ...
//     ]

// Пример:
// [
//     {
//         "Name": "Б.М.",
//         "Title": "Б.М.",
//         "ItemId": 11
//     },
//     {
//         "Name": "Б.Морская 67",
//         "Title": "Б.Морская 67",
//         "ItemId": 1
//     },
// ]