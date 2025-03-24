using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class ExternalApiController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ExternalApiController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts(int userId, string title)
    {
        var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts?userId={userId}&title={title}");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("posts/{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] object user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("https://reqres.in/api/users", content);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }

    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] object user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PutAsync($"https://reqres.in/api/users/{id}", content);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(result);
    }

    [HttpDelete("posts/{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        if (response.IsSuccessStatusCode)
        {
            return Ok(new { message = "Deleted" });
        }
        return BadRequest(new { message = "failed" });
    }
}
