using System;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmConfig : Form
    {
        private Button BtnClose;
        private Button BtnSave;
        private Button BtnManageUsers;
        private Label label1;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private Label label15;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label lblIns;
        private Panel pnConfig;
        private TextBox txtABN;
        private TextBox txtadd1;
        private TextBox txtAdd2;
        private TextBox txtAdd3;
        private TextBox txtDisplay;
        private TextBox txtEmail;
        private TextBox txtFax;
        private TextBox txtName;
        private TextBox txtPrinter;
        private CheckBox checkBoxCards;
        private TextBox txtTel;
        public bool checkCardsEnabled = false;
        private readonly UserAccount currentUser;
        public FrmConfig(UserAccount user)
        {
            currentUser = user ?? new UserAccount { Id = 0, Name = "Unknown", Priority = int.MaxValue };
            InitializeComponent();
            Location = new System.Drawing.Point(1, 30);
        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void FrmConfig_Load(object sender, EventArgs e)
        {
            ConfigurationReader reader = new ConfigurationReader();
            CompanyData companyData = reader.CompanyInfo();
            this.txtPrinter.Text = companyData.PosPrinter;
            this.txtDisplay.Text = companyData.LineDisplayPort;
            this.txtName.Text = companyData.CompanyName;
            this.txtABN.Text = companyData.CompanyABN;
            this.txtadd1.Text = companyData.AddressLine1;
            this.txtAdd2.Text = companyData.AddressCity;
            this.txtAdd3.Text = companyData.AddressCity2;
            this.txtTel.Text = companyData.Telephone;
            this.txtFax.Text = companyData.Fax;
            this.txtEmail.Text = companyData.Email;

            if (BtnManageUsers != null)
                BtnManageUsers.Enabled = currentUser.IsAdmin;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                CompanyData data = new CompanyData
                {
                    PosPrinter = this.txtPrinter.Text,
                    LineDisplayPort = this.txtDisplay.Text,
                    CompanyName = this.txtName.Text,
                    CompanyABN = this.txtABN.Text,
                    AddressLine1 = this.txtadd1.Text,
                    AddressCity = this.txtAdd2.Text,
                    AddressCity2 = this.txtAdd3.Text,
                    Telephone = this.txtTel.Text,
                    Fax = this.txtFax.Text,
                    Email = this.txtEmail.Text
                };
                this.checkCardsEnabled = this.checkBoxCards.Checked;
                ConfigurationReader reader = new ConfigurationReader();
                reader.SaveConfig(data);
                
                this.Close();
            }
            catch (ConfigIOException ex)
            {
                MessageBox.Show($"Validation failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnManageUsers_Click(object sender, EventArgs e)
        {
            using (FrmUserManagement form = new FrmUserManagement(currentUser))
            {
                form.ShowDialog(this);
            }
        }

        private void InitializeComponent()
        {
            this.lblIns = new System.Windows.Forms.Label();
            this.pnConfig = new System.Windows.Forms.Panel();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.txtPrinter = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            this.BtnClose = new System.Windows.Forms.Button();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.txtAdd3 = new System.Windows.Forms.TextBox();
            this.txtAdd2 = new System.Windows.Forms.TextBox();
            this.txtadd1 = new System.Windows.Forms.TextBox();
            this.txtABN = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxCards = new System.Windows.Forms.CheckBox();
            this.BtnManageUsers = new System.Windows.Forms.Button();
            this.pnConfig.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblIns
            // 
            this.lblIns.AutoSize = true;
            this.lblIns.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIns.Location = new System.Drawing.Point(8, 8);
            this.lblIns.Name = "lblIns";
            this.lblIns.Size = new System.Drawing.Size(135, 25);
            this.lblIns.TabIndex = 0;
            this.lblIns.Text = "Instruction here";
            // 
            // pnConfig
            // 
            this.pnConfig.Controls.Add(this.checkBoxCards);
            this.pnConfig.Controls.Add(this.BtnManageUsers);
            this.pnConfig.Controls.Add(this.txtDisplay);
            this.pnConfig.Controls.Add(this.txtPrinter);
            this.pnConfig.Controls.Add(this.label14);
            this.pnConfig.Controls.Add(this.label15);
            this.pnConfig.Controls.Add(this.BtnSave);
            this.pnConfig.Controls.Add(this.BtnClose);
            this.pnConfig.Controls.Add(this.txtEmail);
            this.pnConfig.Controls.Add(this.txtFax);
            this.pnConfig.Controls.Add(this.txtTel);
            this.pnConfig.Controls.Add(this.txtAdd3);
            this.pnConfig.Controls.Add(this.txtAdd2);
            this.pnConfig.Controls.Add(this.txtadd1);
            this.pnConfig.Controls.Add(this.txtABN);
            this.pnConfig.Controls.Add(this.txtName);
            this.pnConfig.Controls.Add(this.label13);
            this.pnConfig.Controls.Add(this.label12);
            this.pnConfig.Controls.Add(this.label11);
            this.pnConfig.Controls.Add(this.label10);
            this.pnConfig.Controls.Add(this.label9);
            this.pnConfig.Controls.Add(this.label8);
            this.pnConfig.Controls.Add(this.label7);
            this.pnConfig.Controls.Add(this.label6);
            this.pnConfig.Controls.Add(this.label1);
            this.pnConfig.Location = new System.Drawing.Point(12, 48);
            this.pnConfig.Name = "pnConfig";
            this.pnConfig.Size = new System.Drawing.Size(963, 452);
            this.pnConfig.TabIndex = 1;
            // 
            // txtDisplay
            // 
            this.txtDisplay.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisplay.Location = new System.Drawing.Point(217, 96);
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(200, 49);
            this.txtDisplay.TabIndex = 27;
            // 
            // txtPrinter
            // 
            this.txtPrinter.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrinter.Location = new System.Drawing.Point(217, 61);
            this.txtPrinter.Name = "txtPrinter";
            this.txtPrinter.Size = new System.Drawing.Size(200, 49);
            this.txtPrinter.TabIndex = 26;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(48, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(114, 45);
            this.label14.TabIndex = 14;
            this.label14.Text = "Printer";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(48, 100);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(122, 45);
            this.label15.TabIndex = 15;
            this.label15.Text = "Display";
            // 
            // BtnSave
            // 
            this.BtnSave.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSave.Location = new System.Drawing.Point(443, 395);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(154, 47);
            this.BtnSave.TabIndex = 13;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnClose
            // 
            this.BtnClose.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnClose.Location = new System.Drawing.Point(617, 395);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(154, 47);
            this.BtnClose.TabIndex = 12;
            this.BtnClose.Text = "Close";
            this.BtnClose.UseVisualStyleBackColor = true;
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(617, 330);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 49);
            this.txtEmail.TabIndex = 11;
            // 
            // txtFax
            // 
            this.txtFax.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.Location = new System.Drawing.Point(617, 295);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(200, 49);
            this.txtFax.TabIndex = 10;
            // 
            // txtTel
            // 
            this.txtTel.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTel.Location = new System.Drawing.Point(617, 260);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(200, 49);
            this.txtTel.TabIndex = 9;
            // 
            // txtAdd3
            // 
            this.txtAdd3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdd3.Location = new System.Drawing.Point(617, 225);
            this.txtAdd3.Name = "txtAdd3";
            this.txtAdd3.Size = new System.Drawing.Size(200, 49);
            this.txtAdd3.TabIndex = 8;
            // 
            // txtAdd2
            // 
            this.txtAdd2.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdd2.Location = new System.Drawing.Point(617, 190);
            this.txtAdd2.Name = "txtAdd2";
            this.txtAdd2.Size = new System.Drawing.Size(200, 49);
            this.txtAdd2.TabIndex = 7;
            // 
            // txtadd1
            // 
            this.txtadd1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtadd1.Location = new System.Drawing.Point(617, 135);
            this.txtadd1.Name = "txtadd1";
            this.txtadd1.Size = new System.Drawing.Size(200, 49);
            this.txtadd1.TabIndex = 6;
            // 
            // txtABN
            // 
            this.txtABN.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtABN.Location = new System.Drawing.Point(617, 100);
            this.txtABN.Name = "txtABN";
            this.txtABN.Size = new System.Drawing.Size(200, 49);
            this.txtABN.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(617, 65);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 49);
            this.txtName.TabIndex = 4;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(443, 330);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(96, 45);
            this.label13.TabIndex = 3;
            this.label13.Text = "Email";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(443, 295);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 45);
            this.label12.TabIndex = 2;
            this.label12.Text = "Fax";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(443, 260);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(167, 45);
            this.label11.TabIndex = 1;
            this.label11.Text = "Telephone";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(443, 225);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(237, 45);
            this.label10.TabIndex = 0;
            this.label10.Text = "Address Line 2 ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(443, 190);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(207, 45);
            this.label9.TabIndex = 23;
            this.label9.Text = "Address City ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(443, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(237, 45);
            this.label8.TabIndex = 7;
            this.label8.Text = "Address Line 1 ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(443, 100);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 45);
            this.label7.TabIndex = 6;
            this.label7.Text = "ABN";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(443, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(249, 45);
            this.label6.TabIndex = 5;
            this.label6.Text = "Company Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(111, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hardware Config";
            // 
            // checkBoxCards
            // 
            this.checkBoxCards.AutoSize = true;
            this.checkBoxCards.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBoxCards.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCards.Location = new System.Drawing.Point(49, 169);
            this.checkBoxCards.Name = "checkBoxCards";
            this.checkBoxCards.Size = new System.Drawing.Size(129, 41);
            this.checkBoxCards.TabIndex = 28;
            this.checkBoxCards.Text = "Cards";
            this.checkBoxCards.UseVisualStyleBackColor = true;
            //
            // BtnManageUsers
            //
            this.BtnManageUsers.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnManageUsers.Location = new System.Drawing.Point(54, 387);
            this.BtnManageUsers.Name = "BtnManageUsers";
            this.BtnManageUsers.Size = new System.Drawing.Size(188, 43);
            this.BtnManageUsers.TabIndex = 35;
            this.BtnManageUsers.Text = "Manage Users";
            this.BtnManageUsers.UseVisualStyleBackColor = true;
            this.BtnManageUsers.Click += new System.EventHandler(this.BtnManageUsers_Click);
            //
            // FrmConfig
            //
            this.ClientSize = new System.Drawing.Size(987, 512);
            this.Controls.Add(this.pnConfig);
            this.Controls.Add(this.lblIns);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmConfig";
            this.Text = "Modify Config";
            this.Load += new System.EventHandler(this.FrmConfig_Load);
            this.pnConfig.ResumeLayout(false);
            this.pnConfig.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}