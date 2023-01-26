// See https://aka.ms/new-console-template for more information



using NBitcoin;
using System.Net.Sockets;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

int index = 0;
Logger logger = new($"{DateTime.Now:yyyyMMdd-HHmmss}.txt");
while (true)
{
    index++;
    var words = new Mnemonic(null, WordCount.Twelve);
    var mnemonic = words.ToString();
    //Console.WriteLine($"{mnemonic} \t {words.IsValidChecksum}");

    //HDWallet.Core.IHDWallet<HDWallet.Tron.TronWallet> hdWallet = new HDWallet.Tron.TronHDWallet(mnemonic);
    //var address = hdWallet.GetAccount(0).GetExternalWallet(0).Address;

    //Nethereum.HdWallet.Wallet wallet = new(mnemonic, "");
    //var address = wallet.GetAddresses()[0];

    Solnet.Wallet.Wallet wallet = new(mnemonic);
    var address = wallet.Account.PublicKey.Key;
    if (address.EndsWith("427"))
    {
        var text1 = Convert.ToBase64String(Encoding.UTF8.GetBytes(mnemonic));
        Logger.WriteLine($"\r{index} \t {address} \t {text1}", ConsoleColor.Green);
        logger.WriteFile($"{address}\t\t{text1}");
        Console.Title = address[^4..];
    }
    if (index % 100 == 0)
        Console.Write($"\r{index} \t {address}");
}


Console.WriteLine("Press any key to exit...");
Console.ReadKey();
