namespace MemoriaApi.Controllers;

public class Note : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime UpdateDateTime { get; set; }
    public List<NoteLink> LinkedNotes { get; set; }
    public List<Tag> Tags { get; set; }
}