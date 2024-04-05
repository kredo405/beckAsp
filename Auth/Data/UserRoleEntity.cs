using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data;

public class UserRoleEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }
    
    public virtual UserEntity User { get; set; }
    public virtual RoleEntity Role { get; set; }
}
public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRoleEntity>
{
    public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
    {
        builder.ToTable("UserRoles");

        builder.HasKey(x => new { x.UserId, x.RoleId });
        
    }
} 