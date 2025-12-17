using System;
using System.Data;
using System.Windows.Forms;

namespace QiPOS
{
    public sealed class FrmNewItem : Form
    {
        public string newBarCode;
        public int stock_id;
        private DataTable accountDB;
        private Label lbl1;
        private Label lbl6;
        private Label lbl5;
        private Label lbl4;
        private Label lbl3;
        private Label lbl2;
        private Label lbl7;
        private ComboBox cbxSupplier;
        private ComboBox cbxCat;
        private TextBox TxtGST;
        private TextBox TxtRRP;
        private TextBox TxtCost;
        private TextBox TxtDesc;
        private TextBox TxtBarcode;
        private CheckBox cbTax;
        private Label lblTitle;
        private Label lbl8;
        private TextBox TxtBottom;
        private TextBox TxtOnHand;
        private Label lbl9;
        private CustomButton customButton1;
        private CustomButton customButton2;
        private Panel pnlItems;

        public FrmNewItem()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the form
        /// (stock magically appear)
        /// (stocking up cost less)
        /// -- PW
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmNewItem_Load(object sender, EventArgs e)
        {
            MagicStockReplenish();
            if (stock_id > 0)
            {
                Text = "Edit Stock Item";
                lblTitle.Text = "Edit Stock Item";
                EditInitiate();
            }
            else
            {
                TxtBarcode.Text = newBarCode;
                AddNewInitiate();
            }
        }

        private void MagicStockReplenish()
        {
            //create more stock from thin air!
        }

        private void EditInitiate()
        {
            Connect connect = new Connect();
            string queryStr1 = "SELECT * FROM pos_stock WHERE stock_id=" + stock_id;
            connect.QueryTable(queryStr1);
            DataTable dataTable = connect.aTable;
            TxtBarcode.Text = dataTable.Rows[0]["barcode"].ToString();
            newBarCode = TxtBarcode.Text;
            string queryStr2 = "SELECT * FROM account_list WHERE acc_type_id=5 ORDER BY acc_name";
            connect.QueryTable(queryStr2);
            cbxCat.DataSource = connect.aTable;
            cbxCat.DisplayMember = "acc_name";
            cbxCat.ValueMember = "acc_id";
            accountDB = connect.aTable;
            string queryStr3 = "SELECT stock_id, account_list.acc_number, account_list.acc_id, card_id FROM pos_stock join account_list on (pos_stock.acc_number=account_list.acc_number)WHERE acc_type_id=5 and  stock_id=" + stock_id.ToString();
            connect.QueryTable(queryStr3);
            DataRow dataRow = connect.aTable.Rows[0];
            for (int index = 0; index < cbxCat.Items.Count; ++index)
            {
                if (((DataRowView)cbxCat.Items[index]).Row["acc_id"].ToString().Equals(dataRow["acc_id"].ToString()))
                {
                    cbxCat.SelectedIndex = index;
                    break;
                }
            }
            string queryStr4 = "SELECT card_id, name FROM account_cards WHERE acc_id=" + cbxCat.SelectedValue + " ORDER BY name";
            connect.QueryTable(queryStr4);
            cbxSupplier.DataSource = connect.aTable;
            cbxSupplier.DisplayMember = "name";
            cbxSupplier.ValueMember = "card_id";
            for (int index = 0; index < cbxSupplier.Items.Count; ++index)
            {
                if (((DataRowView)cbxSupplier.Items[index]).Row["card_id"].ToString().Equals(dataRow["card_id"].ToString()))
                {
                    cbxSupplier.SelectedIndex = index;
                    break;
                }
            }
            TxtDesc.Text = dataTable.Rows[0]["descr"].ToString();
            TxtCost.Text = string.Format("{0:C}", dataTable.Rows[0]["cost"]);
            TxtRRP.Text = string.Format("{0:C}", dataTable.Rows[0]["RRP"]);
            TxtGST.Text = string.Format("{0:C}", dataTable.Rows[0]["GST_collect"]);
            TxtOnHand.Text = dataTable.Rows[0]["stk_on_hand"].ToString();
            TxtBottom.Text = dataTable.Rows[0]["stk_bottom"].ToString();
            if (dataTable.Rows[0]["taxable"].ToString() == "1")
                cbTax.Checked = true;
            else
                cbTax.Checked = false;
        }

