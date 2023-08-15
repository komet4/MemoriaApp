namespace MemoriaApi.Controllers;

public class NoteLink : BaseEntity
{
    public int ParentNoteId { get; set; }
    public int ChildNoteId { get; set; }
}