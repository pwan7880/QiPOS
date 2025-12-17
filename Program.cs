using System.Windows.Forms;

namespace QiPOS
{
    public sealed class Program
    {
        [System.STAThread]
        public static void Main()
        { 
            Application.Run(new QiPOS.FrmPos());
        }
    }
}
