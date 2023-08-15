namespace MemoriaApi.Controllers;

public class Tag : BaseEntity
{
    public string TagName { get; set; }
    public List<Note> Notes { get; set; }
}