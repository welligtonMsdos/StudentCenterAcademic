using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using StudentCenterAcademic.DTOs;
using StudentCenterAcademic.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentCenterAcademic.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly IStudentCenterAuthService _service;
    private readonly ILocalStorageService _localStorage;
    private string token = "";
    private string name = "";

    public CustomAuthStateProvider(IStudentCenterAuthService service, 
                                   ILocalStorageService localStorage)
    {
        _service = service;
        _localStorage = localStorage;
    }

    public async void GeneratedCookie(string tokenValue)
    {
        await _localStorage.SetItemAsync("token", tokenValue);

        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(tokenValue);

        var claimsCookies = jwtToken.Claims.ToList();

        name =  claimsCookies[2].ToString().Replace("name: ", "");

        await _localStorage.SetItemAsync("userId", claimsCookies[1].ToString().Replace("id:", "").Trim());        
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity());

        try
        {
            if (token.Length == 0) new AuthenticationState(user);

            GeneratedCookie(token);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
            };

            var identity = new ClaimsIdentity(claims, token);

            user = new ClaimsPrincipal(identity); 

            return new AuthenticationState(user);
        }
        catch (Exception ex)
        {
            throw;
        }       
    }

    public async Task<ResponseDto> Login(string email, string password)
    {
        try
        {
            var response = await _service.UserLogin(email, password);

            if (response.success)
            {
                token = response.Data.Result;

                NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());               

                return new ResponseDto { success = true };
            }
            else
            {
                return new ResponseDto { success = false, message = response.message };
            }            
        }
        catch (Exception ex)
        {
            return new ResponseDto { success = false, message = ex.Message };
        }
    }

    public async Task Logout()
    {
        await _localStorage.RemoveItemAsync("token");

        await _localStorage.RemoveItemAsync("userId");

        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
    }
}
