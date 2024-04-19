namespace VideoNotesBackend.Models
{
    public class Note
    {
        public Guid Id { get; set; }

        // Not sure of correct
        public Guid VideoId { get; set; }
        public required string Title { get; set; }
        public string? FreeText { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime Edited { get; set; }

        // Will add Tags later
        // Not sure if this is correct
        // public List<string>? Tags { get; private set; } = new List<string>(); // Existing & new
    }
}
