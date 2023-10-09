using MemoriaApi.Controllers;
using MemoriaApi.Data;
using MemoriaApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemoriaApi.Repositories;

public class TagRepository : ITagRepository
{
    private readonly AppDbContext _context;
    
    public TagRepository(AppDbContext context) => _context = context;
    
    public async Task CreateAsync(Tag tag)
    {
        _context.Add(tag);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        return await _context.Tags
            .OrderByDescending(t => t.TagName)
            .ToListAsync();
    }

    public async Task UpdateAsync(Tag tag)
    {
        _context.Tags.Update(tag);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag tag)
    {
        _context.Remove(tag);
        await _context.SaveChangesAsync();
    }
}