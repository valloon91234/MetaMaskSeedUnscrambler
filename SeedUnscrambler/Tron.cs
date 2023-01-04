using Newtonsoft.Json.Linq;

public class Tron
{
    public static int GetTransactionCount(string address)
    {
        using var client = new HttpClient();
        var uri = new Uri($"https://apilist.tronscan.org/api/account?address={address}");
        var response = client.GetAsync(uri).Result;
        var responseText = response.Content.ReadAsStringAsync().Result;
        var jObject = JObject.Parse(responseText);
        var transactionCountObject = jObject["totalTransactionCount"];
        if (transactionCountObject == null) return 0;
        return (int)jObject["totalTransactionCount"];
    }

}
