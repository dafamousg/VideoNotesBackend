namespace VideoNotesBackend.ModelDto
{
    public class NoteCreate
    {
        // Not sure of correct
        public Guid? VideoId { get; set; }
        public string? Title { get; set; }
        public string? FreeText { get; set; }

        // Will add Tags later
        // Not sure if this is correct
        // public List<string>? Tags { get; private set; } = new List<string>(); // Existing & new
        
    }
}
