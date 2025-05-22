using StudentCenterAcademic.DTOs.Response;
using StudentCenterAcademic.DTOs.User;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterAuthService
{
    Task<UserDataLoginDto> UserLogin(string Email, string PassWord);
    Task<ICollection<UserDto>> GetAllUsers();
    Task<UserDataLoginDto> AddNewUser(UserCreateDto user);
    Task<ApiResponseDto> UpdateNameAndEmail(string id, UserUpdateDto user);
    Task<ApiResponseDto> DeleteByEmail(string email);
}
