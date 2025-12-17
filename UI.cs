using System; 
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization; 
using System.Windows.Forms;
using System.Threading.Tasks;

namespace QiPOS
{
    public partial class FrmPos : Form
    {
        private bool CheckCards = false;
        private void InitializePOS()
        {

            conn = new Connect();
            saleService = new SaleService(conn);
            stockService = new StockService(conn);
            refundService = new RefundService(conn);

            try
            {
                SqlParameter[] paramsArray = new SqlParameter[]
                    {
                        new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 }
                    };
                DataTable result = conn.QueryTableSP("dbo.GetSaleCacheByUser", paramsArray);
                CurrentTable = result ?? CreateEmptySaleCacheTable();
                if (dgItemList.Rows.Count > 0)
                    dgItemList.FirstDisplayedScrollingRowIndex  = dgItemList.Rows.Count - 1;
                
            }
            catch (DatabaseUnavailableException ex) { Console.WriteLine($"Database unavailable error: {ex.Message}"); }
            catch (SqlException ex)
            {
                MessageBox.Show($"Database error: {ex.Message}\nInner Exception: {ex.InnerException?.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CurrentTable = CreateEmptySaleCacheTable();
                StartNewSale();
            }
        }

        private DataTable CreateEmptySaleCacheTable()
        {
            var table = new DataTable("pos_sale_cache");
            // Define schema based on pos_sale_cache (inferred from SaleService.cs and DatabaseSchema.txt)
            table.Columns.Add("sale_trans_id", typeof(int));
            table.Columns.Add("stock_id", typeof(int));
            table.Columns.Add("descr", typeof(string));
            table.Columns.Add("acc_number", typeof(int));
            table.Columns.Add("card_id", typeof(int));
            table.Columns.Add("cost", typeof(decimal));
            table.Columns.Add("RRP", typeof(decimal));
            table.Columns.Add("number_items", typeof(int));
            table.Columns.Add("discount", typeof(decimal));
            table.Columns.Add("sales", typeof(decimal));
            table.Columns.Add("GST_collect", typeof(decimal));
            table.Columns.Add("GST_paid", typeof(decimal));
            table.Columns.Add("sale_date", typeof(string)); // or DateTime
            table.Columns.Add("sale_time", typeof(string)); // or TimeSpan
            table.Columns.Add("user_id", typeof(int));
            table.Columns.Add("seq_id", typeof(int));
            return table;
        } 
        private void EndOfSales()
        {
            try
            {
                if (CurrentTable == null || CurrentTable.Rows.Count == 0)
                    return;

                decimal received = CurrencyUtil.SafeToDecimal(txtAmount.Text);
                decimal change;
                bool isCash = rbtCash.Checked;
                if (isCash)
                {
                    OpenCashDrawer();
                }

                int saleId = saleService.FinalizeSale(CurrentTable, isCash, received, out change);
                ResetUiAfterSale();
            }
            catch (Exception ex)
            {
                ErrorLogWriter.Instance.Log(ex, "EndOfSales exception: ");
                MessageBox.Show("An error occurred finalizing the sale:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Clear();
            }
        }

        private void OpenCashDrawer()
        {
            try
            {
                RawPrinterHelper.SendStringToPrinter(printerNameStr, "\x1Bx70\x00\x80\x80");
            }
            catch (Exception ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Failed to open cash drawer");
            }
        }

        private void PrintReceiptFor(int saleId)
        {
            try
            {
                new PrintReceiptCls(saleId.ToString(), printerNameStr).PrintReceiptDirect();
            }
            catch (Exception ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Receipt printing failed");
            }
        }

        private void ResetUiAfterSale()
        {
            StartNewSale();

            // Refresh table to reset DataTable structure properly
            SqlParameter[] paramsArray = new SqlParameter[]
                    {
                        new SqlParameter("@UserId", SqlDbType.Int) { Value = 0 }
                    };
            DataTable result = conn.QueryTableSP("dbo.GetSaleCacheByUser", paramsArray);
            CurrentTable = result;

            dgItemList.Rows.Clear();
            dgItemList.Rows.Add(UIStyles.DefaultRowCount); // Reset rows for new sale


            dgItemList.FirstDisplayedScrollingRowIndex = dgItemList.Rows.Count - 1;
            rBtnoCash.Checked = true;
            DisplayPoleUtil.ClearDisplay(displayNameStr);
        }
        private void RefocusToCat()
        {
            if (txtCat.CanFocus && txtCat.Visible)
            {
                txtCat.Focus();
                txtCat.SelectionStart = txtCat.Text.Length;
            }
        }
        private void ApplyGridDefaults(DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
                col.SortMode = DataGridViewColumnSortMode.NotSortable;

            foreach (DataGridViewBand row in grid.Rows)
                row.ReadOnly = true;

            grid.Columns[0].ReadOnly = true;
            grid.Columns[5].ReadOnly = true;

            grid.ColumnHeadersDefaultCellStyle.Font = UIStyles.FontHeader18;
            grid.DefaultCellStyle.Font = UIStyles.FontCell16;
            grid.Columns[5].DefaultCellStyle.Font = UIStyles.FontHeader18;

            grid.DefaultCellStyle.BackColor = SystemColors.ControlLight;
            grid.DefaultCellStyle.SelectionForeColor = Color.Red;

            grid.CurrentCell = null;

            grid.Columns[2].DefaultCellStyle.Format = "C";
            grid.Columns[4].DefaultCellStyle.Format = "C";
            grid.Columns[5].DefaultCellStyle.Format = "C";
        }

        private void SetReturnInfo()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = DateTime.Today;
            Calendar cal = dfi.Calendar;
            int week = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            week -= 1;
            rich.Text = UIStyles.ZineReturnWeek + week.ToString() + UIStyles.CarriageReturnLineFeed;
        }

