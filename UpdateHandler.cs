using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace weatherbot;
public partial class UpdateHandler : IUpdateHandler
{
    private readonly ILogger<UpdateHandler> _logger;

    public UpdateHandler(ILogger<UpdateHandler> logger)
    {
        _logger = logger;
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Nimadir xotolik yuz berdi xatolikka qarang");
        return Task.CompletedTask;
    }

    public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recived message {updateType}", update.Type);
        var handlerTask = update.Type switch
        {
            UpdateType.Message => HandleMessageUpdateAsync(botClient, update.Message, cancellationToken),
            UpdateType.EditedMessage => HandleEditMessageUpdateAsync(botClient, update.EditedMessage, cancellationToken),
            _ => HandleUnknowUpdateAsync(botClient, update, cancellationToken)
        };

        try
        {
            await handlerTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while handling {updateType} update", update.Type);
            throw;
        }
    }

    public Task HandleUnknowUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recived unknown update {updateType}", update.Type);
        return Task.CompletedTask;
    }
}