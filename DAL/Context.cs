﻿using Microsoft.EntityFrameworkCore;
using WeatherAPI.DAL.Models;

namespace WeatherAPI.DAL;

public class Context : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=WeatherAPI;Username=postgres;Password=13171416");
    }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Models.Region> Regions { get; set; } = null!;
    public DbSet<Models.RegionType> RegionTypes { get; set; } = null!;
    public DbSet<Weather> Weathers { get; set; } = null!;
    public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;
}