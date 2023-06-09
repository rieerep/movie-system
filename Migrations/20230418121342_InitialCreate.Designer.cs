﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using filmsystemet.Data;

#nullable disable

namespace filmsystemet.Migrations
{
    [DbContext(typeof(MovieSystemDbContext))]
    [Migration("20230418121342_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("filmsystemet.Models.FavouriteGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GenreId")
                        .HasColumnType("int");

                    b.Property<string>("Movies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.Property<decimal?>("Rating")
                        .HasColumnType("decimal(18,0)");

                    b.HasKey("Id");

                    b.HasIndex("GenreId");

                    b.HasIndex("PersonId");

                    b.ToTable("FavouriteGenres");
                });

            modelBuilder.Entity("filmsystemet.Models.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("filmsystemet.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("filmsystemet.Models.FavouriteGenre", b =>
                {
                    b.HasOne("filmsystemet.Models.Genre", "Genre")
                        .WithMany("FavouriteGenres")
                        .HasForeignKey("GenreId")
                        .IsRequired()
                        .HasConstraintName("FK_FavouriteGenres_Genres");

                    b.HasOne("filmsystemet.Models.Person", "Person")
                        .WithMany("FavouriteGenres")
                        .HasForeignKey("PersonId")
                        .IsRequired()
                        .HasConstraintName("FK_FavouriteGenres_Persons");

                    b.Navigation("Genre");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("filmsystemet.Models.Genre", b =>
                {
                    b.Navigation("FavouriteGenres");
                });

            modelBuilder.Entity("filmsystemet.Models.Person", b =>
                {
                    b.Navigation("FavouriteGenres");
                });
#pragma warning restore 612, 618
        }
    }
}
