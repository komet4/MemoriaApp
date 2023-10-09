using MemoriaApi.Controllers;
using MemoriaApi.Data;
using MemoriaApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemoriaApi.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;
    
    public NoteRepository(AppDbContext context) => _context = context;
    
    public async Task CreateAsync(Note note)
    {
        _context.Add(note);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Note>> GetAllAsync()
    {
        return await _context.Notes
            .OrderByDescending(n => n.UpdateDateTime)
            .ToListAsync();
    }

    public async Task UpdateAsync(Note note)
    {
        _context.Notes.Update(note);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Note note)
    {
        _context.Remove(note);
        await _context.SaveChangesAsync();
    }
}