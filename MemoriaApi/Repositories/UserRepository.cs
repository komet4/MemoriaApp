using MemoriaApi.Controllers;
using MemoriaApi.Data;
using MemoriaApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MemoriaApi.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context) => _context = context;
    
    public async Task CreateAsync(User user)
    {
        _context.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .OrderByDescending(u => u.CreatedDateTime)
            .ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User user)
    {
        _context.Remove(user);
        await _context.SaveChangesAsync();
    }
}