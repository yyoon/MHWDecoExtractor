namespace MHWDecoExtractorGUI
{
    using MHWSaveUtils;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                await MasterData.Load("kor");
            }
            catch (Exception)
            {
                MessageBox.Show(
                    "장식주 데이터베이스 불러오기에 실패했습니다.\n인터넷 연결이 되어있는지 확인해주세요.",
                    "오류",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            Application.Run(new MainForm());
        }
    }
}
