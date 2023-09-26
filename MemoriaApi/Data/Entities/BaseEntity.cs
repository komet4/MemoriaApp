using System.ComponentModel.DataAnnotations;

namespace MemoriaApi.Controllers;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
    public DateTime CreatedDateTime { get; set; }
}