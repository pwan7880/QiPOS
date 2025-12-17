using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmPrevSale : Form
    {
        #region GUI components
        private DataGridView dgSaleList;
        private Panel pnlFuns;
        private ComboBox cbxType;
        private Label lblDate;
        private Label lblAmount;
        private Label lblType;
        private ComboBox cbxAmount;
        private DateTimePicker DtpDate;
        private DataGridView dgDetails;
        private Connect connDB;
        private string queryStr;
        private string saleId;
        private string printerNameStr;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomButton closeButton;
        private CustomButton customButton1;
        private CustomButton BtnDelete;
        private CustomButton paymentTypeButton;

        #endregion GUI components

        public FrmPrevSale()
        {
            this.InitializeComponent();
            this.Initiate();
        }

        #region components

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgSaleList = new System.Windows.Forms.DataGridView();
            this.pnlFuns = new System.Windows.Forms.Panel();
            this.paymentTypeButton = new QiPOS.CustomButton();
            this.BtnDelete = new QiPOS.CustomButton();
            this.customButton1 = new QiPOS.CustomButton();
            this.closeButton = new QiPOS.CustomButton();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.cbxAmount = new System.Windows.Forms.ComboBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.dgDetails = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dgSaleList)).BeginInit();
            this.pnlFuns.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgSaleList
            // 
            this.dgSaleList.AllowUserToAddRows = false;
            this.dgSaleList.AllowUserToDeleteRows = false;
            this.dgSaleList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgSaleList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgSaleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSaleList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSaleList.Location = new System.Drawing.Point(3, 3);
            this.dgSaleList.Name = "dgSaleList";
            this.dgSaleList.RowHeadersVisible = false;
            this.dgSaleList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSaleList.Size = new System.Drawing.Size(517, 654);
            this.dgSaleList.TabIndex = 0;
            this.dgSaleList.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSaleList_RowEnter);
            // 
            // pnlFuns
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnlFuns, 2);
            this.pnlFuns.Controls.Add(this.paymentTypeButton);
            this.pnlFuns.Controls.Add(this.BtnDelete);
            this.pnlFuns.Controls.Add(this.customButton1);
            this.pnlFuns.Controls.Add(this.closeButton);
            this.pnlFuns.Controls.Add(this.DtpDate);
            this.pnlFuns.Controls.Add(this.cbxAmount);
            this.pnlFuns.Controls.Add(this.lblAmount);
            this.pnlFuns.Controls.Add(this.lblType);
            this.pnlFuns.Controls.Add(this.lblDate);
            this.pnlFuns.Controls.Add(this.cbxType);
            this.pnlFuns.Location = new System.Drawing.Point(3, 663);
            this.pnlFuns.Name = "pnlFuns";
            this.pnlFuns.Size = new System.Drawing.Size(1041, 72);
            this.pnlFuns.TabIndex = 1;
            // 
            // paymentTypeButton
            // 
            this.paymentTypeButton.BackColor = System.Drawing.SystemColors.Control;
            this.paymentTypeButton.CornerRadius = 40;
            this.paymentTypeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.paymentTypeButton.ForeColor = System.Drawing.Color.Black;
            this.paymentTypeButton.Location = new System.Drawing.Point(585, 23);
            this.paymentTypeButton.Name = "paymentTypeButton";
            this.paymentTypeButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.paymentTypeButton.Size = new System.Drawing.Size(131, 40);
            this.paymentTypeButton.TabIndex = 142;
            this.paymentTypeButton.Text = "Cash/EFTPOS";
            this.paymentTypeButton.Click += new System.EventHandler(this.PaymentTypeButton_Click);
            // 
            // BtnDelete
            // 
            this.BtnDelete.BackColor = System.Drawing.SystemColors.Control;
            this.BtnDelete.CornerRadius = 40;
            this.BtnDelete.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDelete.ForeColor = System.Drawing.Color.Black;
            this.BtnDelete.Location = new System.Drawing.Point(449, 23);
            this.BtnDelete.Name = "BtnDelete";
            this.BtnDelete.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.BtnDelete.Size = new System.Drawing.Size(131, 40);
            this.BtnDelete.TabIndex = 141;
            this.BtnDelete.Text = "Delete";
            this.BtnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Black;
            this.customButton1.Location = new System.Drawing.Point(723, 23);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(175, 40);
            this.customButton1.TabIndex = 141;
            this.customButton1.Text = "Reprint Receipt";
            this.customButton1.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // closeButton
            // 
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.CornerRadius = 40;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Black;
            this.closeButton.Location = new System.Drawing.Point(904, 23);
            this.closeButton.Name = "closeButton";
            this.closeButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.closeButton.Size = new System.Drawing.Size(128, 40);
            this.closeButton.TabIndex = 141;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "dd MMM yy";
            this.DtpDate.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(7, 31);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.Size = new System.Drawing.Size(130, 33);
            this.DtpDate.TabIndex = 11;
            this.DtpDate.ValueChanged += new System.EventHandler(this.DtpDate_ValueChanged);
            // 
            // cbxAmount
            // 
            this.cbxAmount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxAmount.FormattingEnabled = true;
            this.cbxAmount.Location = new System.Drawing.Point(309, 31);
            this.cbxAmount.Name = "cbxAmount";
            this.cbxAmount.Size = new System.Drawing.Size(121, 33);
            this.cbxAmount.TabIndex = 10;
            this.cbxAmount.SelectedIndexChanged += new System.EventHandler(this.CbxAmount_SelectedIndexChanged);
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.Location = new System.Drawing.Point(329, 4);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(85, 25);
            this.lblAmount.TabIndex = 6;
            this.lblAmount.Text = "Amount";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.BackColor = System.Drawing.SystemColors.Control;
            this.lblType.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblType.Location = new System.Drawing.Point(178, 5);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(99, 25);
            this.lblType.TabIndex = 5;
            this.lblType.Text = "Item Type";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.BackColor = System.Drawing.SystemColors.Control;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(23, 5);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(94, 25);
            this.lblDate.TabIndex = 4;
            this.lblDate.Text = "Sale Date";
            // 
            // cbxType
            // 
            this.cbxType.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Location = new System.Drawing.Point(143, 30);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(160, 33);
            this.cbxType.TabIndex = 1;
            this.cbxType.SelectedIndexChanged += new System.EventHandler(this.CbxType_SelectedIndexChanged);
            // 
            // dgDetails
            // 
            this.dgDetails.AllowUserToAddRows = false;
            this.dgDetails.AllowUserToDeleteRows = false;
            this.dgDetails.AllowUserToResizeColumns = false;
            this.dgDetails.AllowUserToResizeRows = false;
            this.dgDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgDetails.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgDetails.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDetails.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDetails.Location = new System.Drawing.Point(526, 3);
            this.dgDetails.MultiSelect = false;
            this.dgDetails.Name = "dgDetails";
            this.dgDetails.ReadOnly = true;
            this.dgDetails.RowHeadersVisible = false;
            this.dgDetails.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDetails.Size = new System.Drawing.Size(518, 654);
            this.dgDetails.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.dgDetails, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlFuns, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.dgSaleList, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1047, 738);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // FrmPrevSale
            // 
            this.ClientSize = new System.Drawing.Size(1047, 738);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(0, 30);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmPrevSale";
            this.Text = "Previous Sales --";
            ((System.ComponentModel.ISupportInitialize)(this.dgSaleList)).EndInit();
            this.pnlFuns.ResumeLayout(false);
            this.pnlFuns.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDetails)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion components


        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            base.Dispose();
        }

        private void Initiate()
        {
            try
            {
                GetPrinterNameP();
                saleId = "";

                // Query account list using stored procedure
                DataTable accountTable;
                Connect connect = new Connect();
                connDB = connect;
                using (var connection = new SqlConnection(connect.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetAccountListForType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@AccTypeId", 5);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            accountTable = new DataTable();
                            adapter.Fill(accountTable);                            
                        }
                    }
                }

                // Add default "ALL" row
                DataRow row = accountTable.NewRow();
                row["acc_name"] = " ALL ";
                row["acc_number"] = 0;
                accountTable.Rows.InsertAt(row, 0);

                // Bind to cbxType
                cbxType.DataSource = accountTable;
                cbxType.DisplayMember = "acc_name";
                cbxType.ValueMember = "acc_number";

                // Set up cbxAmount
                connDB.aTable = WeekHelper.GetDayOfWeekTable();
                cbxAmount.DataSource = connDB.aTable;
                cbxAmount.DisplayMember = "week_short";
                cbxAmount.ValueMember = "dayofweek";

                // Configure dgSaleList
                dgSaleList.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgSaleList.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgSaleList.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgSaleList.Columns[1].DefaultCellStyle.Format = "dd MMM yy";
                dgSaleList.Columns[2].DefaultCellStyle.Format = "T";
                dgSaleList.Columns[3].DefaultCellStyle.Format = "C";
                dgSaleList.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgSaleList.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgSaleList.Columns[0].ReadOnly = true;
                dgSaleList.Columns[1].ReadOnly = true;
                dgSaleList.Columns[2].ReadOnly = true;
                dgSaleList.Columns[3].ReadOnly = true;

                BtnDelete.Visible = true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing previous sales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DtpDate_ValueChanged(object sender, EventArgs e)
        {
            this.GetSaleSummary();
        }

        private void CbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSaleSummary();
        }

        private void CbxAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetSaleSummary();
        }

        private void GetSaleSummary()
        {
            try
            {
                Connect connect = new Connect();
                connDB = connect;
                using (var connection = new SqlConnection(connDB.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetSaleSummary", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SaleDate", DtpDate.Value.Date);

                        // Handle cbxType filter
                        if (cbxType.SelectedValue != null && cbxType.SelectedValue.ToString() != "System.Data.DataRowView" && (int)cbxType.SelectedValue != 0)
                            command.Parameters.AddWithValue("@AccNumber", cbxType.SelectedValue);
                        else
                            command.Parameters.AddWithValue("@AccNumber", DBNull.Value);

                        // Handle cbxAmount filter
                        if (cbxAmount.SelectedValue != null && cbxAmount.SelectedValue.ToString() != "System.Data.DataRowView" &&
                            new[] { 50, 100, 200, 500 }.Contains((int)cbxAmount.SelectedValue))
                            command.Parameters.AddWithValue("@AmountRange", cbxAmount.SelectedValue);
                        else
                            command.Parameters.AddWithValue("@AmountRange", DBNull.Value);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var saleTable = new DataTable();
                            adapter.Fill(saleTable);
                            dgSaleList.DataSource = saleTable;
                        }
                    }
                }

                if (dgSaleList.Rows.Count == 0)
                {
                    saleId = "";
                    dgDetails.DataSource = null;
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saleId = "";
                dgDetails.DataSource = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sales summary: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                saleId = "";
                dgDetails.DataSource = null;
            }
        }

        private void DgSaleList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.saleId = this.dgSaleList.SelectedRows.Count <= 0 ? this.dgSaleList.Rows[0].Cells[0].Value.ToString() : this.dgSaleList.SelectedRows[0].Cells[0].Value.ToString();
            if (!(this.saleId != ""))
                return;
            this.dgDetails.DataSource = this.SaleDetails(this.saleId);
            this.dgDetails.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgDetails.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            this.dgDetails.Columns[2].DefaultCellStyle.Format = "C";
            this.dgDetails.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            this.dgDetails.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            int count = this.dgDetails.Rows.Count;
            if (this.dgDetails.Rows[count - 2].Cells[0].Value.ToString() == "Change")
                --count;
            this.dgDetails.Rows[count - 4].DefaultCellStyle.Font = this.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte)0);
        }

        /// <summary>
        /// TODO: check it works with datetime miscast
        /// </summary>
        /// <param name="salesID"></param>
        /// <returns></returns>
        private DataTable SaleDetails(string salesID)
        {
            try
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("Item", typeof(string));
                dataTable.Columns.Add("Qty", typeof(int));
                dataTable.Columns.Add("Amount", typeof(decimal));
                Connect connect = new Connect();
                connDB = connect;
                using (var connection = new SqlConnection(connDB.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("GetSaleDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SaleTransId", salesID);

                        using (var reader = command.ExecuteReader())
                        {
                            // First result set: item details
                            while (reader.Read())
                            {
                                DataRow row = dataTable.NewRow();
                                row["Item"] = reader["Item"].ToString();
                                row["Qty"] = reader.GetInt32(reader.GetOrdinal("Qty"));
                                row["Amount"] = reader.GetDecimal(reader.GetOrdinal("Amount"));
                                dataTable.Rows.Add(row);
                            }

                            // Move to second result set: transaction details
                            if (reader.NextResult() && reader.Read())
                            {
                                // Add TOTAL row
                                DataRow totalRow = dataTable.NewRow();
                                totalRow["Item"] = "TOTAL";
                                totalRow["Amount"] = reader.GetDecimal(reader.GetOrdinal("sales_amount"));
                                dataTable.Rows.Add(totalRow);

                                // Add Total Includes GST row
                                DataRow gstRow = dataTable.NewRow();
                                gstRow["Item"] = "Total Includes GST";
                                gstRow["Amount"] = reader.GetDecimal(reader.GetOrdinal("GST_collect"));
                                dataTable.Rows.Add(gstRow);

                                // Add Received row
                                DataRow receivedRow = dataTable.NewRow();
                                receivedRow["Item"] = "Received";
                                receivedRow["Amount"] = reader.GetDecimal(reader.GetOrdinal("received"));
                                dataTable.Rows.Add(receivedRow);

                                // Add Change row if non-zero
                                decimal change = reader.GetDecimal(reader.GetOrdinal("change"));
                                if (change != 0)
                                {
                                    DataRow changeRow = dataTable.NewRow();
                                    changeRow["Item"] = "Change";
                                    changeRow["Amount"] = change;
                                    dataTable.Rows.Add(changeRow);
                                }

                                // Add Date row
                                DateTime saleDate = reader.GetDateTime(reader.GetOrdinal("sale_date"));
                                string saleTime = reader.GetOrdinal("sale_time").ToString();
                                DataRow dateRow = dataTable.NewRow();
                                dateRow["Item"] = $"Date: {saleDate:dd MMM yyyy} {saleTime}";
                                dataTable.Rows.Add(dateRow);
                            }
                        }
                    }
                }

                return dataTable;
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading sale details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            new PrintReceiptCls(this.saleId, this.printerNameStr).PrintReceiptDirect();
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saleId) ||
                MessageBox.Show("Delete Selected Sales ?", "Delete Sales Transaction", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                Connect connect = new Connect();
                connDB = connect;
                using (var connection = new SqlConnection(connDB.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("SoftDeleteSale", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SaleTransId", saleId);

                        command.ExecuteNonQuery();
                    }
                }

                GetSaleSummary();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting sales transaction: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void GetPrinterNameP()
        {
            this.printerNameStr = new ConfigurationReader().CompanyInfo().PosPrinter;
        }


        private void PaymentTypeButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saleId))
            {
                MessageBox.Show("No sale transaction selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Connect connDB = new Connect();                
                using (var connection = new SqlConnection(connDB.ConnectionStr))
                {
                    connection.Open();

                    using (var command = new SqlCommand("TogglePaymentType", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@SaleTransId", saleId);
                        command.ExecuteNonQuery();
                    }
                }
                Close();
                Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error toggling payment type: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

