namespace Service.Schedule;

public class RepeatingService : BackgroundService
{
    private readonly ILogger<RepeatingService> _logger;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMilliseconds(1000)); // 1s

    public RepeatingService(ILogger<RepeatingService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            // await DoWorkAsync();
            await CheckRoleUserVip();
        }
        
    }

    private static async Task DoWorkAsync()
    {
        Console.WriteLine(DateTime.Now.ToString("o"));
    }

    private static async Task CheckRoleUserVip()
    {
        if (DateTime.Now.Hour == 0)
        {
            // Job.CheckUserVip();
            await Task.Delay(3600000); // 1h
        }
    }
}
