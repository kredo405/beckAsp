using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data;

public class RoleEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public virtual List<UserRoleEntity> Users { get; set; }
    
}

public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
{
    public void Configure(EntityTypeBuilder<RoleEntity> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.HasIndex(x => x.Name).IsUnique();
        builder.Property(x => x.Name).HasMaxLength(32);
        
        builder
            .HasMany(x => x.Users)
            .WithOne(x => x.Role)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        
    }
}