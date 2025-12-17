using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmProducts : Form
    {
        #region declarations

        private Label lblReport;
        private Panel pnlControl;
        private Label lblPeriod;
        private ComboBox cbRange;
        private DateTimePicker DtpReportAt;
        private Label lblStart;
        private TextBox txtStart;
        private ComboBox cbxSupplier;
        private ComboBox cbxCat;
        private Label lblSupplier;
        private Label lblCat;
        private DataGridView dgItems;
        private Label lblItem;
        private string queryStr;

        #endregion declarations

        private string reportPeriod;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomButton closeButton;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton customButton3;
        private string userCondiStr;

        public FrmProducts()
        {
            this.InitializeComponent();
            this.userCondiStr = "";
        }

        public FrmProducts(bool userOnly)
        {
            this.InitializeComponent();
            this.userCondiStr = "";
            if (!userOnly)
                return;
            this.userCondiStr = " AND pos_sale.user_id = 0 ";
        }

        #region components

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblReport = new System.Windows.Forms.Label();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.customButton3 = new QiPOS.CustomButton();
            this.customButton2 = new QiPOS.CustomButton();
            this.customButton1 = new QiPOS.CustomButton();
            this.closeButton = new QiPOS.CustomButton();
            this.lblStart = new System.Windows.Forms.Label();
            this.txtStart = new System.Windows.Forms.TextBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.DtpReportAt = new System.Windows.Forms.DateTimePicker();
            this.cbRange = new System.Windows.Forms.ComboBox();
            this.cbxSupplier = new System.Windows.Forms.ComboBox();
            this.cbxCat = new System.Windows.Forms.ComboBox();
            this.lblCat = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.dgItems = new System.Windows.Forms.DataGridView();
            this.lblItem = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReport.ForeColor = System.Drawing.Color.Blue;
            this.lblReport.Location = new System.Drawing.Point(9, 7);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(104, 25);
            this.lblReport.TabIndex = 28;
            this.lblReport.Text = "Report At:";
            // 
            // pnlControl
            // 
            this.pnlControl.BackColor = System.Drawing.Color.LightCyan;
            this.pnlControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControl.Controls.Add(this.customButton3);
            this.pnlControl.Controls.Add(this.customButton2);
            this.pnlControl.Controls.Add(this.customButton1);
            this.pnlControl.Controls.Add(this.closeButton);
            this.pnlControl.Controls.Add(this.lblStart);
            this.pnlControl.Controls.Add(this.txtStart);
            this.pnlControl.Controls.Add(this.lblPeriod);
            this.pnlControl.Controls.Add(this.DtpReportAt);
            this.pnlControl.Controls.Add(this.cbRange);
            this.pnlControl.Controls.Add(this.cbxSupplier);
            this.pnlControl.Controls.Add(this.lblReport);
            this.pnlControl.Controls.Add(this.cbxCat);
            this.pnlControl.Controls.Add(this.lblCat);
            this.pnlControl.Controls.Add(this.lblSupplier);
            this.pnlControl.Location = new System.Drawing.Point(962, 33);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(210, 560);
            this.pnlControl.TabIndex = 30;
            // 
            // customButton3
            // 
            this.customButton3.BackColor = System.Drawing.SystemColors.Control;
            this.customButton3.CornerRadius = 40;
            this.customButton3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton3.ForeColor = System.Drawing.Color.Blue;
            this.customButton3.Location = new System.Drawing.Point(33, 347);
            this.customButton3.Name = "customButton3";
            this.customButton3.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton3.Size = new System.Drawing.Size(149, 40);
            this.customButton3.TabIndex = 142;
            this.customButton3.Text = "Sales";
            this.customButton3.Click += new System.EventHandler(this.BtnSales_Click);
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.SystemColors.Control;
            this.customButton2.CornerRadius = 40;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Blue;
            this.customButton2.Location = new System.Drawing.Point(33, 400);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(149, 40);
            this.customButton2.TabIndex = 142;
            this.customButton2.Text = "# Items";
            this.customButton2.Click += new System.EventHandler(this.BtnItem_Click);
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Blue;
            this.customButton1.Location = new System.Drawing.Point(33, 453);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(149, 40);
            this.customButton1.TabIndex = 141;
            this.customButton1.Text = "Max Items";
            this.customButton1.Click += new System.EventHandler(this.BtnMax_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.CornerRadius = 40;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Blue;
            this.closeButton.Location = new System.Drawing.Point(33, 506);
            this.closeButton.Name = "closeButton";
            this.closeButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.closeButton.Size = new System.Drawing.Size(149, 40);
            this.closeButton.TabIndex = 141;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStart.ForeColor = System.Drawing.Color.Blue;
            this.lblStart.Location = new System.Drawing.Point(9, 270);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(82, 25);
            this.lblStart.TabIndex = 114;
            this.lblStart.Text = "Contain";
            // 
            // txtStart
            // 
            this.txtStart.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStart.Location = new System.Drawing.Point(14, 296);
            this.txtStart.Name = "txtStart";
            this.txtStart.Size = new System.Drawing.Size(185, 33);
            this.txtStart.TabIndex = 113;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriod.ForeColor = System.Drawing.Color.Blue;
            this.lblPeriod.Location = new System.Drawing.Point(9, 72);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(75, 25);
            this.lblPeriod.TabIndex = 102;
            this.lblPeriod.Text = "Period:";
            // 
            // DtpReportAt
            // 
            this.DtpReportAt.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpReportAt.CalendarMonthBackground = System.Drawing.SystemColors.Control;
            this.DtpReportAt.CustomFormat = "dd MMM yy";
            this.DtpReportAt.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpReportAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpReportAt.Location = new System.Drawing.Point(14, 35);
            this.DtpReportAt.Name = "DtpReportAt";
            this.DtpReportAt.Size = new System.Drawing.Size(185, 33);
            this.DtpReportAt.TabIndex = 29;
            // 
            // cbRange
            // 
            this.cbRange.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRange.FormattingEnabled = true;
            this.cbRange.Location = new System.Drawing.Point(14, 99);
            this.cbRange.Name = "cbRange";
            this.cbRange.Size = new System.Drawing.Size(185, 33);
            this.cbRange.TabIndex = 1;
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(14, 231);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(185, 33);
            this.cbxSupplier.TabIndex = 109;
            // 
            // cbxCat
            // 
            this.cbxCat.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCat.FormattingEnabled = true;
            this.cbxCat.Location = new System.Drawing.Point(14, 162);
            this.cbxCat.Name = "cbxCat";
            this.cbxCat.Size = new System.Drawing.Size(185, 33);
            this.cbxCat.TabIndex = 108;
            this.cbxCat.SelectedIndexChanged += new System.EventHandler(this.CbxCat_SelectedIndexChanged);
            // 
            // lblCat
            // 
            this.lblCat.AutoSize = true;
            this.lblCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCat.ForeColor = System.Drawing.Color.Blue;
            this.lblCat.Location = new System.Drawing.Point(9, 134);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(102, 25);
            this.lblCat.TabIndex = 111;
            this.lblCat.Text = "Categrory";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.ForeColor = System.Drawing.Color.Blue;
            this.lblSupplier.Location = new System.Drawing.Point(9, 202);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(87, 25);
            this.lblSupplier.TabIndex = 112;
            this.lblSupplier.Text = "Supplier";
            // 
            // dgItems
            // 
            this.dgItems.AllowUserToAddRows = false;
            this.dgItems.AllowUserToDeleteRows = false;
            this.dgItems.AllowUserToResizeColumns = false;
            this.dgItems.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.dgItems.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgItems.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.DarkBlue;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgItems.Location = new System.Drawing.Point(3, 33);
            this.dgItems.Name = "dgItems";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgItems.RowHeadersVisible = false;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgItems.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgItems.RowTemplate.Height = 30;
            this.dgItems.Size = new System.Drawing.Size(953, 717);
            this.dgItems.TabIndex = 31;
            // 
            // lblItem
            // 
            this.lblItem.AutoSize = true;
            this.lblItem.Font = new System.Drawing.Font("Segoe UI", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItem.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblItem.Location = new System.Drawing.Point(3, 0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(261, 30);
            this.lblItem.TabIndex = 34;
            this.lblItem.Text = "Top 50 Products Sales";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.dgItems, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.pnlControl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblItem, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1175, 753);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // FrmProducts
            // 
            this.ClientSize = new System.Drawing.Size(1175, 753);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(0, 30);
            this.Name = "FrmProducts";
            this.Text = "Products";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmProducts_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgItems)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion components

        private void FrmProducts_Load(object sender, EventArgs e)
        {
            this.Initiate();
        }

        private void Initiate()
        {
            try
            {
                this.userCondiStr = " AND pos_sale.user_id = 0";
                Connect connect2 = new Connect();
                using (SqlConnection conn = new SqlConnection(connect2.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetProductCategories", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connect2.aTable = new DataTable();
                            adapter.Fill(connect2.aTable);
                            DataRow row = connect2.aTable.NewRow();
                            row[0] = "  ALL  ";
                            row[1] = 0;
                            row[2] = 0;
                            connect2.aTable.Rows.InsertAt(row, 0);
                            cbxCat.DataSource = connect2.aTable;
                            cbxCat.DisplayMember = "acc_name";
                            cbxCat.ValueMember = "acc_id";
                        }
                    }
                }
                cbRange.DataSource = new string[] { "Today", "This Week", "Month to Date", "Quarter to Date", "Year to Date", "Last Week", "Last Month", "Last Quarter", "Last Year" };
                ProductData(2);
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductData(int which)
        {
            try
            {
                Connect connect = new Connect();
                string dateRange = "";
                DateTime currentDay = DtpReportAt.Value.Date;
                DateRange dateRangeObj = new DateRange(cbRange.SelectedValue?.ToString(), currentDay);
                dateRange = dateRangeObj.SQLDateRange;
                reportPeriod = $"From {dateRangeObj.fromDay:dd MMM yyyy} To {dateRangeObj.toDay:dd MMM yyyy}";
                lblItem.Text = $"Top 50 Products Sales {reportPeriod}";

                string procName = which switch
                {
                    1 => "GetTopProductSales",
                    2 => "GetTopProductItems",
                    3 => "GetTopProductMaxItems",
                    _ => throw new ArgumentException("Invalid report type")
                };

                using (SqlConnection conn = new SqlConnection(connect.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StartDate", dateRangeObj.fromDay);
                        cmd.Parameters.AddWithValue("@EndDate", dateRangeObj.toDay);
                        cmd.Parameters.AddWithValue("@AccNumber", DBNull.Value); // Default to NULL
                        cmd.Parameters.AddWithValue("@CardId", DBNull.Value); // Default to NULL
                        cmd.Parameters.AddWithValue("@SearchText", DBNull.Value); // Default to NULL
                        cmd.Parameters.AddWithValue("@UserId", userCondiStr.Contains("user_id = 0") ? 0 : (object)DBNull.Value);

                        // Apply category filter
                        if (cbxCat.SelectedValue?.ToString() != "0")
                        {
                            using (SqlCommand accCmd = new SqlCommand("SELECT acc_number FROM account_list WHERE acc_id=@AccId", conn))
                            {
                                accCmd.Parameters.AddWithValue("@AccId", cbxCat.SelectedValue);
                                int accNumber = (int)accCmd.ExecuteScalar();
                                cmd.Parameters["@AccNumber"].Value = accNumber;
                            }
                        }

                        // Apply supplier filter
                        if (cbxSupplier.SelectedValue != null && (int)cbxSupplier.SelectedValue != 0)
                        {
                            cmd.Parameters["@CardId"].Value = cbxSupplier.SelectedValue;
                        }

                        // Apply search text filter
                        if (!string.IsNullOrWhiteSpace(txtStart.Text))
                        {
                            cmd.Parameters["@SearchText"].Value = txtStart.Text.Trim();
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connect.aTable = new DataTable();
                            adapter.Fill(connect.aTable);
                            dgItems.DataSource = connect.aTable;
                        }
                    }
                }

                // Configure DataGridView columns
                dgItems.Columns["stock_id"].Visible = false;
                dgItems.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 15.75f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);

                if (which == 1)
                {
                    dgItems.Columns["Amount"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["Ave Per Day"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["%"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["Amount"].DefaultCellStyle.Format = "C";
                    dgItems.Columns["Ave Per Day"].DefaultCellStyle.Format = "C";
                    dgItems.Columns["%"].DefaultCellStyle.Format = "P";
                    dgItems.Columns["Amount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgItems.Columns["Ave Per Day"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgItems.Columns["%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (which == 2)
                {
                    dgItems.Columns["No. Items"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["Ave Per Day"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["%"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["Ave Per Day"].DefaultCellStyle.Format = "N";
                    dgItems.Columns["%"].DefaultCellStyle.Format = "P";
                    dgItems.Columns["No. Items"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgItems.Columns["Ave Per Day"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgItems.Columns["%"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                else if (which == 3)
                {
                    dgItems.Columns["Max No Items"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["Sold Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    dgItems.Columns["Max No Items"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgItems.Columns["Sold Date"].DefaultCellStyle.Format = "ddd. dd MMM yy";
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CbxCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Connect connect = new Connect();
                string accIdStr = cbxCat.SelectedValue?.ToString();
                if (!int.TryParse(accIdStr, out int accId))
                    accId = 0;

                using (SqlConnection conn = new SqlConnection(connect.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetSuppliersByCategory", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@AccId", accId);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connect.aTable = new DataTable();
                            adapter.Fill(connect.aTable);
                            DataRow row = connect.aTable.NewRow();
                            row[1] = "  ALL  ";
                            row[0] = 0;
                            connect.aTable.Rows.InsertAt(row, 0);
                            cbxSupplier.DataSource = connect.aTable;
                            cbxSupplier.DisplayMember = "name";
                            cbxSupplier.ValueMember = "card_id";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error selecting category: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }
         

        private void BtnMax_Click(object sender, EventArgs e)
        {
            this.ProductData(3);
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            this.ProductData(1);
        }

        private void BtnItem_Click(object sender, EventArgs e)
        {
            this.ProductData(2);
        }
    }
}

