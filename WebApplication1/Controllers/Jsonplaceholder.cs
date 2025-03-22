using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ExternalApiController(HttpClient httpClient) : ControllerBase
{

    //1
    [HttpGet("posts")]
    public async Task<IActionResult> GetPosts(int userId, string title)
    {
        var response = await httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts?userId={userId}&title={title}");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }


    //2
    [HttpGet("posts/{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var response = await httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    //3
    [HttpPost("users")]
    public async Task<IActionResult> CreateUser([FromBody] object user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("https://reqres.in/api/users", content);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    //4
    [HttpPut("users/{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] object user)
    {
        var json = JsonSerializer.Serialize(user);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"https://reqres.in/api/users/{id}", content);
        var result = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    //5
    [HttpDelete("posts/{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var response = await httpClient.DeleteAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        if (response.IsSuccessStatusCode)
        {
            return Ok(new { message = "Deleted" });
        }
        return BadRequest(new { message = "failed" });
    }
}
