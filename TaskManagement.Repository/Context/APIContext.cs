using TaskManagement.Domain.DbModel;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace TaskManagement.Repository.Context
{
    [ExcludeFromCodeCoverage]
    public class APIContext : DbContext
    {
        public APIContext(DbContextOptions<APIContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Project
            var mbProject = modelBuilder.Entity<ProjectDbModel>();
            mbProject.ToTable("Project");
            mbProject.HasMany(x => x.Tasks).WithOne(y => y.Project).HasForeignKey(z => z.ProjectId);
            #endregion

            #region Task
            var mbTask = modelBuilder.Entity<TaskDbModel>();
            mbTask.ToTable("Task");
            mbTask.HasOne(x => x.Project).WithMany(y => y.Tasks).HasForeignKey(z => z.ProjectId);
            #endregion

            #region Log
            var mbLog = modelBuilder.Entity<LogDbModel>();
            mbLog.ToTable("Log");
            #endregion

            #region Comment
            var mbComment = modelBuilder.Entity<CommentDbModel>();
            mbComment.ToTable("Comment");
            mbComment.HasOne(x => x.Task).WithMany(y => y.Comments).HasForeignKey(z => z.TaskId);
            #endregion

        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            var AddedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Added).ToList();
            AddedEntities.ForEach(E =>
            {
                E.Property("CreatedAt").CurrentValue = DateTime.Now;
                SaveLog(AddedEntities);
            });

            var EditedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();
            EditedEntities.ForEach(E =>
            {
                E.Property("LastUpdatedAt").CurrentValue = DateTime.Now;
                SaveLog(EditedEntities);
            });

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SaveLog(List<Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry> entities)
        {
            foreach (var entry in entities)
            {
                foreach (var property in entry.OriginalValues.Properties)
                {
                    var original = entry.OriginalValues[property];
                    var current = entry.CurrentValues[property];

                    if (!object.Equals(original, current))
                    {
                        var changeLog = new LogDbModel
                        {
                            EntityName = entry.Entity.GetType().Name,
                            EntityId = (int)entry.OriginalValues["Id"],
                            PropertyName = property.Name,
                            OldValue = original?.ToString() ?? string.Empty,
                            NewValue = current?.ToString(),
                            CreatedAt = DateTime.Now
                        };

                        this.Logs.Add(changeLog);
                    }
                }
            }
        }

        public DbSet<TaskDbModel> Tasks { get; set; }
        public DbSet<ProjectDbModel> Projects { get; set; }
        public DbSet<LogDbModel> Logs { get; set; }
        public DbSet<CommentDbModel> Comments { get; set; }
        
    }
}