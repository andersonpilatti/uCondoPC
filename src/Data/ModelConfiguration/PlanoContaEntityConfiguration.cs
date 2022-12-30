using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.ModelConfiguration;

internal class PlanoContaEntityConfiguration
    : IEntityTypeConfiguration<PlanoContaEntity>
{
    public void Configure(EntityTypeBuilder<PlanoContaEntity> builder)
    {
        builder.ToTable("PlanoConta", "dbo");

        builder.HasKey(h => h.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();

        builder
            .HasOne(o => o.ContaPai)
            .WithMany(o => o.ContasFilhas)
            .HasForeignKey(d => d.IdPai)
            .OnDelete(DeleteBehavior.NoAction);


    }
}
