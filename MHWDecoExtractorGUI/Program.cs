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
                    "����� �����ͺ��̽� �ҷ����⿡ �����߽��ϴ�.\n���ͳ� ������ �Ǿ��ִ��� Ȯ�����ּ���.",
                    "����",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Environment.Exit(1);
            }

            Application.Run(new MainForm());
        }
    }
}
