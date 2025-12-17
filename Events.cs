using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace QiPOS
{
    public partial class FrmPos : Form
    {
        private void FrmPos_Load(object sender, EventArgs e)
        {
            VersionLabel.Text = version;

            dgItemList.Width = this.Width;
            dgItemList.Height = gridHeight;
            try
            {
                InitializePOS();
                ApplyGridDefaults(dgItemList);

                cacheTable = CurrentTable;
                cacheSecondTable = CurrentTable.Copy();
                cacheSecondTable.TableName = "NewTable";
                endOfSaleFlag = false;
                SetReturnInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize POS: {ex.Message}", "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                StartNewSale();
            }

            CurrentTable = cacheTable;

            dgItemList.Rows.Add(UIStyles.DefaultRowCount); 

            ApplyGridDefaults(dgItemList); 


        }

        // ok this is what happens when customer pay
        private void FrmPos_TextChanged(object sender, EventArgs e)
        {
            string str = Text;
            dgItemList.Tag = this.Tag;
            if (str.Substring(0, 1) == "S")
            {
                str = str.Substring(1, str.Length - 1);
                GetStockItem(Convert.ToInt32(str));
                StartNewSale();
            }
            Text = UIStyles.PointOfSale;
        }

        //handles refund Button clicked
        private void BtnRefund_Click(object sender, EventArgs e)
        {
            if (CurrentTable.Rows.Count == 0)
            {
                if (refundFlag)
                {
                    refundFlag = false;
                    ButtonRefund.ForeColor = Color.DarkBlue;
                }
                else
                {
                    refundFlag = true;
                    ButtonRefund.ForeColor = Color.Red;
                }
            }
            StartNewSale();
        }

        private void FrmPos_Resize(object sender, EventArgs e)
        {
            this.dgItemList.Width = this.Width; 
            StartNewSale();
        }
        private void FrmPos_Activated(object sender, EventArgs e)
        {
            this.dgItemList.Width = this.Width; 
            StartNewSale();
        }

        // Change cash notes made by PW
        private void Button50_Click(object sender, EventArgs e)
        {
            ChangeNote(50);
        }

        private void Button20_Click(object sender, EventArgs e)
        {
            ChangeNote(20);
        }

        private void Button100_Click(object sender, EventArgs e)
        {
            ChangeNote(100);
        }

        private void Button10_Click(object sender, EventArgs e)
        {
            ChangeNote(10);
        }

        private void Button30_Click(object sender, EventArgs e)
        {
            ChangeNote(30);
        }

        private void Button40_Click(object sender, EventArgs e)
        {
            ChangeNote(40);
        }
        private void BtnShortCut_Click(object sender, EventArgs e)
        {
            try
            {
                FrmShortcut frmShortcut = new FrmShortcut();
                AddOwnedForm(frmShortcut);
                frmShortcut.Show();
                StartNewSale();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            FrmSearch frmSearch = new FrmSearch
            {
                currentItem = searchCache,
                funIdentifier = "Search"
            };
            AddOwnedForm(frmSearch);

            if (frmSearch.ShowDialog(this) == DialogResult.Yes)
            {
                GetStockItem(Convert.ToInt32(Tag.ToString()));
            }
            searchCache = frmSearch.currentItem;
            StartNewSale();
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            try
            {
                FrmProducts frmProducts = new FrmProducts(true);
                AddOwnedForm(frmProducts);
                frmProducts.Show();
                StartNewSale();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }

        private void BtnTransactions_Click(object sender, EventArgs e)
        {
            try
            {
                FrmTransactions frmTransactions = new FrmTransactions(true);
                AddOwnedForm(frmTransactions);
                frmTransactions.Show();
                StartNewSale();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }

        private void BtnPrevSale_Click(object sender, EventArgs e)
        {
            try
            {
                FrmPrevSale frmPrevSale = new FrmPrevSale();
                AddOwnedForm(frmPrevSale);
                frmPrevSale.Show();
                StartNewSale();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }
        private void BtnCashForm_Click(object sender, EventArgs e)
        {
            try
            {
                FrmCash frmCash = new FrmCash();
                AddOwnedForm(frmCash);
                frmCash.Show();
                StartNewSale();
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
            }
        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            RawPrinterHelper.SendStringToPrinter(printerNameStr, "\x001Bp\0 \x0080");

            StartNewSale();
        }


        private void PnlDateBar_Click(object sender, EventArgs e)
        {
            StartNewSale();
        }

        private void LblChange_Click(object sender, EventArgs e)
        {
            StartNewSale();
        }

        private void RbtCash_CheckedChanged(object sender, EventArgs e)
        {
            StartNewSale();
        }

        private void RBtnoCash_CheckedChanged(object sender, EventArgs e)
        {
            StartNewSale();
        }

        private void PnlCash_MouseClick(object sender, MouseEventArgs e)
        {
            StartNewSale();
        }

        private void FrmPos_MouseClick(object sender, MouseEventArgs e)
        {
            StartNewSale();
            if (e.Button == MouseButtons.Right)
            {
                // not implemented
            }
        }

        private void LblDesc_MouseClick(object sender, MouseEventArgs e)
        {
            StartNewSale();
        }

        private void LblTotal_MouseClick(object sender, MouseEventArgs e)
        {
            StartNewSale();
        }

        private void TxtAmount_Enter(object sender, EventArgs e)
        {
            if (txtAmount.Text != UIStyles.Empty)
            {
                dgItemList.CurrentCell = null;
            }
            else
            {
                StartNewSale();
            }
        }


        private void TxtAmount_Leave(object sender, EventArgs e)
        {
            StartNewSale();
        }
        private void TxtAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tmpRate < 0) //negative case
            { 
                if (!txtAmount.Text.StartsWith("-"))
                txtAmount.Text = "-" + txtAmount.Text;
            }
            try
            {
                if ((int)e.KeyChar == 13 && txtAmount.Text != UIStyles.ZeroDollarString)
                {
                    bool negativeFlag = false;
                    if (tmpRate < 0)
                    {
                        negativeFlag = true;                        
                    }
                    Decimal num1 = CurrencyUtil.SafeToDecimal(txtAmount.Text);

                    bool flag = false;
                    if (num1 > new Decimal(500))
                    {
                        if (MessageBox.Show(UIStyles.ItemValueGreaterThan500, UIStyles.ItemValue, MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                    if (flag)
                    {
                        DateTime now = DateTime.Now;
                        DataRow row = CurrentTable.NewRow();
                        row["stock_id"] = 0;
                        row["descr"] = lblDesc.Text;
                        row["acc_number"] = tmpAccNo;
                        row["card_id"] = 0;
                        row["cost"] = (num1 * (new Decimal(100) - tmpRate) / new Decimal(100));
                        row["RRP"] = num1;
                        row["number_items"] = 1;
                        row["discount"] = 0;
                        row["sales"] = num1;
                        Decimal num2 = (Decimal)row["sales"];
                        row["GST_collect"] = (num1 / Convert.ToDecimal(11));
                        row["GST_paid"] = (Convert.ToDecimal(row["cost"]) * Convert.ToDecimal(0.1));
                        row["sale_date"] = now.ToString("yyyy-MM-dd");
                        row["sale_time"] = now.ToString("HH:mm:ss");
                        row["user_id"] = 0;
                        row["seq_id"] = CurrentTable.Rows.Count + 1;

                        CurrentTable.Rows.Add(row);
                        CopyToGrid();
                    }
                    StartNewSale();
                }
                else
                    CurrencyInputValidation(txtAmount, e);
            }
            catch (Exception ex)
            {

                string message = ex.Message;
                ErrorLogWriter.Instance.Log(ex, "Error in TxtAmount_KeyPress: " + message);
                StartNewSale();
            }
        }


        private void DgItemList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 0 && e.ColumnIndex != 5)
            {
                return;
            }
            StartNewSale();
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgItemList.Rows.Count)
                {
                    return;
                }
                string str = dgItemList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (str != UIStyles.Empty)
                {
                    int stock_id = 0;
                    foreach (DataRow dataRow in cacheTable.Rows)
                    {
                        if (dataRow["seq_id"].ToString().Equals(str))
                        {
                            stock_id = Convert.ToInt32(dataRow["stock_id"].ToString());
                            break;
                        }
                    }
                    if (stock_id > 0)
                    {
                        FrmNewItem frmNewItem = new FrmNewItem
                        {
                            newBarCode = UIStyles.Empty,
                            stock_id = stock_id
                        };
                        AddOwnedForm(frmNewItem);
                        frmNewItem.Location = new Point(Width / 2 - 390, Height / 2 - 250);
                        if (frmNewItem.ShowDialog(this) == DialogResult.Yes)
                        {
                            UpdateStockChange(stock_id);
                        }
                    }
                }
            }
        }
        
        private void DgItemList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.CellStyle.ForeColor = Color.Red;
            controlArg = e;
            if (cellEditHandler)
            {
                e.Control.KeyPress += new KeyPressEventHandler(Control_KeyPress);
                cellEditHandler = false;
            }
            cellEditingBeginingFlag = true;
        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                int num = (int)e.KeyChar;
                if (cellEditingBeginingFlag && dgItemList.CurrentCell.ColumnIndex == 1)
                {
                    cellEditingBeginingFlag = false;
                    cellEditedFlag = true;
                }
                if (cellEditingBeginingFlag && dgItemList.CurrentCell.ColumnIndex > 1 && num > 47 && num < 58)
                {
                    controlArg.Control.Text = UIStyles.Empty;
                    cellEditingBeginingFlag = false;
                    cellEditedFlag = true;
                }
                if (dgItemList.CurrentCell.ColumnIndex == 2 || dgItemList.CurrentCell.ColumnIndex == 4)
                {
                    controlArg.Control.Text = string.Format(UIStyles.CurrencyFormat, CurrencyUtil.StringToDecimalValidation(controlArg.Control.Text, e));
                }
                else
                {
                    if (num >= 48 && num <= 57 || dgItemList.CurrentCell.ColumnIndex != 3)
                    {
                        return;
                    }
                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {

                string message = "ctrl key press " + ex.Message;
                Console.WriteLine(message);
                StartNewSale();
            }
        }
         
        private void DgItemList_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                bool flag = false;
                foreach (DataRow dataRow in cacheTable.Rows)
                {
                    if (dataRow["seq_id"].Equals(objCurSeq_id))
                    {
                        dataRow.Delete();
                        flag = true;
                        break;
                    }
                }
                if (flag)
                {
                    int i = 1;
                    foreach (DataRow dataRow in cacheTable.Rows)
                    {
                        dataRow["seq_id"] = i;
                        i++;
                    }
                }
                dgItemList.Rows.Clear();
                dgItemList.Rows.Add(UIStyles.DefaultRowCount);
                CopyToGrid();
                StartNewSale();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Delete Datagridview Row Exception: " + (ex.Message).ToString());
                dgItemList.Rows.Clear();
                dgItemList.Rows.Add(UIStyles.DefaultRowCount);
                CopyToGrid();
                StartNewSale();
            }
        }

        private void DgItemList_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (dgItemList.SelectedCells.Count == 1)
            {
                if (e.Cell.Selected && e.Cell.RowIndex % 2 == 0)
                {
                    dgItemList.DefaultCellStyle.SelectionBackColor = SystemColors.ControlLight;
                }
                else
                {
                    if (!e.Cell.Selected || e.Cell.RowIndex % 2 != 1)
                    {
                        return;
                    }
                    dgItemList.DefaultCellStyle.SelectionBackColor = Color.FromArgb(160, 225, 225);
                }
            }
            else
                dgItemList.DefaultCellStyle.SelectionBackColor = Color.Khaki;
        }
 
        private void DgItemList_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int columnIndex = e.ColumnIndex;
                int rowIndex = e.RowIndex;
                if (cellEditedFlag)
                {
                    DataRow dataRow1 = (DataRow)null;
                    foreach (DataRow dataRow2 in CurrentTable.Rows)
                    {
                        if (dataRow2["seq_id"].Equals(dgItemList[0, rowIndex].Value))
                        {
                            dataRow1 = dataRow2;
                            break;
                        }
                    }
                    if (dataRow1 != null)
                    {
                        string text = controlArg.Control.Text;
                        if (columnIndex == 1)
                            dataRow1["descr"] = text;
                        else if (columnIndex == 2)
                        {
                            Decimal num1 = CurrencyUtil.SafeToDecimal(text);
                            int num2 = Convert.ToInt32(dgItemList[3, rowIndex].Value.ToString());
                            Decimal num3 = num1 * (Decimal)num2 / new Decimal(11);
                            Decimal num4 = num1 * (Decimal)num2;
                            dgItemList[4, rowIndex].Value = num3;
                            dgItemList[5, rowIndex].Value = num4;
                            dataRow1["RRP"] = num1;
                            dataRow1["GST_collect"] = num3;
                            dataRow1["sales"] = num4;
                        }
                        else if (columnIndex == 3)
                        {
                            Decimal num1 = CurrencyUtil.SafeToDecimal(dgItemList[2, rowIndex].Value.ToString());
                            int num2 = Convert.ToInt32(text);
                            Decimal num3 = num1 * (Decimal)num2;
                            Decimal num4 = CurrencyUtil.SafeToDecimal(dgItemList[4, rowIndex].Value.ToString());
                            if (num4 > new Decimal(0))
                                num4 = num3 / new Decimal(11);
                            dgItemList[4, rowIndex].Value = num4;
                            dgItemList[5, rowIndex].Value = num3;
                            dataRow1["GST_collect"] = num4;
                            dataRow1["number_items"] = num2;
                            dataRow1["sales"] = num3;
                        }
                        else if (columnIndex == 4)
                        {
                            Decimal num = CurrencyUtil.SafeToDecimal(text);
                            dataRow1["GST_collect"] = num;
                        }
                        Decimal total = new Decimal(0);
                        Decimal subTotal = new Decimal(0);
                        foreach (DataRow dataRow2 in CurrentTable.Rows)
                        {
                            total += Convert.ToDecimal(dataRow2["sales"].ToString());
                            subTotal += (Decimal)dataRow2["sales"];
                        }
                        lblTotal.Text = string.Format(UIStyles.CurrencyFormat, total);
                        CustomerDisplayItem(dataRow1["descr"].ToString(), (Decimal)dataRow1["sales"], subTotal);
                    }
                }
                cellEditedFlag = false;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Console.WriteLine(message);
                StartNewSale();
            }
        }

        private void DgItemList_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgItemList.SelectedCells.Count != 6)
                return;
            objCurSeq_id = dgItemList[0, e.RowIndex].Value;
        }
         
        private void DgItemList_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Return)
            {
                return;
            }
            StartNewSale();
        }
         
        //Scanner in action
        private void TxtCat_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                bool isEndKey = e.KeyCode == Keys.Oem3 || e.KeyCode == Keys.Add;
                if (HandleFunctionKeys(e))
                    return;
                else if  (e.KeyCode == Keys.Tab)
                {
                    if (rbtCash.Checked)
                    {
                        rBtnoCash.Checked = true;
                    }
                    else
                    {
                        rbtCash.Checked = true;
                    }
                }
                else if (isEndKey && !endOfSaleFlag)
                {
                    CalculateChange();
                }
                else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    //SwipeLeft(); // placeholder for navigation
                }
                else if (isEndKey && endOfSaleFlag)
                {
                    EndOfSales();
                }
                else if (IsShortcutKey(e.KeyCode))
                {
                    ShortCutKeyUp(e);
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    TogglePaymentMethod();
                }
                else if (e.KeyCode == Keys.Return && txtCat.Text.Length > 1)
                {
                    ProcessBarcodeEntry(txtCat.Text.Trim());
                }
                else if (IsNumericKey(e) && dgItemList.Rows.Count > 0 && !e.Control)
                {
                    ProcessCashInputKey(e);
                }
                else if (e.KeyCode == Keys.Enter && !string.IsNullOrEmpty(controlstring))
                {
                    Console.WriteLine("Applying control quantity: " + controlstring);
                    ApplyControlQuantity();
                    return;
                }
                else if (e.Control)
                {
                    Console.WriteLine("Control key pressed: " + e.KeyCode);
                    CaptureControlInput(e);
                }

            }

            catch (Exception ex)
            {
                ErrorLogWriter.Instance.Log(ex, "Error in TxtCat_KeyUp: " + ex.Message);
                StartNewSale();
            }
        }

        private void BtnSubagent_Click(object sender, EventArgs e)
        {
            FrmSubagent agentform = new FrmSubagent();
            AddOwnedForm(agentform);
            agentform.Show();
        }

        private void Rich_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            {
                System.Windows.Forms.Application.Exit();
            }
        }

        private void BtnQuit_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void CheckCards_CheckedChanged(object sender, EventArgs e)
        {
            this.ActiveControl = txtAmount;
        }

        private void CustomButtonConfig_Click(object sender, EventArgs e)
        {
            EnterConfig();
        }

        private void FrmPos_Shown(object sender, EventArgs e)
        {
            txtCat.Focus();
        }
    }
}
