
namespace Client.Contracts.Models;

public class CalendarEvent
{ 
    // events - groups
    public string Group;

    // events - rooms
    public string Room;

    // events - rooms - buildings
    public string Building;

    // events
    public string EventType;

    // events
    public string EventName;

    // events
    public DateTime EventTimeStart;

    // events
    public DateTime EventTimeEnd;

    // events - teachers
    public string TeacherName;

    // events - teachers
    public string TeacherPost;

    // events - teachers
    public string TeacherDegree;

    // events - teachers
    public string TeacherAcademicTitle;
    
    // events - departments
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