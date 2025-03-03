using Application.Storage.Entities;
using StackExchange.Redis;
using Version = Application.Controllers.DTO.Version;

namespace Application.Storage;

public class RedisService : IStorageService
{
    private readonly IDatabase _database;

    public RedisService(IDatabase database)
    {
        _database = database;
    }

    public void DeleteVersion()
    {
        throw new NotImplementedException();
    }

    public void SetVersion(Version version)
    {
        throw new NotImplementedException();
    }

    public Version GetVersion()
    {
        throw new NotImplementedException();
    }

    public void DeleteBuildings()
    {
        throw new NotImplementedException();
    }

    public Building GetBuilding(long id)
    {
        throw new NotImplementedException();
    }

    public void SetBuilding(long id, Building building)
    {
        throw new NotImplementedException();
    }

    public void DeleteTeachers()
    {
        throw new NotImplementedException();
    }

    public Teacher GetTeacher(long id)
    {
        throw new NotImplementedException();
    }

    public void SetTeacher(long id, Teacher teacher)
    {
        throw new NotImplementedException();
    }

    public void DeleteRooms()
    {
        throw new NotImplementedException();
    }

    public Room GetRoom(long id)
    {
        throw new NotImplementedException();
    }

    public void SetRoom(long id, Room room)
    {
        throw new NotImplementedException();
    }

    public void DeleteDepartments()
    {
        throw new NotImplementedException();
    }

    public Department GetDepartment(long id)
    {
        throw new NotImplementedException();
    }

    public void SetDepartment(long id, Department department)
    {
        throw new NotImplementedException();
    }

    public void DeleteGroups()
    {
        throw new NotImplementedException();
    }

    public long GetGroupId(Group group)
    {
        throw new NotImplementedException();
    }

    public void SetGroupId(Group group, long groupId)
    {
        throw new NotImplementedException();
    }

    public void DeleteEvents()
    {
        throw new NotImplementedException();
    }

    public List<Event> GetGroupEvents(long groupId)
    {
        throw new NotImplementedException();
    }

    public void SetGroupEvents(long groupId, List<Event> events)
    {
        throw new NotImplementedException();
    }
}