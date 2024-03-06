using System.Net;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

internal class TelegramClient
{
    static TelegramBotClient? Client;
    static User? Me { get; set; }
    static string[]? adminArray;
    static string[]? listenArray;
    static Logger? logger;

    public static void Init()
    {
        logger = new Logger($"{DateTime.UtcNow:yyyy-MM-dd}", "telegram_log");
        ServicePointManager.Expect100Continue = true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
        try
        {
            try
            {
                Client = new TelegramBotClient(DotNetEnv.Env.GetString("TELEGRAM_TOKEN"));
                Me = Client.GetMeAsync().Result;
            }
            catch (Exception ex)
            {
                logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}", ConsoleColor.Red, false);
                logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex}");
                logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  try again using proxy...");
                var proxy = new WebProxy
                {
                    Address = new Uri(DotNetEnv.Env.GetString("TELEGRAM_PROXY"))
                };
                //proxy.Credentials = new NetworkCredential(); //Used to set Proxy logins. 
                var handler = new HttpClientHandler
                {
                    Proxy = proxy
                };
                var httpClient = new HttpClient(handler);
                Client = new TelegramBotClient(DotNetEnv.Env.GetString("TELEGRAM_TOKEN"), httpClient);
                Me = Client.GetMeAsync().Result;
            }
            using var cts = new CancellationTokenSource();
            // StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
            };
            Client.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
            adminArray = DotNetEnv.Env.GetString("TELEGRAM_ADMIN")?.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            listenArray = DotNetEnv.Env.GetString("TELEGRAM_LISTEN")?.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
            logger.WriteLine($"Telegram connected: username = {Me.Username}");
            logger.WriteLine($"adminArray = {(adminArray == null ? "Null" : string.Join(",", adminArray))}");
        }
        catch (Exception ex)
        {
            logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}", ConsoleColor.Red, false);
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex}");
        }
    }

    static readonly Dictionary<string, string> LastCommand = new();

    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            long chatId;
            int messageId;
            string chatUsername;
            string senderUsername;
            string receivedMessageText;
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text && update.Message!.Chat.Type == ChatType.Private)
            {
                // Only process text messages
                chatId = update.Message.Chat.Id;
                messageId = update.Message.MessageId;
                chatUsername = update.Message.Chat.Username!;
                senderUsername = update.Message.From!.Username!;
                receivedMessageText = update.Message.Text!;
                Logger.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]  \"{receivedMessageText}\" from {senderUsername}. chatId = {chatId}, messageId = {messageId}", ConsoleColor.DarkGray);
            }
            else if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text && (update.Message!.Chat.Type == ChatType.Group || update.Message!.Chat.Type == ChatType.Supergroup))
            {
                chatId = update.Message.Chat.Id;
                messageId = update.Message.MessageId;
                chatUsername = update.Message.Chat.Username!;
                senderUsername = update.Message.From!.Username!;
                receivedMessageText = update.Message.Text!;
                Logger.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]  \"{receivedMessageText}\" from {senderUsername}. chatId = {chatId}, messageId = {messageId}", ConsoleColor.DarkGray);
                if (receivedMessageText[0] == '/' && receivedMessageText.EndsWith($"@{Me!.Username}"))
                {
                    var command = receivedMessageText[..^$"@{Me!.Username}".Length];
                    bool isAdmin = adminArray != null && adminArray.Contains(senderUsername!);
                    switch (command)
                    {
                        case $"/start":
                            if (isAdmin)
                            {
                                string replyMessageText = chatId.ToString();
                                await botClient.SendTextMessageAsync(chatId: chatId, text: replyMessageText, cancellationToken: cancellationToken);
                            }
                            break;
                        case $"/stop":
                            if (isAdmin)
                            {
                                string replyMessageText = chatId.ToString();
                                await botClient.SendTextMessageAsync(chatId: chatId, text: replyMessageText, cancellationToken: cancellationToken);
                            }
                            break;
                    }
                }

                return;
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                chatId = update.CallbackQuery!.Message!.Chat.Id;
                senderUsername = update.CallbackQuery.From.Username!;
                receivedMessageText = update.CallbackQuery.Data!;
                await botClient.AnswerCallbackQueryAsync(callbackQueryId: update.CallbackQuery!.Id, cancellationToken: cancellationToken);
            }
            else
                return;
            {
                bool isAdmin = adminArray != null && adminArray.Contains(senderUsername!);
                if (receivedMessageText[0] == '/')
                {
                    var command = receivedMessageText;
                    switch (command)
                    {
                        case "/start":
                            if (isAdmin)
                            {
                                string replyMessageText = chatId.ToString();
                                await botClient.SendTextMessageAsync(chatId: chatId, text: replyMessageText, cancellationToken: cancellationToken);
                            }
                            break;
                        case "/stop":
                            if (isAdmin)
                            {
                                string replyMessageText = chatId.ToString();
                                await botClient.SendTextMessageAsync(chatId: chatId, text: replyMessageText, cancellationToken: cancellationToken);
                            }
                            break;
                        default:
                            {
                                string replyMessageText = $"Unknown command: {command}";
                                await botClient.SendTextMessageAsync(chatId: chatId, text: replyMessageText, cancellationToken: cancellationToken);
                                logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  replied: \"{replyMessageText}\"", ConsoleColor.DarkGray);
                            }
                            LastCommand.Remove(senderUsername);
                            break;
                    }
                }
                else if (LastCommand.ContainsKey(senderUsername!))
                {
                    if (receivedMessageText == "exit" || receivedMessageText == "/exit")
                        LastCommand.Remove(senderUsername!);
                    else
                        switch (LastCommand[senderUsername!])
                        {
                            default:
                                {
                                    logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  Unknown error", ConsoleColor.Red);
                                }
                                break;
                        }
                }
            }
        }
        catch (Exception ex)
        {
            logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}", ConsoleColor.Red, false);
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex}");
        }
    }

    static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        if (logger != null) logger.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    public static void SendMessageToListenGroup(string text, ParseMode? parseMode = default, IReplyMarkup? replyMarkup = default)
    {
        if (Client == null || listenArray == null) return;
        try
        {
            int count = 0;
            foreach (var chat in listenArray)
            {
                if (string.IsNullOrWhiteSpace(chat)) continue;
                var result = Client.SendTextMessageAsync(chatId: chat, text: text, disableWebPagePreview: true, parseMode: parseMode, replyMarkup: replyMarkup).Result;
                count++;
            }
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  Message sent to {count} chats: {text}");
        }
        catch (Exception ex)
        {
            logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}", ConsoleColor.Red, false);
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex}");
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {text}");
        }
    }

    public static void SendPhotoToListenGroup(string filename, string? caption = null, ParseMode? parseMode = default, IReplyMarkup? replyMarkup = default)
    {
        if (Client == null || listenArray == null) return;
        try
        {
            int count = 0;
            using var fileStream = System.IO.File.OpenRead(filename);
            var inputFileStream = InputFile.FromStream(fileStream, Path.GetFileName(filename));
            foreach (var chat in listenArray)
            {
                if (string.IsNullOrWhiteSpace(chat)) continue;
                var result = Client.SendPhotoAsync(chatId: chat, photo: inputFileStream, caption: caption, parseMode: parseMode, replyMarkup: replyMarkup).Result;
                count++;
            }
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  Photo sent to {count} caption: {caption}");
        }
        catch (Exception ex)
        {
            logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}", ConsoleColor.Red, false);
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex}");
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {caption}");
        }
    }

    public static void SendPhotosToListenGroup(IEnumerable<string> filenameList)
    {
        if (Client == null || listenArray == null) return;
        try
        {
            int count = 0;
            var albumInputMediaList = new List<IAlbumInputMedia>();
            var streamList = new List<Stream>();
            foreach (var filename in filenameList)
            {
                var fileStream = System.IO.File.OpenRead(filename);
                streamList.Add(fileStream);
                var inputFileStream = InputFile.FromStream(fileStream, Path.GetFileName(filename));
                albumInputMediaList.Add(new InputMediaPhoto(inputFileStream));
            }
            foreach (var chat in listenArray)
            {
                if (string.IsNullOrWhiteSpace(chat)) continue;
                var result = Client.SendMediaGroupAsync(chatId: chat, media: albumInputMediaList, disableNotification: true).Result;
                count++;
            }
            foreach (var stream in streamList)
            {
                stream.Dispose();
            }
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  Photos sent to {count}");
        }
        catch (Exception ex)
        {
            logger!.WriteLine($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex.Message}\n{ex.InnerException?.Message}\n{ex.InnerException?.InnerException?.Message}", ConsoleColor.Red, false);
            logger!.WriteFile($"[{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}]  <ERROR>  {ex}");
        }
    }

}
