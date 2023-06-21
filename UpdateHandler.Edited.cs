using Telegram.Bot;
using Telegram.Bot.Types;

namespace weatherbot;
public partial class UpdateHandler
{
    public Task HandleEditMessageUpdateAsync(ITelegramBotClient botClient, Message editedMessage, CancellationToken cancellationToken = default)
    {
        var userName = editedMessage.From?.Username ?? editedMessage.From.FirstName;

        _logger.LogInformation("Recived message from {username}", userName);
        
        return Task.CompletedTask; 
    }
}