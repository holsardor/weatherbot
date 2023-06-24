using Microsoft.Extensions.Caching.Distributed;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace weatherbot;
public partial class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger;
    private readonly WeatherService _weatherService;
    private readonly IDistributedCache _distributedCache;

    public UpdateHandler(ILogger<UpdateHandler> logger, WeatherService weatherService, IDistributedCache distributedCache)
    {
        _logger = logger;
        _weatherService = weatherService;
        _distributedCache = distributedCache;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error while Bot polling.");
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recived message {updateType}", update.Type);
        var handlerTask = update.Type switch
        {
            UpdateType.Message => HandleMessageUpdateAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => HandleEditMessageUpdateAsync(botClient, update.EditedMessage, cancellationToken),
            UpdateType.CallbackQuery => HandleCallbackQueryUpdateAsync(botClient, update.CallbackQuery, cancellationToken),
            
            _ => HandleUnknowUpdateAsync(botClient, update, cancellationToken)
        };

        try
        {
            await handlerTask;
        }
        catch (Exception ex)
        {
            await HandlePollingErrorAsync(botClient, ex, cancellationToken);
        }
    }

    public Task HandleUnknowUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recived unknown update {updateType}", update.Type);
        return Task.CompletedTask;
    }
}