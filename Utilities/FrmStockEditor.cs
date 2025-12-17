using System;
using System.Data;
using System.Windows.Forms;

namespace QiPOS
{
    /// <summary>
    /// Form for editing PLUs (Price Look-Up codes) from the pos_stock table.
    /// Provides a grid-based editor similar to SSMS for quick editing, adding, and deleting stock items.
    /// Changes are saved back to the database using the existing Connect class.
    /// </summary>
    public partial class FrmStockEditor : Form
    {
        private Connect conn;
        private DataTable stockTable;
        private DataGridView dataGridViewStock;

        private Button btnSave;
        private Button btnCancel;
        private Button btnRefresh;

        public FrmStockEditor()
        {
            InitializeComponent();
            LoadStockData();
        }

        private void InitializeComponent()
        {
            this.dataGridViewStock = new DataGridView();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.btnRefresh = new Button();

            // Form settings
            this.Text = "Stock Editor (PLUs)";
            this.Size = new System.Drawing.Size(1200, 800);
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.StartPosition = FormStartPosition.CenterScreen;

            // DataGridView
            this.dataGridViewStock.Dock = DockStyle.Fill;
            this.dataGridViewStock.AllowUserToAddRows = true;
            this.dataGridViewStock.AllowUserToDeleteRows = true;
            this.dataGridViewStock.EditMode = DataGridViewEditMode.EditOnEnter;
            this.dataGridViewStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewStock.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

            // Buttons panel
            var panelButtons = new FlowLayoutPanel();
            panelButtons.Dock = DockStyle.Bottom;
            panelButtons.Height = 50;
            panelButtons.FlowDirection = FlowDirection.LeftToRight;

            this.btnSave.Text = "Save Changes";
            this.btnSave.Width = 120;
            this.btnSave.Click += BtnSave_Click;

            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Width = 120;
            this.btnRefresh.Click += BtnRefresh_Click;

            this.btnCancel.Text = "Close";
            this.btnCancel.Width = 120;
            this.btnCancel.Click += BtnCancel_Click;

            panelButtons.Controls.Add(this.btnSave);
            panelButtons.Controls.Add(this.btnRefresh);
            panelButtons.Controls.Add(this.btnCancel);

            // Add controls to form
            this.Controls.Add(this.dataGridViewStock);
            this.Controls.Add(panelButtons);
        }

        private void LoadStockData()
        {
            try
            {
                conn = new Connect();
                conn.QueryTable("SELECT * FROM pos_stock ORDER BY descr");
                stockTable = conn.aTable;
                dataGridViewStock.DataSource = stockTable;

                // Configure columns for better usability
                ConfigureGridColumns();

                // Read-only columns (e.g., identity and timestamps)
                if (dataGridViewStock.Columns["stock_id"] != null)
                    dataGridViewStock.Columns["stock_id"].ReadOnly = true;
                if (dataGridViewStock.Columns["entered_date"] != null)
                    dataGridViewStock.Columns["entered_date"].ReadOnly = true;
                if (dataGridViewStock.Columns["last_sold_date"] != null)
                    dataGridViewStock.Columns["last_sold_date"].ReadOnly = true;

                // Currency formatting for price columns
                FormatCurrencyColumn("cost");
                FormatCurrencyColumn("RRP");
                FormatCurrencyColumn("GST_collect");
                FormatCurrencyColumn("GST_paid");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading stock data: " + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureGridColumns()
        {
            // Hide unnecessary or internal columns if needed
            // For example: dataGridViewStock.Columns["notes"].Visible = false;

            // Set display order or widths as needed
            dataGridViewStock.Columns["descr"].Width = 300;
            dataGridViewStock.Columns["barcode"].Width = 150;
            dataGridViewStock.Columns["RRP"].Width = 100;
            dataGridViewStock.Columns["stk_on_hand"].Width = 80;

            // Tooltips or headers
            dataGridViewStock.Columns["descr"].HeaderText = "Description";
            dataGridViewStock.Columns["barcode"].HeaderText = "Barcode";
            dataGridViewStock.Columns["acc_number"].HeaderText = "Account Number";
            dataGridViewStock.Columns["RRP"].HeaderText = "RRP (Retail Price)";
            dataGridViewStock.Columns["stk_on_hand"].HeaderText = "Stock on Hand";
            dataGridViewStock.Columns["GST_collect"].HeaderText = "GST Collected";
        }

        private void FormatCurrencyColumn(string columnName)
        {
            if (dataGridViewStock.Columns[columnName] != null)
            {
                dataGridViewStock.Columns[columnName].DefaultCellStyle.Format = "C2"; // Currency with 2 decimals
                dataGridViewStock.Columns[columnName].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // End any active editing
                dataGridViewStock.EndEdit();

                // Get changes
                DataTable changes = stockTable.GetChanges();
                if (changes == null || changes.Rows.Count == 0)
                {
                    MessageBox.Show("No changes to save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Update database
                conn.UpdateTable(changes);
                stockTable.AcceptChanges();

                MessageBox.Show("Changes saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Refresh to show any identity inserts or updates
                LoadStockData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving changes: " + ex.Message + "\n\nNote: Ensure GST and compliance fields are valid per Australian regulations (e.g., GST_collect must align with taxable status).", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            if (stockTable.GetChanges() != null)
            {
                if (MessageBox.Show("Unsaved changes will be lost. Refresh anyway?", "Confirm Refresh", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }
            LoadStockData();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (stockTable.GetChanges() != null)
            {
                var result = MessageBox.Show("Unsaved changes exist. Save before closing?", "Unsaved Changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    BtnSave_Click(null, null);
                }
                else if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
            base.OnFormClosing(e);
        }
    }
}