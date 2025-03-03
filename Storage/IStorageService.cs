using Application.Storage.Entities;
using Version = Application.Controllers.DTO.Version;

namespace Application.Storage;

public interface IStorageService
{
    void DeleteVersion();
    void SetVersion(Version version);
    Version GetVersion();
    
    void DeleteBuildings();
    Building GetBuilding(long id);
    void SetBuilding(long id, Building building);
    
    void DeleteTeachers();
    Teacher GetTeacher(long id);
    void SetTeacher(long id, Teacher teacher);

    void DeleteRooms();
    Room GetRoom(long id);
    void SetRoom(long id, Room room);

    void DeleteDepartments();
    Department GetDepartment(long id);
    void SetDepartment(long id, Department department);
    
    void DeleteGroups();
    long GetGroupId(Group group);
    void SetGroupId(Group group, long groupId);
    
    
    void DeleteEvents();
    List<Event> GetGroupEvents(long groupId);
    void SetGroupEvents(long groupId, List<Event> events);
}
