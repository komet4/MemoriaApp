using MemoriaApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace MemoriaApi.Data;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<NoteTag> NoteTags { get; set; } = null!;
    public DbSet<NoteLink> NoteLinks { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
}