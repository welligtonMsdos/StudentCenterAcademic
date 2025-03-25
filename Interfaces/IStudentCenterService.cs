using StudentCenterAcademic.DTOs;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses();
}
