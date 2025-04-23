using StudentCenterAcademic.DTOs;
using StudentCenterAcademic.Interfaces;
using StudentCenterAcademic.Util;

namespace StudentCenterAcademic.Services;

public class StudentCenterAuthService : IStudentCenterAuthService
{
    private readonly HttpClient _client;
    private const string BASE_PATH = "api/v1/";
    private const string LOGIN = "Login";

    public StudentCenterAuthService(HttpClient cliente)
    {
        _client = cliente ?? throw new ArgumentNullException(nameof(cliente));
    }

    public async Task<UserDataLoginDto> UserLogin(string Email, string PassWord)
    {
        try
        {
            var user = new UserLoginDto(Email, PassWord);

            var response = await _client.PostAsJson(BASE_PATH + LOGIN, user);

            return await response.ReadContentAs<UserDataLoginDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }       
    }
}
