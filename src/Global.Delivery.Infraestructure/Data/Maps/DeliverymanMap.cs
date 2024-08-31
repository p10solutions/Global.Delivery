using Global.Delivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Global.Delivery.Infraestructure.Data.Maps
{
    public class DeliverymanMap : IEntityTypeConfiguration<DeliverymanEntity>
    {
        public void Configure(EntityTypeBuilder<DeliverymanEntity> builder)
        {
            builder.ToTable("TB_DELIVERYMAN");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("ID");

            builder.Property(x => x.Name)
                .HasColumnName("NAME")
                .HasColumnType("varchar(200)");

            builder.Property(x => x.Document)
                .HasColumnName("DOCUMENT")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.DateOfBirth)
                .HasColumnName("DT_OF_BIRTH")
                .HasColumnType("timestamp without time zone");

            builder.Property(x => x.LicenseNumber)
                .HasColumnName("LICENSE_NUMBER")
                .HasColumnType("varchar(20)");

            builder.Property(x => x.LicenseType)
                .HasColumnName("LICENSE_TYPE");

            builder.Property(x => x.LicenseImage)
                .HasColumnName("LICENSE_IMAGE")
                .HasColumnType("varchar(200)");
        }
    }
}
