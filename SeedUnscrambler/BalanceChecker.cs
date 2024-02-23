using Newtonsoft.Json.Linq;

public class BalanceChecker
{

    public static decimal GetErcBalance(string address, out decimal balance, out int transactionCount, out int tokenCount)
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://api.ethplorer.io/getAddressInfo/{address}?apiKey=freekey&showTxsCount=true");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(responseText);
        var price = (decimal)jObject["ETH"]!["price"]!["rate"]!;
        balance = (decimal)jObject["ETH"]!["balance"]!;
        transactionCount = (int)jObject["countTxs"]!;
        tokenCount = jObject["tokens"]?.ToArray().Length ?? 0;
        return price * balance;
    }

    public static decimal GetBscBalance(string address, out decimal balance, out int transactionCount, out int tokenCount)
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://api.binplorer.com/getAddressInfo/{address}?apiKey=freekey&showTxsCount=true");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(responseText);
        var price = (decimal)jObject["ETH"]!["price"]!["rate"]!;
        balance = (decimal)jObject["ETH"]!["balance"]!;
        transactionCount = (int)jObject["countTxs"]!;
        tokenCount = jObject["tokens"]?.ToArray().Length ?? 0;
        return price * balance;
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
