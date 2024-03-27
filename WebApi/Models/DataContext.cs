using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;

namespace WebApi.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options)
    : base(options)
    {
    }

    public DbSet<Colaborator> Colaborators { get; set; }
    public DbSet<Holiday> Holiday { get; set; }
    public DbSet<HolidayPeriod> HolidayPeriod { get; set; }
    public DbSet<Holidays> Holidays { get; set; }
    public DbSet<Associate> Associate { get; set; }
    public DbSet<Project> Project { get; set; }

    // protected override void OnModelCreating(ModelBuilder builder)
    // {
    //     base.OnModelCreating(builder);

    //     builder
    //         .Entity<IColaborator>()
    //         .Property(x => x.Id)
    //         .IsRequired();
    // }

    // protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    // {
    //     base.ConfigureConventions(configurationBuilder);

    //     configurationBuilder.Properties<IColaborator>(opt =>
    //     {
    //         opt.HaveConversion<Colaborator>();
    //     });
    // }
}