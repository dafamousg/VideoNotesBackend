﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VideoNotesBackend.Data;

#nullable disable

namespace VideoNotesBackend.Migrations
{
    [DbContext(typeof(VideoNotesContext))]
    partial class VideoNotesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("VideoNotesBackend.Models.Note", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("Edited")
                        .HasColumnType("datetime2");

                    b.Property<string>("FreeText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("VideoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Notes");
                });

            modelBuilder.Entity("VideoNotesBackend.Models.Rating", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("VideoNotesBackend.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("NoteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("VideoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("NoteId");

                    b.HasIndex("VideoId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("VideoNotesBackend.Models.Video", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.Property<int?>("RatingId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Watched")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("VideoNotesBackend.Models.Tag", b =>
                {
                    b.HasOne("VideoNotesBackend.Models.Note", null)
                        .WithMany("Tags")
                        .HasForeignKey("NoteId");

                    b.HasOne("VideoNotesBackend.Models.Video", null)
                        .WithMany("Tags")
                        .HasForeignKey("VideoId");
                });

            modelBuilder.Entity("VideoNotesBackend.Models.Note", b =>
                {
                    b.Navigation("Tags");
                });

            modelBuilder.Entity("VideoNotesBackend.Models.Video", b =>
                {
                    b.Navigation("Tags");
                });
#pragma warning restore 612, 618
        }
    }
}
