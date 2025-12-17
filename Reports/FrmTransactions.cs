using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmTransactions : Form
    {
        #region declarations

        private IContainer components;
        private Label lblPeriod;
        private DateTimePicker DtpReportAt;
        private ComboBox cbRange;
        private Panel pnlControl;
        private Label lblReport;
        private Timer tmDelay;
        private Label lblY;
        private Label lblX;
        private ComboBox cbxCat;
        private Label lblCat;
        private Label lblDate;
        private Label lblTotal;
        private string queryStr;
        private string reportPeriod;
        private string userCondiStr;
        private int which;
        private int trans_id;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private CustomButton customButton3;
        private CustomButton customButton4;

        public int TransID
        {
            get { return trans_id; }
            set { trans_id = value; }
        }

        #endregion declarations

        public FrmTransactions(bool userOnly)
        {
            InitializeComponent();
            Initiate();
            userCondiStr = "";
            if (!userOnly)
                return;
            userCondiStr = " AND pos_sale.user_id=0";
        }

        #region components

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.DtpReportAt = new System.Windows.Forms.DateTimePicker();
            this.cbRange = new System.Windows.Forms.ComboBox();
            this.pnlControl = new System.Windows.Forms.Panel();
            this.cbxCat = new System.Windows.Forms.ComboBox();
            this.lblCat = new System.Windows.Forms.Label();
            this.lblReport = new System.Windows.Forms.Label();
            this.tmDelay = new System.Windows.Forms.Timer(this.components);
            this.lblY = new System.Windows.Forms.Label();
            this.lblX = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.customButton1 = new QiPOS.CustomButton();
            this.customButton2 = new QiPOS.CustomButton();
            this.customButton3 = new QiPOS.CustomButton();
            this.customButton4 = new QiPOS.CustomButton();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();

            //
            // lblPeriod
            //
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriod.ForeColor = System.Drawing.Color.Blue;
            this.lblPeriod.Location = new System.Drawing.Point(8, 72);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(76, 25);
            this.lblPeriod.TabIndex = 102;
            this.lblPeriod.Text = "Period:";

            //
            // DtpReportAt
            //
            this.DtpReportAt.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpReportAt.CalendarMonthBackground = System.Drawing.SystemColors.Control;
            this.DtpReportAt.CustomFormat = "dd MMM yy";
            this.DtpReportAt.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DtpReportAt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DtpReportAt.Location = new System.Drawing.Point(13, 35);
            this.DtpReportAt.Name = "DtpReportAt";
            this.DtpReportAt.Size = new System.Drawing.Size(185, 33);
            this.DtpReportAt.TabIndex = 29;

            //
            // cbRange
            //
            this.cbRange.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRange.FormattingEnabled = true;
            this.cbRange.Location = new System.Drawing.Point(13, 99);
            this.cbRange.Name = "cbRange";
            this.cbRange.Size = new System.Drawing.Size(185, 33);
            this.cbRange.TabIndex = 1;

            //
            // pnlControl
            //
            this.pnlControl.BackColor = System.Drawing.Color.LightCyan;
            this.pnlControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControl.Controls.Add(this.customButton2);
            this.pnlControl.Controls.Add(this.customButton4);
            this.pnlControl.Controls.Add(this.customButton3);
            this.pnlControl.Controls.Add(this.customButton1);
            this.pnlControl.Controls.Add(this.cbxCat);
            this.pnlControl.Controls.Add(this.lblCat);
            this.pnlControl.Controls.Add(this.lblPeriod);
            this.pnlControl.Controls.Add(this.DtpReportAt);
            this.pnlControl.Controls.Add(this.cbRange);
            this.pnlControl.Controls.Add(this.lblReport);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(795, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(210, 638);
            this.pnlControl.TabIndex = 31;

            //
            // cbxCat
            //
            this.cbxCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCat.FormattingEnabled = true;
            this.cbxCat.Location = new System.Drawing.Point(13, 167);
            this.cbxCat.Name = "cbxCat";
            this.cbxCat.Size = new System.Drawing.Size(185, 33);
            this.cbxCat.TabIndex = 119;

            //
            // lblCat
            //
            this.lblCat.AutoSize = true;
            this.lblCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCat.ForeColor = System.Drawing.Color.Blue;
            this.lblCat.Location = new System.Drawing.Point(8, 139);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(93, 25);
            this.lblCat.TabIndex = 120;
            this.lblCat.Text = "Category";

            //
            // lblReport
            //
            this.lblReport.AutoSize = true;
            this.lblReport.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReport.ForeColor = System.Drawing.Color.Blue;
            this.lblReport.Location = new System.Drawing.Point(8, 7);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(103, 25);
            this.lblReport.TabIndex = 28;
            this.lblReport.Text = "Report At:";

            //
            // tmDelay
            //
            this.tmDelay.Tick += new System.EventHandler(this.TmDelay_Tick);

            //
            // lblY
            //
            this.lblY.AutoSize = true;
            this.lblY.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblY.ForeColor = System.Drawing.Color.Brown;
            this.lblY.Location = new System.Drawing.Point(55, 35);
            this.lblY.Name = "lblY";
            this.lblY.Size = new System.Drawing.Size(85, 25);
            this.lblY.TabIndex = 32;
            this.lblY.Text = "Amount";

            //
            // lblX
            //
            this.lblX.AutoSize = true;
            this.lblX.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblX.ForeColor = System.Drawing.Color.Brown;
            this.lblX.Location = new System.Drawing.Point(899, 565);
            this.lblX.Name = "lblX";
            this.lblX.Size = new System.Drawing.Size(55, 25);
            this.lblX.TabIndex = 33;
            this.lblX.Text = "Time";

            //
            // lblDate
            //
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Blue;
            this.lblDate.Location = new System.Drawing.Point(288, 35);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(53, 25);
            this.lblDate.TabIndex = 34;
            this.lblDate.Text = "Date";

            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Blue;
            this.lblTotal.Location = new System.Drawing.Point(553, 35);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(62, 25);
            this.lblTotal.TabIndex = 35;
            this.lblTotal.Text = "Total:";

            //
            // customButton1
            //
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Blue;
            this.customButton1.Location = new System.Drawing.Point(26, 422);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(165, 40);
            this.customButton1.TabIndex = 142;
            this.customButton1.Text = "Close";
            this.customButton1.Click += new System.EventHandler(this.BtnClose_Click);

            //
            // customButton2
            //
            this.customButton2.BackColor = System.Drawing.SystemColors.Control;
            this.customButton2.CornerRadius = 40;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Blue;
            this.customButton2.Location = new System.Drawing.Point(26, 251);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(165, 40);
            this.customButton2.TabIndex = 142;
            this.customButton2.Text = "Sales";
            this.customButton2.Click += new System.EventHandler(this.BtnSales_Click);

            //
            // customButton3
            //
            this.customButton3.BackColor = System.Drawing.SystemColors.Control;
            this.customButton3.CornerRadius = 40;
            this.customButton3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton3.ForeColor = System.Drawing.Color.Blue;
            this.customButton3.Location = new System.Drawing.Point(26, 308);
            this.customButton3.Name = "customButton3";
            this.customButton3.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton3.Size = new System.Drawing.Size(165, 40);
            this.customButton3.TabIndex = 142;
            this.customButton3.Text = "No. Items";
            this.customButton3.Click += new System.EventHandler(this.BtnItem_Click);

            //
            // customButton4
            //
            this.customButton4.BackColor = System.Drawing.SystemColors.Control;
            this.customButton4.CornerRadius = 40;
            this.customButton4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton4.ForeColor = System.Drawing.Color.Blue;
            this.customButton4.Location = new System.Drawing.Point(26, 365);
            this.customButton4.Name = "customButton4";
            this.customButton4.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton4.Size = new System.Drawing.Size(165, 40);
            this.customButton4.TabIndex = 142;
            this.customButton4.Text = "No. Trans";
            this.customButton4.Click += new System.EventHandler(this.BtnTrans_Click);

            //
            // FrmTrans
            //
            this.ClientSize = new System.Drawing.Size(1005, 638);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblX);
            this.Controls.Add(this.lblY);
            this.Controls.Add(this.pnlControl);
            this.Location = new System.Drawing.Point(0, 30);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmTrans";
            this.Text = "Transactions";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmTrans_Paint);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion components

        private void Initiate()
        {
            try
            {
                cbRange.DataSource = new string[]
                {
            "Today", "This Week", "Month to Date", "Quarter to Date",
            "Year to Date", "Last Week", "Last Month", "Last Quarter", "Last Year"
                };
                Connect connect = new Connect();
                using (SqlConnection conn = new SqlConnection(connect.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("GetProductCategories", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            connect.aTable = new DataTable();
                            adapter.Fill(connect.aTable);
                            DataRow row = connect.aTable.NewRow();
                            row[0] = "  ALL  ";
                            row[1] = 0;
                            row[2] = 0;
                            connect.aTable.Rows.InsertAt(row, 0);
                            cbxCat.DataSource = connect.aTable;
                            cbxCat.DisplayMember = "acc_name";
                            cbxCat.ValueMember = "acc_number";
                        }
                    }
                }
                Numtrans();
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

        private void FrmTrans_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2f);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Point[] points1 = new Point[3]
      {
        new Point(100, 80),
        new Point(100, 580),
        new Point(900, 580)
      };
            e.Graphics.DrawLines(pen, points1);
            Point[] points2 = new Point[3]
      {
        new Point(95, 100),
        new Point(100, 80),
        new Point(105, 100)
      };
            e.Graphics.DrawPolygon(pen, points2);
            e.Graphics.FillPolygon((Brush)solidBrush, points2);
            Point[] points3 = new Point[3]
      {
        new Point(880, 575),
        new Point(900, 580),
        new Point(880, 585)
      };
            e.Graphics.DrawPolygon(pen, points3);
            e.Graphics.FillPolygon((Brush)solidBrush, points3);
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            lblY.Text = "Amount $ ";
            Invalidate();
            tmDelay.Enabled = true;
            which = 1;
        }

        private void BtnItem_Click(object sender, EventArgs e)
        {
            lblY.Text = "No. Items";
            Invalidate();
            tmDelay.Enabled = true;
            which = 2;
        }

        private void BtnTrans_Click(object sender, EventArgs e)
        {
            Numtrans();
        }

        private void Numtrans()
        {
            lblY.Text = "Transactions";
            Invalidate();
            tmDelay.Enabled = true;
            which = 3;
        }

        private void TmDelay_Tick(object sender, EventArgs e)
        {
            tmDelay.Enabled = false;
            DrawChart();
        }

        private void DrawChart()
        {
            try
            {
                Connect connect = new Connect();
                string dateRange = "";
                DateCondition(ref dateRange, ref reportPeriod);
                lblDate.Text = reportPeriod;

                string procName = which switch
                {
                    1 => "GetSalesByHour",
                    2 => "GetItemsByHour",
                    3 => "GetTransactionsByHour",
                    _ => throw new ArgumentException("Invalid chart type")
                };

                using (SqlConnection conn = new SqlConnection(connect.ConnectionStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        var dateRangeObj = new DateRange(cbRange.SelectedValue?.ToString(), DtpReportAt.Value.Date);
                        cmd.Parameters.AddWithValue("@StartDate", dateRangeObj.fromDay);
                        cmd.Parameters.AddWithValue("@EndDate", dateRangeObj.toDay);
                        cmd.Parameters.AddWithValue("@UserId", userCondiStr.Contains("user_id=0") ? 0 : (object)DBNull.Value);

                        // Apply category filter
                        //if (cbxCat.SelectedValue?.ToString() != "0")
                        //{
                        //    cmd.Parameters["@AccNumber"].Value = Convert.ToInt32(cbxCat.SelectedValue);
                        //}

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            int num1 = 1;
                            int num2 = 0;
                            foreach (DataRow row in dataTable.Rows)
                            {
                                string str = row["NoTrans"].ToString();
                                if (str.IndexOf(".") >= 0)
                                    str = str.Substring(0, str.IndexOf("."));
                                int num3 = Convert.ToInt32(str);
                                num2 += num3;
                                if (num3 > num1)
                                    num1 = num3;
                            }

                            int num4 = 45000000 / num1;
                            Point[] points = new Point[2 * dataTable.Rows.Count + 2];
                            Graphics graphics = CreateGraphics();
                            Font font = new Font("Microsoft Sans Serif", 12f, FontStyle.Bold);
                            SolidBrush solidBrush1 = new SolidBrush(Color.Blue);
                            points[0] = new Point(101, 579);

                            for (int index = 0; index < dataTable.Rows.Count; ++index)
                            {
                                string str = dataTable.Rows[index]["NoTrans"].ToString();
                                if (str.IndexOf(".") >= 0)
                                    str = str.Substring(0, str.IndexOf("."));
                                int num3 = Convert.ToInt32(str);
                                points[2 * index + 1] = new Point(101 + index * 50, 580 - num3 * num4 / 100000);
                                points[2 * index + 2] = new Point(101 + (index + 1) * 50, 580 - num3 * num4 / 100000);
                                string s1 = dataTable.Rows[index]["Hours"].ToString() + ":00";
                                graphics.DrawString(s1, font, (Brush)solidBrush1, (float)(80 + index * 50), 585f);
                                string s2 = num3.ToString();
                                graphics.DrawString(s2, font, (Brush)solidBrush1, (float)(100 + index * 50), (float)(580 - num3 * num4 / 100000 - 25));
                            }

                            points[2 * dataTable.Rows.Count + 1] = new Point(101 + dataTable.Rows.Count * 50, 579);
                            int num5 = 6;
                            if (dataTable.Rows.Count > 0)
                                num5 = Convert.ToInt32(dataTable.Rows[dataTable.Rows.Count - 1]["Hours"].ToString()) + 1;
                            string s = num5.ToString() + ":00";
                            graphics.DrawString(s, font, (Brush)solidBrush1, (float)(80 + dataTable.Rows.Count * 50), 585f);

                            Pen pen = new Pen(Color.Black, 2f);
                            SolidBrush solidBrush2 = new SolidBrush(Color.Azure);
                            graphics.DrawPolygon(pen, points);
                            if (which == 1)
                                solidBrush2 = new SolidBrush(Color.LightGoldenrodYellow);
                            if (which == 2)
                                solidBrush2 = new SolidBrush(Color.Honeydew);
                            graphics.FillPolygon((Brush)solidBrush2, points);
                            pen.Dispose();
                            graphics.Dispose();
                            lblTotal.Text = "Total: " + num2;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error drawing chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error drawing chart: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DateCondition(ref string dateRange, ref string reportPeriod)
        {
            DateTime currentDay = DtpReportAt.Value.Date;
            DateRange dateRange1 = new DateRange(cbRange.SelectedValue.ToString(), currentDay);
            dateRange = dateRange1.SQLDateRange;
            reportPeriod = "From " + dateRange1.fromDay.ToString("dd MMM yyyy") + " To " + dateRange1.toDay.ToString("dd MMM yyyy");
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
            base.Dispose();
        }
    }
}

