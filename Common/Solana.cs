using Newtonsoft.Json.Linq;
using Solnet.Rpc;

public class Solana
{

    private static IRpcClient RpcClient = ClientFactory.GetClient(Cluster.MainNet);
    //private static IStreamingRpcClient StreamingRpcClient = ClientFactory.GetStreamingClient(Cluster.MainNet);

    public static int GetTransactionCount(string address)
    {
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

    public static decimal GetBalance(string address)
    {
        var accountInfo = RpcClient.GetAccountInfo(address);
        //var tokenAccounts = RpcClient.GetTokenAccountsByOwner(address);
        return accountInfo.Result.Value?.Lamports / 1000000000m ?? 0;
    }

}
