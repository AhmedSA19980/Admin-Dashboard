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
        public DbSet<userRole> UserRoles { get; set; }

        public DbSet<AuditLogs> AuditLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => { 
            
              
                entity.HasIndex(u => u.Email);

                entity.HasIndex(u => u.UserName);

                entity.HasMany(u => u.RefreshToken)
                .WithOne(rt => rt.User).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Restrict);
            
            
            });

            modelBuilder.Entity<RefreshToken>(entity => {

               
                entity.Property(rt => rt.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(rt => rt.RefreshTok).IsRequired().HasMaxLength(450);
                entity.Property(rt => rt.RefreshTok).HasColumnName("RefreshToken");
                entity.HasIndex(u => u.RefreshTok);

           

            });
           
            modelBuilder.Entity<userRole>(entity => {

                entity.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(u => u.UserId).OnDelete(DeleteBehavior.Restrict);
               
            });

            modelBuilder.Entity<userRole>(entity => {

                entity.HasOne(r => r.u).WithMany(r => r.Id).HasForeignKey(r => r.).OnDelete(DeleteBehavior.Restrict);



            });


            modelBuilder.Entity<Role>(entity => {

             
                entity.HasIndex(r => r.Name);



            });

        }


    }
}
