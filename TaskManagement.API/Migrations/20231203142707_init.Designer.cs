﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskManagement.Repository.Context;

#nullable disable

namespace TaskManagement.API.Migrations
{
    [DbContext(typeof(APIContext))]
    [Migration("20231203142707_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TaskManagement.Domain.DbModel.CommentDbModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdLastUpdate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Comment", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Domain.DbModel.LogDbModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("EntityId")
                        .HasColumnType("int");

                    b.Property<string>("EntityName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("NewValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("OldValue")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PropertyName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("UserIdCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdLastUpdate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Log", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Domain.DbModel.ProjectDbModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdLastUpdate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Project", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Domain.DbModel.TaskDbModel", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime?>("LastUpdatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Priority")
                        .HasColumnType("int");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdCreated")
                        .HasColumnType("int");

                    b.Property<int?>("UserIdLastUpdate")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Task", (string)null);
                });

            modelBuilder.Entity("TaskManagement.Domain.DbModel.CommentDbModel", b =>
                {
                    b.HasOne("TaskManagement.Domain.DbModel.TaskDbModel", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Domain.DbModel.TaskDbModel", b =>
                {
                    b.HasOne("TaskManagement.Domain.DbModel.ProjectDbModel", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TaskManagement.Domain.DbModel.ProjectDbModel", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManagement.Domain.DbModel.TaskDbModel", b =>
                {
                    b.Navigation("Comments");
                });
#pragma warning restore 612, 618
        }
    }
}
