using Blazored.LocalStorage;
using StudentCenterAcademic.DTOs.Response;
using StudentCenterAcademic.DTOs.User;
using StudentCenterAcademic.Interfaces;
using StudentCenterAcademic.Util;

namespace StudentCenterAcademic.Services;

public class StudentCenterAuthService : IStudentCenterAuthService
{
    private readonly HttpClient _client;
    private const string BASE_PATH = "api/v1/";
    private const string LOGIN = "Login";
    private const string USER = "User";
    private string token;
    private readonly ISyncLocalStorageService _localStorage;

    public StudentCenterAuthService(HttpClient cliente, ISyncLocalStorageService localStorage)
    {
        _client = cliente ?? throw new ArgumentNullException(nameof(cliente));

        _localStorage = localStorage;

        token = _localStorage.GetItem<string>("token");

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
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

    public async Task<ICollection<UserDto>> GetAllUsers()
    {
        var response = await _client.GetAsync(BASE_PATH + USER + "/GetUsers");

        return await response.ReadContentAs<List<UserDto>>();
    }

    public async Task<UserDataLoginDto> AddNewUser(UserCreateDto user)
    {
        try
        {
            var response = await _client.PostAsJson(BASE_PATH + USER, user);

            return await response.ReadContentAs<UserDataLoginDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> UpdateNameAndEmail(string id, UserUpdateDto user)
    {
        try
        {
            var endPoint = BASE_PATH + USER + "/UpdateUserNameAndEmail/" + id;

            var response = await _client.PutAsJson(endPoint, user);

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> DeleteByEmail(string email)
    {
        try
        {
            var response = await _client.DeleteAsync($"{BASE_PATH + USER}/{email}");

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }   
}
