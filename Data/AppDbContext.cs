using Microsoft.EntityFrameworkCore;
using TestingPlatform.Models;

namespace TestingPlatform.Data;

public class AppDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Direction> Directions => Set<Direction>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Answer> Answers => Set<Answer>();
    public DbSet<Attempt> Attempts => Set<Attempt>();
    public DbSet<UserAttemptAnswer> UserAttemptAnswers => Set<UserAttemptAnswer>();
    public DbSet<UserSelectedOption> UserSelectedOptions => Set<UserSelectedOption>();
    public DbSet<UserTextAnswer> UserTextAnswers => Set<UserTextAnswer>();
    public DbSet<TestResult> TestResults => Set<TestResult>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.Login).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Login).IsRequired().HasMaxLength(50);
            e.Property(x => x.Email).IsRequired().HasMaxLength(100);
            e.Property(x => x.PasswordHash).IsRequired();
            e.Property(x => x.FirstName).IsRequired();
            e.Property(x => x.LastName).IsRequired();
            e.Property(x => x.Role).HasConversion<string>();
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(x => x.Student)
                .WithOne(s => s.User)
                .HasForeignKey<Student>(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Student>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => x.UserId).IsUnique();
            e.Property(x => x.Phone).IsRequired().HasMaxLength(30);
            e.Property(x => x.VkProfileLink).IsRequired();
        });

        modelBuilder.Entity<Direction>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Course>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Project>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Group>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).IsRequired().HasMaxLength(100);
            e.HasIndex(x => x.Name).IsUnique();

            e.HasOne(x => x.Direction)
                .WithMany(d => d.Groups)
                .HasForeignKey(x => x.DirectionId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Course)
                .WithMany(c => c.Groups)
                .HasForeignKey(x => x.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Project)
                .WithMany(p => p.Groups)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Test>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired();
            e.Property(x => x.Description).IsRequired();
            e.Property(x => x.Type).HasConversion<string>();
            e.Property(x => x.IsRepeatable).HasDefaultValue(false);
            e.Property(x => x.IsPublic).HasDefaultValue(false);
            e.Property(x => x.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.Property(x => x.PublishedAt).IsRequired();
            e.Property(x => x.Deadline).IsRequired();
        });

        modelBuilder.Entity<Question>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Text).IsRequired();
            e.Property(x => x.Number).IsRequired();
            e.Property(x => x.Description).HasMaxLength(2000);
            e.Property(x => x.AnswerType).HasConversion<string>();
            e.Property(x => x.IsScoring).HasDefaultValue(true);
            e.HasIndex(x => new { x.TestId, x.Number }).IsUnique();
            e.HasOne(x => x.Test)
                .WithMany(t => t.Questions)
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Answer>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Text).IsRequired();
            e.Property(x => x.IsCorrect).IsRequired();
            e.HasOne(x => x.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Attempt>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.StartedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            e.HasOne(x => x.Test)
                .WithMany()
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Student)
                .WithMany(s => s.Attempts)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<UserAttemptAnswer>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasIndex(x => new { x.AttemptId, x.QuestionId }).IsUnique();

            e.HasOne(x => x.Attempt)
                .WithMany(a => a.UserAttemptAnswers)
                .HasForeignKey(x => x.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Question)
                .WithMany(q => q.UserAttemptAnswers)
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserSelectedOption>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.UserAttemptAnswer)
                .WithMany(a => a.UserSelectedOptions)
                .HasForeignKey(x => x.UserAttemptAnswerId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Answer)
                .WithMany(a => a.UserSelectedOptions)
                .HasForeignKey(x => x.AnswerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<UserTextAnswer>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.TextAnswer).IsRequired();
            e.HasOne(x => x.UserAttemptAnswer)
                .WithOne(a => a.UserTextAnswer)
                .HasForeignKey<UserTextAnswer>(x => x.UserAttemptAnswerId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TestResult>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Passed).IsRequired();
            e.HasIndex(x => new { x.TestId, x.StudentId, x.AttemptId }).IsUnique();

            e.HasOne(x => x.Test)
                .WithMany()
                .HasForeignKey(x => x.TestId)
                .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(x => x.Attempt)
                .WithMany()
                .HasForeignKey(x => x.AttemptId)
                .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(x => x.Student)
                .WithMany(s => s.TestResults)
                .HasForeignKey(x => x.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Test>()
            .HasMany(t => t.Students)
            .WithMany(s => s.Tests)
            .UsingEntity<TestStudent>(
                j => j.HasOne(x => x.Student).WithMany().HasForeignKey(x => x.StudentId),
                j => j.HasOne(x => x.Test).WithMany().HasForeignKey(x => x.TestId),
                j => { j.HasKey(x => new { x.TestId, x.StudentId }); j.ToTable("test_students"); });

        modelBuilder.Entity<Test>()
            .HasMany(t => t.Projects)
            .WithMany(p => p.Tests)
            .UsingEntity<TestProject>(
                j => j.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId),
                j => j.HasOne(x => x.Test).WithMany().HasForeignKey(x => x.TestId),
                j => { j.HasKey(x => new { x.TestId, x.ProjectId }); j.ToTable("test_projects"); });

        modelBuilder.Entity<Test>()
            .HasMany(t => t.Courses)
            .WithMany(c => c.Tests)
            .UsingEntity<TestCourse>(
                j => j.HasOne(x => x.Course).WithMany().HasForeignKey(x => x.CourseId),
                j => j.HasOne(x => x.Test).WithMany().HasForeignKey(x => x.TestId),
                j => { j.HasKey(x => new { x.TestId, x.CourseId }); j.ToTable("test_courses"); });

        modelBuilder.Entity<Test>()
            .HasMany(t => t.Groups)
            .WithMany(g => g.Tests)
            .UsingEntity<TestGroup>(
                j => j.HasOne(x => x.Group).WithMany().HasForeignKey(x => x.GroupId),
                j => j.HasOne(x => x.Test).WithMany().HasForeignKey(x => x.TestId),
                j => { j.HasKey(x => new { x.TestId, x.GroupId }); j.ToTable("test_groups"); });

        modelBuilder.Entity<Test>()
            .HasMany(t => t.Directions)
            .WithMany(d => d.Tests)
            .UsingEntity<TestDirection>(
                j => j.HasOne(x => x.Direction).WithMany().HasForeignKey(x => x.DirectionId),
                j => j.HasOne(x => x.Test).WithMany().HasForeignKey(x => x.TestId),
                j => { j.HasKey(x => new { x.TestId, x.DirectionId }); j.ToTable("test_directions"); });

        modelBuilder.Entity<Direction>().HasData(
            new Direction { Id = 1, Name = "Backend" },
            new Direction { Id = 2, Name = "Frontend" },
            new Direction { Id = 3, Name = "Design" }
        );

        modelBuilder.Entity<Course>().HasData(
            new Course { Id = 1, Name = "1 курс" },
            new Course { Id = 2, Name = "2 курс" },
            new Course { Id = 3, Name = "3 курс" }
        );

        modelBuilder.Entity<Project>().HasData(
            new Project { Id = 1, Name = "ПАЗЛ" },
            new Project { Id = 2, Name = "КОД" }
        );

        base.OnModelCreating(modelBuilder);
    }
}