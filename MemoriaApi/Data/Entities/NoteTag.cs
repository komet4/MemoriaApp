namespace MemoriaApi.Controllers;

public class NoteTag : BaseEntity
{
    public int NoteId { get; set; }
    public int TagId { get; set; }
}