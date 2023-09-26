using System.ComponentModel.DataAnnotations;

namespace MemoriaApi.Controllers;

public class RefreshToken
{
    [Key]
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}