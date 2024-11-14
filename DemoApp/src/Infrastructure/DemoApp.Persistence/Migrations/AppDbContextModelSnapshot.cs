﻿// <auto-generated />
using System;
using DemoApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DemoApp.Persistence.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DemoApp.Domain.Entities.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = new Guid("18e7c816-e488-41f9-9329-d363ab085d5f"),
                            CreatedAt = new DateTime(2024, 11, 14, 14, 53, 25, 365, DateTimeKind.Utc).AddTicks(5812),
                            CreatedBy = "System",
                            Description = "Bags for men and women",
                            IsDeleted = false,
                            Name = "Bags"
                        },
                        new
                        {
                            Id = new Guid("84a330de-56ea-4a01-a0c5-3e94df5ee54f"),
                            CreatedAt = new DateTime(2024, 11, 14, 14, 53, 25, 365, DateTimeKind.Utc).AddTicks(5828),
                            CreatedBy = "System",
                            Description = "Unisex shoes",
                            IsDeleted = false,
                            Name = "Shoes"
                        },
                        new
                        {
                            Id = new Guid("6dfdfc60-0047-499c-91bb-95fa51b09b23"),
                            CreatedAt = new DateTime(2024, 11, 14, 14, 53, 25, 365, DateTimeKind.Utc).AddTicks(5835),
                            CreatedBy = "System",
                            Description = "Belts for men",
                            IsDeleted = false,
                            Name = "Belts"
                        });
                });

            modelBuilder.Entity("DemoApp.Domain.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DemoApp.Domain.Entities.Product", b =>
                {
                    b.HasOne("DemoApp.Domain.Entities.Category", "Category")
                        .WithMany("products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DemoApp.Domain.Entities.Category", b =>
                {
                    b.Navigation("products");
                });
#pragma warning restore 612, 618
        }
    }
}
