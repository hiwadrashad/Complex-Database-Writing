﻿// <auto-generated />
using System;
using Database_Test.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Database_Test.Migrations
{
    [DbContext(typeof(TestDatabase))]
    partial class TestDatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Database_Test.Models.Child", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("GrandChildId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GrandChildId");

                    b.ToTable("ChildDatabase");
                });

            modelBuilder.Entity("Database_Test.Models.CloneParent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChildId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.ToTable("CloneParentDatabase");
                });

            modelBuilder.Entity("Database_Test.Models.GrandChild", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("GrandChildDatabase");
                });

            modelBuilder.Entity("Database_Test.Models.Parent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChildId")
                        .HasColumnType("int");

                    b.Property<string>("GrandChildrenId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ChildId");

                    b.ToTable("ParentDatabase");
                });

            modelBuilder.Entity("Database_Test.Models.Child", b =>
                {
                    b.HasOne("Database_Test.Models.GrandChild", "GrandChild")
                        .WithMany()
                        .HasForeignKey("GrandChildId");

                    b.Navigation("GrandChild");
                });

            modelBuilder.Entity("Database_Test.Models.CloneParent", b =>
                {
                    b.HasOne("Database_Test.Models.Child", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId");

                    b.Navigation("Child");
                });

            modelBuilder.Entity("Database_Test.Models.GrandChild", b =>
                {
                    b.HasOne("Database_Test.Models.Parent", null)
                        .WithMany("GrandChildren")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("Database_Test.Models.Parent", b =>
                {
                    b.HasOne("Database_Test.Models.Child", "Child")
                        .WithMany()
                        .HasForeignKey("ChildId");

                    b.Navigation("Child");
                });

            modelBuilder.Entity("Database_Test.Models.Parent", b =>
                {
                    b.Navigation("GrandChildren");
                });
#pragma warning restore 612, 618
        }
    }
}
