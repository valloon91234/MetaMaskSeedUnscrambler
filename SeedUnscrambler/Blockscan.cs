using System.Net;
using System.Text.RegularExpressions;

public class Blockscan
{
    public static int GetCount(string address)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");
        var uri = new Uri($"https://blockscan.com/address/{address}");
        var response = client.GetAsync(uri).Result;
        response.EnsureSuccessStatusCode();
        var responseText = response.Content.ReadAsStringAsync().Result;
        return Regex.Matches(responseText, "search-result-list").Count;
    }
}
