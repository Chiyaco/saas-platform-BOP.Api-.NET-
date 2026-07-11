namespace SaaSPlatform.Web.Services;

public class ApiService<T> : IApiService<T>
{
    private readonly HttpClient _http;
    private readonly string _baseApiAddress = "https://localhost:7214/";

    public ApiService(HttpClient http)
    {
        _http = http;
    }
    private string Url(string endpoint)
    {
        return $"{_baseApiAddress}{endpoint}";
    }
    public async Task<List<T>> GetAllAsync(string endpoint)
    {
        return await _http.GetFromJsonAsync<List<T>>(endpoint) ?? new List<T>();
    }

    public async Task<T?> GetAsync(string endpoint, int id)
    {
        return await _http.GetFromJsonAsync<T>($"{Url(endpoint)}/{id}");
    }

    public async Task<T?> CreateAsync(string endpoint, T model)
    {
        var result = await _http.PostAsJsonAsync(Url(endpoint), model);

        return await result.Content.ReadFromJsonAsync<T>();
    }

    public async Task<T?> UpdateAsync(string endpoint, int id, T model)
    {
        var result = await _http.PutAsJsonAsync($"{Url(endpoint)}/{id}", model);

        return await result.Content.ReadFromJsonAsync<T>();
    }

    public async Task DeleteAsync(string endpoint, int id)
    {
        await _http.DeleteAsync($"{Url(endpoint)}/{id}");
    }
}
