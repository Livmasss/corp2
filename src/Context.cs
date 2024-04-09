using Microsoft.EntityFrameworkCore;

// Определение контекста данных
public class RealEstateContext : DbContext
{
    public DbSet<Type> Types { get; set; }
    public DbSet<District> Districts { get; set; }
    public DbSet<BuildingMaterial> BuildingMaterials { get; set; }
    public DbSet<RealEstateObject> RealEstateObjects { get; set; }
    public DbSet<EvaluationCriterion> EvaluationCriteria { get; set; }
    public DbSet<Evaluation> Evaluations { get; set; }
    public DbSet<Realtor> Realtors { get; set; }
    public DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost; Database=corp2; Username=postgres; Password=livmas");
    }
}
