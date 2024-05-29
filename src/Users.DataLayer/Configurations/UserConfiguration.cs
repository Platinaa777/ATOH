using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.Users;

namespace Users.DataLayer.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        AddUsersConstraint(builder);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(x => x.Login)
            .HasColumnName("login");

        builder.Property(x => x.Password)
            .HasColumnName("password");

        builder.Property(x => x.Name)
            .HasColumnName("name");

        builder.Property(x => x.Gender)
            .HasColumnName("gender")
            .HasDefaultValue(2);

        builder.Property(x => x.Birthday)
            .HasColumnName("birthday");

        builder.Property(x => x.IsAdmin)
            .HasColumnName("is_admin");

        builder.Property(x => x.CreatedOn)
            .HasColumnName("created_on");

        builder.Property(x => x.CreatedBy)
            .HasColumnName("created_by");
        
        builder.Property(x => x.ModifiedOn)
            .HasColumnName("modified_on");
        
        builder.Property(x => x.ModifiedBy)
            .HasColumnName("modified_by");

        builder.Property(x => x.RevokedOn)
            .HasColumnName("revoked_on");

        builder.Property(x => x.RevokedBy)
            .HasColumnName("revoked_by");
    }

    private void AddUsersConstraint(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasIndex(x => x.Login)
            .IsUnique();
    }
}