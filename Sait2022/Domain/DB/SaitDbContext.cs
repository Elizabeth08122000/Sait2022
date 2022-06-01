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
        /// База ответов студентов
        /// </summary>
        public DbSet<StudentAnswer> StudentAnswers { get; set; }

        /// <summary>
        /// База выбранных тем вопросов учителем
        /// </summary>
        public DbSet<TeacherTopic> TeacherTopics { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Users>(x =>
            {
                x.HasOne(y => y.Employee)
                .WithOne()
                .HasForeignKey<Users>(b=>b.EmployeeId)
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
                    .IsRequired();
                b.Property(x => x.FirstName)
                    .HasColumnName("FirstName")
                    .IsRequired();
                b.Property(x => x.Patronym)
                    .HasColumnName("Patronym")
                    .IsRequired(false);
                b.Property(x => x.PhoneNumber)
                    .HasColumnName("PhoneNumber")
                    .IsRequired(false);
                b.Property(x => x.Address)
                    .HasColumnName("Address")
                    .IsRequired(false);
                b.Ignore(x => x.FullName);
                b.Property(x => x.IsTeacher)
                    .HasColumnName("IsTeacher");
                b.Property(x => x.IsAdministrator)
                    .HasColumnName("IsAdministrator");
                b.HasOne(t => t.EmployeesNavig)
                    .WithMany(y => y.Employeess)
                    .HasForeignKey(t => t.TeacherId);
                b.Property(x => x.TeacherId)
                    .HasColumnName("TeacherId");
                b.Property(x => x.pathZoom)
                    .HasColumnName("pathZoom")
                    .IsRequired(false);
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
                b.Property(x => x.IsUsedNow)
                    .HasColumnName("IsUsedNow")
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
                    .HasMaxLength(2)
                    .IsRequired();
            });
            #endregion

            #region Questions
            builder.Entity<Questions>(b =>
            {
                b.ToTable("Questions");
                EntityId(b);
                b.HasOne(u => u.QuestionsTopic)
                    .WithMany(t => t.Questions)
                    .HasForeignKey(p => p.QuestionTopcId);
                b.HasOne(u => u.Rangs)
                    .WithMany(y => y.Questions)
                    .HasForeignKey(r => r.RangsId);
                b.Property(x => x.NumberQuest)
                    .HasColumnName("NumberQuest")
                    .IsRequired();
                b.Property(x => x.ValueQuest)
                    .HasColumnName("ValueQuest")
                    .IsRequired();
                b.Property(x => x.ValueAnswer)
                    .HasColumnName("ValueAnswer")
                    .IsRequired();
                b.Property(x => x.Name)
                    .HasColumnName("Name")
                    .IsRequired(false);
                b.Property(x => x.Path)
                    .HasColumnName("Path")
                    .IsRequired(false);
                b.Property(x => x.NamePict)
                    .HasColumnName("NamePict")
                    .IsRequired(false);
                b.Property(x => x.PathPict)
                    .HasColumnName("PathPict")
                    .IsRequired(false);
                b.HasMany(b => b.Users)
                    .WithMany(b => b.Questions)
                    .UsingEntity(j => j.ToTable("LogsAnswers")); //настроена промежуточная таблица
            });
            #endregion

            #region StudentAnswers
            builder.Entity<StudentAnswer>(b =>
            {
                b.ToTable("StudentAnswers");
                EntityId(b);
                b.HasOne(u => u.TeacherTopic)
                    .WithMany(r => r.StudentAnswers)
                    .HasForeignKey(y => y.TeacherTopicId);
                b.HasOne(u => u.Student)
                    .WithMany(x => x.StudentAnswers)
                    .HasForeignKey(y => y.StudentId);
                b.HasOne(u => u.Questions)
                    .WithMany(x => x.StudentAnswers)
                    .HasForeignKey(y => y.QuestionId);
                b.HasOne(u => u.QuestionsTopic)
                    .WithMany(x => x.StudentAnswers)
                    .HasForeignKey(y => y.QuestionsTopicId);
                b.HasOne(u => u.Rangs)
                    .WithMany(x => x.StudentAnswers)
                    .HasForeignKey(x => x.RangId);
                b.Property(x => x.StudentId)
                    .HasColumnName("StudentId")
                    .IsRequired();
                b.Property(x => x.TeacherTopicId)
                    .HasColumnName("TeacherTopicId")
                    .IsRequired();
                b.Property(x => x.QuestionId)
                    .HasColumnName("QuestionId")
                    .IsRequired();
                b.Property(x => x.QuestionsTopicId)
                    .HasColumnName("QuestionsTopicId")
                    .IsRequired(false);
                b.Property(x => x.RangId)
                    .HasColumnName("RangId")
                    .IsRequired();
                b.Property(x => x.Answer)
                    .HasColumnName("Answer");
                b.Property(x => x.IsCheck)
                    .HasColumnName("IsCheck")
                    .IsRequired();
                b.Property(x => x.Result)
                    .HasColumnName("Result")
                    .IsRequired(false);
            });
            #endregion

            #region TeacherTopic
            builder.Entity<TeacherTopic>(b =>
            {
                b.ToTable("TeacherTopic");
                EntityId(b);
                b.Property(x => x.IsUsedNow)
                    .HasColumnName("IsUsedNow")
                    .IsRequired();
                b.HasOne(u => u.QuestionsTopic)
                    .WithMany(x => x.TeacherTopics)
                    .HasForeignKey(y => y.QuestionsTopicId);
                b.Property(x => x.QuestionsTopicId)
                    .HasColumnName("QuestionsTopicId")
                    .IsRequired(false);
                b.HasOne(u => u.Student)
                    .WithMany(x => x.TeacherTopics)
                    .HasForeignKey(y => y.StudentId);
                b.Property(x => x.StudentId)
                    .HasColumnName("StudentId")
                    .IsRequired();

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
