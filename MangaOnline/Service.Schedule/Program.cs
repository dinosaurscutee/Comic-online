using Service.Schedule;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<RepeatingService>();

var app = builder.Build();

app.MapGet("nice", () => $"The answer is: {Math.Sqrt(4761)}");

app.Run();