namespace Test.DTO;

using Newtonsoft.Json;

public record CalendarEvent
{
    [JsonProperty("groupId")]
    public int GroupId { get; init; }

    [JsonProperty("groupName")]
    public string Group { get; init; }

    [JsonProperty("room")]
    public string Room { get; init; }

    [JsonProperty("building")]
    public string Building { get; init; }

    [JsonProperty("eventType")]
    public string EventType { get; init; }

    [JsonProperty("eventName")]
    public string EventName { get; init; }

    [JsonProperty("eventTimeStart")]
    public DateTime EventTimeStart { get; init; }

    [JsonProperty("eventTimeEnd")]
    public DateTime EventTimeEnd { get; init; }

    [JsonProperty("teacherName")]
    public string TeacherName { get; init; }

    [JsonProperty("teacherPost")]
    public string TeacherPost { get; init; }

    [JsonProperty("teacherDegree")]
    public string TeacherDegree { get; init; }

    [JsonProperty("teacherAcademicTitle")]
    public string TeacherAcademicTitle { get; init; }
}

// В редис ключ будет такой:
// calendarEventsByGroup:{groupId}
// Где groupId - id группы, которое можно получить по ключу
// groupIds