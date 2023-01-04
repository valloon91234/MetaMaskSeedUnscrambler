// See https://aka.ms/new-console-template for more information
using NBitcoin;
using Nethereum.Util;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities.Net;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

void parseJson()
{
    var bannedList = File.ReadAllText("list-banned.txt").Split("\r\n", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    var text = File.ReadAllText("text.json");
    var j1 = JObject.Parse(text);
    int count = j1.Count;
    int i = 0;

    var list = new HashSet<string>();
    foreach (var j2 in j1)
    {
        i++;
        try
        {
            var j3 = (JObject?)j2.Value;
            if (j3 == null) continue;

            var t = (string?)j3["text"];
            if (t == null) continue;

            t = t.Trim().Replace("\r\n", " ");
            t = Regex.Replace(t, @"[\d\-\.\:\,]", string.Empty);
            if (bannedList.Contains(t)) continue;
            var array = t.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (array.Length == 1) continue;
            if (array[0] == array[1]) continue;
            list.Add(t);
            Logger.WriteLine($"{i} / {count} \t {t}");
        }
        catch (Exception ex)
        {
            Logger.WriteLine(ex.Message, ConsoleColor.Red);
        }
    }

    using var streamWriter = new StreamWriter("text.txt", false);
    foreach (var t in list)
    {
        streamWriter.WriteLine(t);
    }
}

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

static string ByteArrayToHex(byte[] ba)
{
    StringBuilder hex = new(ba.Length * 2);
    foreach (byte b in ba)
        hex.AppendFormat("{0:x2}", b);
    return hex.ToString();
}

decimal checkE(string text)
{
    var addressList = new List<string>();
    Nethereum.HdWallet.Wallet wallet = new(text, "");
    var address = wallet.GetAddresses()[0];
    using var client = new HttpClient();
    var uri = new Uri($"https://api.etherscan.io/api?module=account&action=balance&address={address}&tag=latest&apikey=QP2KPGPF1AYM2F1Z465JII6N1ZQ92UZ56J");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    var jObject = JObject.Parse(responseText);
    var resultObj = jObject["result"];
    if (resultObj == null) return 0;
    string resultText = (string)resultObj!;
    if (resultText == "") return 0;
    return (decimal)(BigDecimal.Parse(resultText) / BigDecimal.Parse("1000000000000000000"));
}

decimal checkE2(string text)
{
    var addressList = new List<string>();
    {
        Nethereum.HdWallet.Wallet wallet = new(text, "");
        addressList.Add(wallet.GetAddresses()[0]);
    }
    {
        Nethereum.HdWallet.Wallet wallet = new(text, "", "");
        var privateKey = ByteArrayToHex(wallet.GetPrivateKey(0));
        Console.WriteLine(privateKey);
        addressList.Add(wallet.GetAddresses()[0]);
    }
    using var client = new HttpClient();
    var uri = new Uri($"https://api.etherscan.io/api?module=account&action=balancemulti&address={string.Join("|", addressList)}&tag=latest&apikey=QP2KPGPF1AYM2F1Z465JII6N1ZQ92UZ56J");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    var jObject = JObject.Parse(responseText);
    var resultObj = jObject["result"];
    if (resultObj == null) return 0;
    string resultText = (string)resultObj!;
    if (resultText == "") return 0;
    return (decimal)(BigDecimal.Parse(resultText) / BigDecimal.Parse("1000000000000000000"));
}

decimal checkBSC(string text)
{
    Nethereum.HdWallet.Wallet wallet = new(text, "");
    var addressList = wallet.GetAddresses();
    var address = addressList[0];
    using var client = new HttpClient();
    var uri = new Uri($"https://api.bscscan.com/api?module=account&action=balance&address={address}&apikey=8DDZ979J8I4S81EMAPBD1M7R2N5CZANDPS");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    var jObject = JObject.Parse(responseText);
    var resultObj = jObject["result"];
    if (resultObj == null) return 0;
    string resultText = (string)resultObj!;
    if (resultText == "") return 0;
    return (decimal)(BigDecimal.Parse(resultText) / BigDecimal.Parse("1000000000000000000"));
}

decimal checkPy(string text)
{
    Nethereum.HdWallet.Wallet wallet = new(text, "");
    var addressList = wallet.GetAddresses();
    var address = addressList[0];
    using var client = new HttpClient();
    var uri = new Uri($"https://api.polygonscan.com/api?module=account&action=balance&address={address}&apikey=57KJ6PYCBSA4VR64Q747FVF6YW4SS7MV3W");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    var jObject = JObject.Parse(responseText);
    var resultObj = jObject["result"];
    if (resultObj == null) return 0;
    string resultText = (string)resultObj!;
    if (resultText == "") return 0;
    return (decimal)(BigDecimal.Parse(resultText) / BigDecimal.Parse("1000000000000000000"));
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
    return (int)jObject["totalTransactionCount"];
}



bool checkT2(string text)
{
    HDWallet.Core.IHDWallet<HDWallet.Tron.TronWallet> hdWallet = new TronHDWallet2(text);
    var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);
    var address = wallet.Address;
    using var client = new HttpClient();
    var uri = new Uri($"https://apilist.tronscan.org/api/account?address={address}");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    var jObject = JObject.Parse(responseText);
    var transactionCountObject = jObject["totalTransactionCount"];
    if (transactionCountObject == null) return false;
    return (int)jObject["totalTransactionCount"] > 0;
}

