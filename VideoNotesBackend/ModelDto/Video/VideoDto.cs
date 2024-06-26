﻿using System.ComponentModel.DataAnnotations;
using VideoNotesBackend.ModelDto.Rating;
using VideoNotesBackend.ModelDto.Tag;
using VideoNotesBackend.Models;

namespace VideoNotesBackend.ModelDto.Video
{
    public class VideoDto: IEntity
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public bool Watched { get; set; }
        public TimeSpan Duration { get; set; }
        public RatingDto? Rating { get; set; }

        [Url]
        public string? URL { get; set; }
        public List<TagDto>? Tags { get; set; }

        public List<NoteDto>? Notes { get; set; }
    }
}
