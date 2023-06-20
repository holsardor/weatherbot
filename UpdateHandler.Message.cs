using Telegram.Bot;
using Telegram.Bot.Types;

namespace weatherbot;
public partial class UpdateHandler
{
    public Task HandleMessageUpdateAsync(ITelegramBotClient botClient, Message message, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Recived message from {userId}: {text}", message.Chat.Id, message.Text);
        return Task.CompletedTask;  
    }
}