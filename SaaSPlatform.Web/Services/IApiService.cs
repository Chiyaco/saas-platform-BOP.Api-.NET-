namespace SaaSPlatform.Web.Services;

public interface IApiService<T>
{
    Task<List<T>> GetAllAsync(string endpoint);
    Task<T?> GetAsync(string endpoint, int id);
    Task<T?> CreateAsync(string endpoint, T model);
    Task<T?> UpdateAsync(string endpoint, int id, T model);
    Task DeleteAsync(string endpoint, int id);
}