        private void DtpCurrent_ValueChanged(object sender, EventArgs e)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date1 = DtpCurrent.Value;
            Calendar cal = dfi.Calendar;
            int week = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
            week -= 1;
            int diff = (int)(date1.DayOfWeek) - 1;
            DateTime weekdate = date1.AddDays(-diff);
            if (DateTime.Today.DayOfWeek == System.DayOfWeek.Sunday)
            {
                weekdate = weekdate.AddDays(-7);
            }
            string date = UIStyles.NetString + weekdate.ToString(UIStyles.ShortDateFormat);
            rich.Text = UIStyles.ZineReturnWeek + week.ToString() + UIStyles.CarriageReturnLineFeed;
            lblDate.Text = UIStyles.WeekString + week.ToString();

        }
        
        private bool IsShortcutKey(Keys key)
        {
            return (key >= Keys.A && key <= Keys.Z) || // A–Z
                   key == Keys.OemOpenBrackets ||      // [
                   key == Keys.OemCloseBrackets ||     // ]
                   key == Keys.OemSemicolon ||         // ;
                   key == Keys.Oemcomma ||             // ,
                   key == Keys.OemPeriod;              // .
        }
        private bool TryProcessEncodedPriceBarcode(string barcode)
        {
            if (!barcode.StartsWith(UIStyles.LottoPrefix))
                return false;

            try
            {
                // Extract price from encoded barcode
                string path = barcode.Replace(UIStyles.LottoPrefix, UIStyles.Empty);
                path = path.Substring(2, path.Length - 3); // assumes format includes extra wrapping digits
                decimal price = decimal.Parse(path) / 100;

                DateTime now = DateTime.Now;

                DataRow row = CurrentTable.NewRow();
                row["stock_id"] = 0;
                row["descr"] = UIStyles.LottoDescription; // "Lotto"
                row["acc_number"] = 1020;
                row["card_id"] = 0;
                row["cost"] = price * 91 / 100;
                row["RRP"] = price;
                row["number_items"] = 1;
                row["discount"] = 0;
                row["sales"] = price;
                row["GST_collect"] = price / 11;
                row["GST_paid"] = price / 11;
                row["sale_date"] = now.ToString("yyyy-MM-dd");
                row["sale_time"] = now.ToString("HH:mm:ss");
                row["user_id"] = 0;
                row["seq_id"] = CurrentTable.Rows.Count + 1;

                CurrentTable.Rows.Add(row);
                CopyToGrid();
                StartNewSale();

                return true;
            }
            catch (Exception ex)
            {
                ErrorLogWriter.Instance.Log("Failed to process encoded price barcode: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// handle function keys F1-F12
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private bool HandleFunctionKeys(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1: ChangeNote(10); return true;
                case Keys.F2: ChangeNote(20); return true;
                case Keys.F3: ChangeNote(30); return true;
                case Keys.F4: ChangeNote(40); return true;
                case Keys.F5: ChangeNote(50); return true;
                case Keys.F6: ChangeNote(60); return true;
                case Keys.F7: ChangeNote(70); return true;
                case Keys.F8: ChangeNote(80); return true;
                case Keys.F9: ChangeNote(90); return true;
                case Keys.F10: ChangeNote(100); return true;
                case Keys.F11: EnterConfig(); return true;
                case Keys.F12: PrintReceipt(); return true;
                default: return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void EnterConfig()
        {
            FrmConfig form = new FrmConfig();
            form.Show();
            CheckCards = form.checkCardsEnabled;
        }
        /// <summary>
        /// Change from cash to  and vice versa
        /// </summary>
        private void TogglePaymentMethod()
        {
            rbtCash.Checked = !rbtCash.Checked;
            rBtnoCash.Checked = !rBtnoCash.Checked;
        }

        /// <summary>
        /// TODO: check this actually handles the barcode scan
        /// </summary>
        /// <param name="barcode"></param>
        private void ProcessBarcodeEntry(string barcode)
        {
            if (TryProcessEncodedPriceBarcode(barcode))
                return;

            if (CheckCards)
            {
                FrmNewCards frmNewCards = new FrmNewCards
                {
                    supplierIndex = this.supplierIndexCards,
                    newBarCode = barcode,
                    stock_id = 0
                };
                AddOwnedForm(frmNewCards);
                frmNewCards.Location = new Point(this.Width / 2 - 390, this.Height / 2 - 250);
                if (frmNewCards.ShowDialog(this) == DialogResult.Yes)
                {
                    this.supplierIndexCards = frmNewCards.supplierIndex;
                }
            }
            else
            {
                bool stockItem = GetStockItem(0);
                StartNewSale();
                if (!stockItem) return;

                FrmNewItem frmNewItem = new FrmNewItem
                {
                    newBarCode = barcode,
                    stock_id = 0
                };
                AddOwnedForm(frmNewItem);
                frmNewItem.Location = new Point(this.Width / 2 - 390, this.Height / 2 - 250);
                if (frmNewItem.ShowDialog(this) == DialogResult.Yes)
                {
                    AddNewStock(barcode);
                }
            }
        }
        private bool IsNumericKey(KeyEventArgs e)
        {
            return (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9)
                || (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
                || e.KeyCode == Keys.Decimal;
        }
        private void ProcessCashInputKey(KeyEventArgs e)
        {
            endOfSaleFlag = false;
            lblDesc.Text = UIStyles.CashReceived;

            if (e.KeyCode == Keys.Decimal || e.KeyValue == 110)
            {
                txtCat.Text = txtCat.Text.TrimEnd('.') + "00";
                txtCat.SelectionStart = txtCat.Text.Length;
            }

            if (!txtCat.Text.StartsWith(UIStyles.SevenZero) && txtCat.Text.Length < UIStyles.SevenZero.Length)
            {
                txtAmount.Text = CurrencyUtil.ToCurrencyString(CurrencyUtil.ParseCurrency(txtCat.Text) / 100m);
            }
        }
        /// <summary>
        /// prepare to change quantity of the last item in the sale
        /// </summary>
        /// <param name="e"></param>
        private void CaptureControlInput(KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                controlstring += (char)('0' + (e.KeyCode - Keys.NumPad0));
            }
            else
            {
                controlstring += (char)e.KeyCode;
            }

            lblDesc.ForeColor = Color.DarkOrange;
            lblDesc.Text = $"New Qty: {controlstring}";
        }

        private void ShortCutKeyUp(KeyEventArgs e)
        {
            string keyStr = e.KeyCode.ToString();
            int today = (int)DateTime.Today.DayOfWeek; // 0 = Sunday, 6 = Saturday

            // Get shortcut lookup record
            DataRow lookup = GetShortcutLookup(keyStr, today);
            if (lookup == null)
            {
                txtCat.Text = UIStyles.Empty;
                lblChange.Text = UIStyles.ChangeDefault;
                return;
            }

            int stockId = Convert.ToInt32(lookup["stock_id"]);
            int accNo = Convert.ToInt32(lookup["acc_number"]);

            if (stockId == 0)
            {
                LoadCategoryAccount(accNo);
            }
            else
            {
                GetStockItem(stockId);
                StartNewSale();
            }

            lblChange.Text = UIStyles.ChangeDefault;
        }
        
        private void CopyToGrid()
        {
            int i = dgItemList.Rows.Count - 1;
            for (int index2 = CurrentTable.Rows.Count - 1; index2 >= 0; --index2)
            {
                dgItemList[0, i].Value = CurrentTable.Rows[index2]["seq_id"];
                dgItemList[1, i].Value = CurrentTable.Rows[index2]["descr"];
                dgItemList[2, i].Value = CurrentTable.Rows[index2]["RRP"];
                dgItemList[3, i].Value = CurrentTable.Rows[index2]["number_items"];
                dgItemList[4, i].Value = CurrentTable.Rows[index2]["GST_collect"];
                dgItemList[5, i].Value = CurrentTable.Rows[index2]["sales"];
                if (Convert.ToDecimal(CurrentTable.Rows[index2]["sales"]) >= new Decimal(0))
                {
                    dgItemList.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    dgItemList.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                }
                dgItemList.Rows[i].ReadOnly = false;
                --i;
                if (dgItemList.Rows.Count < 1)
                {
                    break;
                }
            }

            // also sets the total string 
            Decimal subTotal = new Decimal(0);
            for (int j = 0; j < CurrentTable.Rows.Count; j++)
            {
                subTotal += (Decimal)CurrentTable.Rows[j]["sales"];
            }
            lblTotal.Text = string.Format(UIStyles.CurrencyFormat, subTotal);

            string descr = UIStyles.Empty;
            Decimal itemPrice = 0M;

            if (CurrentTable.Rows.Count > 0)
            {
                descr = CurrentTable.Rows[CurrentTable.Rows.Count - 1]["descr"].ToString();
                itemPrice = (Decimal)CurrentTable.Rows[CurrentTable.Rows.Count - 1]["sales"];
            }
            CustomerDisplayItem(descr, itemPrice, subTotal);
        }
         
        private void Clear()
        {
            StartNewSale();
            CurrentTable.Rows.Clear();
            dgItemList.Rows.Clear();
            dgItemList.Rows.Add(UIStyles.DefaultRowCount);
            endOfSaleFlag = false;

        }
    }
}
