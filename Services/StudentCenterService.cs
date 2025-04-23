using Blazored.LocalStorage;
using StudentCenterAcademic.DTOs;
using StudentCenterAcademic.Interfaces;
using StudentCenterAcademic.Util;
using System.Net.Http.Json;


namespace StudentCenterAcademic.Services;

public class StudentCenterService : IStudentCenterService
{
    private readonly HttpClient _client;
    private const string BASE_PATH = "api/v1/";   
    private const string SOLICITATION = "Solicitation";
    private string token;
    private readonly ISyncLocalStorageService _localStorage;

    public StudentCenterService(HttpClient cliente, ISyncLocalStorageService localStorage)
    {  
        _client = cliente ?? throw new ArgumentNullException(nameof(cliente));

        _localStorage = localStorage;

        token = _localStorage.GetItem<string>("token");

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
   
    public async Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses()
    {
        //token = await _localStorage.GetItemAsync<string>("token");

        //_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _client.GetAsync(BASE_PATH + SOLICITATION + "/GetAllPendingStatuses");

        return await response.ReadContentAs<List<SolicitationDto>>();
    }

    public async Task<ApiResponseDto> UpdateStatus(int id, int statusId)
    {
        //token = await _localStorage.GetItemAsync<string>("token");

        //_client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var endPoint = BASE_PATH + SOLICITATION;

        var requestContent = new
        {
            id = id,
            statusId = statusId
        };
       
        var response = await _client.PatchAsync(endPoint, JsonContent.Create(requestContent));
       
        if (response.IsSuccessStatusCode)
        {            
            return await response.Content.ReadFromJsonAsync<ApiResponseDto>();
        }
        else
        {
            throw new Exception($"Erro ao atualizar status. Código de status: {response.StatusCode}");
        }
    }
}
