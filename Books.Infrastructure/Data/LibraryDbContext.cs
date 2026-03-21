using Books.Domain.Entities;
using Books.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Books.Infrastructure.Data
{
    public class LibraryDbContext:DbContext
    {
        public DbSet<BookEntity> Books { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<GenreEntity> Genres { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RefreshTokenEntity> refreshTokens { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<CityEntity> City { get; set; }
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            if (Database.IsMySql())
            {
                modelBuilder.Entity<BookEntity>()
                .Property(b => b.CreatedAt)
                .HasColumnType("datetime(6)")        // точность микросекунд
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .ValueGeneratedOnAdd();
            }
            else if (Database.IsSqlServer())
            {
                modelBuilder.Entity<BookEntity>()
                    .Property(b => b.CreatedAt)
                    .HasDefaultValueSql("SYSDATETIME()");
                modelBuilder.Entity<UserEntity>()
                    .Property(u => u.Role)
                    .HasConversion<int>() 
                    .IsRequired();
                modelBuilder.Entity<UserEntity>()
                    .ToTable(t => t.HasCheckConstraint(
                         "CK_User_Role",
                         $"Role IN ({string.Join(",", Enum.GetValues(typeof(UserRole)).Cast<int>())})"
                         ));
            }
            modelBuilder.Entity<UserEntity>().HasIndex(u => u.Email).IsUnique();
            //modelBuilder.Entity<BookEntity>()
            //    .Property(b => b.CreatedAt)
            //    .HasDefaultValueSql("SYSDATETIME()")
            //    .IsRequired(false);
        }
        
    }
}
