using HDWallet.Core;
using HDWallet.Secp256k1;
using HDWallet.Tron;
using Newtonsoft.Json.Linq;

public class TronHDWallet2 : HDWallet<TronWallet>
{
    private static readonly CoinPath2 _path = new(PurposeNumber.BIP44, CoinType.Tron);

    public TronHDWallet2(string mnemonic, string passphrase = "")
        : base(mnemonic, passphrase, _path)
    {
    }
}

public class CoinPath2 : CoinPath
{
    private readonly string _path;
    public CoinPath2(PurposeNumber purpose, CoinType coinType) : base(purpose, coinType)
    {
        _path = $"m/44'/195'/0'";
    }

    public override string ToString()
    {
        return _path;
    }
}
