using StudentCenterAcademic.DTOs;
using StudentCenterAcademic.Interfaces;
using System.Net.Http.Json;
using System.Net;
using StudentCenterAcademic.Pages;
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
        //var endPoint = BASE_PATH + LOGIN;
       
        //var user = new UserLoginDto(Email, PassWord);

        try
        {
            //var response = await _httpClient.PostAsync(endPoint, JsonContent.Create(user));

            //if (response.IsSuccessStatusCode)
            //    return await response.Content.ReadFromJsonAsync<UserDataLoginDto>();
            //else
            //    throw new Exception($"Erro: {response.StatusCode}");

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
