using StudentCenterAcademic.DTOs;
using StudentCenterAcademic.Interfaces;
using System.Net.Http.Json;


namespace StudentCenterAcademic.Services;

public class StudentCenterService : IStudentCenterService
{
    private readonly HttpClient _httpClient;
    private const string BASE_PATH = "/api/v1/";   
    private const string SOLICITATION = "Solicitation";

    public StudentCenterService(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("StudentCenterAcademicAPI");
    }
   
    public async Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses()
    {
        var endPoint = BASE_PATH + SOLICITATION + "/GetAllPendingStatuses";        

        return await _httpClient.GetFromJsonAsync<ICollection<SolicitationDto>>(endPoint);
    }

    public async Task<ApiResponseDto> UpdateStatus(int id, int statusId)
    {
        var endPoint = BASE_PATH + SOLICITATION;

        var requestContent = new
        {
            id = id,
            statusId = statusId
        };
       
        var response = await _httpClient.PatchAsync(endPoint, JsonContent.Create(requestContent));
       
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
