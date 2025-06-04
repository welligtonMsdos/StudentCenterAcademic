using Blazored.LocalStorage;
using StudentCenterAcademic.DTOs.RequestType;
using StudentCenterAcademic.DTOs.Response;
using StudentCenterAcademic.DTOs.Solicitation;
using StudentCenterAcademic.DTOs.StudentCenterBase;
using StudentCenterAcademic.DTOs.TimeLine;
using StudentCenterAcademic.Interfaces;
using StudentCenterAcademic.Util;
using System.Net.Http.Json;

namespace StudentCenterAcademic.Services;

public class StudentCenterService : IStudentCenterService
{
    private readonly HttpClient _client;
    private const string BASE_PATH = "api/v1/";   
    private const string SOLICITATION = "Solicitation";
    private const string STUDENT_CENTER_BASE = "StudentCenterBase";
    private const string REQUEST_TYPE = "RequestType";
    private const string TIME_LINE = "TimeLine";
    private string token;
    private readonly ISyncLocalStorageService _localStorage;

    public StudentCenterService(HttpClient cliente, ISyncLocalStorageService localStorage)
    {  
        _client = cliente ?? throw new ArgumentNullException(nameof(cliente));

        _localStorage = localStorage;

        token = _localStorage.GetItem<string>("token")?? "";

        _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
   
    public async Task<ICollection<SolicitationDto>> GetAllSolicitationPendingStatuses()
    { 
        var response = await _client.GetAsync(BASE_PATH + SOLICITATION + "/GetAllPendingStatuses");

        return await response.ReadContentAs<List<SolicitationDto>>();
    }

    public async Task<ApiResponseDto> UpdateStatus(int id, int statusId)
    {
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

    public async Task<ICollection<StudentCenterBaseDto>> GetAllStudentCenterBase()
    {
        var response = await _client.GetAsync(BASE_PATH + STUDENT_CENTER_BASE);

        return await response.ReadContentAs<List<StudentCenterBaseDto>>();
    }

    public async Task<ICollection<RequestTypeDto>> GetAllRequestType()
    {
        var response = await _client.GetAsync(BASE_PATH + REQUEST_TYPE + "/GetAll");

        return await response.ReadContentAs<List<RequestTypeDto>>();
    }

    public async Task<ApiResponseDto> PostRequestType(RequestTypeCreateDto requestTypeCreateDto)
    {
        try
        {
            var response = await _client.PostAsJson(BASE_PATH + REQUEST_TYPE, requestTypeCreateDto);

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> PutRequestType(RequestTypeUpdateDto requestTypeUpdateDto)
    {
        try
        {
            var endPoint = BASE_PATH + REQUEST_TYPE;

            var response = await _client.PutAsJson(endPoint, requestTypeUpdateDto);

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> DeleteRequestType(RequestTypeDto requestTypeDto)
    {
        try
        {
            var response = await _client.DeleteAsync($"{BASE_PATH + REQUEST_TYPE}/{requestTypeDto.Id}");

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> PostStudentCenterBase(StudentCenterBaseCreateDto studentCenterBaseCreateDto)
    {
        try
        {
            var response = await _client.PostAsJson(BASE_PATH + STUDENT_CENTER_BASE, studentCenterBaseCreateDto);

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> PutStudentCenterBase(StudentCenterBaseUpdateDto studentCenterBaseUpdateDto)
    {
        try
        {
            var endPoint = BASE_PATH + STUDENT_CENTER_BASE;

            var response = await _client.PutAsJson(endPoint, studentCenterBaseUpdateDto);

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ApiResponseDto> DeleteStudentCenterBase(StudentCenterBaseDto studentCenterBaseDto)
    {
        try
        {
            var response = await _client.DeleteAsync($"{BASE_PATH + STUDENT_CENTER_BASE}/{studentCenterBaseDto.Id}");

            return await response.ReadContentAs<ApiResponseDto>();
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro: {ex.Message}");
        }
    }

    public async Task<ICollection<TimeLineDto>> GetAllTimeLines(string studentId)
    {
        studentId = studentId.Replace("\"", "");

        string endPoint =$"{BASE_PATH}{TIME_LINE}/GetByStudentId?studentId={studentId}";       

        var response = await _client.GetAsync(endPoint);

        return await response.ReadContentAs<List<TimeLineDto>>();
    }
}
