namespace MHWDecoExtractorGUI.Models
{
    using MHWSaveUtils;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    class MHWWikiDBDecorationListExporter : IDecorationListExporter
    {
        public string DisplayName => "mhw.wiki-db.com";

        public string ExportAsString(IReadOnlyDictionary<uint, uint> decorations)
        {
            var counts = MasterData.Jewels.ToDictionary(j => j.ItemId, j => (uint)0);

            foreach (var kv in decorations)
            {
                counts[kv.Key] = kv.Value;
            }

            var exportString = "{" + string.Join(", ", counts.ToList()
                .Select(kv => KeyValuePair.Create(GetNormalizedDecorationName(kv.Key), kv.Value))
                .OrderBy(kv => kv.Key, DecorationNameComparer.Instance)
                .Select(kv => $"\"{kv.Key}\": {Math.Min(kv.Value, 9)}")) + "}";
            return exportString;
        }

        internal string GetNormalizedDecorationName(uint decorationId)
        {
            string decorationName = MasterData.FindJewelInfoByItemId(decorationId).Name
                .Replace("IV", "Ⅳ")
                .Replace("-", "・");

            // NOTE: Handle the inconsistencies between Gobbler combined L4 jewel names.
            // mhw.wiki-db.com consistently uses "흡입-" prefix, whereas the in-game name is mixed.
            decorationName = decorationName.Replace("조식", "흡입");

            // There is a space between the decoration name and its level,
            // except for the ones with roman numerals.
            if (!Regex.IsMatch(decorationName, "[ⅡⅢⅣ]"))
            {
                decorationName = $"{decorationName[0..^3]} {decorationName[^3..]}";
            }

            // The following decoration names should be normalized to use the half-width digits
            // in the decoration level portion.
            if (Regex.IsMatch(decorationName, "^(낙법주|삭격주|양동주|추위 내성주).*"))
            {
                decorationName = decorationName.Normalize(System.Text.NormalizationForm.FormKC);
            }

            return decorationName;
        }
    }
}
