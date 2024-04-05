using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Data;

public class AuthEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime IssueDate { get; set; } // дата выдачи токена
    public DateTime ExpireDate { get; set; } // дата истечения токена
    public virtual UserEntity User { get; set; }
}
public class AuthenticationEntityConfiguration: IEntityTypeConfiguration<AuthEntity>
{
    public void Configure(EntityTypeBuilder<AuthEntity> builder)
    {
        builder.ToTable("Authentication");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();

        builder.Property(x => x.IssueDate).HasColumnType("datetime2(7)");
        builder.Property(x => x.ExpireDate).HasColumnType("datetime2(7)");
    }
}