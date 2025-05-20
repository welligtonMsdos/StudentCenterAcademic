using StudentCenterAcademic.DTOs.Response;
using StudentCenterAcademic.DTOs.Solicitation;
using StudentCenterAcademic.DTOs.StudentCenterBase;
using StudentCenterAcademic.DTOs.RequestType;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses();
    Task<ApiResponseDto> UpdateStatus(int id, int statusId);
    Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase();
    Task<ICollection<RequestTypeDto>> GetAllRequestType();
}
