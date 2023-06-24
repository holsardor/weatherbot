using Telegram.Bot;
using Telegram.Bot.Types;

namespace weatherbot;

public partial class UpdateHandler
{
    private readonly string[] availableLanguages = { "uz", "ru", "en" };
    private async Task HandleCallbackQueryUpdateAsync(ITelegramBotClient botClient, CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        if (availableLanguages.Contains(callbackQuery.Data))
            await PersistUserLanguageAsync(botClient, callbackQuery.From.Id, callbackQuery.Data, cancellationToken);

        await RemoveMessageAsync(botClient, callbackQuery.Message.Chat.Id, callbackQuery.Message.MessageId, cancellationToken: cancellationToken);
    }

    private async Task PersistUserLanguageAsync(ITelegramBotClient botClient, long userId, string data, CancellationToken cancellationToken)
    {

        var languageCacheKey = $"Language:{userId}";
        var selectedQuery = data;

        var value = await _distributedCache.GetOrCreateAsync(
            key: languageCacheKey,
            callback: () => data,
            cancellationToken: cancellationToken);
    }
}