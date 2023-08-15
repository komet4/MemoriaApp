using MemoriaApi.Controllers;
using Microsoft.EntityFrameworkCore;

namespace MemoriaApi.Data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions options) : base(options)
    {
        Database.Migrate();
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Note> Notes { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<NoteTag> NoteTags { get; set; }
    public DbSet<NoteLink> NoteLinks { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}