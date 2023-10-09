using MemoriaApi.Controllers;

namespace MemoriaApi.Interfaces;

public interface IUserRepository
{
    public Task CreateAsync(User user);
    
    public Task<List<User>> GetAllAsync();
    
    public Task UpdateAsync(User user);
    
    public Task DeleteAsync(User user);
}