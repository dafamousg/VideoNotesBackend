﻿using System.ComponentModel.DataAnnotations;

namespace VideoNotesBackend.Models
{
    public class Video
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate {  get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }
        
        public int? RatingId { get; set; }

        // Will add tags later
        // Not sure if this is correct
        //public string[]? Tags { get; set; }  // Existing & new

        [Url(ErrorMessage = "URL is not a valid URL")]
        public string? URL { get; set; }

        //public ICollection<Note>? Notes { get; set; }
    }
}
