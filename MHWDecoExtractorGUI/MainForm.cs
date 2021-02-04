namespace MHWDecoExtractorGUI
{
    using MHWCrypto;
    using MHWDecoExtractorGUI.Models;
    using MHWSaveUtils;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Reflection;
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
            labelVersion.Text = string.Format("버전: v{0}", Application.ProductVersion);

            ReloadDecorations();

            PopulateDecorationListExporters();
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
                .Select(kv => new ListViewItem(new string[] { MasterData.FindJewelInfoByItemId(kv.Key).Name, kv.Value.ToString() }))
                .OrderBy(lvi => lvi.SubItems[0].Text, DecorationNameComparer.Instance)
                .ToArray());

            buttonClipboard.Focus();
        }

        private void ButtonReload_Click(object sender, EventArgs e)
        {
            ReloadDecorations();
        }

        private async void ButtonClipboard_Click(object sender, EventArgs e)
        {
            if (!(comboBoxCharacters.SelectedValue is IReadOnlyDictionary<uint, uint> decorations))
            {
                return;
            }

            if (!(comboBoxExporterType.SelectedValue is IDecorationListExporter exporter))
            {
                return;
            }

            await ClipboardService.SetTextAsync(exporter.ExportAsString(decorations));
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

        private void PopulateDecorationListExporters()
        {
            Type exporterType = typeof(IDecorationListExporter);

            var exporters = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.Namespace == exporterType.Namespace)
                .Where(t => t.IsClass && exporterType.IsAssignableFrom(t))
                .Select(t => (IDecorationListExporter)t.GetConstructors()[0].Invoke(null))
                .Select(exporter => new { exporter.DisplayName, Exporter = exporter })
                .ToList();

            comboBoxExporterType.DisplayMember = "DisplayName";
            comboBoxExporterType.ValueMember = "Exporter";
            comboBoxExporterType.DataSource = exporters;
            comboBoxExporterType.SelectedIndex = 0;
        }
    }
}
