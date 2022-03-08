﻿// <auto-generated />
using System;
using ExcerciseApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExcerciseApp.API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220308003035_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExcerciseApp.API.Models.BaseExcercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkoutId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("WorkoutId");

                    b.ToTable("BaseExcercise");
                });

            modelBuilder.Entity("ExcerciseApp.API.Models.Workout", b =>
                {
                    b.Property<int>("WorkoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("TimeEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TimeStart")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isCompleted")
                        .HasColumnType("bit");

                    b.HasKey("WorkoutId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("ExcerciseApp.API.Models.CardioExcercise", b =>
                {
                    b.HasBaseType("ExcerciseApp.API.Models.BaseExcercise");

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.ToTable("CardioExcercise");
                });

            modelBuilder.Entity("ExcerciseApp.API.Models.WeightLiftingExcercise", b =>
                {
                    b.HasBaseType("ExcerciseApp.API.Models.BaseExcercise");

                    b.Property<int>("Reps")
                        .HasColumnType("int");

                    b.Property<int>("Sets")
                        .HasColumnType("int");

                    b.Property<double>("Wieght")
                        .HasColumnType("float");

                    b.ToTable("WeightLiftingExcercise");
                });

            modelBuilder.Entity("ExcerciseApp.API.Models.BaseExcercise", b =>
                {
                    b.HasOne("ExcerciseApp.API.Models.Workout", "Workout")
                        .WithMany("Excercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("ExcerciseApp.API.Models.CardioExcercise", b =>
                {
                    b.HasOne("ExcerciseApp.API.Models.BaseExcercise", null)
                        .WithOne()
                        .HasForeignKey("ExcerciseApp.API.Models.CardioExcercise", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExcerciseApp.API.Models.WeightLiftingExcercise", b =>
                {
                    b.HasOne("ExcerciseApp.API.Models.BaseExcercise", null)
                        .WithOne()
                        .HasForeignKey("ExcerciseApp.API.Models.WeightLiftingExcercise", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExcerciseApp.API.Models.Workout", b =>
                {
                    b.Navigation("Excercises");
                });
#pragma warning restore 612, 618
        }
    }
}