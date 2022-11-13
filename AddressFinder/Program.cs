// See https://aka.ms/new-console-template for more information
using NBitcoin;
using Nethereum.Hex.HexConvertors.Extensions;
using Org.BouncyCastle.Crypto.Paddings;
using System.Data.SQLite;
using System.Numerics;
using System.Text;

static byte[] StringToByteArray(string hex)
{
    return Enumerable.Range(0, hex.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                     .ToArray();
}

static string ByteArrayToString(byte[] ba)
{
    StringBuilder hex = new StringBuilder(ba.Length * 2);
    foreach (byte b in ba)
        hex.AppendFormat("{0:x2}", b);
    return hex.ToString();
}

static void IncrementBytes(ref byte[] bytes)
{
    int index = bytes.Length - 1;
    while (index >= 0)
    {
        if (bytes[index] < 255)
        {
            bytes[index]++;
            break;
        }
        else
        {
            bytes[index--] = 0;
        }
    }
}

static (string, string, string) GetB1(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var wif = key.GetWif(Network.Main).ToWif();
    var add_3 = key.GetAddress(ScriptPubKeyType.Legacy, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^3..];
    return (id, address, wif);
}

static (string, string, string) GetB3(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var wif = key.GetWif(Network.Main).ToWif();
    var add_3 = key.GetAddress(ScriptPubKeyType.SegwitP2SH, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^3..];
    return (id, address, wif);
}

static (string, string, string) GetBC(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var wif = key.GetWif(Network.Main).ToWif();
    var add_3 = key.GetAddress(ScriptPubKeyType.Segwit, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^3..];
    return (id, address, wif);
}

static (string, string, string) GetBc1q(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var wif = key.GetWif(Network.Main).ToWif();
    var add_3 = key.GetAddress(ScriptPubKeyType.Segwit, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^4..];
    return (id, address, wif);
}

static (string, string, string) GetBc1p(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var wif = key.GetWif(Network.Main).ToWif();
    var add_3 = key.GetAddress(ScriptPubKeyType.TaprootBIP86, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^4..];
    return (id, address, wif);
}

static (string, string) GetErc(byte[] privateKey)
{
    string address = Nethereum.Web3.Web3.GetAddressFromPrivateKey(ByteArrayToString(privateKey));
    string id = address[^4..];
    return (id, address);
}

static (string, string) GetErc5(byte[] privateKey)
{
    string address = Nethereum.Web3.Web3.GetAddressFromPrivateKey(ByteArrayToString(privateKey));
    string id = $"{address[2..3]}{address[^4..]}";
    return (id, address);
}

static (string, string) GetTrc(byte[] privateKey)
{
    HDWallet.Tron.TronWallet wallet = new(ByteArrayToString(privateKey));
    string address = wallet.Address;
    string id = address[^3..];
    return (id, address);
}

static (string, string) GetSol(byte[] privateKey)
{
    int pkLength = privateKey.Length;
    byte[] newBytes = new byte[pkLength * 2];
    Array.Copy(privateKey, newBytes, pkLength);
    Array.Copy(privateKey, 0, newBytes, pkLength, pkLength);
    Solnet.Wallet.Wallet wallet = new(newBytes, "", Solnet.Wallet.SeedMode.Bip39);
    var address = wallet.Account.PublicKey.Key;
    string id = $"{address[..1]}{address[^2..]}";
    return (id, address);
}

const string START_KEY = "625c5fb2d4D168770174C4E85553c23F4f452cef000000000000000000000000";
byte[] keyBytes = StringToByteArray(START_KEY);
string type = "sol";
//long insertCount = Dao.CountAll(type);
//string? maxKey = Dao.SelectMaxKey(type);
//if (maxKey == null)
//{
//    keyBytes = StringToByteArray(START_KEY);
//}
//else
//{
//    keyBytes = StringToByteArray(maxKey);
//    IncrementBytes(ref keyBytes);
//}

//long loopCount = insertCount;
long loopCount = 0, insertCount = 0;

string filename = $"{type}.csv";
File.Delete(filename);
using var writer = new StreamWriter(filename, false, Encoding.UTF8);
//writer.WriteLine($"id,address,key,wif");
writer.WriteLine($"id,address,key");
var idList = new List<string>();
while (true)
{
    loopCount++;
    try
    {

        //(string id, string address, string wif) = GetB1(keyBytes);
        //(string id, string address, string wif) = GetB3(keyBytes);
        //(string id, string address, string wif) = GetBC(keyBytes);
        //(string id, string address, string wif) = GetBc1q(keyBytes);
        //(string id, string address, string wif) = GetBc1p(keyBytes);
        //(string id, string address) = GetErc(keyBytes);
        //(string id, string address) = GetErc5(keyBytes);
        //(string id, string address) = GetTrc(keyBytes);
        (string id, string address) = GetSol(keyBytes);

        int binarySearchIndex = idList.BinarySearch(id);
        if (binarySearchIndex < 0)
        {
            string pkText = ByteArrayToString(keyBytes);

            //Dao.Insert(type, id, address, pkText, wif);
            //writer.WriteLine($"{id},{address},{pkText},{wif}");
            writer.WriteLine($"{id},{address},{pkText}");
            writer.Flush();
            idList.Insert(~binarySearchIndex, id);
            insertCount++;
        }

        if (loopCount % 1000 == 0)
        {
            Console.WriteLine($"{insertCount} / {loopCount} \t {id}");
            Console.Title = $"{insertCount} / {loopCount}";
        }
    }
    catch (Exception ex)
    {
        if (ex is SQLiteException sqliteException && sqliteException.ErrorCode == 19)
        {
        }
        else
        {
            Console.WriteLine(ex.ToString());
        }
        if (loopCount % 100000 == 0)
            Console.WriteLine($"{insertCount} / {loopCount}");
    }
    IncrementBytes(ref keyBytes);
}


