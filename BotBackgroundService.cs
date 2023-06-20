using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace weatherbot;
public class BotBackgroundService : BackgroundService
{
    private readonly ILogger<BotBackgroundService> _logger;
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly IUpdateHandler _updateHandler;

    public BotBackgroundService(
        ILogger<BotBackgroundService> logger,
        ITelegramBotClient telegramBotClient,
        IUpdateHandler updateHandler)
    {
        _logger = logger;
        _telegramBotClient = telegramBotClient;
        _updateHandler = updateHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var me = await _telegramBotClient.GetMeAsync(stoppingToken);
        _logger.LogInformation("Telegram bot {userName} id {id} started at {time}", me.Username, me.Id, DateTime.UtcNow);

        _telegramBotClient.StartReceiving(
        updateHandler: _updateHandler,
        receiverOptions: new ReceiverOptions()
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message, UpdateType.EditedMessage
            },
            ThrowPendingUpdates = true // bo't o'chiq bo'lgan paytidagi kelgan messagelarga etibor bermaydi
        },
        cancellationToken: stoppingToken);
    }
}