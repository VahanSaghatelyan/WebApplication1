using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication1.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersApiController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public UsersApiController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpPost("users")]
    public async Task<ActionResult<UserResponseModel>> CreateUser([FromBody] UserResponseModel user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://reqres.in/api/users", content);
        var result = await response.Content.ReadAsStringAsync();
        return Created("https://reqres.in/api/users", result);
    }

    [HttpPut("users/{id}")]
    public async Task<ActionResult<UserResponseModel>> UpdateUser(int id, [FromBody] UserResponseModel user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"https://reqres.in/api/users/{id}", content);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }

}
