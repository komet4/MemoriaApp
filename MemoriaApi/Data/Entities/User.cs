namespace MemoriaApi.Controllers;

public class User : BaseEntity
{
    public string Username { get; set; }
    public byte[] PasswordHash { get; set; }
}