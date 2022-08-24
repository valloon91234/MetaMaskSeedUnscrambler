using Nethereum.HdWallet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MetaMaskSeedUnscrambler
{
    public partial class Form1 : Form
    {
        static string[] bip32Words;
        public Form1()
        {
            InitializeComponent();
            using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("MetaMaskSeedUnscrambler.BIP39Words.txt"))
            {
                using (StreamReader reader = new StreamReader(resource))
                {
                    string result = reader.ReadToEnd();
                    bip32Words = result.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                }
            }
            this.Text += $" ({bip32Words.Length})";
        }

        private static bool VerifyAllExist(params string[] words) => ValidPhraseLength(words) && words.All(bip32Words.Contains);
        private static bool ValidPhraseLength(string[] words) => words.Length == 12 || words.Length == 15 || words.Length == 18 || words.Length == 21 || words.Length == 24;

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var text = ((TextBox)sender).Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;
            if (!VerifyAllExist(text.Split(' '))) return;
            var wallet = new Wallet(text, "");
            var privateKeyHex = ByteArrayToHex(wallet.GetPrivateKey(1));
            var address = wallet.GetAddresses(1)[0];
            textBox2.Text = privateKeyHex;
            textBox3.Text = address;
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
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
