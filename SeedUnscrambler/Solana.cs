using Newtonsoft.Json.Linq;

public class Solana
{
    public static int GetTransactionCount(string address)
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://public-api.solscan.io/account/transactions?account={address}");
        var response = client.GetAsync(uri).Result;
        var responseText = response.Content.ReadAsStringAsync().Result;
        return JArray.Parse(responseText).Count;
    }

}
