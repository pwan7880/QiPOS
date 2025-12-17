using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmStatistics : Form
    {
        #region declarations

        private ComboBox cbxCondi;
        private Connect connDB;
        private DataGridView dgPresent;
        private DataGridView dgCompare;
        private DateTimePicker DtpDate;
        private Label lbl1;
        private Label lbl2;
        private Label lbl3;
        private Label lbl4;
        private Panel pnlFun;
        private Panel pnlPay;
        private string queryStr;
        private RadioButton rbAll;
        private RadioButton rbAmount;
        private RadioButton rbCash;
        private RadioButton rbCheck;
        private RadioButton rbFpos;
        private RadioButton rbMargin;
        private RadioButton rbSales;
        private TableLayoutPanel tableLayoutPanel1;
        private CustomButton customButton1;
        private CustomButton BtnItem;


        #endregion declarations

        public FrmStatistics()
        {
            InitializeComponent();
            Initiate();
        }

        private void BtnItem_Click(object sender, System.EventArgs e)
        {
            if (BtnItem.Text == "Sep Item")
            {
                FrmSearch frmSearch = new FrmSearch
                {
                    funIdentifier = "Search"
                };
                AddOwnedForm(frmSearch);
                int i = 0;
                bool flag = frmSearch.ShowDialog(this) != DialogResult.Yes;
                if (!flag)
                    i = System.Convert.ToInt32(Tag.ToString());
                BtnItem.Text = "Summary";
                SepItem(i);
            }
            else
            {
                ConstructQuery();
                BtnItem.Text = "Sep Item";
            }
            FormatDG();
        }

        private void CbxCondi_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ConstructQuery();
            this.FormatDG();
            this.lbl2.Text = this.cbxCondi.Text;
        }

        private string[] CompDateCond(DateTime selectedDate)
        {
            string[] strArray1 = new string[2]
              {
                "",
                "1"
              };
            int num1;
            if (this.cbxCondi.Text == "Same Day of Last Week")
            {
                selectedDate = selectedDate.AddDays(-7.0);
                strArray1[0] = " AND pos_sale_detail.sale_date='" + selectedDate.ToString("yyyy-MM-dd") + "'";
                strArray1[1] = "1";
            }
            else if (this.cbxCondi.Text == "Same Day of Last 4 Weeks")
            {
                selectedDate = selectedDate.AddDays(-28.0);
                strArray1[0] = "  AND ( pos_sale_detail.sale_date='" + selectedDate.ToString("yyyy-MM-dd");
                selectedDate = selectedDate.AddDays(7.0);
                string[] strArray2;
                (strArray2 = strArray1)[0] = strArray2[0] + "' OR pos_sale_detail.sale_date='" + selectedDate.ToString("yyyy-MM-dd");
                selectedDate = selectedDate.AddDays(7.0);
                string[] strArray3;
                (strArray3 = strArray1)[0] = strArray3[0] + "' OR pos_sale_detail.sale_date='" + selectedDate.ToString("yyyy-MM-dd");
                selectedDate = selectedDate.AddDays(7.0);
                string[] strArray4;
                (strArray4 = strArray1)[0] = strArray4[0] + "' OR pos_sale_detail.sale_date='" + selectedDate.ToString("yyyy-MM-dd") + "') ";
                strArray1[1] = "4";
            }
            else if (this.cbxCondi.Text == "Ave of Last Week")
            {
                num1 = (int)(selectedDate.DayOfWeek) + 1;
                selectedDate = selectedDate.AddDays((double)-num1);
                strArray1[0] = "' AND '" + selectedDate.ToString("yyyy-MM-dd") + "'";
                selectedDate = selectedDate.AddDays(-6.0);
                strArray1[0] = " AND pos_sale_detail.sale_date BETWEEN '" + selectedDate.ToString("yyyy-MM-dd") + strArray1[0];
                strArray1[1] = "7";
            }
            else if (this.cbxCondi.Text == "Ave of Last 4 Weeks")
            {

                num1 = (int)selectedDate.DayOfWeek;
                selectedDate = selectedDate.AddDays((double)-num1);
                strArray1[0] = "' AND '" + selectedDate.ToString("yyyy-MM-dd") + "'";
                selectedDate = selectedDate.AddDays(-27.0);
                strArray1[0] = " AND pos_sale_detail.sale_date BETWEEN '" + selectedDate.ToString("yyyy-MM-dd") + strArray1[0];
                strArray1[1] = "28";
            }
            else if (this.cbxCondi.Text == "Ave of Year to Date")
            {
                int dayOfYear = selectedDate.DayOfYear;
                selectedDate = selectedDate.AddDays(-1.0);
                strArray1[0] = "' AND '" + selectedDate.ToString("yyyy-MM-dd") + "'";
                selectedDate = selectedDate.AddDays((double)(2 - dayOfYear));
                strArray1[0] = " AND pos_sale_detail.sale_date BETWEEN '" + selectedDate.ToString("yyyy-MM-dd") + strArray1[0];
                int num2 = dayOfYear - 1;
                strArray1[1] = num2.ToString();
            }
            else if (this.cbxCondi.Text == "Ave of Last Year")
            {
                selectedDate = selectedDate.AddYears(-1);
                strArray1[0] = " AND pos_sale_detail.sale_date BETWEEN '" + selectedDate.ToString("yyyy") + "-01-01' AND '" + selectedDate.ToString("yyyy") + "-12-31'";
                selectedDate = new DateTime(selectedDate.Year, 12, 31);
                strArray1[1] = selectedDate.DayOfYear.ToString();
            }
            return strArray1;
        }

        private void ConstructQuery()
        {
            try
            {
                this.connDB = new Connect();
                DateTime selectedDate = DtpDate.Value;
                string[] compCond = CompDateCond(selectedDate);
                string compareCondition = compCond[0];
                int compareFactor = int.Parse(compCond[1]);

                string paymentFilter = "";
                if (rbCash.Checked) paymentFilter = "AND cashsale=1";
                else if (rbCheck.Checked) paymentFilter = "AND cashsale=2";
                else if (rbFpos.Checked) paymentFilter = "AND cashsale=0";

                string procName = "";
                if (rbAmount.Checked) procName = "GetStatisticsAmount";
                else if (rbSales.Checked) procName = "GetStatisticsSales";
                else if (rbMargin.Checked) procName = "GetStatisticsMargin";

                using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                {
                    conn.Open();

                    // Present data
                    using (SqlCommand cmdPresent = new SqlCommand(procName, conn))
                    {
                        cmdPresent.CommandType = CommandType.StoredProcedure;
                        cmdPresent.Parameters.AddWithValue("@SaleDate", selectedDate.Date);
                        cmdPresent.Parameters.AddWithValue("@CompareCondition", ""); // Not used for present
                        cmdPresent.Parameters.AddWithValue("@CompareFactor", 1); // Not used for present
                        cmdPresent.Parameters.AddWithValue("@PaymentFilter", paymentFilter);
                        cmdPresent.Parameters.AddWithValue("@IsCompare", 0);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdPresent))
                        {
                            DataTable dtPresent = new DataTable();
                            adapter.Fill(dtPresent);
                            dgPresent.DataSource = dtPresent;
                        }
                    }

                    // Compare data
                    using (SqlCommand cmdCompare = new SqlCommand(procName, conn))
                    {
                        cmdCompare.CommandType = CommandType.StoredProcedure;
                        cmdCompare.Parameters.AddWithValue("@SaleDate", selectedDate.Date); // Used if needed
                        cmdCompare.Parameters.AddWithValue("@CompareCondition", compareCondition);
                        cmdCompare.Parameters.AddWithValue("@CompareFactor", compareFactor);
                        cmdCompare.Parameters.AddWithValue("@PaymentFilter", paymentFilter);
                        cmdCompare.Parameters.AddWithValue("@IsCompare", 1);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdCompare))
                        {
                            DataTable dtCompare = new DataTable();
                            adapter.Fill(dtCompare);
                            dgCompare.DataSource = dtCompare;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error constructing query: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error constructing statistics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void DtpDate_ValueChanged(object sender, System.EventArgs e)
        {
            ConstructQuery();
            FormatDG();
        }

        private void FormatDG()
        {
            dgPresent.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPresent.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPresent.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgPresent.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPresent.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPresent.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPresent.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgPresent.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dgPresent.Columns[3].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dgPresent.Columns[1].DefaultCellStyle.Format = "####0";
            dgPresent.Columns[2].DefaultCellStyle.Format = "C";
            dgPresent.Columns[3].DefaultCellStyle.Format = "C";
            dgCompare.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgCompare.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgCompare.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgCompare.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompare.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompare.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgCompare.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dgCompare.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Italic, GraphicsUnit.Point, 0);
            dgCompare.Columns[3].DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dgCompare.Columns[1].DefaultCellStyle.Format = "####0";
            dgCompare.Columns[2].DefaultCellStyle.Format = "C";
            dgCompare.Columns[3].DefaultCellStyle.Format = "C";
        }

        private void Initiate()
        {
            this.ConstructQuery();
            this.FormatDG();
        }

        #region components

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlFun = new System.Windows.Forms.Panel();
            this.BtnItem = new QiPOS.CustomButton();
            this.customButton1 = new QiPOS.CustomButton();
            this.rbSales = new System.Windows.Forms.RadioButton();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.pnlPay = new System.Windows.Forms.Panel();
            this.rbFpos = new System.Windows.Forms.RadioButton();
            this.rbCheck = new System.Windows.Forms.RadioButton();
            this.rbCash = new System.Windows.Forms.RadioButton();
            this.rbAll = new System.Windows.Forms.RadioButton();
            this.rbMargin = new System.Windows.Forms.RadioButton();
            this.rbAmount = new System.Windows.Forms.RadioButton();
            this.cbxCondi = new System.Windows.Forms.ComboBox();
            this.DtpDate = new System.Windows.Forms.DateTimePicker();
            this.dgPresent = new System.Windows.Forms.DataGridView();
            this.dgCompare = new System.Windows.Forms.DataGridView();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFun.SuspendLayout();
            this.pnlPay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPresent)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompare)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFun
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pnlFun, 2);
            this.pnlFun.Controls.Add(this.BtnItem);
            this.pnlFun.Controls.Add(this.customButton1);
            this.pnlFun.Controls.Add(this.rbSales);
            this.pnlFun.Controls.Add(this.lbl4);
            this.pnlFun.Controls.Add(this.lbl3);
            this.pnlFun.Controls.Add(this.pnlPay);
            this.pnlFun.Controls.Add(this.rbMargin);
            this.pnlFun.Controls.Add(this.rbAmount);
            this.pnlFun.Controls.Add(this.cbxCondi);
            this.pnlFun.Controls.Add(this.DtpDate);
            this.pnlFun.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFun.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlFun.Location = new System.Drawing.Point(0, 687);
            this.pnlFun.Margin = new System.Windows.Forms.Padding(0);
            this.pnlFun.Name = "pnlFun";
            this.pnlFun.Size = new System.Drawing.Size(1153, 90);
            this.pnlFun.TabIndex = 0;
            // 
            // BtnItem
            // 
            this.BtnItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.BtnItem.CornerRadius = 40;
            this.BtnItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnItem.ForeColor = System.Drawing.Color.Black;
            this.BtnItem.Location = new System.Drawing.Point(647, 6);
            this.BtnItem.Name = "BtnItem";
            this.BtnItem.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
            | QiPOS.Corners.BottomLeft)
            | QiPOS.Corners.BottomRight)));
            this.BtnItem.Size = new System.Drawing.Size(181, 35);
            this.BtnItem.TabIndex = 142;
            this.BtnItem.Text = "Sep Item";
            this.BtnItem.Click += new System.EventHandler(this.BtnItem_Click);
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(214)))), ((int)(((byte)(214)))));
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Black;
            this.customButton1.Location = new System.Drawing.Point(834, 6);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
            | QiPOS.Corners.BottomLeft)
            | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(181, 35);
            this.customButton1.TabIndex = 142;
            this.customButton1.Text = "Close";
            this.customButton1.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // rbSales
            // 
            this.rbSales.AutoSize = true;
            this.rbSales.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSales.Location = new System.Drawing.Point(310, 50);
            this.rbSales.Name = "rbSales";
            this.rbSales.Size = new System.Drawing.Size(74, 29);
            this.rbSales.TabIndex = 8;
            this.rbSales.TabStop = true;
            this.rbSales.Text = "Sales";
            this.rbSales.UseVisualStyleBackColor = true;
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl4.Location = new System.Drawing.Point(3, 50);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(172, 25);
            this.lbl4.TabIndex = 7;
            this.lbl4.Text = "Sale and Pay Type";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.Location = new System.Drawing.Point(198, 6);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(140, 25);
            this.lbl3.TabIndex = 6;
            this.lbl3.Text = "Compare With";
            // 
            // pnlPay
            // 
            this.pnlPay.Controls.Add(this.rbFpos);
            this.pnlPay.Controls.Add(this.rbCheck);
            this.pnlPay.Controls.Add(this.rbCash);
            this.pnlPay.Controls.Add(this.rbAll);
            this.pnlPay.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPay.Location = new System.Drawing.Point(496, 49);
            this.pnlPay.Name = "pnlPay";
            this.pnlPay.Size = new System.Drawing.Size(356, 38);
            this.pnlPay.TabIndex = 4;
            // 
            // rbFpos
            // 
            this.rbFpos.AutoSize = true;
            this.rbFpos.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFpos.Location = new System.Drawing.Point(163, 3);
            this.rbFpos.Name = "rbFpos";
            this.rbFpos.Size = new System.Drawing.Size(86, 29);
            this.rbFpos.TabIndex = 3;
            this.rbFpos.TabStop = true;
            this.rbFpos.Text = "eftpos";
            this.rbFpos.UseVisualStyleBackColor = true;
            this.rbFpos.CheckedChanged += new System.EventHandler(this.RbFpos_CheckedChanged);
            // 
            // rbCheck
            // 
            this.rbCheck.AutoSize = true;
            this.rbCheck.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCheck.Location = new System.Drawing.Point(256, 3);
            this.rbCheck.Name = "rbCheck";
            this.rbCheck.Size = new System.Drawing.Size(94, 29);
            this.rbCheck.TabIndex = 2;
            this.rbCheck.TabStop = true;
            this.rbCheck.Text = "cheque";
            this.rbCheck.UseVisualStyleBackColor = true;
            this.rbCheck.CheckedChanged += new System.EventHandler(this.RbCheck_CheckedChanged);
            // 
            // rbCash
            // 
            this.rbCash.AutoSize = true;
            this.rbCash.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCash.Location = new System.Drawing.Point(77, 3);
            this.rbCash.Name = "rbCash";
            this.rbCash.Size = new System.Drawing.Size(68, 29);
            this.rbCash.TabIndex = 1;
            this.rbCash.Text = "cash";
            this.rbCash.UseVisualStyleBackColor = true;
            this.rbCash.CheckedChanged += new System.EventHandler(this.RbCash_CheckedChanged);
            // 
            // rbAll
            // 
            this.rbAll.AutoSize = true;
            this.rbAll.Checked = true;
            this.rbAll.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAll.Location = new System.Drawing.Point(3, 3);
            this.rbAll.Name = "rbAll";
            this.rbAll.Size = new System.Drawing.Size(63, 29);
            this.rbAll.TabIndex = 0;
            this.rbAll.TabStop = true;
            this.rbAll.Text = "ALL";
            this.rbAll.UseVisualStyleBackColor = true;
            this.rbAll.CheckedChanged += new System.EventHandler(this.RbAll_CheckedChanged);
            // 
            // rbMargin
            // 
            this.rbMargin.AutoSize = true;
            this.rbMargin.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMargin.Location = new System.Drawing.Point(400, 50);
            this.rbMargin.Name = "rbMargin";
            this.rbMargin.Size = new System.Drawing.Size(95, 29);
            this.rbMargin.TabIndex = 3;
            this.rbMargin.Text = "Margin";
            this.rbMargin.UseVisualStyleBackColor = true;
            this.rbMargin.CheckedChanged += new System.EventHandler(this.RbMargin_CheckedChanged);
            // 
            // rbAmount
            // 
            this.rbAmount.AutoSize = true;
            this.rbAmount.Checked = true;
            this.rbAmount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAmount.Location = new System.Drawing.Point(203, 50);
            this.rbAmount.Name = "rbAmount";
            this.rbAmount.Size = new System.Drawing.Size(103, 29);
            this.rbAmount.TabIndex = 2;
            this.rbAmount.TabStop = true;
            this.rbAmount.Text = "Amount";
            this.rbAmount.UseVisualStyleBackColor = true;
            this.rbAmount.CheckedChanged += new System.EventHandler(this.RbAmount_CheckedChanged);
            // 
            // cbxCondi
            // 
            this.cbxCondi.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCondi.FormattingEnabled = true;
            this.cbxCondi.Items.AddRange(new object[] {
            "Same Day of Last Week",
            "Same Day of Last 4 Weeks",
            "Ave of Last Week",
            "Ave of Last 4 Weeks",
            "Ave of Year to Date",
            "Ave of Last Year"});
            this.cbxCondi.Location = new System.Drawing.Point(347, 6);
            this.cbxCondi.Name = "cbxCondi";
            this.cbxCondi.Size = new System.Drawing.Size(294, 33);
            this.cbxCondi.TabIndex = 1;
            this.cbxCondi.Text = "Same Day of Last Week";
            this.cbxCondi.SelectedIndexChanged += new System.EventHandler(this.CbxCondi_SelectedIndexChanged);
            // 
            // DtpDate
            // 
            this.DtpDate.CustomFormat = "ddd dd MMM yy";
            this.DtpDate.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpDate.Location = new System.Drawing.Point(3, 3);
            this.DtpDate.Name = "DtpDate";
            this.DtpDate.Size = new System.Drawing.Size(194, 33);
            this.DtpDate.TabIndex = 0;
            this.DtpDate.ValueChanged += new System.EventHandler(this.DtpDate_ValueChanged);
            // 
            // dgPresent
            // 
            this.dgPresent.AllowUserToAddRows = false;
            this.dgPresent.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            this.dgPresent.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgPresent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgPresent.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgPresent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgPresent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgPresent.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgPresent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPresent.Location = new System.Drawing.Point(0, 30);
            this.dgPresent.Margin = new System.Windows.Forms.Padding(0);
            this.dgPresent.Name = "dgPresent";
            this.dgPresent.ReadOnly = true;
            this.dgPresent.RowHeadersVisible = false;
            this.dgPresent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPresent.Size = new System.Drawing.Size(576, 657);
            this.dgPresent.TabIndex = 1;
            // 
            // dgCompare
            // 
            this.dgCompare.AllowUserToAddRows = false;
            this.dgCompare.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightYellow;
            this.dgCompare.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgCompare.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgCompare.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgCompare.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgCompare.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightCyan;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCompare.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgCompare.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCompare.Location = new System.Drawing.Point(576, 30);
            this.dgCompare.Margin = new System.Windows.Forms.Padding(0);
            this.dgCompare.Name = "dgCompare";
            this.dgCompare.ReadOnly = true;
            this.dgCompare.RowHeadersVisible = false;
            this.dgCompare.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCompare.Size = new System.Drawing.Size(577, 657);
            this.dgCompare.TabIndex = 2;
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.Location = new System.Drawing.Point(3, 0);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(97, 25);
            this.lbl1.TabIndex = 3;
            this.lbl1.Text = "Daily Sale";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.Location = new System.Drawing.Point(579, 0);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(218, 25);
            this.lbl2.TabIndex = 4;
            this.lbl2.Text = "Same Day of Last Week";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.lbl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pnlFun, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.dgCompare, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbl2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgPresent, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1153, 777);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // FrmStatistics
            // 
            this.ClientSize = new System.Drawing.Size(1153, 777);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmStatistics";
            this.Text = "Sales Statistics";
            this.pnlFun.ResumeLayout(false);
            this.pnlFun.PerformLayout();
            this.pnlPay.ResumeLayout(false);
            this.pnlPay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPresent)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCompare)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
            base.Dispose();
        }


        #endregion components

        private void RbAll_CheckedChanged(object sender, System.EventArgs e)
        {
            ConstructQuery();
            FormatDG();
        }

        private void RbAmount_CheckedChanged(object sender, System.EventArgs e)
        {
            ConstructQuery();
            FormatDG();
        }

        private void RbCash_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ConstructQuery();
            this.FormatDG();
        }

        private void RbCheck_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ConstructQuery();
            this.FormatDG();
        }

        private void RbFpos_CheckedChanged(object sender, System.EventArgs e)
        {
            ConstructQuery();
            FormatDG();
        }

        private void RbMargin_CheckedChanged(object sender, System.EventArgs e)
        {
            this.ConstructQuery();
            this.FormatDG();
        }
        private void SepItem(int stock_id)
        {
            try
            {
                this.connDB = new Connect();
                DateTime selectedDate = DtpDate.Value;
                string[] compCond = CompDateCond(selectedDate);
                string compareCondition = compCond[0];
                int compareFactor = int.Parse(compCond[1]);

                using (SqlConnection conn = new SqlConnection(connDB.ConnectionStr))
                {
                    conn.Open();

                    // Present data
                    using (SqlCommand cmdPresent = new SqlCommand("GetItemStatistics", conn))
                    {
                        cmdPresent.CommandType = CommandType.StoredProcedure;
                        cmdPresent.Parameters.AddWithValue("@StockId", stock_id);
                        cmdPresent.Parameters.AddWithValue("@SaleDate", selectedDate.Date);
                        cmdPresent.Parameters.AddWithValue("@CompareCondition", ""); // Not used
                        cmdPresent.Parameters.AddWithValue("@CompareFactor", 1); // Not used
                        cmdPresent.Parameters.AddWithValue("@IsCompare", 0);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdPresent))
                        {
                            DataTable dtPresent = new DataTable();
                            adapter.Fill(dtPresent);
                            dgPresent.DataSource = dtPresent;
                        }
                    }

                    // Compare data
                    using (SqlCommand cmdCompare = new SqlCommand("GetItemStatistics", conn))
                    {
                        cmdCompare.CommandType = CommandType.StoredProcedure;
                        cmdCompare.Parameters.AddWithValue("@StockId", stock_id);
                        cmdCompare.Parameters.AddWithValue("@SaleDate", selectedDate.Date); // Used if needed
                        cmdCompare.Parameters.AddWithValue("@CompareCondition", compareCondition);
                        cmdCompare.Parameters.AddWithValue("@CompareFactor", compareFactor);
                        cmdCompare.Parameters.AddWithValue("@IsCompare", 1);
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmdCompare))
                        {
                            DataTable dtCompare = new DataTable();
                            adapter.Fill(dtCompare);
                            dgCompare.DataSource = dtCompare;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error retrieving item statistics: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving item details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

