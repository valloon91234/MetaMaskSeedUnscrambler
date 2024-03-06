// See https://aka.ms/new-console-template for more information

using Telegram.Bot.Types.Enums;

//AppHelper.QuickEditMode(false);
//Console.BufferHeight = Int16.MaxValue - 1;
//AppHelper.MoveWindow(AppHelper.GetConsoleWindow(), 24, 0, 1080, 280, true);
AppHelper.FixCulture();

// See https://github.com/tonerdo/dotnet-env
DotNetEnv.Env.Load("config.env");
TelegramClient.Init();

int round = 0;
var lastTimeDic = new Dictionary<string, DateTime?>();
while (true)
{
    Logger.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]  Round = {++round}");
    Console.Title = $"{round}";
    var ethplorerApiKey = DotNetEnv.Env.GetString("ETHPLORER_API_KEY");
    var text = File.ReadAllText("_data.txt");
    var textLines = text.Split("\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    foreach (var line in textLines)
    {
        if (line[0] == '#') continue;
        var words = line.Split('\t', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        if (words.Length < 4)
        {
            Logger.WriteLine($"Invalid line: {line}", ConsoleColor.DarkYellow);
            continue;
        }
        try
        {
            var intervalMinutes = int.Parse(words[0]);
            var address = words[3];
            if (!lastTimeDic.ContainsKey(address) || (DateTime.Now - lastTimeDic[address]!.Value).TotalMinutes >= intervalMinutes)
            {
                var requireBalance = decimal.Parse(words[1]);
                var type = words[2].ToUpper();
                var id = address[^12..^4];
                decimal balance = 0;
                int sleep = 0;
                if (type == "BTC")
                {
                    balance = BalanceChecker.GetBtcBalance(address.Split('|', ','));
                    sleep = 1;
                }
                else if (type == "ERC")
                {
                    balance = BalanceChecker.GetErcBscBalance(address, out _, out _, ethplorerApiKey);
                    sleep = 1;
                }
                else if (type == "TRC")
                {
                    balance = BalanceChecker.GetTrxTotalAssetInUsd(address);
                    sleep = 1;
                }
                else
                {
                    Logger.WriteLine($"Invalid type: {type}", ConsoleColor.DarkYellow);
                    continue;
                }
                lastTimeDic[address] = DateTime.Now;
                if (balance > requireBalance)
                {
                    TelegramClient.SendMessageToListenGroup($"{id}\t{balance}", ParseMode.Html);
                    Logger.WriteWait($"{id}\t{balance}  ", sleep, 1, ConsoleColor.Green);
                }
                else if (balance < 1)
                {
                    Logger.WriteWait($"{id}\t{balance}  ", sleep, 1, ConsoleColor.DarkGray);
                }
                else
                {
                    Logger.WriteWait($"{id}\t{balance}  ", sleep);
                }
            }
        }
        catch (HttpRequestException ex)
        {
            Logger.WriteWait($"Failed on line: {line}\n{ex}  ", 15, 1, ConsoleColor.Red);
        }
        catch (Exception ex)
        {
            Logger.WriteLine($"Failed on line: {line}\n{ex}", ConsoleColor.Red);
        }
    }
    Logger.WriteWait("> Waiting for next round.", 60);
}
