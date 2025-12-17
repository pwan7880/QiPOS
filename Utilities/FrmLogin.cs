using System;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmLogin : Form
    {
        private readonly LoginService loginService;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label lblMessage;
        private Button btnLogin;
        private Button btnCancel;

        public UserAccount AuthenticatedUser { get; private set; }

        public FrmLogin()
        {
            loginService = new LoginService();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "QiPOS Login";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new Size(350, 200);

            Label lblUser = new Label { Text = "Username", Location = new Point(20, 20), AutoSize = true };
            Label lblPass = new Label { Text = "Password", Location = new Point(20, 60), AutoSize = true };

            txtUsername = new TextBox { Location = new Point(120, 16), Width = 200 };
            txtPassword = new TextBox { Location = new Point(120, 56), Width = 200, PasswordChar = '•' };

            btnLogin = new Button { Text = "Login", Location = new Point(120, 100), Width = 90 };
            btnCancel = new Button { Text = "Cancel", Location = new Point(230, 100), Width = 90 };

            lblMessage = new Label { ForeColor = Color.Red, Location = new Point(20, 140), AutoSize = true };

            btnLogin.Click += BtnLogin_Click;
            btnCancel.Click += (s, e) => this.Close();
            txtPassword.KeyDown += TxtPassword_KeyDown;

            this.Controls.Add(lblUser);
            this.Controls.Add(lblPass);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(btnCancel);
            this.Controls.Add(lblMessage);
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                AttemptLogin();
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            AttemptLogin();
        }

        private void AttemptLogin()
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            try
            {
                UserAccount user = loginService.Authenticate(username, password);
                if (user == null)
                {
                    lblMessage.Text = "Invalid username or password.";
                    return;
                }

                AuthenticatedUser = user;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}
