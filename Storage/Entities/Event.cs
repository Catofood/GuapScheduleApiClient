namespace Application.Storage.Entities;

public class Event
{
    public string EventName;
    public long? EventDateStart;
    public long? EventDateEnd;
    public List<int> RoomIds;
    public List<int> TeacherIds;
    public int DepartmentId;
    public string EventType;
}