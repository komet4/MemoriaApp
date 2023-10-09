using MemoriaApi.Controllers;

namespace MemoriaApi.Interfaces;

public interface INoteRepository
{
    public Task CreateAsync(Note note);
    
    public Task<List<Note>> GetAllAsync();
    
    public Task UpdateAsync(Note note);
    
    public Task DeleteAsync(Note note);
}