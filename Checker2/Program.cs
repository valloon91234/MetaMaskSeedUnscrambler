// See https://aka.ms/new-console-template for more information
using NBitcoin;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

decimal checkB(string text)
{
    List<string> addressList = new();
    Mnemonic mnemo = new(text, Wordlist.English);
    {
        var hdroot = mnemo.DeriveExtKey();
        var key = hdroot.Derive(new KeyPath("m/84'/0'/0'/0/0")).PrivateKey;
        addressList.Add(key.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString());
        addressList.Add(key.GetAddress(ScriptPubKeyType.SegwitP2SH, Network.Main).ToString());
        addressList.Add(key.GetAddress(ScriptPubKeyType.Segwit, Network.Main).ToString());
        addressList.Add(key.GetAddress(ScriptPubKeyType.TaprootBIP86, Network.Main).ToString());
    }
    {
        var hdroot = mnemo.DeriveExtKey();
        var key = hdroot.Derive(new KeyPath("m/44'/0'/0'/0/0")).PrivateKey;
        addressList.Add(key.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString());
        addressList.Add(key.GetAddress(ScriptPubKeyType.SegwitP2SH, Network.Main).ToString());
        addressList.Add(key.GetAddress(ScriptPubKeyType.Segwit, Network.Main).ToString());
        addressList.Add(key.GetAddress(ScriptPubKeyType.TaprootBIP86, Network.Main).ToString());
    }
    List<decimal> balanceList = new();
    //var proxy = new WebProxy
    //{
    //    Address = new Uri("socks5://54.249.108.164:8443")
    //};
    ////proxy.Credentials = new NetworkCredential(); //Used to set Proxy logins. 
    //var handler = new HttpClientHandler
    //{
    //    Proxy = proxy
    //};
    //using var client = new HttpClient(handler);
    using var client = new HttpClient();
    var uri = new Uri($"https://blockchain.info/balance?active={string.Join("|", addressList)}");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    var obj = JObject.Parse(responseText);
    foreach (var pair in obj)
    {
        var w = (JObject)pair.Value;
        balanceList.Add((decimal)w["final_balance"]);
    }
    return balanceList.Sum();
}

int checkBlock(string text)
{
    Nethereum.HdWallet.Wallet wallet = new(text, "");
    var address = wallet.GetAddresses()[0];
    return Blockscan.GetCount(address);
}

int checkT(string text)
{
    HDWallet.Core.IHDWallet<HDWallet.Tron.TronWallet> hdWallet = new HDWallet.Tron.TronHDWallet(text);
    var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);
    var address = wallet.Address;
    using var client = new HttpClient();
    var uri = new Uri($"https://apilist.tronscan.org/api/account?address={address}");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    var jObject = JObject.Parse(responseText);
    var transactionCountObject = jObject["totalTransactionCount"];
    if (transactionCountObject == null) return 0;
    return (int)jObject["totalTransactionCount"]!;
}

int checkS(string text)
{
    Solnet.Wallet.Wallet wallet = new(text);
    var address = wallet.Account.PublicKey.Key;
    using var client = new HttpClient();
    var uri = new Uri($"https://public-api.solscan.io/account/transactions?account={address}");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    try
    {
        return JArray.Parse(responseText).Count;
    }
    catch
    {
        throw new Exception(responseText);
    }
}

void check()
{
    Logger logger = new($"{DateTime.Now:yyyyMMdd-HHmmss}.txt");
    var list = File.ReadAllText("text.txt").Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var count = list.Length;
    for (int i = 0; i < count; i++)
    {
        try
        {
            var text = list[i].ToLower();
            if (text.StartsWith("#")) continue;
            var textEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            logger.WriteLine($"\n{i + 1} / {count}\n{textEncoded}");
            try
            {
                var b = checkB(text);
                if (b > 0) logger.WriteLine($"Bc = {b}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.Message, ConsoleColor.Red);
            }
            try
            {
                var e = checkBlock(text);
                if (e > 0) logger.WriteLine($"Block = {e}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.Message, ConsoleColor.Red);
            }
            try
            {
                var t = checkT(text);
                if (t > 0) logger.WriteLine($"T = {t}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.Message, ConsoleColor.Red);
            }
            try
            {
                var s = checkS(text);
                if (s > 0) logger.WriteLine($"S = {s}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.Message, ConsoleColor.Red);
            }
        }
        catch (Exception ex)
        {
            logger.WriteLine(ex.ToString(), ConsoleColor.Red);
        }
    }
}

check();

Console.WriteLine();
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();
Console.ReadKey();
Console.ReadKey();
Console.ReadKey();
Console.ReadKey();
Console.ReadKey();
