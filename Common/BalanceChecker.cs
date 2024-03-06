using Newtonsoft.Json.Linq;
using System.Collections;

public class BalanceChecker
{

    public static decimal GetBtcBalance(string[] addressList)
    {
        List<decimal> balanceList = new();
        using var client = new HttpClient();
        var uri = new Uri($"https://blockchain.info/balance?active={string.Join("|", addressList)}");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        var obj = JObject.Parse(responseText);
        foreach (var pair in obj)
        {
            var balance = (decimal)pair.Value!["final_balance"]!;
            balanceList.Add(balance);
        }
        return balanceList.Sum();
    }

    public static decimal GetErcBscBalance(string address, out decimal ercBalance, out decimal bscBalance, string apiKey = "freekey")
    {
        ercBalance = GetErcBalance(address, out _, out _, apiKey);
        bscBalance = GetBscBalance(address, out _, out _, apiKey);
        return ercBalance + bscBalance;
    }

    public static decimal GetErcBalance(string address, out decimal ethBalance, out int transactionCount, string? apiKey = "freekey")
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://api.ethplorer.io/getAddressInfo/{address}?apiKey={apiKey}&showTxsCount=true");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(responseText);
        var price = (decimal)jObject["ETH"]!["price"]!["rate"]!;
        ethBalance = (decimal)jObject["ETH"]!["balance"]!;
        transactionCount = (int)jObject["countTxs"]!;
        var totalBalance = price * ethBalance;
        var tokens = jObject["tokens"]?.ToArray();
        if (tokens != null)
            foreach (var token in tokens)
            {
                try
                {
                    if (token["tokenInfo"]?["price"]?["rate"] == null) continue;
                }
                catch { continue; }
                var tokenBalance = (double)token["balance"]!;
                var decimals = (int)token["tokenInfo"]!["decimals"]!;
                var rate = (double)token["tokenInfo"]!["price"]!["rate"]!;
                totalBalance += (decimal)(tokenBalance / Math.Pow(10, decimals) * rate);
            }
        return totalBalance;
    }

    public static readonly string[] BSC_DISCARD_TOKEN = new string[] { "0xd22202d23fe7de9e3dbe11a2a88f42f4cb9507cf" };

    public static decimal GetBscBalance(string address, out decimal bnbBalance, out int transactionCount, string? apiKey = "freekey")
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://api.binplorer.com/getAddressInfo/{address}?apiKey={apiKey}&showTxsCount=true");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(responseText);
        var price = (decimal)jObject["ETH"]!["price"]!["rate"]!;
        bnbBalance = (decimal)jObject["ETH"]!["balance"]!;
        transactionCount = (int)jObject["countTxs"]!;
        var totalBalance = price * bnbBalance;
        var tokens = jObject["tokens"]?.ToArray();
        if (tokens != null)
            foreach (var token in tokens)
            {
                try
                {
                    if (token["tokenInfo"]?["price"]?["rate"] == null) continue;
                }
                catch { continue; }
                var tokenAddress = (string)token["tokenInfo"]!["address"]!;
                if (BSC_DISCARD_TOKEN.Any(s => s == tokenAddress)) continue;
                var tokenBalance = (double)token["balance"]!;
                var decimals = (int)token["tokenInfo"]!["decimals"]!;
                var rate = (double)token["tokenInfo"]!["price"]!["rate"]!;
                totalBalance += (decimal)(tokenBalance / Math.Pow(10, decimals) * rate);
                if (totalBalance > 100)
                    Console.WriteLine(totalBalance);
            }
        return totalBalance;
    }


    public static int GetTrxTransactionCount(string address)
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://apilist.tronscan.org/api/account?address={address}");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(responseText);
        var transactionCountObject = jObject["totalTransactionCount"];
        if (transactionCountObject == null) return 0;
        return (int)transactionCountObject;
    }

    public static decimal GetTrxTotalAssetInUsd(string address)
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://apilist.tronscanapi.com/api/account/token_asset_overview?address={address}");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(responseText);
        var transactionCountObject = jObject["totalAssetInUsd"];
        if (transactionCountObject == null) return 0;
        return (decimal)transactionCountObject;
    }

}
