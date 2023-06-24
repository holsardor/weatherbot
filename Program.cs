using Telegram.Bot;
using Telegram.Bot.Polling;
using weatherbot;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.AddTransient<IUpdateHandler, UpdateHandler>();
builder.Services.AddTransient<WeatherService>();

builder.Services.AddSingleton<ITelegramBotClient>(
    new TelegramBotClient(builder.Configuration.GetValue("BotApiKey", string.Empty)));
builder.Services.AddHttpClient("OpenMeteoClient", client => 
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue("OpenMeteoBaseUrl", string.Empty));
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
});

builder.Build().Run();