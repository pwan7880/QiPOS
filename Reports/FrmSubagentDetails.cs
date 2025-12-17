using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmSubagentDetails : Form
    {
        private Label lblIns;
        private Panel panel1;
        private Label label1;
        private TextBox txtEmail;
        private Label label16;
        private TextBox txtTel;
        private TextBox txtFax;
        private TextBox textBox4;
        private TextBox txtAdd3;
        private TextBox txtAdd2;
        private TextBox txtAdd1;
        private TextBox txtABN;
        private TextBox txtName;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private Label label23;
        private Label label24;
        private ComboBox cbxTemplate;
        private TextBox txtComm;
        private Label label2;
        private ComboBox cbxSub;
        private Label lblNo;
        private ComboBox cbxNoWeeks;
        private TextBox txtRate;
        private Label lblComm;
        private ComboBox cbxLink;
        private Label label3;
        private ComboBox cbxAccount;
        private Label label4;
        private string queryStr;
        private CustomButton saveButton;
        private CustomButton deleteButton;
        private CustomButton closeButton;
        private Connect connDB;

        public FrmSubagentDetails()
        {
            this.InitializeComponent();
            this.Initiate();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSubagentDetails));
            this.lblIns = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxAccount = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxLink = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this.lblComm = new System.Windows.Forms.Label();
            this.lblNo = new System.Windows.Forms.Label();
            this.cbxNoWeeks = new System.Windows.Forms.ComboBox();
            this.cbxSub = new System.Windows.Forms.ComboBox();
            this.txtComm = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxTemplate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.txtFax = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtAdd3 = new System.Windows.Forms.TextBox();
            this.txtAdd2 = new System.Windows.Forms.TextBox();
            this.txtAdd1 = new System.Windows.Forms.TextBox();
            this.txtABN = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.saveButton = new QiPOS.CustomButton();
            this.deleteButton = new QiPOS.CustomButton();
            this.closeButton = new QiPOS.CustomButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();

            //
            // lblIns
            //
            this.lblIns.AutoSize = true;
            this.lblIns.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIns.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblIns.Location = new System.Drawing.Point(314, 27);
            this.lblIns.Name = "lblIns";
            this.lblIns.Size = new System.Drawing.Size(221, 29);
            this.lblIns.TabIndex = 3;
            this.lblIns.Text = "Sub Agent Details";

            //
            // panel1
            //
            this.panel1.Controls.Add(this.closeButton);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.cbxAccount);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbxLink);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtRate);
            this.panel1.Controls.Add(this.lblComm);
            this.panel1.Controls.Add(this.lblNo);
            this.panel1.Controls.Add(this.cbxNoWeeks);
            this.panel1.Controls.Add(this.cbxSub);
            this.panel1.Controls.Add(this.txtComm);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cbxTemplate);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.txtTel);
            this.panel1.Controls.Add(this.txtFax);
            this.panel1.Controls.Add(this.textBox4);
            this.panel1.Controls.Add(this.txtAdd3);
            this.panel1.Controls.Add(this.txtAdd2);
            this.panel1.Controls.Add(this.txtAdd1);
            this.panel1.Controls.Add(this.txtABN);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label17);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.label24);
            this.panel1.Location = new System.Drawing.Point(12, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(804, 468);
            this.panel1.TabIndex = 34;

            //
            // cbxAccount
            //
            this.cbxAccount.FormattingEnabled = true;
            this.cbxAccount.Location = new System.Drawing.Point(205, 65);
            this.cbxAccount.Name = "cbxAccount";
            this.cbxAccount.Size = new System.Drawing.Size(240, 32);
            this.cbxAccount.TabIndex = 44;

            //
            // label4
            //
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(52, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 24);
            this.label4.TabIndex = 43;
            this.label4.Text = "Link Account";

            //
            // cbxLink
            //
            this.cbxLink.FormattingEnabled = true;
            this.cbxLink.Location = new System.Drawing.Point(205, 104);
            this.cbxLink.Name = "cbxLink";
            this.cbxLink.Size = new System.Drawing.Size(240, 32);
            this.cbxLink.TabIndex = 42;

            //
            // label3
            //
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(84, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 24);
            this.label3.TabIndex = 41;
            this.label3.Text = "Link Card";

            //
            // txtRate
            //
            this.txtRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRate.Location = new System.Drawing.Point(655, 156);
            this.txtRate.Name = "txtRate";
            this.txtRate.Size = new System.Drawing.Size(60, 31);
            this.txtRate.TabIndex = 39;
            this.txtRate.Text = "12.5";
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;

            //
            // lblComm
            //
            this.lblComm.AutoSize = true;
            this.lblComm.Location = new System.Drawing.Point(481, 157);
            this.lblComm.Name = "lblComm";
            this.lblComm.Size = new System.Drawing.Size(157, 24);
            this.lblComm.TabIndex = 38;
            this.lblComm.Text = "Commission Rate";

            //
            // lblNo
            //
            this.lblNo.AutoSize = true;
            this.lblNo.Location = new System.Drawing.Point(481, 117);
            this.lblNo.Name = "lblNo";
            this.lblNo.Size = new System.Drawing.Size(103, 24);
            this.lblNo.TabIndex = 37;
            this.lblNo.Text = "No. Weeks";

            //
            // cbxNoWeeks
            //
            this.cbxNoWeeks.FormattingEnabled = true;
            this.cbxNoWeeks.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6"});
            this.cbxNoWeeks.Location = new System.Drawing.Point(655, 109);
            this.cbxNoWeeks.Name = "cbxNoWeeks";
            this.cbxNoWeeks.Size = new System.Drawing.Size(60, 32);
            this.cbxNoWeeks.TabIndex = 36;
            this.cbxNoWeeks.Text = "1";

            //
            // cbxSub
            //
            this.cbxSub.FormattingEnabled = true;
            this.cbxSub.Location = new System.Drawing.Point(205, 27);
            this.cbxSub.Name = "cbxSub";
            this.cbxSub.Size = new System.Drawing.Size(240, 32);
            this.cbxSub.TabIndex = 35;
            this.cbxSub.SelectedIndexChanged += new System.EventHandler(this.CbxSub_SelectedIndexChanged);

            //
            // txtComm
            //
            this.txtComm.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComm.Location = new System.Drawing.Point(205, 427);
            this.txtComm.Name = "txtComm";
            this.txtComm.Size = new System.Drawing.Size(240, 29);
            this.txtComm.TabIndex = 10;

            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 427);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 24);
            this.label2.TabIndex = 32;
            this.label2.Text = "Comments";

            //
            // cbxTemplate
            //
            this.cbxTemplate.FormattingEnabled = true;
            this.cbxTemplate.Location = new System.Drawing.Point(497, 65);
            this.cbxTemplate.Name = "cbxTemplate";
            this.cbxTemplate.Size = new System.Drawing.Size(218, 32);
            this.cbxTemplate.TabIndex = 11;

            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(481, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 24);
            this.label1.TabIndex = 30;
            this.label1.Text = "News Paper Template";

            //
            // txtEmail
            //
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(205, 392);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(240, 29);
            this.txtEmail.TabIndex = 9;

            //
            // label16
            //
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(126, 30);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(51, 24);
            this.label16.TabIndex = 27;
            this.label16.Text = "Initial";

            //
            // txtTel
            //
            this.txtTel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTel.Location = new System.Drawing.Point(205, 321);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(240, 29);
            this.txtTel.TabIndex = 7;

            //
            // txtFax
            //
            this.txtFax.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFax.Location = new System.Drawing.Point(205, 356);
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(240, 29);
            this.txtFax.TabIndex = 8;

            //
            // textBox4
            //
            this.textBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(205, 279);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(240, 29);
            this.textBox4.TabIndex = 22;

            //
            // txtAdd3
            //
            this.txtAdd3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdd3.Location = new System.Drawing.Point(205, 286);
            this.txtAdd3.Name = "txtAdd3";
            this.txtAdd3.Size = new System.Drawing.Size(240, 29);
            this.txtAdd3.TabIndex = 6;

            //
            // txtAdd2
            //
            this.txtAdd2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdd2.Location = new System.Drawing.Point(205, 251);
            this.txtAdd2.Name = "txtAdd2";
            this.txtAdd2.Size = new System.Drawing.Size(240, 29);
            this.txtAdd2.TabIndex = 5;

            //
            // txtAdd1
            //
            this.txtAdd1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAdd1.Location = new System.Drawing.Point(205, 216);
            this.txtAdd1.Name = "txtAdd1";
            this.txtAdd1.Size = new System.Drawing.Size(240, 29);
            this.txtAdd1.TabIndex = 4;

            //
            // txtABN
            //
            this.txtABN.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtABN.Location = new System.Drawing.Point(205, 181);
            this.txtABN.Name = "txtABN";
            this.txtABN.Size = new System.Drawing.Size(240, 29);
            this.txtABN.TabIndex = 3;

            //
            // txtName
            //
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(205, 143);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(240, 29);
            this.txtName.TabIndex = 1;

            //
            // label17
            //
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(123, 394);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(57, 24);
            this.label17.TabIndex = 12;
            this.label17.Text = "Email";

            //
            // label18
            //
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(140, 359);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(42, 24);
            this.label18.TabIndex = 11;
            this.label18.Text = "Fax";

            //
            // label19
            //
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(74, 324);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(103, 24);
            this.label19.TabIndex = 10;
            this.label19.Text = "Telephone";

            //
            // label20
            //
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(32, 289);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(136, 24);
            this.label20.TabIndex = 9;
            this.label20.Text = "Address Line 3";

            //
            // label21
            //
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(26, 254);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(141, 24);
            this.label21.TabIndex = 8;
            this.label21.Text = "Address Line 2 ";

            //
            // label22
            //
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(26, 219);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(141, 24);
            this.label22.TabIndex = 7;
            this.label22.Text = "Address Line 1 ";

            //
            // label23
            //
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(133, 184);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(49, 24);
            this.label23.TabIndex = 6;
            this.label23.Text = "ABN";

            //
            // label24
            //
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(120, 145);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(61, 24);
            this.label24.TabIndex = 5;
            this.label24.Text = "Name";

            //
            // saveButton
            //
            this.saveButton.BackColor = System.Drawing.SystemColors.Control;
            this.saveButton.CornerRadius = 40;
            this.saveButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ForeColor = System.Drawing.Color.Blue;
            this.saveButton.Location = new System.Drawing.Point(516, 203);
            this.saveButton.Name = "saveButton";
            this.saveButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.saveButton.Size = new System.Drawing.Size(165, 40);
            this.saveButton.TabIndex = 140;
            this.saveButton.Text = "Save";
            this.saveButton.Click += new System.EventHandler(this.BtnSave_Click);

            //
            // deleteButton
            //
            this.deleteButton.BackColor = System.Drawing.SystemColors.Control;
            this.deleteButton.CornerRadius = 40;
            this.deleteButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deleteButton.ForeColor = System.Drawing.Color.Blue;
            this.deleteButton.Location = new System.Drawing.Point(516, 273);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.deleteButton.Size = new System.Drawing.Size(165, 40);
            this.deleteButton.TabIndex = 140;
            this.deleteButton.Text = "Delete";
            this.deleteButton.Click += new System.EventHandler(this.BtnDelete_Click);

            //
            // closeButton
            //
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.CornerRadius = 40;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Blue;
            this.closeButton.Location = new System.Drawing.Point(516, 343);
            this.closeButton.Name = "closeButton";
            this.closeButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.closeButton.Size = new System.Drawing.Size(165, 40);
            this.closeButton.TabIndex = 140;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.BtnClose_Click);

            //
            // FrmSubAgentDef
            //
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(836, 576);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblIns);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmSubAgentDef";
            this.Text = "Sub Agent Definition";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void Initiate()
        {
            try
            {
                connDB = new Connect();
                using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                {
                    conn.Open();

                    // Populate cbxSub
                    using (SqlCommand cmd = new SqlCommand("GetSubagents", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connDB.aTable = new DataTable();
                            adapter.Fill(connDB.aTable);
                            DataRow row = connDB.aTable.NewRow();
                            row["sub_id"] = 0;
                            row["name_short"] = "--New--";
                            connDB.aTable.Rows.InsertAt(row, 0);
                            cbxSub.DataSource = connDB.aTable;
                            cbxSub.DisplayMember = "name_short";
                            cbxSub.ValueMember = "sub_id";
                        }
                    }

                    // Populate cbxTemplate
                    using (SqlCommand cmd = new SqlCommand("GetTemplates", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connDB.aTable = new DataTable();
                            adapter.Fill(connDB.aTable);
                            cbxTemplate.DataSource = connDB.aTable;
                            cbxTemplate.DisplayMember = "template_name";
                            cbxTemplate.ValueMember = "paper_template_id";
                            if (connDB.aTable.Rows.Count > 0)
                                cbxTemplate.SelectedIndex = 0;
                        }
                    }

                    // Populate cbxAccount
                    using (SqlCommand cmd = new SqlCommand("GetAccounts", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connDB.aTable = new DataTable();
                            adapter.Fill(connDB.aTable);
                            cbxAccount.DataSource = connDB.aTable;
                            cbxAccount.DisplayMember = "acc_name";
                            cbxAccount.ValueMember = "acc_id";
                            for (int index = 0; index < connDB.aTable.Rows.Count; ++index)
                            {
                                if (connDB.aTable.Rows[index]["acc_name"].ToString() == "Sub Agent")
                                    cbxAccount.SelectedIndex = index;
                            }
                        }
                    }

                    // Populate cbxLink
                    using (SqlCommand cmd = new SqlCommand("GetAccountCards", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccId", cbxAccount.SelectedValue ?? 0);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connDB.aTable = new DataTable();
                            adapter.Fill(connDB.aTable);
                            cbxLink.DataSource = connDB.aTable;
                            cbxLink.DisplayMember = "name";
                            cbxLink.ValueMember = "card_id";
                        }
                    }

                    // Clear textboxes
                    txtName.Text = "";
                    txtABN.Text = "";
                    txtAdd1.Text = "";
                    txtAdd2.Text = "";
                    txtAdd3.Text = "";
                    txtTel.Text = "";
                    txtFax.Text = "";
                    txtEmail.Text = "";
                    txtComm.Text = "";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
{
    try
    {
        // Validate inputs
        if (string.IsNullOrWhiteSpace(txtName.Text))
        {
            MessageBox.Show("Name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!decimal.TryParse(txtRate.Text, out decimal cRate))
        {
            MessageBox.Show("Invalid commission rate.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (!int.TryParse(cbxNoWeeks.Text, out int noWeek))
        {
            MessageBox.Show("Invalid number of weeks.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        connDB = new Connect();
        using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
        {
            conn.Open();
            string procName = cbxSub.SelectedValue?.ToString() == "0" ? "InsertSubagent" : "UpdateSubagent";
            using (SqlCommand cmd = new SqlCommand(procName, conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (procName == "UpdateSubagent")
                    cmd.Parameters.AddWithValue("@SubId", cbxSub.SelectedValue);
                cmd.Parameters.AddWithValue("@AccId", cbxAccount.SelectedValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CardId", cbxLink.SelectedValue ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Name", connDB.AddBackslash(txtName.Text) ?? "");
                cmd.Parameters.AddWithValue("@NameShort", connDB.AddBackslash(cbxSub.Text) ?? "");
                cmd.Parameters.AddWithValue("@AddressLine1", connDB.AddBackslash(txtAdd1.Text) ?? "");
                cmd.Parameters.AddWithValue("@AddressLine2", connDB.AddBackslash(txtAdd2.Text) ?? "");
                cmd.Parameters.AddWithValue("@AddressLine3", connDB.AddBackslash(txtAdd3.Text) ?? "");
                cmd.Parameters.AddWithValue("@ABN", txtABN.Text ?? "");
                cmd.Parameters.AddWithValue("@Telephone", txtTel.Text ?? "");
                cmd.Parameters.AddWithValue("@Fax", txtFax.Text ?? "");
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text ?? "");
                cmd.Parameters.AddWithValue("@DefaultTemplateId", cbxTemplate.SelectedValue ?? "");
                cmd.Parameters.AddWithValue("@NoWeek", noWeek);
                cmd.Parameters.AddWithValue("@CRate", cRate);
                cmd.Parameters.AddWithValue("@Comments", connDB.AddBackslash(txtComm.Text) ?? "");
                cmd.ExecuteNonQuery();
            }
        }
        Initiate();
    }
    catch (SqlException ex)
    {
        MessageBox.Show($"Database error saving subagent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error saving subagent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string str = cbxSub.SelectedValue?.ToString() ?? "0";
                if (str == "0")
                    return;

                if (MessageBox.Show("Delete This Sub-Agent ?", "Sub-Agent Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    connDB = new Connect();
                    using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand("DeleteSubagent", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SubId", str);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Initiate();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error deleting subagent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting subagent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }

        private void CbxSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string str = cbxSub.SelectedValue?.ToString();
                if (str == "System.Data.DataRowView" || str == "0")
                {
                    Initiate();
                    return;
                }

                connDB = new Connect();
                using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetSubagentById", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubId", str);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connDB.aTable = new DataTable();
                            adapter.Fill(connDB.aTable);
                            if (connDB.aTable.Rows.Count > 0)
                            {
                                txtName.Text = connDB.aTable.Rows[0]["name"].ToString();
                                txtABN.Text = connDB.aTable.Rows[0]["ABN"].ToString();
                                txtAdd1.Text = connDB.aTable.Rows[0]["address_line1"].ToString();
                                txtAdd2.Text = connDB.aTable.Rows[0]["address_line2"].ToString();
                                txtAdd3.Text = connDB.aTable.Rows[0]["address_line3"].ToString();
                                txtTel.Text = connDB.aTable.Rows[0]["telephone"].ToString();
                                txtFax.Text = connDB.aTable.Rows[0]["fax"].ToString();
                                txtEmail.Text = connDB.aTable.Rows[0]["email"].ToString();
                                txtComm.Text = connDB.aTable.Rows[0]["comments"].ToString();
                                cbxTemplate.SelectedValue = connDB.aTable.Rows[0]["default_template_id"].ToString();
                                cbxNoWeeks.Text = connDB.aTable.Rows[0]["no_week"].ToString();
                                txtRate.Text = connDB.aTable.Rows[0]["c_rate"].ToString();
                                cbxAccount.SelectedValue = connDB.aTable.Rows[0]["acc_id"].ToString();
                                cbxLink.SelectedValue = connDB.aTable.Rows[0]["card_id"].ToString();
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading subagent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Initiate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subagent: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Initiate();
            }
        }
    }
}

