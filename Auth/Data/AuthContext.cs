using Microsoft.EntityFrameworkCore;

namespace Auth.Data;

public class AuthContext: DbContext
{
    public AuthContext(DbContextOptions<AuthContext> options) : base(options)
    {
    }

    public virtual DbSet<AuthEntity> Tokens { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
    
    public virtual DbSet<UserRoleEntity> UsersRoles { get; set; }
    
    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=Yakovbook\\SQLEXPRESS;Database=AuthProgZona;User Id=sa;Password=sa;TrustServerCertificate=True;");
    }*/
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthenticationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
    }
}