        private void AddNewInitiate()
        {
            Connect connect = new Connect();
            string queryStr1 = "SELECT * FROM account_list WHERE acc_type_id=5 ORDER BY acc_name";
            connect.QueryTable(queryStr1);
            cbxCat.DataSource = connect.aTable;
            cbxCat.DisplayMember = "acc_name";
            cbxCat.ValueMember = "acc_id";
            accountDB = connect.aTable;
            string queryStr2 = "SELECT card_id, name FROM account_cards WHERE acc_id=" + cbxCat.SelectedValue + " ORDER BY name";
            connect.QueryTable(queryStr2);
            cbxSupplier.DataSource = connect.aTable;
            cbxSupplier.DisplayMember = "name";
            cbxSupplier.ValueMember = "card_id";
            TxtDesc.Text = "";
            TxtCost.Text = string.Format("{0:C}", 0);
            TxtRRP.Text = string.Format("{0:C}", 0);
            TxtGST.Text = string.Format("{0:C}", 0);
            TxtOnHand.Text = "0";
            TxtBottom.Text = "0";
            cbTax.Checked = true;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Connect connect = new Connect();
            string text = TxtDesc.Text;
            string str1 = connect.AddBackslash(text);
            string str2 = TxtCost.Text.Substring(1);
            string str3 = TxtRRP.Text.Substring(1);
            string str4 = TxtGST.Text.Substring(1);
            Decimal num1 = Convert.ToDecimal(str2) / new Decimal(11);
            if (str4 == "0.00")
                num1 = new Decimal(0);
            int num2 = 0;
            if (cbTax.Checked)
                num2 = 1;
            int num3 = 0;
            foreach (DataRow dataRow in accountDB.Rows)
            {
                if (dataRow["acc_id"].ToString().Equals(cbxCat.SelectedValue.ToString()))
                {
                    num3 = (int)dataRow["acc_number"];
                    break;
                }
            }
            string str5 = "0";
            if (cbxSupplier.SelectedValue != null)
                str5 = cbxSupplier.SelectedValue.ToString();
            if (stock_id > 0)
            {
                if (newBarCode == TxtBarcode.Text.Trim())
                {
                    string queryStr1 = "SELECT * FROM pos_stock WHERE stock_id=" + stock_id;
                    connect.QueryTable(queryStr1);
                    Decimal num4 = new Decimal(0);
                    if (connect.aTable.Rows.Count > 0)
                        num4 = (Decimal)connect.aTable.Rows[0]["RRP"];
                    string queryStr2 = "UPDATE pos_stock SET descr='" + str1 + "', acc_number=" + num3.ToString()
                        + ", cat_id=" + cbxCat.SelectedValue.ToString() + ", card_id=" + str5 + ", cost=" + str2
                        + ", RRP=" + str3 + ", GST_paid=" + num1.ToString() + ", GST_collect=" + str4 + ", stk_on_hand=" +
                        TxtOnHand.Text + ",  stk_bottom=" + TxtBottom.Text + ", taxable=" + num2.ToString() +
                        ", lastRRP=" + num4.ToString() + " WHERE stock_id=" + stock_id.ToString();
                    connect.NoReturnQuery(queryStr2);
                }
                else
                {
                    string queryStr1 = "SELECT * FROM pos_stock WHERE barcode='" + TxtBarcode.Text.Trim() + "'";
                    connect.QueryTable(queryStr1);
                    if (connect.aTable.Rows.Count > 0)
                    {
                        MessageBox.Show("Existed Barcode");
                    }
                    else
                    {
                        DateTime now = DateTime.Now;
                        string queryStr2 = "INSERT INTO pos_stock (barcode, descr, acc_number, cat_id, card_id, cost, RRP, GST_paid, GST_collect, entered_date, last_sold_date, stk_on_hand, stk_bottom, taxable, lastRRP) VALUES ('" + TxtBarcode.Text.Trim() + "', '" + str1 + "', "
                            + num3.ToString() + ", " + cbxCat.SelectedValue.ToString() + ", " + str5 + ", " + str2 + ", " + str3 + ", " + num1.ToString() + ", " + str4 + ", '" + now.ToString("yyyy-MM-dd") + "' , '1900-01-01 00:00:00', "
                            + TxtOnHand.Text + ", " + TxtBottom.Text + ", " + num2.ToString() + ", " + str3 + ")";
                        connect.NoReturnQuery(queryStr2);
                    }
                }
            }
            else
            {
                DateTime now = DateTime.Now;
                string queryStr = "INSERT INTO pos_stock (barcode, descr, acc_number, cat_id, card_id, cost, RRP, GST_paid, GST_collect, entered_date, last_sold_date, stk_on_hand, stk_bottom, taxable, lastRRP) VALUES ('" + TxtBarcode.Text.Trim() + "', '" + str1 + "', " + num3.ToString() + ", " + cbxCat.SelectedValue.ToString() + ", " + str5 + ", " + str2 + ", " + str3 + ", " + num1.ToString() + ", " + str4 + ", '" + now.ToString("yyyy-MM-dd") + "' , '1900-01-01 00:00:00', " + TxtOnHand.Text + ", " + TxtBottom.Text + ", " + num2.ToString() + ", " + str3 + ")";
                connect.NoReturnQuery(queryStr);
            }
            DialogResult = DialogResult.Yes;
            Close();
            base.Dispose();
        }

