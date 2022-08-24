using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace SeedUnscrambler
{
    public partial class Form1 : Form
    {
        static string[]? bip32Words;

        public Form1()
        {
            InitializeComponent();
            using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("SeedUnscrambler.BIP39Words.txt");
            using StreamReader reader = new(resource!);
            string result = reader.ReadToEnd();
            bip32Words = result.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            this.Text += $" ({bip32Words.Length})";
        }

        private static bool VerifyAllExist(params string[] words) => ValidPhraseLength(words) && words.All(bip32Words!.Contains);
        private static bool ValidPhraseLength(string[] words) => words.Length is 12 or 15 or 18 or 21 or 24 or 25;

        private void textBox_Seed_TextChanged(object sender, EventArgs e)
        {
            var text = ((TextBox)sender).Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;
            if (!VerifyAllExist(text.Split(' '))) return;
            try
            {
                Nethereum.HdWallet.Wallet wallet = new(text, "");
                var privateKeyHex = ByteArrayToHex(wallet.GetPrivateKey(1));
                var address = wallet.GetAddresses(1)[0];
                textBox_Eth_PK.Text = privateKeyHex;
                textBox_Eth_Address.Text = address;
            }
            catch (Exception ex)
            {
                textBox_Eth_PK.Text = ex.Message;
                if (ex.InnerException != null)
                    textBox_Eth_Address.Text = ex.InnerException.Message;
            }
            try
            {
                HDWallet.Core.IHDWallet<HDWallet.Tron.TronWallet> hdWallet = new HDWallet.Tron.TronHDWallet(text);
                var wallet = hdWallet.GetAccount(0).GetExternalWallet(0);
                var privateKeyHex = ByteArrayToHex(wallet.PrivateKeyBytes);
                var address = wallet.Address;
                textBox_Tron_PK.Text = privateKeyHex;
                textBox_Tron_Address.Text = address;
            }
            catch (Exception ex)
            {
                textBox_Tron_PK.Text = ex.Message;
                if (ex.InnerException != null)
                    textBox_Tron_Address.Text = ex.InnerException.Message;
            }
            try
            {
                Solnet.Wallet.Wallet wallet = new(text);
                var privateKeyHex = ByteArrayToHex(wallet.Account.PrivateKey.KeyBytes);
                var address = wallet.Account.PublicKey.Key;
                textBox_Sol_PK.Text = privateKeyHex;
                textBox_Sol_Address.Text = address;
            }
            catch (Exception ex)
            {
                textBox_Sol_PK.Text = ex.Message;
                if (ex.InnerException != null)
                    textBox_Sol_Address.Text = ex.InnerException.Message;
            }
        }

        public static byte[] HexToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static string ByteArrayToHex(byte[] ba)
        {
            StringBuilder hex = new(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        private void button_ETH_Go_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://blockscan.com/address/{textBox_Eth_Address.Text}",
                UseShellExecute = true
            });
        }

        private void button_Tron_Go_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://tronscan.org/#/address/{textBox_Tron_Address.Text}",
                UseShellExecute = true
            });
        }

        private void button_SOL_Go_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://solscan.io/account/{textBox_Sol_Address.Text}",
                UseShellExecute = true
            });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox_TopMost.Checked;
        }

        private void checkBox_HideTaskbar_CheckedChanged(object sender, EventArgs e)
        {
            this.ShowInTaskbar = !checkBox_HideTaskbar.Checked;
        }
    }
}