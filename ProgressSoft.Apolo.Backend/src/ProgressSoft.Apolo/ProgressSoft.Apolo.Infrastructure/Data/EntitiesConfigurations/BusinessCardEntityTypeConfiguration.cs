using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure;

public class BusinessCardEntityTypeConfiguration : IEntityTypeConfiguration<BusinessCard>
{
    public void Configure(EntityTypeBuilder<BusinessCard> builder)
    {
        builder.ToTable("BusinessCards", schema: "dbo");

        #region Properties
        builder.Property(bc => bc.Id)
            .HasColumnOrder(0)
            .ValueGeneratedOnAdd();

        builder.Property(bc => bc.Name)
            .HasColumnOrder(1)
            .HasColumnType("nvarchar(120)")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(bc => bc.Gender)
            .HasColumnOrder(2)
            .HasColumnType("tinyint")
            .IsRequired();

        builder.Property(bc => bc.BirthOfDate)
            .HasColumnOrder(3)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(bc => bc.Email)
            .HasColumnOrder(4)
            .HasColumnType("varchar(320)")
            .IsRequired()
            .IsUnicode(false);

        builder.Property(bc => bc.Phone)
            .HasColumnOrder(5)
            .HasColumnType("varchar(20)")
            .HasMaxLength(20)
            .IsRequired()
            .IsUnicode(false);

        builder.Property(bc => bc.ImageId)
            .HasColumnOrder(7)
            .HasColumnType("int");

        builder.Property(bc => bc.Address)
            .HasColumnOrder(6)
            .HasColumnType("varchar(60)")
            .HasMaxLength(60)
            .IsRequired()
            .IsUnicode(false);
        #endregion

        #region Relationships
        builder.HasOne(bc => bc.Image)
            .WithMany(i => i.BusinessCards)
            .HasForeignKey(bc => bc.ImageId)
            .HasConstraintName("FK_Image_BusinessCard");
        #endregion


        #region Constraints
        builder.HasKey(bc => bc.Id)
            .HasName("PrimaryKey_BusinessCards");

        builder.HasIndex(bc => bc.Name, "Unique_BusinessCard_Name")
            .IsUnique();

        builder.HasIndex(bc => bc.Email, "Unique_BusinessCard_Email")
            .IsUnique();

        builder.HasIndex(bc => bc.Phone, "Unique_BusinessCard_Phone")
            .IsUnique();
        #endregion
    }
}
