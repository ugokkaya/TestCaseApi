using Microsoft.EntityFrameworkCore;
using TestCaseApi.Models;

namespace TestCaseApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public DbSet<TestCaseEntity> TestCases { get; set; } = null!;
}