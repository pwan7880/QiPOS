using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace QiPOS
{    
    public sealed class FrmCash : Form
    {
        #region variables

        private Connect connDB;
        private string queryStr;
        private string reportPeriod;
        private string userCondiStr;
        private DataTable CashTable;
        private IContainer components;
        private Panel pnlControl;
        private Label lblReport;
        private Panel pnlDateRange;
        private Label lblPeriod;
        private ComboBox cbRange;
        private Label lblFrom;
        private DateTimePicker DtpFromDate;
        private Label lblTo;
        private DateTimePicker DtpToDate;
        private DateTimePicker DtpReportAt;
        private readonly static Decimal zerodec = new Decimal(0.0);        
        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label lblCash;
        private Label lblReportAT;
        private CustomButton customButton4;
        private CustomButton customButton5;
        private DataGridView grid;
        private CustomButton customButton6;


        #endregion item
        private List<CashSummary> allData;
        private static Font defaultFont = new Font("Segoe UI", 14F, FontStyle.Regular);
        private static Font boldFont = new Font("Segoe UI", 14F, FontStyle.Bold);
        public FrmCash()
        {
            InitializeComponent();
        }

        private void FrmCash_Load(object sender, EventArgs e)
        { 
            Initiate();
        }

        private void Initiate()
        {
            grid.RowHeadersVisible = false;
            grid.Font = defaultFont;
            grid.ColumnHeadersDefaultCellStyle.Font = boldFont;
            grid.RowTemplate.Height = 32; // Adjust row height for better spacing
            grid.Location = new Point(20, 20);
            grid.Size = new Size(640, 500);
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.ColumnCount = 4;
            
            grid.Columns[0].Name = "Category";
            grid.Columns[1].Name = "Cash";
            grid.Columns[2].Name = "EFTPOS";
            grid.Columns[3].Name = "Total";

            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            allData = LoadRealData(DateTime.Today.AddDays(-1), DateTime.Today);
            FilterData();            
        }
                 
        private void BtnShort_Click(object sender, EventArgs e)
        {
            PrintThermal();
        }
        // TODO: fix the printing logic
        private void PrintThermal()
        {
            //MessageBox.Show("Printing Cash Summary Report...", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ConfigurationReader reader = new ConfigurationReader();
            CompanyData companyData = reader.CompanyInfo();

            string str1 = "\x001B!\x0002" + "\x001D!\0" + "\x001B \x0001" + "\x001B3D" + "\x001BE\x0001 " + companyData.CompanyName
                + "\n" + "\x001Ba\x0001" + "Sales Statistics\n" + "\x001BE\0" + "\x001Ba\0" + " --------------------------------------" + "\n"
                + " CATEGORY                       AMOUNT" + "\n" + " --------------------------------------" + "\n";
            //for (int index = 0; index < CashTable.Rows.Count; ++index)
            //{
            //    if (CashTable.Rows[index]["Category"].ToString().Trim() == "Total Sales:")
            //    {
            //        string str2 = " --------------------------------------";
            //        str1 = str1 + str2 + "\n";
            //    }
            //    string in_str = " " + CashTable.Rows[index]["Category"].ToString().Trim();
            //    string pad = "";
            //    int blanks = 38 - in_str.Length + CurrencyFormat(CashTable.Rows[index]["total"].ToString().Trim()).Length;
            //    pad = pad.PadRight(blanks, ' ');

            //    string str3 = in_str + pad + CurrencyFormat(CashTable.Rows[index]["total"].ToString().Trim());
            //    str1 = str1 + str3 + "\n";
            //}
            string str4 = " --------------------------------------";
            string szString = str1 + str4 + "\n" + " Date: " + DtpReportAt.Value.ToString("dd/MM/yyyy") + "\n" + "\x001Bd\x0006\x001DV\x0001";
            RawPrinterHelper.SendStringToPrinter(companyData.PosPrinter, szString);
        }

        public List<CashSummary> LoadRealData(DateTime startDate, DateTime endDate)
        {
            var list = new List<CashSummary>();
            connDB = new Connect();
            connDB.ConnectBD();
            // ✅ Update with your actual connection string!
            string connStr = connDB.ConnectionStr;
            using var conn = new SqlConnection(connStr);
            using var cmd = new SqlCommand("GetCashSummaryByDateRange", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@start", startDate.Date);
            cmd.Parameters.AddWithValue("@end", endDate.Date);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new CashSummary(
                    reader["Category"].ToString(),
                    reader.GetDecimal(reader.GetOrdinal("Cash")),
                    reader.GetDecimal(reader.GetOrdinal("EFTPOS")),
                    reader.GetDecimal(reader.GetOrdinal("Total")),
                    Convert.ToInt16(reader["acc_number"])
                ));
            }
            return list;
        }

        private void FilterData()
        {          
            LoadGrid(allData.ToList());
        }

        private void LoadGrid(List<CashSummary> list)
        {
            for (int i = 1; i <= 3; i++)
            {
                grid.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            grid.Rows.Clear();

            var incomeItems = list
                .Where(x => x.CashTotal >= 0)
                .OrderByDescending(x => x.CashTotal)
                .ToList();

            var payoutItems = list
                .Where(x => x.CashTotal < 0)
                .OrderByDescending(x => Math.Abs(x.CashTotal))
                .ToList(); 

            decimal totalSalesCash = incomeItems.Sum(x => x.CashCash);
            decimal totalSalesEftpos = incomeItems.Sum(x => x.CashPos);
            decimal totalSales = incomeItems.Sum(x => x.CashTotal);

            decimal totalPaidCash = payoutItems.Sum(x => x.CashCash);
            decimal totalPaidEftpos = payoutItems.Sum(x => x.CashPos);
            decimal totalPaid = payoutItems.Sum(x => x.CashTotal);

            decimal totalNetCash = totalSales + totalPaid; // Paid is negative
            decimal netCash = totalSalesCash + totalPaidCash;
            decimal netEftpos = totalSalesEftpos + totalPaidEftpos;


            foreach (var item in incomeItems)
                AddRow(item);

            AddSummaryRow("Total Sales:", totalSalesCash, totalSalesEftpos, totalSales, Color.Blue);

            foreach (var item in payoutItems)
                AddRow(item);

            AddSummaryRow("Total Cash Paid:", totalPaidCash, totalPaidEftpos, totalPaid, Color.Red);

            
            AddSummaryRow("Total Cash:", netCash, netEftpos, totalNetCash, Color.Blue);
        }

        /// <summary>
        /// Adds a row to the grid for each CashSummary item.
        /// </summary>
        /// <param name="item"></param>
        private void AddRow(CashSummary item)
        {
            int rowIndex = grid.Rows.Add(
                item.CashCatogory,
                item.CashCash.ToString("C"),
                item.CashPos.ToString("C"),
                item.CashTotal.ToString("C")
            );

            var row = grid.Rows[rowIndex];

            if (item.AccNo == 0)
            {
                row.DefaultCellStyle.BackColor = Color.LightBlue;
                row.DefaultCellStyle.Font = new Font(Font, FontStyle.Bold);

            }

            if (item.CashTotal < 0)
            {
                row.DefaultCellStyle.ForeColor = Color.Red;
            }
        }


        /// <summary>
        /// Adds a summary row to the grid with bold font and light gray background.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="cash"></param>
        /// <param name="eftpos"></param>
        /// <param name="total"></param>
        /// <param name="fontColor"></param>
        private void AddSummaryRow(string label, decimal cash, decimal eftpos, decimal total, Color fontColor)
        {
            int rowIndex = grid.Rows.Add(
                label,
                cash.ToString("C"),
                eftpos.ToString("C"),
                total.ToString("C")
            );

            var row = grid.Rows[rowIndex];
            row.DefaultCellStyle.Font = boldFont;
            row.DefaultCellStyle.BackColor = Color.LightGray;
            row.DefaultCellStyle.ForeColor = fontColor;
        }

        /// <summary>
        /// Click to exit the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();            
        }

        /// <summary>
        /// need to deal with situations when the range of dates is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            // not implemented
        }

        /// <summary>
        /// Print out a report of the cash summary on thermal receipt printer.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, EventArgs e)
        {
             PrintThermal();
        }

        private void DtpReportAt_ValueChanged(object sender, EventArgs e)
        {
            LoadGrid(LoadRealData(DtpReportAt.Value.AddDays(-1), DtpReportAt.Value));
        }

        /// <summary>
        /// TODO: Implement the logic to handle date changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpFromDate_ValueChanged(object sender, EventArgs e)
        {
        
        }

        /// <summary>
        /// TODO: Implement the logic to handle date changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DtpToDate_ValueChanged(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// TODO: Implement the logic to handle summary button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSummary_Click(object sender, EventArgs e)
        {
             
        }

        /// <summary>
        /// TODO: Implement the logic to handle cell content click in the DataGridView.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgCash_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // not implemented

        }

        #region components

        private void InitializeComponent()
        {
            this.pnlControl = new System.Windows.Forms.Panel();
            this.lblReport = new System.Windows.Forms.Label();
            this.pnlDateRange = new System.Windows.Forms.Panel();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.cbRange = new System.Windows.Forms.ComboBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.DtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblTo = new System.Windows.Forms.Label();
            this.DtpToDate = new System.Windows.Forms.DateTimePicker();
            this.DtpReportAt = new System.Windows.Forms.DateTimePicker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCash = new System.Windows.Forms.Label();
            this.lblReportAT = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.pnlControl.SuspendLayout();
            this.pnlDateRange.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlControl
            // 
            this.pnlControl.Controls.Add(this.lblReport);
            this.pnlControl.Controls.Add(this.pnlDateRange);
            this.pnlControl.Controls.Add(this.DtpReportAt);
            this.pnlControl.Location = new System.Drawing.Point(778, 40);
            this.pnlControl.Margin = new System.Windows.Forms.Padding(0);
            this.pnlControl.MaximumSize = new System.Drawing.Size(300, 600);
            this.pnlControl.MinimumSize = new System.Drawing.Size(300, 600);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(300, 600);
            this.pnlControl.TabIndex = 29;
            // 
            // lblReport
            // 
            this.lblReport.AutoSize = true;
            this.lblReport.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.lblReport.ForeColor = System.Drawing.Color.Blue;
            this.lblReport.Location = new System.Drawing.Point(3, 9);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(157, 40);
            this.lblReport.TabIndex = 24;
            this.lblReport.Text = "Report At:";
            // 
            // pnlDateRange
            // 
            this.pnlDateRange.BackColor = System.Drawing.Color.LightCyan;
            this.pnlDateRange.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDateRange.Controls.Add(this.lblPeriod);
            this.pnlDateRange.Controls.Add(this.cbRange);
            this.pnlDateRange.Controls.Add(this.lblFrom);
            this.pnlDateRange.Controls.Add(this.DtpFromDate);
            this.pnlDateRange.Controls.Add(this.lblTo);
            this.pnlDateRange.Controls.Add(this.DtpToDate);
            this.pnlDateRange.Location = new System.Drawing.Point(8, 44);
            this.pnlDateRange.Name = "pnlDateRange";
            this.pnlDateRange.Size = new System.Drawing.Size(281, 98);
            this.pnlDateRange.TabIndex = 27;
            // 
            // lblPeriod
            // 
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriod.Location = new System.Drawing.Point(7, 9);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(105, 32);
            this.lblPeriod.TabIndex = 102;
            this.lblPeriod.Text = "Period:";
            // 
            // cbRange
            // 
            this.cbRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRange.FormattingEnabled = true;
            this.cbRange.Location = new System.Drawing.Point(81, 5);
            this.cbRange.Name = "cbRange";
            this.cbRange.Size = new System.Drawing.Size(186, 37);
            this.cbRange.TabIndex = 1;
            this.cbRange.SelectedIndexChanged += new System.EventHandler(this.CbRange_SelectedIndexChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFrom.Location = new System.Drawing.Point(7, 39);
            this.lblFrom.MaximumSize = new System.Drawing.Size(670, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(87, 32);
            this.lblFrom.TabIndex = 103;
            this.lblFrom.Text = "From:";
            // 
            // DtpFromDate
            // 
            this.DtpFromDate.CustomFormat = "dd MMM yy";
            this.DtpFromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpFromDate.Location = new System.Drawing.Point(81, 37);
            this.DtpFromDate.Name = "DtpFromDate";
            this.DtpFromDate.Size = new System.Drawing.Size(107, 35);
            this.DtpFromDate.TabIndex = 2;
            this.DtpFromDate.ValueChanged += new System.EventHandler(this.DtpFromDate_ValueChanged);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTo.Location = new System.Drawing.Point(7, 67);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(55, 32);
            this.lblTo.TabIndex = 104;
            this.lblTo.Text = "To:";
            // 
            // DtpToDate
            // 
            this.DtpToDate.CustomFormat = "dd MMM yy";
            this.DtpToDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpToDate.Location = new System.Drawing.Point(81, 67);
            this.DtpToDate.Name = "DtpToDate";
            this.DtpToDate.Size = new System.Drawing.Size(108, 35);
            this.DtpToDate.TabIndex = 3;
            this.DtpToDate.ValueChanged += new System.EventHandler(this.DtpToDate_ValueChanged);
            // 
            // DtpReportAt
            // 
            this.DtpReportAt.CalendarFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpReportAt.CalendarMonthBackground = System.Drawing.SystemColors.Control;
            this.DtpReportAt.CustomFormat = "dd MMM yy";
            this.DtpReportAt.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold);
            this.DtpReportAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpReportAt.Location = new System.Drawing.Point(124, 9);
            this.DtpReportAt.Name = "DtpReportAt";
            this.DtpReportAt.Size = new System.Drawing.Size(152, 45);
            this.DtpReportAt.TabIndex = 25;
            this.DtpReportAt.ValueChanged += new System.EventHandler(this.DtpReportAt_ValueChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlControl, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.grid, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1078, 653);
            this.tableLayoutPanel1.TabIndex = 34;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.lblCash);
            this.flowLayoutPanel1.Controls.Add(this.lblReportAT);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(778, 40);
            this.flowLayoutPanel1.TabIndex = 32;
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.ForeColor = System.Drawing.Color.Crimson;
            this.lblCash.Location = new System.Drawing.Point(3, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(359, 40);
            this.lblCash.TabIndex = 32;
            this.lblCash.Text = "Cash Balance Report";
            // 
            // lblReportAT
            // 
            this.lblReportAT.AutoSize = true;
            this.lblReportAT.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportAT.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblReportAT.Location = new System.Drawing.Point(368, 0);
            this.lblReportAT.Name = "lblReportAT";
            this.lblReportAT.Size = new System.Drawing.Size(125, 40);
            this.lblReportAT.TabIndex = 33;
            this.lblReportAT.Text = "Report";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 43);
            this.grid.Name = "grid";
            this.grid.RowHeadersWidth = 62;
            this.grid.RowTemplate.Height = 28;
            this.grid.Size = new System.Drawing.Size(772, 587);
            this.grid.TabIndex = 33;
            // 
            // FrmCash
            // 
            this.ClientSize = new System.Drawing.Size(1078, 653);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Location = new System.Drawing.Point(0, 30);
            this.Name = "FrmCash";
            this.Text = "Cash Summary Report";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmCash_Load);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.pnlDateRange.ResumeLayout(false);
            this.pnlDateRange.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion components

    }

    public class CashSummary
    {
        public string CashCatogory { get; set; }
        public decimal CashCash { get; set; }
        public decimal CashPos { get; set; }
        public decimal CashTotal { get; set; }
        public short AccNo { get; set; }

        public CashSummary(string category, decimal cash, decimal pos, decimal total, short accNo)
        {
            CashCatogory = category;
            CashCash = cash;
            CashPos = pos;
            CashTotal = total;
            AccNo = accNo;
        }
    }
}

