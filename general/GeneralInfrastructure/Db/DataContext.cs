using GeneralDomain.Configs;
using GeneralDomain.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GeneralInfrastructure.Db;

public class DataContext:DbContext
{

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DataContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    public DbSet<User> User { get; set; }
    public DbSet<UserFriends> UserFriends { get; set; }
    public DbSet<UserPhotos> UserPhotos { get; set; }
}