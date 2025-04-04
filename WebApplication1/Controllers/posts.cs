using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using WebApplication1.Models;
using System.Net;

[ApiController]
[Route("api/[controller]")]
public class PostsApiController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public PostsApiController(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    [HttpGet("posts")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPosts(int userId, string title)
    {
        var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts?userId={userId}&title={title}");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpGet("posts/{id}")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPostById(int id)
    {
        var response = await _httpClient.GetAsync($"https://jsonplaceholder.typicode.com/posts/{id}");
        var content = await response.Content.ReadAsStringAsync();
        return Ok(content);
    }

    [HttpDelete("posts/{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var response = await _httpClient.DeleteAsync($"https://jsonplaceholder.typicode.com/posts/{id}");

        if (response.IsSuccessStatusCode)
        {
            return NoContent();
        }

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return NotFound(new { message = "not foundd" });
        }

        return StatusCode((int)response.StatusCode, new { message = "Failed" });
    }

}
