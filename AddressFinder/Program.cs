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

static (string, string, string) GetB3(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var add_3 = key.GetAddress(ScriptPubKeyType.SegwitP2SH, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^3..];
    return ("b3", id, address);
}

static (string, string, string) GetBC(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var add_3 = key.GetAddress(ScriptPubKeyType.Segwit, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^3..];
    return ("bc", id, address);
}

static (string, string, string) GetBC4(byte[] privateKey)
{
    var key = new Key(privateKey); // private key
    var add_3 = key.GetAddress(ScriptPubKeyType.Segwit, Network.Main); // Segwit P2SH address
    string address = add_3.ToString();
    string id = address[^4..];
    return ("bc4", id, address);
}

static (string, string, string) GetErc(byte[] privateKey)
{
    string address = Nethereum.Web3.Web3.GetAddressFromPrivateKey(ByteArrayToString(privateKey));
    string id = address[^4..];
    return ("erc", id, address);
}

static (string, string, string) GetTrc(byte[] privateKey)
{
    HDWallet.Tron.TronWallet wallet = new(ByteArrayToString(privateKey));
    string address = wallet.Address;
    string id = address[^3..];
    return ("trc", id, address);
}

const string START_KEY = "625c5fb2d4D168770174C4E85553c23F4f452cef000000000000000000000000";
byte[] keyBytes = StringToByteArray(START_KEY);
long insertCount = 0, loopCount = 0;
while (true)
{
    loopCount++;
    try
    {
        //(string type, string id, string address) = GetB3(keyBytes);
        //(string type, string id, string address) = GetBC(keyBytes);
        (string type, string id, string address) = GetBC4(keyBytes);
        //(string type, string id, string address) = GetErc(keyBytes);
        //(string type, string id, string address) = GetTrc(keyBytes);
        string pkText = ByteArrayToString(keyBytes);
        Dao.Insert(type, id, address, pkText);
        insertCount++;
        Console.WriteLine($"{insertCount} / {loopCount} \t {id}");
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


