using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using WebAppTemplate.Application.DTOs;
using WebAppTemplate.Application.DTOs.Requests;
using WebAppTemplate.Application.DTOs.Responses;

namespace WebAppTemplate.IntegrationTests;

public class AccountEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AccountEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task SignIn_WithSeededAdmin_ReturnsJwt()
    {
        var signInResponse = await _client.PostAsJsonAsync(
            "/api/Account/sign-in",
            new LoginRequest("admin", "Admin@123"));

        signInResponse.StatusCode.Should().Be(HttpStatusCode.OK);

        var payload = await signInResponse.Content.ReadFromJsonAsync<ApiResponse<LoginResponse>>(JsonOptions);
        payload.Should().NotBeNull();
        payload!.Success.Should().BeTrue();
        payload.Data!.AccessToken.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task SignIn_WithInvalidCredentials_ReturnsBadRequest()
    {
        var signInResponse = await _client.PostAsJsonAsync(
            "/api/Account/sign-in",
            new LoginRequest("admin", "WrongPassword@1"));

        signInResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task HealthCheck_ReturnsOk()
    {
        var response = await _client.GetAsync("/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
