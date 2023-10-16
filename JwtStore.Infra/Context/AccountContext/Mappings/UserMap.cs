using JwtStore.Core.Context.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtStore.Infra.Context.AccountContext.Mappings;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        const string tableName = "user";

        builder.ToTable(tableName);

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .HasColumnName("name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property( user => user.Image)
        .HasColumnName("image")
        .HasColumnType("VARCHAR")
        .HasMaxLength(255)
        .IsRequired(false);

        builder.OwnsOne(user => user.Email)
        .Property(email => email.Address)
        .HasColumnName("email")
        .HasColumnType("VARCHAR")
        .HasMaxLength(250)
        .IsRequired();

        builder.OwnsOne(user => user.Email)
            .OwnsOne(email => email.Verification)
            .Property(vr => vr.Code)
            .HasColumnName("emailVerificationCode")
            .IsRequired();

        builder.OwnsOne(user => user.Email)
        .OwnsOne(email => email.Verification)
        .Property(vr => vr.ExpiresAt)
        .HasColumnName("emailVerificationExpiresAt")
        .IsRequired(false);

        builder.OwnsOne(user => user.Email)
        .OwnsOne(email => email.Verification)
        .Property(vr => vr.VerifiedAt)
        .HasColumnName("emailVerificationVerifiedAt")
        .IsRequired(false);


        builder.OwnsOne(user => user.Email)
        .OwnsOne(email => email.Verification)
        .Ignore(vr => vr.IsActive);
        
        builder.OwnsOne(user => user.Password)
        .Property(pw => pw.Hash)
        .HasColumnName("passwordHash")
        .IsRequired();

        builder.OwnsOne(user => user.Password)
        .Property(pw => pw.ResetCode)
        .HasColumnName("passwordResetCode")
        .IsRequired();
    }
}