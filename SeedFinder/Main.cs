using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SeedFinder
{

    internal class Main
    {
        static string[]? bip32Words;

        public Main()
        {
            using var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("SeedFinder.BIP39Words.txt");
            using StreamReader reader = new(resource!);
            string result = reader.ReadToEnd();
            bip32Words = result.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static bool VerifyAllExist(params string[] words) => ValidPhraseLength(words) && words.All(bip32Words!.Contains);
        private static bool ValidPhraseLength(string[] words) => words.Length is 12 or 15 or 18 or 21 or 24 or 25;

        public void CheckMissedWord()
        {
            var oldSeed = "";
            var oldSeedWords = oldSeed.Split(' ');
            int wordCount = oldSeedWords.Length;
            for (int missedIndex = 0; missedIndex < wordCount; missedIndex++)
            {
                Logger.WriteLine($"<{missedIndex + 1}>", ConsoleColor.DarkGray);
                for (int wordIndex = 0; wordIndex < bip32Words!.Length; wordIndex++)
                {
                    string text = "";
                    for (int i = 0; i < oldSeedWords.Length; i++)
                    {
                        if (i == missedIndex)
                        {
                            text += bip32Words[wordIndex] + " ";
                        }
                        else
                        {
                            text += oldSeedWords[i] + " ";
                        }
                    }

                    if (!VerifyAllExist(text.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)))
                    {
                        Logger.WriteLine("Invaild word", ConsoleColor.Red);
                        return;
                    }
                    Nethereum.HdWallet.Wallet wallet = new(text, "");
                    if (!wallet.IsMnemonicValidChecksum) continue;
                    var address = wallet.GetAddresses()[0];
                    Logger.WriteLine(address);
                    if (address.EndsWith("dddB"))
                    {
                        Logger.WriteLine("End", ConsoleColor.Green);
                        Logger.WriteLine(bip32Words[wordIndex]);
                        return;
                    }

                }
            }
        }

    }
}
