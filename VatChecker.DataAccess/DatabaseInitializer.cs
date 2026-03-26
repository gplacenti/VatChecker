namespace VatChecker.DataAccess;

public static class DatabaseInitializer
{
    public static void Initialize(VatCheckerDbContext dbContext)
    {
        dbContext.Database.EnsureCreated();
    }
}
