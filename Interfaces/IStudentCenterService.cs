using StudentCenterAcademic.DTOs;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses();
    Task<ApiResponseDto> UpdateStatus(int id, int statusId);
}
