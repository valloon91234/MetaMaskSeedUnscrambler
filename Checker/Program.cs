// See https://aka.ms/new-console-template for more information
using NBitcoin;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net;

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
        List<string> addressList = new();
        Mnemonic mnemo = new(t, Wordlist.English);
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
        try
        {
            var obj = JObject.Parse(responseText);
            foreach (var pair in obj)
            {
                var w = (JObject)pair.Value;
                balanceList.Add((decimal)w["final_balance"]);
            }
            if (balanceList.Sum() > 0)
                Logger.WriteLine(string.Join(", ", balanceList), ConsoleColor.Green);
        }
        catch (Exception ex)
        {
            Logger.WriteLine(responseText, ConsoleColor.Red);
        }
        Thread.Sleep(1000);
    }
    catch (Exception ex)
    {
        Logger.WriteLine(ex.Message, ConsoleColor.Red);
    }
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();
