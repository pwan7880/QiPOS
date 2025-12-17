using System.Windows.Forms;

namespace QiPOS
{
    public sealed class Program
    {
        [System.STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var loginForm = new QiPOS.FrmLogin())
            {
                if (loginForm.ShowDialog() == DialogResult.OK && loginForm.AuthenticatedUser != null)
                {
                    Application.Run(new QiPOS.FrmPos(loginForm.AuthenticatedUser));
                }
            }
        }
    }
}
