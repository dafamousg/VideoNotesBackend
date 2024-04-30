﻿using System.ComponentModel.DataAnnotations;
using VideoNotesBackend.ModelDto.Tag;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto.Video
{
    public class VideoCreate
    {
        [Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }

        public int RatingId { get; set; }

        [Url]
        public string? URL { get; set; }

        //public ICollection<Note>? Notes { get; set; }

        public List<TagDto> Tags { get; set; } = new List<TagDto>();
    }
}
