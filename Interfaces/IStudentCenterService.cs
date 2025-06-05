using StudentCenterAcademic.DTOs.Response;
using StudentCenterAcademic.DTOs.Solicitation;
using StudentCenterAcademic.DTOs.StudentCenterBase;
using StudentCenterAcademic.DTOs.RequestType;
using StudentCenterAcademic.DTOs.TimeLine;
using StudentCenterAcademic.DTOs.Dashboard;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<DashboardDto>> GetDashboardByStudentId(string studentId);
    Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses();
    Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase();
    Task<ICollection<TimeLineDto>> GetAllTimeLines(string studentId);
    Task<ICollection<RequestTypeDto>> GetAllRequestType();
    Task<ApiResponseDto> PostStudentCenterBase(StudentCenterBaseCreateDto studentCenterBaseCreateDto);
    Task<ApiResponseDto> PostRequestType(RequestTypeCreateDto requestTypeCreateDto);   
    Task<ApiResponseDto> PutStudentCenterBase(StudentCenterBaseUpdateDto studentCenterBaseUpdateDto);
    Task<ApiResponseDto> PutRequestType(RequestTypeUpdateDto requestTypeUpdateDto);   
    Task<ApiResponseDto> DeleteStudentCenterBase(StudentCenterBaseDto studentCenterBaseDto);
    Task<ApiResponseDto> DeleteRequestType(RequestTypeDto requestTypeDto);    
    Task<ApiResponseDto> UpdateStatus(int id, int statusId);
}
