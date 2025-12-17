using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmUserManagement : Form
    {
        private readonly UserRepository userRepository;
        private readonly UserAccount currentUser;
        private DataGridView dgvUsers;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private NumericUpDown numPriority;
        private Button btnAddUser;
        private Button btnResetPassword;
        private Button btnUpdatePriority;
        private Button btnClose;
        private Label lblStatus;

        public FrmUserManagement(UserAccount user)
        {
            currentUser = user ?? new UserAccount { Name = "Unknown", Priority = int.MaxValue };
            userRepository = new UserRepository();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "User Management";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(600, 420);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            dgvUsers = new DataGridView
            {
                Location = new Point(20, 20),
                Size = new Size(560, 200),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvUsers.SelectionChanged += DgvUsers_SelectionChanged;

            Label lblUser = new Label { Text = "Username", Location = new Point(20, 240), AutoSize = true };
            Label lblPassword = new Label { Text = "Password", Location = new Point(20, 270), AutoSize = true };
            Label lblPriority = new Label { Text = "Priority", Location = new Point(20, 300), AutoSize = true };

            txtUsername = new TextBox { Location = new Point(100, 236), Width = 200 };
            txtPassword = new TextBox { Location = new Point(100, 266), Width = 200, PasswordChar = '•' };
            numPriority = new NumericUpDown { Location = new Point(100, 296), Width = 80, Minimum = 0, Maximum = 10, Value = 2 };

            btnAddUser = new Button { Text = "Add User", Location = new Point(330, 236), Width = 120 };
            btnResetPassword = new Button { Text = "Reset Password", Location = new Point(330, 266), Width = 120 };
            btnUpdatePriority = new Button { Text = "Update Priority", Location = new Point(330, 296), Width = 120 };
            btnClose = new Button { Text = "Close", Location = new Point(460, 340), Width = 120 };
            lblStatus = new Label { Location = new Point(20, 340), ForeColor = Color.DarkGreen, AutoSize = true };

            btnAddUser.Click += BtnAddUser_Click;
            btnResetPassword.Click += BtnResetPassword_Click;
            btnUpdatePriority.Click += BtnUpdatePriority_Click;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(dgvUsers);
            this.Controls.Add(lblUser);
            this.Controls.Add(lblPassword);
            this.Controls.Add(lblPriority);
            this.Controls.Add(txtUsername);
            this.Controls.Add(txtPassword);
            this.Controls.Add(numPriority);
            this.Controls.Add(btnAddUser);
            this.Controls.Add(btnResetPassword);
            this.Controls.Add(btnUpdatePriority);
            this.Controls.Add(btnClose);
            this.Controls.Add(lblStatus);

            this.Load += FrmUserManagement_Load;
        }

        private void FrmUserManagement_Load(object sender, EventArgs e)
        {
            if (!currentUser.IsAdmin)
            {
                MessageBox.Show("You do not have permission to manage users.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            LoadUsers();
        }

        private void LoadUsers()
        {
            DataTable users = userRepository.GetUsers();
            dgvUsers.DataSource = users;
            dgvUsers.ClearSelection();
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            int priority = (int)numPriority.Value;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                lblStatus.Text = "Username and password are required.";
                lblStatus.ForeColor = Color.DarkRed;
                return;
            }

            try
            {
                string hashedPassword = PasswordHasher.HashPassword(password);
                userRepository.AddUser(username, hashedPassword, priority);
                lblStatus.Text = "User added successfully.";
                lblStatus.ForeColor = Color.DarkGreen;
                LoadUsers();
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.DarkRed;
            }
        }

        private void BtnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                lblStatus.Text = "Select a user to reset password.";
                lblStatus.ForeColor = Color.DarkRed;
                return;
            }

            string password = txtPassword.Text;
            if (string.IsNullOrWhiteSpace(password))
            {
                lblStatus.Text = "Provide a new password.";
                lblStatus.ForeColor = Color.DarkRed;
                return;
            }

            try
            {
                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["user_id"].Value);
                string hashedPassword = PasswordHasher.HashPassword(password);
                userRepository.UpdatePassword(userId, hashedPassword);
                lblStatus.Text = "Password updated.";
                lblStatus.ForeColor = Color.DarkGreen;
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.DarkRed;
            }
        }

        private void BtnUpdatePriority_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                lblStatus.Text = "Select a user to update priority.";
                lblStatus.ForeColor = Color.DarkRed;
                return;
            }

            try
            {
                int userId = Convert.ToInt32(dgvUsers.SelectedRows[0].Cells["user_id"].Value);
                int priority = (int)numPriority.Value;
                userRepository.UpdatePriority(userId, priority);
                lblStatus.Text = "Priority updated.";
                lblStatus.ForeColor = Color.DarkGreen;
                LoadUsers();
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.Message;
                lblStatus.ForeColor = Color.DarkRed;
            }
        }

        private void DgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
                return;

            var row = dgvUsers.SelectedRows[0];
            txtUsername.Text = row.Cells["name"].Value.ToString();
            numPriority.Value = Convert.ToDecimal(row.Cells["priority"].Value);
            txtPassword.Clear();
        }
    }
}
