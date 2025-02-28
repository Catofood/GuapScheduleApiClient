using Newtonsoft.Json;

namespace Application.Models;

public class CalendarEvent
{ 
    // events - groups
    [JsonProperty("groupName")]
    public string Group;

    // events - rooms
    [JsonProperty("room")]
    public string Room;

    // events - rooms - buildings
    [JsonProperty("building")]
    public string Building;

    // events
    [JsonProperty("eventType")]
    public string EventType;

    // events
    [JsonProperty("eventName")]
    public string EventName;

    // events
    [JsonProperty("eventTimeStart")]
    public DateTime EventTimeStart;

    // events
    [JsonProperty("eventTimeEnd")]
    public DateTime EventTimeEnd;

    // events - teachers
    [JsonProperty("teacherName")]
    public string TeacherName;

    // events - teachers
    [JsonProperty("teacherPost")]
    public string TeacherPost;

    // events - teachers
    [JsonProperty("teacherDegree")]
    public string TeacherDegree;

    // events - teachers
    [JsonProperty("teacherAcademicTitle")]
    public string TeacherAcademicTitle;
    
    // events - departments
    [JsonProperty("department")]
    public string Department;
}

// Что нужно (Запросы):
// Rooms
// Buildings
// Teachers
// Departments
// Groups
// Events

// В редис ключ будет такой:
// calendarEventsByGroup:{groupId}
// Где groupId - id группы, которое можно получить по ключу
// groupIds