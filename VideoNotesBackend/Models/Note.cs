﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VideoNotesBackend.Models
{
    public class Note: IEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = new Guid();

        [ForeignKey("Video")]
        public Guid? VideoId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }
        public string? FreeText { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public DateTime? Edited { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
