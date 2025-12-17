using System; 
using System.Data;
using System.Data.SqlClient;
using System.Drawing; 
using System.Windows.Forms;
namespace QiPOS
{
    public partial class FrmPos : Form
    {
        private bool GetStockItem(int stockId)
        {
            bool insert = false;
            string barcode = txtCat.Text.Trim();
            if (barcode.Length == 15)
                barcode = barcode.Substring(0, 13);

            try
            {
                conn.ConnectBD(); // Ensure connection is open
                using (var cmd = new SqlCommand(stockId == 0 ? "GetStockByBarcode" : "GetStockById", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (stockId == 0)
                        cmd.Parameters.AddWithValue("@Barcode", barcode);
                    else
                        cmd.Parameters.AddWithValue("@StockId", stockId);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable stockTable = new DataTable();
                        adapter.Fill(stockTable);

                        if (stockTable.Rows.Count > 0)
                        {
                            var stockRow = stockTable.Rows[0];
                            bool exists = false;

                            foreach (DataRow dataRow in CurrentTable.Rows)
                            {
                                if (Equals(dataRow["stock_id"], stockRow["stock_id"]))
                                {
                                    exists = true;
                                    int qty = Convert.ToInt32(dataRow["number_items"]) + 1;
                                    decimal rrp = CurrencyUtil.SafeToDecimal(stockRow["RRP"]);
                                    decimal gst = CurrencyUtil.SafeToDecimal(stockRow["GST_collect"]);
                                    decimal gstPaid = CurrencyUtil.SafeToDecimal(stockRow["GST_paid"]);

                                    if (refundFlag)
                                    {
                                        rrp = -rrp;
                                        gst = -gst;
                                        gstPaid = -gstPaid;
                                    }

                                    dataRow["number_items"] = qty;
                                    dataRow["sales"] = CurrencyUtil.SafeToDecimal(dataRow["sales"]) + rrp;
                                    dataRow["GST_collect"] = CurrencyUtil.SafeToDecimal(dataRow["GST_collect"]) + gst;
                                    dataRow["GST_paid"] = CurrencyUtil.SafeToDecimal(dataRow["GST_paid"]) + gstPaid;
                                    break;
                                }
                            }

                            if (!exists)
                            {
                                var newRow = CreateSaleRowFromStock(stockRow);
                                CurrentTable.Rows.Add(newRow);
                            }

                            lblChange.Text = UIStyles.ChangeDefault;
                            CopyToGrid();
                        }
                        else if (MessageBox.Show(UIStyles.AddNewItemNow, UIStyles.NotStockedItem, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            insert = true;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Error executing stored procedure in GetStockItem");
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            return insert;
        }
        private void AddNewStock(string barcode)
        {
            Connect connect = new Connect();
            SqlParameter[] paramsArray = new SqlParameter[]
                 {
                    new SqlParameter("@Barcode", SqlDbType.NVarChar, 50) { Value = barcode ?? (object)DBNull.Value }
                 };
            connect.QueryTableSP("dbo.GetStockByBarcode", paramsArray);
           
            if (connect.aTable != null && connect.aTable.Rows.Count > 0)
            {
                // Update dgItemList DataSource or Rows
                dgItemList.DataSource = connect.aTable; // Or manually add rows if not bound
                dgItemList.ClearSelection(); // Optional: Clear selection before scrolling
                dgItemList.FirstDisplayedScrollingRowIndex = dgItemList.Rows.Count -1; // Scroll to last row
                
            }
            else
            {
                // Handle empty result (e.g., clear or show message)
                dgItemList.DataSource = null;
                dgItemList.Rows.Clear();
            }

            DataRow stockRow = connect.aTable.Rows[0];
            DataRow saleRow = CreateSaleRowFromStock(stockRow);
            CurrentTable.Rows.Add(saleRow);
            CopyToGrid();
            StartNewSale();
        }
        private DataRow CreateSaleRowFromStock(DataRow stockRow, int quantity = 1)
        {
            DateTime now = DateTime.Now;
            decimal rrp = CurrencyUtil.SafeToDecimal(stockRow["RRP"]);
            decimal cost = CurrencyUtil.SafeToDecimal(stockRow["cost"]);
            decimal gst = CurrencyUtil.SafeToDecimal(stockRow["GST_collect"]);
            decimal gstPaid = CurrencyUtil.SafeToDecimal(stockRow["GST_paid"]);

            if (refundFlag)
            {
                rrp = -rrp;
                gst = -gst;
                gstPaid = -gstPaid;
            }

            var row = CurrentTable.NewRow();
            row["stock_id"] = stockRow["stock_id"];
            row["descr"] = stockRow["descr"];
            row["acc_number"] = stockRow["acc_number"];
            row["card_id"] = stockRow["card_id"];
            row["cost"] = cost;
            row["RRP"] = rrp;
            row["number_items"] = quantity;
            row["discount"] = 0;
            row["sales"] = rrp * quantity;
            row["GST_collect"] = gst * quantity;
            row["GST_paid"] = gstPaid * quantity;
            row["sale_date"] = now.ToString("yyyy-MM-dd");
            row["sale_time"] = now.ToString("HH:mm:ss");
            row["user_id"] = currentUser?.Id ?? 0;
            row["seq_id"] = CurrentTable.Rows.Count + 1;

            return row;
        }

        private void LoadCategoryAccount(int accNo)
        {
            string query = $"SELECT acc_number, acc_name, markup_rate FROM account_list WHERE acc_type_id = 4 AND acc_number = {accNo}";
            conn.QueryTable(query);
            if (conn.aTable.Rows.Count == 0) return;

            DataRow acc = conn.aTable.Rows[0];
            lblDesc.Text = acc["acc_name"].ToString();
            tmpAccNo = acc["acc_number"].ToString();
            tmpRate = Convert.ToDecimal(acc["markup_rate"]);

            //if (refundFlag)
            //    tmpRate = tmpRate >= 0 ? -1 : 1;
            ErrorLogWriter.Instance.Log($"Loaded Category Account: {tmpAccNo} - {lblDesc.Text} with Markup Rate: {tmpRate}");
            lblDesc.ForeColor = tmpRate >= 0 ? Color.DarkBlue : Color.Red;
            txtAmount.ForeColor = tmpRate >= 0 ? Color.DarkBlue : Color.Red;
            txtAmount.Text = string.Format(UIStyles.CurrencyFormat, 0);
            txtAmount.Focus();
        }
        private void UpdateStockChange(int stock_id)
        {
            try
            {
                conn.ConnectBD(); // Ensure connection is open
                using (var cmd = new SqlCommand("GetStockById", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StockId", stock_id);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable stockTable = new DataTable();
                        adapter.Fill(stockTable);

                        if (stockTable.Rows.Count > 0)
                        {
                            DataRow dataRow1 = stockTable.Rows[0];
                            foreach (DataRow dataRow2 in CurrentTable.Rows)
                            {
                                if (dataRow2["stock_id"].Equals(dataRow1["stock_id"]))
                                {
                                    int num1 = (int)dataRow2["number_items"];
                                    dataRow2["descr"] = dataRow1["descr"];
                                    dataRow2["acc_number"] = dataRow1["acc_number"];
                                    dataRow2["card_id"] = dataRow1["card_id"];
                                    dataRow2["cost"] = dataRow1["cost"];
                                    dataRow2["RRP"] = dataRow1["RRP"];
                                    Decimal num2 = (Decimal)dataRow1["RRP"];
                                    dataRow2["sales"] = (num2 * (Decimal)num1);
                                    Decimal num3 = (Decimal)dataRow1["GST_collect"];
                                    dataRow2["GST_collect"] = (num3 * (Decimal)num1);
                                    Decimal num4 = (Decimal)dataRow1["GST_paid"];
                                    dataRow2["GST_paid"] = (num4 * (Decimal)num1);
                                    break;
                                }
                            }
                            CopyToGrid();
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Error executing GetStockById in UpdateStockChange");
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            StartNewSale();
            try
            {
                conn.ConnectBD(); // Ensure connection is open
                foreach (DataRow dataRow in CurrentTable.Rows)
                {
                    int stockId = Convert.ToInt32(dataRow["stock_id"].ToString());
                    if (stockId == 0) continue;

                    using (var cmd = new SqlCommand("UpdateStockDetails", Connect.connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@StockId", stockId);
                        cmd.Parameters.AddWithValue("@Descr", dataRow["descr"].ToString());
                        cmd.Parameters.AddWithValue("@RRP", CurrencyUtil.SafeToDecimal(dataRow["RRP"]));
                        cmd.Parameters.AddWithValue("@GSTCollect", CurrencyUtil.SafeToDecimal(dataRow["GST_collect"]));
                        cmd.Parameters.AddWithValue("@EnteredDate", DateTime.Now);

                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            ErrorLogWriter.Instance.Log(ex, $"Error updating stock_id {stockId} in UpdateStockDetails");
                            MessageBox.Show($"Failed to update stock item {stockId}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Error in BtnSave_Click while connecting to database");
                MessageBox.Show($"Database connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// Get the shortcut lookup for a given key and day of the week.
        /// </summary>
        /// <param name="keyStr"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        private DataRow GetShortcutLookup(string keyStr, int dayOfWeek)
        {
            try
            {
                conn.ConnectBD(); // Ensure connection is open
                using (var cmd = new SqlCommand("GetShortcutLookup", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@KeyStr", keyStr);
                    cmd.Parameters.AddWithValue("@DayOfWeek", dayOfWeek);

                    using (var adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable resultTable = new DataTable();
                        adapter.Fill(resultTable);
                        return resultTable.Rows.Count > 0 ? resultTable.Rows[0] : null;
                    }
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, $"Error executing GetShortcutLookup for key {keyStr} and day {dayOfWeek}");
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                conn.Close();
            }
        }

        private string GetPrinterName()
        {
            try
            {
                var config = new ConfigurationReader().CompanyInfo();
                return config.PosPrinter;
            }
            catch (ConfigIOException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Fatal: Could not load printer name from config.");
                MessageBox.Show("The system could not start because the POS printer configuration failed.\n\n" +
                                ex.Message,
                                "Startup Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
               // Environment.Exit(1);
                return null; // won't be reached, but required by compiler
            }
        }

        private string GetDisplayPortName()
        {
            try
            {
                var config = new ConfigurationReader().CompanyInfo();
                return config.LineDisplayPort;
            }
            catch (ConfigIOException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Warning: Could not load display port from config. Continuing without customer display.");
                MessageBox.Show("Customer display is not available.\n\n" +
                                "You may continue using the POS without the pole display.\n\n" +
                                ex.Message,
                                "Display Port Warning",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return string.Empty; // non-fatal: empty string means display won't be used
            }
        }
    }
}
