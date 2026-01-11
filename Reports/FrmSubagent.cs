using System;
using System.Drawing.Printing;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace QiPOS
{
    public sealed class FrmSubagent : Form
    {
        #region declarations

        private IContainer components;
        private DateTimePicker dptStart;
        private ListBox lbInvNo;
        private Label lblFun;
        private Panel pnlBtn;
        private Label lblTitle;
        private DataGridView dgInvoice;
        private Label lblSub;
        private ListBox cbxSub;
        private Timer tmLoad;
        private string queryStr;
        private Connect connDB;
        private Connect connTmp;
        private DataTable templateTB;
        private DataTable inputDataTB;
        private DataTable subSupDetails;
        private DateTime fromDate;
        private DateTime toDate;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomButton BtnMag;
        private CustomButton BtnDelete;
        private CustomButton BtnPrint;
        private CustomButton closeButton;
        private CustomButton BtnNew;
        private CustomButton paymentButton;
        private CustomButton customButton2;
        private CustomButton customButton3;
        private CustomButton customButton4;
        private bool cellEditHandler;

        #endregion declarations

        #region components 

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dptStart = new System.Windows.Forms.DateTimePicker();
            this.lbInvNo = new System.Windows.Forms.ListBox();
            this.lblFun = new System.Windows.Forms.Label();
            this.pnlBtn = new System.Windows.Forms.Panel();
            this.closeButton = new QiPOS.CustomButton();
            this.BtnPrint = new QiPOS.CustomButton();
            this.BtnNew = new QiPOS.CustomButton();
            this.customButton4 = new QiPOS.CustomButton();
            this.BtnDelete = new QiPOS.CustomButton();
            this.cbxSub = new System.Windows.Forms.ListBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgInvoice = new System.Windows.Forms.DataGridView();
            this.lblSub = new System.Windows.Forms.Label();
            this.tmLoad = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.customButton3 = new QiPOS.CustomButton();
            this.customButton2 = new QiPOS.CustomButton();
            this.paymentButton = new QiPOS.CustomButton();
            this.BtnMag = new QiPOS.CustomButton();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBtn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dptStart
            // 
            this.dptStart.CustomFormat = "dd MMM yy";
            this.dptStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dptStart.Location = new System.Drawing.Point(-1, 556);
            this.dptStart.Name = "dptStart";
            this.dptStart.Size = new System.Drawing.Size(160, 40);
            this.dptStart.TabIndex = 1;
            this.dptStart.ValueChanged += new System.EventHandler(this.DptStart_ValueChanged);
            // 
            // lbInvNo
            // 
            this.lbInvNo.FormattingEnabled = true;
            this.lbInvNo.ItemHeight = 33;
            this.lbInvNo.Location = new System.Drawing.Point(3, 603);
            this.lbInvNo.Name = "lbInvNo";
            this.lbInvNo.Size = new System.Drawing.Size(165, 202);
            this.lbInvNo.TabIndex = 3;
            this.lbInvNo.SelectedIndexChanged += new System.EventHandler(this.LbInvNo_SelectedIndexChanged);
            // 
            // lblFun
            // 
            this.lblFun.AutoSize = true;
            this.lblFun.Location = new System.Drawing.Point(21, 526);
            this.lblFun.Name = "lblFun";
            this.lblFun.Size = new System.Drawing.Size(145, 33);
            this.lblFun.TabIndex = 5;
            this.lblFun.Text = "Start Date";
            // 
            // pnlBtn
            // 
            this.pnlBtn.Controls.Add(this.closeButton);
            this.pnlBtn.Controls.Add(this.BtnPrint);
            this.pnlBtn.Controls.Add(this.BtnNew);
            this.pnlBtn.Controls.Add(this.customButton4);
            this.pnlBtn.Controls.Add(this.BtnDelete);
            this.pnlBtn.Controls.Add(this.lbInvNo);
            this.pnlBtn.Controls.Add(this.dptStart);
            this.pnlBtn.Controls.Add(this.cbxSub);
            this.pnlBtn.Controls.Add(this.lblFun);
            this.pnlBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBtn.Location = new System.Drawing.Point(1083, 54);
            this.pnlBtn.Name = "pnlBtn";
            this.pnlBtn.Size = new System.Drawing.Size(178, 949);
            this.pnlBtn.TabIndex = 25;
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.closeButton.CornerRadius = 40;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Blue;
            this.closeButton.Location = new System.Drawing.Point(3, 199);
            this.closeButton.Name = "closeButton";
            this.closeButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.closeButton.Size = new System.Drawing.Size(165, 40);
            this.closeButton.TabIndex = 139;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // BtnPrint
            // 
            this.BtnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnPrint.CornerRadius = 40;
            this.BtnPrint.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnPrint.ForeColor = System.Drawing.Color.Blue;
            this.BtnPrint.Location = new System.Drawing.Point(3, 150);
            this.BtnPrint.Name = "BtnPrint";
            this.BtnPrint.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnPrint.Size = new System.Drawing.Size(165, 40);
            this.BtnPrint.TabIndex = 139;
            this.BtnPrint.Text = "Print";
            this.BtnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // BtnNew
            // 
            this.BtnNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnNew.CornerRadius = 40;
            this.BtnNew.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnNew.ForeColor = System.Drawing.Color.Red;
            this.BtnNew.Location = new System.Drawing.Point(3, 3);
            this.BtnNew.Name = "BtnNew";
            this.BtnNew.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnNew.Size = new System.Drawing.Size(165, 40);
            this.BtnNew.TabIndex = 139;
            this.BtnNew.Text = "Existing";
            this.BtnNew.Click += new System.EventHandler(this.BtnNew_Click);
            // 
            // customButton4
            // 
            this.customButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.customButton4.CornerRadius = 40;
            this.customButton4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton4.ForeColor = System.Drawing.Color.Blue;
            this.customButton4.Location = new System.Drawing.Point(4, 52);
            this.customButton4.Name = "customButton4";
            this.customButton4.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton4.Size = new System.Drawing.Size(165, 40);
            this.customButton4.TabIndex = 139;
            this.customButton4.Text = "Save";
            this.customButton4.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnDelete.CornerRadius = 40;
            this.BtnDelete.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.ForeColor = System.Drawing.Color.Blue;
            this.BtnDelete.Location = new System.Drawing.Point(3, 101);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnDelete.Size = new System.Drawing.Size(165, 40);
            this.BtnDelete.TabIndex = 139;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // cbxSub
            // 
            this.cbxSub.FormattingEnabled = true;
            this.cbxSub.ItemHeight = 33;
            this.cbxSub.Location = new System.Drawing.Point(0, 255);
            this.cbxSub.Name = "cbxSub";
            this.cbxSub.Size = new System.Drawing.Size(175, 268);
            this.cbxSub.TabIndex = 25;
            this.cbxSub.SelectedIndexChanged += new System.EventHandler(this.CbxSub_SelectedIndexChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(138, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(257, 47);
            this.lblTitle.TabIndex = 28;
            this.lblTitle.Text = "New Invoice";
            // 
            // dgInvoice
            // 
            this.dgInvoice.AllowUserToAddRows = false;
            this.dgInvoice.AllowUserToDeleteRows = false;
            this.dgInvoice.AllowUserToResizeColumns = false;
            this.dgInvoice.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.dgInvoice.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgInvoice.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgInvoice.BackgroundColor = System.Drawing.Color.LightYellow;
            this.dgInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Blue;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgInvoice.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgInvoice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgInvoice.Location = new System.Drawing.Point(3, 54);
            this.dgInvoice.MultiSelect = false;
            this.dgInvoice.Name = "dgInvoice";
            this.dgInvoice.RowHeadersVisible = false;
            this.dgInvoice.RowHeadersWidth = 62;
            this.dgInvoice.RowTemplate.Height = 27;
            this.dgInvoice.Size = new System.Drawing.Size(1074, 949);
            this.dgInvoice.TabIndex = 31;
            this.dgInvoice.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DgData_EditingControlShowing);
            // 
            // lblSub
            // 
            this.lblSub.AutoSize = true;
            this.lblSub.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSub.ForeColor = System.Drawing.Color.Fuchsia;
            this.lblSub.Location = new System.Drawing.Point(3, 0);
            this.lblSub.Name = "lblSub";
            this.lblSub.Size = new System.Drawing.Size(129, 64);
            this.lblSub.TabIndex = 32;
            this.lblSub.Text = "Sub";
            // 
            // tmLoad
            // 
            this.tmLoad.Enabled = true;
            this.tmLoad.Tick += new System.EventHandler(this.TmLoad_Tick);
            // 
            // flowLayoutPanel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel1, 2);
            this.flowLayoutPanel1.Controls.Add(this.lblSub);
            this.flowLayoutPanel1.Controls.Add(this.lblTitle);
            this.flowLayoutPanel1.Controls.Add(this.customButton3);
            this.flowLayoutPanel1.Controls.Add(this.customButton2);
            this.flowLayoutPanel1.Controls.Add(this.paymentButton);
            this.flowLayoutPanel1.Controls.Add(this.BtnMag);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1264, 51);
            this.flowLayoutPanel1.TabIndex = 34;
            // 
            // customButton3
            // 
            this.customButton3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.customButton3.CornerRadius = 50;
            this.customButton3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton3.ForeColor = System.Drawing.Color.Blue;
            this.customButton3.Location = new System.Drawing.Point(401, 3);
            this.customButton3.Name = "customButton3";
            this.customButton3.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton3.Size = new System.Drawing.Size(121, 48);
            this.customButton3.TabIndex = 139;
            this.customButton3.Text = "Template";
            this.customButton3.Click += new System.EventHandler(this.BtnTemplate_Click);
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.customButton2.CornerRadius = 50;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Blue;
            this.customButton2.Location = new System.Drawing.Point(528, 3);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(121, 48);
            this.customButton2.TabIndex = 139;
            this.customButton2.Text = "Subagent";
            this.customButton2.Click += new System.EventHandler(this.BtnSubAgent_Click);
            // 
            // paymentButton
            // 
            this.paymentButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.paymentButton.CornerRadius = 50;
            this.paymentButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentButton.ForeColor = System.Drawing.Color.Blue;
            this.paymentButton.Location = new System.Drawing.Point(655, 3);
            this.paymentButton.Name = "paymentButton";
            this.paymentButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.paymentButton.Size = new System.Drawing.Size(121, 48);
            this.paymentButton.TabIndex = 139;
            this.paymentButton.Text = "Payment";
            this.paymentButton.Click += new System.EventHandler(this.BtnMag_Click);
            // 
            // BtnMag
            // 
            this.BtnMag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnMag.CornerRadius = 50;
            this.BtnMag.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnMag.ForeColor = System.Drawing.Color.Blue;
            this.BtnMag.Location = new System.Drawing.Point(782, 3);
            this.BtnMag.Name = "BtnMag";
            this.BtnMag.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnMag.Size = new System.Drawing.Size(121, 48);
            this.BtnMag.TabIndex = 139;
            this.BtnMag.Text = "Magazine";
            this.BtnMag.Click += new System.EventHandler(this.BtnMag_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dgInvoice, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlBtn, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1264, 1006);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // FrmSubagent
            // 
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(1264, 1006);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmSubagent";
            this.Text = "Sub Paper";
            this.Load += new System.EventHandler(this.FrmPaper_Load);
            this.pnlBtn.ResumeLayout(false);
            this.pnlBtn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgInvoice)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion components

        public FrmSubagent()
        {
            this.fromDate = new DateTime(2099, 12, 31);
            this.toDate = new DateTime(1900, 1, 1);
            this.cellEditHandler = true;
            this.InitializeComponent();
            this.Initiate();
        }
        private void Initiate()
        {
            try
            {
                connDB = new Connect();
                using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetSubagentWithName", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);
                            cbxSub.DataSource = dt;
                            cbxSub.DisplayMember = "name";
                            cbxSub.ValueMember = "sub_id";
                        }
                    }
                }
                cbxSub.SelectedIndex = 0;
                LoadInvoiceList();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading subagents: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing subagent form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoiceList()
        {
            try
            {
                if (cbxSub.SelectedValue == null || Convert.ToInt32(cbxSub.SelectedValue) == 0)
                {
                    lbInvNo.DataSource = null;
                    return;
                }

                Connect connect = new Connect();
                using (var conn = new SqlConnection(connect.ConnectionStr))
                { 
                using (var cmd = new SqlCommand("GetSubInvoiceSummaries",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SubId", Convert.ToInt32(cbxSub.SelectedValue));
                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        var table = new DataTable();
                        adapter.Fill(table);
                        lbInvNo.DataSource = table;
                        lbInvNo.DisplayMember = "inv_date";
                        lbInvNo.ValueMember = "sub_inv_id";
                    }
                }
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading invoice summaries: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lbInvNo.DataSource = null;
            }
        }
        private void LoadSheet()
        {
            try
            {
                dgInvoice.BackgroundColor = Color.Azure;

                // Initialize inputDataTB schema
                inputDataTB = new DataTable();
                inputDataTB.Columns.Add("sub_inv_paper_id", typeof(int));
                inputDataTB.Columns.Add("sub_inv_id", typeof(int));
                inputDataTB.Columns.Add("row_title", typeof(string));
                inputDataTB.Columns.Add("row_price", typeof(decimal));
                inputDataTB.Columns.Add("col_1_supply", typeof(string));
                inputDataTB.Columns.Add("col_1_return", typeof(string));
                inputDataTB.Columns.Add("col_1_date", typeof(DateTime));
                inputDataTB.Columns.Add("col_1_stock_id", typeof(int));
                inputDataTB.Columns.Add("col_2_supply", typeof(string));
                inputDataTB.Columns.Add("col_2_return", typeof(string));
                inputDataTB.Columns.Add("col_2_date", typeof(DateTime));
                inputDataTB.Columns.Add("col_2_stock_id", typeof(int));
                inputDataTB.Columns.Add("col_3_supply", typeof(string));
                inputDataTB.Columns.Add("col_3_return", typeof(string));
                inputDataTB.Columns.Add("col_3_date", typeof(DateTime));
                inputDataTB.Columns.Add("col_3_stock_id", typeof(int));
                inputDataTB.Columns.Add("col_4_supply", typeof(string));
                inputDataTB.Columns.Add("col_4_return", typeof(string));
                inputDataTB.Columns.Add("col_4_date", typeof(DateTime));
                inputDataTB.Columns.Add("col_4_stock_id", typeof(int));
                inputDataTB.Columns.Add("col_5_supply", typeof(string));
                inputDataTB.Columns.Add("col_5_return", typeof(string));
                inputDataTB.Columns.Add("col_5_date", typeof(DateTime));
                inputDataTB.Columns.Add("col_5_stock_id", typeof(int));
                inputDataTB.Columns.Add("row_fee", typeof(decimal));

                // Get sub_inv_id
                int subInvId = lbInvNo.SelectedValue != null ? Convert.ToInt32(lbInvNo.SelectedValue) : 0;
                Connect conn = new Connect();
                // Fetch paper details
                using (var connect = new SqlConnection(conn.ConnectionStr))
                {
                    using (var cmd = new SqlCommand("GetSubInvoicePaperDetails", connect))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@SubInvId", subInvId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            subSupDetails = new DataTable();
                            adapter.Fill(subSupDetails);
                        }
                    }
                }

                DateTime[] dateTimeArray = new DateTime[5];
                if (subSupDetails.Rows.Count > 0)
                {
                    DataRow row1 = inputDataTB.NewRow();
                    DataRow row2 = inputDataTB.NewRow();
                    for (int index = 0; index < 5; ++index)
                    {
                        dateTimeArray[index] = (DateTime)subSupDetails.Rows[0][index * 4 + 6];
                        row1[index * 4 + 4] = dateTimeArray[index].ToString("ddd");
                        row1["sub_inv_paper_id"] = 0;
                        row2[index * 4 + 4] = dateTimeArray[index].ToString("dd/MM");
                        row2[index * 4 + 5] = "RT";
                        row2["sub_inv_paper_id"] = 0;
                    }
                    inputDataTB.Rows.Add(row1);
                    inputDataTB.Rows.Add(row2);
                }

                for (int index1 = 0; index1 < subSupDetails.Rows.Count; index1++)
                {
                    bool flag = false;
                    for (int i = 0; i < 5; i++)
                    {
                        if (dateTimeArray[i].CompareTo((DateTime)subSupDetails.Rows[index1][i * 4 + 6]) != 0)
                            flag = true;
                        dateTimeArray[i] = (DateTime)subSupDetails.Rows[index1][i * 4 + 6];
                    }
                    if (flag)
                    {
                        DataRow row1 = inputDataTB.NewRow();
                        DataRow row2 = inputDataTB.NewRow();
                        for (int index2 = 0; index2 < 5; index2++)
                        {
                            row1[index2 * 4 + 4] = dateTimeArray[index2].ToString("ddd");
                            row1["sub_inv_paper_id"] = 0;
                            row2[index2 * 4 + 4] = dateTimeArray[index2].ToString("dd/MM");
                            row2[index2 * 4 + 5] = "RT";
                            row2["sub_inv_paper_id"] = 0;
                        }
                        inputDataTB.Rows.Add(row1);
                        inputDataTB.Rows.Add(row2);
                    }
                    DataRow row = inputDataTB.NewRow();
                    row["sub_inv_paper_id"] = subSupDetails.Rows[index1]["sub_inv_paper_id"];
                    row["sub_inv_id"] = subSupDetails.Rows[index1]["sub_inv_id"];
                    row["row_title"] = subSupDetails.Rows[index1]["row_title"];
                    row["row_price"] = subSupDetails.Rows[index1]["row_price"];
                    row["col_1_supply"] = subSupDetails.Rows[index1]["col_1_supply"].ToString();
                    row["col_1_return"] = subSupDetails.Rows[index1]["col_1_return"].ToString();
                    row["col_1_date"] = subSupDetails.Rows[index1]["col_1_date"];
                    row["col_1_stock_id"] = subSupDetails.Rows[index1]["col_1_stock_id"];
                    row["col_2_supply"] = subSupDetails.Rows[index1]["col_2_supply"].ToString();
                    row["col_2_return"] = subSupDetails.Rows[index1]["col_2_return"].ToString();
                    row["col_2_date"] = subSupDetails.Rows[index1]["col_2_date"];
                    row["col_2_stock_id"] = subSupDetails.Rows[index1]["col_2_stock_id"];
                    row["col_3_supply"] = subSupDetails.Rows[index1]["col_3_supply"].ToString();
                    row["col_3_return"] = subSupDetails.Rows[index1]["col_3_return"].ToString();
                    row["col_3_date"] = subSupDetails.Rows[index1]["col_3_date"];
                    row["col_3_stock_id"] = subSupDetails.Rows[index1]["col_3_stock_id"];
                    row["col_4_supply"] = subSupDetails.Rows[index1]["col_4_supply"].ToString();
                    row["col_4_return"] = subSupDetails.Rows[index1]["col_4_return"].ToString();
                    row["col_4_date"] = subSupDetails.Rows[index1]["col_4_date"];
                    row["col_4_stock_id"] = subSupDetails.Rows[index1]["col_4_stock_id"];
                    row["col_5_supply"] = subSupDetails.Rows[index1]["col_5_supply"].ToString();
                    row["col_5_return"] = subSupDetails.Rows[index1]["col_5_return"].ToString();
                    row["col_5_date"] = subSupDetails.Rows[index1]["col_5_date"];
                    row["col_5_stock_id"] = subSupDetails.Rows[index1]["col_5_stock_id"];
                    row["row_fee"] = subSupDetails.Rows[index1]["row_fee"];
                    inputDataTB.Rows.Add(row);
                }

                dgInvoice.DataSource = inputDataTB;

                // Apply grid formatting
                for (int i = 0; i < dgInvoice.Rows.Count; ++i)
                {
                    if (dgInvoice.Rows[i].Cells["sub_inv_paper_id"].Value != null && dgInvoice.Rows[i].Cells["sub_inv_paper_id"].Value.ToString() == "0")
                    {
                        dgInvoice.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                        dgInvoice.Rows[i].DefaultCellStyle.ForeColor = Color.MediumBlue;
                        dgInvoice.Rows[i].ReadOnly = true;
                    }
                }
                dgInvoice.Columns["row_title"].ReadOnly = true;
                dgInvoice.Columns["row_price"].DefaultCellStyle.Format = "C";
                dgInvoice.Columns["row_title"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgInvoice.Columns["row_title"].DefaultCellStyle.ForeColor = Color.MediumBlue;
                dgInvoice.Columns["row_price"].DefaultCellStyle.ForeColor = Color.MediumBlue;
                for (int index = 4; index < dgInvoice.Columns.Count; ++index)
                    dgInvoice.Columns[index].CellTemplate = new DataGridViewSelectedCell();
                dgInvoice.Columns["sub_inv_paper_id"].Visible = false;
                dgInvoice.Columns["sub_inv_id"].Visible = false;
                for (int i = 0; i < dgInvoice.Rows.Count; ++i)
                {
                    for (int j = 0; j < 5; ++j)
                    {
                        if (dgInvoice.Rows[i].Cells[j * 4 + 4].Value.ToString() == "0")
                        {
                            dgInvoice.Rows[i].Cells[j * 4 + 4].Value = "";
                        }
                        else
                        {
                            if (int.TryParse(dgInvoice.Rows[i].Cells[j * 4 + 4].Value.ToString(), out int result))
                                dgInvoice.Rows[i].Cells[j * 4 + 4].Selected = true;
                        }
                        if (dgInvoice.Rows[i].Cells[j * 4 + 5].Value.ToString() == "0")
                            dgInvoice.Rows[i].Cells[j * 4 + 5].Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sheet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgInvoice.DataSource = null;
            }
        }

        /// <summary>
        /// only allow cycles of 1 week for subagents
        /// </summary>
        private void NewSheet()
        {
            try
            {
                dgInvoice.BackgroundColor = Color.LightYellow;
                int templateId = 0; 
                // Get default_template_id
                Connect connect = new Connect();
                using (var connection = new SqlConnection(connect.ConnectionStr))
                {
                    if (!string.IsNullOrEmpty(cbxSub.Text))
                    {
                        using (var cmd = new SqlCommand("GetSubAgentTemplateId", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@NameShort", cbxSub.Text);
                            using (var adapter = new SqlDataAdapter(cmd))
                            {
                                var table = new DataTable();
                                adapter.Fill(table);
                                if (table.Rows.Count > 0)
                                {
                                    templateId = Convert.ToInt32(table.Rows[0]["default_template_id"]);
                                }
                            }
                        }
                    }

                    // Get template details
                    using (var cmd = new SqlCommand("GetTemplateDetailsAdvanced", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PaperTemplateId", templateId);
                        using (var adapter = new SqlDataAdapter(cmd))
                        {
                            templateTB = new DataTable();
                            adapter.Fill(templateTB);
                            inputDataTB = templateTB.Clone();
                            if (templateTB.Rows.Count > 0)
                            {
                                EmptyTemplateInitiate();
                            }
                        }
                    }

                }
                dgInvoice.DataSource = inputDataTB;

                // Apply grid formatting
                for (int i = 0; i < dgInvoice.Rows.Count; i++)
                {
                    if (dgInvoice.Rows[i].Cells["dgContentId"].Value != null && dgInvoice.Rows[i].Cells["dgContentId"].Value.ToString() == "0")
                    {
                        dgInvoice.Rows[i].DefaultCellStyle.BackColor = SystemColors.Control;
                        dgInvoice.Rows[i].DefaultCellStyle.ForeColor = Color.MediumBlue;
                        dgInvoice.Rows[i].ReadOnly = true;
                    }
                }
                dgInvoice.Columns["row_title"].ReadOnly = true;
                dgInvoice.Columns["row_price"].DefaultCellStyle.Format = "C";
                dgInvoice.Columns["row_title"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dgInvoice.Columns["row_title"].DefaultCellStyle.ForeColor = Color.MediumBlue;
                dgInvoice.Columns["row_price"].DefaultCellStyle.ForeColor = Color.MediumBlue;
                for (int index = 0; index < 5; ++index)
                {
                    dgInvoice.Columns[4 + 5 * index].CellTemplate = new DataGridViewSelectedCell();
                    dgInvoice.Columns[5 + 5 * index].CellTemplate = new DataGridViewSelectedCell();
                }
                dgInvoice.ColumnHeadersVisible = false;
                dgInvoice.Columns["dgContentId"].Visible = false;
                dgInvoice.Columns["col_1_day"].Visible = false;
                dgInvoice.Columns["col_1_date"].Visible = false;
                dgInvoice.Columns["col_1_stock_id"].Visible = false;
                dgInvoice.Columns["col_2_day"].Visible = false;
                dgInvoice.Columns["col_2_date"].Visible = false;
                dgInvoice.Columns["col_2_stock_id"].Visible = false;
                dgInvoice.Columns["col_3_day"].Visible = false;
                dgInvoice.Columns["col_3_date"].Visible = false;
                dgInvoice.Columns["col_3_stock_id"].Visible = false;
                dgInvoice.Columns["col_4_day"].Visible = false;
                dgInvoice.Columns["col_4_date"].Visible = false;
                dgInvoice.Columns["col_4_stock_id"].Visible = false;
                dgInvoice.Columns["col_5_day"].Visible = false;
                dgInvoice.Columns["col_5_date"].Visible = false;
                dgInvoice.Columns["col_5_stock_id"].Visible = false;
                dgInvoice.Columns["row_fee"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading new sheet: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dgInvoice.DataSource = null;
            }
        }


        private void EmptyTemplateInitiate()
        {
            int index1 = -1;
            int pos = -1;
            for (int i = 0; i < this.templateTB.Rows.Count; i++)
            {
                if (this.templateTB.Rows[i]["dgContentId"].ToString() == "1")
                {
                    index1 = i;
                    break;
                }
            }

            for (int i = 0; i < this.templateTB.Rows.Count; i++)
            {
                if (this.templateTB.Rows[i]["dgContentId"].ToString() == "2")
                {
                    pos = i;
                    if (index1 > -1)
                    {
                        pos += 2;
                        break;
                    }
                    else
                        break;
                }
            }

            this.templateTB = this.connDB.aTable.Copy();
            DateTime dateTime1 = this.dptStart.Value;
            int num1 = (int)dateTime1.DayOfWeek;
            if (num1 == 0)
                num1 = 7;

            this.fromDate = dateTime1.AddDays((double)(1 - num1));
            this.toDate = dateTime1.AddDays((double)((int)7 - num1));
            DateTime dateTime2 = dateTime1.AddDays((double)(1 - num1));
            DateTime dateTime4;
            if (index1 > -1)
            {
                DataRow row1 = this.templateTB.NewRow();
                DataRow row2 = this.templateTB.NewRow();
                row1["dgContentId"] = 0;
                row2["dgContentId"] = 0;
                for (int index3 = 0; index3 < 5; ++index3)
                {
                    short num2 = Convert.ToInt16(this.templateTB.Rows[index1][index3 * 5 + 3].ToString());
                    bool flag = true;
                    int num3 = pos - 2;
                    if (pos == -1)
                        num3 = this.connDB.aTable.Rows.Count;
                    for (int index4 = 0; index4 < num3; ++index4)
                    {
                        this.templateTB.Rows[index4][index3 * 5 + 4] = "";
                        DataRow dataRow = this.templateTB.Rows[index4];
                        int index5 = index3 * 5 + 6;
                        dateTime4 = dateTime2.AddDays((double)((int)num2 - 1));
                        string str = dateTime4.ToString("yyyy-MM-dd");
                        dataRow[index5] = str;
                        if (this.templateTB.Rows[index4][index3 * 5 + 7].ToString() != "0")
                            flag = false;
                    }
                    if (!flag)
                    {
                        DataRow dataRow1 = row2;
                        int index4 = index3 * 5 + 4;
                        dateTime4 = dateTime2.AddDays((double)((int)num2 - 1));
                        string str1 = dateTime4.ToString("ddd");
                        dataRow1[index4] = str1;
                        DataRow dataRow2 = row1;
                        int index5 = index3 * 5 + 4;
                        dateTime4 = dateTime2.AddDays((double)((int)num2 - 1));
                        string str2 = dateTime4.ToString("dd/MM");
                        dataRow2[index5] = str2;
                        row2[index3 * 5 + 5] = "RT";
                    }
                }
                this.templateTB.Rows.InsertAt(row1, 0);
                this.templateTB.Rows.InsertAt(row2, 0);
            }

            if (pos > -1)
            {
                DataRow row1 = this.templateTB.NewRow();
                DataRow row2 = this.templateTB.NewRow();
                row1["dgContentId"] = 0;
                row2["dgContentId"] = 0;
                for (int index3 = 0; index3 < 5; ++index3)
                {
                    dateTime2 = new DateTime(dateTime2.Ticks);
                    short num2 = Convert.ToInt16(this.templateTB.Rows[pos][index3 * 5 + 3].ToString());
                    bool flag = true;
                    int num3 = this.connDB.aTable.Rows.Count + 2;
                    if (index1 == -1)
                        num3 = this.connDB.aTable.Rows.Count;
                    for (int index4 = pos; index4 < num3; ++index4)
                    {
                        this.templateTB.Rows[index4][index3 * 5 + 4] = "";
                        DataRow dataRow = this.templateTB.Rows[index4];
                        int index5 = index3 * 5 + 6;
                        dateTime4 = dateTime2.AddDays((double)((int)num2 - 1));
                        string str = dateTime4.ToString("yyyy-MM-dd");
                        dataRow[index5] = str;
                        if (this.templateTB.Rows[index4][index3 * 5 + 7].ToString() != "0")
                            flag = false;
                    }
                    if (!flag)
                    {
                        DataRow dataRow1 = row1;
                        int index4 = index3 * 5 + 4;
                        dateTime4 = dateTime2.AddDays((double)((int)num2 - 1));
                        string dayOfWeek = dateTime4.ToString("ddd");
                        dataRow1[index4] = dayOfWeek;
                        DataRow dataRow2 = row2;
                        dateTime4 = dateTime2.AddDays((double)((int)num2 - 1));
                        string date = dateTime4.ToString("dd/MM");
                        dataRow2[index4] = date;
                        row1[index3 * 5 + 5] = "RT";
                    }
                }

                this.templateTB.Rows.InsertAt(row1, pos);
                this.templateTB.Rows.InsertAt(row2, pos + 1);
            }

            foreach (DataRow row in this.templateTB.Rows)
            {
                this.inputDataTB.ImportRow(row);
            }

        }

        private void CbxSub_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.BtnNew.Text == "New")
            {
                this.dgInvoice.Visible = true;
            }
            if (this.BtnNew.Text == "New")
            {
                this.LoadInvoiceList();
                if (this.lbInvNo.SelectedValue != null)
                { this.BtnMag.Visible = true; }
                else
                { this.BtnMag.Visible = false; }
                this.LoadSheet();
            }
            else
            {
                this.NewSheet();
            }
            this.lblSub.Text = this.cbxSub.GetItemText(this.cbxSub.SelectedItem);
            this.dgInvoice.Visible = true;
            this.lblTitle.Visible = true;
        }


        private void DptStart_ValueChanged(object sender, EventArgs e)
        {
            this.NewSheet();
            this.dgInvoice.Visible = true;
            this.lblTitle.Visible = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int invNo = 0;
                int subId = cbxSub.SelectedValue != null ? Convert.ToInt32(cbxSub.SelectedValue) : 0;
                if (lbInvNo.SelectedValue != null)
                    invNo = Convert.ToInt32(lbInvNo.SelectedValue);

                Connect conn= new Connect();
                using (var connect = new SqlConnection(conn.ConnectionStr))
                {
                    if (lblTitle.Text == "New Invoice" && subId != 0)
                    {
                        DateTime invDate = toDate.AddDays(1.0);
                        // Insert new invoice summary
                        using (var cmd = new SqlCommand("InsertSubInvoiceSummary", connect))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@SubId", subId);
                            cmd.Parameters.AddWithValue("@InvDate", invDate);
                            cmd.Parameters.AddWithValue("@FromDate", fromDate);
                            cmd.Parameters.AddWithValue("@ToDate", toDate);
                            cmd.Parameters.Add("@NewInvId", SqlDbType.Int).Direction = ParameterDirection.Output;
                            cmd.ExecuteNonQuery();
                            invNo = (int)cmd.Parameters["@NewInvId"].Value;
                        }

                        CopyNewsheetToPaperDetailTB(invNo);
                        BtnNew.Text = "New";
                        lblTitle.Text = "Existing Invoice";
                        BtnMag.Visible = true;
                        dgInvoice.Visible = false;
                    }
                    else if (subId != 0 && invNo != 0)
                    {
                        for (int index = inputDataTB.Rows.Count - 1; index >= 0; --index)
                        {
                            if (inputDataTB.Rows[index][0].ToString() == "0")
                            {
                                inputDataTB.Rows.Remove(inputDataTB.Rows[index]);
                            }
                        }
                        for (int index1 = 0; index1 < subSupDetails.Rows.Count; ++index1)
                        {
                            subSupDetails.Rows[index1][3] = inputDataTB.Rows[index1][3];
                            for (int index2 = 0; index2 < 5; ++index2)
                            {
                                int num2 = !string.IsNullOrEmpty(inputDataTB.Rows[index1][4 * index2 + 4].ToString())
                                    ? Convert.ToInt32(inputDataTB.Rows[index1][4 * index2 + 4].ToString()) : 0;
                                subSupDetails.Rows[index1][4 * index2 + 4] = num2;
                                int num3 = !string.IsNullOrEmpty(inputDataTB.Rows[index1][4 * index2 + 5].ToString())
                                    ? Convert.ToInt32(inputDataTB.Rows[index1][4 * index2 + 5].ToString()) : 0;
                                subSupDetails.Rows[index1][4 * index2 + 5] = num3;
                            }
                        }
                        conn.UpdateTable(subSupDetails.GetChanges());
                    }
                }

                LoadInvoiceList();
                lbInvNo.SelectedValue = invNo;
                LoadSheet();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                connDB.Close();
            }
        }
        private void CopyNewsheetToPaperDetailTB(int invNo)
        {
            try
            {
                // Initialize subSupDetails schema programmatically
                subSupDetails = new DataTable();
                subSupDetails.Columns.Add("sub_inv_paper_id", typeof(int));
                subSupDetails.Columns.Add("sub_inv_id", typeof(int));
                subSupDetails.Columns.Add("row_title", typeof(string));
                subSupDetails.Columns.Add("row_price", typeof(decimal));
                subSupDetails.Columns.Add("col_1_supply", typeof(int));
                subSupDetails.Columns.Add("col_1_return", typeof(int));
                subSupDetails.Columns.Add("col_1_date", typeof(DateTime));
                subSupDetails.Columns.Add("col_1_stock_id", typeof(int));
                subSupDetails.Columns.Add("col_2_supply", typeof(int));
                subSupDetails.Columns.Add("col_2_return", typeof(int));
                subSupDetails.Columns.Add("col_2_date", typeof(DateTime));
                subSupDetails.Columns.Add("col_2_stock_id", typeof(int));
                subSupDetails.Columns.Add("col_3_supply", typeof(int));
                subSupDetails.Columns.Add("col_3_return", typeof(int));
                subSupDetails.Columns.Add("col_3_date", typeof(DateTime));
                subSupDetails.Columns.Add("col_3_stock_id", typeof(int));
                subSupDetails.Columns.Add("col_4_supply", typeof(int));
                subSupDetails.Columns.Add("col_4_return", typeof(int));
                subSupDetails.Columns.Add("col_4_date", typeof(DateTime));
                subSupDetails.Columns.Add("col_4_stock_id", typeof(int));
                subSupDetails.Columns.Add("col_5_supply", typeof(int));
                subSupDetails.Columns.Add("col_5_return", typeof(int));
                subSupDetails.Columns.Add("col_5_date", typeof(DateTime));
                subSupDetails.Columns.Add("col_5_stock_id", typeof(int));
                subSupDetails.Columns.Add("row_fee", typeof(decimal));

                // Copy data from inputDataTB
                for (int index1 = 0; index1 < inputDataTB.Rows.Count; ++index1)
                {
                    if (inputDataTB.Rows[index1]["dgContentId"].ToString() != "0")
                    {
                        DataRow row = subSupDetails.NewRow();
                        row["sub_inv_id"] = invNo;
                        row["row_title"] = inputDataTB.Rows[index1]["row_title"];
                        row["row_price"] = inputDataTB.Rows[index1]["row_price"];
                        row["row_fee"] = inputDataTB.Rows[index1]["row_fee"];
                        for (int index2 = 0; index2 < 5; ++index2)
                        {
                            int num1 = !string.IsNullOrEmpty(inputDataTB.Rows[index1][4 + 5 * index2].ToString().Trim())
                                ? Convert.ToInt32(inputDataTB.Rows[index1][4 + 5 * index2].ToString().Trim()) : 0;
                            row[index2 * 4 + 4] = num1;
                            int num2 = !string.IsNullOrEmpty(inputDataTB.Rows[index1][5 + 5 * index2].ToString().Trim())
                                ? Convert.ToInt32(inputDataTB.Rows[index1][5 + 5 * index2].ToString().Trim()) : 0;
                            row[index2 * 4 + 5] = num2;
                            row[index2 * 4 + 6] = inputDataTB.Rows[index1][6 + 5 * index2];
                            row[index2 * 4 + 7] = inputDataTB.Rows[index1][7 + 5 * index2];
                        }
                        subSupDetails.Rows.Add(row);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error copying newsheet to paper details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                subSupDetails = null;
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            try
            {
                dgInvoice.Visible = true;
                lblTitle.Visible = true;

                if (BtnNew.Text == "New")
                {
                    BtnNew.Text = "Existing";
                    lblTitle.Text = "New Invoice";
                    lblTitle.ForeColor = Color.Red;
                    dgInvoice.Visible = true;
                    BtnMag.Visible = false;
                    BtnPrint.Enabled = false;
                    BtnDelete.Enabled = false;
                    NewSheet();

                    int subId = cbxSub.SelectedValue != null ? Convert.ToInt32(cbxSub.SelectedValue) : 0;
                    if (subId != 0)
                    {
                        DataTable paperDetailsTable;
                        Connect conn = new Connect();
                        using (var connect = new SqlConnection(conn.ConnectionStr))
                        {
                            // Get latest sub_inv_id
                            int subInvId;
                            using (var cmd = new SqlCommand("GetMaxSubInvoiceId", connect))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@SubId", subId);
                                object result = cmd.ExecuteScalar();
                                subInvId = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                            }

                            // Get paper details for latest invoice
                            using (var cmd = new SqlCommand("GetLatestSubInvoicePaperDetails", connect))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@SubId", subId);
                                using (var adapter = new SqlDataAdapter(cmd))
                                {
                                    paperDetailsTable = new DataTable();
                                    adapter.Fill(paperDetailsTable);
                                }
                            }
                        }

                        // Update dgInvoice with paper details
                        for (int i = 0; i < paperDetailsTable.Rows.Count; i++)
                        {
                            foreach (DataGridViewRow row in dgInvoice.Rows)
                            {
                                if (row.Cells["row_title"].Value?.ToString() == paperDetailsTable.Rows[i]["row_title"].ToString())
                                {
                                    DateTime gridDate = DateTime.Parse(row.Cells["col_1_date"].Value.ToString());
                                    DateTime tableDate = (DateTime)paperDetailsTable.Rows[i]["col_1_date"];
                                    if (gridDate.DayOfWeek == tableDate.DayOfWeek)
                                    {
                                        row.Cells[4].Value = paperDetailsTable.Rows[i]["col_1_supply"];
                                        row.Cells[9].Value = paperDetailsTable.Rows[i]["col_2_supply"];
                                        row.Cells[14].Value = paperDetailsTable.Rows[i]["col_3_supply"];
                                        row.Cells[19].Value = paperDetailsTable.Rows[i]["col_4_supply"];
                                        row.Cells[24].Value = paperDetailsTable.Rows[i]["col_5_supply"];
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    BtnNew.Text = "New";
                    lblTitle.Text = "";
                    lblTitle.ForeColor = Color.MediumBlue;
                    dgInvoice.Visible = false;
                    LoadInvoiceList();
                    if (lbInvNo.Items.Count > 0)
                    {
                        BtnMag.Visible = true;
                    }
                    BtnPrint.Enabled = true;
                    BtnDelete.Enabled = true;
                    LoadSheet();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing new invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LbInvNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dgInvoice.Visible = true;
            this.LoadSheet();
        }

        private void FrmPaper_Load(object sender, EventArgs e)
        {
            this.NewSheet();
        }

        /// <summary>
        /// Does not need to modify this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.ForeColor = Color.Red;
            if (!this.cellEditHandler)
            {
                return;
            }
            e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
            this.cellEditHandler = false;
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int num = (int)e.KeyChar;
                if (num >= 46 && num <= 57 && num != 47)
                {
                    return;
                }
                e.Handled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Edit Datagridview Cell Exception: " + (ex.Message).ToString());
            }
        }

        /// <summary>
        /// Extract method 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnMag_Click(object sender, EventArgs e)
        {
            decimal in_rate = 12.5m; // Default value (125 with 1 decimal place)
            try
            {
                Connect connect = new Connect();
                using (var connection = new SqlConnection(connect.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetSubAgentRate", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubId", cbxSub.SelectedValue);

                        var result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            in_rate = Convert.ToDecimal(result);
                        }
                    }
                }
                if (lbInvNo.SelectedValue == null)
                {
                    FrmMag frmMagEmpty = new FrmMag(0, in_rate);
                    AddOwnedForm(frmMagEmpty);
                    frmMagEmpty.Show();
                }
                else
                {
                    FrmMag frmMag = new FrmMag((int)lbInvNo.SelectedValue, in_rate);
                    AddOwnedForm(frmMag);
                    frmMag.Show();
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening magazine form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void PrintRaw()
        {
            Decimal in_rate = new Decimal(125, 0, 0, false, (byte)1);
            FrmMag frmMag = new FrmMag((int)this.lbInvNo.SelectedValue, in_rate);

            var printer = new GridPrinter(dgInvoice, "Subagent Paper Report");
            printer.MagazineGrid = frmMag.GetGrid();
            printer.Line1 = cbxSub.Text;
            printer.Line2 = DateTime.Today.ToString("dd/MM/yyyy");
            printer.Print();
            frmMag.Close();
        }

        /// <summary>
        /// does not need to modify this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            //this.dgInvoice.Visible = true;
            //string in_invNo = "0";
            //string in_subId = "0";
            //if (this.cbxSub.SelectedValue != null)
            //{ in_subId = this.cbxSub.SelectedValue.ToString(); }
            //if (this.lbInvNo.SelectedValue != null)
            //{ in_invNo = this.lbInvNo.SelectedValue.ToString(); }
            //(new FrmSubInvPrint(in_subId, in_invNo)).Show();

            PrintRaw();
        }

        /// <summary>
        /// does not need to modify this
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (lbInvNo.SelectedValue == null ||
                MessageBox.Show("Delete Invoice Now ?", "Invoice Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Connect connect = new Connect();    
                using (var connection = new SqlConnection(connect.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("DeleteInvoice", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SubInvId", lbInvNo.SelectedValue);

                        command.ExecuteNonQuery();
                    }
                }

                LoadInvoiceList();
                LoadSheet();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting invoice: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnTemplate_Click(object sender, EventArgs e)
        {
            (new FrmPaperTemplate()).Show();
        }

        private void BtnSubAgent_Click(object sender, EventArgs e)
        {
            (new FrmSubagentDetails()).Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //(new FrmCheck()).Show();
        }

        private void TmLoad_Tick(object sender, EventArgs e)
        {
            this.BtnNew.Text = "New";
            this.lblTitle.Text = "Existing Invoice";
            this.lblTitle.ForeColor = Color.MediumBlue;
            this.LoadInvoiceList();
            if (this.lbInvNo.Items.Count > 0)
            {
                this.BtnMag.Visible = true;
            }
            this.BtnPrint.Enabled = true;
            this.BtnDelete.Enabled = true;
            this.LoadSheet();
            this.tmLoad.Enabled = false;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Export CSV File",

                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,
            };

            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //ExportDataGridViewToCsv(saveFileDialog.FileName);
            //}
            ExportDataGridViewToCsv(@"C:\magic\output.csv");
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse CSV Files",

                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,

                ReadOnlyChecked = true,
                ShowReadOnly = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ImportCsvToDataGridView(openFileDialog.FileName);
            }

        }

        private void MakeTopRowsReadOnly()
        {
            // Check if the DataGridView has enough rows
            if (dgInvoice.Rows.Count >= 2)
            {
                // Loop through the first two rows
                for (int i = 0; i < 2; i++)
                {
                    // Make the entire row read-only
                    dgInvoice.Rows[i].ReadOnly = true;

                    // Optionally, to visually distinguish the read-only rows, you might want to change their style
                    dgInvoice.Rows[i].DefaultCellStyle.BackColor = Color.LightBlue;
                }
            }
        }

        private void ImportCsvToDataGridView(string filePath)
        {
            int count = 12;
            DataTable dataTable = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                //string[] headers = sr.ReadLine().Split(',');
                for (int i = 0; i < 12; i++)
                {
                    dataTable.Columns.Add(i.ToString());
                }


                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(',');
                    DataRow dr = dataTable.NewRow();
                    for (int i = 0; i < count; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dataTable.Rows.Add(dr);
                }
            }

            dgInvoice.DataSource = dataTable;
            MakeTopRowsReadOnly();
        }


        private void ExportDataGridViewToCsv(string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                // Write the headers.
                //for (int i = 0; i < dgInvoice.Columns.Count; i++)
                //{
                //    if (!dgInvoice.Columns[i].HeaderText.StartsWith("Column"))
                //        { sw.Write(dgInvoice.Columns[i].HeaderText);
                //    }
                //    if (i < dgInvoice.Columns.Count - 1)
                //    {
                //        sw.Write(",");
                //    }
                //}
                //sw.WriteLine();

                // Write the data.
                foreach (DataGridViewRow dr in dgInvoice.Rows)
                {
                    for (int i = 0; i < dgInvoice.Columns.Count; i++)
                    {
                        if (dr.Cells[i].Value != null) // Check for null value
                        {
                            if (!dr.Cells[i].Value.ToString().StartsWith("Column"))
                            {
                                sw.Write(dr.Cells[i].Value.ToString());
                            }                            
                        }
                        if (i < dgInvoice.Columns.Count - 1)
                        {
                            sw.Write(",");
                        }
                    }
                    sw.WriteLine();
                }
            }
            PrintDocument(filePath);
        }

        private void PrintDocument(string csvfile)
        {
            var lines = File.ReadAllLines(csvfile);
            List<string[]> csvData = new List<string[]>();
            foreach (string line in lines)
            {
                csvData.Add(line.Split(','));
            }
            PrintDocument printDocument = new PrintDocument();
            printDocument.DocumentName = "CSV Data Transcribed";

            // Adjust the margins (in hundredths of an inch)
            // For thin margins, let's set them to about a quarter of an inch (25 units)
            var margins = new Margins(25, 25, 25, 25); // Left, Right, Top, Bottom margins

            printDocument.DefaultPageSettings.Margins = margins;

            printDocument.PrintPage += (sender, e) =>
            {
                Graphics graphics = e.Graphics;
                Font font = new Font("Times New Roman", 8);
                Brush brush = Brushes.Black;
                float lineHeight = font.GetHeight();
                // Use the adjusted margins
                float x = e.MarginBounds.Left;
                float y = e.MarginBounds.Top;

                // Assuming csvData is your parsed CSV content
                foreach (var row in csvData)
                {
                    string line = string.Join("    ", row);
                    graphics.DrawString(line, font, brush, x, y);
                    y += lineHeight;
                    if (y + lineHeight > e.MarginBounds.Bottom)
                    {
                        // Check to prevent drawing below the bottom margin
                        break;
                    }
                }
            };

            // Summon the print dialogue to allow the mortal user to command the printing
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

                PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
                {
                    Document = printDocument,
                    Width = 800,
                    Height = 600
                };

                // Optional: Adjust the dialog to show a larger preview or other properties
                printPreviewDialog.PrintPreviewControl.Zoom = 1; // Sets the zoom level. 1 = 100%
                printPreviewDialog.ShowDialog();
            
        }
    }
}

