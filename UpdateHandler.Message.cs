using Telegram.Bot;
using Telegram.Bot.Types;

namespace weatherbot;
public partial class UpdateHandler
{
    public Task HandleMessageUpdateAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        var userName = message.From?.Username ?? message.From.FirstName;
        _logger.LogInformation("Recived message from {username}", userName);
        return Task.CompletedTask;  
    }
}