using StudentCenterAcademic.DTOs.Response;
using StudentCenterAcademic.DTOs.Solicitation;
using StudentCenterAcademic.DTOs.StudentCenterBase;
using StudentCenterAcademic.DTOs.RequestType;
using StudentCenterAcademic.DTOs.TimeLine;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterService
{
    Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses();
    Task<ICollection<TimeLineDto>> GetAllTimeLines(string studentId);
    Task<ApiResponseDto> UpdateStatus(int id, int statusId);
    Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase();
    Task<ICollection<RequestTypeDto>> GetAllRequestType();
    Task<ApiResponseDto> PostRequestType(RequestTypeCreateDto requestTypeCreateDto);
    Task<ApiResponseDto> PutRequestType(RequestTypeUpdateDto requestTypeUpdateDto);
    Task<ApiResponseDto> DeleteRequestType(RequestTypeDto requestTypeDto);
    Task<ApiResponseDto> PostStudentCenterBase(StudentCenterBaseCreateDto studentCenterBaseCreateDto);
    Task<ApiResponseDto> PutStudentCenterBase(StudentCenterBaseUpdateDto studentCenterBaseUpdateDto);
    Task<ApiResponseDto> DeleteStudentCenterBase(StudentCenterBaseDto studentCenterBaseDto);
}
