using Telegram.Bot;
using Telegram.Bot.Types;

namespace weatherbot;
public partial class UpdateHandler
{
    public Task HandleEditMessageUpdateAsync(ITelegramBotClient botClient, Message editedMessage, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Recived edited message from {userId} New Text: {newText}",
        editedMessage.Chat.Id,
        editedMessage.Text);

        return Task.CompletedTask;
    }
}