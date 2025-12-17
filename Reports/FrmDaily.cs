using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmDaily : Form
    {
        #region declarations

        private ComboBox cbRange;
        private ComboBox cbxBase;
        private ComboBox cbxCat;
        private System.ComponentModel.IContainer components;
        private DateTimePicker DtpReportAt;
        private Label label1;
        private Label lblCat;
        private Label lblDate;
        private Label lblMax;
        private Label lblMin;
        private Label lblPeriod;
        private Label lblReport;
        private Label lblTotal;
        private Panel pnlControl;
        private string queryStr;
        private Timer tmrDraw;
        private string userCondiStr;
        private CustomButton customButton1;
        private CustomButton closeButton;

        #endregion declarations


        private void BtnClose_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void BtnShow_Click(object sender, System.EventArgs e)
        {
            Invalidate();
            tmrDraw.Enabled = true;
        }

        private void DateCondition(ref string dateRange, ref string reportPeriod)
        {
            DateTime currentDay = DateTime.Today;
            DateRange dateRange1 = new DateRange(this.cbRange.SelectedValue.ToString(), currentDay);
            dateRange = dateRange1.SQLDateRange;
            reportPeriod = "From " + dateRange1.fromDay.ToString("dd MMM yyyy") + " To " + dateRange1.toDay.ToString("dd MMM yyyy");
        }

        private async void DrawChart()
        {
            try
            {
                string s1 = "";
                string s2 = "";
                DateCondition(ref s2, ref s1); // Existing method to set date range
                lblDate.Text = s1;

                string groupBy = cbxBase.SelectedValue?.ToString() ?? "Day";
                int? accNumber = cbxCat.SelectedValue?.ToString() == "0" ? null : Convert.ToInt32(cbxCat.SelectedValue);
                int userId = 0; // Hardcoded as per original
                Connect connect = new Connect();
                using (var connection = new SqlConnection(connect.ConnectionStr))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("GetSalesChartData", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StartDate", s2); // Ensure s2 is a valid DATE
                        command.Parameters.AddWithValue("@EndDate", s2);   // Adjust if DateCondition provides separate start/end
                        if (accNumber.HasValue)
                            command.Parameters.AddWithValue("@AccNumber", accNumber.Value);
                        else
                            command.Parameters.AddWithValue("@AccNumber", DBNull.Value);
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@GroupBy", groupBy);

                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Calculate max, min, total
                            decimal maxSales = 0M;
                            decimal minSales = decimal.MaxValue;
                            decimal totalSales = 0M;

                            foreach (DataRow row in dataTable.Rows)
                            {
                                decimal sales = Convert.ToDecimal(row["Sales"]);
                                maxSales = Math.Max(maxSales, sales);
                                minSales = Math.Min(minSales, sales);
                                totalSales += sales;
                            }

                            lblMax.Text = $"${(int)maxSales}";
                            lblMin.Text = $"${(int)minSales}";
                            lblTotal.Text = $"Total: ${(int)totalSales}";

                            // Chart drawing
                            var points = new Point[dataTable.Rows.Count];
                            for (int i = 0; i < dataTable.Rows.Count; i++)
                            {
                                decimal sales = Convert.ToDecimal(dataTable.Rows[i]["Sales"]);
                                int x = (i * 850 / dataTable.Rows.Count) + 100;
                                int y = 640 - (int)(sales * 560M / (maxSales == 0 ? 1 : maxSales));
                                points[i] = new Point(x, y);
                            }

                            using (Graphics graphics = CreateGraphics())
                            using (var font = new Font("Microsoft Sans Serif", 12.0F, FontStyle.Bold))
                            using (var brush = new SolidBrush(Color.Blue))
                            using (var pen = new Pen(Color.Red, 2.0F))
                            {
                                if (points.Length > 1)
                                    graphics.DrawCurve(pen, points);

                                using (var bluePen = new Pen(Color.Blue, 2.0F))
                                {
                                    for (int i = 0; i < points.Length; i++)
                                    {
                                        Point p1 = new Point((i * 850 / dataTable.Rows.Count) + 100, 600);
                                        Point p2 = new Point((i * 850 / dataTable.Rows.Count) + 100, 650);
                                        graphics.DrawLine(bluePen, p1, p2);

                                        int labelStep = points.Length <= 20 ? 1 : points.Length <= 50 ? 5 : 10;
                                        if ((i + 1) % labelStep == 0)
                                            graphics.DrawString((i + 1).ToString(), font, brush, (i * 850 / dataTable.Rows.Count) + 100, 660.0F);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sales Report Data Exception: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmDaily_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 2f);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Point[] points1 = new Point[3]
              {
                new Point(100, 80),
                new Point(100, 650),
                new Point(950, 650)
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
                new Point(920, 645),
                new Point(950, 650),
                new Point(920, 655)
              };
            e.Graphics.DrawPolygon(pen, points3);
            e.Graphics.FillPolygon((Brush)solidBrush, points3);
        }



        private void TmrDraw_Tick(object sender, System.EventArgs e)
        {
            this.tmrDraw.Enabled = false;
            this.DrawChart();
        }

        #region components

        protected override void Dispose(bool disposing)
        {
            if (disposing && this.components != null)
                this.components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tmrDraw = new System.Windows.Forms.Timer(this.components);
            this.pnlControl = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cbxBase = new System.Windows.Forms.ComboBox();
            this.cbxCat = new System.Windows.Forms.ComboBox();
            this.lblCat = new System.Windows.Forms.Label();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.DtpReportAt = new System.Windows.Forms.DateTimePicker();
            this.cbRange = new System.Windows.Forms.ComboBox();
            this.lblReport = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.closeButton = new QiPOS.CustomButton();
            this.customButton1 = new QiPOS.CustomButton();
            this.pnlControl.SuspendLayout();
            this.SuspendLayout();

            //
            // tmrDraw
            //
            this.tmrDraw.Tick += new System.EventHandler(this.TmrDraw_Tick);

            //
            // pnlControl
            //
            this.pnlControl.BackColor = System.Drawing.Color.LightCyan;
            this.pnlControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlControl.Controls.Add(this.customButton1);
            this.pnlControl.Controls.Add(this.closeButton);
            this.pnlControl.Controls.Add(this.label1);
            this.pnlControl.Controls.Add(this.cbxBase);
            this.pnlControl.Controls.Add(this.cbxCat);
            this.pnlControl.Controls.Add(this.lblCat);
            this.pnlControl.Controls.Add(this.lblPeriod);
            this.pnlControl.Controls.Add(this.DtpReportAt);
            this.pnlControl.Controls.Add(this.cbRange);
            this.pnlControl.Controls.Add(this.lblReport);
            this.pnlControl.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlControl.Location = new System.Drawing.Point(676, 0);
            this.pnlControl.Name = "pnlControl";
            this.pnlControl.Size = new System.Drawing.Size(228, 718);
            this.pnlControl.TabIndex = 32;

            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(15, 218);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 25);
            this.label1.TabIndex = 122;
            this.label1.Text = "Base on";

            //
            // cbxBase
            //
            this.cbxBase.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxBase.FormattingEnabled = true;
            this.cbxBase.Location = new System.Drawing.Point(20, 249);
            this.cbxBase.Name = "cbxBase";
            this.cbxBase.Size = new System.Drawing.Size(185, 33);
            this.cbxBase.TabIndex = 121;

            //
            // cbxCat
            //
            this.cbxCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCat.FormattingEnabled = true;
            this.cbxCat.Location = new System.Drawing.Point(20, 167);
            this.cbxCat.Name = "cbxCat";
            this.cbxCat.Size = new System.Drawing.Size(185, 33);
            this.cbxCat.TabIndex = 119;

            //
            // lblCat
            //
            this.lblCat.AutoSize = true;
            this.lblCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCat.ForeColor = System.Drawing.Color.Blue;
            this.lblCat.Location = new System.Drawing.Point(15, 139);
            this.lblCat.Name = "lblCat";
            this.lblCat.Size = new System.Drawing.Size(101, 25);
            this.lblCat.TabIndex = 120;
            this.lblCat.Text = "Categrory";

            //
            // lblPeriod
            //
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeriod.ForeColor = System.Drawing.Color.Blue;
            this.lblPeriod.Location = new System.Drawing.Point(15, 72);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(80, 25);
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
            this.DtpReportAt.Location = new System.Drawing.Point(20, 35);
            this.DtpReportAt.Name = "DtpReportAt";
            this.DtpReportAt.Size = new System.Drawing.Size(185, 33);
            this.DtpReportAt.TabIndex = 29;

            //
            // cbRange
            //
            this.cbRange.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbRange.FormattingEnabled = true;
            this.cbRange.Location = new System.Drawing.Point(20, 99);
            this.cbRange.Name = "cbRange";
            this.cbRange.Size = new System.Drawing.Size(185, 33);
            this.cbRange.TabIndex = 1;

            //
            // lblReport
            //
            this.lblReport.AutoSize = true;
            this.lblReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReport.ForeColor = System.Drawing.Color.Blue;
            this.lblReport.Location = new System.Drawing.Point(15, 7);
            this.lblReport.Name = "lblReport";
            this.lblReport.Size = new System.Drawing.Size(108, 25);
            this.lblReport.TabIndex = 28;
            this.lblReport.Text = "Report At:";

            //
            // lblDate
            //
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Blue;
            this.lblDate.Location = new System.Drawing.Point(94, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(53, 25);
            this.lblDate.TabIndex = 35;
            this.lblDate.Text = "Date";

            //
            // lblMin
            //
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.ForeColor = System.Drawing.Color.Blue;
            this.lblMin.Location = new System.Drawing.Point(12, 446);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(47, 25);
            this.lblMin.TabIndex = 36;
            this.lblMin.Text = "Min";

            //
            // lblMax
            //
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMax.ForeColor = System.Drawing.Color.Blue;
            this.lblMax.Location = new System.Drawing.Point(12, 102);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(51, 25);
            this.lblMax.TabIndex = 37;
            this.lblMax.Text = "Max";

            //
            // lblTotal
            //
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Blue;
            this.lblTotal.Location = new System.Drawing.Point(567, 36);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(57, 25);
            this.lblTotal.TabIndex = 38;
            this.lblTotal.Text = "Total";

            //
            // closeButton
            //
            this.closeButton.BackColor = System.Drawing.SystemColors.Control;
            this.closeButton.CornerRadius = 40;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.ForeColor = System.Drawing.Color.Blue;
            this.closeButton.Location = new System.Drawing.Point(20, 393);
            this.closeButton.Name = "closeButton";
            this.closeButton.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.closeButton.Size = new System.Drawing.Size(165, 40);
            this.closeButton.TabIndex = 141;
            this.closeButton.Text = "Close";
            this.closeButton.Click += new System.EventHandler(this.BtnClose_Click);

            //
            // customButton1
            //
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Blue;
            this.customButton1.Location = new System.Drawing.Point(20, 319);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight)
                        | QiPOS.Corners.BottomLeft)
                        | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(165, 40);
            this.customButton1.TabIndex = 141;
            this.customButton1.Text = "Show";
            this.customButton1.Click += new System.EventHandler(this.BtnShow_Click);

            //
            // FrmDaily
            //
            this.ClientSize = new System.Drawing.Size(904, 718);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblMax);
            this.Controls.Add(this.lblMin);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.pnlControl);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmDaily";
            this.Text = "Daily Sales";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmDaily_Paint);
            this.pnlControl.ResumeLayout(false);
            this.pnlControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion components
    }
}

