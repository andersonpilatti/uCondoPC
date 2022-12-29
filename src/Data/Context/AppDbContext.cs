using Data.ModelConfiguration;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class AppDbContext
    : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.UseCollation("SQL_Latin1_General_CP1_CI_AI");

        builder.ApplyConfiguration(new PlanoContaEntityConfiguration());

        base.OnModelCreating(builder);
    }

    public DbSet<PlanoContaEntity> PlanoConta { get; set; }
}
