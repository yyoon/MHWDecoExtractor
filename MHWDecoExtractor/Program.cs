namespace MHWDecoExtractor
{
    using MHWCrypto;
    using MHWSaveUtils;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Threading.Tasks;
    using TextCopy;

    class Program
    {
        static async Task Main(string[] args)
        {
            var saveFiles = FileSystemUtils.EnumerateSaveDataInfo();
            if (saveFiles.Count() != 1)
            {
                Console.WriteLine("Multiple user accounts found.");
                Environment.Exit(1);
                return;
            }

            await MasterData.Load("kor");

            var saveDataInfo = saveFiles.First();
            using (Stream inputStream = File.OpenRead(saveDataInfo.SaveDataFullFilename))
            {
                byte[] buffer = new byte[inputStream.Length];
                await inputStream.ReadAsync(buffer, 0, buffer.Length, CancellationToken.None);
                await new Crypto().DecryptAsync(buffer);
                var ms = new MemoryStream(buffer);

                await ClipboardService.SetTextAsync(ReadDecorations(ms));
            }

        }

        private static string ReadDecorations(Stream saveData)
        {
            var decorationsReader = new DecorationsReader(saveData);
            var counts = MasterData.Jewels.ToDictionary(j => j.ItemId, j => (uint)0);

            foreach (DecorationsSaveSlotInfo decorationInfo in decorationsReader.Read())
            {
                foreach (var kv in decorationInfo.Decorations)
                {
                    counts[kv.Key] += kv.Value;
                }
            }

            return string.Join("", counts
                .ToList()
                .OrderBy(kv => MasterData.FindJewelInfoByItemId(kv.Key).Name)
                .Select(NormalizeDecoString));
        }

        private static string NormalizeDecoString(KeyValuePair<uint, uint> decoKeyValue)
        {
            JewelInfo deco = MasterData.FindJewelInfoByItemId(decoKeyValue.Key);
            string decoName = deco.Name
                .Replace("Ⅱ", "II")
                .Replace("Ⅲ", "III");
            if (Regex.IsMatch(decoName, "【.】$"))
            {
                decoName = decoName.Substring(0, decoName.Length - 3);
            }

            Match match = Regex.Match(decoName, "(.+[^I]+)(I+)?");
            decoName = string.IsNullOrEmpty(match.Groups[2].Value)
                ? decoName
                : string.Format("{0} {1}", match.Groups[1], match.Groups[2]);

            return string.Format("{0}|{1};", decoName, decoKeyValue.Value);
        }
    }
}
