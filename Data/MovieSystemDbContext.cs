using System;
using System.Collections.Generic;
using filmsystemet.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace filmsystemet.Data
{
    public class MovieSystemDbContext : DbContext
    {
        public MovieSystemDbContext()
        {
        }

        public MovieSystemDbContext(DbContextOptions<MovieSystemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FavouriteGenre> FavouriteGenres { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Person> Persons { get; set; }
		public object Person { get; internal set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // Laptop (Saturn)
                // optionsBuilder.UseSqlServer("Data Source=Saturn;Initial Catalog=MovieSystemDb;Integrated Security=True;");
                // DESKTOP (Jupiter)
                 optionsBuilder.UseSqlServer("Data Source=Jupiter;Initial Catalog=miniapiDb;Integrated Security=True;");
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<FavouriteGenre>(entity =>
        //    {
        //        entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");

        //        entity.HasOne(d => d.Genre)
        //            .WithMany(p => p.FavouriteGenres)
        //            .HasForeignKey(d => d.GenreId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_FavouriteGenres_Genres");

        //        entity.HasOne(d => d.Person)
        //            .WithMany(p => p.FavouriteGenres)
        //            .HasForeignKey(d => d.PersonId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK_FavouriteGenres_Persons");
        //    });

        //    modelBuilder.Entity<Genre>(entity =>
        //    {
        //        entity.Property(e => e.Description).HasMaxLength(200);
        //    });

        //    modelBuilder.Entity<Person>(entity =>
        //    {
        //        entity.Property(e => e.Email)
        //            .IsRequired()
        //            .HasMaxLength(50);

        //        entity.Property(e => e.FirstName)
        //            .IsRequired()
        //            .HasMaxLength(15);

        //        entity.Property(e => e.LastName).HasMaxLength(20);
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
