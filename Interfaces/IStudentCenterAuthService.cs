using StudentCenterAcademic.DTOs.Response;
using StudentCenterAcademic.DTOs.User;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterAuthService
{
    Task<UserDataLoginDto> UserLogin(string Email, string PassWord);
    Task<ICollection<UserDto>> GetAllUsers();
    Task<UserDataLoginDto> AddNewUser(UserCreateDto user);
    Task<ResponseDto> UpdateNameAndEmail(string id, UserUpdateDto user);
    Task<ResponseDto> DeleteByEmail(string email);
}
