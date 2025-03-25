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
}
