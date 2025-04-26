using StudentCenterAcademic.DTOs;

namespace StudentCenterAcademic.Interfaces;

public interface IStudentCenterAuthService
{
    Task<UserDataLoginDto> UserLogin(string Email, string PassWord);
    Task<ICollection<UserDto>> GetAllUsers();
    Task<UserDataLoginDto> AddNewUser(UserCreateDto user);
}
