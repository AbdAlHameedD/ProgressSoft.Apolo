using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProgressSoft.Apolo.Domain;

namespace ProgressSoft.Apolo.Infrastructure.Data.EntitiesConfigurations
{
    internal class ImageEntityTypeConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("Images", schema: "asset");

            #region Properties
            builder.Property(i => i.Id)
                .HasColumnOrder(0)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.EncodedImage)
                .HasColumnOrder(1)
                .HasColumnType("varbinary(max)")
                .IsRequired();

            builder.Property(i => i.Type)
                .HasColumnOrder(2)
                .HasColumnType("varchar(10)")
                .HasMaxLength(10)
                .IsRequired();
            #endregion

            #region Constraints
            builder.HasKey(i => i.Id)
                .HasName("PrimaryKey_Images");
            #endregion
        }
    }
}
