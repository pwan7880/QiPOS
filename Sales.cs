using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QiPOS
{
    public partial class FrmPos : Form
    {

        /// <summary>
        /// prepare to start a new sale        
        /// </summary>
        private void StartNewSale()
        {
            //set 5 minute timer on action, if nothing happens put the sale in
            timerClean.Stop();
            timerClean.Start();

            txtCat.Focus();
            dgItemList.CurrentCell = null;
            lblDesc.ForeColor = Color.DarkBlue;
            lblDesc.Text = UIStyles.ReadyForNewSale;
            txtCat.Text = UIStyles.Empty;
            txtAmount.Text = UIStyles.Empty;
            lblChange.ForeColor = Color.Purple;
            endOfSaleFlag = false;
        }

        /// <summary>
        /// Display change, from note tendered
        /// </summary>
        /// <param name="tender"></param>
        private void ChangeNote(int tender)
        {

            if (dgItemList.Rows.Count == 0)
            {
                return;
            }
            rbtCash.Checked = true;
            txtCat.Text = tender.ToString();
            endOfSaleFlag = false;
            lblDesc.Text = UIStyles.CashReceived;
            txtAmount.Text = CurrencyUtil.ToCurrencyString(tender);
            CalculateChange();
            EndOfSales();
        }

        private void CalculateChange()
        {
            Decimal remainder = new Decimal(0);
            if (txtAmount.Text != UIStyles.Empty)
            {
                remainder = CurrencyUtil.SafeToDecimal(txtAmount.Text) - CurrencyUtil.SafeToDecimal(lblTotal.Text);
                if (remainder < new Decimal(0))
                {
                    lblChange.ForeColor = Color.Red;
                    StartNewSale();
                }
                else if (remainder > 100M)
                {
                    lblChange.ForeColor = Color.DeepPink;
                }
                else
                {
                    lblChange.ForeColor = Color.Purple;
                }
                lblChange.Text = string.Format(UIStyles.ChangeDefault, remainder);
            }
            else
            {
                lblChange.Text = UIStyles.ChangeDefault;
            }

            if (remainder < 0M)
            {
                return;
            }
            txtCat.Text = UIStyles.Empty;
            endOfSaleFlag = true;
        }

        /// <summary>
        /// Change the last item quantity in the current sale, holding down Ctrl
        /// </summary>
        private void ApplyControlQuantity()
        {
            if (CurrentTable.Rows.Count == 0 || string.IsNullOrEmpty(controlstring))
                return;

            if (!int.TryParse(controlstring, out int qtyEntered) || qtyEntered <= 0)
                return;

            controlstring = UIStyles.Empty;

            // Get last data row index
            int dataRowIndex = CurrentTable.Rows.Count - 1;
            DataRow dataRow = CurrentTable.Rows[dataRowIndex];

            // Calculate matching DataGridView row index (since it's filled from bottom-up)
            int gridRowIndex = dgItemList.Rows.Count - CurrentTable.Rows.Count + dataRowIndex;

            if (gridRowIndex < 0 || gridRowIndex >= dgItemList.Rows.Count)
                return; // safety check

            // Get price and compute totals
            decimal unitPrice = CurrencyUtil.SafeToDecimal(dataRow["RRP"]);
            decimal totalAmount = unitPrice * qtyEntered;
            decimal gstAmount = totalAmount / 11;

            // Update DataRow
            dataRow["number_items"] = qtyEntered;
            dataRow["GST_collect"] = gstAmount;
            dataRow["sales"] = totalAmount;

            // Update Grid
            dgItemList[3, gridRowIndex].Value = qtyEntered;
            dgItemList[4, gridRowIndex].Value = gstAmount;
            dgItemList[5, gridRowIndex].Value = totalAmount;

            // Recalculate totals
            decimal subTotal = 0;
            foreach (DataRow row in CurrentTable.Rows)
                subTotal += CurrencyUtil.SafeToDecimal(row["sales"]);

            lblTotal.Text = CurrencyUtil.ToCurrencyString(subTotal);
            CustomerDisplayItem(dataRow["descr"].ToString(), totalAmount, subTotal);

            // Reset label
            lblDesc.ForeColor = Color.DarkBlue;
            lblDesc.Text = UIStyles.ReadyForNewSale;
        }
        
        private void CustomerDisplayItem(string description, Decimal itemPrice, Decimal subTotal)
        {
            string totalText;
            string subtotalText;
            if (description == "totalz" && itemPrice == -1M && subTotal == -1M)
            {
                totalText = (lblTotal.Text).ToString().Trim();
                totalText.PadLeft(15, ' ');
                totalText = "TOTAL" + totalText;
                string str4 = (lblChange.Text).ToString().Trim();
                string str5 = str4.Substring(str4.IndexOf("$"));
                int length2 = str5.Length;
                for (int index = 0; index < 14 - length2; ++index)
                {
                    str5 = " " + str5;
                }
                subtotalText = "CHANGE" + str5;
            }
            else
            {
                string str3 = string.Format(UIStyles.CurrencyFormat, itemPrice);
                int num1 = str3.Length + 1;
                if (description.Length > 20 - num1)
                {
                    description = description.Substring(0, 20 - num1);
                }
                else
                {
                    int num2 = num1 + description.Length;
                    for (int j = 0; j < 20 - num2; ++j)
                    {
                        str3 = " " + str3;
                    }
                }
                totalText = description + " " + str3;
                string str4 = string.Format(UIStyles.CurrencyFormat, subTotal);
                int length = str4.Length;
                for (int index = 0; index < 12 - length; ++index)
                {
                    str4 = " " + str4;
                }
                subtotalText = "SubTotal" + str4;
            }
            string sendTo = "\x001BQA" + totalText + "\r\x001BQB" + subtotalText + "\r";
            DisplayPoleUtil.SendToDisplay(displayNameStr, sendTo);
            timerClean.Enabled = false;
        }

        private static Decimal CurrencyInputValidation(System.Windows.Forms.TextBox txtBox, KeyPressEventArgs e)
        {
            // TODO: what is tmpRate for?
            decimal tmpRate = new decimal(0);
            Decimal num = CurrencyUtil.StringToDecimalValidation(txtBox.Text, e);
            if (tmpRate < new Decimal(0) && num > new Decimal(0))
                num = new Decimal(0) - num;
            if (num > new Decimal(0))
                txtBox.ForeColor = Color.DarkBlue;
            else
                txtBox.ForeColor = Color.Red;
            txtBox.Text = string.Format(UIStyles.CurrencyFormat, num);
            return num;
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintReceipt();
                StartNewSale();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }

        private void PrintReceipt()
        {
            try
            {
                conn.ConnectBD(); // Ensure connection is open
                using (var cmd = new SqlCommand("GetLatestSaleTransId", Connect.connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Define output parameter for SaleTransId
                    var saleTransIdParam = new SqlParameter("@SaleTransId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(saleTransIdParam);

                    cmd.ExecuteNonQuery();

                    // Retrieve the output parameter value
                    if (saleTransIdParam.Value != DBNull.Value)
                    {
                        int saleTransId = Convert.ToInt32(saleTransIdParam.Value);
                        new PrintReceiptCls(saleTransId.ToString(), printerNameStr).PrintReceiptDirect();
                    }

                    StartNewSale();
                }
            }
            catch (SqlException ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Error executing GetLatestSaleTransId in PrintReceipt");
                MessageBox.Show($"Database error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                ErrorLogWriter.Instance.Log(ex, "General error in PrintReceipt");
                MessageBox.Show($"Error printing receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }


        private void BtnSum_Click(object sender, EventArgs e)
        {
            try
            {
                FrmStatistics frmStatistics = new FrmStatistics();
                AddOwnedForm(frmStatistics);
                frmStatistics.Show();
                StartNewSale();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }
        private void BtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                dgItemList.Rows.Clear();
                CurrentTable.Rows.Clear();
                dgItemList.Rows.Add(UIStyles.DefaultRowCount);
                StartNewSale();
                DisplayPoleUtil.SendToDisplay(displayNameStr, "\f");
                refundFlag = false;
                dgItemList.Tag = null;
                ButtonRefund.ForeColor = Color.DarkBlue;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }


        private void BtnEndSale_Click(object sender, EventArgs e)
        {
            try
            {
                CalculateChange();
                EndOfSales();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                ErrorLogWriter.Instance.Log(message);
            }
        }


        //clears the pole display
        private void TmrClean_Tick(object sender, EventArgs e)
        {
            if (CurrentTable != null && CurrentTable.Rows.Count > 0 && !endOfSaleFlag)
            {
                // optional: set default tendered amount = total
                txtAmount.Text = lblTotal.Text;
                rbtCash.Checked = true;
                CalculateChange();   // calculate change
                EndOfSales();  // auto process the sale
                RefocusToCat();
                return;
            }
            DisplayPoleUtil.SendToDisplay(displayNameStr, "\f");
            timerClean.Enabled = false;
        }
        /// <summary>
        /// Process refund for a given sale ID and amount
        /// </summary>
        /// <param name="originalSaleId"></param>
        /// <param name="refundAmount"></param>
        private void ProcessRefund(string originalSaleId, decimal refundAmount)
        {
            try
            {
                DataTable originalItems = refundService.LoadRefundableSale(originalSaleId);
                if (originalItems == null || originalItems.Rows.Count == 0)
                {
                    MessageBox.Show("Original sale not found.", "Refund Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal refundChange;
                int refundSaleId = refundService.ProcessRefund(originalItems, refundAmount, out refundChange);

                PrintReceiptFor(refundSaleId);
                ResetUiAfterSale();
            }
            catch (Exception ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Refund failed");
                MessageBox.Show("Refund failed: " + ex.Message, "Refund Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
