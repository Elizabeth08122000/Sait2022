﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sait2022.Domain.DB;

namespace Sait2022.Migrations
{
    [DbContext(typeof(SaitDbContext))]
    [Migration("20220418114225_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("QuestionsUsers", b =>
                {
                    b.Property<long>("QuestionsId")
                        .HasColumnType("bigint");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("QuestionsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LogsAnswers");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Answers", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("NumberAnswer")
                        .HasColumnType("integer")
                        .HasColumnName("NumberAnswer");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint");

                    b.Property<string>("ValueAnswer")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ValueAnswer");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:Serial", true);

                    b.HasIndex("QuestionId")
                        .IsUnique();

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Employee", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("Address");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FirstName");

                    b.Property<bool>("IsAdministrator")
                        .HasColumnType("boolean")
                        .HasColumnName("IsAdministrator");

                    b.Property<bool>("IsTeacher")
                        .HasColumnType("boolean")
                        .HasColumnName("IsTeacher");

                    b.Property<string>("Patronym")
                        .HasColumnType("text")
                        .HasColumnName("Patronym");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("PhoneNumber");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Surname");

                    b.Property<long?>("TeacherId")
                        .HasColumnType("bigint")
                        .HasColumnName("TeacherId");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:Serial", true);

                    b.HasIndex("TeacherId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Questions", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("NumberQuest")
                        .HasColumnType("integer")
                        .HasColumnName("NumberQuest");

                    b.Property<long>("QuestionTopcId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("RangsId")
                        .HasColumnType("bigint");

                    b.Property<string>("ValueQuest")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ValueQuest");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:Serial", true);

                    b.HasIndex("QuestionTopcId");

                    b.HasIndex("RangsId");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.QuestionsTopic", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Topic");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:Serial", true);

                    b.ToTable("QuestionsTopic");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Rangs", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<char>("RangQuest")
                        .HasColumnType("character(1)")
                        .HasColumnName("RangQuest");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:Serial", true);

                    b.ToTable("Rangs");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.StudentAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Answer")
                        .HasColumnType("text")
                        .HasColumnName("Answer");

                    b.Property<bool>("IsCheck")
                        .HasColumnType("boolean")
                        .HasColumnName("IsCheck");

                    b.Property<long>("QuestionId")
                        .HasColumnType("bigint")
                        .HasColumnName("QuestionId");

                    b.Property<long>("QuestionsTopicId")
                        .HasColumnType("bigint")
                        .HasColumnName("QuestionsTopicId");

                    b.Property<long>("RangId")
                        .HasColumnType("bigint")
                        .HasColumnName("RangId");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint")
                        .HasColumnName("StudentId");

                    b.HasKey("Id")
                        .HasAnnotation("Npgsql:Serial", true);

                    b.HasIndex("QuestionId");

                    b.HasIndex("QuestionsTopicId");

                    b.HasIndex("RangId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAnswers");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<long>("EmployeeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<int>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sait2022.Domain.Model.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Users", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("QuestionsUsers", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Questions", null)
                        .WithMany()
                        .HasForeignKey("QuestionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sait2022.Domain.Model.Users", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Answers", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Questions", "Questions")
                        .WithOne("Answers")
                        .HasForeignKey("Sait2022.Domain.Model.Answers", "QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questions");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Employee", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Employee", "EmployeesNavig")
                        .WithMany("Employeess")
                        .HasForeignKey("TeacherId");

                    b.Navigation("EmployeesNavig");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Questions", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.QuestionsTopic", "QuestionsTopic")
                        .WithMany("Questions")
                        .HasForeignKey("QuestionTopcId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sait2022.Domain.Model.Rangs", "Rangs")
                        .WithMany("Questions")
                        .HasForeignKey("RangsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("QuestionsTopic");

                    b.Navigation("Rangs");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.StudentAnswer", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Questions", "Questions")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sait2022.Domain.Model.QuestionsTopic", "QuestionsTopic")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("QuestionsTopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sait2022.Domain.Model.Rangs", "Rangs")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("RangId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sait2022.Domain.Model.Employee", "Student")
                        .WithMany("StudentAnswers")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Questions");

                    b.Navigation("QuestionsTopic");

                    b.Navigation("Rangs");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Users", b =>
                {
                    b.HasOne("Sait2022.Domain.Model.Employee", "Employee")
                        .WithOne()
                        .HasForeignKey("Sait2022.Domain.Model.Users", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Employee", b =>
                {
                    b.Navigation("Employeess");

                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Questions", b =>
                {
                    b.Navigation("Answers");

                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.QuestionsTopic", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("StudentAnswers");
                });

            modelBuilder.Entity("Sait2022.Domain.Model.Rangs", b =>
                {
                    b.Navigation("Questions");

                    b.Navigation("StudentAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}