using Admin.Core.models;

using Microsoft.EntityFrameworkCore;


namespace Admin.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<AuditLogs> AuditLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { 
            
              
                entity.HasIndex(u => u.Email);

                entity.HasIndex(u => u.UserName);

                entity.HasMany(u => u.RefreshToken)
                .WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Restrict);
            
                entity.Property(u => u.IsUserDeleted).HasDefaultValue(false);
            
            });

            modelBuilder.Entity<RefreshToken>(entity => {

               
                entity.Property(rt => rt.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(rt => rt.RefreshTok).IsRequired().HasMaxLength(450);
                entity.Property(rt => rt.RefreshTok).HasColumnName("RefreshToken");
                entity.HasIndex(u => u.RefreshTok);

           

            });
           
            modelBuilder.Entity<UserRole>(entity => {

                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
               
            });

            modelBuilder.Entity<UserRole>(entity => {

                entity.HasOne(r => r.Role).WithMany(r => r.UserRoles).HasForeignKey(r => r.RoleId).OnDelete(DeleteBehavior.Restrict);


            });


            modelBuilder.Entity<Role>(entity => {

             
                entity.HasIndex(r => r.Name);



            });

            modelBuilder.Entity<AuditLogs>(entity =>
            {
                entity.Property(al => al.LoggedDate).HasDefaultValueSql("GETUTCDATE()");
            });

        }


    }
}
