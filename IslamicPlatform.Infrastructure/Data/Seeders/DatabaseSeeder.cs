using IslamicPlatform.Infrastructure.Data;
using IslamicPlatform.Infrastructure.Data.Seed;
using IslamicPlatform.Infrastructure.Data.Seeders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public class DatabaseSeeder
{
    private readonly ApplicationDbContext _context;
    private readonly HttpClient _httpClient;
    private readonly ILogger<DatabaseSeeder> _logger;
    private readonly IConfiguration _config;

    public DatabaseSeeder(
        ApplicationDbContext context,
        HttpClient httpClient,
        ILogger<DatabaseSeeder> logger,
        IConfiguration config)
    {
        _context = context;
        _httpClient = httpClient;
        _logger = logger;
        _config = config;
    }

    public async Task SeedAsync()
    {
        await new QuranSeeder(_context, _httpClient, _logger).SeedAsync();
        await new SheikhSeeder(_context,  _logger).SeedAsync();
        await new HadithSeeder(_context, _logger).SeedAsync(); 
        await new AzkarSeeder(_context, _logger).SeedAsync();
    }
}