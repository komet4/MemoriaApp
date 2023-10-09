using MemoriaApi.Controllers;

namespace MemoriaApi.Interfaces;

public interface ITagRepository
{
    public Task CreateAsync(Tag tag);
    
    public Task<List<Tag>> GetAllAsync();
    
    public Task UpdateAsync(Tag tag);
    
    public Task DeleteAsync(Tag tag);
}