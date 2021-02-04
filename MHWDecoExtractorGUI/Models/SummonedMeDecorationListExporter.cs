namespace MHWDecoExtractorGUI.Models
{
    using MHWSaveUtils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class SummonedMeDecorationListExporter : IDecorationListExporter
    {
        public string DisplayName => "summoned.me";

        public string ExportAsString(IReadOnlyDictionary<uint, uint> decorations)
        {
            var counts = MasterData.Jewels.ToDictionary(j => j.ItemId, j => (uint)0);

            foreach (var kv in decorations)
            {
                counts[kv.Key] = kv.Value;
            }

            var exportString = string.Join("", counts.ToList()
                .Select(kv => KeyValuePair.Create(GetNormalizedDecorationName(kv.Key), kv.Value))
                .OrderBy(kv => kv.Key, DecorationNameComparer.Instance)
                .Select(kv => string.Format("{0}|{1};", kv.Key, Math.Min(kv.Value, 9))));
            return exportString;
        }

        internal string GetNormalizedDecorationName(uint decorationId)
        {
            string decorationName = MasterData.FindJewelInfoByItemId(decorationId).Name
                .Replace("Ⅱ", "II")
                .Replace("Ⅲ", "III");

            // NOTE: Handle the inconsistencies between Gobbler combined L4 jewel names.
            // summoned.me website consistently uses "조식-" prefix, whereas the in-game name is mixed.
            decorationName = decorationName.Replace("흡입", "조식");

            if (Regex.IsMatch(decorationName, "【.】$"))
            {
                decorationName = decorationName.Substring(0, decorationName.Length - 3);
            }

            Match match = Regex.Match(decorationName, "(.+[^IVX]+)([IVX]+)?");
            decorationName = string.IsNullOrEmpty(match.Groups[2].Value)
                ? decorationName
                : string.Format("{0} {1}", match.Groups[1], match.Groups[2]);

            return decorationName;
        }
    }
}
