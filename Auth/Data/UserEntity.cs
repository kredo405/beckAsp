using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data;

public class UserEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public virtual List<AuthEntity> Tokens { get; set; }
    public virtual List<UserRoleEntity> Roles { get; set; }
}

public class UserEntityConfiguration: IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(x => x.Name).IsRequired();

        builder.Property(x => x.PasswordHash).IsRequired();
        
        
        builder
            .HasMany(x => x.Tokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Roles)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}