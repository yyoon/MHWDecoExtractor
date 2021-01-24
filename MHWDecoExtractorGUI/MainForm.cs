namespace MHWDecoExtractorGUI
{
    using MHWCrypto;
    using MHWSaveUtils;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Text.Unicode;
    using System.Threading;
    using System.Windows.Forms;
    using TextCopy;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string LastUsedSteamUserId { get; set; }

        private string LastUsedCharacterName { get; set; }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ReloadDecorations();
        }

        private async void ComboBoxSteamUsers_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxCharacters.DataSource = null;

            if (comboBoxSteamUsers.SelectedIndex == -1)
            {
                return;
            }

            var saveFileFullPath = comboBoxSteamUsers.SelectedValue.ToString();

            // Decrypt the save data.
            using Stream inputStream = File.OpenRead(saveFileFullPath);
            byte[] buffer = new byte[inputStream.Length];
            await inputStream.ReadAsync(buffer, 0, buffer.Length, CancellationToken.None);
            await new Crypto().DecryptAsync(buffer);
            var saveData = new MemoryStream(buffer);

            // Load the decorations info.
            var decorationsReader = new DecorationsReader(saveData);
            var decorationsSaveSlots = decorationsReader.Read().ToList();
            comboBoxCharacters.DisplayMember = "Name";
            comboBoxCharacters.ValueMember = "Decorations";
            comboBoxCharacters.DataSource = decorationsSaveSlots;

            // Auto-select the remembered character or the first available character.
            if (decorationsSaveSlots.Any())
            {
                int indexToSelect = -1;
                if (LastUsedCharacterName != null)
                {
                    indexToSelect = decorationsSaveSlots
                        .FindIndex(saveSlot => saveSlot.Name == LastUsedCharacterName);
                }

                comboBoxCharacters.SelectedIndex = indexToSelect >= 0
                    ? indexToSelect
                    : 0;
            }

            LastUsedCharacterName = null;
        }

        private void ComboBoxCharacters_SelectedValueChanged(object sender, EventArgs e)
        {
            listViewDecorations.Items.Clear();

            if (comboBoxCharacters.SelectedIndex == -1)
            {
                return;
            }

            if (!(comboBoxCharacters.SelectedValue is IReadOnlyDictionary<uint, uint> decorations))
            {
                return;
            }

            listViewDecorations.Items.AddRange(decorations.ToList()
                .OrderBy(kv => GetNormalizedDecorationName(kv.Key), DecorationNameComparer.Instance)
                .Select(kv => new ListViewItem(new string[] { MasterData.FindJewelInfoByItemId(kv.Key).Name, kv.Value.ToString() }))
                .ToArray());

            buttonClipboard.Focus();
        }

        private void ButtonReload_Click(object sender, EventArgs e)
        {
            ReloadDecorations();
        }

        private async void ButtonClipboard_Click(object sender, EventArgs e)
        {
            var counts = MasterData.Jewels.ToDictionary(j => j.ItemId, j => (uint)0);

            if (!(comboBoxCharacters.SelectedValue is IReadOnlyDictionary<uint, uint> decorations))
            {
                return;
            }

            foreach (var kv in decorations)
            {
                counts[kv.Key] = kv.Value;
            }

            var exportString = string.Join("", counts.ToList()
                .Select(kv => KeyValuePair.Create(GetNormalizedDecorationName(kv.Key), kv.Value))
                .OrderBy(kv => kv.Key, DecorationNameComparer.Instance)
                .Select(kv => string.Format("{0}|{1};", kv.Key, Math.Min(kv.Value, 9))));

            await ClipboardService.SetTextAsync(exportString);
        }

        private void ReloadDecorations()
        {
            // In case there are pre-selected items, try to remember them.
            LastUsedSteamUserId = comboBoxSteamUsers.SelectedItem is SaveDataInfo saveData
                ? saveData.UserId
                : null;

            LastUsedCharacterName = comboBoxCharacters.SelectedItem is DecorationsSaveSlotInfo saveSlot
                ? saveSlot.Name
                : null;

            // Collect save files from all Steam users.
            var saveFiles = FileSystemUtils.EnumerateSaveDataInfo().ToList();
            if (!saveFiles.Any())
            {
                MessageBox.Show(
                    "몬스터 헌터: 월드 세이브 파일이 있는 스팀 계정을 찾지 못했습니다.",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            comboBoxSteamUsers.DisplayMember = "UserId";
            comboBoxSteamUsers.ValueMember = "SaveDataFullFilename";
            comboBoxSteamUsers.DataSource = saveFiles;

            // Auto-select the remembered steam user or the first steam user.
            int indexToSelect = -1;
            if (LastUsedSteamUserId != null)
            {
                indexToSelect = saveFiles
                    .FindIndex(saveFile => saveFile.UserId == LastUsedSteamUserId);
            }

            comboBoxSteamUsers.SelectedIndex = indexToSelect >= 0
                ? indexToSelect
                : 0;

            LastUsedSteamUserId = null;
        }

        private string GetNormalizedDecorationName(uint decorationId)
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

        private class DecorationNameComparer : IComparer<string>
        {
            private DecorationNameComparer() { }

            public static DecorationNameComparer Instance { get; } = new DecorationNameComparer();

            int IComparer<string>.Compare(string x, string y)
            {
                int minLength = Math.Min(x.Length, y.Length);
                for (int i = 0; i < minLength; ++i)
                {
                    char xch = x[i];
                    char ych = y[i];

                    // Place Korean letters before English alphabets.
                    if (IsHangul(xch) && IsEnglish(ych))
                        return -1;
                    if (IsHangul(ych) && IsEnglish(xch))
                        return 1;

                    if (xch < ych)
                        return -1;
                    if (xch > ych)
                        return 1;
                }

                if (x.Length < y.Length)
                    return -1;
                if (x.Length > y.Length)
                    return 1;

                return 0;
            }

            private bool IsHangul(char ch)
            {
                return IsCharInRange(ch, UnicodeRanges.HangulSyllables);
            }

            private bool IsEnglish(char ch)
            {
                return IsCharInRange(ch, UnicodeRanges.BasicLatin) && char.IsLetter(ch);
            }

            private bool IsCharInRange(char ch, UnicodeRange range)
            {
                return range.FirstCodePoint <= ch && ch < range.FirstCodePoint + range.Length;
            }
        }
    }
}