        private void BtnAbort_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            Close();
            base.Dispose();
        }

        private void FrmNewItem_FormClosed(object sender, FormClosedEventArgs e)
        {
            base.Dispose();
        }

        private void CbxCat_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connect connect = new Connect();
            string s = cbxCat.SelectedValue.ToString();
            if (!char.IsDigit(s, 0))
                s = "0";
            string queryStr = "SELECT card_id, name FROM account_cards WHERE acc_id=" + s + " ORDER BY name";
            connect.QueryTable(queryStr);
            cbxSupplier.DataSource = connect.aTable;
            cbxSupplier.DisplayMember = "name";
            cbxSupplier.ValueMember = "card_id";
        }

        private void TxtRRP_KeyPress(object sender, KeyPressEventArgs e)
        {
            Decimal num1 = CurrencyInputValidation(TxtRRP, e);
            TxtGST.Text = string.Format("{0:C}", (num1 / new Decimal(11)));
            Decimal num2 = Convert.ToDecimal(accountDB.Rows[cbxCat.SelectedIndex]["markup_rate"].ToString());
            TxtCost.Text = string.Format("{0:C}", (num1 * (new Decimal(100) - num2) / new Decimal(100)));
        }

        private void TxtGST_KeyPress(object sender, KeyPressEventArgs e)
        {
            CurrencyInputValidation(TxtGST, e);
        }

        private void TxtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            CurrencyInputValidation(TxtCost, e);
        }

        private Decimal CurrencyInputValidation(TextBox TxtBox, KeyPressEventArgs e)
        {
            string str = TxtBox.Text.Trim().Replace(".", "").Replace("$", "").Replace(",", "");
            int length = str.Length;
            Decimal num1 = new Decimal(0);
            if (str != "")
                num1 = Convert.ToDecimal(str);
            Decimal num2 = num1 / new Decimal(100);
            int num3 = (int)e.KeyChar;
            int num4;
            switch (num3)
            {
                case 46:
                    e.Handled = true;
                    num2 *= new Decimal(100);
                    goto label_15;
                case 45:
                    num4 = !(num2 != new Decimal(0)) ? 1 : 0;
                    break;
                default:
                    num4 = 1;
                    break;
            }
            if (num4 == 0)
            {
                e.Handled = true;
                num2 = new Decimal(0) - num2;
            }
            else if (num3 == 8 || num3 == 27 || num3 == 32 || length > 8)
            {
                num2 = new Decimal(0);
                e.Handled = true;
            }
            else if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) || (num3 == 47 || num3 < 45) || num3 > 58)
            {
                e.Handled = true;
            }
            else
            {
                if (TxtBox.SelectionLength == TxtBox.TextLength)
                    num2 = new Decimal(0);
                int num5 = num3 - 48;
                num2 = !(num2 >= new Decimal(0)) ? (num2 * new Decimal(1000) - (Decimal)num5) / new Decimal(100) : (num2 * new Decimal(1000) + (Decimal)num5) / new Decimal(100);
                e.Handled = true;
            }
        label_15:
            TxtBox.Text = string.Format("{0:C}", num2);
            return num2;
        }

        private void TxtOnHand_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitalValidation(e);
        }

        private void TxtBottom_KeyPress(object sender, KeyPressEventArgs e)
        {
            DigitalValidation(e);
        }

        private void DigitalValidation(KeyPressEventArgs e)
        {
            int num = (int)e.KeyChar;
            if (num >= 48 && num <= 57 || num <= 31)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TxtOnHand_Leave(object sender, EventArgs e)
        {
            if (TxtOnHand.Text.Trim().Length != 0)
                return;
            TxtOnHand.Text = "0";
        }

        private void TxtBottom_Leave(object sender, EventArgs e)
        {
            if (TxtBottom.Text.Trim().Length != 0)
                return;
            TxtBottom.Text = "0";
        }

        private void TxtCost_Leave(object sender, EventArgs e)
        {
            Dollar_empty_validation(TxtCost);
        }

        private void TxtRRP_Leave(object sender, EventArgs e)
        {
            Dollar_empty_validation(TxtRRP);
        }

        private void TxtGST_Leave(object sender, EventArgs e)
        {
            Dollar_empty_validation(TxtGST);
        }

        private void Dollar_empty_validation(TextBox tBox)
        {
            string str = tBox.Text.Trim();
            if (str.Length == 0)
                tBox.Text = "$0.00";
            else if (str.Substring(0, 1) != "$")
                tBox.Text = "$" + tBox.Text;
        }

        private void CbTax_CheckedChanged(object sender, EventArgs e)
        {
            if (cbTax.Checked)
                TxtGST.Text = string.Format("{0:C}", (Convert.ToDecimal(TxtRRP.Text.Trim().Replace("$", "").Replace(",", "")) / new Decimal(11)));
            else
                TxtGST.Text = "$0.00";
        }

        private void TxtBarcode_TextChanged(object sender, EventArgs e)
        {
            string str = TxtBarcode.Text.Trim();
            if (str.Length <= 1)
                return;
            string queryStr = "SELECT *  FROM [pos_stock] WHERE [barcode]='" + str + "'";
            Connect connect = new Connect();
            connect.QueryTable(queryStr);
            if (connect.aTable.Rows.Count > 0)
            {
                stock_id = (int)connect.aTable.Rows[0]["stock_id"];
                EditInitiate();
            }
            else
            {
                stock_id = 0;
                AddNewInitiate();
            }
        }

        private void InitializeComponent()
        {
            this.pnlItems = new System.Windows.Forms.Panel();
            this.TxtBottom = new System.Windows.Forms.TextBox();
            this.TxtOnHand = new System.Windows.Forms.TextBox();
            this.lbl9 = new System.Windows.Forms.Label();
            this.lbl8 = new System.Windows.Forms.Label();
            this.cbTax = new System.Windows.Forms.CheckBox();
            this.cbxSupplier = new System.Windows.Forms.ComboBox();
            this.cbxCat = new System.Windows.Forms.ComboBox();
            this.TxtGST = new System.Windows.Forms.TextBox();
            this.TxtBarcode = new System.Windows.Forms.TextBox();
            this.TxtRRP = new System.Windows.Forms.TextBox();
            this.TxtCost = new System.Windows.Forms.TextBox();
            this.TxtDesc = new System.Windows.Forms.TextBox();
            this.lbl7 = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.customButton1 = new QiPOS.CustomButton();
            this.customButton2 = new QiPOS.CustomButton();
            this.pnlItems.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlItems
            // 
            this.pnlItems.BackColor = System.Drawing.Color.LightYellow;
            this.pnlItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlItems.Controls.Add(this.TxtBottom);
            this.pnlItems.Controls.Add(this.TxtOnHand);
            this.pnlItems.Controls.Add(this.lbl9);
            this.pnlItems.Controls.Add(this.lbl8);
            this.pnlItems.Controls.Add(this.cbTax);
            this.pnlItems.Controls.Add(this.cbxSupplier);
            this.pnlItems.Controls.Add(this.cbxCat);
            this.pnlItems.Controls.Add(this.TxtGST);
            this.pnlItems.Controls.Add(this.TxtBarcode);
            this.pnlItems.Controls.Add(this.TxtRRP);
            this.pnlItems.Controls.Add(this.TxtCost);
            this.pnlItems.Controls.Add(this.TxtDesc);
            this.pnlItems.Controls.Add(this.lbl7);
            this.pnlItems.Controls.Add(this.lbl6);
            this.pnlItems.Controls.Add(this.lbl5);
            this.pnlItems.Controls.Add(this.lbl4);
            this.pnlItems.Controls.Add(this.lbl3);
            this.pnlItems.Controls.Add(this.lbl2);
            this.pnlItems.Controls.Add(this.lbl1);
            this.pnlItems.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlItems.Location = new System.Drawing.Point(53, 48);
            this.pnlItems.Name = "pnlItems";
            this.pnlItems.Size = new System.Drawing.Size(674, 334);
            this.pnlItems.TabIndex = 0;
            // 
            // TxtBottom
            // 
            this.TxtBottom.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBottom.Location = new System.Drawing.Point(280, 284);
            this.TxtBottom.Name = "TxtBottom";
            this.TxtBottom.Size = new System.Drawing.Size(100, 33);
            this.TxtBottom.TabIndex = 8;
            this.TxtBottom.Text = "0";
            this.TxtBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtBottom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtBottom_KeyPress);
            this.TxtBottom.Leave += new System.EventHandler(this.TxtBottom_Leave);
            // 
            // TxtOnHand
            // 
            this.TxtOnHand.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtOnHand.Location = new System.Drawing.Point(53, 286);
            this.TxtOnHand.Name = "TxtOnHand";
            this.TxtOnHand.Size = new System.Drawing.Size(100, 33);
            this.TxtOnHand.TabIndex = 7;
            this.TxtOnHand.Text = "0";
            this.TxtOnHand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtOnHand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtOnHand_KeyPress);
            this.TxtOnHand.Leave += new System.EventHandler(this.TxtOnHand_Leave);
            // 
            // lbl9
            // 
            this.lbl9.AutoSize = true;
            this.lbl9.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl9.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl9.Location = new System.Drawing.Point(275, 258);
            this.lbl9.Name = "lbl9";
            this.lbl9.Size = new System.Drawing.Size(134, 25);
            this.lbl9.TabIndex = 2009;
            this.lbl9.Text = "Stock Bottom";
            // 
            // lbl8
            // 
            this.lbl8.AutoSize = true;
            this.lbl8.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl8.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl8.Location = new System.Drawing.Point(48, 258);
            this.lbl8.Name = "lbl8";
            this.lbl8.Size = new System.Drawing.Size(147, 25);
            this.lbl8.TabIndex = 2008;
            this.lbl8.Text = "Stock On Hand";
            // 
            // cbTax
            // 
            this.cbTax.AutoSize = true;
            this.cbTax.Checked = true;
            this.cbTax.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbTax.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTax.Location = new System.Drawing.Point(505, 284);
            this.cbTax.Name = "cbTax";
            this.cbTax.Size = new System.Drawing.Size(98, 29);
            this.cbTax.TabIndex = 9;
            this.cbTax.Text = "Taxable";
            this.cbTax.UseVisualStyleBackColor = true;
            this.cbTax.CheckedChanged += new System.EventHandler(this.CbTax_CheckedChanged);
            // 
            // cbxSupplier
            // 
            this.cbxSupplier.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxSupplier.FormattingEnabled = true;
            this.cbxSupplier.Location = new System.Drawing.Point(365, 136);
            this.cbxSupplier.Name = "cbxSupplier";
            this.cbxSupplier.Size = new System.Drawing.Size(270, 33);
            this.cbxSupplier.TabIndex = 3;
            // 
            // cbxCat
            // 
            this.cbxCat.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxCat.FormattingEnabled = true;
            this.cbxCat.Location = new System.Drawing.Point(53, 136);
            this.cbxCat.MaxDropDownItems = 12;
            this.cbxCat.Name = "cbxCat";
            this.cbxCat.Size = new System.Drawing.Size(270, 33);
            this.cbxCat.TabIndex = 2;
            this.cbxCat.SelectedIndexChanged += new System.EventHandler(this.CbxCat_SelectedIndexChanged);
            // 
            // TxtGST
            // 
            this.TxtGST.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtGST.Location = new System.Drawing.Point(505, 211);
            this.TxtGST.Name = "TxtGST";
            this.TxtGST.Size = new System.Drawing.Size(130, 33);
            this.TxtGST.TabIndex = 6;
            this.TxtGST.Text = "$0.00";
            this.TxtGST.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtGST.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtGST_KeyPress);
            this.TxtGST.Leave += new System.EventHandler(this.TxtGST_Leave);
            // 
            // TxtBarcode
            // 
            this.TxtBarcode.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.TxtBarcode.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBarcode.Location = new System.Drawing.Point(445, 15);
            this.TxtBarcode.Name = "TxtBarcode";
            this.TxtBarcode.Size = new System.Drawing.Size(190, 33);
            this.TxtBarcode.TabIndex = 110;
            this.TxtBarcode.TabStop = false;
            this.TxtBarcode.TextChanged += new System.EventHandler(this.TxtBarcode_TextChanged);
            // 
            // TxtRRP
            // 
            this.TxtRRP.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtRRP.Location = new System.Drawing.Point(280, 211);
            this.TxtRRP.Name = "TxtRRP";
            this.TxtRRP.Size = new System.Drawing.Size(130, 33);
            this.TxtRRP.TabIndex = 4;
            this.TxtRRP.Text = "$0.00";
            this.TxtRRP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtRRP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtRRP_KeyPress);
            this.TxtRRP.Leave += new System.EventHandler(this.TxtRRP_Leave);
            // 
            // TxtCost
            // 
            this.TxtCost.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtCost.Location = new System.Drawing.Point(53, 211);
            this.TxtCost.Name = "TxtCost";
            this.TxtCost.Size = new System.Drawing.Size(130, 33);
            this.TxtCost.TabIndex = 5;
            this.TxtCost.Text = "$0.00";
            this.TxtCost.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TxtCost.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtCost_KeyPress);
            this.TxtCost.Leave += new System.EventHandler(this.TxtCost_Leave);
            // 
            // TxtDesc
            // 
            this.TxtDesc.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtDesc.Location = new System.Drawing.Point(53, 64);
            this.TxtDesc.Name = "TxtDesc";
            this.TxtDesc.Size = new System.Drawing.Size(582, 33);
            this.TxtDesc.TabIndex = 1;
            // 
            // lbl7
            // 
            this.lbl7.AllowDrop = true;
            this.lbl7.AutoSize = true;
            this.lbl7.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl7.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl7.Location = new System.Drawing.Point(500, 183);
            this.lbl7.Name = "lbl7";
            this.lbl7.Size = new System.Drawing.Size(48, 25);
            this.lbl7.TabIndex = 2007;
            this.lbl7.Text = "GST";
            // 
            // lbl6
            // 
            this.lbl6.AutoSize = true;
            this.lbl6.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl6.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl6.Location = new System.Drawing.Point(275, 183);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(48, 25);
            this.lbl6.TabIndex = 2006;
            this.lbl6.Text = "RRP";
            // 
            // lbl5
            // 
            this.lbl5.AutoSize = true;
            this.lbl5.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl5.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl5.Location = new System.Drawing.Point(53, 183);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(51, 25);
            this.lbl5.TabIndex = 2005;
            this.lbl5.Text = "Cost";
            // 
            // lbl4
            // 
            this.lbl4.AutoSize = true;
            this.lbl4.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl4.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl4.Location = new System.Drawing.Point(360, 108);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(87, 25);
            this.lbl4.TabIndex = 2004;
            this.lbl4.Text = "Supplier";
            // 
            // lbl3
            // 
            this.lbl3.AutoSize = true;
            this.lbl3.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl3.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl3.Location = new System.Drawing.Point(53, 108);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(94, 25);
            this.lbl3.TabIndex = 2003;
            this.lbl3.Text = "Category";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl2.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl2.Location = new System.Drawing.Point(53, 36);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(114, 25);
            this.lbl2.TabIndex = 2002;
            this.lbl2.Text = "Description";
            // 
            // lbl1
            // 
            this.lbl1.AutoSize = true;
            this.lbl1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl1.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbl1.Location = new System.Drawing.Point(323, 18);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(85, 25);
            this.lbl1.TabIndex = 2001;
            this.lbl1.Text = "Barcode";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Verdana", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(253, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(254, 25);
            this.lblTitle.TabIndex = 2000;
            this.lblTitle.Text = "Add New Stock Item";
            // 
            // customButton1
            // 
            this.customButton1.BackColor = System.Drawing.SystemColors.Control;
            this.customButton1.CornerRadius = 40;
            this.customButton1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton1.ForeColor = System.Drawing.Color.Blue;
            this.customButton1.Location = new System.Drawing.Point(414, 399);
            this.customButton1.Name = "customButton1";
            this.customButton1.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton1.Size = new System.Drawing.Size(165, 40);
            this.customButton1.TabIndex = 2001;
            this.customButton1.Text = "Abort";
            this.customButton1.Click += new System.EventHandler(this.BtnAbort_Click);
            // 
            // customButton2
            // 
            this.customButton2.BackColor = System.Drawing.SystemColors.Control;
            this.customButton2.CornerRadius = 40;
            this.customButton2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.customButton2.ForeColor = System.Drawing.Color.Blue;
            this.customButton2.Location = new System.Drawing.Point(208, 399);
            this.customButton2.Name = "customButton2";
            this.customButton2.RoundCorners = ((QiPOS.Corners)((((QiPOS.Corners.TopLeft | QiPOS.Corners.TopRight) 
            | QiPOS.Corners.BottomLeft) 
            | QiPOS.Corners.BottomRight)));
            this.customButton2.Size = new System.Drawing.Size(165, 40);
            this.customButton2.TabIndex = 2001;
            this.customButton2.Text = "Save";
            this.customButton2.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FrmNewItem
            // 
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(784, 464);
            this.Controls.Add(this.customButton2);
            this.Controls.Add(this.customButton1);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pnlItems);
            this.Margin = new System.Windows.Forms.Padding(12);
            this.Name = "FrmNewItem";
            this.Text = "Add New Stock Item";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmNewItem_FormClosed);
            this.Load += new System.EventHandler(this.FrmNewItem_Load);
            this.pnlItems.ResumeLayout(false);
            this.pnlItems.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}

