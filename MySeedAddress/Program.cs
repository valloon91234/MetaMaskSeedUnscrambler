// See https://aka.ms/new-console-template for more information


using Org.BouncyCastle.Security;

int index = 0;
int found = 0;
Logger logger = new($"{DateTime.Now:yyyyMMdd-HHmmss}.txt");
SecureRandom random = new();
while (true)
{
    index++;
    //var words = new Mnemonic(null, WordCount.Twelve);
    //var mnemonic = words.ToString();
    //Console.WriteLine($"{mnemonic} \t {words.IsValidChecksum}");

    //Nethereum.HdWallet.Wallet wallet = new(mnemonic, "");
    //var address = wallet.GetAddresses()[0];

    //HDWallet.Core.IHDWallet<HDWallet.Tron.TronWallet> hdWallet = new HDWallet.Tron.TronHDWallet(mnemonic);
    //var address = hdWallet.GetAccount(0).GetExternalWallet(0).Address;

    //Solnet.Wallet.Wallet wallet = new(mnemonic);
    //var address = wallet.Account.PublicKey.Key;

    byte[] pkBytes = new byte[32];
    random.NextBytes(pkBytes);
    var privateKey = HexUtil.ByteArrayToHexViaLookup32Unsafe(pkBytes);
    var address = Nethereum.Web3.Web3.GetAddressFromPrivateKey(privateKey);
    if (address.EndsWith("Afc") && address[2] == '8')
    {
        Logger.WriteLine($"\r{index} \t {privateKey[^4..]}", ConsoleColor.Green);
        logger.WriteFile($"{address}\t\t{privateKey}");
        found++;
    }
    if (index % 100 == 0)
    {
        Console.Write($"\r{index}");
        Console.Title = $"{found} / {index}";
    }
}


Console.WriteLine("Press any key to exit...");
Console.ReadKey();
