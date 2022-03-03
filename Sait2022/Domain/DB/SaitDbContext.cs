using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sait2022.Domain.Model;
using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.DB
{
    public class SaitDbContext: IdentityDbContext<Users, IdentityRole<int>,int>
    {
        public SaitDbContext(DbContextOptions<SaitDbContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }

        /// <summary>
        /// Пользователи
        /// </summary>
        public override DbSet<Users> Users { get; set; }

        /// <summary>
        /// Сотрудники
        /// </summary>
        public DbSet<Employee> Employees { get; private set; }

        /// <summary>
        /// Справочник тем вопросов
        /// </summary>
        public DbSet<QuestionsTopic> QuestionsTopics { get; set; }

        /// <summary>
        /// Справочник рангов вопросов
        /// </summary>
        public DbSet<Rangs> Rangs { get; set; }

        /// <summary>
        /// База вопросов
        /// </summary>
        public DbSet<Questions> Questions { get; set; }

        /// <summary>
        /// База ответов
        /// </summary>
        public DbSet<Answers> Answers { get; set; }

        /// <summary>
        /// Ответы ученика
        /// </summary>
        public DbSet<MainOut> Main_Outs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Users>(x =>
            {
                x.HasOne(y => y.Employee)
                .WithOne()
                .HasForeignKey<Users>("EmployeeId")
                .IsRequired(true);
                x.HasIndex("EmployeeId").IsUnique(true);
            });

            #region Employee

            builder.Entity<Employee>(b =>
            {
                b.ToTable("Employees");
                EntityId(b);
                b.Property(x => x.Surname)
                    .HasColumnName("Surname")
                    .IsRequired(true);
                b.Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired(true);
                b.Property(x => x.Patronym)
                    .HasColumnName("Patronym")
                    .IsRequired();
                b.Property(x => x.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .IsRequired(true);
                b.Property(x => x.Address)
                    .HasColumnName("Address")
                    .IsRequired();
                b.Ignore(x => x.FullName);
                b.Property(x => x.EmailAddress)
                    .HasColumnName("EmailAddress")
                    .IsRequired(true);
                b.Property(x => x.IsTeacher)
                    .HasColumnName("IsTeacher")
                    .IsRequired(true);
                b.Property(x => x.IsAdministrator)
                    .HasColumnName("IsAdministrator")
                    .IsRequired(true);
                b.HasOne(t => t.Employees)
                    .WithMany(y => y.Employeess)
                    .HasForeignKey(x => x.Employees.TeacherId);
                b.HasIndex("TeacherId").IsUnique(true);
            });
            #endregion

            #region QuestionsTopic
            builder.Entity<QuestionsTopic>(b =>
            {
                b.ToTable("QuestionsTopic");
                EntityId(b);
                b.Property(x => x.Topic)
                    .HasColumnName("Topic")
                    .IsRequired();
            });
            #endregion

            #region Rangs
            builder.Entity<Rangs>(b =>
            {
                b.ToTable("Rangs");
                EntityId(b);
                b.Property(x => x.RangQuest)
                    .HasColumnName("RangQuest")
                    .IsRequired();
            });
            #endregion

            #region Answers
            builder.Entity<Answers>(b =>
            {
                b.ToTable("Answers");
                EntityId(b);
                b.Property(x => x.NumberAnswer)
                    .HasColumnName("NumberAnswer")
                    .IsRequired(true);
                b.Property(x => x.ValueAnswer)
                    .HasColumnName("ValueAnswer")
                    .IsRequired(true);
            });
            #endregion

            #region Questions
            builder.Entity<Questions>(b =>
            {
                b.ToTable("Questions");
                EntityId(b);
                b.HasOne(y => y.QuestionsTopic)
                    .WithMany(y => y.Questions)
                    .HasForeignKey(x => x.QuestionsTopicId)
                    .IsRequired(true);
                b.HasIndex("QuestionsTopicId").IsUnique(true);
                b.Property(x => x.NumberQuest)
                    .HasColumnName("NumberQuest")
                    .IsRequired(true);
                b.Property(x => x.ValueQuest)
                    .HasColumnName("ValueQuest")
                    .IsRequired(true);               
                b.HasOne(y => y.Rangs)
                    .WithMany(y => y.Questions)
                    .HasForeignKey(x => x.RangsId)
                    .IsRequired(true);
                b.HasIndex("RangsId").IsUnique(true);
                b.HasOne(y => y.Answers)
                    .WithMany(y => y.Questions)
                    .HasForeignKey(x => x.AnswersId)
                    .IsRequired(true);
                b.HasIndex("AnswersId").IsUnique(true);
            });
            #endregion

            #region MainOut
            builder.Entity<MainOut>(b =>
            {
                b.ToTable("MainOut");
                EntityId(b);
                b.Property(x => x.NumberAnswer)
                    .HasColumnName("NumberAnswer")
                    .IsRequired(true);
                b.HasOne(t => t.Questions)
                    .WithMany(y => y.Main_out)
                    .HasForeignKey(x => x.QuestionsId)
                    .IsRequired(true);
                b.HasIndex("QuestionsId").IsUnique(true);
                b.Property(x => x.ValueAnswer)
                    .HasColumnName("ValueAnswer")
                    .IsRequired(true);
                b.Property(x => x.CheckAnswer)
                    .HasColumnName("CheckAnswer")
                    .IsRequired(true);
            });
            #endregion

            #region LogsAnswers
            builder.Entity<LogsAnswers>(b =>
            {
                b.ToTable("LogsAnswers");
                EntityId(b);
                //отношение многие ко многим
                b.HasOne(t => t.Users)
                    .WithMany(y => y.LogsAnswers)
                    .HasForeignKey(x => x.UsersId)
                    .IsRequired(true);
                b.HasIndex("UsersId").IsUnique(true);

                b.HasOne(t => t.MainOut)
                    .WithMany(y => y.LogsAnswers)
                    .HasForeignKey(x => x.MainOutId)
                    .IsRequired(true);
                b.HasIndex("MainOutId").IsUnique(true);
            });
            #endregion
        }

        /// <summary>
        /// Описание идентификатора сущности модели
        /// </summary>
        /// <typeparam name="TEntity">Тип сущности</typeparam>
        /// <param name="builder">Построитель модели данных</param>
        private static void EntityId<TEntity>(EntityTypeBuilder<TEntity> builder)
            where TEntity : Entity
        {
            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder.HasKey(x => x.Id)
                .HasAnnotation("Npgsql:Serial", true);
        }
    }
}