int checkS(string text)
{
    Solnet.Wallet.Wallet wallet = new(text);
    var address = wallet.Account.PublicKey.Key;
    using var client = new HttpClient();
    var uri = new Uri($"https://public-api.solscan.io/account/transactions?account={address}");
    var response = client.GetAsync(uri).Result;
    var responseText = response.Content.ReadAsStringAsync().Result;
    return JArray.Parse(responseText).Count;
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
            var textEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes(text));
            logger.WriteLine($"\n{i + 1} / {count}\n{textEncoded}");
            try
            {
                var b = checkB(text);
                if (b > 0) logger.WriteLine($"B = {b}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.ToString(), ConsoleColor.Red);
            }
            try
            {
                var e = checkE(text);
                if (e > 0) logger.WriteLine($"E = {e}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.ToString(), ConsoleColor.Red);
            }
            try
            {
                var bsc = checkBSC(text);
                if (bsc > 0) logger.WriteLine($"BS = {bsc}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.ToString(), ConsoleColor.Red);
            }
            try
            {
                var py = checkPy(text);
                if (py > 0) logger.WriteLine($"Py = {py}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.ToString(), ConsoleColor.Red);
            }
            try
            {
                var t = checkT(text);
                if (t > 0) logger.WriteLine($"T = {t}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.ToString(), ConsoleColor.Red);
            }
            try
            {
                var s = checkS(text);
                if (s > 0) logger.WriteLine($"S = {s}", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                logger.WriteLine(ex.ToString(), ConsoleColor.Red);
            }
        }
        catch (Exception ex)
        {
            logger.WriteLine(ex.ToString(), ConsoleColor.Red);
        }
    }
}




parseJson();
//check();
//checkE2("flush index jaguar column among combine review rate cart canal immense diagram");
//checkT2("flush index jaguar column among combine review rate cart canal immense diagram");

return;


var text = File.ReadAllText("text.json");
var j1 = JObject.Parse(text);
int count = j1.Count;
int i = 0;
foreach (var j2 in j1)
{
    i++;
    try
    {
        var j3 = (JObject?)j2.Value;
        if (j3 == null) continue;
        var id = (string?)j3["id"];
        if (id != null)
        {
            if (id == "9DB3B5C5" || id == "2B935E48" || id == "107D327C") continue;
        }
        var t = (string?)j3["text"];
        if (t == null) continue;

        t = t.Trim();
        Logger.WriteLine($"{i} / {count} \t {id} {t}");

        Thread.Sleep(1000);
    }
    catch (Exception ex)
    {
        Logger.WriteLine(ex.Message, ConsoleColor.Red);
    }
}



Console.WriteLine("Press any key to exit...");
Console.ReadKey();
